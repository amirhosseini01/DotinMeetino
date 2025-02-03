# DotinMeetino
Interview project, simple solution for meeting manager system!

| task                                                                                                                                                                                                                     | status | 
|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|:-------|
| add meeting with different persons, <br/> with specific duration<br/> and specific room                                                                                                                                  | ✅      |
| specify title for each meeting                                                                                                                                                                                           | ✅      |
| Send email 2 hour before meeting start  <br/> [* sender functionality](src/Server/BackgroundJob/Notification/NotificationSender.cs)<br/> [* background job hangfire](src/Server/BackgroundJob/HangfireHelper.cs)         | ✅      |
| add report for each meeting    <br/> [* implementation in services](src/Server/Modules/Meeting/Services/MeetingServices.cs#L137)<br/> [* api controller/action](src/Server/Modules/Meeting/Api/MeetingController.cs#L38) | ✅      |
| the meetings should not have any overlap with each others [* chain of related validation](src/Server/Modules/Meeting/Services/MeetingServices.cs#L178)                                                                   | ✅      |
| cancel option for persons [* cancel implementation](src/Server/Modules/Meeting/Services/MeetingServices.cs#L121)                                                                                                         | ✅      |
| 🥈 silver challenge: edit session                                                                                                                                                                                        | ✅      |
| 🥇 golden challenge: smart meeting time recognition by system<br/> and using elapsed time and other data for suggest best time<br/> for meeting                                                                          | ✅      |
| summary: all changes passed!                                                                                                                                                                                             | ✅      |

### next plans:

- write unit tests
- refactor code and using suitable patterns for this scenario and project scale

### requirement for running this project on your local machine

- sql server instance and make sure the connection string in the `appsetting.Development.json` is correct
- dotnet 9 sdk