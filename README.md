# Congestion Tax Calculator
The application calculates congestion tax fees for vehicles within the Gothenburg area.


Solution implemented using ASP.NET Core Web API (.NET 8) with Visual Studio IDE.

The endpoints can be tested using postman or swagger.

- The application calculates the tax for the Gothenburg area only.
- All tax rules are hardcoded.
- The application is limited to the year 2013.


### Avaliable endpoint:


following endpoint gets `vehicle type` and `passess dates` as inputs and returns `tax amount`

##### Endpoint:
```
Get  https://localhost:7233/api/tax/calculate
```

##### Input from body:
```
Content-Type: application/json
{
    "VehicleType": "Car",
    "PassesDates": [
      "2013-05-13T06:15:00",
      "2013-05-13T08:45:00"
    ]
}
```


##### Technical Details:
- Solution implemented with TDD (Test-Driven Development) in practice.
- Solution implemented using SOLID, YAGNI, KISS and DRY principles.
- Commits serve as history to see the development of the solution.
