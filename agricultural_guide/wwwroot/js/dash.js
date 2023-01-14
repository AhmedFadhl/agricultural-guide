const sidemenu=document.querySelector("aside");
const menu_btn=document.querySelector(".menu_btn");
const close_btn=document.querySelector("#close-btn");
const theme_toggler=document.querySelector(".theme-toggler");
const read_more=document.querySelector(".read_more");
const delet=document.querySelector(".delet");
const see_crop=document.querySelector(".see_crop");
const sidebar=document.querySelector(".sidelink");
const cards=document.querySelector(".cards");
const tables=document.querySelector(".tables");
const table_toggler=document.querySelector(".table_toggler");
const sub_menu=document.querySelector(".sub_menu");



menu_btn.addEventListener('click',()=>{
    sidemenu.style.display='block';
})

close_btn.addEventListener('click',()=>{
    sidemenu.style.display='none';
})
theme_toggler.addEventListener('click',()=>{
    document.body.classList.toggle('dart-theme-varibles');


})
read_more.addEventListener('click',()=>{
    toppost.style.overflow="auto";
    read_more.style.display="none";
})
see_crop.addEventListener('click',()=>{
    request_crop.style.overflow='auto';
})
sidelink.addEventListener('click',()=>{
    this.classList.toggle("active");
})
table_toggler.addEventListener('click',()=>{
    tables.style.display='flex';
    cards.style.display='none';
})





function myfunction(){
    var input,filter,table,tr,td,i;
    input=document.getElementById('myinput');
    filter=input.value.toUpperCase();
    table=document.getElementsByClassName('table_toggler');
    tr=document.getElementsByTagName("tr");
        for(i=0;i< tr.length;i++){
      
            td=tr[i].getElementsByTagName("td")[0];
            if(td){
                if(td.innerHTML.toUpperCase().indexOf(filter)>-1){
                    tr[i].style.display="";

                }else{
                   // tr[i].style.display="none";

                   td=tr[i].getElementsByTagName("td")[1];
                   if(td){
                       if(td.innerHTML.toUpperCase().indexOf(filter)>-1){
                           tr[i].style.display="";
                       }else{
                        //   tr[i].style.display="none";

                        td=tr[i].getElementsByTagName("td")[2];
                        if(td){
                            if(td.innerHTML.toUpperCase().indexOf(filter)>-1){
                                tr[i].style.display="";
                            }else{
                              //  tr[i].style.display="none";
                              td=tr[i].getElementsByTagName("td")[3];
                              if(td){
                                  if(td.innerHTML.toUpperCase().indexOf(filter)>-1){
                                      tr[i].style.display="";
                                  }else{
                                      tr[i].style.display="none";
           
           
           
                                  }
                              }
     
     
                            }
                        }

                       }
                   }


                }
            }
           
               
            
        
        

    }
  

}



function lastchat(){
    var input,filter,last_chat,tr,td,i;
    input=document.getElementById('search_chat');
    filter=input.value.toUpperCase();
    last_chat=document.getElementsByClassName('last_chat');
    tr=document.getElementsByTagName("tr");
        for(i=0;i< tr.length;i++){
            td=last_chat.getElementsByTagName("h2");
            if(td){
                if(td.innerHTML.toUpperCase().indexOf(filter)>-1){
                    last_chat[i].style.display="";
                }else{
                    last_chat[i].style.display="none";
                }
            }

    }
  
}




(function () {
    var counter = 0;
    var btn = document.getElementById('btn');
    var form = document.getElementById('form');
    var addInput = function () {
        counter++;
        var input = document.createElement("input");
        input.id = 'input-' + counter;
        input.type = 'text';
        input.name = 'name';
        input.placeholder = 'Input number ' + counter;
        form.appendChild(input);
    };
    btn.addEventListener('click', function () {
        addInput();
    }.bind(this));
})();


function backtocrop() {
    
}