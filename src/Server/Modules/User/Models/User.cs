using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Server.Data.Base;
using Server.Data.Models;
using Server.Modules.Meeting.Models;

namespace Server.Modules.User.Models;

public class User: BaseEntity
{
    [StringLength(DatabaseHelper.TitlesStandardLength)]
    public required string UserName { get; set; }
    
    [StringLength(DatabaseHelper.TitlesStandardLength)]
    public required string FirstName { get; set; }
    
    [StringLength(DatabaseHelper.TitlesStandardLength)]
    public required string LastName { get; set; }
    
    [DataType(DataType.PhoneNumber)]
    [StringLength(DatabaseHelper.PhoneNumbersStandardLength)]
    public required string PhoneNumber { get; set; }
    
    [DataType(DataType.EmailAddress)]
    [StringLength(DatabaseHelper.EmailsStandardLength)]
    public required string Email { get; set; }

    [NotMapped] 
    public string FullName => $"{FirstName} {LastName}";
    
    public virtual ICollection<MeetingMember>? Meetings { get; set; }
}