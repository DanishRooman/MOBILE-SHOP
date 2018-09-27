var Brand = function () {
    var handleCreateBrand = function () {

        //Ajax call
        $.ajax({
            url: '/Brand/AddBrand',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#CreateBrand").empty();
                $("#CreateBrand").html(result);
                $("#brandTitle").text("Add Mobile Brand");
                $("#CreateBrand").modal("show");
            },
            error: function () {
                console.log("Error");
            }
        });

    };
    var handleSuccesBrand = function (result) {
        if (result.key) {

            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
            handleEmptyBrand();
            handleBrandList();
           

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
    var handleBrandList = function () {

        $.ajax({
            url: '/Brand/BrandListing',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#divBrand").empty();
                $("#divBrand").html(result);
            },
            error: function () {
                console.log("Error");
            }
        });

    };
    var handleDeleteBrand = function (id) {
       
        $.confirm({
            title: 'Delete Brand',
            content: 'Are you sure you want to delete this brand?',
            theme: 'material',
            buttons: {
                confirm: {
                    btnClass: "btn-blue",
                    keys: ["enter"],
                    action: function () {
                        $.ajax({
                            url: '/Brand/DeleteBrand',
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
                                    handleBrandList();
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
    var handleEmptyBrand = function () {
        $('.form-control').val("");
        $("#CreateBrand").modal("hide");
    };
    var handleUpdateBrand = function (id) {
     
        $.ajax({
            url: '/Brand/UpdateBrand',
            type: 'GET',
            dataType: 'HTML',
            data: { "id": id },
            success: function (result) {
                $("#CreateBrand").empty();
                $("#CreateBrand").html(result);
                $("#brandTitle").text("Update Mobile Brand");
                $("#CreateBrand").modal("show");
            },
            error: function () {
                console.log("Error");
            }
        });
    };
    return {
        initCreateBrand: function () {
            handleCreateBrand();
        },

        initBrandSuccess: function (result) {
            handleSuccesBrand(result);
        },
        initBrandList: function () {
            handleBrandList();
        },
        initDeleteBrand: function (id) {
            handleDeleteBrand(id);
        },
        initUpdateBrand: function (id) {
            handleUpdateBrand(id);
        },

    };


}();