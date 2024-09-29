$(document).ready(function () {
    $('#sidebarToggle').on('click', function () {
        $('#sidebar').toggleClass('active');
        
        // Pindahkan tombol toggle sesuai posisi sidebar
        if ($('#sidebar').hasClass('active')) {
            $('#sidebarToggle').css('left', '225px'); // Setengah di dalam sidebar
        } else {
            $('#sidebarToggle').css('left', '10px'); // Kembali ke posisi awal
        }
    });
});
