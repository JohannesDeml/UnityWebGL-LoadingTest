
var WebGlPlugins =
{
    _LogStartTime: function (startTime, unityVersion) {
        var startTimeText = "";
        var unityVersionText = "";

        if(typeof UTF8ToString != 'undefined') {
            startTimeText = UTF8ToString(startTime);
            unityVersionText = UTF8ToString(unityVersion);
        }
        else if (typeof Pointer_stringify !== 'undefined') {
            startTimeText = Pointer_stringify(startTime);
            unityVersionText = Pointer_stringify(unityVersion);
        }
        
        if(typeof unityLoadingFinished !== 'undefined') {
            unityLoadingFinished(startTimeText, unityVersionText);
        }
        else {
            console.info("Unity " + unityVersionText + " real time since startup: " + startTimeText);
        }
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
