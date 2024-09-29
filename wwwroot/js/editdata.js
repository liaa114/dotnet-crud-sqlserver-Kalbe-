$(document).on('submit', 'form[action^="/Assets/EditAsset/"]', function(e) {
    e.preventDefault();
    var form = $(this);
    var url = form.attr('action');

    $.ajax({
        url: url,
        type: 'POST',
        data: form.serialize(),
        success: function(response) {
            const swalWithBootstrapButtons = Swal.mixin({
                customClass: {
                    confirmButton: 'btn btn-success',
                    cancelButton: 'btn btn-danger'
                },
                buttonsStyling: false
            });

            if (response.success) {
                swalWithBootstrapButtons.fire(
                    'Updated!',
                    response.message,
                    'success'
                ).then(() => {
                    location.reload();
                });
            } else {
                swalWithBootstrapButtons.fire(
                    'Error!',
                    response.message,
                    'error'
                );
            }
        },
        error: function() {
            Swal.fire('Error!', 'There was an issue updating the asset.', 'error');
        }
    });
});