/**
 * Created by rusty on 3/24/2017.
 * scaffold based on https://github.com/rustyswayne/Merchello/blob/merchello-dev/src/Merchello.Mui.Client/src/jquery/mui/MUI.js
 *
 * REQUIRES:    jquery.js
 *              underscore.js
 *              typeahead.js
 */
var Peeps = (function() {

    var DEBUG_MODE = {
        events: false,
        console: false
    };

    // Private members
    var eventHandlers = [];

    // Initialization
    function init() {
        $(document).ready(function() {

            // intialize the search
            Peeps.Search.init();

            // bind the forms
            Peeps.Forms.init();

            // initialize the dashboards
            Peeps.Dashboards.init();

            // bind the people records
            Peeps.Editors.init();

        });
    }

    // indicates whether or not a JS logger is configured
    function hasLogger() {
        return false;
    }

    //// registers an event
    function registerEvent(evt, callback) {
        try
        {
            var existing = _.find(eventHandlers, function(ev) { return ev.event === evt; });
            if (existing !== undefined && existing !== null) {
                if (callback !== undefined && typeof callback === "function") {
                    existing.callbacks.push(callback);
                }
            } else {
                var callbacks = [];
                if (callback !== undefined && typeof callback === "function") {
                    callbacks.push(callback);
                }
                eventHandlers.push({ event: evt, callbacks: callbacks });
            }
        }
        catch (err) {
            console.info(err);
        }
    }

    /// emit the event
    function trigger(name, args) {
        var existing = _.find(eventHandlers, function(ev) { return ev.event === name; });
        if (existing === undefined || existing === null) return;

        // execute each of the registered callbacks
        _.each(existing.callbacks, function(cb) {
            try {
                cb(name, args);
            }
            catch(err) {
                console.info(err);
            }
        });
    }

    // create a generic cache of functions, where fn is the function to retrieve and execute for a value.
    // also ensures, the function is executed once and a single value is returned.
    function createCache(fn) {
        var cache = {};
        return function( key, callback ) {
            if ( !cache[ key ] ) {
                cache[ key ] = $.Deferred(function( defer ) {
                    fn( defer, key );
                }).promise();
            }
            return cache[ key ].done( callback );
        };
    }

    // utility method to map the event name from an alias so they can be
    // more easily referenced in other modules
    function getEventNameByAlias(events, alias) {
        var evt = _.find(events, function(e) { return e.alias === alias});
        return evt === undefined ? '' : evt.name;
    }

    // writes events to debug console
    function debugConsoleEvents(events) {
        if (DEBUG_MODE.events) {
            _.each(events, function(ev) {
                MUI.on(ev.name, function(name, args) {
                    console.log(ev);
                    console.log(args === undefined ? 'No args' : args);
                });
            });
        }
    }

    // write to debug console if in debug mode
    function debugConsole(obj) {
        if (DEBUG_MODE.console) {
            console.log(obj);
        }
    }

    //
    function willWork(selector) {
        return $(selector).length > 0;
    }

    // exposed members
    return {
        // ensures the settings object is created
        Settings: { },
        Editors: { },
        // ensures the services object is created
        init: init,
        willWork: willWork,
        hasLogger: hasLogger,
        createCache: createCache,
        on: registerEvent,
        emit: trigger,
        getEventNameByAlias: getEventNameByAlias,
        debugConsoleEvents : debugConsoleEvents,
        debugConsole: debugConsole
    }

})();
