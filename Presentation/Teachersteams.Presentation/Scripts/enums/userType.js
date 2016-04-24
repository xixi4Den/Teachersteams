var app = angular.module("ttApp");

app.value('UserType', {
    Teacher: 1,
    Student: 2,

    properties: {
        1: { name: "Teacher", value: 1 },
        2: { name: "Student", value: 2 }
    },
});