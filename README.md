# DotinMeetino
Interview project, simple solution for meeting manager system!

| task                                                                                                                                                                                                                                                                                                      | status | 
|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|:-------|
| add meeting with different persons, <br/> with specific duration<br/> and specific room                                                                                                                                                                                                                   | ✅      |
| specify title for each meeting                                                                                                                                                                                                                                                                            | ✅      |
| Send email 2 hour before meeting start  <br/> [* sender functionality](src/Server/BackgroundJob/Notification/NotificationSender.cs)<br/> [* background job hangfire](src/Server/BackgroundJob/HangfireHelper.cs)                                                                                          | ✅      |
| add report for each meeting    <br/> [* implementation in services](src/Server/Modules/Meeting/Services/MeetingServices.cs#L137)<br/> [* api controller/action](src/Server/Modules/Meeting/Api/MeetingController.cs#L38)                                                                                  | ✅      |
| the meetings should not have any overlap with each others [* chain of related validation](src/Server/Modules/Meeting/Services/MeetingServices.cs#L178)                                                                                                                                                    | ✅      |
| cancel option for persons [* cancel implementation](src/Server/Modules/Meeting/Services/MeetingServices.cs#L121)                                                                                                                                                                                          | ✅      |
| 🥈 silver challenge: edit session<br/>  [* edit implementation](src/Server/Modules/Meeting/Services/MeetingServices.cs#L84)                                                                                                                                                                                    | ✅      |
| 🥇 golden challenge: smart meeting time recognition by system<br/> and using elapsed time and other data for suggest best time<br/> for meeting<br/> [* functionality](src/Server/Modules/Meeting/Services/MeetingServices.cs#L204) [* usage](src/Server/Modules/Meeting/Services/MeetingServices.cs#L42) | ✅      |
| summary: all changes passed!                                                                                                                                                                                                                                                                              | ✅      |

### next plans:

- write unit tests
- refactor code and using suitable patterns for this scenario and project scale

### requirement for running this project on your local machine

- sql server instance and make sure the connection string in the `appsetting.Development.json` is correct
- dotnet 9 sdk

### how to call API?

1. using `.http` files in [this](src/Server/HttpFiles) folders
2. using api document generator with `http://localhost:5158/scalar/v1` [path](http://localhost:5158/scalar/v1)
3. `postman` or `insomnia` import soon :)

### extra features?

- auto maper with source generator using nuget package called mapperly
- meeting with different types `in-person` and `online`. in the in-person we should specify the room but in the online we should specify the skype or meet link.
- seed data [here](src/Server/Data/Seeder.cs)
- `DateTimeOffset` instead of `DateTime` because we have international users! :) and we have to support different time zones

### images

- #### success add
![some text](https://github.com/amirhosseini01/DotinMeetino/images/1successadd.png)