'use strict';

var ProjectListViewModel = function() {
    var self = this;
    self.projects = ko.observableArray();
    self.load = function() {
        $.getJSON("api/projects", function(data) {
            _.each(data, function(project) {
                projects.push(project);
            });
        });
    }
}