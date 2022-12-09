
var consoleDiv;

function initialzeDebugConsole() {
    let debugConsole = document.createElement('div');
    debugConsole.id = 'debugConsole';
    document.body.appendChild(debugConsole);

    consoleDiv = document.createElement('div');
    consoleDiv.className = 'log-entries';
    debugConsole.appendChild(consoleDiv);

    var consoleInput = document.createElement("input");
    consoleInput.className = 'console-input';
    consoleInput.type = "text";
    consoleInput.value = 'unityGame.SendMessage("WebGL", "Help");';
    debugConsole.appendChild(consoleInput);

    consoleInput.onkeydown = function(e) {
        if ( ['Enter', 'NumpadEnter'].includes(e.key)) {
            console.log(`Evaluating ${consoleInput.value}`);
            eval(consoleInput.value);
        }
    };

    setupConsoleLogPipe();
}

function setupConsoleLogPipe() {
    // Store actual console log functions
    let defaultConsoleLog = console.log;
    let defaultConsoleInfo = console.info;
    let defaultConsoleDebug = console.debug;
    let defaultConsoleWarn = console.warn;
    let defaultConsoleError = console.error;

    // Overwrite log functions to parse and pipe to debug html console
    console.log = (message) => { parseMessageAndLog(message, 'log', defaultConsoleLog); };
    console.info = (message) => { parseMessageAndLog(message, 'info', defaultConsoleInfo); };
    console.debug = (message) => { parseMessageAndLog(message, 'debug', defaultConsoleDebug); };
    console.warn = (message) => { parseMessageAndLog(message, 'warn', defaultConsoleWarn); };
    console.error = (message) => { parseMessageAndLog(message, 'error', defaultConsoleError); };


    parseMessageAndLog = (message, logLevel, consoleLogFunction) => {
        let index = 0;
        let consoleArgs = [];
        let consoleMessage = "";
        let divMessage = "";
        // parse unity color tags to render them nicely in the console
        while (index <= message.length) {
            let startTagStart = message.indexOf("<color=", index);
            if (startTagStart == -1) {
                // Parse the reset of the message without styling
                let remainingMessage = message.substring(index);
                consoleMessage += `%c${remainingMessage}`;
                consoleArgs.push("color:inherit");
                divMessage += remainingMessage;
                break;
            }

            if (startTagStart > index) {
                let textBeforeTag = message.substring(index, startTagStart);
                consoleMessage += `%c${textBeforeTag}`;
                consoleArgs.push("color:inherit");
                divMessage += textBeforeTag;
            }

            let startTagEnd = message.indexOf(">", startTagStart);
            let closingTag = message.indexOf("</color>", startTagStart);
            if (startTagEnd == -1 || closingTag == -1) {
                // Tag is set up in the wrong way, abort parsing
                defaultConsoleError({ error: "Could not parse color tag, since no end tag was found", startStartIndex: startTagStart, startEndIndex: startTagEnd, closingIndex: closingTag });
                let remainingMessage = message.substring(index);
                consoleMessage += `%c${remainingMessage}`;
                consoleArgs.push("color:inherit");
                divMessage += remainingMessage;
                break;
            }
            let color = message.substring(startTagStart + "<color=".length, startTagEnd);
            let text = message.substring(startTagEnd + 1, closingTag);
            consoleMessage += `%c${text}`;
            consoleArgs.push("color:" + color);
            divMessage += `<span style="color:${color};">${text}</span>`;
            index = closingTag + "</color>".length;
        }

        htmlLog(divMessage, logLevel);
        consoleLogFunction(consoleMessage, ...consoleArgs);
    };
}

