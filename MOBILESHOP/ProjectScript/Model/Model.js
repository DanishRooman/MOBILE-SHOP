var Model = function () {
    //private static
    var handleSuccessModel = function (result) {

        if (result.key) {

            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
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

    handleEmptyModel = function () {
        $('.form-control').val("");
        $('.select2').val("");
    };

    //public static
    return {

        initSuccessModel: function (result) {
              handleSuccessModel(result);
        },
        initModelList: function () {
            handleModelListing();
        },
    };


}();