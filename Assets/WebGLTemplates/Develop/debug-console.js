
function initialzeDebugConsole() {
    var consoleDiv = document.createElement('div');
    consoleDiv.id = 'debugConsole';
    document.body.appendChild(consoleDiv);

    divLog = (message, className) => {
        var entry = document.createElement('p');
        entry.className = className;
        entry.innerHTML = message;
        consoleDiv.appendChild(entry);
        consoleDiv.scrollTop = consoleDiv.scrollHeight;
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

initializeToggleButton(false);
initialzeDebugConsole();
