var app = angular.module("ttApp");

app.value('StudentBoardFilterType', {
    New: 1,
    Expired: 2,
    Completed: 3,
    Checked: 4,

    properties: {
        1: { name: "New", value: 1 },
        2: { name: "Expired", value: 2 },
        3: { name: "Completed", value: 3 },
        4: { name: "Checked", value: 4 }
    }
});