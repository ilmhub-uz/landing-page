function ButtonClick(tab) {
    document.getElementById(tab).click();
}
  
function FormInputValidation(){
    let empty = false;
    $('.form-control').each(function() {
        if ($(this).val() === "") {
        empty = true;
        }
    });
    if (empty) {
    $('#SendButton').attr('disabled', 'disabled');
    } else {
    $('#SendButton').removeAttr('disabled');
    }
}
  
  function StopVideo(){
    $('#exampleModal').on('hidden.bs.modal', function () {
        $("#exampleModal iframe").attr("src", $("#exampleModal iframe").attr("src"));
    });
  }
  function SetCountryCode(){
    const phoneInputField = document.querySelector("#phone");
    const phoneInput = window.intlTelInput(phoneInputField, {
      preferredCountries: ["uz", "us","gb", "ru"],
      utilsScript:
      "tel.js",
    });
    const CountryCode = phoneInput.getSelectedCountryData().dialCode;
    document.getElementById("phone").value = "+"+CountryCode;
  }
  
  function PhoneValidation(){
    const phoneInputField = document.querySelector("#phone");
    const phoneInput = window.intlTelInput(phoneInputField, {
    preferredCountries: ["uz", "us","gb", "ru"],
    utilsScript:
      "tel.js",
    });
    const phoneNumber = phoneInput.getNumber();
    if(phoneInput.isValidNumber()) {
      document.getElementById("phone").value = phoneNumber;
    }else {
      // $('#SendButton').attr('disabled', 'disabled');
    }
  }
  
  function Send_Data(url, formtype){
      if(formtype=="contact-form" && jQuery("#message").val() == ""){
        return false;
      }
      document.getElementById("SendButton").disabled = true;
      const form = document.forms[formtype]
      fetch(url, { method: 'POST', body: new FormData(form)})
      .then(response => $("#form_alerts").html("<div class='alert alert-success'>Xabar muvaffaqiyatli yetkazildi</div>"))
      .catch(error => $("#form_alerts").html("<div class='alert alert-danger'>Xabarni yetkazishda xatolik yuz berdi! Iltimos qaytadan urinib ko'ring.</div>"))
      .finally(response=> SetCountryCode());
      
      form.addEventListener('submit', e => {
        e.preventDefault();
      });
      jQuery.ajax({ 
          success: function(response) {
              jQuery("#"+formtype)[0].reset();
          }
      });
  }