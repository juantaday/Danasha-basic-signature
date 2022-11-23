using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADsisVenta.Funtions
{
   public  class SecuritySpamp
    {
		public static string SHA1(string strToHash)
		{
			using (var sha1Obj = new System.Security.Cryptography.SHA1CryptoServiceProvider())
			{
				byte[] bytesToHash = System.Text.Encoding.ASCII.GetBytes(strToHash);
				bytesToHash = (byte[])(sha1Obj.ComputeHash(bytesToHash));
				string strResult = "";
				foreach (byte b in bytesToHash)
				{
					strResult += b.ToString("x2");
				}
				return strResult;
			}

		}



	}
}
