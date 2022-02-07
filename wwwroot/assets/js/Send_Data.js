function Send_Data() {

    const scriptURL = 'https://script.google.com/macros/s/AKfycbw1apW_s44TvfpFKHhu8B_X5LAPVCsYmhW25GSTacRFK0Wz97Rs1EjF5ohHtdfG_CmwVw/exec'
    var form = document.forms['contact-form']
    form.addEventListener('submit', e => {
        e.preventDefault()
        fetch(scriptURL, { method: 'POST', body: new FormData(form)})
        .then(response => $("#form_alerts").html("<div class='alert alert-success'> Message send successfully</div>"))
        .catch(error => $("#form_alerts").html("<div class='alert alert-danger'> Message sending failed</div>"))
        .finally(response => window.location.reload())
    })
    jQuery.ajax({ 
        success: function(response) {
            document.forms['contact-form'].reset();
        }
    });
}