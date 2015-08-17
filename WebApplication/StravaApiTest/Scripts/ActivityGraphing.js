﻿

var chart;
var chartCursor;


function GraphData(data) {
    chart.dataProvider = JSON.parse(data);
    chart.validateData();
}

function timeFormatter(timeInSeconds) {
    var sec_num = parseInt(timeInSeconds, 10); // don't forget the second param
    var hours = Math.floor(sec_num / 3600);
    var minutes = Math.floor((sec_num - (hours * 3600)) / 60);
    var seconds = sec_num - (hours * 3600) - (minutes * 60);

    if (hours < 10) { hours = "0" + hours; }
    if (minutes < 10) { minutes = "0" + minutes; }
    if (seconds < 10) { seconds = "0" + seconds; }

    var time = minutes + '-' + seconds;

    time = hours + '-' + time;

    return time;
}

function paceFormatter(valueText, date, valueAxis)
{
    return 'hello';
}

AmCharts.ready(function () {
    // SERIAL CHART    
    chart = new AmCharts.AmSerialChart();
    //chart.pathToImages = "http://www.amcharts.com/lib/images/";
    chart.marginTop = 0;
    chart.marginRight = 10;
    chart.autoMarginOffset = 5;
    chart.zoomOutButton = {
        backgroundColor: '#000000',
        backgroundAlpha: 0.15
    };
    chart.dataDateFormat = "JJ-NN-SS";
    chart.categoryField = "x";
        
    // AXES
    // category
    var categoryAxis = chart.categoryAxis;
    categoryAxis.parseDates = true; // as our data is date-based, we set parseDates to true
    categoryAxis.minPeriod = "ss";
    categoryAxis.dashLength = 1;
    categoryAxis.gridAlpha = 0.15;
    categoryAxis.gridColor = 'ffffff';
    categoryAxis.axisColor = "#DADADA";    

    // value                
    var valueAxisHR = new AmCharts.ValueAxis();
    valueAxisHR.position = "right"
    valueAxisHR.title = "HR";
    valueAxisHR.maximum = 250;
    valueAxisHR.minimum = 30;
    valueAxisHR.gridColor = 'ffffff';
    chart.addValueAxis(valueAxisHR);

    var valueAxisPace = new AmCharts.ValueAxis();
    valueAxisPace.reversed = true;
    valueAxisPace.position = "right"
    valueAxisPace.duration = "ss";
    valueAxisPace.title = "Pace";
    valueAxisPace.offset = 55;
    valueAxisPace.gridColor = 'ffffff';
    valueAxisPace.minimum = 1800;
    valueAxisPace.maximum = 0;
    valueAxisPace.unit = "/km";
    chart.addValueAxis(valueAxisPace);

    var valueAxisAlt = new AmCharts.ValueAxis();
    valueAxisAlt.title = "Altitude";
    valueAxisAlt.gridCount = 0;
    //valueAxisAlt.visible = false;
    chart.addValueAxis(valueAxisAlt);

    // HEART RATE
    var graphHR = new AmCharts.AmGraph();
    graphHR.valueAxis = valueAxisHR;
    graphHR.title = "Heart Rate";
    graphHR.valueField = "y";
    graphHR.bullet = "round";
    graphHR.bulletBorderColor = "#FFFFFF";
    graphHR.bulletBorderThickness = 2;
    graphHR.lineThickness = 2;
    graphHR.lineColor = "#b5030d";
    graphHR.hideBulletsCount = 50; // this makes the chart to hide bullets when there are more than 50 series in selection
    chart.addGraph(graphHR);

    //PACE
    var graphPace = new AmCharts.AmGraph();
    graphPace.valueAxis = valueAxisPace;
    graphPace.title = "Pace";
    graphPace.valueField = "z";
    graphPace.bullet = "round";
    graphPace.bulletBorderColor = "#FFFFFF";
    graphPace.bulletBorderThickness = 2;
    graphPace.lineThickness = 2;
    graphPace.lineColor = "#1010cc";    
    graphPace.hideBulletsCount = 50; // this makes the chart to hide bullets when there are more than 50 series in selection
    chart.addGraph(graphPace);

    //ALTITUDE
    var graphAlt = new AmCharts.AmGraph();
    graphAlt.valueAxis = valueAxisAlt;
    graphAlt.title = "Altitude";
    graphAlt.valueField = "r";
    graphAlt.bullet = "round";
    graphAlt.bulletBorderColor = "#FFFFFF";
    graphAlt.bulletBorderThickness = 2;
    graphAlt.lineThickness = 2;
    graphAlt.lineColor = "#10cc10";
    graphAlt.fillAlphas = 0.6;
    graphAlt.hideBulletsCount = 50; // this makes the chart to hide bullets when there are more than 50 series in selection
    chart.addGraph(graphAlt);

    // red HR guide
    var guide1 = new AmCharts.Guide();
    guide1.value = 100;
    guide1.toValue = 190;
    //guide1.fillColor = "#CC0000";
    //guide1.fillAlpha = 0.2;
    guide1.dashLength = 2;
    guide1.inside = true;
    guide1.labelRotation = 90;
    guide1.label = "High HR";
    valueAxisHR.addGuide(guide1);
    
    // CURSOR
    chartCursor = new AmCharts.ChartCursor();
    chartCursor.cursorPosition = "mouse";
    chartCursor.categoryBalloonDateFormat = "JJ:NN:SS";
    chartCursor.categoryBalloonEnabled = true; 
    chart.addChartCursor(chartCursor);

    // SCROLLBAR
    var chartScrollbar = new AmCharts.ChartScrollbar();
    chartScrollbar.graph = graphAlt;
    chartScrollbar.scrollbarHeight = 40;
    chartScrollbar.color = "#FFFFFF";
    chartScrollbar.autoGridCount = true;
    chart.addChartScrollbar(chartScrollbar);

    // WRITE
    chart.write("chartdiv");    
});



function BuildGraphForActivity(activityId) {
    $.ajax({
        url: '/Api/activity/' + activityId,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            GraphData(data);
        },
        error: function () {
            alert("oh, shit.");
        }
    });
}