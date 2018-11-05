using System;
using System.Collections.Generic;
using System.Text;

namespace CrossPlatform.Demo.Services
{
    public interface IServices
    {
        void Toast(string message);

        bool SendSms(string message, string receipient);
    }
}
