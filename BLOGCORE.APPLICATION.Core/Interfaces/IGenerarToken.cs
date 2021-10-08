using BLOGCORE.APPLICATION.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.Interfaces
{
    public interface IGenerarToken
    {
        string CreateToken(Usuario usuario);
    }
}
