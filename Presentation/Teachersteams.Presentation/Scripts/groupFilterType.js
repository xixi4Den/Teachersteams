var app = angular.module("ttApp");

app.value('GroupFilterType', {
    AllForTeacher: 0,
    Own: 1,
    ForTeacherAssistant: 2,
    ForStudent: 3,

    properties: {
        0: { name: "AllForTeacher", value: 0 },
        1: { name: "Own", value: 1 },
        2: { name: "ForTeacherAssistant", value: 2 },
        3: { name: "ForStudent", value: 3 }
    }
});