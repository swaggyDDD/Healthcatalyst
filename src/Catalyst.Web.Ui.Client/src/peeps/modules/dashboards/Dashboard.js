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
