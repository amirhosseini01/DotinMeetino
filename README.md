# DotinMeetino
Interview project, simple solution for meeting manager system!

| task                                                                                                                                            | status | 
|-------------------------------------------------------------------------------------------------------------------------------------------------|:-------|
| add meeting with different persons, <br/> with specific duration<br/> and specific room                                                         | ✅      |
| specify title for each meeting                                                                                                                  | ✅      |
| Send email 2 hour before meeting start                                                                                                          | ✅      |
| add report for each meeting                                                                                                                     | ✅      |
| the meetings should not have any overlap with each others                                                                                       | ✅      |
| cancel option for persons                                                                                                                       | ✅      |
| 🥈 silver challenge: edit session                                                                                                               | ✅      |
| 🥇 golden challenge: smart meeting time recognition by system<br/> and using elapsed time and other data for suggest best time<br/> for meeting | ✅      |
| summary: all changes passed!                                                                                                                    | ✅      |

### next plans:

- write unit tests
- refactor code and using suitable patterns for this scenario and project scale

### requirement for running this project on your local machine

- sql server instance and make sure the connection string in the `appsetting.Development.json` is correct
- dotnet 9 sdk