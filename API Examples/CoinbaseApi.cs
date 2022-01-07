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
        private CoinbaseApiCredentials _Credentials;

        private static CoinbaseApiHelper _Helper = new CoinbaseApiHelper();
        
        public CoinbaseApi()
        {
            GetCredentials();
        }

        // Account Apis
        #region Accounts
        public async Task<MethodResponse<IEnumerable<Account>>> GetAllAccounts()
        {
            var response = new MethodResponse<IEnumerable<Account>>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetAllAccountsForProfile]);
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
                    var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetAccountById].WithId(account_id));
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
                    var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetAccountHolds].WithId(account_id));
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
                    var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetAccountLedger].WithId(account_id));
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
                    var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetAccountLedger].WithId(account_id));
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetAllWallets]);
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GenerateCryptoAddress].WithId(account_id));
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.ConvertCurrency]);
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetConversion].WithId(conversion_id));
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetAllKnownCurrencies]);
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetCurrency].WithId(currency_id));
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.DepositFromAccount]);
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.DepositFromPaymentMethod]);
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetAllPaymentMethods]);
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetAllTransfers]);
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
                    var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetTransfer].WithId(transfer_id));
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.WithdrawToAccount]);
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.WithdrawToAddress]);
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.WithdrawToPaymentMethod]);
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetFees]);
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
                var uri = new Uri($"{CoinbaseUrls.Url[CoinbaseUrl.GetFeeEstimateForWithdrawal]}?currency={currency}&crypto_address={address}");
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetAllFills]);
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetAllOrders]);
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.CancelAllOrders]);
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
                response = await api.DeleteAsync<IEnumerable<string>>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<CreateNewOrderResponse>> CreateOrder(CreateNewOrderRequest request)
        {
            var response = new MethodResponse<CreateNewOrderResponse>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.CreateNewOrder]);
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath, request);
                response = await api.PostAsync<CreateNewOrderResponse>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<Order>> GetOrderById(string order_id)
        {
            var response = new MethodResponse<Order>();
            using (var api = new ApiClient())
            {
                var uri = new Uri($"{CoinbaseUrls.Url[CoinbaseUrl.GetOrder].WithId(order_id)}");
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
                var uri = new Uri($"{CoinbaseUrls.Url[CoinbaseUrl.CancelOrder].WithId(order_id)}");
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetSignedPrices]);
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetAllKnownProducts]);
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetProduct].WithId(product_id));
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetProductBook].WithId(product_id));
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetProductCandles].WithId(product_id));
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetProductStats].WithId(product_id));
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetProductTicker].WithId(product_id));
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetProductTrades].WithId(product_id));
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetProfiles]);
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.CreateProfile]);
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath, request);
                response = await api.PostAsync<CreateProfileResponse>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<TransferFundsBetweenProfilesResponse>> TransferFundsBetweenProfiles(TransferFundsBetweenProfilesRequest request)
        {
            var response = new MethodResponse<TransferFundsBetweenProfilesResponse>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.TransferFundsBetweenProfiles]);
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath, request);
                response = await api.PostAsync<TransferFundsBetweenProfilesResponse>(uri.ToString(), headers);
            }
            return response;
        }
        public async Task<MethodResponse<Profile>> GetProfileById(string profile_id)
        {
            var response = new MethodResponse<Profile>();
            using (var api = new ApiClient())
            {
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetProfileById].WithId(profile_id));
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.RenameProfile].WithId(profile_id));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath, request);
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
                var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.DelateProfile].WithId(profile_id));
                var headers = _Helper.GetHeaders(_Credentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath, request);
                response = await api.PutAsync<DeleteProfileResponse>(uri.ToString(), headers);
            }
            return response;
        }
        #endregion

        // Reports Apis
        #region Reports
        //public async Task<MethodResponse<IEnumerable<Fee>>> GetFees()
        //{
        //    var response = new MethodResponse<IEnumerable<Fee>>();
        //    using (var api = new ApiClient())
        //    {
        //        var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetFees]);
        //        var headers = _Helper.GetHeaders(_ApiCredentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
        //        response = await api.GetAsync<IEnumerable<Fee>>(uri.ToString(), headers);
        //    }
        //    return response;
        //}
        #endregion

        // Users Apis
        #region Users
        //public async Task<MethodResponse<IEnumerable<Fee>>> GetFees()
        //{
        //    var response = new MethodResponse<IEnumerable<Fee>>();
        //    using (var api = new ApiClient())
        //    {
        //        var uri = new Uri(CoinbaseUrls.Url[CoinbaseUrl.GetFees]);
        //        var headers = _Helper.GetHeaders(_ApiCredentials, DateTime.Now, ApiRequestType.GET.ToString(), uri.AbsolutePath);
        //        response = await api.GetAsync<IEnumerable<Fee>>(uri.ToString(), headers);
        //    }
        //    return response;
        //}
        #endregion

        public IExternalCredentials GetCredentials()
        {
            var credentialsHelper = new CredentialsHelper();
            _Credentials = credentialsHelper.GetCredentials<CoinbaseApiCredentials>();
            return _Credentials;
        }
    }
}
