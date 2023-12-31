﻿using FluentAssertions;
using ShopNow.Domain.Checkout.Entities;

namespace ShopNow.UnitTests.Specs.Entities
{
    public class CpfTests
    {
        private Cpf _cpf;

        [SetUp]
        public void Setup()
        {
            _cpf = new Cpf();
        }

        [TestCase("")]
        [TestCase(null)]
        public void ShouldBeAbleToValidateAnInvalidCpfWith(string invalidCpf)
        {
            var validatedCpf = _cpf.Validate(invalidCpf);

            validatedCpf.Should().BeFalse();
        }

        [Test]
        public void ShouldBeAbleToValidateAnInvalidCpfWithRepeatedNumbers()
        {
            const string invalidCpf = "111.111.111-11";

            var validatedCpf = _cpf.Validate(invalidCpf);

            validatedCpf.Should().BeFalse();
        }

        [TestCase("111.444.777-45")]
        [TestCase("111.444.777-36")]
        [TestCase("111.444.777-26")]
        public void ShouldBeAbleToValidateAnInvalidCpfWithLastDigitsInvalid(string invalidCpf)
        {
            var validatedCpf = _cpf.Validate(invalidCpf);

            validatedCpf.Should().BeFalse();
        }

        [TestCase("batatatinha")]
        [TestCase("123.bat.999-99")]
        [TestCase("123.999.bat-99")]
        [TestCase("123.123.999-bat")]
        [TestCase("bat.123.999-bat")]
        public void ShouldBeAbleToValidateAnInvalidCpfThatHasLetters(string invalidCpf)
        {
            var validatedCpf = _cpf.Validate(invalidCpf);

            validatedCpf.Should().BeFalse();
        }

        [TestCase("111444777356")]
        [TestCase("1114447773")]
        public void ShouldBeAbleToValidateAnInvalidCpfThatHasInvalidLength(string invalidCpf)
        {
            var validatedCpf = _cpf.Validate(invalidCpf);

            validatedCpf.Should().BeFalse();
        }

        [TestCase("111.444.777-35")]
        [TestCase("93541134780")]
        public void ShouldBeAbleToValidateAnValidCpf(string validCpf)
        {
            var validatedCpf = _cpf.Validate(validCpf);

            validatedCpf.Should().BeTrue();
        }

    }
}
