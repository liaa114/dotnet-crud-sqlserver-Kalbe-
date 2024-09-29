$(document).on('click', '[id^=btn_delete_]', function() {
    var assetId = $(this).attr('id').split('_')[2];

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    });

    swalWithBootstrapButtons.fire({
        title: 'Are you sure?',
        text: 'You won\'t be able to revert this!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, cancel!',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Assets/DeleteAsset',
                type: 'POST',
                data: { id: assetId },
                success: function(response) {
                    if (response.success) {
                        swalWithBootstrapButtons.fire(
                            'Deleted!',
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
                    swalWithBootstrapButtons.fire(
                        'Error!',
                        'There was an issue deleting the asset.',
                        'error'
                    );
                }
            });
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            swalWithBootstrapButtons.fire(
                'Cancelled',
                'Your asset is safe :)',
                'error'
            );
        }
    });
});