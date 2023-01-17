var consoleDiv;


function setCookie(cname, cvalue, exdays) {
    const d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    let name = cname + "=";
    let ca = document.cookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

var scrollToBottom = true;
if(getCookie("scrollToBottom") === "false") {
    scrollToBottom = false;
}

function initialzeDebugConsole() {
    let debugConsole = document.createElement('div');
    debugConsole.id = 'debugConsole';
    document.body.appendChild(debugConsole);
    consoleDiv = document.createElement('div');
    consoleDiv.className = 'log-entries';

    addDebugConsoleTopBar(debugConsole, consoleDiv);

    debugConsole.appendChild(consoleDiv);

    var consoleInput = document.createElement("input");
    consoleInput.className = 'console-input';
    consoleInput.type = "text";
    consoleInput.value = 'unityGame.SendMessage("WebGL", "Help");';
    debugConsole.appendChild(consoleInput);

    consoleInput.onkeydown = function (e) {
        if (['Enter', 'NumpadEnter'].includes(e.key)) {
            console.log(`Evaluating ${consoleInput.value}`);
            eval(consoleInput.value);
        }
    };

    setupConsoleLogPipe();
}

function addDebugConsoleTopBar(debugConsole, consoleDiv) {
    var consoleTopBar = document.createElement('div');
    consoleTopBar.className = 'console-top-bar';
    debugConsole.appendChild(consoleTopBar);

    // Clear logs button
    var clearButton = document.createElement('button');
    clearButton.classList.add('clear-button', 'bubble-click-indicator');
    clearButton.title = 'clear logs';
    consoleTopBar.appendChild(clearButton);

    var trashIcon = document.createElement('div');
    trashIcon.classList.add('icon', 'gg-trash');
    clearButton.appendChild(trashIcon);
    clearButton.addEventListener('click', function () {
        consoleDiv.innerHTML = '';
        clearButton.setAttribute('data-before', 'cleared');

        // Show copy feedback to the user
        clearButton.classList.add('active');
        setTimeout(function () {
            clearButton.classList.remove('active');
        }, 1500);
    });


    // Copy all logs button
    var copyButton = document.createElement('button');
    copyButton.classList.add('copy-button', 'bubble-click-indicator');
    copyButton.title = 'copy to clipboard';
    consoleTopBar.appendChild(copyButton);

    var copyIcon = document.createElement('div');
    copyIcon.classList.add('icon', 'gg-copy');
    copyButton.appendChild(copyIcon);

    copyButton.addEventListener('click', function () {
        if (navigator.clipboard) {
            var clipboardText = consoleDiv.innerText;
            navigator.clipboard.writeText(clipboardText).then(function () {
                copyButton.setAttribute('data-before', 'copied');
            }).catch(function () {
                copyButton.setAttribute('data-before', 'copy with navigator.clipboard failed');
            });
        } else {
            copyButton.setAttribute('data-before', 'copy failed - navigator.clipboard not supported');
        }

        // Show copy feedback to the user
        copyButton.classList.add('active');
        setTimeout(function () {
            copyButton.classList.remove('active');
        }, 1500);
    });

    // Lock scrolling to bottom button
    var toBottomButton = document.createElement('button');
    toBottomButton.classList.add('to-bottom-button', 'bubble-click-indicator');
    toBottomButton.title = 'lock scrolling to bottom';
    consoleTopBar.appendChild(toBottomButton);

    var arrowDownIcon = document.createElement('div');
    arrowDownIcon.classList.add('icon', 'gg-arrow-down');
    toBottomButton.appendChild(arrowDownIcon);
    applyScrollToBottom(toBottomButton, consoleDiv);

    toBottomButton.addEventListener('click', function () {
        scrollToBottom = !scrollToBottom;
        setCookie("scrollToBottom", scrollToBottom, 365);
        applyScrollToBottom(toBottomButton, consoleDiv);
        // Show copy feedback to the user
        toBottomButton.classList.add('active');
        setTimeout(function () {
            toBottomButton.classList.remove('active');
        }, 1500);
    });
}

function applyScrollToBottom(toBottomButton, consoleDiv) {
    toBottomButton.setAttribute('data-before', scrollToBottom ? 'locked' : 'unlocked');
    if (scrollToBottom) {
        toBottomButton.classList.add("locked");
        consoleDiv.scrollTo(0, consoleDiv.scrollHeight);
    } else {
        toBottomButton.classList.remove("locked");
    }
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

        htmlLog(divMessage.trim(), logLevel);
        consoleLogFunction(consoleMessage, ...consoleArgs);
    };
}

