
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

var pageStartTime = performance.now();

// Called by Unity
function unityLoadingFinished(unityRealTimeSiceStartup, unityVersion) {
    var timeDiv = document.getElementById('startupTime');

    if(timeDiv == null || timeDiv == 'undefined') {
        timeDiv = document.createElement('div');
        timeDiv.id = 'startupTime';
        document.body.appendChild(timeDiv);
    }

    var currentTime = performance.now();
    var startupTimeSeconds = ((currentTime - pageStartTime) / 1000.0).toFixed(2);

    timeDiv.innerHTML = `<h3>Startup time Unity ${unityVersion}</h3><br /><dl><dt>Page</dt> <dd>${startupTimeSeconds}s</dd><br /><dt>Unity</dt> <dd>${unityRealTimeSiceStartup}s</dd></dl>`;
    console.info(`Startup finished - Page time: ${startupTimeSeconds}s, Unity real time since startup: ${unityRealTimeSiceStartup}s`);
}

initializeToggleButton(false);
initialzeDebugConsole();
