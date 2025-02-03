namespace Server.Common;

//todo: implement resource base instead
public static class Messages
{
    public const string MeetingCreatedSuccessfully = "The meeting has been created successfully";
    public const string MeetingHasOverLap = "The meeting has overlap with other meetings! plz try another time or room and check the meetings list";
    public const string SelectMoreMember = "Select other or more memebers for the meeting";
    public const string EndTimeShouldBiggerThanStart = "End time should bigger than start time";
    public const string RoomRequiredForInPersonMeeting = "Select the room for the in person meeting";
    public const string UrlRequiredForOnlineMeeting = "Url required for the online meetings";
    public const string NotFount = "No item founded!";
}