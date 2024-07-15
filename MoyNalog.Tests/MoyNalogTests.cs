using MoyNalog.Enums;
using MoyNalog.Models;
using MoyNalog.Tests.Helpers;

namespace TDV.MoyNalog.Tests
{
    [Collection("Enviroment collection")]
    public class MoyNalogTests
    {
        [Fact]
        public async Task Init()
        {
            // Arrange
            var client = GetClient();

            // Act
            await client.Auth();
            var sut = client._auth;

            // Assert
            Assert.NotNull(sut);
            Assert.NotNull(sut.RefreshToken);
            Assert.NotNull(sut.Token);
            Assert.NotNull(sut.TokenExpireIn);
            Assert.NotNull(sut.Profile);
        }

        [Fact]
        public async Task RefreshToken()
        {
            // Arrange
            var client = GetClient();
            await client.Auth();
            Assert.NotNull(client._auth);
            client._auth.TokenExpireIn = DateTime.UtcNow.AddMinutes(-1);

            // Act
            await client.GetToken();

            // Assert
            Assert.True(client._auth.TokenExpireIn > DateTime.UtcNow);
        }

        // В случае фейла этого теста нужно самостоятельно удалить созданный платеж. иначе прийдет налог
        [Fact]
        public async Task Add_Get_CancelIncome()
        {
            // Arrange
            var client = GetClient();
            const decimal servicePrice = 11.1m;
            List<ServiceInfo> services = [new ServiceInfo {
                Amount = servicePrice,
                Name = "Test",
                Quantity = 1,
            }];
            // Act
            var receiptUuid = await client.AddIncome(new Client() { IncomeType = IncomeType.FROM_INDIVIDUAL}, services, DateTime.UtcNow);

            // Assert
            Assert.NotEmpty(receiptUuid);

            // Act get Income
            var income = await client.GetRecipiet(receiptUuid);
            Assert.NotNull(income);
            Assert.Null(income.CancellationInfo);
            Assert.Equal(services.Count(), income.Services.Count());
            Assert.Equal(services.First().Name, income.Services.First().Name);
            Assert.Equal(services.First().Amount, income.Services.First().Amount);
            Assert.Equal(IncomeType.FROM_INDIVIDUAL, income.IncomeType);
            Assert.Equal(PaymentType.CASH, income.PaymentType);
            Assert.Equal(EnviromentHelper.GetVariable("inn"), income.Inn);
            Assert.Equal(servicePrice, income.TotalAmount);

            // Act cancel income
            var cancelledIncome = await client.CancelIncome(receiptUuid, DateTime.UtcNow);

            // Assert
            Assert.Equal(receiptUuid, cancelledIncome.IncomeInfo.ApprovedReceiptUuid);
            Assert.Equal("Чек сформирован ошибочно", cancelledIncome.IncomeInfo.CancellationInfo?.Comment);
        }

        [Fact]
        public async Task GetIncomes()
        {
            // Arrange
            var client = GetClient();

            // Act
            var sut = await client.GetIncomes(new GetIncomesRequest {
                From = DateTime.Now.AddYears(-1),
                To = DateTime.Now,
                Limit = 10,
                SortBy = IncomesSortBy.OperationTimeDesc
            });

            // Assert
            Assert.Equal(10, sut.CurrentLimit);
            Assert.NotEmpty(sut.Content);
        }

        private MoyNalog GetClient() {
            return new MoyNalog(EnviromentHelper.GetVariable("inn"), EnviromentHelper.GetVariable("passw"));
        }
    }
}