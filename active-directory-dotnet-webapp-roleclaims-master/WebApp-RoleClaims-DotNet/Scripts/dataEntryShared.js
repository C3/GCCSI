$(document).ready(function () {
    $.ajaxSetup({ cache: false });

    $("#NodesToMove").detach().appendTo('#DestinationContainerNode')

    $(".dataTables_filter").css("display", "none");

    $('body').on('click', '.createDialogButton', function (e) {
        e.preventDefault();
        var url = $(this).attr('href');
        $("#dialog-create").dialog({
            title: 'Add Record',
            autoOpen: false,
            width: '100%', // overcomes width:'auto' and maxWidth bug
            height: 'auto',
            modal: true,
            fluid: true, //new option
            resizable: false,
            open: function (event, ui) {
                $(this).load(url);
            },
            close: function (event, ui) {
                $(this).dialog("destroy");
            }
        });
        $("#dialog-create").dialog('open');
        return false;
    });

    $('body').on('click', '.editDialog', function (e) {
        var url = $(this).attr('href');
        $("#dialog-edit").dialog({
            title: 'Edit Record',
            autoOpen: false,
            width: '100%', // overcomes width:'auto' and maxWidth bug
            height: 'auto',
            modal: true,
            fluid: true, //new option
            resizable: false,
            open: function (event, ui) {
                $(this).load(url);
            },
            close: function (event, ui) {
                $(this).dialog("destroy");
            }
        });
        $("#dialog-edit").dialog('open');
        return false;
    });

    $('body').on('click', '.detailsDialog', function (e) {
        var url = $(this).attr('href');
        $("#dialog-view").dialog({
            title: 'View Record',
            autoOpen: false,
            width: '100%', // overcomes width:'auto' and maxWidth bug
            height: 'auto',
            modal: true,
            fluid: true, //new option
            resizable: false,
            open: function (event, ui) {
                $(this).load(url);
            },
            close: function (event, ui) {
                $(this).dialog("destroy");
            }
        });
        $("#dialog-view").dialog('open');
        return false;
    });

    $('body').on('click', '.confirmDialog', function (e) {
        var url = $(this).attr('href');
        $("#dialog-confirm").dialog({
            title: 'Delete Record',
            autoOpen: false,
            height: 'auto',
            modal: true,
            fluid: true, //new option
            resizable: false,
            buttons: {
                "OK": function () {
                    $(this).dialog("close");
                    window.location = url;
                },
                "Cancel": function () {
                    $(this).dialog("close");
                }
            }
        });
        $("#dialog-confirm").dialog('open');
        return false;
    });


    $('body').on('click', '#btncancelEdit', function (e) {
        $("#dialog-edit").dialog('close');
    });

    $('body').on('click', '#btncancelView', function (e) {
        $("#dialog-view").dialog('close');
    }); 

    $('body').on('click', '#btncancelCreate', function (e) {
        $("#dialog-create").dialog('close');
    });

});

// on window resize run function
$(window).resize(function () {
    fluidDialog();
});

// catch dialog if opened within a viewport smaller than the dialog width
$(document).on("dialogopen", ".ui-dialog", function (event, ui) {
    fluidDialog();
});

function fluidDialog() {
    var $visible = $(".ui-dialog:visible");
    // each open dialog
    $visible.each(function () {
        var $this = $(this);
        var dialog = $this.find(".ui-dialog-content").data("dialog");
        // if fluid option == true
        if (dialog.options.fluid) {
            var wWidth = $(window).width();
            // check window width against dialog width
            if (wWidth < dialog.options.maxWidth + 50) {
                // keep dialog from filling entire screen
                $this.css("max-width", "90%");
            } else {
                // fix maxWidth bug
                $this.css("max-width", dialog.options.maxWidth);
            }
            //dialog.option("position", dialog.options.position);
            dialog.option("position", ['50%', '50%']);
        }
    });

}