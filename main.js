'use strict';

require('dotenv').config({path: __dirname + '/.env'});

//#region Inizializzazione costanti

const SerialPort = require("serialport");
const Client = require('azure-iot-device').Client;
const Message = require('azure-iot-device').Message;
const Protocol = require('azure-iot-device-mqtt').Mqtt;

//#endregion

console.log(new Date());

//#region Variabili per la comunicazione con Azure

let connectionString = process.env['CLIENT_CS'];							//recupero la connectionString dalle variabili di sistema
let classRoomSchedule;

//#endregion

//#region CONFIGURAZIONE / FUNZIONI IoT-Device

//Handler di disconnessione
//
function disconnectHandler () {
  console.log("Client disconnesso.")
  client.open().catch((err) => {
    console.error(err.message);
  });
}

//Handler di messaggio
//
function messageHandler (msg) {
	console.log(' Body: ' + msg.data);
	client.complete(msg, printResultFor('completed'));

	classRoomSchedule = JSON.parse(msg.data);

	let date = new Date();

	if(classRoomSchedule != undefined)
		classRoomSchedule.forEach(element =>{
			var schDate = new Date(element.date);

			var nowH = date.getHours();
			var nowM = date.getMinutes();

			var elH = schDate.getHours();
			var elM = schDate.getMinutes();

			if(nowH == elH && nowM == elM){
				let mts = new MessageToSend(element.deviceId,element.teacher, element.subject, element.duration);
				var filtered = classRoomSchedule.filter(function(value, index, arr){ 
					return value != element;
				});

				classRoomSchedule = filtered;				

				setTimeout(()=>{
					console.log("Waiting 10 seconds");
					sendData(mts);
				},10000);
			}
		});

}

//Funzione chiamata in caso di errore
//
function errorCallback (err) {

	console.error(err.message);
	
}
  
//Funzione che viene chiamata una volta eseguita la connessione al device
//
function connectCallback () {

	console.log('Client connected, listening for messages...');

	const message = generateMessage();
	console.log('Sending message: ' + message.getData());

	client.sendEvent(message, printResultFor('send'));

}

//Funzione per generare il messaggio da inviare all'IoT Hub
//
function generateMessage () {

	const data = JSON.stringify({ deviceId: 'ndreu-testdevice', message: "Client connected succesfully." });
	const message = new Message(data);
	
	return message;
}

// Helper function to print results in the console
//
function printResultFor(op) {
	return function printResult(err, res) {
	  if (err) console.log(op + ' error: ' + err.toString());
	  if (res) console.log(op + ' status: ' + res.constructor.name);
	};
  }

//#endregion

//#region Creazione IoT-Device

let client = Client.fromConnectionString(connectionString, Protocol);		

//client.on('connect', connectCallback);
client.on('error', errorCallback);
client.on('disconnect', disconnectHandler);
client.on('message', messageHandler);

client.open().catch(err => {

  console.error('Could not connect: ' + err.message);

});


//#endregion

//#region CONFIGURAZIONE / FUNZIONI SERIAL PORT

//Variabili per l'invio dei messaggi seriali
//
let sp = new SerialPort("/dev/ttyUSB2",{ baudRate: 9600 });
let buf; 	

function portOpened(err){
	if(err){
		return console.log('error opening port: ', err.message)
	}else{
		console.log("Listening for data... ");
	}
}

function manageDataReceived(data){
	console.log("Received data:",data.toString('hex'));

	var code = data.toString("hex");

	if (code[1] == 'f'){
		console.log("ALLARM LAUNCHED ON CLASSROOM " + code[0]);
		var json = JSON.stringify({deviceId: code[0], message: "Help required"});
		var mes = new Message(json);

		client.sendEvent(mes);
	}
}

sp.on('open', function(err){portOpened(err)});
sp.on('data', function(data){manageDataReceived(data)});

function sendData(mts){

	let deviceId = mts.deviceId;
	let teacherCode = deviceId + "D";
	let subjectCode = deviceId + "A";
	let durationCode = deviceId + "C";

	buf = Buffer.from(teacherCode,"hex");
	sp.write(buf); 
	sp.write(mts.teacher + "-");

	buf = Buffer.from(subjectCode,"hex");
	sp.write(buf);
	sp.write(mts.subject + "-");

	buf = Buffer.from(durationCode, "hex");
	sp.write(buf);
	sp.write(mts.duration + "-");

}

//#endregion

/*setInterval(function(){
	var date = new Date();

	console.log("Controllo per le ore " + date.getHours() + " " + date.getMinutes());
	if(classRoomSchedule != undefined)
		classRoomSchedule.forEach(element =>{
			var schDate = new Date(element.date);

			var nowH = date.getHours();
			var nowM = date.getMinutes();

			var elH = schDate.getHours();
			var elM = schDate.getMinutes();

			if(nowH == elH && nowM == elM){
				let mts = new MessageToSend(element.deviceId,element.teacher, element.subject, element.duration);
				var filtered = classRoomSchedule.filter(function(value, index, arr){ 
					return value != element;
				});

				classRoomSchedule = filtered;

				console.log(mts);
				sendData(mts);
			}
		});

},30000);*/

//#region Classe per l'invio dei messaggi ai pic

let MessageToSend = class {
	constructor(deviceId, teacher, subject, duration) {
	  this.deviceId = deviceId;
	  this.teacher = teacher;
	  this.subject = subject;
	  this.duration = duration;
	}
};

//#endregion