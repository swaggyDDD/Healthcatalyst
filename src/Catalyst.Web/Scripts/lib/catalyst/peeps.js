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

/**
 * Created by rusty on 3/24/2017.
 */
// Settings for Peeps
Peeps.Settings = {

    localCacheDuration: 10, // 10 minutes

    // path to the spinner svg file
    spinnerSvg: '/Media/Placeholders/balls.svg',

    // flag for demoing api delays
    enableApiDelays: true,

    searchApiEndpoint: '/api/searchapi/getall/',

    newPerson: '/people/newperson/',

    apiRoutes: [
     // { id: "route alias", value: "use this for the $.ajax url", title: "message to replace 'Intializing...'", notes: "notes replacement",  delay: NOT REQUIRE (FOR DEMO) }
        { id: 'countrymetrics', value: '/dashboard/countriessnapshot', reqId: false, title: "Querying Country Metrics...", notes: 'Country filter queries not implemented.', delay: 750 },
        { id: 'peopleprops', value: '/dashboard/peoplepropertystats', reqId: false, title: "Evaluating Profiles...", notes: 'Property filter queries not implemented.', delay: 1250 },
        { id: 'randomwatched', value: '/dashboard/randomwatched', reqId: false, title: "Selecting random...", notes: 'Randomly selected from watched', delay: 0 },

        // editors
        { id: 'addperson', value: '/editors/personeditor/editor', reqId: false, title: 'Loading form...', notes: '', delay: 0 },
        { id: 'updateperson', value: '/editors/personeditor/editor', reqId: true, title: 'Loading form...', notes: '', delay: 0 },
        { id: 'interestlist', value: '/editors/interesteditor/editor', reqId: true, title: 'Loading form...', notes: '', delay: 0 },
        { id: 'sociallinks', value: '/editors/sociallinkseditor/editor', reqId: true, title: 'Loading form...', notes: '', delay: 0 },
        { id: 'photo', value: '/editors/photoeditor/editor', reqId: true, title: 'Loading form...', notes: '', delay: 0 },
        { id: 'address', value: '/editors/addresseditor/editor', reqId: true, title: 'Loading form...', notes: '', delay: 0 }
    ]

}
Peeps.Dialogs = {

    confirmDelete: function(confirm, cancel, itemName) {
        itemName = itemName === undefined ? 'person' : itemName;
        Peeps.Dialogs.confirm({
            title: 'Delete this ' + itemName + '?',
            message: 'This <strong>' + itemName + '</strong> will be permanently deleted and cannot be recovered. Are you sure?',
            confirm: confirm,
            cancel: cancel
        });
    },

    confirm: function(args) {

        // args { title: '', message: '', confirm: callback, cancel: callback}

        var template = $('<div id="dialog-confirm" title="' + args.title + '">' +
            '<p><span class="ui-icon ui-icon-alert" style="float:left; margin:12px 12px 20px 0;"></span>' + args.message + '</p>' +
            '</div>')

        $('#peeps').html(template);

        $('#dialog-confirm').dialog({
            resizable: false,
            height: "auto",
            width: 400,
            modal: true,
            buttons: {
                "Confirm": function () {
                    if (args.confirm !== undefined) args.confirm();
                    $(this).dialog("close");
                },
                Cancel: function () {
                    if (args.cancel !== undefined) args.cancel();
                    $(this).dialog("close");
                }
            }
        });
    },

    popForm: function(args) {

        // args { frm: formElement, save: callback, cancel: callback  }

        var template = '<div id="dialog-form" title="Create new user">' + args.frm + '</div>';

        $('#peeps').html(template);

        var dialog = $('#dialog-form').dialog({
            autoOpen: false,
            height: 'auto',
            width: 600,
            modal: true
        });

        return dialog;
    }

}

/**
 * Created by rusty on 3/24/2017.
 */
