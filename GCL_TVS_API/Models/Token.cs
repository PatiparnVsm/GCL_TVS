namespace GCL_TVS_API.Models
{
    public class Token
    {
        public class RequestToken
        {
            public string SystemID { get; set; }
        }
        public class ResponseToken
        {
            public string access_token { get; set; }
            public string token_tpye { get; set; } = "Bearer";
            public long expires_in { get; set; }
        }

        public class AuthenByUser
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class ResponseTokenByUser
        {
            public string access_token { get; set; }
            public string token_tpye { get; set; } = "Bearer";
            public long expires_in { get; set; }
            public string UserID { get; set; }
            public string UserType { get; set; }
            public string FullName { get; set; }
        }

        public class ErrorAuthen
        {
            public StatusError status { get; set; }
        }

        public class StatusError
        {
            public string code { get; set; } = "99";
            public string message { get; set; } = "System Error";
        }
    }
}