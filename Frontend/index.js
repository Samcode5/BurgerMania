function $(id)
{
    return document.getElementById(id);
}

    $("form").onsubmit=function(e){
    // e.preventDefault()
    const number=$("phone");
    var formValid=true;
    if(number.value.length!=10)
    {
        e.preventDefault()
          formValid=false;
          $("error").innerHTML="Please Enter 10 digits Number"
          $('phone').classList.add("errorPhone")

    }


    
    if(formValid)
    {
       $("error").innerHTML=""
        console.log("Form submitted succesfully");
        $('phone').classList.remove("errorPhone")

    }
    
}