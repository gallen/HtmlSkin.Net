const HtmlSkinHub = {
    call: function(name){
        switch(arguments.length){
            case 0: return;
            case 1: return window.external[name]();
            case 2: return window.external[name](arguments[1]);
            case 3: return window.external[name](arguments[1], arguments[2]);
            case 4: return window.external[name](arguments[1], arguments[2], arguments[3]);
            case 5: return window.external[name](arguments[1], arguments[2], arguments[3], arguments[4]);
            case 6: return window.external[name](arguments[1], arguments[2], arguments[3], arguments[4], arguments[5]);
            case 7: return window.external[name](arguments[1], arguments[2], arguments[3], arguments[4], arguments[5], arguments[6]);
            case 8: return window.external[name](arguments[1], arguments[2], arguments[3], arguments[4], arguments[5], arguments[6], arguments[7]);
            case 9: return window.external[name](arguments[1], arguments[2], arguments[3], arguments[4], arguments[5], arguments[6], arguments[7], arguments[8]);
            case 10: return window.external[name](arguments[1], arguments[2], arguments[3], arguments[4], arguments[5], arguments[6], arguments[7], arguments[8], arguments[9]);            
            default: return;
        }
    },
    methods: function(methods)
    {
        for(var prop in methods) {
            if(methods.hasOwnProperty(prop)){
                let rawName = "##hostcall##"+prop;
                window[rawName] = methods[prop]
            }
        }
    }
};

export default HtmlSkinHub;

