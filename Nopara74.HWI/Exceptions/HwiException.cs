using Nopara74.HWI;
using System;
using System.Collections.Generic;

namespace Nopara74.HWI.Exceptions
{
    public class HwiException : Exception
    {
        public ErrorCode ErrorCode { get; }

        public HwiException(ErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }
    }
}
