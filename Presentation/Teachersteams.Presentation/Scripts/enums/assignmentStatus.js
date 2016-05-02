var app = angular.module("ttApp");

app.value('AssignmentStatus', {
    Draft: 1,
    Active: 2,
    Expired: 3,
    Deleted: 4,

    properties: {
        1: { name: "Draft", value: 1 },
        2: { name: "Active", value: 2 },
        3: { name: "Expired", value: 3 },
        4: { name: "Deleted", value: 4 }
    },

    localizedName: {
        1: function () { return window.resources.assignmentStatusEnumDraft },
        2: function () { return window.resources.assignmentStatusEnumActive },
        3: function () { return window.resources.assignmentStatusEnumExpired }
    }
});