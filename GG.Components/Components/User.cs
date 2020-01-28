using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.Components
{
    public interface IUser
    {
        int Id { get; set; }

    }
    public class User : IUser
    {
        public int Id { get; set; }
    }
}
