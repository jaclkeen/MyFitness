//var data = {
//    labels: [
//        "Red",
//        "Blue",
//        "Yellow"
//    ],
//    datasets: [
//        {
//            data: [300, 50, 100],
//            backgroundColor: [
//                "#FF6384",
//                "#36A2EB",
//                "#FFCE56"
//            ],
//            hoverBackgroundColor: [
//                "#FF6384",
//                "#36A2EB",
//                "#FFCE56"
//            ]
//        }]
//};

function MakePieChart(CalorieInfo) {
    let data = {
        labels: [
            "Calories Remaining",
            "Calories Eaten"
        ],
        datasets: [
            {
                data: [CalorieInfo[1], CalorieInfo[0]],
                backgroundColor: [
                    "darkcyan",
                    "grey"
                ],
                hoverBackgroundColor: [
                    "#222",
                    "#222"
                ]
            }],
        options: {
            title: {
                display: true,
                text: 'Custom Chart Title'
            }
        }
    }

    let ctx = document.getElementById('FirstPieChart').getContext('2d');
    ctx.canvas.width = "200px";
    ctx.canvas.height = "200px";

    var myPieChart = new Chart(ctx, {
        type: 'pie',
        data: data
        //options: options
    });


    $(".PieChart1").html(myPieChart)
}