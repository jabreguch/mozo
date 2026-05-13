
using Mozo.Helper.Enu;

namespace Mozo.ApiSeguridad.SaveImagen
{
    public class AlmacenaArchivo : IAlmacenaArchivo
    {

        public ArchivoModel Almacenar(AlmacenaModel contenedor, IFormFile file, IConfiguration configuration)
        {
            ArchivoModel archivo = new();
            string folderContenedor = "";

            if (contenedor.CarpetaGuarda == EnuCommon.CoCarpetaGuarda.FolderImage)
                folderContenedor = configuration.GetSection("AppSettings").GetSection("FolderImage").Value!;
            if (contenedor.CarpetaGuarda == EnuCommon.CoCarpetaGuarda.FolderDocument)
                folderContenedor = configuration.GetSection("AppSettings").GetSection("FolderDocument").Value!;

            folderContenedor = contenedor.GetRuta(folderContenedor);

            if (File.Exists(folderContenedor))
                Directory.CreateDirectory(folderContenedor);


            archivo.NoExtension = Path.GetExtension(file.FileName);
            archivo.NoArchivo = contenedor.Id.ToString();
            string nameFile = archivo.NoExtension + "." + archivo.NoArchivo;




            string ruta = Path.Combine(folderContenedor, nameFile);
            using (MemoryStream ms = new MemoryStream())
            {
                file.CopyTo(ms);
                byte[] contenido = ms.ToArray();
                File.WriteAllBytes(ruta, contenido);
            }

            return archivo;
        }

        public void Delete(AlmacenaModel contenedor, IConfiguration configuration)
        {
            string folderContenedor = "";

            if (contenedor.CarpetaGuarda == EnuCommon.CoCarpetaGuarda.FolderImage)
                folderContenedor = configuration.GetSection("AppSettings").GetSection("FolderImage").Value!;
            if (contenedor.CarpetaGuarda == EnuCommon.CoCarpetaGuarda.FolderDocument)
                folderContenedor = configuration.GetSection("AppSettings").GetSection("FolderDocument").Value!;

            folderContenedor = contenedor.GetRuta(folderContenedor);

            string nameFile = contenedor.Id + "." + contenedor.NoExtension;
            string ruta = Path.Combine(folderContenedor, nameFile);

            if (File.Exists(ruta))
                File.Delete(ruta);

        }


    }
}
