using entities.models;

namespace entities.interfaces
{
    public class AuthenticationResult : TokenModel
    {
        public AuthenticationResult()
        {
            this.success = false;
            this.errors = new List<string>();
        }

        public bool success { get; set; }
        public IEnumerable<string> errors { get; set; }
    }
}