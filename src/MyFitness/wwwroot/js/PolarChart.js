function CreatePolarChart(NutrientData) {
    var data = {
        datasets: [{
            data: [
                11,
                16,
                7,
                14
            ],
            backgroundColor: [
                "#FF6384",
                "#4BC0C0",
                "#FFCE56",
                "#E7E9ED",
            ],
            label: 'My dataset' // for legend
        }],
        labels: [
            "Red",
            "Green",
            "Yellow",
            "Blue"
        ]
    };

    if ($(".FirstPolarChart").is(":visible")) {
        var ctx = $(".FirstPolarChart").get(0).getContext("2d");
        ctx.canvas.width = "200";
        ctx.canvas.height = "200";
        new Chart(ctx, {
            data: data,
            type: 'polarArea'
        });
    }
}