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
    };
     return {
        initSuccessFault: function (result) {
            handleSuccessFault(result);
        },
        initFaultList: function () {
            handleFaultList();
        },
    };
}();