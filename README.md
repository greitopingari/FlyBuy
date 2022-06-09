# FlyBuy
This project is an upgraded version of previous one 
https://github.com/KlevisMema/FlyBuy  
made with asp.net mvc. The technology used for this project is asp.net core 6.0, it has all the functionalities that the previous project has but it has more featues such as:

- The user now can add a product in the wish list
User doesn't need to be authenticated in order to interact with the page for ex. to buy or see clothes

- User can use external login service like Google , LinkedIn and Facebook
When the user register for the first time he needs to confirm his email adress this was achieved by using SendGrid service

- Register page has ReCaptcha supprot client side and server side this was made using google recaptcha service

- Error handling, this means that when a user tries to access a resource that it does not exists it will be shown a 404 message

- PayPal integration when he checks out

- Admin besides the functionalities that derives from previous project, now it can manage roles like add a role, delete e role or delete e user

- Admin can see number of orders made in different months displayed in a graph, this was made using ChartJs

- Overall page is more user friendly
