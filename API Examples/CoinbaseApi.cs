using Backend.Api;
using Backend.Credentials;
using Models.Backend;
using Models.Backend.Coinbase;
using Models.Backend.Coinbase.ApiRequests;
using Models.Enums;
using Models.Enums.Coinbase;
using Models.ModelExtensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Coinbase
{
    public class CoinbaseApi : IExternalIntegration
    {
        private static CoinbaseApiHelper _Helper = new CoinbaseApiHelper();
        private CoinbaseApiCredentials _Credentials;
        private bool _SandboxMode = false;
        
        public CoinbaseApi(bool sandboxMode = false)
        {
            _SandboxMode = sandboxMode;
            GetCredentials();
        }

        // Account Apis
        #region Accounts
        public async Task<MethodResponse<IEnumerable<Account>>> GetAllAccounts()
        {
            var response = new MethodResponse<IEnumerable<Account>>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetAllAccountsForProfile, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<IEnumerable<Account>>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<Account>> GetAccountById(string account_id)
        {
            var response = new MethodResponse<Account>();
            if (account_id.IsNullOrEmpty() == false)
            {
                using (var api = new ApiClient())
                {
                    var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetAccountById, _SandboxMode).WithId(account_id));
                    var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                    response = await api.GetAsync<Account>(uri.ToString(), headers);
                }
            }
            else
            {
                response.Fail("No id was given.");
            }
            return response;
        }
        public async Task<MethodResponse<IEnumerable<AccountHold>>> GetAccountHolds(string account_id)
        {
            var response = new MethodResponse<IEnumerable<AccountHold>>();
            if (account_id.IsNullOrEmpty() == false)
            {
                using (var api = new ApiClient())
                {
                    var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetAccountHolds, _SandboxMode).WithId(account_id));
                    var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                    response = await api.GetAsync<IEnumerable<AccountHold>>(uri.ToString(), headers);
                }
            }
            else
            {
                response.Fail("No id was given.");
            }
            return response;
        }
        public async Task<MethodResponse<IEnumerable<AccountLedger>>> GetAccountLedgers(string account_id)
        {
            var response = new MethodResponse<IEnumerable<AccountLedger>>();
            if (account_id.IsNullOrEmpty() == false)
            {
                using (var api = new ApiClient())
                {
                    var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetAccountLedger, _SandboxMode).WithId(account_id));
                    var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                    response = await api.GetAsync<IEnumerable<AccountLedger>>(uri.ToString(), headers);
                }
            }
            else
            {
                response.Fail("No id was given.");
            }
            return response;
        }
        public async Task<MethodResponse<IEnumerable<AccountTransfer>>> GetAccountTransfers(string account_id)
        {
            var response = new MethodResponse<IEnumerable<AccountTransfer>>();
            if (account_id.IsNullOrEmpty() == false)
            {
                using (var api = new ApiClient())
                {
                    var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetAccountLedger, _SandboxMode).WithId(account_id));
                    var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                    response = await api.GetAsync<IEnumerable<AccountTransfer>>(uri.ToString(), headers);
                }
            }
            else
            {
                response.Fail("No id was given.");
            }
            return response;
        }
        #endregion

        // Coinbase Account Apis
        #region Coinbase_Accounts
        public async Task<MethodResponse<IEnumerable<Wallet>>> GetAllWallets()
        {
            var response = new MethodResponse<IEnumerable<Wallet>>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetAllWallets, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<IEnumerable<Wallet>>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<CryptoAddress>> GenerateCryptoAddress(string account_id)
        {
            var response = new MethodResponse<CryptoAddress>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GenerateCryptoAddress, _SandboxMode).WithId(account_id));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.POST.ToString(), uri.AbsolutePath);
                response = await api.PostAsync<CryptoAddress>(uri.ToString(), null, headers);
            }
            return response;
        }
        #endregion

        // Conversion Apis
        #region Conversions
        public async Task<MethodResponse<ConvertCurrencyResponse>> ConvertCurrency(ConvertCurrencyRequest request)
        {
            var response = new MethodResponse<ConvertCurrencyResponse>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.ConvertCurrency, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.POST.ToString(), uri.AbsolutePath, request);
                response = await api.PostAsync<ConvertCurrencyResponse>(uri.ToString(), null, headers);
            }
            return response;
        }
        public async Task<MethodResponse<Conversion>> GetConversionById(string conversion_id)
        {
            var response = new MethodResponse<Conversion>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetConversion, _SandboxMode).WithId(conversion_id));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<Conversion>(uri.ToString(), headers);
            }
            return response;
        }
        #endregion

        // Currency Apis
        #region Currencies
        public async Task<MethodResponse<IEnumerable<Currency>>> GetAllKnownCurrencies()
        {
            var response = new MethodResponse<IEnumerable<Currency>>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetAllKnownCurrencies, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<IEnumerable<Currency>>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<Currency>> GetCurrencyById(string currency_id)
        {
            var response = new MethodResponse<Currency>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetCurrency, _SandboxMode).WithId(currency_id));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<Currency>(uri.ToString(), headers);
            }
            return response;
        }
        #endregion

        // Transfer Apis
        #region Transfers
        public async Task<MethodResponse<DepositFromAccountResponse>> DepositFromCoinbaseAccount(DepositFromAccountRequest request)
        {
            var response = new MethodResponse<DepositFromAccountResponse>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.DepositFromAccount, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.POST.ToString(), uri.AbsolutePath, request);
                response = await api.PostAsync<DepositFromAccountResponse>(uri.ToString(), null, headers);
            }
            return response;
        }
        public async Task<MethodResponse<DepositFromPayMethodResponse>> DepositFromPaymentMethod(DepositFromPayMethodRequest request)
        {
            var response = new MethodResponse<DepositFromPayMethodResponse>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.DepositFromPaymentMethod, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.POST.ToString(), uri.AbsolutePath, request);
                response = await api.PostAsync<DepositFromPayMethodResponse>(uri.ToString(), null, headers);
            }
            return response;
        }
        public async Task<MethodResponse<IEnumerable<PaymentMethod>>> GetAllPaymentMethods()
        {
            var response = new MethodResponse<IEnumerable<PaymentMethod>>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetAllPaymentMethods, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<IEnumerable<PaymentMethod>>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<IEnumerable<AccountTransfer>>> GetAllTransfers()
        {
            var response = new MethodResponse<IEnumerable<AccountTransfer>>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetAllTransfers, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<IEnumerable<AccountTransfer>>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<AccountTransfer>> GetTransferById(string transfer_id)
        {
            var response = new MethodResponse<AccountTransfer>();
            if (transfer_id.IsNullOrEmpty() == false)
            {
                using (var api = new ApiClient())
                {
                    var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetTransfer, _SandboxMode).WithId(transfer_id));
                    var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                    response = await api.GetAsync<AccountTransfer>(uri.ToString(), headers);
                }
            }
            else
            {
                response.Fail("No id was given.");
            }
            return response;
        }
        public async Task<MethodResponse<WithdrawToAccountResponse>> WithdrawToCoinbaseAccount(WithdrawToAccountRequest request)
        {
            var response = new MethodResponse<WithdrawToAccountResponse>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.WithdrawToAccount, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.POST.ToString(), uri.AbsolutePath, request);
                response = await api.PostAsync<WithdrawToAccountResponse>(uri.ToString(), null, headers);
            }
            return response;
        }
        public async Task<MethodResponse<WithdrawToAddressResponse>> WithdrawToCryptoAddress(WithdrawToAddressRequest request)
        {
            var response = new MethodResponse<WithdrawToAddressResponse>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.WithdrawToAddress, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.POST.ToString(), uri.AbsolutePath, request);
                response = await api.PostAsync<WithdrawToAddressResponse>(uri.ToString(), null, headers);
            }
            return response;
        }
        public async Task<MethodResponse<WithdrawToPayMethodResponse>> WithdrawToCryptoAddress(WithdrawToPayMethodRequest request)
        {
            var response = new MethodResponse<WithdrawToPayMethodResponse>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.WithdrawToPaymentMethod, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.POST.ToString(), uri.AbsolutePath, request);
                response = await api.PostAsync<WithdrawToPayMethodResponse>(uri.ToString(), null, headers);
            }
            return response;
        }
        #endregion

        // Fees Apis
        #region Fees
        public async Task<MethodResponse<IEnumerable<Fee>>> GetFees()
        {
            var response = new MethodResponse<IEnumerable<Fee>>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetFees, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<IEnumerable<Fee>>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<FeeEstimate>> GetFeeEstimateForWithdrawal(string currency, string address)
        {
            var response = new MethodResponse<FeeEstimate>();
            using (var api = new ApiClient())
            {
                var uri = new Uri($"{CoinbaseUrls.Get(CoinbaseUrl.GetFeeEstimateForWithdrawal, _SandboxMode)}?currency={currency}&crypto_address={address}");
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<FeeEstimate>(uri.ToString(), headers);
            }
            return response;
        }
        #endregion

        // Orders Apis
        #region Orders
        public async Task<MethodResponse<IEnumerable<Fill>>> GetAllFills()
        {
            var response = new MethodResponse<IEnumerable<Fill>>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetAllFills, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<IEnumerable<Fill>>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<IEnumerable<Order>>> GetAllOrders()
        {
            var response = new MethodResponse<IEnumerable<Order>>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetAllOrders, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<IEnumerable<Order>>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<IEnumerable<string>>> CancelAllOrders()
        {
            var response = new MethodResponse<IEnumerable<string>>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.CancelAllOrders, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.DELETE.ToString(), uri.AbsolutePath);
                response = await api.DeleteAsync<IEnumerable<string>>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<CreateNewOrderResponse>> CreateOrder(CreateNewOrderRequest request)
        {
            var response = new MethodResponse<CreateNewOrderResponse>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.CreateNewOrder, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.POST.ToString(), uri.AbsolutePath, request);
                response = await api.PostAsync<CreateNewOrderResponse>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<Order>> GetOrderById(string order_id)
        {
            var response = new MethodResponse<Order>();
            using (var api = new ApiClient())
            {
                var uri = new Uri($"{CoinbaseUrls.Get(CoinbaseUrl.GetOrder, _SandboxMode).WithId(order_id)}");
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<Order>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<string>> CancelOrderById(string order_id)
        {
            var response = new MethodResponse<string>();
            using (var api = new ApiClient())
            {
                var uri = new Uri($"{CoinbaseUrls.Get(CoinbaseUrl.CancelOrder, _SandboxMode).WithId(order_id)}");
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.DELETE.ToString(), uri.AbsolutePath);
                response = await api.DeleteAsync<string>(uri.ToString(), headers);
            }
            return response;
        }
        #endregion

        // Coinbase Price Oracle Apis
        #region Coinbase_Price_Oracle
        public async Task<MethodResponse<SignedPrice>> GetSignedPrices()
        {
            var response = new MethodResponse<SignedPrice>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetSignedPrices, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<SignedPrice>(uri.ToString(), headers);
            }
            return response;
        }
        #endregion

        // Products Apis
        #region Products
        public async Task<MethodResponse<IEnumerable<Product>>> GetAllKnownProducts()
        {
            var response = new MethodResponse<IEnumerable<Product>>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetAllKnownProducts, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<IEnumerable<Product>>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<Product>> GetProductById(string product_id)
        {
            var response = new MethodResponse<Product>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetProduct, _SandboxMode).WithId(product_id));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<Product>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<ProductBook>> GetProductBookById(string product_id)
        {
            var response = new MethodResponse<ProductBook>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetProductBook, _SandboxMode).WithId(product_id));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<ProductBook>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<IEnumerable<object>>> GetProductCandlesById(string product_id)
        {
            var response = new MethodResponse<IEnumerable<object>>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetProductCandles, _SandboxMode).WithId(product_id));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<IEnumerable<object>>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<ProductStats>> GetProductStatsById(string product_id)
        {
            var response = new MethodResponse<ProductStats>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetProductStats, _SandboxMode).WithId(product_id));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<ProductStats>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<ProductTicker>> GetProductTickerById(string product_id)
        {
            var response = new MethodResponse<ProductTicker>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetProductTicker, _SandboxMode).WithId(product_id));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<ProductTicker>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<IEnumerable<ProductTrade>>> GetProductTradesById(string product_id)
        {
            var response = new MethodResponse<IEnumerable<ProductTrade>>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetProductTrades, _SandboxMode).WithId(product_id));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<IEnumerable<ProductTrade>>(uri.ToString(), headers);
            }
            return response;
        }
        #endregion

        // Profiles Apis
        #region Profiles
        public async Task<MethodResponse<IEnumerable<Profile>>> GetProfiles()
        {
            var response = new MethodResponse<IEnumerable<Profile>>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetProfiles, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<IEnumerable<Profile>>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<CreateProfileResponse>> CreateProfile(CreateProfileRequest request)
        {
            var response = new MethodResponse<CreateProfileResponse>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.CreateProfile, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.POST.ToString(), uri.AbsolutePath, request);
                response = await api.PostAsync<CreateProfileResponse>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<TransferFundsBetweenProfilesResponse>> TransferFundsBetweenProfiles(TransferFundsBetweenProfilesRequest request)
        {
            var response = new MethodResponse<TransferFundsBetweenProfilesResponse>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.TransferFundsBetweenProfiles, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.POST.ToString(), uri.AbsolutePath, request);
                response = await api.PostAsync<TransferFundsBetweenProfilesResponse>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<Profile>> GetProfileById(string profile_id)
        {
            var response = new MethodResponse<Profile>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetProfileById, _SandboxMode).WithId(profile_id));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<Profile>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<RenameProfileResponse>> RenameProfileById(RenameProfileRequest request)
        {
            var response = new MethodResponse<RenameProfileResponse>();
            using (var api = new ApiClient())
            {
                var profile_id = request.profile_id;
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.RenameProfile, _SandboxMode).WithId(profile_id));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.PUT.ToString(), uri.AbsolutePath, request);
                response = await api.PutAsync<RenameProfileResponse>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<DeleteProfileResponse>> DeleteProfileById(DeleteProfileRequest request)
        {
            var response = new MethodResponse<DeleteProfileResponse>();
            using (var api = new ApiClient())
            {
                var profile_id = request.profile_id;
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.DelateProfile, _SandboxMode).WithId(profile_id));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.PUT.ToString(), uri.AbsolutePath, request);
                response = await api.PutAsync<DeleteProfileResponse>(uri.ToString(), headers);
            }
            return response;
        }
        #endregion

        // Reports Apis
        #region Reports
        public async Task<MethodResponse<IEnumerable<Report>>> GetAllReports()
        {
            var response = new MethodResponse<IEnumerable<Report>>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetAllReports, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<IEnumerable<Report>>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<CreateReportResponse>> CreateReport(CreateReportRequest request)
        {
            var response = new MethodResponse<CreateReportResponse>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.CreateReport, _SandboxMode));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.POST.ToString(), uri.AbsolutePath, request);
                response = await api.PostAsync<CreateReportResponse>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<Report>> GetReportById(string report_id)
        {
            var response = new MethodResponse<Report>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetReportById, _SandboxMode).WithId(report_id));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<Report>(uri.ToString(), headers);
            }
            return response;
        }
        #endregion

        // Users Apis
        #region Users
        public async Task<MethodResponse<UserExchangeLimit>> GetUserExchangeLimits(string user_id)
        {
            var response = new MethodResponse<UserExchangeLimit>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Get(CoinbaseUrl.GetUserExchangeLimits, _SandboxMode).WithId(user_id));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.GetAsync<UserExchangeLimit>(uri.ToString(), headers);
            }
            return response;
        }
        #endregion

        public IExternalCredentials GetCredentials()
        {
            var credentialsHelper = new CredentialsHelper();
            if (_SandboxMode == true)
            {
                _Credentials = credentialsHelper.GetCredentials<CoinbaseSandboxApiCredentials>();
            }
            else
            {
                _Credentials = credentialsHelper.GetCredentials<CoinbaseApiCredentials>();
            }
            return _Credentials;
        }
    }
}
