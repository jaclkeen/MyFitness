function CreatePolarChart(NutrientData) {
    var data = {
        datasets: [{
            data: [
                NutrientData[0],
                NutrientData[1],
                NutrientData[2],
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
            "Carbs",
            "Protein",
            "Fat",
        ]
    };

    if ($(".FirstPolarChart").is(":visible")) {
        var ctx = $(".FirstPolarChart").get(0).getContext("2d");
        ctx.canvas.width = "200";
        ctx.canvas.height = "200";
        new Chart(ctx, {
            data: data,
            type: 'polarArea',
            options: {
                title: {
                    display: true,
                    text: "Total Percentage of Macronutrient Consumed This Week"
                }
            }
        });
    }
}