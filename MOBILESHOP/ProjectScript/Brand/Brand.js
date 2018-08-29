var Brand = function () {
   
    var handleSuccesBrand = function (result) {
        if (result.key) {

            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
            handleBrandList();
            handleEmptyBrand();
           
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
        debugger
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
                            url:'/Brand/DeleteBrand',
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
        };
    return {

        initBrandSuccess: function (result) {
          handleSuccesBrand(result);
        },
        initBrandList: function () {
            handleBrandList();
        },
        initDeleteBrand: function (id) {
            handleDeleteBrand(id);
        },

    };


}();