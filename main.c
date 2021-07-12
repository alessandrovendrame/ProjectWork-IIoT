/*
 * File:   main.c
 * Author: Alessandro Vendrame
 *
 * Created on June 1, 2021, 2:21 PM
 */

//--------------------------------------------------------------
//----------------------- CONFIGURAZIONE -----------------------
//--------------------------------------------------------------

// DEVCFG3
#pragma config USERID = 0xFFFF         // Enter Hexadecimal value (Enter Hexadecimal value)

// DEVCFG2
#pragma config FPLLIDIV = DIV_2        // PLL Input Divider (12x Divider)
#pragma config FPLLMUL = MUL_20        // PLL Multiplier (24x Multiplier)
#pragma config UPLLIDIV = DIV_12       // USB PLL Input Divider (12x Divider)
#pragma config UPLLEN = OFF            // USB PLL Enable (Disabled and Bypassed)
#pragma config FPLLODIV = DIV_1        // System PLL Output Clock Divider (PLL Divide by 256)

// DEVCFG1
#pragma config FNOSC = PRIPLL          // Oscillator Selection Bits (Fast RC Osc w/Div-by-N (FRCDIV))
#pragma config FSOSCEN = ON            // Secondary Oscillator Enable (Enabled)
#pragma config IESO = ON               // Internal/External Switch Over (Enabled)
#pragma config POSCMOD = HS            // Primary Oscillator Configuration (Primary osc disabled)
#pragma config OSCIOFNC = ON           // CLKO Output Signal Active on the OSCO Pin (Enabled)
#pragma config FPBDIV = DIV_8          // Peripheral Clock Divisor (Pb_Clk is Sys_Clk/8)
#pragma config FCKSM = CSDCMD          // Clock Switching and Monitor Selection (Clock Switch Disable, FSCM Disabled)
#pragma config WDTPS = PS1048576       // Watchdog Timer Postscaler (1:1048576)
#pragma config FWDTEN = OFF            // Watchdog Timer Enable (WDT Enabled)

// DEVCFG0
#pragma config DEBUG = OFF             // Background Debugger Enable (Debugger is disabled)
#pragma config ICESEL = ICS_PGx2       // ICE/ICD Comm Channel Select (ICE EMUC2/EMUD2 pins shared with PGC2/PGD2)
#pragma config PWP = OFF               // Program Flash Write Protect (Disable)
#pragma config BWP = OFF               // Boot Flash Write Protect bit (Protection Disabled)
#pragma config CP = OFF                // Code Protect (Protection Disabled)

// #pragma config statements should precede project file includes.
// Use project enums instead of #define for ON and OFF.


//---------------------------------------------------------------------------------
//----------------------- DICHIARAZIONE COSTANTI E LIBRERIE -----------------------
//---------------------------------------------------------------------------------

#include <xc.h>
#include <sys/attribs.h>                  //registro dell'interrupt
#include<stdio.h>
#include <stdlib.h>

#define CLASS_ID 0x10
#define BOTTONE_1 PORTDbits.RD5
#define ENABLE PORTGbits.RG8   //D10
#define RS PORTGbits.RG9   //D11

#define RE PORTDbits.RD6
#define DE PORTDbits.RD7

#define SCR_ON 0x0C
#define CLR_D 0x01
#define L_L1 0x80
#define L_L2 0xC0
#define L_L3 0X90
#define L_L4 0xD0
#define NOME_AULA "L10"



//----------------------------------------------------------------------
//----------------------- DICHIARAZIONE FUNZIONI -----------------------
//----------------------------------------------------------------------

void initSys();
void sendData(char x);
void invia(char x);
void ricevi();
char checkDestination(char id);
void saveString(char *s);
void checkButton();
void initLCD();
void sendCmd(char rs, char dato);
void sendCfg(char conf);
void sendLetter(char value);
void sendString(char *string);
void _delay(int millisecond);
void splitTeacher(char *s);
void resetString(char *s);
void pulseE();

void setLesson();


//-------------------------------------------------------------------------------
//----------------------- DICHIARAZIONE VARIABILI GLOBALI -----------------------
//-------------------------------------------------------------------------------

int baudRate = 0;
int count=0,millis = 0;
char go = 0;
char string[50];
char subject[50];
char prof[50];
char nProf[50];
char cProf[50];
int flag=0;
char duration[20];
char carattere,typeOfMessage;
int parteInviata = 0;
int nDataReceived = 0;
int indice=0;
int time = 0;
int t3_count=0;
int ricevuti=1;
char bottone1 = -1 , o_bottone1 = 0, checkb1 = 0, contButton = 0;


//----------------------------------------------------
//----------------------- MAIN -----------------------
//----------------------------------------------------

void main(void) {
    
  

    initSys();                           //inizializzo il microcontrollore
    //invia('D');
    char z = 32;
    
    while(1)                             //loop infinito
    {
        checkButton();
        if(time == 0){
            T3CONbits.TON = 0;
            sendCfg(CLR_D);
            sendCfg(L_L1);
            sendString("AULA LIBERA");
            time = 1;
        }        
    }
}

