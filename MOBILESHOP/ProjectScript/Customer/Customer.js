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
            setTimeout(function () { handlePrintBill(deviceKey); }, 1000);

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

    var handlePrintBill = function (devicekey) {
        var url = "/Customer/PrintBill?id=" + devicekey;
        window.location.href = url;
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

    var handleReceiverSignatureModel = function () {
        $("#receiverSignature").empty();
        var htmldiv = "<div id='signatureReceiver'><div id='receivedSignature'></div></div>";
        $("#receiverSignature").html(htmldiv);
        $('#receiverModel').modal({
            backdrop: 'static',
            keyboard: false
        })
        $("#receiverModel").modal("show");

        $("#signatureReceiver").jSignature({
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

    var handleReceiverSignatureReset = function () {
        $("#signatureReceiver").jSignature('reset');
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

    var handleReceiverSignatureSave = function () {
        var rawdata = $("#signatureReceiver").jSignature('getData', "default");
        $("#receivedSignature").val(rawdata);
        $("#ReceiverSignatureImg").attr("src", rawdata);
        $("#receiverModel").modal("hide");
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
            failure: function () { }
        });
    };

    var handleDeleteImage = function (id) {
        $.confirm({
            title: 'Delete Group',
            content: 'Are you sure you want to delete this Image?',
            theme: 'material',
            buttons: {
                confirm: {
                    btnClass: "btn-blue",
                    keys: ["enter"],
                    action: function () {
                        $.ajax({
                            url: '/Customer/DeleteImage',
                            type: 'GET',
                            dataType: 'json',
                            data: { "imageKey": id },
                            success: function (result) {
                                if (result.key) {
                                    $.toast({
                                        heading: 'Success',
                                        text: result.value,
                                        showHideTransition: 'slide',
                                        icon: 'success'
                                    });
                                    setTimeout(function () { location.reload(); }, 1200);
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


    var handleModelDropdown = function (id) {
        if (id != "" && id != undefined && id != null)
        {
            $.ajax({
                url: '/Customer/GetModels',
                type: 'GET',
                dataType: 'json',
                data: { "brandkey": id },
                success: function (result) {
                    if (result.key) {
                        debugger
                        $("#model").empty();
                        var options = '<option value="">--Select Model--</option>';
                        for (var i = 0; i < result.value.length; ++i) {
                            options = options + "<option value='" + result.value[i].id + "'>" + result.value[i].modelName + "</option>";
                        }
                        $("#model").html(options);
                    }
                },
                error: function () {
                    console.log("Error");
                }
            });
        }
        else
        {
            $("#model").empty();
            var options = '<option value="">--Select Model--</option>';
            $("#model").html(options);
        }
       
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
        initModelDropdown: function (id) {
            handleModelDropdown(id);
        },
        initCameraClose: function () {
            handleCameraClose();
        },
        initSignatureModel: function () {
            handleSignatureModel();
        },
        initReceiverSignatureModel: function () {
            handleReceiverSignatureModel();
        },
        initSignatureReset: function () {
            handleSignatureReset();
        },
        initReceiverSignatureReset: function () {
            handleReceiverSignatureReset();
        },
        initSignatureSave: function () {
            handleSignatureSave();
        },
        initReceiverSignatureSave: function () {
            handleReceiverSignatureSave();
        },
        initDeleteImage: function (id) {
            handleDeleteImage(id);
        },


    };


}();
