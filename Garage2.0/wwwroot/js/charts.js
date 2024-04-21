

const createDoughnutChart = (containerId, titleText, dataPoints) => {
    const chart = new CanvasJS.Chart(containerId, {
        animationEnabled: true,
        title: {
            text: titleText,
            fontFamily: "Arial, Helvetica, sans-serif",
            fontsize: 16,
        },
        creditText: "",
        data: [{
            type: "doughnut",
            dataPoints: dataPoints
        }],
        toolTip: {
            fontFamily: "Arial, Helvetica, sans-serif"
        },
        legend: {
            fontFamily: "Arial, Helvetica, sans-serif"
        },
    });
    chart.render();
}


const setupCharts = (vehicleTypesData, wheelsData) => {
    createDoughnutChart("chartContainer", "Parked Vehicle Types", vehicleTypesData);
    createDoughnutChart("wheelsChartContainer", "Wheels Distribution", wheelsData);
}




