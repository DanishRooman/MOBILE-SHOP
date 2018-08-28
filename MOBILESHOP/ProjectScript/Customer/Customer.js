var Customer = function () {
    //private Static

    var handleEmptyCustomer = function () {
        $('.form-control').val("");

    };

    var handleSuccessCustomer = function (result) {
       
        if (result.key) {

            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
            handleEmptyCustomer();
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
    var handleCustomerList = function () {
        $.ajax({
            url: '/Customer/CustomerListing',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#divCustomer").empty();
                $("#divCustomer").html(result);
            },
            error: function () {
                console.log("Error");
            }
        });

    };
    handleDeleteCustomer = function (id) {

        $.confirm({
            title: 'Delete Group',
            content: 'Are you sure you want to delete this Category?',
            theme: 'material',
            buttons: {
                confirm: {
                    btnClass: "btn-blue",
                    keys: ["enter"],
                    action: function () {
                        $.ajax({
                            url: '/Customer/DeleteCustomer',
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
                                    handleCustomerList();
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
    //public Static
    return {

        initCreateCustomer: function () {
            handleCreateCustomer();
        },
        initCustomerSuccess: function (result) {
            handleSuccessCustomer(result);
        },
        initCustomerList: function () {
            handleCustomerList();
        },
        initDeleteCustomer: function (id) {
            handleDeleteCustomer(id);
        },
      

    };


}();
