
function initialzeDebugConsole() {
    // Store actual console log functions, since they will be overwritten with new logic
    defaultConsoleLog = console.log;
    defaultConsoleInfo = console.info;
    defaultConsoleDebug = console.debug;
    defaultConsoleWarn = console.warn;
    defaultConsoleError = console.error;

    var consoleDiv = document.createElement('div');
    consoleDiv.id = 'debugConsole';
    document.body.appendChild(consoleDiv);

    divLog = (message, className) => {
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
            navigator.clipboard.writeText(message);

            copyButton.classList.add('active');
            setTimeout(function () {
                copyButton.classList.remove('active');
            }, 1500);
        });
    };

    parseMessageAndLog = (message, logLevel, consoleLogFunction) => {
        let index = 0;
        let consoleArgs = [];
        let consoleMessage="";
        let divMessage = "";
        // parse unity color tags to render them nicely in the console
        while(index <= message.length) {
            let startTagStart = message.indexOf("<color=", index);
            if(startTagStart == -1) {
                // Parse the reset of the message without styling
                let remainingMessage = message.substring(index);
                consoleMessage += `%c${remainingMessage}`;
                consoleArgs.push("color:inherit");
                divMessage += remainingMessage;
                break;
            }

            if(startTagStart>index) {
                let textBeforeTag = message.substring(index, startTagStart);
                consoleMessage += `%c${textBeforeTag}`;
                consoleArgs.push("color:inherit");
                divMessage += textBeforeTag;
            }

            let startTagEnd = message.indexOf(">", startTagStart);
            let closingTag = message.indexOf("</color>", startTagStart);
            if(startTagEnd == -1 || closingTag == -1) {
                // Tag is set up in the wrong way, abort parsing
                defaultConsoleError({error: "Could not parse color tag, since no end tag was found", startStartIndex: startTagStart, startEndIndex: startTagEnd, closingIndex: closingTag});
                let remainingMessage = message.substring(index);
                consoleMessage += `%c${remainingMessage}`;
                consoleArgs.push("color:inherit");
                divMessage += remainingMessage;
                break;
            }
            let color = message.substring(startTagStart + "<color=".length, startTagEnd);
            let text = message.substring(startTagEnd + 1, closingTag);
            consoleMessage += `%c${text}`
            consoleArgs.push("color:"+color);
            divMessage += `<span style="color:${color};">${text}</span>`
            index = closingTag + "</color>".length;
        }

        divLog(divMessage, logLevel);
        consoleLogFunction(consoleMessage, ...consoleArgs);
    };

    console.log = (message) => { parseMessageAndLog(message, 'log', defaultConsoleLog) };
    console.info = (message) => { parseMessageAndLog(message, 'info', defaultConsoleInfo) };
    console.debug = (message) => { parseMessageAndLog(message, 'debug', defaultConsoleDebug) };
    console.warn = (message) => { parseMessageAndLog(message, 'warn', defaultConsoleWarn) };
    console.error = (message) => { parseMessageAndLog(message, 'error', defaultConsoleError) };
}

function initializeToggleButton(startActive) {
    var inputDiv = document.createElement('input');
    inputDiv.type = 'checkbox';
    inputDiv.id = 'debugToggle';
    inputDiv.name = 'debugToggle';
    inputDiv.checked = startActive;
    document.body.appendChild(inputDiv);

    var labelDiv = document.createElement('label');
    labelDiv.className = 'debugToggleMenu';
    labelDiv.htmlFor = 'debugToggle';
    document.body.appendChild(labelDiv);
    var iconDiv = document.createElement('div');
    iconDiv.classList = 'icon';
    labelDiv.appendChild(iconDiv);
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
