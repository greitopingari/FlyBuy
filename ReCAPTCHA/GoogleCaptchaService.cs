using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FlyBuy.ReCAPTCHA
{
    public class GoogleCaptchaService
    {

        private readonly IOptionsMonitor<ReCAPTCHASettings> _config;

        public GoogleCaptchaService(IOptionsMonitor<ReCAPTCHASettings> config)
        {
            _config = config;
        }

        public async Task<bool> VerifyToken(string token)
        {
            try
            {
                var url = $"https://www.google.com/recaptcha/api/siteverify?secret={_config.CurrentValue.ReCAPTCHA_Secret_Key}&response={token}";
                
                using (var client = new HttpClient())
                {
                    var httpResult = await client.GetAsync(url);
                    if(httpResult.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        return false;
                    }

                    var response = await httpResult.Content.ReadAsStringAsync();
                    var googleResult = JsonConvert.DeserializeObject<GoogleCaptchaResponse>(response);

                    return googleResult.Success && googleResult.Score >= 0.5;
                }
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
