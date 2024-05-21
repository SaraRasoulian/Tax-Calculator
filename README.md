## Congestion Tax Calculator
The application calculates congestion tax fees for vehicles.



## Avaliable endpoints

The endpoints can be tested using postman or swagger.

following endpoint gets `city name`, `vehicle type` and `passess dates` as inputs and returns `tax amount`

##### Endpoint:
```
Get  https://localhost:7233/api/tax/calculate
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
  - Clean Architecture
  - Clean Code
  - Repository Service Pattern
  - PostgreSQL
  - Database built via Entity framework migrations (code-first approach)
  - Visual Studio 2022 -v17
