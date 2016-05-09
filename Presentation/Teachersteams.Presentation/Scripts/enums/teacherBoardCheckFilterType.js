var app = angular.module("ttApp");

app.value('TeacherBoardCheckFilterType', {
    Unchecked: 0,
    Checked: 1,

    properties: {
        0: { name: "Unchecked", value: 0 },
        1: { name: "Checked", value: 1 }
    }
});