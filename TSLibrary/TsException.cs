using System;
using System.Runtime.Serialization;

namespace TSLibrary
{
    public class TsException : Exception
    {
        public TsException(string message) : base(message) { }
    }
}