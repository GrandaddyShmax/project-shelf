mergeInto(LibraryManager.library, {
    ShowMobileKeyboard: function() {
        if (!document.getElementById('unity-mobile-input')) {
            var input = document.createElement('input');
            input.id = 'unity-mobile-input';
            input.style.position = 'fixed';
            input.style.bottom = '0';
            input.style.left = '0';
            input.style.opacity = '0';
            input.style.width = '1px';
            input.style.height = '1px';
            document.body.appendChild(input);
            
            input.addEventListener('input', function(e) {
                var gameObject = "Keyboard Manager";
                var methodName = "OnMobileInput";
                var value = input.value;
                unityInstance.SendMessage(gameObject, methodName, value);
            });
        }
        
        var input = document.getElementById('unity-mobile-input');
        input.focus();
    },
    
    HideMobileKeyboard: function() {
        var input = document.getElementById('unity-mobile-input');
        if (input) {
            input.blur();
        }
    }
});