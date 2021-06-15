using BLOGCORE.APPLICATION.Core.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BLOGCORE.APPLICATION.Core.Interfaces.Storage
{
    public interface IStorageService
    {
        bool GuardarArchivo(MemoryStream ms, string RutaArchivo, ref string mensaje, string containerName = null);
        bool EliminarArchivo(string RutaArchivo, ref string mensaje, string containerName = null);
        bool ValidarRutaArchivo(string RutaArchivo, ref string mensaje, string containerName = null);
        MemoryStream ObtenerArchivo(string RutaArchivo, ref string mensaje, string containerName = null);
        //List<T> ObtenerRecovery<T>(string DirectorioRecovery, string PrefijoArchivo, ref string mensaje, string containerName = null);
        bool GenerarDirectorio(string RutaDirectorio, ref string mensaje, string containerName = null);
        //bool GenerarRecovery<T>(ref T Obj, bool DBResult, string DirectorioRecovery, string NombreArchivo, ref string XmlRecovery, ref string mensaje, string containerName = null);
    }
}
