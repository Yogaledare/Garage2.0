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


const initializeChartsFromModel = (model) => {
    const vehicleTypesData = model.vehicleTypeSummaries
        .map(m => ({
                label: m.vehicleType.toString(),
                y: m.count
            })
        );
    const wheelsData = model.vehicleTypeSummaries
        .map(m => ({
                label: m.vehicleType.toString(),
                y: m.totalWheels
            })
        );

    setupCharts(vehicleTypesData, wheelsData);
}





const setupCharts = (vehicleTypesData, wheelsData) => {
    createDoughnutChart("chartContainer", "Parked Vehicle Types", vehicleTypesData);
    createDoughnutChart("wheelsChartContainer", "Wheels Distribution", wheelsData);
}


