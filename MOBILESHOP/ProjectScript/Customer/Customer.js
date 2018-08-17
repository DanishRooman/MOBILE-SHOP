var Customer = function () {

    //private Static
    var handleCreateCustomer = function () {

        //Ajax Call
        $.ajax({
            url: '/Categories/AddCategories',
            type: 'GET',
            dataType: 'HTML',
            data: {},
            success: function (result) {
                $("#CreateCategories").empty();
                $("#CreateCategories").html(result);
                $("#CreateCategories").modal("show");
            },
            error: function () {
                console.log("Error");
            }
        });
 };
    //public Static
    return {

        initCreateCustomer: function () {
            handleCreateCustomer();

        },

    };


}();