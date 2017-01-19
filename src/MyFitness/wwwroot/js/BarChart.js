function CreateBarChart(ChartData) {
    let ctx = document.getElementById('FirstBarChart').getContext('2d');
    ctx.canvas.width = "200px";
    ctx.canvas.height = "200px";

    var data = {
        labels: ["Protein", "Fat", "Carbs"],
        datasets: [
            {
                label: "Breakdown",
                backgroundColor: [
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    "black", "black", "black"
                    //'rgba(54, 162, 235, 1)'
                ],
                borderWidth: 2,
                data: [ChartData[1], ChartData[3], ChartData[2]],
            }
        ]
    };

    new Chart(ctx, {
        type: "bar",
        data: data,
        options: {
            title: {
                display: true,
                text: "Macronutrient Breakdown (In Calories)"
            }
        }
    });
}