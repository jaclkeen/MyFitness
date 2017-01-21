function CreateBarChart(ChartData) {
    console.log(ChartData)
    let ctx = document.getElementById('FirstBarChart').getContext('2d');
    ctx.canvas.width = "200px";
    ctx.canvas.height = "200px";

    var data = {
        labels: ["Fat", "Protein", "Carbs"],
        datasets: [
            {
                label: "Breakdown",
                backgroundColor: [
                    'rgba(54, 162, 235, 0.7)',
                    'rgba(255, 206, 86, 0.7)',
                    'rgba(75, 192, 192, 0.7)',
                ],
                borderColor: [
                    "black", "black", "black"
                ],
                borderWidth: 2,
                data: [ChartData[3], ChartData[1], ChartData[2]],
            }
        ]
    };

    new Chart(ctx, {
        type: "horizontalBar",
        data: data,
        options: {
            title: {
                display: true,
                text: "Macronutrient Breakdown of Calories Eaten Today"
            }
        }
    });
}

function CreateBarChartForCaloriesBurnedInTimeRange(CaloricData) {
    let ctx = document.getElementById('BGGramTotalsGraph').getContext('2d');

    var data = {
        labels: ["6 Days Ago", "5 Days Ago", "4 Days Ago", "3 Days Ago", "2 Days Ago", "Yesterday", "Today"],
        datasets: [
            {
                label: "Breakdown",
                backgroundColor: [
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)',
                    'rgba(353, 102, 255, 0.6)',
                    'rgba(54, 162, 235, 0.6)'

                ],
                borderColor: [
                    "black", "black", "black", "black", "black", "black", "black"
                ],
                borderWidth: 1,
                data: CaloricData,
            }
        ]
    };

    new Chart(ctx, {
        type: "bar",
        data: data,
        options: {
            title: {
                display: true,
                text: "Calories Burned the Last 7 Days"
            }
        }
    });
}


function CreateMonthlyDoubleBarChart(CaloricData) {
    let ctx = document.getElementById('DoubleBarChart').getContext('2d');

    var data = {
        labels: ["29 Days Ago", "28 Days Ago", "27 Days Ago", "26 Days Ago", "25 Days Ago", "24 Days Ago",
            "23 Days Ago", "22 Days Ago", "21 Days Ago", "20 Days Ago", "19 Days Ago", "18 Days Ago", "17 Days Ago",
            "16 Days Ago", "15 Days Ago", "14 Days Ago", "13 Days Ago", "12 Days Ago", "11 Days Ago", "10 Days Ago",
            "9 Days Ago", "8 Days Ago", "7 Days Ago", "6 Days Ago", "5 Days Ago", "4 Days Ago", "3 Days Ago",
            "2 Days Ago", "Yesterday", "Today",
        ],
        datasets: [
            {
                type: "line",
                label: "Daily Calorie Goal",
                data: CaloricData[0],
                borderWidth: 2,
                borderColor: "black",
                backgroundColor: "rgba(75, 192, 192, 0.6)"
            },
            {
                type: "bar",
                label: "Calories Consumed",
                backgroundColor: 'salmon',
                pointBackgroundColor: "salmon",
                borderColor: "black",
                pointBorderColor: "black",
                borderWidth: 2,
                data: CaloricData[1],
            },
        ]
    };

    new Chart(ctx, {
        type: "bar",
        data: data,
        options: {
            title: {
                display: true,
                text: "Calorie Goal and Calories Consumed the Last 30 Days"
            }
        }
    });
}

function CreateCaloriePercantageHorizontalBarChart(CalPercentData){
    let ctx = document.getElementById('HorizontalBarChart').getContext('2d');
    ctx.canvas.width = "200px";
    ctx.canvas.height = "200px";

    var data = {
        labels: ["Carbs", "Protein", "Fat"],
        datasets: [
            {
                label: "Breakdown",
                backgroundColor: [
                    'rgba(54, 162, 235, 0.7)',
                    'rgba(255, 206, 86, 0.7)',
                    'rgba(255, 159, 64, 0.7)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                ],
                borderColor: [
                    "black", "black", "black"
                ],
                borderWidth: 2,
                data: CalPercentData,
            }
        ]
    };

    new Chart(ctx, {
        type: "horizontalBar",
        data: data,
        options: {
            title: {
                display: true,
                text: "Macronutrient Percentage of Calories Eaten This Month"
            }
        }
    });
}

function CreateYearlyWeightLostChart(WeightLostData) {
    let ctx = document.getElementById('YearlyHorizontalBarChart').getContext('2d');
    ctx.canvas.width = "200px";
    ctx.canvas.height = "200px";

    var data = {
        labels: ["January", "Febuary", "March", "April", "May", "June", "July", "August", "September",
            "October", "November", "December"],
        datasets: [
            {
                label: "Breakdown",
                backgroundColor: [
                    'rgba(54, 162, 235, 0.7)',
                    'rgba(255, 206, 86, 0.7)',
                    'rgba(255, 159, 64, 0.7)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                ],
                borderColor: [
                    "black", "black", "black", "black", "black", "black", "black", "black", "black", "black", "black", "black"
                ],
                borderWidth: 2,
                data: WeightLostData,
            }
        ]
    };

    new Chart(ctx, {
        type: "bar",
        data: data,
        options: {
            title: {
                display: true,
                text: "Total Weight Lost Each Month"
            }
        }
    });
}