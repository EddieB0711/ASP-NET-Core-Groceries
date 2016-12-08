(function() {
    var hub = $.connection.groceriesHub;

    $.connection.hub.logging = true;
    $.connection.hub.start().done(function() {
        hub.server.polling();
    });

    var Model = function() {
        var self = this;
        self.items = ko.observableArray();
    };

    Model.prototype = {

        addItem: function(item) {
            var self = this;
            self.items.removeAll();
            $.each(item,
                function(index, value) {
                    self.items.push(value);
                });
        }
    };

    var model = new Model();

    hub.client.newItem = function(item) {
        model.addItem(item);
    };

    $(function() {
        ko.applyBindings(model);
    });

}());