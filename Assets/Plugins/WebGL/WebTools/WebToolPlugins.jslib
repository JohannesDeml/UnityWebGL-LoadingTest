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
        console.log('Time tracker event ' +eventNameText +': ' + currentTimeRounded + 'ms');
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
        if(typeof TOTAL_MEMORY !== 'undefined') {
            return TOTAL_MEMORY;
        }

        console.warn("Problem with retrieving unity value. TOTAL_MEMORY: " + typeof TOTAL_MEMORY);
        return -1;
    },

    _GetTotalStackSize: function()
    {
        if(typeof TOTAL_STACK !== 'undefined') {
            return TOTAL_STACK;
        }

        console.warn("Problem with retrieving unity value. TOTAL_STACK: " + typeof TOTAL_STACK);
        return -1;
    },

    _GetStaticMemorySize: function()
    {
        if(typeof STATICTOP !== 'undefined' && typeof STATIC_BASE !== 'undefined') {
            return STATICTOP - STATIC_BASE;
        }

        console.warn("Problem with retrieving unity value. STATICTOP: " + typeof STATICTOP + ", STATIC_BASE: " + typeof STATIC_BASE);
        return -1;
    },

    _GetDynamicMemorySize: function()
    {
        if(typeof HEAP32 !== 'undefined' && typeof DYNAMICTOP_PTR !== 'undefined' && typeof DYNAMIC_BASE !== 'undefined') {
            return HEAP32[DYNAMICTOP_PTR >> 2] - DYNAMIC_BASE;
        }

        console.warn("Problem with retrieving unity value. HEAP32: " + typeof HEAP32 + ", DYNAMICTOP_PTR: " + typeof DYNAMICTOP_PTR + ", DYNAMIC_BASE: " + typeof DYNAMIC_BASE);
        return -1;
    }
};

mergeInto(LibraryManager.library, WebGlPlugins);
