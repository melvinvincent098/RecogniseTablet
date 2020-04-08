using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecogniseTablet.Interfaces
{
    public interface IFaceManager
    {
        Task<bool> RegisterFace(byte[] byteData, string PersonGroupID, string username, string name);

        Task<bool> IdentifyFace(byte[] byteData, string PersonGroupID);
    }
}