//-----------------------------------------------------------------
//----------------------- SVILUPPO FUNZIONI -----------------------
//-----------------------------------------------------------------

void initSys()                          //funzione che inizializza i bit del microcontrollore
{
    
    TRISBbits.TRISB1= 0;
    TRISBbits.TRISB2= 0;    
    TRISBbits.TRISB3= 0;
    TRISBbits.TRISB4= 0;

    TRISG = 0x0000;
    TRISD = 0x0001;
    
    TRISDbits.TRISD5 = 1;
 
    //UART TX
    
    U1MODEbits.BRGH = 0; 
    U1BRG = 64;
    U1MODEbits.PDSEL = 0;
    U1MODEbits.STSEL = 0;
    
    U1STAbits.UTXEN = 1;

    //UART RX
    
    //Enabling Interrupt on receive
    
    IEC0bits.U1RXIE = 1;
    IPC6bits.U1IP = 7;
    IPC6bits.U1IS = 0;
    U1STAbits.URXISEL = 0;
    ricevi();   
    //Enabling UART receiver
    U1STAbits.URXEN = 1;
    
    //Timer 2 CONF
    
    T2CONbits.TON = 0;                  //spengo il timer in modo che non inizi subito a contare
    T2CONbits.TCKPS = 7;                //imposto il prescaler al massimo impostando i 3 bit di TCKPS a 1
    
    PR2 = 2000;                         //il numero assegnato a PR2 è la soglia del clock, una volta raggiunto lancia l'interrupt
    TMR2 = 0;                           //faccio partire il timer2 da 0
    
    IPC2bits.T2IP = 7;                  //setto la priorità sull'interrupt, quindi se ne vengono lanciati due nello stesso momento
                                        //verrà eseguito prima quello con priorità più alta
    
    IFS0bits.T2IF = 0;                  //setto l'interrupt flag di timer2 a 0
    IEC0bits.T2IE = 1;
    
    U1MODEbits.ON = 1;

    //TIMER 3 CONF
    
    T3CONbits.TON = 0;                  //spengo il timer in modo che non inizi subito a contare
    T3CONbits.TCKPS = 7;                //imposto il prescaler al massimo impostando i 3 bit di TCKPS a 1
    
    PR3 = 39062;                        //il numero assegnato a PR3 è la soglia del clock, una volta raggiunto lancia l'interrupt
    TMR3 = 0;                           //faccio partire il timer3 da 0
    
    IPC3bits.T3IP = 7;                  //setto la priorità sull'interrupt, quindi se ne vengono lanciati due nello stesso momento
                                        //verrà eseguito prima quello con priorità più alta
    
    IFS0bits.T3IF = 0;
    IEC0bits.T3IE = 1;
    
    //Interrupt CONF
    
    INTCONSET = _INTCON_MVEC_MASK;      //abilita interrupt multivettore
    
    __builtin_enable_interrupts();      //abilità l'interruttore generale di tutti gli interrupt    
    
    initLCD();
    
    ENABLE = 1;
   
}

void sendData(char x){
    while(U1STAbits.UTXBF);
    U1TXREG = x;
}
void invia(char x){
    
    RE = 1;
    DE = 1;
    sendData(x);   
    T2CONbits.TON = 1; //fa partire il timer
}
void ricevi(){
    RE = 0;
    DE = 0;
}
char checkDestination(char id)
{
    if((id & 0xf0) == CLASS_ID){ // controllo dell indirizzo che viene inviato nel primo byte del pacchetto
      return 1;        //true
    }
    else return 0;      //false
}
void divideByType(){
    switch(typeOfMessage){
        case 0x0a: //materia
            saveString(subject); 
            break;
        case 0x0d:   //prof
            saveString(prof);
            splitTeacher(prof);
            break;
        case 0x0c: //durata
            saveString(duration);
            time = atoi(duration);
            time = time/60;
            snprintf(duration, sizeof(duration), "%d", time);
            setLesson();
            ricevuti++;
            break;
        
    }
}

void saveString(char *s){
    int y;
    for( y=1;y<indice-1;y++){
        s[y-1] = string[y]; //salva la stringa ricevuta nell'array dato in precedenza
        string[y]='\0';
    }
}

void splitTeacher(char *s){
    int iNome = 0, iCognome = 0, iFor;
    
    resetString(nProf);
    resetString(cProf);
    
    for(iFor = 0; s[iFor] != '\0'; iFor++){
        if(s[iFor] == (char)0x20){
            flag = 1;
            iFor++;
        }
        if(!flag){
            nProf[iNome] = s[iFor];
            iNome++;
        }else{
            cProf[iCognome] = s[iFor];
            iCognome++;
        }
    }
    
    flag = 0;
}

