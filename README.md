# GBank Microservices

The Identity Server protecting APIs with [Client Credentials Flow](https://auth0.com/docs/flows/client-credentials-flow).

- IdentityServer (Duende V5) ([Identity Provider](https://duendesoftware.com/products/identityserver))
- Account API
- Customer API
- Transaction API

### Instructions

* All data store 'in memory'
* Run 'docker-compose up'
* You can use Swagger or Postman, I've uploaded Postman collection which configurated for Docker 
* There is a AWS MQ configuration, I'm going to delete after. If you use after deleting,  you should change this config.

##### Tech Stack & Libraries

* .Net 5
* Identity Server (Duende)
* EF Core with InMemory
* Message Broker - RabbitMq
* FluentValidation
* Mapster
* Swagger
* Docker

##### Scheme
![Scheme](https://github.com/kahramanumut/gbank-microservice-sample/blob/main/_images/Scheme.jpeg?raw=true)
