using System;
using System.ComponentModel.DataAnnotations;

namespace CarPool.WebAPI.ViewModels;

public class YearRangeAttribute : ValidationAttribute
{
    private readonly int _minYear;
    private readonly int _maxYear;

    public YearRangeAttribute(int minYear)
    {
        _minYear = minYear;
        _maxYear = DateTime.Now.Year;
    }

    public override bool IsValid(object? value)
    {
        return value is int year && year >= _minYear && year <= _maxYear;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"The {name} must be between {_minYear} and {_maxYear}.";
    }
}

