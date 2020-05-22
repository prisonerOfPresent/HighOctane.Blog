// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const currentTheme = localStorage.getItem('theme');

if (currentTheme) {
    document.documentElement.setAttribute('data-theme', currentTheme);
}


function setDarkBlueTheme() {
    switchTheme('darkblue');
}


function setDarkTheme() {
    switchTheme('dark');
}

function setLightTheme() {
    switchTheme('light');
}

function setLightBlueTheme() {
    switchTheme('lightblue');
}

function setRedTheme() {
    switchTheme('');
}


function switchTheme(val) {
        document.documentElement.setAttribute('data-theme', val);
        localStorage.setItem('theme', val);
}

const tagContainer = document.getElementById('tagContainer');

const tagInput = document.getElementById('tagTextField');

const submitButton = document.getElementById('submitBtn');

var tags = [];

function reset() {
    document.querySelectorAll(".tag").forEach(function (tag) {
        tag.parentElement.removeChild(tag);
    });
}


function addTags() {
    reset();
    tags.slice().reverse().forEach(function (tag) {
        const actualTag = createTag(tag);
        tagContainer.prepend(actualTag);
    })
    var tagString = '';
    tags.forEach(function (tag) { tagString += tag + ','; });
    tagString = tagString.substring(0, tagString.length - 1);
    document.getElementById("tags").value = tagString;
}


tagInput.addEventListener('keyup', function (e) {
    if (e.keyCode === 13) {
        tags.push(tagInput.value);
        addTags();
        tagInput.value = '';
    }
});

function EnterKeyFilter() {
    if (window.event.keyCode == 13) {
    event.returnValue = false;
        event.cancel = true;
    }
}

document.addEventListener('click', function (e) {
    if (e.target.tagName === 'I') {
        const value = e.target.getAttribute('data-item');
        console.log("The clicked value is : " + value);
        var index = 0;
        tags.forEach(function (tag) {
            if (tag === value)
                index = tags.indexOf(tag);
        });
        tags.splice(index,1);        
        addTags();
    }
});


function createTag(label) {
    const div = document.createElement("div");
    div.setAttribute("class", 'tag');
    const span = document.createElement("span");
    span.innerHTML = label;
    const closeButton = document.createElement("i");
    closeButton.setAttribute("class", "material-icons");
    closeButton.setAttribute("data-item", label);
    closeButton.innerHTML = "close";
    div.appendChild(span);
    div.appendChild(closeButton);
    return div;
}