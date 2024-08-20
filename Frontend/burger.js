
var obj=[]; 



   
var prices={
    "Veg":100,
    "nonVeg":200,
    "egg":150
}
var typeOfBurger={
    "Veg":0,
    "nonVeg":0,
    "egg":0
}
var burgerType=[{cheeseChilli:[0,0,0]},{tandoriGrill:[0,0,0]},{surprise:[0,0,0]},{whopper:[0,0,0]},{crispySurprise:[0,0,0]}];
  
var map={
    "cheeseChilli":1,
    "tandoriGrill":2,
    "surprise":3,
    "whopper":4,
    "crispySurprise":5
}
var burgerTypeMap={
    "Veg":1,
    "NonVeg":2,
    "Egg":3
}
   
var Total=0;


document.getElementById('viewCart').onclick=function()
{
   
      location.href="Cart.html"
      

}
function handleCart(e)
{ var parent=e.parentElement;
var option=parent.childNodes[5];
var qty=parent.childNodes[9]
var burgerName=parent.childNodes[3].id;


 
if(qty.value>5)
{
     return alert("You can Select atmost 5 items only")
    
}

if(qty.value=="")
{
    return alert("please Select atleast one item");
}




// fetch('https://localhost:7168/api/Ordersapi', {
//     method: 'GET',
//     headers: {
//       "Content-Type": "application/json"
//     }
//   })
//   .then(data => data.json())
//   .then(response => {
//     console.log("Inside Get body");
//     console.log(response)});

var body={ userNumber:"",burgerId:'',burgerTypeId:'',quantity:0,price:0};



//      console.log(parent.childNodes);

//       console.log(option.value)
//     // console.log(parent.childNodes)
//     console.log(burgerName)

    
//     Total+=prices[option.value]*qty.value;
//     var index=map[burgerName];
//     // console.log("Type of burgerName",typeof(burgerName))
//     console.log(burgerType[index])
 
//     burgerType[index]=burgerType[index];


//     // console.log('b '+burgerName);
//     // console.log(burgerType[index].cheeseChilli);
//     // console.log(burgerType[index].burgerName);
//     // console.log(burgerType[index]['cheeseChilli']);
//     typeOfBurger[option.value]+=parseInt(qty.value)
//     // console.log(typeOfBurger[option.value])
//     console.log()
//     if(option.value=="Veg")
// {
//     burgerType[index][burgerName][0]=burgerType[index][burgerName][0]+ parseInt(qty.value)
// }

// else if(option.value=="nonVeg")
// {
//     burgerType[index][burgerName][1]=burgerType[index][burgerName][1]+ parseInt(qty.value)
// }

// else
// {
//     burgerType[index][burgerName][2]=burgerType[index][burgerName][2]+ parseInt(qty.value)
// }
    
    // console.log("Total amount is ",Total)
    // console.log(burgerType)
    // console.log(typeOfBurger)
    // localStorage.setItem("Total",Total)
    // localStorage.setItem("burgerType",JSON.stringify(burgerType));
    // localStorage.setItem("typeOfBurger",JSON.stringify(typeOfBurger));

    //getting userid

    

    //sending data to api
    var userData=JSON.parse(localStorage.getItem("UserData"));
    console.log(userData)



 
    

    var token=sessionStorage.getItem("Usertoken");
    // console.log(token)

    fetch("https://localhost:7287/api/OrdersAPI", {
        method: "POST",
        body: JSON.stringify({
            userNumber:userData.number,
            burgerId:map[burgerName],
            burgeTypeId:burgerTypeMap[option.value],
            quantity:parseInt(qty.value),
            price:prices[option.value]

        }),
        headers: {
          "Content-Type": "application/json"
        }
      })
      .then(data => data.json())
      .then(response => {
        console.log("Inside POST body");
        console.log(response)}).catch((err)=>{ console.log(err)});


}


function handleRemove(e)
{

    var body={burgerName:'',burgerType:'',quantity:0,price:0};

    

    if(Total==0)
    {
        return alert("Cart is Empty");
    }


    var parent=e.parentElement;
    var option=parent.childNodes[5];
    var qty=parent.childNodes[9]
    var burgerName=parent.childNodes[3].id;
    var index=map[burgerName];

    if(option.value=="Veg")
        {
            burgerType[index][burgerName][0]=burgerType[index][burgerName][0]- parseInt(qty.value)
        }
        
        else if(option.value=="nonVeg")
        {
            burgerType[index][burgerName][1]=burgerType[index][burgerName][1]- parseInt(qty.value)
        }
        
        else
        {
            burgerType[index][burgerName][2]=burgerType[index][burgerName][2]- parseInt(qty.value)
        }

    Total-=prices[option.value]*qty.value;
    console.log("Total amount is ",Total);
    console.log(burgerType);
    console.log(typeOfBurger);
    // localStorage.setItem("Total",Total);
    // localStorage.setItem("burgerType",JSON.stringify(burgerType));
    // localStorage.setItem("typeOfBurger",JSON.stringify(typeOfBurger));


     //reomving the burger
    console.log(typeof(burgerName));
    var len=0;
    let data;
    fetch('https://localhost:7287/api/OrdersAPI', {
        method: 'GET',
        headers: {
          "Content-Type": "application/json"
        }
      })
      .then(data => data.json())
      .then(response => {
        console.log("Inside Get  body");
        console.log(response)
        len=response.length;
        console.log(burgerName,option.value);
        
        response.forEach(element => {
            //unique key 
            if(element.burgerName==burgerName && element.burgerType==option.value)
                {
                   const  burgerID=element.burgerID;
                   
                    fetch(`https://localhost:7287/api/OrdersAPI/${burgerID}`, {
                        method: 'DELETE'
                    })
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Network response was not ok ' + response.statusText);
                        }
                        
                    })
                    .then(data => {
                        console.log('Success:', data); // Handle success
                    })
                    .catch(error => {
                        console.error('Error:', error); 
                    });
                }
        });
 
    });



    

    



    // localStorage.removeItem("Total"); 
    // localStorage.removeItem("burgerType"); 
    // localStorage.removeItem("typeOfBurger");  
}

function Stringify(obj)
{
    return JSON.stringify(obj)
}



