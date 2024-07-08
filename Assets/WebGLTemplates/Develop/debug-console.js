/// Processes all unity log messages and converts unity rich text to css styled console messages & adds a debug html debug console
/// from https://github.com/JohannesDeml/UnityWebGL-LoadingTest

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

function initializeDebugConsole() {
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
  consoleInput.value = 'runUnityCommand("Help");';
  debugConsole.appendChild(consoleInput);

  consoleInput.onkeydown = function (e) {
    if (['Enter', 'NumpadEnter'].includes(e.key)) {
      console.log(`Evaluating ${consoleInput.value}`);
      eval(consoleInput.value);
    }
  };

  setupConsoleLogPipe();
}

function runUnityCommand(...command) {
  unityGame.SendMessage("WebBridge", ...command);
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
  console.log = (message) => { handleLog(message, 'log', defaultConsoleLog); };
  console.info = (message) => { handleLog(message, 'info', defaultConsoleInfo); };
  console.debug = (message) => { handleLog(message, 'debug', defaultConsoleDebug); };
  console.warn = (message) => { handleLog(message, 'warn', defaultConsoleWarn); };
  console.error = (message) => { handleLog(message, 'error', defaultConsoleError); errorReceived(); };


  handleLog = (message, logLevel, consoleLogFunction) => {
    updateLogCounter(logLevel);
    if (typeof message === 'string') {
      // Only parse messages that are actual strings
      parseMessageAndLog(message, logLevel, consoleLogFunction);
    }
    else {
      consoleLogFunction(message);
      // Try to also log the object to the html console (does not always work in a meaningful manner)
      var htmlMessage;
      try {
        htmlMessage = JSON.stringify(message);
      } catch (error) {
        htmlMessage = `Can't convert message to JSON: ${error}`;
      }
      htmlLog(htmlMessage, logLevel);
    }
  };

  parseMessageAndLog = (message, logLevel, consoleLogFunction) => {
    let styledTextParts = parseUnityRichText(message);

    let consoleText = '';
    let consoleStyle = [];
    let htmlText = '';
    styledTextParts.forEach(textPart => {
      consoleText += textPart.toConsoleTextString();
      consoleStyle.push(textPart.toConsoleStyleString());
      htmlText += textPart.toHTML();
    });
    htmlLog(htmlText.trim(), logLevel);
    consoleLogFunction(consoleText, ...consoleStyle);
  };
}

class StyledTextPart {
  constructor(text, styles) {
    this.text = text;
    this.styles = styles;
  }

  toHTML() {
    if (this.styles.length > 0) {
      return `<span style="${this.styles.join(';')}">${this.text}</span>`;
    }
    return this.text;
  }

  toConsoleStyleString() {
    if (this.styles.length > 0) {
      return this.styles.join(';');
    }
    // Return some styling that does not change anything to support unstyled text
    return 'color:inherit';
  }

  toConsoleTextString() {
    return `%c${this.text}`;
  }
}

function parseUnityRichText(message) {
  // Mapping for unity tags to css style tags
  // Unity rich text tags, see https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/StyledText.html
  const tagMap = {
    'color': { startTag: '<color=', closeTag: '</color>', styleTag: 'color:', postfix: '', hasParameter: true },
    'size': { startTag: '<size=', closeTag: '</size>', styleTag: 'font-size:', postfix: 'px', hasParameter: true },
    'bold': { startTag: '<b', closeTag: '</b>', styleTag: 'font-weight:', styleValue: 'bold', hasParameter: false },
    'italic': { startTag: '<i', closeTag: '</i>', styleTag: 'font-style:', styleValue: 'italic', hasParameter: false }
  };

  let index = 0;
  const styledTextParts = [];

  // Stack of applied styles, so we can also nest styling
  let styleStack = [];
  // next start index for each tag
  let nextStartIndex = [];
  // next end index for each tag
  let nextEndIndex = [];
  Object.keys(tagMap).forEach(key => {
    nextStartIndex[key] = message.indexOf(tagMap[key].startTag, index);
    nextEndIndex[key] = message.indexOf(tagMap[key].closeTag, index);
  });

  while (index < message.length) {
    let nextTagFound = false;
    let nextKey;
    let fromArray = [];
    let nextIndex = message.length;

    // find the next tag start or end
    Object.keys(tagMap).forEach(key => {
      if (nextStartIndex[key] >= 0 && nextStartIndex[key] < nextIndex) {
        nextIndex = nextStartIndex[key];
        nextKey = key;
        fromArray = nextStartIndex;
        nextTagFound = true;
      }

      if (nextEndIndex[key] >= 0 && nextEndIndex[key] < nextIndex) {
        nextIndex = nextEndIndex[key];
        nextKey = key;
        fromArray = nextEndIndex;
        nextTagFound = true;
      }
    });

    // write the text in before the next tag to our style text part array
    if (nextIndex > index) {
      let messageToNextTag = message.substring(index, nextIndex);
      let styles = [...styleStack];
      styledTextParts.push(new StyledTextPart(messageToNextTag, styles));
    }

    // end if no more tags exist
    if (nextTagFound === false) {
      index = message.length;
      break;
    }

    let closeTagIndex = message.indexOf('>', nextIndex + 1);

    // Process start tag
    if (fromArray === nextStartIndex) {
      let styleValue;
      if (tagMap[nextKey].hasParameter) {
        styleValue = message.substring(nextIndex + tagMap[nextKey].startTag.length, closeTagIndex);
        styleValue += tagMap[nextKey].postfix;
      } else {
        styleValue = tagMap[nextKey].styleValue;
      }
      styleStack.push(`${tagMap[nextKey].styleTag}${styleValue}`);

      // Update list entry to next unprocessed start tag of type nextKey
      nextStartIndex[nextKey] = message.indexOf(tagMap[nextKey].startTag, nextIndex + 1);
    }
    // process end tag
    else if (fromArray === nextEndIndex) {
      styleStack.pop();

      // Update list entry to next unprocessed end tag of type nextKey
      nextEndIndex[nextKey] = message.indexOf(tagMap[nextKey].closeTag, nextIndex + 1);
    }

    // Update index to position after the tag closes
    index = closeTagIndex + 1;
  }
  return styledTextParts;
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
  });
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
      runUnityCommand("DisableCaptureAllKeyboardInput");
      return;
    }

    if (typeof unityCaptureAllKeyboardInputDefault !== 'undefined' && unityCaptureAllKeyboardInputDefault === 'false') {
      runUnityCommand("DisableCaptureAllKeyboardInput");
    }
    else {
      runUnityCommand("EnableCaptureAllKeyboardInput");
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
    if (typeof unityVersion != `undefined` && typeof applicationVersion != `undefined` && typeof webGlVersion != `undefined`) {
      // Set by WebGlBridge in Unity
      infoHeader.textContent = `Unity ${unityVersion}@${applicationVersion} (${webGlVersion})`;
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

function onFpsTrackingEvent(fps) {
  const fpsDiv = getOrCreateInfoEntry('fps');
  fpsDiv.textContent = `FPS: ${fps.toFixed(1)}`;
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

// Polyfill for older browsers
if (!String.prototype.replaceAll) {
  String.prototype.replaceAll = function(str, newStr) {
    if (str === '?')
      str = '\\\\?';
    return this.replace(str instanceof RegExp ? str : new RegExp(str, 'g'), newStr);
  }
}

const urlParams = new URLSearchParams(window.location.search);
const debugParam = urlParams.get('debug');
const debug = debugParam === 'true';

initializeToggleButton(debug);
initializeDebugConsole();