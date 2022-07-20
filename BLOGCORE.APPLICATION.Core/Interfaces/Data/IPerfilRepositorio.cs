using BLOGCORE.APPLICATION.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLOGCORE.APPLICATION.Core.Interfaces.Data
{
    public interface IPerfilRepositorio
    {
        List<TipoRedSocial> GetTipoRedSocial();
        List<RedesSociales> GetMisRedes(string username);
    }
}
