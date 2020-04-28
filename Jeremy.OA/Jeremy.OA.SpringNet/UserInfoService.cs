using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeremy.OA.SpringNet
{
    public class UserInfoService : IUserInfoService
    {
        public string UserName { get; set; }
        public Person Person { get; set; }
        public string ShowMsg()
        {
            return "Hello SpringNet, Name - " + UserName + " , Age - " + Person.Age;
        }
    }
}
