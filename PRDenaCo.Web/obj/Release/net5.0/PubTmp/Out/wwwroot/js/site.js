// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var dialogConfirm = null;
showInPopup = (url, title, closeUrl) => {

    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $("#form-modal").modal({
                backdrop: 'static',
                keyboard: false
            });
            $('#form-modal .close').click(function () {
                jQueryAjaxCloseDialog(closeUrl);
            });
            $('#form-modal').modal('show');
            // to make popup draggable
            $('.modal-dialog').draggable({
                handle: ".modal-header"
            });
         
        }
    })
}

function jQueryAjaxCloseDialog(url) {
    if (url == undefined || url==null) {
        $('#form-modal .modal-body').html('');
        $('#form-modal .modal-title').html('');
        $('#form-modal').modal('hide');
    }
    try {
        
        $.ajax({
            type: 'POST',
            url: url,
            data: '[]',
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                }
                else
                $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}


jQueryAjaxPost = form => {
   
    try {
       
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            beforeSend: function () {
                var btnSubmit = $(form).closest('form').find(':submit');
                $(btnSubmit).prop("disabled", true);
                $(btnSubmit).append( '<span class="spinner-border spinner-border-sm m-r-5" role="status" aria-hidden="true"></span>');
            },
            success: function (res) {
                //if (res.isValid) {
                //    $('#view-all').html(res.html)
                //    $('#form-modal .modal-body').html('');
                //    $('#form-modal .modal-title').html('');
                //    $('#form-modal').modal('hide');
                //}
                //else

                    $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

function jQueryAjaxDeleteWithParam(idforDelete, url) {
  
    if (dialogConfirm == null)
        SwitAlert('Are you sure?', 'Are you sure to delete this record ?', 'warning', true, true, jQueryAjaxDeleteWithParam, [idforDelete, url]);
    else {
        if (dialogConfirm == true) {
            try {
                $.ajax({
                    type: 'POST',
                    url: url,
                    dataType: "JSON",
                    data: { id: idforDelete },
                 
                    success: function (res) {
                        if (res.isValid != null && res.isValid == true) {
                            SwitAlert('', 'Operation Success', 'success');
                            $('#view-all').html(res.html);
                        } else
                            SwitAlert('', res.message, 'error');
                        dialogConfirm = null;
                    },
                    error: function (err) {
                        // console.log(err)
                        SwitAlert('', err, 'error');
                        dialogConfirm = null;
                    }
                })
            } catch (ex) {
                console.log(ex);
                dialogConfirm = null;
            }
        }
    }
    dialogConfirm = null;
    //prevent default form submit event
    return false;
}

jQueryAjaxDelete = form => {
    if (dialogConfirm == null)
        SwitAlert('Are you sure?', 'Are you sure to delete this record ?', 'warning', true, true, jQueryAjaxDelete, [form]);
    else {
        if (dialogConfirm == true) {
            try {
                $.ajax({
                    type: 'POST',
                    url: form.action,
                    data: new FormData(form),
                    contentType: false,
                    processData: false,
                    success: function (res) {
                        if (res.isValid != null && res.isValid == true) {
                            SwitAlert('', 'Operation Success', 'success');
                            $('#view-all').html(res.html);
                        } else
                            SwitAlert('', res.message, 'error');
                        dialogConfirm = null;
                    },
                    error: function (err) {
                        // console.log(err)
                        SwitAlert('', err, 'error');
                        dialogConfirm = null;
                    }
                })
            } catch (ex) {
                console.log(ex);
                dialogConfirm = null;
            }
        }
    }
    dialogConfirm = null;
    //prevent default form submit event
    return false;
}

jQueryAjaxDeleteL2 = form => {

    if (dialogConfirm == null)
        SwitAlert('Are you sure?', 'Are you sure to delete this record ?', 'warning', true, true, jQueryAjaxDeleteL2, [form]);
    else {
        if (dialogConfirm == true) {
            try {
                $.ajax({
                    type: 'POST',
                    url: form.action,
                    data: new FormData(form),
                    contentType: false,
                    processData: false,
                    success: function (res) {
                        if (res.isValid != null && res.isValid == true) {
                            SwitAlert('', 'Operation Success', 'success');
                            $('#view-allL2').html(res.html);
                        } else
                            SwitAlert('', res.message, 'error');
                        dialogConfirm = null;
                    },
                    error: function (err) {
                        SwitAlert('', err, 'error');
                        dialogConfirm = null;
                        //console.log(err)
                    }
                })
            } catch (ex) {
                console.log(ex);
                dialogConfirm = null;
            }
        }
    }
    dialogConfirm = null;
    //prevent default form submit event
    return false;
}
showInPopupL2 = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
         
            $('#form-modalL2 .modal-body').html(res);
            $('#form-modalL2 .modal-title').html(title);
            //$("#form-modalL2").modal({
            //    backdrop: 'static',
            //    keyboard: false
            //});
            $('#form-modalL2').modal('show');
            // to make popup draggable
           
           
        }
    })
}

jQueryAjaxPostL2 = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            //beforeSend: function () {
            //    var btnSubmit = $(form).closest('form').find(':submit');
            //    $(btnSubmit).prop("disabled", true);
            //    $(btnSubmit).append('<span class="spinner-border spinner-border-sm m-r-5" role="status" aria-hidden="true"></span>');
            //},
            success: function (res) {
                if (res.isValid == undefined) {
                    $('#view-allL2').html(res.html)
                    $('#form-modalL2 .modal-body').html('');
                    $('#form-modalL2 .modal-title').html('');
                    $('#form-modalL2').modal('hide');
                }else
                if (res.isValid != null && res.isValid==true) {
                    $('#view-allL2').html(res.html)
                    $('#form-modalL2 .modal-body').html('');
                    $('#form-modalL2 .modal-title').html('');
                    $('#form-modalL2').modal('hide');
                    toastr.success('Operation Success');
                }
                else {
                    $('#form-modalL2 .modal-body').html(res.html);
                    toastr.warning('Operation Error');
                }
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}


function SwitAlert(alertTitle, alertText, alertIcon, alertButtons=false, alertDangerMode=false , callback, params) {
    /*
     alertButtons : 'warning','success','error','info'
     */
  
    if (alertTitle == '')
        alertTitle = 'System Message';
    if (alertButtons == true || alertDangerMode == true) {
        swal({
            title: alertTitle,
            text: alertText,
            icon: alertIcon,
            buttons: alertButtons,
            dangerMode: alertDangerMode,
        })
            .then((willDelete) => {
                
                if (willDelete != null) {
                    dialogConfirm = willDelete;
                    callback.apply(this, params);
                } else
                    dialogConfirm = null;
            });
    } else {
        swal({
            title: alertTitle,
            text: alertText,
            icon: alertIcon
        });
        dialogConfirm = false;
    }
}