function htmlLog(message, className) {
    var entry = document.createElement('div');
    entry.classList.add('entry', className);
    consoleDiv.appendChild(entry);
    consoleDiv.scrollTop = consoleDiv.scrollHeight;

    var text = document.createElement('p');
    message = message.replaceAll("\n", "<br />");
    text.innerHTML = message;
    entry.appendChild(text);

    var copyButton = document.createElement('button');
    copyButton.className = 'copy-button';
    copyButton.title = 'copy to clipboard';
    entry.appendChild(copyButton);

    var copyIcon = document.createElement('div');
    copyIcon.classList.add('icon', 'gg-copy');
    copyButton.appendChild(copyIcon);

    copyButton.addEventListener('click', function () {
        function customCopyCommand(e) {
            // copy innerHTML as rich text
            e.clipboardData.setData("text/html", message);
            e.clipboardData.setData("text/plain", message);
            e.preventDefault();
        }
        document.addEventListener("copy", customCopyCommand);
        document.execCommand("copy");
        document.removeEventListener("copy", customCopyCommand);

        // Show copy feedback to the user
        copyButton.classList.add('active');
        setTimeout(function () {
            copyButton.classList.remove('active');
        }, 1500);
    });
}

function initializeToggleButton(startActive) {
    var debugToggle = document.createElement('input');
    debugToggle.type = 'checkbox';
    debugToggle.id = 'debugToggle';
    debugToggle.name = 'debugToggle';
    debugToggle.checked = startActive;
    document.body.appendChild(debugToggle);

    var debugLabel = document.createElement('label');
    debugLabel.className = 'debugToggleMenu';
    debugLabel.htmlFor = 'debugToggle';
    document.body.appendChild(debugLabel);
    var iconDiv = document.createElement('div');
    iconDiv.classList = 'icon';
    debugLabel.appendChild(iconDiv);

    debugToggle.addEventListener('change', (event) => {
        if(typeof unityGame === 'undefined') {
            return;
        }
        if (event.currentTarget.checked) {
            unityGame.SendMessage("WebGL","DisableCaptureAllKeyboardInput");
        } else {
            if(typeof unityCaptureAllKeyboardInputDefault !== 'undefined' && unityCaptureAllKeyboardInputDefault === 'false') {
                unityGame.SendMessage("WebGL","DisableCaptureAllKeyboardInput");
            }
            else {
                unityGame.SendMessage("WebGL","EnableCaptureAllKeyboardInput");
            }
        }
    })
}

function getInfoPanel() {
    let infoPanel = document.getElementById('infoPanel');

    if(infoPanel == null || infoPanel == 'undefined') {
        infoPanel = document.createElement('div');
        infoPanel.id = 'infoPanel';
        document.body.appendChild(infoPanel);
        var infoHeader = document.createElement('h3');
        if(typeof unityVersion != `undefined` && typeof webGlVersion != `undefined`) {
            // Set by WebGlBridge in Unity
            infoHeader.textContent = `Unity ${unityVersion} (${webGlVersion})`;
        } else {
            infoHeader.textContent = `Unity InfoPanel`;
        }
        infoPanel.appendChild(infoHeader);
    }

    return infoPanel;
}

function setInfoPanelVisible(visible) {
    const infoPanel = getInfoPanel();
    if(visible) {
        infoPanel.style.visibility = 'visible';
    }
    else {
        infoPanel.style.visibility = 'hidden';
    }
}

function getOrCreateInfoEntry(id) {
    const infoPanel = getInfoPanel();
    var entry = infoPanel.querySelector(':scope > #' + id);

    if(entry == null || entry == 'undefined') {
        entry = document.createElement('div');
        entry.id = id;
        infoPanel.appendChild(entry);
    }

    return entry;
}

function onAddTimeTracker(eventName) {
    refreshTrackingDiv();
}

function refreshTrackingDiv() {
    const trackingDiv = getOrCreateInfoEntry('tracking');
    let innerHtml = '<dl>';
    unityTimeTrackers.forEach((value, key, map) => {
        innerHtml += `<div id='tracking-${key}'>
                        <dt>${key}</dt>
                        <dd class='tracking-seconds'>${(value/1000.0).toFixed(2)}</dd>
                        <dd class='tracking-milliseconds'>${value}</dd>
                      </div>`;
    });
    innerHtml += '</dl>';
    trackingDiv.innerHTML = innerHtml;
}

initializeToggleButton(false);
initialzeDebugConsole();
