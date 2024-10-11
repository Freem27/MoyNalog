using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.Text;
using TDV.MoyNalog.Models;
using System.Runtime.CompilerServices;
using System.Net.Http.Headers;
using TDV.MoyNalog.Enums;
using TDV.MoyNalog.Extensions;

[assembly: InternalsVisibleTo("MoyNalog.Tests")]
namespace TDV.MoyNalog
{
    public class MoyNalog
    {
        private const string ApiUrl = "https://lknpd.nalog.ru/api/v1";
        private const string USER_AGENT = "TDV.MoyNalog";
        private readonly string _user;
        private readonly string _password;
        private string _deviceId;
        internal AuthResponse? _auth;
        private HttpClient _httpClient;

        public MoyNalog(string user, string password)
        {
            _deviceId = Guid.NewGuid().ToString().Substring(0, 21);
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json, text/plain, */*");
            _httpClient.DefaultRequestHeaders.Add("accept-language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            _user = user;
            _password = password;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken().Result);
        }

        internal async Task Auth()
        {
            var req = new HttpRequestMessage(HttpMethod.Post, ApiUrl + "/auth/lkfl");
            req.Headers.Add("referrer", "https://lknpd.nalog.ru/");
            req.Headers.Add("referrerPolicy", "strict-origin-when-cross-origin");
            var authReq = new
            {
                username = _user,
                password = _password,
                deviceInfo = DeviceInfo
            };

            _auth = await Post<AuthResponse>(ApiUrl + "/auth/lkfl", authReq);
        }

        internal async Task<string> GetToken()
        {
            if (_auth?.TokenExpireIn > DateTime.UtcNow)
            {
                return _auth.Token;
            }


            if (!string.IsNullOrEmpty(_auth?.RefreshToken))
            {
                var refreshTokenReq = new
                {
                    deviceInfo = DeviceInfo,
                    refreshToken = _auth.RefreshToken
                };
                var resp = await Post<TokenResponse>(ApiUrl + "/auth/token", refreshTokenReq);
                _auth.TokenExpireIn = resp.TokenExpireIn;
                _auth.RefreshTokenExpiresIn = resp.RefreshTokenExpiresIn;
                _auth.Token = resp.Token;
                _auth.RefreshToken = resp.RefreshToken;
            }

            if (_auth == null)
            {
                await Auth();
            }

            return _auth?.Token ?? throw new ApplicationException("Unable to recieve token");
        }

        public async Task<string> AddIncome(Client client, List<ServiceInfo> services, DateTime operationTime, PaymentType paymentType = PaymentType.CASH)
        {
            var req = new AddIncomeRequest
            {
                Client = client,
                Services = services,
                PaymentType = paymentType,
                OperationTime = operationTime,
            };
            req.Assert();
            var resp = await Post<AddIncomeResponse>(ApiUrl + "/income", req);
            return resp.ApprovedReceiptUuid;
        }

        public async Task<CancelIncomeResponse> CancelIncome(string receiptUuid, DateTime requestTime, string comment = "Чек сформирован ошибочно")
        {
            var req = new CancelIncomeRequest
            {
                ReceiptUuid = receiptUuid,
                Comment = comment,
                RequestTime = requestTime,

            };
            return await Post<CancelIncomeResponse>(ApiUrl + "/cancel", req);
        }

        public string GetRecipietUrl(string receiptUuid)
        {
            return $"{ApiUrl}/receipt/{_user}/{receiptUuid}/print";
        }

        public async Task<Income> GetRecipiet(string receiptUuid)
        {
            return await Get<Income>($"{ApiUrl}/receipt/{_user}/{receiptUuid}/json");
        }

        public async Task<GetIncomesResponse> GetIncomes(GetIncomesRequest req)
        {
            var resp = await Get<GetIncomesResponse>(ApiUrl + "/incomes", req);
            return resp;
        }

        public async Task<T> Post<T>(string url, object postData)
        {
            var req = new StringContent(
                JsonSerializer.Serialize(postData, JsonSerializerOptions)
                , Encoding.UTF8, Application.Json
            );
            var resp = await _httpClient.PostAsync(url, req);
            var body = await resp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? throw new ApplicationException("unable to deserialize");
        }

        public async Task<T> Get<T>(string url, object? req = null)
        {
            if (req != null)
            {
                url = url + req.ToQueryString();
            }
            var resp = await _httpClient.GetAsync(url);
            var body = await resp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? throw new ApplicationException("unable to deserialize");
        }

        private object DeviceInfo => new
        {
            appVersion = "1.0.0",
            sourceDeviceId = _deviceId,
            sourceType = "WEB",
            metaDetails = new
            {
                userAgent = USER_AGENT
            }
        };

        private JsonSerializerOptions JsonSerializerOptions
        {
            get
            {
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                options.Converters.Add(new DecimalJsonConverter());
                return options;
            }
        }
    }
}
