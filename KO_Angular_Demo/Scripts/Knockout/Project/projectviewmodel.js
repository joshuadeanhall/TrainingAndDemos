'use strict';

var ProjectViewModel = function() {
    var self = this;
    self.projectListViewModel = new ProjectListViewModel();
    self.createProjectViewModel = new CreateProjectViewModel();
    self.currentTemplate = ko.observable('list');
    self.currentModel = ko.observable(self.projectListViewModel);
    self.create = function() {
        self.currentTemplate('create');
        self.currentModel(self.createProjectViewModel);
    }

}