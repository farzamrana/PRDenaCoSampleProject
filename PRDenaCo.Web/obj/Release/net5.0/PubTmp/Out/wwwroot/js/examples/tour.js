'use strict';
$(document).ready(function () {

    $(document).on('click', 'a.tour', function(){
        Tour.run([
            {
                element: $('.header .header-body ul.navbar-nav'),
                content: 'This is the title bar.',
                position: 'bottom'
            },
            {
                element: $('.page-title'),
                content: 'This is the main title of the page.',
                position: 'bottom'
            },
            {
                element: $('.tour-card'),
                content: 'This is the area that covers the content.',
                position: 'top'
            },
            {
                element: $('.tour-card a.btn'),
                content: 'This is the learn more button.',
                position: 'top'
            }
        ]);
    });

});