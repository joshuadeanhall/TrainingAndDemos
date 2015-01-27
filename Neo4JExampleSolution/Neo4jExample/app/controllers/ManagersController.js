App.ManagersIndexController = Ember.ArrayController.extend({
    // the initial value of the `search` property
    search: 'Josh',

    actions: {
        query: function () {
            var self = this;
            // the current value of the text field
            var searchString = self.get('search');
            $.get('/api/manager?name=' + self.get('search'), function (data) {
               self.set('model', data);
            });
           // var model = this.store.find('manager');
            //this.set('model', model);
        }
    }
});