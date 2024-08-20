function $(id)
{
    return document.getElementById(id);
}


$("form").onsubmit=function(e)
{
    e.preventDefault();
    var number=$("phone").value;
    var password=$("password").value;

    console.log(number,password);
    var data={};
    data.Number=number;
    data.Password=password;

   fetch("https://localhost:7287/api/Auth/login",{
    method:"POST",
    body:JSON.stringify(data),
    headers: {
        "Content-Type": "application/json"
      }
   }).then(res => res.json()).then((res)=>{
    console.log(res);
    sessionStorage.setItem("Usertoken",res.token);

    if(data.Number=="0000000000" && data.Password=="admin")
    {
           window.location.href="./Adminfolder/index.html";
    }

    else window.location.href="./burgerPage.html"
   }).catch((err)=>{
    console.log(err);
   })


}