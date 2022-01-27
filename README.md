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

## Test (on AWS EC2)
This project is running on EC2 t2.micro
If you would not like to run on your computer, you can use with [Test Postman Collection](https://github.com/kahramanumut/gbank-microservice-sample/blob/main/_postman/GBankCollection%20-%20EC2%20Test.postman_collection.json)

* [Identity](http://3.71.115.206:5001/.well-known/openid-configuration)
* [Account API](http://3.71.115.206:5003/swagger/index.html)
* [Customer API](http://3.71.115.206:5002/swagger/index.html)
* [Transaction API](http://3.71.115.206:5004/swagger/index.html)

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
