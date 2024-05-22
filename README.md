## Congestion Tax Calculator
The application calculates congestion tax fees for vehicles.



## Avaliable endpoints

The endpoints can be tested using postman or swagger.

following endpoint gets `city name`, `vehicle type` and `passess dates` as inputs and returns `tax amount`

##### Endpoint:
```
Post    http://localhost:5000/api/tax/calculate
```

##### Input from body:
```
Content-Type: application/json
{
    "CityName": "Gothenburg",
    "VehicleType": "Car",
    "PassesDates": [
      "2013-05-13T06:15:00",
      "2013-05-13T08:45:00"
    ]
}
```


## Technical details
  -	ASP.NET Core Web API -v8
  - Entity Framework Core -v8
  - DDD (Domain-Driven Design)
  - TDD (Test-Driven Development)
  - Clean Architecture
  - Clean Code
  - Repository Service Pattern
  - PostgreSQL -v15
  - Testcontainers for integration testing
  - Fluent Assertions
  - Docker
  - Visual Studio 2022 -v17

### Database design



![Database Design](https://github.com/SaraRasoulian/Congestion-Tax-Calculator/assets/51083712/5000d8a4-56b5-4682-82a3-a7851215c6a0)




## Get started

#### 1. Clone the repository

```
git clone https://github.com/SaraRasoulian/Congestion-Tax-Calculator.git
```
#### 2. Start with docker compose

Make sure [docker](https://docs.docker.com/get-docker/) is installed on your machine.

Run the following command in project directory:

```
docker-compose up -d
```

Docker compose in this project includes 3 services: 

- __Web API application__ will be running and listening at `http://localhost:5000`

- __Postgres database__ will be listening at `http://localhost:5433`

- __PgAdmin4 web interface__ will be listening at `http://localhost:8080`


To apply your modified code, you can add build option:

```
  docker-compose up -d --build
```

To stop and remove all containers, use the following command:

```
  docker-compose down
```


#### 3. Run the migrations

Open `CongestionTaxCalculator.sln` file in visual studio, then in package manager console tab, run:

```
update-database
```

This command will generate the database schema in postgres container.

