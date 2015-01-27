'use strict';

var CreateProjectViewModel = function() {
    var self = this;
    self.name = ko.observable();
    self.effort = ko.observable();
    self.cost = ko.observable();
    self.create = function() {
        var data = ko.toJSON(self);
        $.ajax({
            url: "/api/projects",
            data: data,
            contentType: 'application/json',
            type: 'POST',
            success: function (resp) {
                alert('success');
            },
            //$.post("/api/projects", data, function(returnedData) {
            //    // This callback is executed if the post was successful     
            //});
        });
    }
}