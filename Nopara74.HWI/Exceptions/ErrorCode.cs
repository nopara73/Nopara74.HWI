using System;
using System.Collections.Generic;
using System.Text;

namespace Nopara74.HWI.Exceptions
{
    public enum ErrorCode
    {
        NoDevicePath = -1,
        NoDeviceType = -2,
        DeviceConnError = -3,
        UnknownDeviceType = -4,
        InvalidTx = -5,
        NoPassword = -6,
        BadArgument = -7,
        NotImplemented = -8,
        UnavailableAction = -9,
        DeviceAlreadyInit = -10,
        DeviceAlreadyUnlocked = -11,
        DeviceNotReady = -12,
        UnknownError = -13,
        ActionCanceled = -14,
        DeviceBusy = -15,
        NeedToBeRoot = -16
    }
}
