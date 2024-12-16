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
    }
};

mergeInto(LibraryManager.library, WebGlPlugins);
