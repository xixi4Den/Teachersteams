var app = angular.module("ttApp");

app.value('GroupFilterType', {
    AllForTeacher: 0,
    Own: 1,
    Assistant: 2,
    AllForStudent: 3,

    properties: {
        0: { name: "AllForTeacher", value: 0 },
        1: { name: "Own", value: 1 },
        2: { name: "Assistant", value: 2 },
        3: { name: "AllForStudent", value: 3 }
    }
});