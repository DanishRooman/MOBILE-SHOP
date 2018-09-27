var Model = function () {
    //private static
    var handleCreateModel = function () {

        //Ajax call
        $.ajax({
            url: '/Model/AddModel',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#CreateModel").empty();
                $("#CreateModel").html(result);
                $("#ModelTitle").text("Add Mobile Model");
                $("#CreateModel").modal("show");
            },
            error: function () {
                console.log("Error");
            }
        });


    };
    var handleSuccessModel = function (result) {

        if (result.key) {
            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
            handleModelListing();
            handleEmptyModel();
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
    var handleModelListing = function () {
        $.ajax({
            url: '/Model/ModelListing',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#divModel").empty();
                $("#divModel").html(result);
            },
            error: function () {
                console.log("Error");
            }
        });

    };

    var handleEmptyModel = function () {
        $('.form-control').val("");
        $('.select2').val("");
        $("#CreateModel").modal("hide");
    };

    var handleUpdateModel = function (id) {
        debugger
        $.ajax({
            url: '/Model/UpdateModel',
            type: 'GET',
            dataType: 'HTML',
            data: { "id": id },
            success: function (result) {
                $("#CreateModel").empty();
                $("#CreateModel").html(result);
                $("#ModelTitle").text("Update Mobile Model");
                $("#CreateModel").modal("show");
            },
            error: function () {
                console.log("Error");
            }
        });

    };
    var handleDeleteModel = function (id) {
        debugger
        $.confirm({
            title: 'Delete Model',
            content: 'Are you sure you want to delete this Model..?',
            theme: 'material',
            buttons: {
                confirm: {
                    btnClass: "btn-blue",
                    keys: ["enter"],
                    action: function () {
                        $.ajax({
                            url: '/Model/DeleteModel',
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
                                    handleModelListing();
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

    //public static
    return {
        initCreateModel: function () {
            handleCreateModel();
        },
        initSuccessModel: function (result) {
            handleSuccessModel(result);
        },
        initModelList: function () {
            handleModelListing();
        },
        initUpdateModel: function (id) {
            handleUpdateModel(id);
        },
        initDeleteModel: function (id) {
            handleDeleteModel(id);
        },
    };


}();