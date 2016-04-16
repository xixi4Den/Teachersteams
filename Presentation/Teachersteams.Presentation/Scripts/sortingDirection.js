var app = angular.module("ttApp");

app.value('SortingDirection', {
    None: 0,
    Ascending: 1,
    Descending: 2,

    properties: {
        0: { name: "None", value: 0, code: "none" },
        1: { name: "Ascending", value: 1, code: "asc" },
        2: { name: "Descending", value: 2, code: "desc" }
    },

    getByCode: function(code) {
        return _.findWhere(this.properties, { code: code }).value;
    }
});