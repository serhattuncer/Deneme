function create(uri, model, action) {
    return new Promise((resolve, reject) => {
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
                        resolve(response);  // ✅ Başarılı olursa promise çözülür
                    },
                    error: function () {
                        Swal.fire(
                            'Hata!',
                            'Kaydederken hata oluştu.',
                            'error'
                        );
                        reject('Hata oluştu!');  // ❌ Hata olursa promise reddedilir
                    }
                });
            } else {
                swal.fire(
                    'İptal',
                    'Kayıtlarınız güvende :)',
                    'error'
                );
                reject('İptal edildi!');  // ❌ Kullanıcı iptal ederse promise reddedilir
            }
        });
    });
}
