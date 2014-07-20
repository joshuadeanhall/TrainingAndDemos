'use strict';

var ProjectListViewModel = function () {
    var self = this;
    self.loadedProjects = new Array();
    self.query = ko.observable('');
    self.projects = ko.observableArray();
    self.query.subscribe(function () {
        self.updateProjects();
    });
    self.updateProjects = function () {
        self.projects.removeAll();
        var search = self.query();
        var projects = ko.utils.arrayFilter(self.loadedProjects, function (loadedProject) {
            return loadedProject.name().toLowerCase().indexOf(search) >= 0;
        });
        ko.utils.arrayPushAll(self.projects, projects);
    }
    self.load = function () {
        $.getJSON("/api/projects", function (data) {
            _.each(data, function (projectData) {
                var project = new Project();
                project.name(projectData.Name);
                project.manager(projectData.UserName);
                project.id(projectData.Id);
                project.cost(projectData.Cost);
                project.effort(projectData.Effort);
                self.loadedProjects.push(project);
            });
            self.updateProjects();
        });
    }
}

var Project = function () {
    var self = this;
    self.name = ko.observable();
    self.manager = ko.observable();
    self.id = ko.observable();
    self.cost = ko.observable();
    self.effort = ko.observable();
}