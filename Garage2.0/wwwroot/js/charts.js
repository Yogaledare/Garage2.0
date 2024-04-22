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


const createTimePlotChart = (containerId, dataPoints) => {
    console.log(dataPoints)
    const formattedDataPoints = dataPoints.map(dp => ({
        x: new Date(dp.x),
        y: dp.y
    }));
    const chart = new CanvasJS.Chart(containerId, {
        animationEnabled: true,
        title: {
            text: "Garage Usage Over Time",
            fontFamily: "Arial, Helvetica, sans-serif",
            fontsize: 16,
        },
        axisX: {
            title: "Time",
            valueFormatString: "DD MMM, YYYY",
            gridThickness: 0
        },
        axisY: {
            title: "Number of Vehicles",
            gridThickness: 0
        },
        data: [{
            type: "line",
            xValueFormatString: "DD MMM, YYYY HH:mm",
            yValueFormatString: "#,##0",
            dataPoints: formattedDataPoints
        }]
    });
    chart.render();
}



const setupDoughnutCharts = (vehicleTypesData, wheelsData) => {
    createDoughnutChart("chartContainer", "Parked Vehicle Types", vehicleTypesData);
    createDoughnutChart("wheelsChartContainer", "Wheels Distribution", wheelsData);
}

