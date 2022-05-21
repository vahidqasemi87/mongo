using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AppMongoRead.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public virtual string AddedBy { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string NationCode { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public virtual DateTime? CreateDate { get; set; } = DateTime.Now;
        [BsonRepresentation(BsonType.DateTime)]
        public virtual DateTime? UpdateDate { get; set; } = DateTime.Now;
        [BsonRepresentation(BsonType.DateTime)]
        public virtual DateTime? DeleteDate { get; set; } = DateTime.Now;
        [BsonRepresentation(BsonType.String)]
        public virtual string UserName { get; set; }
		public string _LoweCaseUserName { get; set; }
		public string _LowerCaseUserName;
		[BsonRepresentation(BsonType.String)]
		[BsonIgnoreIfNull]
		public virtual string LowerCaseUserName
		{
			get
			{
				if (_LowerCaseUserName == null)
					return NationCode;
				else return NationCode;  //Guid.NewGuid().ToString();
			}
			set
			{
				_LowerCaseUserName = value;
			}
		}
		[BsonRepresentation(BsonType.String)]
        public string EmailAddress { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string LowerCaseEmailAddress { get; set; }
        [BsonRepresentation(BsonType.Boolean)]
        public virtual Boolean EmailAddressConfirmed { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string PhoneNumber { get; set; }
        [BsonRepresentation(BsonType.Boolean)]
        public virtual Boolean PhoneNumberConfirmed { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string PasswordHash { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string SecurityStamp { get; set; }
        //[BsonRepresentation(BsonType.Int32)]
        [BsonIgnoreIfNull]
        public DateTimeOffset? LockoutEndDateUtc { get; set; }
        [BsonRepresentation(BsonType.Boolean)]
        public bool LockoutEnabled { get; set; }
        [BsonRepresentation(BsonType.Int32)]
        public int AccessFailedCount { get; set; }
        [BsonRepresentation(BsonType.Boolean)]
        public bool TwoFactorEnabled { get; set; }
        [BsonIgnoreIfNull]
        public virtual ICollection<RoleSql> Roles { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string FirstName { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string LastName { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string FatherName { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string IdSeries { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string IdSerial { get; set; }
        [BsonRepresentation(BsonType.Int32)]
        public int BirthDate { get; set; }
        [BsonRepresentation(BsonType.Boolean)]
        public bool? SabteAhvalConfirmed { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string AllowedIP { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string LastIP { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? LastLoginDate { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? LastPasswordChangedDate { get; set; }
        [BsonRepresentation(BsonType.Boolean)]
        public bool IsLocked { get; set; }
        //  public List<ADDRESSSql> Addresses { get; set; }
        ////  [NotMapped]
        //public string Gender { get; set; }
        ////   public ICollection<ProfileFieldParamsSql> Gender { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string Email { get; set; }
        //////    [NotMapped]
        ////    //public ICollection<ProfileFieldParamsSql> Job { get; set; }
        ////    public string Job { get; set; }
        //// //   [NotMapped]
        ////    //public ICollection<ProfileFieldParamsSql> EduGrade { get; set; }
        ////    public string EduGrade { get; set; }
        //////    [NotMapped]
        ////    //public ICollection<ProfileFieldParamsSql> EduField { get; set; }
        ////    public string EduField { get; set; }
        ////   // [NotMapped]
        ////    //public ICollection<ProfileFieldParamsSql> MarriageStatus { get; set; }
        ////    public string MarriageStatus { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Address { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string PostalCode { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string LineTel { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string MobileTel { get; set; }
        [BsonRepresentation(BsonType.Boolean)]
        public bool IsCoworker { get; set; }
        [BsonRepresentation(BsonType.Boolean)]
        public bool IsMobileBankActive { get; set; }
        [BsonRepresentation(BsonType.Boolean)]
        public bool IsInternetBankActive { get; set; }
        [BsonRepresentation(BsonType.Int32)]
        public int LastCalculatedScoreDate { get; set; } = 0;
        [BsonRepresentation(BsonType.Int32)]
        public int Scores { get; set; } = 0;
        [BsonIgnoreIfNull]
        public ICollection<ClaimsSql> Claims { get; set; }
        [BsonIgnoreIfNull]
        public ICollection<LoginsSql> Logins { get; set; }
        [BsonIgnoreIfNull]
        public ICollection<AddressesSql> Addresses { get; set; }
        [BsonIgnoreIfNull]
        //public ICollection<GenderSql> Gender { get; set; }
        public ProfileFieldParams Gender { get; set; }
        public ProfileFieldParams Job { get; set; }
        public ProfileFieldParams EduGrade { get; set; }

        public ProfileFieldParams EduField { get; set; }

        public ProfileFieldParams MarriageStatus { get; set; }
        public ProfileFieldParams FavouriteWorks { get; set; }
        public List<ProfileFieldParams> FavouriteProducts { get; set; }
        public bool? IsConfirmContract { get; set; } = false;
        public DateTime? ConfirmContractDate { get; set; }
    }
    public class RoleSql
    {
        public RoleSql()
        {
            RoleId = new ObjectId();
        }
        [BsonIgnoreIfNull]
        public ObjectId RoleId
        {
            get; set;
        }
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfNull]
        public string Name { get; set; }
    }
    public class ClaimsSql
    {
        public ClaimsSql()
        {
            RoleId = new ObjectId();
        }
        [BsonIgnoreIfNull]
        public ObjectId RoleId
        {
            get; set;
        }
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfNull]
        public string Name { get; set; }
    }
    public class LoginsSql
    {
        public LoginsSql()
        {
            RoleId = new ObjectId();
        }
        [BsonIgnoreIfNull]
        public ObjectId RoleId
        {
            get; set;
        }
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfNull]
        public string Name { get; set; }
    }
    public class AddressesSql
    {
        public AddressesSql()
        {
            RoleId = new ObjectId();
        }
        [BsonIgnoreIfNull]
        public ObjectId RoleId
        {
            get; set;
        }
    }
    public class GenderSql
    {
        public GenderSql()
        {
            Id = new ObjectId();
        }
        [BsonIgnoreIfNull]
        [BsonId]
        public ObjectId Id
        {
            get; set;
        }
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.Int32)]
        public int? SortOrder { get; set; }
        [BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.Int32)]
        public int? Type { get; set; }
    }
    public class ProfileFieldParams
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        //[BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        //[BsonRepresentation(BsonType.Int32)]
        public int SortOrder { get; set; }

        public ProfileFieldParamEnum Type { get; set; }

    }
    public enum ProfileFieldParamEnum : byte
    {
        [Description("شغل")]
        Job = 1,
        [Description("سطح تحصیلات")]
        EduGrade = 2,
        [Description("نحوه آشنایی با باشگاه مشتریان")]
        NotifyMethod = 3,
        [Description("جنسیت")]
        Gender = 4,
        [Description("وضعیت تاهل")]
        MarriageStatus = 5,
        [Description("خدمات بانکی")]
        BankProducts = 6,
        [Description("رشته تحصیلی")]
        EduField = 7,
        [Description("سلایق و علایق")]
        Favourite = 8,

    }
}
