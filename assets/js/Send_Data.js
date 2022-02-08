function Send_Data() {
    var form = document.forms['contact-form']

    fetch('https://script.google.com/macros/s/AKfycbw1apW_s44TvfpFKHhu8B_X5LAPVCsYmhW25GSTacRFK0Wz97Rs1EjF5ohHtdfG_CmwVw/exec', { method: 'POST', body: new FormData(form)})
    .then(response => $("#form_alerts").html("<div class='alert alert-success'> Message send successfully</div>"))
    .catch(error => $("#form_alerts").html("<div class='alert alert-danger'> Message sending failed</div>"))
    form.addEventListener('submit', e => {
        e.preventDefault()
    })

    jQuery.ajax({ 
        success: function(response) {
            document.forms['contact-form'].reset();
        }
    });
}