// Dashboards
// REQUIRES:  underscore.js
Peeps.Dashboards = {

    loadedEvtName: 'dashboardLoaded',

    // initializes the component
    init: function() {

        var dashboards = $('div[data-dashboard]');
        if (dashboards.length) {
            _.each(dashboards, function(d) {
                // invoke the binder
                Peeps.Dashboards.binder.invoke(d);
            });
        }
    },

    // method to bind individual dashboard item
    binder: {

        // invokes the initialization of the dashboard component
        invoke: function(dashboard) {

            // if the alias from the data value is 'donothing' skip making the ajax call (but log).
            const skipflag = 'donothing';

            var routeAlias = $(dashboard).data('dashboard');

            // Append the ajax spinner to the dashboard item content
            // todo remove this entire "id" concept.  turns out not need
            var id = Peeps.Dashboards.spinner.makeId();
            Peeps.Dashboards.spinner.appendSpinner(dashboard, id);

            var dashParams = {
                dashboard: dashboard,
                args: {},
                skip: true
            };

            // Lookup what controller action needs to be called
            // IF skipflag - leave spinner spinning
            if (routeAlias === skipflag) {
                return; // bail
            }

            // find the route record (hard code in Peeps.Settings)
            var route = _.find(Peeps.Settings.apiRoutes, function (r) {
                if (r.id === routeAlias) {
                    return r;
                }
            });

            // update the dash params
            if (route !== undefined) {
                dashParams.args = route;
                dashParams.skip = false;
                Peeps.Dashboards.binder.query(dashParams);
            }
        },

        // query the controller for the partial view content
        query: function(params) {
            if (params === undefined) throw new Error('Routing params have not been defined.');

            var panel = params.dashboard;
            $(panel).find('.chart-title').text(params.args.title);

            // adding a note here for the demo
            // the load is pretty quick and you can't really see the ajax loader if running locally
            // update the note
            var delay = params.args.delay === undefined ? 0 : params.args.delay;
            if (!Peeps.Settings.enableApiDelays) delay = 0;

            if (delay > 0) {
                $(panel).find('.chart-notes').text('FAKE DELAY of (' + params.args.delay + ' ms) added for demo!');
            }

            $(panel).find('');
            // replace the dashboard
            $.ajax({
                url: params.args.value,
                dataType: "html"
            }).done(function(data) {

                setTimeout(function() {
                    $(panel).parent().html(data);
                    $(panel).find('.chart-notes').text('Note:');
                    Peeps.emit(Peeps.Dashboards.loadedEvtName, { panel: panel, params: params.args });
                    }, delay);

            }).fail(function(jqXHR, textStatus, errorThrown) {
                console.info({ jqXHR: jqXHR, status: textStatus, error: errorThrown });
            })
            .always(function() {
                Peeps.debugConsole({ msg: 'Finished dashboard $.ajax', params: params });
            });
        }

    },

    spinner: {

        activeIds: [],

        appendSpinner: function(dashboard, id) {

            if (dashboard === undefined) {
                // sort of a hack here for the spinner
                // just gonig to replace the existing (markdown) content with the spinner.
                // this will not affect the title / note .. but out of time
                dashboard = Peeps.Editors.Person.editorPanel.find('.chart-wrapper');
            }

            Peeps.Dashboards.spinner.activeIds.push(id);
            var spinner = Peeps.Dashboards.spinner.build(id);
            $(dashboard).find(".chart-stage").html(spinner);

        },

        build: function(id) {

            return $('<div class="dashboard-spinner"><img src="' + Peeps.Settings.spinnerSvg + '" id="' + id + '" alt="Loading..." /></div>');
        },

        makeId: function() {
            var text = "",
                possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            for ( var i = 0; i < 10; i++ )
                text += possible.charAt(Math.floor(Math.random() * possible.length));

            return text;
        }

    }

};

/**
 * Created by rusty on 3/25/2017.
 */
