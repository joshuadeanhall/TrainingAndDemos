App.ManagersJsonrepresentationController = Ember.ObjectController.extend({
    //name: 'asdf',
    jsonString: '',
    actions: {
        load: function () {
            var self = this;
            $.get('/api/manager', function (data) {
                self.set('jsonString', data);
            });
        }
    }
});