document.addEventListener('DOMContentLoaded', function () {
    var searchButton = document.getElementById('searchButton');

    var isRefreshed = performance.navigation.type === 1 ||
        performance.getEntriesByType("navigation")[0].type === "reload"; 

    var fromNavbar = sessionStorage.getItem('fromNavbar') === 'true'; 

    var isDirectLink = document.referrer === "" || new URL(document.referrer).origin !== window.location.origin;

    if (searchButton && (isRefreshed || fromNavbar || isDirectLink)) {
        searchButton.click();
        sessionStorage.removeItem('fromNavbar');
    }
});