function htmlLog(message, className) {
    var entry = document.createElement('div');
    entry.classList.add('entry', className);
    consoleDiv.appendChild(entry);

    var text = document.createElement('p');
    // Remove last line break if existing
    if (message.endsWith("\n")) {
        message = message.substring(0, message.length - "\n".length);
    }
    message = message.replaceAll("\n", "<br />");
    text.innerHTML = message;
    entry.appendChild(text);

    var copyButton = document.createElement('button');
    copyButton.classList.add('copy-button', 'bubble-click-indicator');
    copyButton.title = 'copy to clipboard';
    entry.appendChild(copyButton);

    var copyIcon = document.createElement('div');
    copyIcon.classList.add('icon', 'gg-copy');
    copyButton.appendChild(copyIcon);

    copyButton.addEventListener('click', function () {
        if (navigator.clipboard) {
            var clipboardText = text.innerText;
            navigator.clipboard.writeText(clipboardText).then(function () {
                copyButton.setAttribute('data-before', 'copied');
            }).catch(function () {
                copyButton.setAttribute('data-before', 'copy with navigator.clipboard failed');
            });
        } else {
            copyButton.setAttribute('data-before', 'copy failed - navigator.clipboard not supported');
        }

        // Show copy feedback to the user
        copyButton.classList.add('active');
        setTimeout(function () {
            copyButton.classList.remove('active');
        }, 1500);
    });


    if(scrollToBottom) {
        consoleDiv.scrollTo(0, consoleDiv.scrollHeight);
    }
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
        if (typeof unityGame === 'undefined') {
            return;
        }
        if (event.currentTarget.checked) {
            unityGame.SendMessage("WebGL", "DisableCaptureAllKeyboardInput");
        } else {
            if (typeof unityCaptureAllKeyboardInputDefault !== 'undefined' && unityCaptureAllKeyboardInputDefault === 'false') {
                unityGame.SendMessage("WebGL", "DisableCaptureAllKeyboardInput");
            }
            else {
                unityGame.SendMessage("WebGL", "EnableCaptureAllKeyboardInput");
            }
        }
    })
}

function getInfoPanel() {
    let infoPanel = document.getElementById('infoPanel');

    if (infoPanel == null || infoPanel == 'undefined') {
        infoPanel = document.createElement('div');
        infoPanel.id = 'infoPanel';
        document.body.appendChild(infoPanel);
        var infoHeader = document.createElement('h3');
        if (typeof unityVersion != `undefined` && typeof webGlVersion != `undefined`) {
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
    if (visible) {
        infoPanel.style.visibility = 'visible';
    }
    else {
        infoPanel.style.visibility = 'hidden';
    }
}

function getOrCreateInfoEntry(id) {
    const infoPanel = getInfoPanel();
    var entry = infoPanel.querySelector(':scope > #' + id);

    if (entry == null || entry == 'undefined') {
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
                        <dd class='tracking-seconds'>${(value / 1000.0).toFixed(2)}</dd>
                        <dd class='tracking-milliseconds'>${value}</dd>
                      </div>`;
    });
    innerHtml += '</dl>';
    trackingDiv.innerHTML = innerHtml;
}

initializeToggleButton(false);
initialzeDebugConsole();
