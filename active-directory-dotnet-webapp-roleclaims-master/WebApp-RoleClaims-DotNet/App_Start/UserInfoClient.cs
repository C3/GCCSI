using System;

namespace GCCSI_CO2RE
{
    internal class UserInfoClient
    {
        private object accessToken;
        private Uri uri;

        public UserInfoClient(Uri uri, object accessToken)
        {
            this.uri = uri;
            this.accessToken = accessToken;
        }
    }
}