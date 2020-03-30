using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecogniseTablet.Interfaces
{
    public interface IFaceManager
    {
        Task RegisterFace(string filePath, string PersonGroupID, string username, string name);

        Task<bool> IdentifyFace(string filePath, string PersonGroupID);
    }
}
