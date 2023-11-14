using System;

namespace Seasons.Exceptions;

public class InvalidStateException : Exception
{
    public InvalidStateException(string message) : base(message)
    {
    }
}