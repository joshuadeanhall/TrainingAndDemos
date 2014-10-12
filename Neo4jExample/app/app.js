App = Ember.Application.create();

App.Router.map(function () {
    this.resource('managers', { path: '/managers' }, function() {
        this.route('jsonrepresentation');
    });
});

App.ManagerRoute = Ember.Route.extend({

});

App.ManagerIndexRoute = App.ManagerRoute.extend({
    templateName: 'managers',
    controllerName: 'managers'
});

App.ManagerJsonrepresentationRoute = App.ManagerRoute.extend({
    templateName: 'jsonrepresentation'
});