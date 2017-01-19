function CreateLineChart(CalorieInfo) {
    let ctx = document.getElementById('FirstLineChart').getContext('2d');
    ctx.canvas.width = "200px";
    ctx.canvas.height = "200px";

    var data = {
        labels: ["6 Days Ago", "5 Days Ago", "4 Days Ago", "3 Days Ago", "2 Days Ago", "Yesterday", "Today"],
        datasets: [
            {
                label: "Calories Consumed",
                backgroundColor: "transparent",
                borderColor: "#FF9999",
                borderCapStyle: 'butt',
                borderJoinStyle: 'miter',
                pointBorderColor: "white",
                pointBackgroundColor: "salmon",
                pointHoverBackgroundColor: "salmon",
                pointHoverBorderColor: "white",
                data: CalorieInfo[0],
            },
            {
                label: "Daily Calorie Limit",
                backgroundColor: "rgba(75, 192, 192, 0.2)",
                borderColor: "rgba(75,192,192,1)",
                borderCapStyle: 'butt',
                pointBorderColor: "white",
                pointBackgroundColor: "darkcyan",
                pointHoverBackgroundColor: "darkcyan",
                pointHoverBorderColor: "rgba(220,220,220,1)",
                data: CalorieInfo[1]
            },
            {
                label: "Calories Burned",
                backgroundColor: "transparent",
                borderColor: "dodgerblue",
                borderCapStyle: 'butt',
                pointBorderColor: "white",
                pointBackgroundColor: "dodgerblue",
                pointHoverBackgroundColor: "dodgerblue",
                pointHoverBorderColor: "white",
                data: CalorieInfo[2]
            }
        ]
    };


    var myLineChart = new Chart(ctx, {
        type: 'line',
        data: data,
        options: {
            title: {
                display: true,
                text: "Calories Consumed The Past 7 Days"
            }
        }
    });
}

function WeeklyWeightLostLineChart(WeightLost) {
    console.log(WeightLost)

    let ctx = document.getElementById('WWLineChart').getContext('2d');
    ctx.canvas.width = "200px";
    ctx.canvas.height = "200px";

    var data = {
        labels: ["6 Days Ago", "5 Days Ago", "4 Days Ago", "3 Days Ago", "2 Days Ago", "Yesterday", "Today"],
        datasets: [
            {
                label: "Weight Lost",
                backgroundColor: "rgba(75, 192, 192, 0.2)",
                borderColor: "darkcyan",
                borderCapStyle: 'butt',
                borderJoinStyle: 'miter',
                pointBorderColor: "white",
                pointBackgroundColor: "darkcyan",
                pointHoverBackgroundColor: "darkcyan",
                pointHoverBorderColor: "white",
                data: WeightLost[0]
            },
            {
                label: "Carbs Eaten",
                backgroundColor: "rgba(54, 162, 235, 0.2)",
                borderColor: "dodgerblue",
                borderCapStyle: 'butt',
                borderJoinStyle: 'miter',
                pointBorderColor: "white",
                pointBackgroundColor: "dodgerblue",
                pointHoverBackgroundColor: "dodgerblue",
                pointHoverBorderColor: "white",
                data: WeightLost[1]
            },
            {
                label: "Protein Eaten",
                backgroundColor: "rgba(255, 159, 64, 0.2)",
                borderColor: "orange",
                borderCapStyle: 'butt',
                borderJoinStyle: 'miter',
                pointBorderColor: "white",
                pointBackgroundColor: "orange",
                pointHoverBackgroundColor: "orange",
                pointHoverBorderColor: "white",
                data: WeightLost[2]
            },
            {
                label: "Fat Eaten",
                backgroundColor: "rgba(153, 102, 255, 0.2)",
                borderColor: "salmon",
                borderCapStyle: 'butt',
                borderJoinStyle: 'miter',
                pointBorderColor: "white",
                pointBackgroundColor: "salmon",
                pointHoverBackgroundColor: "salmon",
                pointHoverBorderColor: "white",
                data: WeightLost[3]
            },
        ]
    };

    var myLineChart = new Chart(ctx, {
        type: 'line',
        data: data,
        options: {
            title: {
                display: true,
                text: "Weight Lost This Week vs. Macronutrients Consumed In Grams"
            }
        }
    });
}

function MonthlyWeightLostLineChart(WeightLost) {
    console.log(WeightLost)

    let ctx = document.getElementById('MonthlyWWLineChart').getContext('2d');
    ctx.canvas.width = "300px";
    ctx.canvas.height = "300px";

    var data = {
        labels: ["29 Days Ago", "28 Days Ago", "27 Days Ago", "26 Days Ago", "25 Days Ago", "24 Days Ago",
            "23 Days Ago", "22 Days Ago", "21 Days Ago", "20 Days Ago", "19 Days Ago", "18 Days Ago", "17 Days Ago",
            "16 Days Ago", "15 Days Ago", "14 Days Ago", "13 Days Ago", "12 Days Ago", "11 Days Ago", "10 Days Ago",
            "9 Days Ago", "8 Days Ago", "7 Days Ago", "6 Days Ago", "5 Days Ago", "4 Days Ago", "3 Days Ago",
            "2 Days Ago", "Yesterday", "Today",
        ],
        datasets: [
            {
                label: "Weight Lost",
                backgroundColor: "rgba(75, 192, 192, 0.2)",
                borderColor: "darkcyan",
                borderCapStyle: 'butt',
                borderJoinStyle: 'miter',
                pointBorderColor: "white",
                pointBackgroundColor: "darkcyan",
                pointHoverBackgroundColor: "darkcyan",
                pointHoverBorderColor: "white",
                data: WeightLost[0]
            },
            {
                label: "Carbs Eaten",
                backgroundColor: "rgba(54, 162, 235, 0.2)",
                borderColor: "dodgerblue",
                borderCapStyle: 'butt',
                borderJoinStyle: 'miter',
                pointBorderColor: "white",
                pointBackgroundColor: "dodgerblue",
                pointHoverBackgroundColor: "dodgerblue",
                pointHoverBorderColor: "white",
                data: WeightLost[1]
            },
            {
                label: "Protein Eaten",
                backgroundColor: "rgba(255, 159, 64, 0.2)",
                borderColor: "orange",
                borderCapStyle: 'butt',
                borderJoinStyle: 'miter',
                pointBorderColor: "white",
                pointBackgroundColor: "orange",
                pointHoverBackgroundColor: "orange",
                pointHoverBorderColor: "white",
                data: WeightLost[2]
            },
            {
                label: "Fat Eaten",
                backgroundColor: "rgba(153, 102, 255, 0.2)",
                borderColor: "salmon",
                borderCapStyle: 'butt',
                borderJoinStyle: 'miter',
                pointBorderColor: "white",
                pointBackgroundColor: "salmon",
                pointHoverBackgroundColor: "salmon",
                pointHoverBorderColor: "white",
                data: WeightLost[3]
            },
        ]
    };

    var myLineChart = new Chart(ctx, {
        type: 'line',
        data: data,
        options: {
            title: {
                display: true,
                text: "Weight Lost This Week vs. Macronutrients Consumed In Grams",
            }
        }
    });
}