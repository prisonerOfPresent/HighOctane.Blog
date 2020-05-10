// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const currentTheme = localStorage.getItem('theme');

if (currentTheme) {
    document.documentElement.setAttribute('data-theme', currentTheme);
}


function setDarkTheme() {
    switchTheme('dark');
}

function setLightTheme() {
    switchTheme('light');
}


function setRedTheme() {
    switchTheme('');
}


function switchTheme(val) {
        document.documentElement.setAttribute('data-theme', val);
        localStorage.setItem('theme', val);
}