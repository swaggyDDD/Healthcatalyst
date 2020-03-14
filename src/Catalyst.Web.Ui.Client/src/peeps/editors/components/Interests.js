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