void _delay(int millisecond){
    int x = 2000;
    
    while(millisecond--){
        int i;
        for(i=0;i<x;i++);
    }
}

void checkButton(){
//BOTTONE 1
    
    bottone1 = !(BOTTONE_1); // CHECK LEVEL LOGIC RD2
    if(((bottone1 == 1)&&(o_bottone1 == 0)) || checkb1 == 1) {
       checkb1 = 1;
       bottone1 = !(BOTTONE_1);
       if(((bottone1 == 1) && (o_bottone1 == 0)) || contButton >= 1) {
           checkb1 = 0;
           contButton = 0;
           invia(CLASS_ID | 0x0F);   //quando viene premuto il bottone invia il dato
               //DATI = (IdPressedButton >> serialIndex) & 1;
                              
               //serialIndex++;
               
              // T3CONbits.TON = 1;        
           //sendLetter('D');
           
       }
    }
    
    o_bottone1 = bottone1;
}

void initLCD(){
    RS = 0;
    ENABLE = 0;
    
    _delay(20);
    
    ENABLE = 1;
    sendCfg(0x28);
    _delay(5);
    
    sendCfg(0x28);
    _delay(1);
    
    sendCfg(0x28);
    
    sendCfg(SCR_ON);
    _delay(5);
    sendCfg(SCR_ON);
    
    sendCfg(0x08); //SPEGNE
    sendCfg(0x0F); //ACCENDE
    sendCfg(CLR_D);
    
    _delay(5);   

    sendCfg(L_L1);
}

void sendCmd(char rs, char dato){
    ENABLE = 0;  
     _delay(5);
    PORTD = dato << 8;
    RS = rs;
    _delay(5);
    
    ENABLE = 1;
    
    _delay(5);
    
    ENABLE = 0;
    
}

void resetString(char *s){
    int k = 0;
    for(k;k<50;k++){
        s[k] = '\0';
    }
}

void sendCfg(char conf){
    sendCmd(0,(conf & 0xF0) >> 4);
    sendCmd(0,(conf & 0x0F));
}

void sendLetter(char value){
    char prova = (value & 0xF0) >> 4;
    
    sendCmd(1,(value & 0xF0) >> 4);
    sendCmd(1,(value & 0x0F));
}

void sendString(char *string){
    int i;
    
    for(i=0;string[i] != '\0'; i++){
        sendLetter(string[i]);
       // string[i] = '\0';
    }
}

void setLesson(){
    sendCfg(CLR_D);
    sendCfg(L_L1);
    sendString(NOME_AULA);
    sendCfg(L_L2);
    sendString(prof);
    sendCfg(L_L3);
    sendString(subject);
    sendCfg(L_L4);
    sendString(duration);
    sendString(" min");
   
    t3_count = 0;
    T3CONbits.TON = 1;
    
    resetString(prof);
    resetString(subject);
    resetString(duration);
}

// INTERRUPT

void __ISR(_UART_1_VECTOR, IPL7SRS) UartInterrupt(void)      //interrupt di ricezione
{    
    
        carattere = U1RXREG; //riceve il byte dal registro
        string[indice]=carattere; //inserisce il byte ricevuto in un array

        indice++;

        if(indice == 1){
            if(checkDestination(string[0]) == 0){ //controlla se il messaggio non è diretto a me
                indice=0; // se non lo è resetta l'indice
            }
            else{
                typeOfMessage = (char)(string[0] & 0x0f); //prende il tipo del messaggio(prof,materia,durata) dagli ultimi 4 bit del primo byte del pacchetto
            }
        }
        else{
            if(checkDestination(string[0]) == 1)
                if(string[indice-1] == '-'){ //quando arriva il byte con il carattere di fine stringa deciso '-'
                    divideByType(); // in base al tipo definito nel pacchetto smista nelle diverse variabili
                    indice=0;     
                }
        }

        IFS0bits.U1RXIF = 0;
       
}

void __ISR(_TIMER_2_VECTOR, IPL7SRS) T2Interrupt(void)      //interrupt di timer2
{    
    //LATDbits.LATD5 ^= 1;
    
    ++count;
    if(count >= 20){ //quando raggiunge il counter setta i pin in ricezione
        ricevi();//è stato fatto cosi perche senza un po di attesa l'invio dei dati non poteva avvenire
        T2CONbits.TON = 0;
        count=0;
    }
    
    IFS0bits.T2IF = 0;                                       //resetto l'interrupt del timer

}

void __ISR(_TIMER_3_VECTOR, IPL7SRS) T3Interrupt(void)      //interrupt di timer3
{
    t3_count ++;
    
    if(t3_count > 59)
    {
        LATDbits.LATD1 ^= 1;
        
        time--;
        snprintf(duration, sizeof(duration), "%d", time);
        
        sendCfg(L_L4);
        sendString("                ");
        sendCfg(L_L4);
        sendString(duration);
        sendString(" min");
        
        t3_count = 0;
    }
    IFS0bits.T3IF = 0;
}
