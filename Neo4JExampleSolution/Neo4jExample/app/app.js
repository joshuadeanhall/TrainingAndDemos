App = Ember.Application.create();

App.ApplicationAdapter = DS.RESTAdapter.extend({
    namespace: 'api'
});

App.Router.map(function () {
    this.resource('managers', { path: '/managers' }, function() {
        this.route('jsonrepresentation');
    });
});

App.ManagersRoute = Ember.Route.extend({

});

App.ManagersIndexRoute = App.ManagersRoute.extend({

});
