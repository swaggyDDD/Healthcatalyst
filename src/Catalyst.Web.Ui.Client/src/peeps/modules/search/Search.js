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
