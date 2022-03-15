function ButtonClick(tab) {
    document.getElementById(tab).click();
}
// function changeSelectedLanguage(lan){
//   var select = document.getElementById('selectLanguage');
//   var option;
//   console.log(lan);
//   for (var i=0; i<select.options.length; i++) {
//   option = select.options[i];
//   if (option.value == lan) {
//       option.setAttribute('selected', true);
//       return; 
//   } 
//   }
// }

function ValidationIcon(inputName){
  if(document.getElementById(inputName).value == null || document.getElementById(inputName).value === ""){
    document.getElementById(inputName).classList.remove('is-valid');
    document.getElementById(inputName).classList.add('is-invalid');
  }
  else if(inputName!="phone"){
    document.getElementById(inputName).classList.remove('is-invalid');
    document.getElementById(inputName).classList.add('is-valid');
  }else if(PhoneValidation(inputName)){
    document.getElementById(inputName).classList.remove('is-invalid');
    document.getElementById(inputName).classList.add('is-valid');
  }else{
    document.getElementById(inputName).classList.remove('is-valid');
    document.getElementById(inputName).classList.add('is-invalid');
  }
}
function FormInputValidation(inputName){
    let empty = false;
    $('.form-control').each(function() {
        if ($(this).val() === "") {
          empty = true;
        }
    });
    ValidationIcon(inputName);
    if (empty) {
      $('#SendButton').attr('disabled', 'disabled');
    } else if(PhoneValidation(inputName)) {
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
  
  function PhoneValidation(inputName){
    // document.getElementById("ph").value = document.getElementById("phone").value;
    // var b = document.querySelector("#ph");
    // console.log(b)
    // let phoneInput = intlTelInput(b);
    const phoneInputField = document.querySelector("#phone");
    const phoneInput = window.intlTelInput(phoneInputField, {
      preferredCountries: ["uz", "us","gb", "ru"],
      utilsScript:
      "tel.js",
    });
    if(phoneInput.isValidNumber()) {
      document.getElementById("phone").value = phoneInput.getNumber();
      return true
    }else {
      $('#SendButton').attr('disabled', 'disabled');
      document.getElementById(inputName).focus();
      return false
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

window.onscroll = function() {scrollFunction()};

function scrollFunction() {
  $(window).scroll(function () {
    if($(window).scrollTop() > 200) {
      $('#navcollapseAll').css('position', 'fixed');
      $('#navcollapseAll').css('right', 0);
      $('#navcollapseAll').css('left', 0);
      $('#navcollapseAll').css('top', 0);
    } else {
    $('#navcollapseAll').css('position', '');
    // $('#navcollapseAll').css('background-color', 'white');
      $('#navcollapseAll').css('top', '-100%');
    }
  });
}

