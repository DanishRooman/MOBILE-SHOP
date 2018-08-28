var Fault = function () {

    var handleSuccessFault = function (result) {

        if (result.key) {

            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
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
    var handleEmptyFault = function () {
        $('.form-control').val("");
    };


    return {


        initSuccessFault: function (result) {
            handleSuccessFault(result);
        },

    };



}();