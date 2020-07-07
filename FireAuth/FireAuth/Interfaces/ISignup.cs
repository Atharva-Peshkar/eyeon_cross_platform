using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FireAuth
{
    public interface ISignup
    {
        Task<string> SignUpWithEmailPassword(string email, string password);
    }
}
