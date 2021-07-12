let classes;
let teachers;
let floors;
let lessons=[];
let eventsArr = [];


document.addEventListener('DOMContentLoaded', function () {
    $.get("api/Calendar/LoadLessons", function (data) {

        lessons = data;
        lessons.forEach(lesson => {
            let date = new Date(lesson.date);
            let endStr = new Date();
            endStr.setSeconds(date.getSeconds() + lesson.duration);
            console.log(endStr);
            let eventObj = {
                title: lesson.subject,
                start: date.toISOString(),
                end: endStr.toISOString(),
                id: lesson.id,
                teacher: lesson.teacher,
                classroom: lesson.classroom,
                floor: lesson.floor,
            };
            eventsArr.push(eventObj);
        });
        console.log(lessons);
        console.log(eventsArr);
        calendarRender();
    });

    document.getElementById("editButton").addEventListener("click", function () {
        let id = document.getElementById("id").value;
        let subject = document.getElementById("subject").value;
        let teacher = document.getElementById("teacher").value;
        let date = new Date(document.getElementById("date").value);
        let classroom = document.getElementById("classroom").value;
        let floor = document.getElementById("floor").value;
        let SplittedHours = document.getElementById("startTime").value.split(":");
        date.setHours(SplittedHours[0]);
        date.setMinutes(SplittedHours[1]);
        let duration = (document.getElementById("duration").value * 3600 )

        $.ajax({
            type: "POST",
            url: "api/Calendar/EditLesson/" + id,
            data: JSON.stringify({ "subject": subject, "teacher": teacher, "classroom": classroom, "floor": floor, "date": date, "duration": duration }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: window.location.reload()
        })
            
    });
    document.getElementById("deleteButton").addEventListener("click", function () {
        let id = document.getElementById("id").value;
     
        $.ajax({
            type: "POST",
            url: "api/Calendar/DeleteLesson/" + id,
            data: {},
            contentType: "application/json; charset=utf-8",
            success: window.location.reload()
        })

    });
});


function calendarRender() {
    var calendarEl = document.getElementById('calendar');
    var today = new Date();

    var calendar = new FullCalendar.Calendar(calendarEl, {
        //initialView: 'timeGridWeek',
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'
        },
        themeSystem: 'bootstrap',
        eventClick: function (info) {
            var eventObj = info.event;
            console.log(eventObj);
            OpenModal(eventObj.id);
        },
        initialDate: today,
        events: eventsArr
        //weekends: false
    });
    calendar.render();
}
function addEvent(event) {
    calendar.addEvent(event)
}
function mapArray(id){
   return lessons.find(l => l.id == id);
    
    
}
function OpenModal(id){
    let clickedLesson = mapArray(id);
    let data = new Date(clickedLesson.date);
    console.log(data.toLocaleDateString());
    document.getElementById("subject").value = clickedLesson.subject;
    document.getElementById("id").value = clickedLesson.id;
    document.getElementById("teacher").value = clickedLesson.teacher;
    document.getElementById("date").value = data.toISOString().substring(0, 10);
    document.getElementById("classroom").value = clickedLesson.classroom;
    document.getElementById("floor").value = clickedLesson.floor;
    document.getElementById("startTime").value = data.toISOString().substring(11, 16);
    document.getElementById("duration").value = (clickedLesson.duration / 3600).toFixed(2);
    $(document).ready(function () {
        $("#details-modal").modal('show');
    });
}





