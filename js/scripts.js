/*!
    * Start Bootstrap - Resume v6.0.1 (https://startbootstrap.com/template-overviews/resume)
    * Copyright 2013-2020 Start Bootstrap
    * Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-resume/blob/master/LICENSE)
    */
    (function ($) {
    "use strict"; // Start of use strict

    // Smooth scrolling using jQuery easing
    $('a.js-scroll-trigger[href*="#"]:not([href="#"])').click(function () {
        if (
            location.pathname.replace(/^\//, "") ==
                this.pathname.replace(/^\//, "") &&
            location.hostname == this.hostname
        ) {
            var target = $(this.hash);
            target = target.length
                ? target
                : $("[name=" + this.hash.slice(1) + "]");
            if (target.length) {
                $("html, body").animate(
                    {
                        scrollTop: target.offset().top,
                    },
                    1000,
                    "easeInOutExpo"
                );
                return false;
            }
        }
    });

    // Closes responsive menu when a scroll trigger link is clicked
    $(".js-scroll-trigger").click(function () {
        $(".navbar-collapse").collapse("hide");
    });

    // Activate scrollspy to add active class to navbar items on scroll
    $("body").scrollspy({
        target: "#sideNav",
    });
})(jQuery); // End of use strict

$(document).ready(function() {
    // BEGINNING

    // JAMIE
    var jamScores = 0,
        jamResults = 0,
        jamNorm = (3*jamScores) + jamResults;
 
    // MATT
    var mattScores = 0,
        mattResults = 0,
        mattNorm = (3*mattScores) + mattResults;

    // PHIL
    var philScores = 0,
        philResults = 0,
        philNorm = (3*philScores) + philResults;

       // HANNAH
    var hanScores = 0,
        hanResults = 0,
        hanNorm = (3*hanScores) + hanResults;

       // SUE
    var sueScores = 0,
        sueResults = 0,
        sueNorm = (3*sueScores) + sueResults;
    
    var normalScores = [
      {id:1, name:"Jamie", results:jamResults, scores:jamScores, points:jamNorm},
      {id:2, name:"Matt", results:mattResults, scores:mattScores, points:mattNorm},
      {id:3, name:"Phil", results:philResults, scores:philScores, points:philNorm},
      {id:4, name:"Hannah", results:hanResults, scores:hanScores, points:hanNorm},
      {id:5, name:"Sue", results:sueResults, scores:sueScores, points:suePoints}
    ];

    var table = new Tabulator("#normalRules", {
        data:normalScores, //assign data to table
        layout: "fitDataTable",
        columns:[ //Define Table Columns
            {title:"Name", field:"name", hozAlign:"center"},
            {title:"Correct Results", field:"results", sorter:"number", hozAlign:"center"},
            {title:"Correct Scores", field:"scores", sorter:"number", hozAlign:"center"},
            {title:"Total Points", field:"points", sorter:"number", hozAlign:"center"},
        ],
        initialSort:[
            {column:"points", dir:"desc"},
        ],
   });
});
