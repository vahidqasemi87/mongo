using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMongoRead.Models
{
	public class UserExport
	{
		public long Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string LineTel { get; set; }
		public string MobileTel { get; set; }
		public string MeliCode { get; set; }
	}
}
