
var WebGlPlugins =
{
    _LogStartTime: function (str) {
        var startTimeText = Pointer_stringify(str);

        if(typeof unityLoadingFinished !== 'undefined') {
            unityLoadingFinished(startTimeText);
        }
        else {
            console.info('Unity real time since startup: ' + startTimeText);
        }
    },
    
    _GetTotalMemorySize: function()
    {
        return TOTAL_MEMORY;
    },

    _GetTotalStackSize: function()
    {
        return TOTAL_STACK;
    },

    _GetStaticMemorySize: function()
    {
        return STATICTOP - STATIC_BASE;
    },

    _GetDynamicMemorySize: function()
    {
        return HEAP32[DYNAMICTOP_PTR >> 2] - DYNAMIC_BASE;
    }
};

mergeInto(LibraryManager.library, WebGlPlugins);
