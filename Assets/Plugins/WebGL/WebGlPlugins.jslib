
var WebGlPlugins =
{
    _LogStartTime: function (str) {
        var startTimeText = "Missing function";

        if(typeof UTF8ToString != 'undefined') {
            startTimeText = UTF8ToString(str);
        }
        else if (typeof Pointer_stringify !== 'undefined') {
            startTimeText = Pointer_stringify(str);
        }
        
        if(typeof unityLoadingFinished !== 'undefined') {
            unityLoadingFinished(startTimeText);
        }
        else {
            console.info('Unity real time since startup: ' + startTimeText);
        }
    },
    
    _GetTotalMemorySize: function()
    {
        if(typeof TOTAL_MEMORY !== 'undefined') {
            return TOTAL_MEMORY;
        }

        console.warn('Problem with retrieving unity value. TOTAL_MEMORY: ' + typeof TOTAL_MEMORY);
        return -1;
    },

    _GetTotalStackSize: function()
    {
        if(typeof TOTAL_STACK !== 'undefined') {
            return TOTAL_STACK;
        }

        console.warn('Problem with retrieving unity value. TOTAL_STACK: ' + typeof TOTAL_STACK);
        return -1;
    },

    _GetStaticMemorySize: function()
    {
        if(typeof STATICTOP !== 'undefined' && typeof STATIC_BASE !== 'undefined') {
            return STATICTOP - STATIC_BASE;
        }

        console.warn('Problem with retrieving unity value. STATICTOP: ' + typeof STATICTOP + ', STATIC_BASE: ' + typeof STATIC_BASE);
        return -1;
    },

    _GetDynamicMemorySize: function()
    {
        if(typeof HEAP32 !== 'undefined' && typeof DYNAMICTOP_PTR !== 'undefined' && typeof DYNAMIC_BASE !== 'undefined') {
            return HEAP32[DYNAMICTOP_PTR >> 2] - DYNAMIC_BASE;
        }

        console.warn('Problem with retrieving unity value. HEAP32: ' + HEAP32 + ', DYNAMICTOP_PTR: ' + DYNAMICTOP_PTR  + ', DYNAMIC_BASE: ' + DYNAMIC_BASE);
        return -1;
    }
};

mergeInto(LibraryManager.library, WebGlPlugins);
