! function (e) {
  "use strict";
  var a = function () {};
  a.prototype.createLineChart = function (e, a, r, t, i, o) {
    Morris.Line({
      element: e,
      data: a,
      xkey: r,
      ykeys: t,
      labels: i,
      hideHover: "auto",
      gridLineColor: "#eef0f2",
      resize: !0,
      lineColors: o,
      lineWidth: 2
    })
  }, a.prototype.createBarChart = function (e, a, r, t, i, o) {
    Morris.Bar({
      element: e,
      data: a,
      xkey: r,
      ykeys: t,
      labels: i,
      gridLineColor: "#eef0f2",
      barSizeRatio: .4,
      resize: !0,
      hideHover: "auto",
      barColors: o
    })
  }, a.prototype.createAreaChart = function (e, a, r, t, i, o, b, y) {
    Morris.Area({
      element: e,
      pointSize: 0,
      lineWidth: 0,
      data: t,
      xkey: i,
      ykeys: o,
      labels: b,
      resize: !0,
      gridLineColor: "#eee",
      hideHover: "auto",
      lineColors: y,
      fillOpacity: .6,
      behaveLikeLine: !0
    })
  }, a.prototype.createDonutChart = function (e, a, r) {
    Morris.Donut({
      element: e,
      data: a,
      resize: !0,
      colors: r
    })
  }, a.prototype.createStackedChart = function (e, a, r, t, i, o) {
    Morris.Bar({
      element: e,
      data: a,
      xkey: r,
      ykeys: t,
      stacked: !0,
      labels: i,
      hideHover: "auto",
      barSizeRatio: .4,
      resize: !0,
      gridLineColor: "#eeeeee",
      barColors: o
    })
  }, a.prototype.init = function () {
    this.createLineChart("morris-line-example", [{
      y: "2009",
      a: 50,
      b: 80,
      c: 20
    }, {
      y: "2010",
      a: 130,
      b: 100,
      c: 80
    }, {
      y: "2011",
      a: 80,
      b: 60,
      c: 70
    }, {
      y: "2012",
      a: 70,
      b: 200,
      c: 140
    }, {
      y: "2013",
      a: 180,
      b: 140,
      c: 150
    }, {
      y: "2014",
      a: 105,
      b: 100,
      c: 80
    }, {
      y: "2015",
      a: 250,
      b: 150,
      c: 200
    }], "y", ["a", "b", "c"], ["Activated", "Pending", "Deactivated"], ["#ccc", "#3eb7ba", "#36508b"]);
    this.createBarChart("morris-bar-example", [{
      y: "2009",
      a: 100,
      b: 90
    }, {
      y: "2010",
      a: 75,
      b: 65
    }, {
      y: "2011",
      a: 50,
      b: 40
    }, {
      y: "2012",
      a: 75,
      b: 65
    }, {
      y: "2013",
      a: 50,
      b: 40
    }, {
      y: "2014",
      a: 75,
      b: 65
    }, {
      y: "2015",
      a: 100,
      b: 90
    }, {
      y: "2016",
      a: 90,
      b: 75
    }], "y", ["a", "b"], ["Series A", "Series B"], ["#3eb7ba", "#36508b"]);
    this.createAreaChart("morris-area-example", 0, 0, [{
      y: "2007",
      a: 0,
      b: 0,
      c: 0
    }, {
      y: "2008",
      a: 150,
      b: 45,
      c: 15
    }, {
      y: "2009",
      a: 60,
      b: 150,
      c: 195
    }, {
      y: "2010",
      a: 180,
      b: 36,
      c: 21
    }, {
      y: "2011",
      a: 90,
      b: 60,
      c: 360
    }, {
      y: "2012",
      a: 75,
      b: 240,
      c: 120
    }, {
      y: "2013",
      a: 30,
      b: 30,
      c: 30
    }], "y", ["a", "b", "c"], ["Series A", "Series B", "Series C"], ["#ccc", "#36508b", "#3eb7ba"]);
    this.createDonutChart("morris-donut-example", [{
      label: "FDC",
      value: 12
    }, {
      label: "Waste Permits",
      value: 30
    }, {
      label: "Sampling Permits",
      value: 20
    }], ["#f0f1f4", "#3eb7ba", "#36508b"]);
    this.createStackedChart("morris-bar-stacked", [{
      y: "2005",
      a: 45,
      b: 180
    }, {
      y: "2006",
      a: 75,
      b: 65
    }, {
      y: "2007",
      a: 100,
      b: 90
    }, {
      y: "2008",
      a: 75,
      b: 65
    }, {
      y: "2009",
      a: 100,
      b: 90
    }, {
      y: "2010",
      a: 75,
      b: 65
    }, {
      y: "2011",
      a: 50,
      b: 40
    }, {
      y: "2012",
      a: 75,
      b: 65
    }, {
      y: "2013",
      a: 50,
      b: 40
    }, {
      y: "2014",
      a: 75,
      b: 65
    }, {
      y: "2015",
      a: 100,
      b: 90
    }, {
      y: "2016",
      a: 80,
      b: 65
    }], "y", ["a", "b"], ["Series A", "Series B"], ["#36508b", "#f0f1f4"])
  }, e.MorrisCharts = new a, e.MorrisCharts.Constructor = a
}(window.jQuery),
function (e) {
  "use strict";
  window.jQuery.MorrisCharts.init()
}();
