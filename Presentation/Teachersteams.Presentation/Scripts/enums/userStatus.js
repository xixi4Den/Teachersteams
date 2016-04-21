var app = angular.module("ttApp");

app.value('UserStatus', {
    Requested: 1,
    Accepted: 2,
    Declined: 3,

    properties: {
        1: { name: "Requested", value: 1 },
        2: { name: "Accepted", value: 2 },
        3: { name: "Declined", value: 3 }
    },
});