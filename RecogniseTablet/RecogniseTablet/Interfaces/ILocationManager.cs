using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecogniseTablet.Interfaces
{
    public interface ILocationManager
    {
        Task GetLocation(int UserId);
    }
}
