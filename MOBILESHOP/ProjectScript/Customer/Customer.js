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
            //alert(result.key);
            var deviceKey = result.id;
            if (window.FormData !== undefined) {
                var fileUpload = $("#fileUpload").get(0);
                var files = fileUpload.files;
                // Create FormData object  
                var fileData = new FormData();
                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }
                fileData.append('deviceKey', deviceKey);
                $.ajax({
                    url: '/Customer/UploadFiles',
                    type: "POST",
                    contentType: false, // Not to set any content header  
                    processData: false, // Not to process data  
                    data: fileData,
                    success: function (result) {

                    },
                    error: function (err) {
                        alert(err.statusText);
                    }
                });
            }
            else {
                alert("Form data not supported by browser");
            }

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
