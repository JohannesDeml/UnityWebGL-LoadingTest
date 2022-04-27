
function initialzeDebugConsole() {
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

    defaultConsoleLog = console.log;
    console.log = (message) => { divLog(message, 'log'); defaultConsoleLog(message) };

    defaultConsoleInfo = console.info;
    console.info = (message) => { divLog(message, 'info'); defaultConsoleInfo(message) };

    defaultConsoleDebug = console.debug;
    console.debug = (message) => { divLog(message, 'debug'); defaultConsoleDebug(message) };

    defaultConsoleWarn = console.warn;
    console.warn = (message) => { divLog('⚠️ ' + message, 'warn'); defaultConsoleWarn(message) };

    defaultConsoleError = console.error;
    console.error = (message) => { divLog('🔴 ' + message, 'error'); defaultConsoleError(message) };
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
