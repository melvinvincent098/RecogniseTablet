using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecogniseTablet.Interfaces
{
    public interface IFaceManager
    {
        Task DetectFace(string filePath);
    }
}
