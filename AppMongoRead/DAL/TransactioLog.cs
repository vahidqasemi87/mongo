using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMongoRead.DAL
{
	public class TransactioLog
	{
		public string _id { get; set; }
		public int? ARCHIVE_NO { get; set; }
		public long? TRANSACTION_LOG_ID { get; set; }
		public int? TRANSACTION_TYPE { get; set; }
		public int? CHANNEL_ID { get; set; }
		public int? EB_SERVICE_ID { get; set; }
		public int? TRANSACTION_STATE_ID { get; set; }
		public int? DUPLICATE { get; set; }
		public int? CSP_CHANNEL_ID { get; set; }
		public string CSP_USERNAME { get; set; }
		public string USERNAME { get; set; }
		public string STATUS_CODE { get; set; }
		public string ACCOUNT_NO { get; set; }
		public string SERVER_CODE { get; set; }
		public string MESSAGE_SEQUENCE_ID { get; set; }
		public DateTime? LOG_TIME { get; set; }
		public string SERVER_EXCEPTION { get; set; }
		public string DESCRIPTION { get; set; }
		public string DOC_NO { get; set; }
		public string TERMINAL_TYPE { get; set; }
		public string INTER_BANK { get; set; }
		public long? AMOUNT { get; set; }
		public int? TERMINAL_ID { get; set; }
		public DateTime? CLIENT_DATE { get; set; }
		public string EXTERNAL_SEQUENCE_ID { get; set; }
		public string ORIGINAL_SEQUENCE_ID { get; set; }
		public DateTime? CREATIONDATE { get; set; }
		public string NATIONAL_CODE { get; set; }
		public int? USER_ID { get; set; }
		public string CustomerPlanRuleId { get; set; }
		public int? LogDateYear { get; set; }
		public int? LogDateMonth { get; set; }
		public int? LogDateDay { get; set; }
		public int? LogDateTime { get; set; }


	}
	//public class LoanPaymentAddRequest
	//{
		
	//}
	//public class Payment
	//{
	//	public int Account { get; set; }
	//	public int Amount { get; set; }
	//}
}
