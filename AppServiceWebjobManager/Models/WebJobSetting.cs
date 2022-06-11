using System.Text;

namespace AppServiceWebjobManager.Models
{
    /// <summary>
    /// WebJobSetting
    /// </summary>
    public class WebJobSetting
    {
        /// <summary>
        /// Name
        /// </summary>

        public string Name { get; set; }
        /// <summary>
        /// Kudo Url
        /// </summary>
        public string KudoUrl { get; set; }
        /// <summary>
        /// Account
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Basic Authorization
        /// </summary>
        public string BasicAuthorization => Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(Account + ":" + Password));
    }
}
