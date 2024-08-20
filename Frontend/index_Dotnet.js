function $(id)
{
    return document.getElementById(id);
}
 

  var data={number:0,name:"",password:"",role:"User"}
    $("form").onsubmit=function(e){
      e.preventDefault();
    const number=$("phone");
    const name=$("Name");
    const password=$("password");
    console.log(name,number);
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
        $('phone').classList.remove("errorPhone");


        //check if the number already exists
        
        fetch("https://localhost:7287/api/UserAPI", {
          method: "GET",
          headers: {
            "Content-Type": "application/json"
          }
        })
        .then(data => data.json())
        .then(response => {
          console.log("Inside GET body");
          console.log(response);
          
          var isIdAvailable=0;
          response.forEach(element => {
              if(element.number!= number.value.toString())
              {
                 console.log("Creating a new user");
              


              }

              else
              {
              
                 isIdAvailable=1;
              }
          });

          if(isIdAvailable==0)
          {
                    // post api call 
        data.name=name.value;
        data.number=number.value.toString();
        data.password=password.value;

        console.log("Sending data:",data)
        console.log(typeof(data.number));
        localStorage.setItem("UserData",JSON.stringify(data));
        fetch("https://localhost:7287/api/UserAPI", {
        method: "POST",
        body: JSON.stringify(data),
        headers: {
          "Content-Type": "application/json"
        }
      })
      .then(data => data.json())
      .then(response => {
        console.log("Inside POST body");
        console.log(response)});
          }


      window.location.href="./login.html";
        
        });
        

      

  }
}