using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vixi.OTC.Application.Interfaces;
using Vixi.OTC.Application.Service;
using Vixi.OTC.Domain.Interfaces.Repository;
using Xunit;

namespace Vixi.OTC.Application.Tests
{
    public class ReconciliationServiceTests
    {
        private readonly Mock<IRepository<Domain.Entities.BankAccount>> _mockBankAccountRepository = new Mock<IRepository<Domain.Entities.BankAccount>>();
        private readonly Mock<IRepository<Domain.Entities.BankStatement>> _mockBankStatementRepository = new Mock<IRepository<Domain.Entities.BankStatement>>();
        private readonly Mock<IPluggyClientService> _mockPluggyClientService = new Mock<IPluggyClientService>();
        private readonly Mock<ILogger<BankStatementService>> _mockLogger = new Mock<ILogger<BankStatementService>>();
        private readonly Mock<IRepositoryBankStatement> _mockIRepositoryBankStatement = new Mock<IRepositoryBankStatement>();

        public BankStatementService GetService()
        {
            return new BankStatementService(
                _mockLogger.Object,
                _mockPluggyClientService.Object,
                _mockBankAccountRepository.Object,
                _mockBankStatementRepository.Object,
                _mockIRepositoryBankStatement.Object);
        }

        [Fact]
        public async Task GetTransactions_Sucess()
        {
            var pluggyService = GetService();

            _mockBankAccountRepository
                .Setup(x => x.GetAsync<Infra.Data.Entities.BankAccountPersistence>())
                .ReturnsAsync(new List<Infra.Data.Entities.BankAccountPersistence>()
                {
                new Infra.Data.Entities.BankAccountPersistence()
                {
                    AccountId = Guid.NewGuid()
                }
                });

            _mockPluggyClientService
                .Setup(x => x.GetTransactionsByAccountIdAndDateTimeNow(It.IsAny<List<Guid>>()))
                .ReturnsAsync(GetTransactions());

            await pluggyService.ProcessTransactionsAsync();

            _mockBankAccountRepository.Verify(x => x.GetAsync<Infra.Data.Entities.BankAccountPersistence>(), Times.Exactly(1));
            _mockPluggyClientService.Verify(x => x.GetTransactionsByAccountIdAndDateTimeNow(It.IsAny<List<Guid>>()), Times.Exactly(1));

            //TODO Validar chamada no repo de insert
        }

        [Fact]
        public async Task GetTransactions_Sucess_NotTransactions()
        {
            var pluggyService = GetService();

            _mockBankAccountRepository
                .Setup(x => x.GetAsync<Infra.Data.Entities.BankAccountPersistence>())
                .ReturnsAsync(new List<Infra.Data.Entities.BankAccountPersistence>()
                {
                new Infra.Data.Entities.BankAccountPersistence()
                {
                    AccountId = Guid.NewGuid()
                }
                });

            _mockPluggyClientService
                .Setup(x => x.GetTransactionsByAccountIdAndDateTimeNow(It.IsAny<List<Guid>>()))
                .ReturnsAsync(new List<Domain.Entities.TransactionDto>());

            await pluggyService.ProcessTransactionsAsync();

            _mockBankAccountRepository.Verify(x => x.GetAsync<Infra.Data.Entities.BankAccountPersistence>(), Times.Exactly(1));
            _mockPluggyClientService.Verify(x => x.GetTransactionsByAccountIdAndDateTimeNow(It.IsAny<List<Guid>>()), Times.Exactly(1));

            //TODO Validar que n�o foi feita a chamada no repo de insert
        }

        [Fact]
        public async Task GetTransactions_Sucess_NotAccounts()
        {
            var pluggyService = GetService();

            _mockBankAccountRepository
                .Setup(x => x.GetAsync<Infra.Data.Entities.BankAccountPersistence>())
                .ReturnsAsync(new List<Infra.Data.Entities.BankAccountPersistence>());

            _mockPluggyClientService
                .Setup(x => x.GetTransactionsByAccountIdAndDateTimeNow(It.IsAny<List<Guid>>()))
                .ReturnsAsync(new List<Domain.Entities.TransactionDto>());

            await pluggyService.ProcessTransactionsAsync();

            _mockBankAccountRepository.Verify(x => x.GetAsync<Infra.Data.Entities.BankAccountPersistence>(), Times.Exactly(1));
            _mockPluggyClientService.Verify(x => x.GetTransactionsByAccountIdAndDateTimeNow(It.IsAny<List<Guid>>()), Times.Never());

            //TODO Validar que n�o foi feita a chamada no repo de insert
        }

        public static List<Domain.Entities.TransactionDto> GetTransactions()
        {
            return new List<Domain.Entities.TransactionDto>()
        {
            new Domain.Entities.TransactionDto
            (
                id: Guid.NewGuid(),
                description: "",
                currencyCode: Domain.Entities.CurrencyCode.BRL,
                amount: 100d,
                date: DateTime.Now,
                balance: 10d,
                accountId: Guid.NewGuid(),
                "",
                type: Domain.Entities.TransactionType.CREDIT,
                category: "Transfers",
                paymentData: TransactionPaymentDataDto()
             )
        };
        }

        private static Domain.Entities.TransactionPaymentDataDto TransactionPaymentDataDto()
        {
            return new Domain.Entities.TransactionPaymentDataDto
                    (
                        payer: TransactionPaymentParticipantDto(),
                        receiver: TransactionPaymentParticipantDto(),
                        paymentMethod: null,
                        referenceNumber: null,
                        reason: null
                    );
        }

        private static Domain.Entities.TransactionPaymentParticipantDto TransactionPaymentParticipantDto()
        {
            return new Domain.Entities.TransactionPaymentParticipantDto(
                documentNumber: null,
                name: "",
                accountNumber: null,
                branchNumber: null,
                routingNumber: null
                );
        }
    }
}
