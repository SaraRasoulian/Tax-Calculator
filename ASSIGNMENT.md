# Congestion Tax Calculator

## The Scenario

Your junior colleague started working on an application for calculating congestion tax fees for vehicles within the Gothenburg area and unfortunately, said colleague has gone on parental leave. The uncompleted project of your colleague attached to this file. There are no syntax errors in the attached code, but there seem to be bugs in the calculation, and architecture as well.
Looking around your colleagues desk, you find a list of dates scribbled on a post-it. Maybe they'll come in handy.

 "2013-01-14 21:00:00"

"2013-01-15 21:00:00"

"2013-02-07 06:23:27"

"2013-02-07 15:27:00"

"2013-02-08 06:27:00"

"2013-02-08 06:20:27"

"2013-02-08 14:35:00"

"2013-02-08 15:29:00"

"2013-02-08 15:47:00"

"2013-02-08 16:01:00"

"2013-02-08 16:48:00"

"2013-02-08 17:49:00"

"2013-02-08 18:29:00"

"2013-02-08 18:35:00"

"2013-03-26 14:25:00"

"2013-03-28 14:07:27"

## Assignment

**We want you to think through and refactor the code to a correct and clean one**

You use relational databases (like sqlite, sql server, sqlite in memory and etc.)

You may limit the scope to the year 2013.

## Congestion tax rules in Gothenburg

A congestion tax is charged during fixed hours for vehicles driving into and out of Gothenburg.

The maximum amount per day and vehicle is 60 SEK.

The tax is not charged on weekends (Saturdays and Sundays), public holidays, days before a public holiday and during the month of July.

### Hours and amounts for congestion tax in Gothenburg

| Time        | Amount |
| ----------- | :----: |
| 06:00–06:29 | SEK 8  |
| 06:30–06:59 | SEK 13 |
| 07:00–07:59 | SEK 18 |
| 08:00–08:29 | SEK 13 |
| 08:30–14:59 | SEK 8  |
| 15:00–15:29 | SEK 13 |
| 15:30–16:59 | SEK 18 |
| 17:00–17:59 | SEK 13 |
| 18:00–18:29 | SEK 8  |
| 18:30–05:59 | SEK 0  |

### The single charge rule

A single charge rule applies in Gothenburg. Under this rule, a vehicle that passes several tolling stations within 60 minutes is only taxed once. The amount that must be paid is the highest one.

### Tax Exempt vehicles

- Emergency vehicles
- Busses
- Diplomat vehicles
- Motorcycles
- Military vehicles
- Foreign vehicles

## Bonus Scenario

Just as you finished coding, your manager shows up and tells you that the same application should be used in other cities with different tax rules. These tax rules need to be handled as content outside the application because different content editors for different cities will be in charge of keeping the parameters up to date.

Move the parameters used by the application to an outside data store of your own choice to be read during runtime by the application.

Please don't cheat in your code with this link!
https://github.com/volvo-cars/congestion-tax-calculator

