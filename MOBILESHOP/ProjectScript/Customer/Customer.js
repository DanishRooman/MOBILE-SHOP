var Customer = function () {
    //private Static

    var handleEmptyCustomer = function () {
        $('.form-control').val("");
    };

    var handleCamera = function () {
        Webcam.set({
            width: 320,
            height: 240,
            dest_width: 640,
            dest_height: 480,
            image_format: 'jpeg',
            jpeg_quality: 90,
            force_flash: false
        });
        Webcam.attach('#my_camera');
        $("#captureModal").modal("show");
    };

    var handleCameraSnap = function () {

        Webcam.snap(function (data_uri) {
            document.getElementById('my_result').innerHTML = '<img src="' + data_uri + '" class="img img-responsive img-thumbnail" alt="Image" id="imgCaptured"/>';
        });
        $("#txtLabel").removeClass("hide");
    };

    var handleCameraClose = function () {
        Webcam.reset();
        $("#captureModal").modal("hide");
    };

    var handleSuccessCustomer = function (result) {

        if (result.key) {

            $.toast({
                heading: 'Success',
                text: result.value,
                showHideTransition: 'slide',
                icon: 'success'
            });
            var deviceKey = result.id;
            if (window.FormData !== undefined) {
                debugger
                var fileUpload = $("#fileUpload").get(0);
                var files = fileUpload.files;

                var fileData = new FormData();
                // Create FormData object  
                var capturedfile = document.getElementById("imgCaptured").src;
                if (capturedfile != "http://www.placehold.it/200x150/EFEFEF/AAAAAA&text=no+image") {
                    fileData.append("CameraImage", capturedfile);
                    fileData.append("iscapture", true);
                }
                else {
                    fileData.append("iscapture", false);
                }

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
            handleSaveServices(deviceKey);
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
    var handleDeleteCustomer = function (id) {

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
    var handleSignatureModel = function () {
        $("#CustomerSignature").empty();
        var htmldiv = "<div id='signatureCustomer'><div id='signature'></div></div>";
        $("#CustomerSignature").html(htmldiv);
        $('#signatureModel').modal({
            backdrop: 'static',
            keyboard: false
        })
        $("#signatureModel").modal("show");

        $("#signatureCustomer").jSignature({

            "destroy": true,
            "reset": true,
            "clear": true,
            "UndoButton": true,

            "color": "#000",
            "lineWidth": 4,
            "width": 250,
            "height": 200,
            "background-color": "#edf6e9"
        });

    };
    var handleSignatureReset = function () {
        $("#signatureCustomer").jSignature('reset');
    };
    var handleSignatureSave = function () {
        var rawdata = $("#signatureCustomer").jSignature('getData', "default");
        $("#txtSignature").val(rawdata);
        $("#signatureImg").attr("src", rawdata);
        $("#signatureModel").modal("hide");
    };
    var handleSaveServices = function (deviceKey) {
        var services = [];
        $("#destination").find("option").each(function () {
            var id = $(this).val();
            services.push(id);
        });
        $.ajax({
            type: "POST",
            url: "/Customer/SaveServices",
            data: { "deviceId": deviceKey, "servicesId": services },
            traditional: true,
            dataType: "json",
            success: function () { },
            failure: function () {  }
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
        initCamera: function () {
            handleCamera();
        },
        initCameraSnap: function () {
            handleCameraSnap();
        },
        initCameraClose: function () {
            handleCameraClose();
        },
        initSignatureModel: function () {
            handleSignatureModel();
        },
        initSignatureReset: function () {
            handleSignatureReset();
        },
        initSignatureSave: function () {
            handleSignatureSave();
        },
    };


}();
