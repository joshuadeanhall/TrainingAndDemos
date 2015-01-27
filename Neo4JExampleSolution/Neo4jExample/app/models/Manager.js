App.Manager = DS.Model.extend({
    name: DS.attr('string'),
    Name: DS.attr('string')
});


App.ManagerSerializer = DS.RESTSerializer.extend({
    primaryKey: 'email',

    //// ember-data-1.0.0-beta2 does not handle embedded data like they once did in 0.13, so we've to update individually if present
    //// once embedded is implemented in future release, we'll move this back to WebAPISerializer.
    //// see https://github.com/emberjs/data/blob/master/TRANSITION.md for details
    extractArray: function (store, primaryType, payload) {
        var primaryTypeName = primaryType.typeKey;

        var typeName = primaryTypeName,
            type = store.modelFor(typeName);

        var data = {};
        data[typeName] = payload;
        payload = data;
        return this._super.apply(this, arguments);
    },

    normalizeHash: {
        userList: function (hash) {
            hash.email = hash.id;
            return hash;
        }
    }

});