Peeps.Search = {

    // map for people names
    peopleMap: {},

    // the names
    names: [],

    current: {},

    // initializes the search
    init: function() {
        if (Peeps.willWork('#person-search')) {
            $.get(Peeps.Settings.searchApiEndpoint).done(function(results) {

                // build the map
                _.each(results, function(r) {
                   Peeps.Search.peopleMap[r.name] = r;
                   Peeps.Search.names.push(r.name);
                });

                Peeps.Search.bind.searchBox($('#person-search'));
            });
        }
    },

    bind: {
        searchBox: function(el) {

            var people = new Bloodhound({
                datumTokenizer: Bloodhound.tokenizers.whitespace,
                queryTokenizer: Bloodhound.tokenizers.whitespace,
                local: Peeps.Search.names
            });

            $(el)
            .bind('keydown', function(e) {
                var regex = new RegExp("^[a-zA-Z0-9]");
                var code = !e.charCode ? e.which : e.charCode;
                var key = String.fromCharCode(code);
                if (!regex.test(key) && code !== 32 && code !== 8) {
                    e.preventDefault();
                    return false;
                }
            }).typeahead({
                hint: true,
                highlight: true, /* Enable substring highlighting */
                minLength: 1 /* Specify minimum characters required for showing result */
            },
            {
                name: 'names',
                source: people
            })
            .bind('typeahead:select', function(ev, suggestion) {
                console.log('Selection: ' + suggestion);
                Peeps.Search.redirect(suggestion);
            })
            .bind("typeahead:autocompleted", function(ev, suggestion) {
                console.log('Auto: ' + suggestion);
                Peeps.Search.redirect(suggestion);
            });
        }
    },

    redirect: function(suggestion) {
        var record = Peeps.Search.peopleMap[suggestion];
        window.location = record.url;
    }
}

Peeps.Forms = {

    fileSelectEvtName: 'fileselect',

    init: function() {
        Peeps.Forms.bind.inputTypeFile();
    },

    rebind: function(frm) {
        $.validator.unobtrusive.parse(frm);
    },

    bind: {

        //// see also: https://www.abeautifulsite.net/whipping-file-inputs-into-shape-with-bootstrap-3
        inputTypeFile: function() {

            $(document).on('change', ':file', function() {
            var input = $(this),
                numFiles = input.get(0).files ? input.get(0).files.length : 1,
                label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
                input.trigger('fileselect', [ numFiles, label, input.attr('id') ]);
                Peeps.emit(Peeps.Forms.fileSelectEvtName, { id: input.attr('id'), fileName: label })
            });

            $(':file').on('fileselect', function(event, numFiles, label, id) {
                Peeps.debugConsole(numFiles);
                Peeps.debugConsole(label);
                Peeps.debugConsole(id);
            });
        }
    }

};

/**
 * Created by rusty on 3/26/2017.
 */
Peeps.Editors = {

    init: function() {

        Peeps.Editors.Person.init();
    }

}
Peeps.Editors.Interests = {

    route: {},

    saveEndpoint: '/editors/interesteditor/save/',

    init: function() {

        // get the route;
        Peeps.Editors.Interests.route = _.find(Peeps.Settings.apiRoutes, function (r) {
            if (r.id === 'interestlist') {
                return r;
            }
        });

        Peeps.Editors.Interests.bind.form();

        // bind delete buttons
        Peeps.Editors.Interests.bind.deletes();
    },

    bind: {

        form: function() {

            var frm = $('#interests-editor');
            var route = Peeps.Editors.Interests.route;

            $(frm).bind('submit', function(e) {
                e.preventDefault();

                Peeps.Dashboards.spinner.appendSpinner();

                $.ajax({

                    url: Peeps.Editors.Interests.saveEndpoint,
                    type: 'POST',
                    data: $(frm).serialize()

                }).done(function(data) {
                    Peeps.Editors.Person.bind.editorPanel(route, data);
                });
            });

        },

        deletes: function() {

            var route = Peeps.Editors.Interests.route;

            function execute(targetUrl) {
                $.get(targetUrl).done(function(data) {
                    Peeps.Editors.Person.bind.editorPanel(route, data);
                });
            }
            _.each($('.interest-trash'), function(el) {

                $(el).bind('click', function(e) {
                    e.preventDefault();

                    var targetUrl = $(this).attr("href");
                    Peeps.Dialogs.confirmDelete(
                        function () {
                            Peeps.Dashboards.spinner.appendSpinner();
                            execute(targetUrl);
                            },
                        undefined,
                        'interest');

                }); // .bind
            }); // _.each

        }
    }
}

