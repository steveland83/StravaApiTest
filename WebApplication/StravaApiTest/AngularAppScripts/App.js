
var angularApp = angular.module('AngularApp', ["ChartingModule"]);


(function(){
    var chartingModule = angular.module("ChartingModule", []);

    function timeFormatter(timeInSeconds)
    {
        var sec_num = parseInt(timeInSeconds, 10); // don't forget the second param
        var hours   = Math.floor(sec_num / 3600);
        var minutes = Math.floor((sec_num - (hours * 3600)) / 60);
        var seconds = sec_num - (hours * 3600) - (minutes * 60);

        if (hours   < 10) {hours   = "0"+hours;}
        if (minutes < 10) {minutes = "0"+minutes;}
        if (seconds < 10) {seconds = "0"+seconds;}

        var time = minutes+':'+seconds;

        if(hours>0)
            time=hours+':'+time;
        
        return time;
    }

    function PaceFormatter(pacems)
    {
        return timeFormatter(1000/(pacems));
    }

    //chartingModule.controller("graphingController", function ($scope, $location, GraphDataService) {
    //    $scope.options = {
    //        axes: {
    //            x: { key: 'x', type: 'linear', ticksFormatter: timeFormatter },
    //            y: { type: 'linear' },
    //            y2: { type: 'linear', ticksFormatter: PaceFormatter, max: 25 },
    //            y3: { type: 'linear' }
    //        },
    //        margin: {
    //            left: 100
    //        },
    //        series: [
    //          { y: 'y', axis: 'y', color: 'red', type: 'area', label: 'Heart Rate', width: 1 },
    //          { y: 'z', axis: 'y2', color: 'blue', type: "line", label: "Pace" },
    //          { y: 'r', axis: 'y', color: 'green', type: "area", label: "Altitude" }
    //        ],
    //        lineMode: 'linear',
    //        tension: 0.7,
    //        tooltip: { mode: 'scrubber', formatter: function (x, y, series) { return y; } },
    //        drawLegend: true,
    //        hideOverflow: false,
    //        drawDots: false
    //    };

    //    $scope.data = [];

    //    ApplyGraphData = function(data)
    //    {
    //        var jsonData = JSON.parse(data);
    //        $scope.data = jsonData;
    //    }

    //    var tempData = GraphDataService.LoadActivityGraphData(activityId)
    //        .then(
    //            function (data) {

    //                ApplyGraphData(data);

    //            }
    //        );
        
    //});

    //chartingModule.service("GraphDataService", function ($http, $q) {
    //    return { LoadActivityGraphData: LoadActivityGraphData };

    //    function LoadActivityGraphData(activityId)
    //    {
    //        var request = $http({
    //            url: "/api/activity/" + activityId,                
    //        });

    //        return (request.then(handleSuccess, handleError));              
    //    }

    //    function handleSuccess(response)
    //    {
    //        return (response.data);
    //    }

    //    function handleError(response) {
    //        if (
    //                    !angular.isObject(response.data) ||
    //                    !response.data.message
    //                    ) {

    //            return ($q.reject("An unknown error occurred."));

    //        }

    //        // Otherwise, use expected error message.
    //        return ($q.reject(response.data.message));
    //    }
    //});
})();

    