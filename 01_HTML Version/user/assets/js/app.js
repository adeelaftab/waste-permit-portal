! function(i) {
    "use strict";
    var t = function() {};
    t.prototype.intSlimscrollmenu = function() {
        i(".slimscroll-menu").slimscroll({
            height: "auto",
            position: "right",
            size: "5px",
            color: "#9ea5ab",
            wheelStep: 5,
            touchScrollStep: 50
        })
    }, t.prototype.initSlimscroll = function() {
        i(".slimscroll").slimscroll({
            height: "auto",
            position: "right",
            size: "5px",
            color: "#9ea5ab",
            touchScrollStep: 50
        })
    }, t.prototype.initMetisMenu = function() {
        i("#side-menu").metisMenu()
    }, t.prototype.initLeftMenuCollapse = function() {
        i(".button-menu-mobile").on("click", function(t) {
            t.preventDefault(), i("body").toggleClass("enlarged")
        })
    }, t.prototype.initEnlarge = function() {
        i(window).width() < 1025 ? i("body").addClass("enlarged") : i("body").removeClass("enlarged")
    }, t.prototype.initActiveMenu = function() {
        i("#sidebar-menu a").each(function() {
            var t = window.location.href.split(/[?#]/)[0];
            this.href == t && (i(this).addClass("active"), i(this).parent().addClass("active"), i(this).parent().parent().addClass("in"), i(this).parent().parent().prev().addClass("active"), i(this).parent().parent().parent().addClass("active"), i(this).parent().parent().parent().parent().addClass("in"), i(this).parent().parent().parent().parent().parent().addClass("active"))
        })
    }, t.prototype.initComponents = function() {
        i('[data-toggle="tooltip"]').tooltip(), i('[data-toggle="popover"]').popover()
    }, t.prototype.initHeaderCharts = function() {
        i("#header-chart-1").sparkline([8, 6, 4, 7, 10, 12, 7, 4, 9, 12, 13, 11, 12], {
            type: "bar",
            height: "35",
            barWidth: "5",
            barSpacing: "3",
            barColor: "#3eb7ba"
        }), i("#header-chart-2").sparkline([8, 6, 4, 7, 10, 12, 7, 4, 9, 12, 13, 11, 12], {
            type: "bar",
            height: "35",
            barWidth: "5",
            barSpacing: "3",
            barColor: "#7a6fbe"
        })
    }, t.prototype.init = function() {
        this.intSlimscrollmenu(), this.initSlimscroll(), this.initMetisMenu(), this.initLeftMenuCollapse(), this.initEnlarge(), this.initActiveMenu(), this.initComponents(), this.initHeaderCharts(), Waves.init()
    }, i.MainApp = new t, i.MainApp.Constructor = t
}(window.jQuery),
function(t) {
    "use strict";
    window.jQuery.MainApp.init()
}();