﻿function CreateLineChart(CalorieInfo) {
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
            }
        ]
    };

    var myLineChart = new Chart(ctx, {
        type: 'line',
        data: data,
        options: {
            title: {
                display: true,
                text: "Weight Lost This Week"
            }
        }
    });
}