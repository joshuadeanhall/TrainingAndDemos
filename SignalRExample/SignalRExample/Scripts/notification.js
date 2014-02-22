$(function() {
    toastr.options = {
        "debug": false,
        "positionClass": "toast-top-right",
        "onclick": null,
        "fadeIn": 300,
        "fadeOut": 1000,
        "timeOut": 5000,
        "extendedTimeOut": 1000
    };
    $.connection.hub.url = signalRUrl;
    $.connection.hub.qs = 'username=' + userName;
    var message = $.connection.messageHub;
    message.client.send = function(title, messageBody) {
        toastr.success(messageBody, title);
    };

    $.connection.hub.start().done(function() {
        
    });

});

$('#submitMessage').click(function () {
    $.ajax({
        type: "POST",
        url: "/api/notification",
        // The key needs to match your method's input parameter (case-sensitive).
        data: JSON.stringify($('#message').val()),
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
    $('#message').val('');
});