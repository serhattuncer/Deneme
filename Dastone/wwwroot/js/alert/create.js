﻿function create(uri, model, action) {
    debugger;
    swal.fire({
        title: 'Emin misiniz?',
        text: 'Kaydetme işlemi yapmaktasınız, kaydı geri alamazsınız!',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: '"Evet, Kaydet!',
        cancelButtonText: 'Hayır, Kaydetme!',
        reverseButtons: true
    }).then(function (result) {
        if (result.value) {
            $.ajax({
                url: uri,
                type: 'POST',
                data: JSON.stringify(model),
                contentType: 'application/json',
                success: function (response) {
                    Swal.fire(
                        'Kaydedildi!',
                        'Kaydetme Başarılı.',
                        'success'
                    ).then(() => {
                        window.location.href = action;
                    });
                },
                error: function () {
                    Swal.fire(
                        'Hata!',
                        'Kaydederken hata oluştu.',
                        'error'
                    );
                }
            });
        } else if (
            result.dismiss === Swal.DismissReason.cancel
        ) {
            swal.fire(
                'İptal',
                'Kayıtlarınız güvende:)',
                'error'
            )
        }
    })
}
