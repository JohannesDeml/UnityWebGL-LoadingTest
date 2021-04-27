mergeInto(LibraryManager.library, {
    LogStartTime: function (str) {
        var startTimeText = Pointer_stringify(str);

        if(unityLoadingFinished !== 'undefined') {
            unityLoadingFinished(startTimeText);
        }
        else {
            console.info('Unity real time since startup: ' + startTimeText);
        }
    }
});