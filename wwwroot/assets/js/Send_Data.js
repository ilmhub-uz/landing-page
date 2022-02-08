function Send_Data(url, formtype){
    var form = document.forms[formtype]
    fetch(url, { method: 'POST', body: new FormData(form)})
    .then(response => $("#form_alerts").html("<div class='alert alert-success'>Xabar muvaffaqiyatli yetkazildi</div>"))
    .catch(error => $("#form_alerts").html("<div class='alert alert-danger'>Xabarni yetkazishda xatolik yuz berdi! Iltimos qaytadan urinib ko'ring.</div>"))
    form.addEventListener('submit', e => {
        e.preventDefault()
    })

    jQuery.ajax({ 
        success: function(response) {
            document.forms[formtype].reset();
        }
    });
}