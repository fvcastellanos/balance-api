
using NUnit.Framework;
using BalanceApi.Validators;

namespace BalanceApi.Tests.Validators 
{
    public class ValidatorTest
    {
        [Test]
        public void FirstTest()
        {
            TransactionTypeValidator transactionTypeValidator = new TransactionTypeValidator();
            var result = transactionTypeValidator.Validate(null);
            Assert.True(result.HasFailed());
        }
    }
}