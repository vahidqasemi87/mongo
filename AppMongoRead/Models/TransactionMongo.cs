using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppMongoRead.Models
{
    public class TransactionMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CustomerPlanRuleId { get; set; }

        [BsonRepresentation(BsonType.Int64)]
        public long Score { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Description { get; set; }
        public int Type { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string RuleId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string PlanId { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string NationId { get; set; }

        [BsonDateTimeOptions(DateOnly = false, Kind = DateTimeKind.Local, Representation = BsonType.DateTime)]
        public DateTime CreationOn { get; set; }

        [BsonRepresentation(BsonType.Int32)]
        public int CreationOnYear { get; set; }

        [BsonRepresentation(BsonType.Int32)]
        public int CreationOnMonth { get; set; }

        [BsonRepresentation(BsonType.Int32)]
        public int CreationOnDay { get; set; }

        [BsonDateTimeOptions(DateOnly = false, Kind = DateTimeKind.Local, Representation = BsonType.DateTime)]
        public DateTime TransactionDate { get; set; }

        [BsonRepresentation(BsonType.Int32)]
        public int TransactionDateYear { get; set; }

        [BsonRepresentation(BsonType.Int32)]
        public int TransactionDateMonth { get; set; }

        [BsonRepresentation(BsonType.Int32)]
        public int TransactionDateDay { get; set; }

        [BsonRepresentation(BsonType.Boolean)]
        public bool Spend { get; set; }
    }
}
