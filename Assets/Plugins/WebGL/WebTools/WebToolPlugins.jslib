var WebGlPlugins =
{
    _SetStringVariable: function(variableName, variableValue) {
        //convert the string from unity to javascript strings
        function toString(unityString) {
            if(typeof UTF8ToString !== 'undefined') {
                return UTF8ToString(unityString);
            }

            if (typeof Pointer_stringify !== 'undefined') {
                return Pointer_stringify(unityString);
            }

            return unityString;
        }

        const variableNameText = toString(variableName);
        const variableValueText = toString(variableValue);
        window[variableNameText] = variableValueText;
    },

    _AddTimeTrackingEvent: function(eventName) {
        //convert the string from unity to javascript strings
        function toString(unityString) {
            if(typeof UTF8ToString !== 'undefined') {
                return UTF8ToString(unityString);
            }

            if (typeof Pointer_stringify !== 'undefined') {
                return Pointer_stringify(unityString);
            }

            return unityString;
        }

        if(typeof unityTimeTrackers == 'undefined') {
            unityTimeTrackers = new Map();
        }

        const eventNameText = toString(eventName);
        const currentTime = performance.now();
        unityTimeTrackers.set(eventNameText, currentTime);

        if(typeof onAddTimeTracker !== 'undefined') {
            onAddTimeTracker(eventNameText);
        }

        var currentTimeRounded = currentTime.toFixed(2);
        console.log('Time tracker event ' + eventNameText + ': ' + currentTimeRounded + 'ms');
    },

    _AddFpsTrackingEvent: function(fps) {
        if(typeof onFpsTrackingEvent !== 'undefined') {
            onFpsTrackingEvent(fps);
        }
    },

    _ShowInfoPanel: function () {
        if(typeof setInfoPanelVisible !== 'undefined') {
            setInfoPanelVisible(true);
        }
    },

    _HideInfoPanel: function () {
        if(typeof setInfoPanelVisible !== 'undefined') {
            setInfoPanelVisible(false);
        }
    },

    _GetUserAgent: function () {
        var userAgent = navigator.userAgent;

        //Get size of the string
        var bufferSize = lengthBytesUTF8(userAgent) + 1;
        //Allocate memory space
        var buffer = _malloc(bufferSize);

        //Copy old data to the new one then return it
        if(typeof stringToUTF8 !== 'undefined') {
            stringToUTF8(userAgent, buffer, bufferSize);
        }
        else if(typeof writeStringToMemory !== 'undefined') {
            writeStringToMemory(userAgent, buffer);
        }

        return buffer;
    },

    _GetTotalMemorySize: function()
    {
        if(typeof HEAP8 !== 'undefined') {
            return HEAP8.length;
        }
        if(typeof TOTAL_MEMORY !== 'undefined') {  // Legacy support
            return TOTAL_MEMORY;
        }

        console.warn("Problem with retrieving total memory size");
        return -1;
    },

    _CopyToClipboard: function(text) {
        var str = UTF8ToString(text);
        navigator.clipboard.writeText(str)
            .then(function() {
            })
            .catch(function(err) {
                console.error('Failed to copy text: ', err);
            });
    },

    _IsOnline: function() {
        return navigator.onLine ? 1 : 0;
    },

    _DownloadFile: function(filename, content) {
        var filenameStr = UTF8ToString(filename);
        var contentStr = UTF8ToString(content);

        var element = document.createElement('a');
        element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(contentStr));
        element.setAttribute('download', filenameStr);
        element.style.display = 'none';
        document.body.appendChild(element);
        element.click();
        document.body.removeChild(element);
    },

    _DownloadBlob: function(filename, byteArray, byteLength, mimeType) {
        var filenameStr = UTF8ToString(filename);
        var mimeTypeStr = UTF8ToString(mimeType);

        var data = new Uint8Array(byteLength);
        for (var i = 0; i < byteLength; i++) {
            data[i] = HEAPU8[byteArray + i];
        }

        var blob = new Blob([data], { type: mimeTypeStr });
        var url = URL.createObjectURL(blob);

        var element = document.createElement('a');
        element.setAttribute('href', url);
        element.setAttribute('download', filenameStr);
        element.style.display = 'none';
        document.body.appendChild(element);
        element.click();
        document.body.removeChild(element);
        URL.revokeObjectURL(url);
    }
};

mergeInto(LibraryManager.library, WebGlPlugins);
