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

function MakeCaloricPieChart(CalorieInfo) {
    let data = {
        labels: [
            "Fat",
            "Carbs",
            "Protein",
        ],
        datasets: [{
            data: CalorieInfo,
            backgroundColor: [
                "salmon",
                "dodgerblue",
                "yellow"
            ],
            hoverBackgroundColor: [
                "#222",
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
                text: "Nutrients Consumed Today (In Grams)"
            }
        }
    });
}

function YearlyDoughnut(CalorieInfo) {
    console.log(CalorieInfo)

    let data = {
        labels: [
            "Starting Calories",
            "Calories Consumed",
        ],
        datasets: [{
            data: CalorieInfo,
            backgroundColor: [
                "salmon",
                "dodgerblue",
                "yellow"
            ],
            hoverBackgroundColor: [
                "#222",
                "#222",
                "#222"
            ]
        }]
    }

    let ctx = document.getElementById('YearlyDonutChart').getContext('2d');
    var myPieChart = new Chart(ctx, {
        type: 'doughnut',
        data: data,
        options: {
            title: {
                display: true,
                text: "Average Calories Consumed vs. Average Calorie Limit This Year"
            }
        }
    });
}

function YearlyPie(PieInfo) {
    console.log(PieInfo)
    let data = {
        labels: [
            "Carbs",
            "Fat",
            "Protein",
        ],
        datasets: [{
            data: PieInfo,
            backgroundColor: [
                "salmon",
                "dodgerblue",
                "yellow"
            ],
            hoverBackgroundColor: [
                "#222",
                "#222",
                "#222"
            ]
        }]
    }

    let ctx = document.getElementById('YearlyPieChart').getContext('2d');
    var myPieChart = new Chart(ctx, {
        type: 'pie',
        data: data,
        options: {
            title: {
                display: true,
                text: "Average Macros Consumed This Year (In Grams)"
            }
        }
    });
}
