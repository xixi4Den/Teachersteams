var app = angular.module("ttApp");

app.value('TeacherBoardAssignFilterType', {
    NotAssigned: 0,
    AssignedToMe: 2,
    AssignedToOthers: 4,

    properties: {
        0: { name: "NotAssigned", value: 0 },
        2: { name: "AssignedToMe", value: 2 },
        4: { name: "AssignedToOthers", value: 4 }
    }
});