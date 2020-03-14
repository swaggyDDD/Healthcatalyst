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