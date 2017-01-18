function MakePieChart(CalorieInfo) {
    CalorieInfo[1] < 0 ? CalorieInfo[1] = 0 : false;
    CalorieInfo[2] < 0 ? CalorieInfo[1] = 2 : false;

    let data = {
        labels: [
            "Calories Remaining",
            "Calories Eaten"
        ],
        datasets: [{
                data: [CalorieInfo[1], CalorieInfo[0]],
                backgroundColor: [
                    "darkcyan",
                    "grey"
                ],
                hoverBackgroundColor: [
                    "#222",
                    "#222"
                ]
            }]
    }

    let ctx = document.getElementById('FirstPieChart').getContext('2d');
    ctx.canvas.width = "200px";
    ctx.canvas.height = "200px";

    var myPieChart = new Chart(ctx, {
        type: 'pie',
        data: data,
        options: {
            title: {
                display: true,
                text: "Calories Consumed Today"
            }
        }
    });
}

function MakeCaloricPieChart(){
    //CalorieInfo[1] < 0 ? CalorieInfo[1] = 0 : false;
    //CalorieInfo[2] < 0 ? CalorieInfo[1] = 2 : false;

    let data = {
        labels: [
            "Protein",
            "Fat",
            "Carbs",
        ],
        datasets: [{
            data: [89, 105, 90],
            backgroundColor: [
                "darkcyan",
                "grey"
            ],
            hoverBackgroundColor: [
                "#222",
                "#222"
            ]
        }]
    }

    let ctx = document.getElementById('PieChart2').getContext('2d');
    ctx.canvas.width = "200px";
    ctx.canvas.height = "200px";

    var myPieChart = new Chart(ctx, {
        type: 'doughnut',
        data: data,
        options: {
            title: {
                display: true,
                text: "Calories Consumed Today"
            }
        }
    });
}
MakeCaloricPieChart()