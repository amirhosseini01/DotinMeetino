using Microsoft.EntityFrameworkCore.Migrations;
using Server.Modules.Meeting.Enums;
using Server.Modules.Meeting.Models;
using Server.Modules.User.Models;

namespace Server.Data;

public class Seeder
{
    public static void Seed1(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: nameof(DataBaseContext.Rooms),
            columns: new[] { nameof(Room.Id), nameof(Room.Title) },
            values: new object[,]
            {
                { 1, "اتاق جلسات واحد تامین مالی" },
                { 2, "اتاق جلسات منابع انسانی" },
            });
            
        migrationBuilder.InsertData(
            table: nameof(DataBaseContext.Users),
            columns: new[] { nameof(Room.Id), nameof(User.UserName), nameof(User.FirstName), nameof(User.LastName), nameof(User.PhoneNumber), nameof(User.Email) },
            values: new object[,]
            {
                { 1, "s.arham", "ساسان", "ارحام", "09121234567", "s.arham@dotin.ir" },
                { 2, "amircsharp7", "امیر", "حسینی", "09198015606", "amircsharp7@dotin.ir" },
                { 3, "test", "تست", "تستی 0", "09191234567", "test@dotin.ir" },
                { 4, "test", "تست", "تستی 1", "09197234567", "test1@dotin.ir" },
                { 5, "test", "تست", "تستی 2", "09195234567", "test2@dotin.ir" },
            });
            
        migrationBuilder.InsertData(
            table: nameof(DataBaseContext.Meetings),
            columns: new[] { nameof(Meeting.RoomId), nameof(Meeting.Status), nameof(Meeting.Type), nameof(Meeting.Title),  nameof(Meeting.StartDateTime),  nameof(Meeting.EndDateTime),},
            values: new object[,]
            {
                { 1, nameof(MeetingStatus.Active), nameof(MeetingType.InPerson), "جلسه تحویل پروژه تست", new DateTime(year: 2025, month: 2, day: 4, hour: 10, minute:0, second: 0), new DateTime(year: 2025, month: 2, day: 4, hour: 11, minute:0, second: 0) }
            });
    }
}