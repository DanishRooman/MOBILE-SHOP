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


    handleEmptyModel = function () {
        $('.form-control').val("");
        $('.select2').val("");
    };

    //public static
    return {

        initSuccessModel: function (result) {
              handleSuccessModel(result);
        },
    };


}();