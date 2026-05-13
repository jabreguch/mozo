namespace Mozo.ApiSeguridad.SaveImagen
{
    public interface IAlmacenaArchivo
    {
        void Delete(AlmacenaModel contenedor, IConfiguration configuration);
        ArchivoModel Almacenar(AlmacenaModel contenedor, IFormFile file, IConfiguration configuration);

        ArchivoModel Editar(AlmacenaModel contenedor, IFormFile file, IConfiguration configuration)
        {
            Delete(contenedor, configuration);
            return Almacenar(contenedor, file, configuration);
        }


    }
}
