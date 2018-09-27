var Service = function () {
    var hadleCreateService = function () {

        $.ajax({
            url: '/Service/AddService',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#CreateService").empty();
                $("#CreateService").html(result);
                $("#ServiceTitle").text("Add Mobile Service");
                $("#CreateService").modal("show");
            },
            error: function () {
                console.log("Error");
            }
        });


    };
  
var handleSuccessService = function (result) {
        debugger
        if (result.key) {

            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
            handleEmptyService();
            handleServiceList();
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
var handleEmptyService = function () {
    debugger
    $('.form-control').val("");
    $("#CreateService").modal("hide");
};
    var handleServiceList = function () {
        $.ajax({
            url: '/Service/ServiceListing',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#divService").empty();
                $("#divService").html(result);
            },
            error: function () {
                console.log("Error");
            }
        });

    };
    var handleDeleteService = function (id) {
        $.confirm({
            title: 'Delete Service',
            content: 'Are you sure you want to delete this Service?',
            theme: 'material',
            buttons: {
                confirm: {
                    btnClass: "btn-blue",
                    keys: ["enter"],
                    action: function () {
                        $.ajax({
                            url: '/Service/DeleteService',
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
                                    handleServiceList();
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
    var handleEditService = function (id) {
        $.ajax({
            url: '/Service/EditService',
            type: 'GET',
            dataType: 'HTML',
            data: { "id": id },
            success: function (result) {
                $("#CreateService").empty();
                $("#CreateService").html(result);
                $("#ServiceTitle").text("Update Mobile Service");
                $("#CreateService").modal("show");
            },
            error: function () {
                console.log("Error");
            }
        });
    };
    return {
        initCreateService: function () {
            hadleCreateService();
        },
        initCreateSuccess: function (result) {
            handleSuccessService(result);
        },
        initServiceList: function () {
            handleServiceList();
        },
        initDeleteService: function (id) {
            handleDeleteService(id)
        },
        initEditService: function (id) {
            handleEditService(id);
        },

    };



}();