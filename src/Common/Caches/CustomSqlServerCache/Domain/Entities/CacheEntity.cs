using Cache.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomSqlServerCache.Domain.Entities
{
    public class CacheEntity
    {
		private string key;

		public string Key
		{
			get { return CacheItem?.Key ?? key; }
			set { key = value; }
		}

		public CacheItem CacheItem { get; set; }
    }
}
