using HomeWork3.Models;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;

namespace HomeWork3.Interfaces
{
    public interface ITemperatureValidator
    {
        ValidationResult Validate(double temperature);
    }
}