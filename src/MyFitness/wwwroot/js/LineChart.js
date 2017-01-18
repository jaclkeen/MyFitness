function CreateLineChart(CalorieInfo) {

    console.log(CalorieInfo)

    let ctx = document.getElementById('FirstLineChart').getContext('2d');
    ctx.canvas.width = "200px";
    ctx.canvas.height = "200px";

    var data = {
        labels: ["6", "5", "4", "3", "2", "Yesterday", "Today"],
        datasets: [
            {
                label: "Calories Consumed",
                fill: false,
                lineTension: 0.1,
                backgroundColor: "rgba(75,192,192,0.4)",
                borderColor: "rgba(75,192,192,1)",
                borderCapStyle: 'butt',
                borderDash: [],
                borderDashOffset: 0.0,
                borderJoinStyle: 'miter',
                pointBorderColor: "rgba(75,192,192,1)",
                pointBackgroundColor: "#fff",
                pointBorderWidth: 1,
                pointHoverRadius: 5,
                pointHoverBackgroundColor: "rgba(75,192,192,1)",
                pointHoverBorderColor: "rgba(220,220,220,1)",
                pointHoverBorderWidth: 2,
                pointRadius: 1,
                pointHitRadius: 10,
                data: CalorieInfo[0],
                spanGaps: false,
            },
            {
                label: "Daily Calorie Limit",
                backgroundColor: "rgba(255, 99, 132, 0.2)",
                borderColor: "grey",
                borderCapStyle: 'butt',
                pointBorderColor: "darkcyan",
                pointBackgroundColor: "#fff",
                pointHoverBackgroundColor: "white",
                pointHoverBorderColor: "rgba(75,192,192,1)",
                data: CalorieInfo[1]
            },
            {
                label: "Calories Burned",
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