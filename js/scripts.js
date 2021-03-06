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
    // UPDATED AS OF ENGLAND - DENMARK

    // JAMIE
    var jamScores = 4,
        jamResults = 17,
        jamHome = 15,
        jamAway = 17,
        jamGD = 7,
        jamNorm = (3*jamScores) + jamResults,
        jamUEFA = (3*jamScores) + jamResults + jamHome + jamAway + jamGD;
 
    // MATT
    var mattScores = 5,
        mattResults = 22,
        mattHome = 22,
        mattAway = 14,
        mattGD = 8,
        mattNorm = (3*mattScores) + mattResults,
        mattUEFA = (3*mattScores) + mattResults + mattHome + mattAway + mattGD;

    // PHIL
    var philScores = 5,
        philResults = 22,
        philHome = 12,
        philAway = 20,
        philGD = 8,
        philNorm = (3*philScores) + philResults,
        philUEFA = (3*philScores) + philResults + philHome + philAway + philGD;
    
    var normalScores = [
        {id:1, name:"Jamie", results:jamResults, scores:jamScores, points:jamNorm},
        {id:2, name:"Matt", results:mattResults, scores:mattScores, points:mattNorm},
        {id:3, name:"Phil", results:philResults, scores:philScores, points:philNorm},
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

   var uefaScores = [
       {id:1, name:"Jamie", results:jamResults, scores:jamScores, home:jamHome, away:jamAway, diff:jamGD, points:jamUEFA},
       {id:2, name:"Matt", results:mattResults, scores:mattScores, home:mattHome, away:mattAway, diff:mattGD, points:mattUEFA},
       {id:3, name:"Phil", results:philResults, scores:philScores, home:philHome, away:philAway, diff:philGD, points:philUEFA},
    ];

    var uefatable = new Tabulator("#uefaRules", {
        data:uefaScores, //assign data to table
        layout: "fitDataTable",
        columns:[ //Define Table Columns
            {title:"Name", field:"name", hozAlign:"center"},
            {title:"Correct Results", field:"results", sorter:"number", hozAlign:"center"},
            {title:"Correct Scores", field:"scores", sorter:"number", hozAlign:"center"},
            {title:"Home Goals", field:"home", sorter:"number", hozAlign:"center"},
            {title:"Away Goals", field:"away", sorter:"number", hozAlign:"center"},
            {title:"Goal Difference", field:"diff", sorter:"number", hozAlign:"center"},
            {title:"Total Points", field:"points", sorter:"number", hozAlign:"center"},
        ],
        initialSort:[
            {column:"points", dir:"desc"},
        ],
    });
});
