using System.Text.RegularExpressions;

namespace ShopNow.Domain.Entities
{
    public class Cpf
    {
        private const int VALID_CPF_LENGTH = 11;
        private const int FACTOR_FIRST_VERIFIER_DIGIT = 10;
        private const int FACTOR_SECOND_VERIFIER_DIGIT = 11;

        private string RemoveMaskCpf(string cpf)
        {
            return cpf
                .Replace(".", "")
                .Replace(".", "")
                .Replace("-", "")
                .Replace(" ", "");
        }

        private bool HasOnlyNumbers(string value)
        {
            return Regex.IsMatch(value, @"^\d+$");
        }

        private bool AreAllDigitsEqual(string cpf)
        {
            return cpf.All(c => c == cpf.First());
        }

        public string ExtractVerifierDigit(string cpf)
        {
            return cpf.Substring(cpf.Length - 2);
        }

        public int CalculateDigit(string cpf, int factor)
        {
            var total = 0;
            foreach (var digit in cpf)
            {
                if (factor > 1)
                {
                    total += int.Parse(digit.ToString()) * factor--;
                }
            }

            var rest = total % 11;
            return rest < 2 ? 0 : 11 - rest;
        }

        public bool Validate(string rawCpf)
        {
            if (string.IsNullOrEmpty(rawCpf))
            {
                return false;
            }

            var cpf = RemoveMaskCpf(rawCpf);

            if (!HasOnlyNumbers(cpf))
            {
                return false;
            }

            if (cpf.Length != VALID_CPF_LENGTH)
            {
                return false;
            }

            if (AreAllDigitsEqual(cpf))
            {
                return false;
            }

            var firstVerifierDigit = CalculateDigit(cpf, FACTOR_FIRST_VERIFIER_DIGIT);
            var secondVerifierDigit = CalculateDigit(cpf, FACTOR_SECOND_VERIFIER_DIGIT);

            var verifierDigit = ExtractVerifierDigit(cpf);
            var calculatedVerifierDigit = $"{firstVerifierDigit}{secondVerifierDigit}";

            return calculatedVerifierDigit == verifierDigit;
        }

    }
}
