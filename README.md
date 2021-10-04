# Library Project Example 📚

Project applying CQRS and DDD concepts learned on [Hands-On Domain-Driven Design with .NET Core](https://www.packtpub.com/product/hands-on-domain-driven-design-with-net-core/9781788834094)

## 🧪 Technologies
<ul>
  <li>ASP .NET Core 6</li>
  <li>Keycloak</li>
  <li>PostgreSQL </li>
  <li>Docker </li>
</ul>

## 🚀 How to launch the application?
1. Run ***docker-compose up*** in root folder
2. Go to http://localhost:8080/auth/ to acess Keycloak
3. Enter 'Administration Console' and access with 
  - Username: admin
  - Password: secret
5. Go to 'Users'and click on 'Add user' button:
![Showing where to add an user](/resources/1.PNG "Add User")
6. Create an User with the flag 'Email Verified' enabled
![Create user](/resources/2.PNG "Creating User")
7. Go to 'Credentials' tab and set the user's password and disable the flag 'Temporary'
![Create user's password](/resources/3.PNG "Creating User's password")
8. Make POST request to http://localhost:8080/auth/realms/library/protocol/openid-connect/token to generate the access token: 
(You can use the ./Library.http file replacing the username and password variables to make the request)
![Generating Token](/resources/4.PNG "Generating Token")
9. Get the access_token and go to the Library API (http://localhost:5000/index.html) and add it to 'Authorize' button:
![Authentication Request](/resources/5.PNG "Authentication Request")
10. Now you can make any request!
![Successful Request](/resources/6.PNG "Successful Request")

## 🧵 Libs 
<ul>
  <li>EF Core 6</li>
  <li>Dapper</li>
</ul>

## 🔹 Todo 
- [ ] Integration Tests
- [ ] Book stock context
  - [ ] Register new books by donation   
  - [ ] Register new books buy in batch (excel, json files, api)
- [ ] Locators registration and penalty for return book late
