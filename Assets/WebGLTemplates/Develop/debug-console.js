let consoleDiv;
let debugToggle;
let logCounterDivs = {};
let logCounter = {
  "log": 0,
  "info": 0,
  "debug": 0,
  "warn": 0,
  "error": 0
};

let scrollToBottom = true;
if (getCookie("scrollToBottom") === "false") {
  scrollToBottom = false;
}
let addTimestamp = true;
if (getCookie("addTimestamp") === "false") {
  addTimestamp = false;
}

initializeToggleButton(false);
initialzeDebugConsole();


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

function initialzeDebugConsole() {
  let debugConsole = document.createElement('div');
  debugConsole.id = 'debugConsole';
  document.body.appendChild(debugConsole);
  consoleDiv = document.createElement('div');
  consoleDiv.className = 'log-entries';

  addDebugConsoleTopBar(debugConsole);

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

function addDebugConsoleTopBar(debugConsole) {
  var consoleTopBar = document.createElement('div');
  consoleTopBar.className = 'console-top-bar';
  debugConsole.appendChild(consoleTopBar);

  addTopBarLogCounter(consoleTopBar);

  var separator = document.createElement('div');
  separator.classList.add('separator', 'element');
  consoleTopBar.appendChild(separator);

  // Add timestamp toggle
  var timestampButton = document.createElement('button');
  timestampButton.classList.add('timestamp-button', 'bubble-click-indicator', 'element');
  timestampButton.title = 'Add timestamp to logs';
  consoleTopBar.appendChild(timestampButton);

  var timestampIcon = document.createElement('div');
  timestampIcon.classList.add('icon');
  timestampButton.appendChild(timestampIcon);
  applyTimestamp(timestampButton);

  timestampButton.addEventListener('click', function () {
    addTimestamp = !addTimestamp;
    setCookie("addTimestamp", addTimestamp, 365);
    applyTimestamp(timestampButton);
    // Show feedback to the user
    timestampButton.classList.add('active');
    setTimeout(function () {
      timestampButton.classList.remove('active');
    }, 1500);
  });

  // Lock scrolling to bottom button
  var toBottomButton = document.createElement('button');
  toBottomButton.classList.add('to-bottom-button', 'bubble-click-indicator', 'element');
  toBottomButton.title = 'lock scrolling to bottom';
  consoleTopBar.appendChild(toBottomButton);

  var arrowDownIcon = document.createElement('div');
  arrowDownIcon.classList.add('icon');
  toBottomButton.appendChild(arrowDownIcon);
  applyScrollToBottom(toBottomButton);

  toBottomButton.addEventListener('click', function () {
    scrollToBottom = !scrollToBottom;
    setCookie("scrollToBottom", scrollToBottom, 365);
    applyScrollToBottom(toBottomButton);
    // Show feedback to the user
    toBottomButton.classList.add('active');
    setTimeout(function () {
      toBottomButton.classList.remove('active');
    }, 1500);
  });

  // Clear logs button
  var clearButton = document.createElement('button');
  clearButton.classList.add('clear-button', 'bubble-click-indicator', 'element');
  clearButton.title = 'clear logs';
  consoleTopBar.appendChild(clearButton);

  var trashIcon = document.createElement('div');
  trashIcon.classList.add('icon');
  clearButton.appendChild(trashIcon);
  clearButton.addEventListener('click', function () {
    consoleDiv.innerHTML = '';
    clearButton.setAttribute('data-before', 'cleared');
    // Show feedback to the user
    clearButton.classList.add('active');
    setTimeout(function () {
      clearButton.classList.remove('active');
    }, 1500);
  });


  // Copy all logs button
  var copyButton = document.createElement('button');
  copyButton.classList.add('copy-button', 'bubble-click-indicator', 'element');
  copyButton.title = 'copy to clipboard';
  consoleTopBar.appendChild(copyButton);

  var copyIcon = document.createElement('div');
  copyIcon.classList.add('icon');
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
}

function addTopBarLogCounter(consoleTopBar) {

  addLogToggle = (logLevels) => {
    var counter = document.createElement('div');
    const logLevelsArray = logLevels.split(',');
    counter.classList.add('log-counter', 'bubble-click-indicator', 'element', ...logLevelsArray);
    counter.setAttribute('data-loglevels', logLevels);
    counter.title = logLevels;
    let count = 0;
    logLevelsArray.forEach(logLevel => {
      count += logCounter[logLevel];
      logCounterDivs[logLevel] = counter;
    });
    consoleTopBar.appendChild(counter);

    var checkbox = document.createElement('input');
    checkbox.type = 'checkbox';
    checkbox.id = `counter-${logLevels}`;
    checkbox.name = checkbox.id;
    checkbox.checked = true;
    checkbox.setAttribute('hidden', 'true');
    counter.appendChild(checkbox);

    var counterText = document.createElement('label');
    counterText.className = 'counter';
    counterText.htmlFor = checkbox.id;
    counterText.innerHTML = count;
    counter.appendChild(counterText);

    checkbox.addEventListener('change', (event) => {
      var logLevels = event.currentTarget.parentNode.getAttribute('data-loglevels');
      const logLevelsArray = logLevels.split(',');
      var debugConsole = document.getElementById('debugConsole');
      if (event.currentTarget.checked) {
        logLevelsArray.forEach(element => {
          debugConsole.classList.remove(`hidelogs-${element}`);
        });
      } else {
        logLevelsArray.forEach(element => {
          debugConsole.classList.add(`hidelogs-${element}`);
        });
      }
    })
  }

  addLogToggle('debug,log,info');
  addLogToggle('warn');
  addLogToggle('error');
}

function applyScrollToBottom(toBottomButton) {
  toBottomButton.setAttribute('data-before', scrollToBottom ? 'locked' : 'unlocked');
  if (scrollToBottom) {
    toBottomButton.classList.add("locked");
  } else {
    toBottomButton.classList.remove("locked");
  }
  applyScrollToBottomSetting();
}

function applyScrollToBottomSetting() {
  if (scrollToBottom) {
    consoleDiv.scrollTo(0, consoleDiv.scrollHeight);
  }
}

function applyTimestamp(timestampButton) {
  timestampButton.setAttribute('data-before', addTimestamp ? 'Show' : 'Hide');
  if (addTimestamp) {
    timestampButton.classList.add("show");
  } else {
    timestampButton.classList.remove("show");
  }
  applyTimestampSetting();
}

function applyTimestampSetting() {
  var debugConsole = document.getElementById('debugConsole');
  if (addTimestamp) {
    debugConsole.classList.remove("hidelogtimestamps");
  } else {
    debugConsole.classList.add("hidelogtimestamps");
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
  console.error = (message) => { parseMessageAndLog(message, 'error', defaultConsoleError); errorReceived(); };


  parseMessageAndLog = (message, logLevel, consoleLogFunction) => {
    updateLogCounter(logLevel);
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

function updateLogCounter(logLevel) {
  logCounter[logLevel]++;

  counter = logCounterDivs[logLevel];
  const logLevels = counter.getAttribute('data-loglevels');
  const logLevelsArray = logLevels.split(',');
  let count = 0;
  logLevelsArray.forEach(logLevel => {
    count += logCounter[logLevel];
  });

  counter.querySelector(".counter").innerHTML = count;
}

function htmlLog(message, className) {
  var entry = document.createElement('div');
  entry.classList.add('entry', className);
  consoleDiv.appendChild(entry);

  var text = document.createElement('p');
  var time = new Date().toLocaleTimeString('en-US', {
    hour12: false,
    hour: "numeric",
    minute: "numeric",
    second: "numeric"
  }
  );
  message = "<span class='timestamplog'>[" + time + "] </span>" + message;
  message = message.replaceAll("\n", "<br />");
  text.innerHTML = message;
  entry.appendChild(text);

  var copyButton = document.createElement('button');
  copyButton.classList.add('copy-button', 'bubble-click-indicator');
  copyButton.title = 'copy to clipboard';
  entry.appendChild(copyButton);

  var copyIcon = document.createElement('div');
  copyIcon.classList.add('icon');
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


  if (scrollToBottom) {
    consoleDiv.scrollTo(0, consoleDiv.scrollHeight);
  }
}

function errorReceived() {
  if (debugToggle.checked === false) {
    let debugToggleMenu = document.getElementById('debugToggleMenu');
    debugToggleMenu.classList.remove('unseen-error');
    // trigger reflow
    debugToggleMenu.offsetWidth;
    debugToggleMenu.classList.add('unseen-error');
  }
}

function initializeToggleButton(startActive) {
  debugToggle = document.createElement('input');
  debugToggle.type = 'checkbox';
  debugToggle.id = 'debugToggle';
  debugToggle.name = 'debugToggle';
  debugToggle.checked = startActive;
  document.body.appendChild(debugToggle);

  var debugLabel = document.createElement('label');
  debugLabel.id = 'debugToggleMenu';
  debugLabel.htmlFor = 'debugToggle';
  document.body.appendChild(debugLabel);
  var iconDiv = document.createElement('div');
  iconDiv.classList = 'icon';
  debugLabel.appendChild(iconDiv);

  debugToggle.addEventListener('change', (event) => {
    if (event.currentTarget.checked) {
      applyScrollToBottomSetting();
      debugLabel.classList.remove('unseen-error');
    }

    if (typeof unityGame === 'undefined') {
      return;
    }
    if (event.currentTarget.checked) {
      unityGame.SendMessage("WebGL", "DisableCaptureAllKeyboardInput");
      return;
    }

    if (typeof unityCaptureAllKeyboardInputDefault !== 'undefined' && unityCaptureAllKeyboardInputDefault === 'false') {
      unityGame.SendMessage("WebGL", "DisableCaptureAllKeyboardInput");
    }
    else {
      unityGame.SendMessage("WebGL", "EnableCaptureAllKeyboardInput");
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
