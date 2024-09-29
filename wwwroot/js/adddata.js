$(document).on('submit', '#addAssetForm', function(e) {
    e.preventDefault();

    const formData = $(this).serialize();

    $.ajax({
        url: '/Assets/AddAsset',
        type: 'POST',
        data: formData,
        success: function(response) {
            if (response.success) {
                Swal.fire(
                    'Success!',
                    'Asset has been added successfully.',
                    'success'
                ).then(() => {
                    location.reload();
                });
            } else {
                Swal.fire(
                    'Error!',
                    response.message,
                    'error'
                );
            }
        },
        error: function() {
            Swal.fire(
                'Error!',
                'There was an issue adding the asset.',
                'error'
            );
        }
    });
});