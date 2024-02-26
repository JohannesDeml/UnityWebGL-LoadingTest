/// Processes all unity log messages and converts unity rich text to css styled console messages
/// from https://github.com/JohannesDeml/UnityWebGL-LoadingTest

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
  console.error = (message) => { handleLog(message, 'error', defaultConsoleError); };


  handleLog = (message, logLevel, consoleLogFunction) => {
    if (typeof message === 'string') {
      // Only parse messages that are actual strings
      parseMessageAndLog(message, logLevel, consoleLogFunction);
    }
    else {
      consoleLogFunction(message);
    }
  };

  parseMessageAndLog = (message, logLevel, consoleLogFunction) => {
    let styledTextParts = parseUnityRichText(message);
    let consoleText = '';
    let consoleStyle = [];
    styledTextParts.forEach(textPart => {
      consoleText += textPart.toConsoleTextString();
      consoleStyle.push(textPart.toConsoleStyleString());
    });
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

setupConsoleLogPipe();