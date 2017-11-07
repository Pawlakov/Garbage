using System;
using System.IO;
using System.Net;

class Program
{
	private static String Experiment()
	{
		//setup some variables
		String display = "";
		String enable_profile_selector = "";
		String isprivate = "";
		String legacy_return = "0";
		String profile_selector_ids = "";
		String return_session = "";
		String skip_api_login = "";
		String signed_next = "";
		String trynum = "1";
		String u_0_8 = "";
		String u_0_9 = "";
		String lgnrnd = "120707_R2xy";
		String lgnjs = "n";
		String email = "pmatusek1@wp.pl";
		String pass = "sportova1313";
		//setup some variables end
		String result = "";
		String strPost =
			"display=" + display +
			"&enable_profile_selector=" + enable_profile_selector +
			"&isprivate=" + isprivate +
			"&legacy_return=" + legacy_return +
			"&profile_selector_ids=" + profile_selector_ids +
			"&return_session=" + return_session +
			"&skip_api_login=" + skip_api_login +
			"&signed_next=" + signed_next +
			"&trynum=" + trynum +
			"&u_0_8=" + u_0_8 +
			"&u_0_9=" + u_0_9 +
			"&lgnrnd=" + lgnrnd +
			"&lgnjs=" + lgnjs +
			"&email=" + email +
			"&pass=" + pass;
		StreamWriter myWriter = null;
		HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create("https://www.facebook.com/login.php?login_attempt=1&lwv=100");
		objRequest.Method = "POST";
		objRequest.ContentLength = strPost.Length;
		objRequest.ContentType = "application/x-www-form-urlencoded";
		try
		{
			myWriter = new StreamWriter(objRequest.GetRequestStream());
			myWriter.Write(strPost);
		}
		catch (Exception e)
		{
			return e.Message;
		}
		finally
		{
			myWriter.Close();
		}
		HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
		using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
		{
			result = sr.ReadToEnd();
			// Close and clean up the StreamReader
			sr.Close();
		}
		return result;
	}

	static void Main(string[] args)
	{
		Console.WriteLine(ExperimentOther());
		Console.ReadKey();
	}
}