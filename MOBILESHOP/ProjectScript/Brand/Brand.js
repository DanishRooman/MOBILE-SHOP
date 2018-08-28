var Brand = function () {
   
    var handleSuccesBrand = function (result) {
        if (result.key) {

            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
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
    var handleEmptyBrand = function () {
        $('.form-control').val("");
        };
    return {

        initBrandSuccess: function (result) {
          handleSuccesBrand(result);
        },

    };


}();