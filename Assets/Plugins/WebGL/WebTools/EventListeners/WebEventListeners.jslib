var WebGlEventListeners =
{
    _AddJsEventListener: function(eventNamePtr) {
        var eventName = UTF8ToString(eventNamePtr);

        // Add the event listener to the window object
        window.addEventListener(eventName, function(event) {
            if (typeof unityGame === 'undefined') {
                console.error('Unity instance "unityGame" not found - cannot send event ' + eventName);
                return;
            }
            unityGame.SendMessage("WebEvent-" + eventName, "TriggerEvent");
        });
    },
};

mergeInto(LibraryManager.library, WebGlEventListeners);
