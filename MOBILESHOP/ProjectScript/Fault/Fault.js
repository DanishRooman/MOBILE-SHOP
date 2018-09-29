var Fault = function () {
    var handleCreateFault = function () {
        //Ajax call
        $.ajax({
            url: '/Fault/AddFault',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#CreateFault").empty();
                $("#CreateFault").html(result);
                $("#FaultTitle").text("Add Mobile Fault");
                $("#CreateFault").modal("show");
            },
            error: function () {
                console.log("Error");
            }
        });

    };
    var handleSuccessFault = function (result) {

        if (result.key) {

            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
            handleFaultList();
            handleEmptyFault();
        }
        else {
            $.toast({
                heading: 'Error',
                text: result.value,
                showHideTransition: 'fade',
                icon: 'error'
            });
        }

    };
    var handleFaultList = function () {
        $.ajax({
            url: '/Fault/FaultListing',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#divFault").empty();
                $("#divFault").html(result);
            },
            error: function () {
                console.log("Error");
            }
        });
    };
    var handleEmptyFault = function () {
        $('.form-control').val("");
        $("#CreateFault").modal("hide");
    };
    var handleDeleteFault = function (id) {
        $.confirm({
            title: 'Delete Fault',
            content: 'Are you sure you want to delete this Fault?',
            theme: 'material',
            buttons: {
                confirm: {
                    btnClass: "btn-blue",
                    keys: ["enter"],
                    action: function () {
                        $.ajax({
                            url: '/Fault/DeleteFault',
                            type: 'GET',
                            dataType: 'json',
                            data: { "id": id },
                            success: function (result) {
                                if (result.key) {
                                    $.toast({
                                        heading: 'Success',
                                        text: result.value,
                                        showHideTransition: 'slide',
                                        icon: 'success'
                                    });
                                    handleFaultList();
                                }
                                else {
                                    $.toast({
                                        heading: 'Error',
                                        text: result.value,
                                        showHideTransition: 'fade',
                                        icon: 'error'
                                    });
                                }
                            },
                            error: function () {
                                console.log("Error");
                            }
                        });
                    }
                },
                cancel: function () {

                },
            }
        });

    };
    var handleUpdateFault = function (id) {
       
        $.ajax({
            url: '/Fault/UpdateFault',
            type: 'GET',
            dataType: 'HTML',
            data: { "id": id },
            success: function (result) {
                $("#CreateFault").empty();
                $("#CreateFault").html(result);
                $("#FaultTitle").text("Update Mobile Fault");
                $("#CreateFault").modal("show");
            },
            error: function () {
                console.log("Error");
            }
        });

    };
    return {
        initCreateFault: function () {
            handleCreateFault();
        },
        initSuccessFault: function (result) {
            handleSuccessFault(result);
        },
        initFaultList: function () {
            handleFaultList();
        },
        initDeleteFault: function (id) {
            handleDeleteFault(id);
        },
        initUpdateFault: function (id) {
            handleUpdateFault(id);
        },
    };
}();