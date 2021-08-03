# Library Example with Event Sourcing 📚

Project applying CQRS, DDD and Event Sourcing concepts learned on [Hands-On Domain-Driven Design with .NET Core](https://www.packtpub.com/product/hands-on-domain-driven-design-with-net-core/9781788834094)

## 🧪 Tecnologies
<ul>
  <li>ASP .NET Core 6</li>
  <li>PostgreSQL </li>
  <li>Docker </li>
</ul>

## 🧵 Libs 
<ul>
  <li>EF Core 6</li>
  <li>Dapper</li>
</ul>

## 🔹 Todo 
- [ ] Use [Marten](https://martendb.io/) or [EventStore](https://www.eventstore.com/) to store [Events](https://github.com/vmamore/library/blob/main/src/Library.Api/Books/Events.cs)
- [ ] Integration Tests
- [ ] Book stock context
  - [ ] Register new books by donation   
  - [ ] Register new books buy in batch (excel, json files, api)
