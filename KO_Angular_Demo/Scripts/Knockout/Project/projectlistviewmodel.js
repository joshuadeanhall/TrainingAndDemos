'use strict';

var ProjectListViewModel = function() {
    var self = this;
    self.projects = ko.observableArray();
    self.load = function () {
        $.getJSON("/api/projects", function(data) {
            _.each(data, function (projectData) {
                var project = new Project();
                project.name(projectData.Name);
                project.manager(projectData.UserName);
                project.id(projectData.Id);
                project.cost(projectData.Cost);
                project.effort(projectData.Effort);
                self.projects.push(project);
            });
        });
    }
}

var Project = function() {
    var self = this;
    self.name = ko.observable();
    self.manager = ko.observable();
    self.id = ko.observable();
    self.cost = ko.observable();
    self.effort = ko.observable();
}