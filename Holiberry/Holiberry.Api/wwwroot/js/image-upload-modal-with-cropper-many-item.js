/**
 * Function for image uploader in modal box with cropper
 * @param {string} btnShowModalElement - jquery selector for button opening modal with cropper
 * 
 * @param {string} uploadURL - url for api or ajax for upload imagw
 * 
 * @param {int} minImgHeight - minimum image height (in pixels)
 * 
 * @param {int} minImgWidth - minimum image width (in pixels)
 * 
 * @param {string} - jquery selector for image after cropping
 */
function imageUploadToGalleryModal(btnShowModalElement, uploadURL, minImgWidth, minImgHeight, imageBase) {
    //settings

    var imgQuality = 0.9;
    var imgRatio = minImgWidth / minImgHeight;

    //variables
    var imageUploadedElement = $("#img-uploaded-gallery");
    var modalElement = $("#modal-upload-image-gallery");
    var _btnShowModalElement = $(btnShowModalElement);
    var fileInputElement = $("#input-image-to-gallery-upload");
    var fileUploadBox = $("#file-upload-to-gallery-box");
    var btnUploadSend = $("#btn-upload-to-gallery-send");
    var _imageBase = $(imageBase);
    var fileType = null;
    var cropBoxData;
    var canvasData;
    var cropper;
    var entityId;
    var optionsButtons = $('#optionsGalleryButtons');

    console.log("image:", $(imageBase));
    //Toogle modal button event
    _btnShowModalElement.on('click', function (e) {
        e.preventDefault();
        entityId = $(this).data('id');
        uploadURL = uploadURL.replace('id', entityId);
        console.log('EntityId: ', entityId);
        console.log('uploadURL: ', uploadURL);
        btnUploadSend.hide();
        optionsButtons.hide();

        modalElement.modal({ backdrop: 'static', keyboard: false });

    });

    //Modal events
    modalElement.on('shown.bs.modal', function () {
        //Show upload box
        fileUploadBox.show();

        //File input change venet
        fileInputElement.bind('change', function (evt) {
            console.log(evt);
            var tgt = evt.target || window.event.srcElement,
                files = tgt.files;

            //Exit when file format is not correct
            if (files[0].type !== "image/jpeg" && files[0].type !== "image/png") {
                alert("Niedozwolony format pliku. PrzeĹlij zdjÄcie .jpg lub .png");
                return;
            }

            //Save fileType
            fileType = files[0].type;

            // FileReader support
            if (FileReader && files && files.length) {
                var fr = new FileReader();
                fr.onload = function () {

                    //Intit image
                    imageUploadedElement.attr('src', fr.result);


                    //Destroy cropper if inititialized
                    imageUploadedElement.cropper('destroy');

                    cropper = null;

                    //Init cropper
                    cropper = imageUploadedElement.cropper({
                        aspectRatio: imgRatio,
                        dragMode: 'move',
                        viewMode: 0,
                        responsive: true,
                        restore: true,
                        //center: true,
                        autoCrop: true,
                        maxheight: 600,
                        autoCropArea: 0.8,
                        wheelZoomRatio: 0.05,
                        rotatable: false,
                        movable: true,
                        background: true,
                        modal: true,
                        ready: function () {
                            // Methods
                            optionsButtons.fadeIn(750);
                        }
                    });
                }

                fr.readAsDataURL(files[0]);
                btnUploadSend.show();

                //Hide upload box
                fileUploadBox.hide();

            }
            // Not supported
            else {
                // fallback -- perhaps submit the input to an iframe and temporarily store
                // them on the server until the user's session ends.
            }
        });

        //Send cropped image event
        btnUploadSend.bind('click', function (e) {
            e.preventDefault();
            $(this).addClass('btn-loading').prop('disabled', true);

            //Get Canvas dataURL from cropper
            var contentType = "image/png"; //Default content type
            if (fileType !== null) {
                contentType = fileType;
            }

            //Replace string based on type
            var replaceStr = 'data:' + contentType + ';base64,';

            var canvas = cropper.cropper('getCroppedCanvas', {
                width: minImgWidth,
                height: minImgHeight,
                fillColor: "#fff"
            });
            console.log(canvas);
            var dataURL = canvas.toDataURL(contentType, imgQuality).replace(replaceStr, '');
            $.ajax({
                type: 'POST',
                url: uploadURL,
                data: JSON.stringify({
                    imageData: dataURL,
                    contentType: contentType,
                    width: minImgWidth,
                    height: minImgHeight
                }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    if (data.isSuccess) {
                        location.reload();
                        modalElement.modal('hide');
                    } else {
                        alert("Nie udało się wysłać zdjęcia. Odśwież strone i spróbuj ponownie.");
                    }
                }
            });
        }).prop('disabled', false);

    });
    modalElement.on('hide.bs.modal', function () {
        //Destroy cropper if inititialized
        imageUploadedElement.cropper('destroy');
        cropper = null;
        imageUploadedElement.attr('src', '');
        btnUploadSend.removeClass('btn-loading');
        fileInputElement.val("");
        //Hidse upload box
        fileUploadBox.hide();


        fileInputElement.unbind();
        btnUploadSend.unbind();
        //init cropper


    });

    $('.cropper-buttons').on('click', '[data-method]', function () {
        var $this = $(this);
        var data = $this.data();
        var cropper = imageUploadedElement.data('cropper');
        var cropped;
        var $target;
        var result;


        if ($this.prop('disabled') || $this.hasClass('disabled')) {
            return;
        }

        if (cropper && data.method) {
            data = $.extend({}, data); // Clone a new one

            if (typeof data.target !== 'undefined') {
                $target = $(data.target);

                if (typeof data.option === 'undefined') {
                    try {
                        data.option = JSON.parse($target.val());
                    } catch (e) {
                        console.log(e.message);
                    }
                }
            }

            cropped = cropper.cropped;

            switch (data.method) {
                case 'rotate':
                    if (cropped && options.viewMode > 0) {
                        imageUploadedElement.cropper('clear');
                    }

                    break;

                case 'getCroppedCanvas':
                    if (uploadedImageType === 'image/jpeg') {
                        if (!data.option) {
                            data.option = {};
                        }

                        data.option.fillColor = '#fff';
                    }

                    break;
            }

            result = imageUploadedElement.cropper(data.method, data.option, data.secondOption);

            switch (data.method) {
                case 'rotate':
                    if (cropped && options.viewMode > 0) {
                        imageUploadedElement.cropper('crop');
                    }

                    break;

                case 'scaleX':
                case 'scaleY':
                    $(this).data('option', -data.option);
                    break;

                case 'getCroppedCanvas':
                    if (result) {
                        // Bootstrap's Modal
                        $('#getCroppedCanvasModal').modal().find('.modal-body').html(result);

                        if (!$download.hasClass('disabled')) {
                            download.download = uploadedImageName;
                            $download.attr('href', result.toDataURL(uploadedImageType));
                        }
                    }

                    break;

                case 'destroy':
                    if (uploadedImageURL) {
                        URL.revokeObjectURL(uploadedImageURL);
                        uploadedImageURL = '';
                        imageUploadedElement.attr('src', originalImageURL);
                    }

                    break;
            }

            if ($.isPlainObject(result) && $target) {
                try {
                    $target.val(JSON.stringify(result));
                } catch (e) {
                    console.log(e.message);
                }
            }
        }
    });
}