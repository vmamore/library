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
2. Run the script to create a locator and a librarian in keycloak: ***.\scripts\create-users.ps1***
3. To enter as a locator use:
   - username: bob.aerso
   - password: @test123
4. To enter as a librarian use:
   - username: john.hiake
   - password: @test123

## 🧵 Libs 
<ul>
  <li>EF Core 6</li>
  <li>Dapper</li>
</ul>

## 🔹 Todo 
- [x] Register new locators
- [x] Show error messages when trying to create new user or book
- [ ] Style Book cards in Book Catalog to have the same size
- [x] Locators registration and penalty for return book late

