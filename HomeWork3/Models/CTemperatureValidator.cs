using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HomeWork3.Interfaces;

namespace HomeWork3.Models
{
    public class CTemperatureValidator : ITemperatureValidator
    {
        public ValidationResult Validate(double temperature)
        {
            var result = new ValidationResult()
            {
                Valid = true
            };

            if (temperature > double.MaxValue)
            {
                result.Valid = false;
                result.ErrorMessage = $"{temperature} bigger than double max value.";

            }
            else if (temperature < -273.15d)
            {
                result.Valid = false;
                result.ErrorMessage = $"{temperature}°  on the Celsius scale is lower than absolute zero.";
            }

            return result;
        }
    }
}