Peeps.Editors.Person = {

    personId: '',

    editorPanel: null,

    init: function() {

        // For async form loads we have to wait for the operation to complete. In
        // this case we can't always rely on a promise, since the initiation may occur from another module
        // or directly from a view (via a dashboard placeholder).
        Peeps.on(Peeps.Dashboards.loadedEvtName, Peeps.Editors.Person.onDashboardLoaded);

        Peeps.on(Peeps.Forms.fileSelectEvtName, function(elId, fileName) {
            if (elId === 'fileselect') {
              // this is the photo upload editor
                $('.photo-label-box').val(fileName.fileName);
            };
        });

        // Bind deletes on the listing pages
        Peeps.Editors.Person.bind.deletes();

        if (Peeps.willWork('#person-details')) {
            var pd = $('#person-details');
            Peeps.Editors.Person.personId = $(pd).data('person');
            // console.info(Peeps.Editors.Person.personId);
        }
        if (Peeps.willWork('#editor-panel')) {
            Peeps.Editors.Person.editorPanel = $('#editor-panel');
        }

        // bind the property editor links
        Peeps.Editors.Person.bind.editorLinks();

    },

    onDashboardLoaded: function(s, e) {

        switch ( e.params.id ) {
            case 'addperson':
            case 'updateperson':
                Peeps.Editors.Person.bind.personEntry(e);
                break;
            case 'interestlist':
                Peeps.Editors.Interests.init();
                break;
            default:
                break;
        };

    },

    bind: {

        deletes: function() {
            if (Peeps.willWork('.delete-person')) {
            _.each($('.delete-person'), function(el) {

                $(el).bind('click', function(e) {
                    e.preventDefault();
                    var targetUrl = $(this).attr("href");
                    Peeps.Dialogs.confirmDelete(function() {

                       window.location = targetUrl;
                    });
                });

            });
            }
        },

        editorPanel: function(route, data) {

            Peeps.Editors.Person.editorPanel.html(data);

            // they all have forms
            // rebind
            $(Peeps.Editors.Person.editorPanel).find('.btn-cancel').bind('click', function(e) {
                e.preventDefault();
                // bit hacky here
                window.location.reload();
            });

            var frm = $(Peeps.Editors.Person.editorPanel).find('form');
            Peeps.Forms.rebind(frm);

            var panel = $(Peeps.Editors.Person.editorPanel).find('.chart-wrapper');
            Peeps.emit(Peeps.Dashboards.loadedEvtName, { panel: panel, params: route })

        },

        editorLinks: function() {
            if (!Peeps.willWork($('[data-editor]'))) return;

            _.each($('[data-editor]'), function(link) {

                var routeAlias = $(link).data('editor');
                $(link).bind('click', function(e) {
                    e.preventDefault();

                    // sort of a hack here for the spinner
                    // just gonig to replace the existing (markdown) content with the spinner.
                    // this will not affect the title / note .. but out of time
                    var dash = Peeps.Editors.Person.editorPanel.find('.chart-wrapper');

                    Peeps.Dashboards.spinner.appendSpinner();

                    // get the route;
                    var route = _.find(Peeps.Settings.apiRoutes, function (r) {
                        if (r.id === routeAlias) {
                            return r;
                        }
                    });

                    $.ajax({
                        url: route.value,
                        dataType: 'html',
                        data: { id: Peeps.Editors.Person.personId },
                    }).done(function(data) {
                        Peeps.Editors.Person.bind.editorPanel(route, data);

                    });
                });

            });
        },

        personEntry: function(args) {

            if (!Peeps.willWork('#person-entry')) return;

            var frm = $('#person-entry');

            // rebind the unobtrusive validation
            Peeps.Forms.rebind(frm);

            Peeps.Editors.Person.bind.birthday(frm);
        },

        birthday: function(frm) {
            $(frm).find('#Birthday').datepicker({
                changeYear: true,
                yearRange: "1930:2017"
            });
        }
    }
}

//// Bootstrap Peeps!
Peeps.init();
