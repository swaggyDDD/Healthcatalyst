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
