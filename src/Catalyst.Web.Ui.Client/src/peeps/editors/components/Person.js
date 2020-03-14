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
