document.addEventListener("DOMContentLoaded", function () {
    var canvas = document.getElementById('donutChart');
    if (!canvas) return;

    var ctx = canvas.getContext('2d');
    var completionPercentage = parseInt(canvas.getAttribute('data-percentage'), 10) || 0;

    var data = {
        datasets: [{
            data: [completionPercentage, 100 - completionPercentage],
            backgroundColor: ['#0d6efd', '#e9ecef'],
            borderWidth: 0
        }]
    };

    var options = {
        cutout: '70%',
        responsive: false,
        plugins: {
            tooltip: { enabled: false },
            legend: { display: false }
        }
    };

    var donutChart = new Chart(ctx, {
        type: 'doughnut',
        data: data,
        options: options,
        plugins: [{
            id: 'textCenter',
            beforeDraw: function (chart) {
                var width = chart.width,
                    height = chart.height,
                    ctx = chart.ctx;
                ctx.restore();
                var fontSize = (height / 114).toFixed(2);
                ctx.font = fontSize + "em sans-serif";
                ctx.textBaseline = "middle";

                var text = completionPercentage + "%",
                    textX = Math.round((width - ctx.measureText(text).width) / 2),
                    textY = height / 2;

                ctx.fillStyle = '#0d6efd';
                ctx.fillText(text, textX, textY);
                ctx.save();
            }
        }]
    });
});
