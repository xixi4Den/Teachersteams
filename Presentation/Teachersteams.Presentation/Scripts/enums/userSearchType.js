var app = angular.module("ttApp");

app.value('UserSearchType', {
    Friends: 1,
    AllUsers: 2,

    properties: {
        1: { name: "Friends", value: 1 },
        2: { name: "AllUsers", value: 2 }
    }
});