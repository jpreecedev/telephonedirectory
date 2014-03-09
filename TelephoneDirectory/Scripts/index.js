/// <reference path="jquery-2.1.0.intellisense.js" />
/// <reference path="knockout-3.0.0.debug.js" />

function TelephoneEntry(data) {
    var self = this;
    self.id = data.id;
    self.firstName = data.firstName;
    self.lastName = data.lastName;
    self.number = data.number;
}

function TelephoneViewModel() {
    var self = this;
    self.id = ko.observable(0);
    self.firstName = ko.observable('');
    self.lastName = ko.observable('');
    self.number = ko.observable('');
    self.addText = ko.observable('Add');
    self.resetText = ko.observable('Reset');
    self.selectedIndex = -1;

    var hub = $.connection.entryHub;

    self.add = function () {

        var entry = new TelephoneEntry({
            id: self.id(),
            firstName: self.firstName(),
            lastName: self.lastName(),
            number: self.number()
        });

        if (self.addText() == 'Edit') {
            var oldTelephoneEntry = self.telephoneEntries()[self.selectedIndex];
            entry.id = self.id();

            self.telephoneEntries.replace(oldTelephoneEntry, entry);
        }

        self.post(entry);
        self.reset();
    };

    self.edit = function (telephoneEntry) {
        self.id(telephoneEntry.id),
        self.firstName(telephoneEntry.firstName);
        self.lastName(telephoneEntry.lastName);
        self.number(telephoneEntry.number);
        self.addText('Update');
        self.resetText('Cancel');
        self.selectedIndex = self.telephoneEntries.indexOf(telephoneEntry);
    };

    self.delete = function (telephoneEntry) {
        self.telephoneEntries.destroy(telephoneEntry);

        $.ajax({
            url: '/api/Data/' + telephoneEntry.id,
            type: 'DELETE',
            contentType: "application/json;charset=UTF-8",
            data: JSON.stringify({ id: telephoneEntry.id }),
            dataType: "json"
        });

        hub.server.delete(telephoneEntry);
    };

    self.reset = function () {
        self.id(0);
        self.firstName('');
        self.lastName('');
        self.number('');
        self.addText('Add');
        self.resetText('Reset');
        self.selectedIndex = -1;
    };

    self.load = function () {
        $.getJSON('/api/Data/', function (data) {
            $.each(data, function (index, item) {
                self.telephoneEntries.push(new TelephoneEntry({
                    id: item.id,
                    firstName: item.firstName,
                    lastName: item.lastName,
                    number: item.number
                }));
            });
        });
    };

    self.post = function (telephoneEntry) {
        $.post('/api/Data/', telephoneEntry, function (id) {
            telephoneEntry.id = id;

            hub.server.addOrUpdate(telephoneEntry);
        });
    };

    hub.client.addOrUpdate = function (telephoneEntry) {

        var result = $.grep(self.telephoneEntries(), function (entry) {
            return entry.id == telephoneEntry.id;
        });

        if (result.length == 0) {
            self.telephoneEntries.push(telephoneEntry);
        } else {
            self.telephoneEntries.replace(result[0], telephoneEntry);
        }
    };

    hub.client.delete = function (id) {
        var result = $.grep(self.telephoneEntries(), function (entry) {
            return entry.id == id;
        });

        if (result.length > 0)
            self.telephoneEntries.destroy(result[0]);
    };

    $.connection.hub.start();
    self.telephoneEntries = ko.observableArray([]);
    self.load();
}

var telephoneEntryViewModel = new TelephoneViewModel();
ko.applyBindings(telephoneEntryViewModel);