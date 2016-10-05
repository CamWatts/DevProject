var app = angular.module("myapp", []);

app.controller("reportController", function ($scope) {

    GetProducts($scope)

    $scope.generateMonthlyReport = function (months, products) {
        if (angular.isUndefined(months) || months == null) {
            window.alert("Please select the number of months back to report")
        }

        months = Math.floor(months);
        productList = products;
        count = productList.length;

        GetMonthlyReports(months, count, $scope);
    }

    $scope.generateWeeklyReport = function (weeks, products) {
        if (angular.isUndefined(weeks) || weeks == null) {
            window.alert("Please select the number of months back to report")
        }

        weeks = Math.floor(weeks);
        productList = products;
        count = productList.length;

        GetWeeklyReports(weeks, count, $scope);
    }

    /*
    $scope.createCSV = function () {
        var outputFile = window.prompt("What do you want to name your output file?") || 'monthlyreport';
        outputFile = outputFile.replace('.csv','') + '.csv';

        CreateCSV($('#tbl_monthly > table'), outputFile);
    }
    */

});

function GetProducts($scope) {

    var jqxhr = $.get("/api/project/getallproducts", function (response) {
    },
        "json").success(function (response) {
            $scope.Products = response.ProductDtoList;
            $scope.$apply();
        }).fail(function (response) {

        });
};

function GetMonthlyReports(months, productCount, $scope) {

    var jqxhr = $.get("/api/project/getmonthlyreports?months=" + months + "&productCount=" + productCount, function (response) {
    },
        "json").success(function (response) {
            $scope.MonthlyReports = response.ReportDtoList;
            $scope.$apply();
        }).fail(function (response) {

        });
};

function GetWeeklyReports(weeks, productCount, $scope) {

    var jqxhr = $.get("/api/project/getweeklyreports?months=" + weeks + "&productCount=" + productCount, function (response) {
    },
        "json").success(function (response) {
            $scope.WeeklyReports = response.ReportDtoList;
            $scope.$apply();
        }).fail(function (response) {

        });
};

$(document).ready(function () {

    function exportTableToCSV($table, filename) {
        var $headers = $table.find('tr:has(th)')
            , $rows = $table.find('tr:has(td)')
            , tmpColDelim = String.fromCharCode(11) // vertical tab character
            , tmpRowDelim = String.fromCharCode(0) // null character
            , colDelim = '","'
            , rowDelim = '"\r\n"';

        var csv = '"';
        csv += formatRows($headers.map(grabRow));
        csv += rowDelim;
        csv += formatRows($rows.map(grabRow)) + '"';

        var csvData = 'data:application/csv;charset=utf-8,' + encodeURIComponent(csv);

        // For IE
        if (window.navigator.msSaveOrOpenBlob) {
            var blob = new Blob([decodeURIComponent(encodeURI(csv))], {
                type: "text/csv;charset=utf-8;"
            });
            navigator.msSaveBlob(blob, filename);
        } else {
            $(this)
                .attr({
                    'download': filename
                    , 'href': csvData
                    , 'target': '_blank'

                });
        }

        function formatRows(rows) {
            return rows.get().join(tmpRowDelim)
                .split(tmpRowDelim).join(rowDelim)
                .split(tmpColDelim).join(colDelim);
        }

        function grabRow(i, row) {
            var $row = $(row);
            var $cols = $row.find('td');
            if (!$cols.length) $cols = $row.find('th');

            return $cols.map(grabCol)
                .get().join(tmpColDelim);
        }

        function grabCol(j, col) {
            var $col = $(col),
                $text = $col.text();

            return $text.replace('"', '""');
        }
    }

    $(".exportMonthly").on('click', function (event) {
        exportTableToCSV.apply(this, [$('#tbl_monthly>table'), 'monthlyreport.csv']);
    })

    $(".exportWeekly").on('click', function (event) {
        exportTableToCSV.apply(this, [$('#tbl_weekly>table'), 'weeklyreport.csv']);
    })

});
