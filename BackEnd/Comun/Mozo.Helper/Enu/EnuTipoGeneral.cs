using static Mozo.Helper.Enu.EnuSeguridad;

namespace Mozo.Helper.Enu;

public static class EnuTipoGeneral
{
    public static readonly HashSet<int?> MoreFiles = new()
    {
        FormatoArchivo.Soportee.Soporte.Sustento,
        FormatoArchivo.Soportee.Seguimiento.Sustento,
        FormatoArchivo.Catalogo.Producto.Imagen
    };

    public static class FormatoArchivo
    {
        public static int CoGrupo = 6;

        public static class Seguridad
        {
            public static int CoModulo = Modulo.Seguridad.CoModulo;
            public static class Empresa
            {
                public const int Logo = 4;

            }
        };

        public static class Condominio
        {
            public static int CoModulo = Modulo.Condominio.CoModulo;
            public static class Predio
            {
                public const int Minuta = 1;
            }
            public static class PredioAlquiler
            {
                public const int Contrato = 2;
            }
        };

        public static class Soportee
        {
            public static int CoModulo = Modulo.Soporte.CoModulo;

            public static class Soporte
            {
                public const int Sustento = 3;
            }

            public static class Seguimiento
            {
                public const int Sustento = 18;
            }
        };

        public static class Maestro
        {
            public static int CoModulo = Modulo.Maestro.CoModulo;

            public static class Persona
            {
                public const int Foto = 5;
            }
        };
        public static class Empresa
        {
            public static int CoModulo = Modulo.Empresa.CoModulo;

            public static class Blog
            {
                public const int Foto = 6;
            }

            public static class Cliente
            {
                public const int Imagen = 9;
            }

            public static class Servicio
            {
                public const int Imagen = 10;
            }

            public static class ServicioCaracteristica
            {
                public const int Imagen = 21;
            }

            public static class ImagenWeb
            {
                public const int Imagen = 16;
            }
        };

        public static class Expediente
        {
            public static int CoModulo = Modulo.Expediente.CoModulo;

            public static class Documento
            {
                public const int Escrito = 7;
            }

            public static class Notificacion
            {
                public const int Constancia = 8;
            }
        };

        public static class Matricula
        {
            public static int CoModulo = Modulo.Matricula.CoModulo;

            //matricula.matriculapension.recibo
            public static class MatriculaPension
            {
                public const int Recibo = 1;
            }
        };

        public static class Inventario
        {
            public static int CoModulo = Modulo.Inventario.CoModulo;
            public static class Articulo
            {
                public const int Foto = 19;
            }

            public static class Marca
            {
                public const int Logo = 20;
            }
        };

        public static class Catalogo
        {
            public static int CoModulo = Modulo.Catalogo.CoModulo;

            public static class Producto
            {
                public const int Imagen = 17;
            }
        };
        public static string GetPath(string? path, int? coEmpresa, int? coEntidad, string? noArchivo)
        {
            ///Document/CoEmpresa/NoEsquema/NoEntidad/CoEntidad/TipoImagen/nameFile.jpg            
            string[] pathSplit = path!.Split(".");
            return $"{coEmpresa}/{pathSplit[0]}/{pathSplit[1]}/{coEntidad}/{pathSplit[3]}/{noArchivo}";

        }

    };


    public static class Rol
    {
        public static int CoGrupo = 11;

        public static class Seguridad
        {
            public static int CoModulo = EnuSeguridad.Modulo.Seguridad.CoModulo;
            public const int Sistema = 3;
        };
        public static class Condominio
        {
            public static int CoModulo = EnuSeguridad.Modulo.Condominio.CoModulo;
            public const int Inquilino = 4;
            public const int Propietario = 5;
        };
        public static class FinanzaPersonal
        {
            public static int CoModulo = EnuSeguridad.Modulo.FinanzaPersonal.CoModulo;
            public const int Beneficiario = 7;
            public const int Proveedor = 8;
        };
        public static class Soporte
        {
            public static int CoModulo = EnuSeguridad.Modulo.Soporte.CoModulo;
            public const int Cliente = 6;
        };

        public static class Expediente
        {
            public static int CoModulo = EnuSeguridad.Modulo.Expediente.CoModulo;
            public const int Abogado = 9;
            public const int Notificado = 12;
            public const int Parte = 13;
        };

        public static class Empresa
        {
            public static int CoModulo = EnuSeguridad.Modulo.Empresa.CoModulo;
            public const int Bloguero = 10;
            public const int Cliente = 11;
            public const int Equipo = 20;
        };

        public static class Matricula
        {
            public static int CoModulo = EnuSeguridad.Modulo.Matricula.CoModulo;
            public const int Alumno = 14;
            public const int Profesor = 15;
        };

        public static class Facturacion
        {
            public static int CoModulo = EnuSeguridad.Modulo.Facturacion.CoModulo;
            public const int Proveedor = 16;
            public const int Cliente = 17;

        };

        public static class Urbano
        {
            public static int CoModulo = EnuSeguridad.Modulo.Urbano.CoModulo;
            public const int Cliente = 18;
            public const int Vendedor = 19;
            public const int Entidad = 22;
        };

        public static class Maestro
        {
            public static int CoModulo = EnuSeguridad.Modulo.Maestro.CoModulo;
            public const int Entidad = 21;
            public const int Descendiente = 24;
        };
    };


    public static class Maestro
    {
        public static class Dia
        {
            public static int CoModulo = EnuSeguridad.Modulo.Maestro.CoModulo;
            public static int CoGrupo = 2;
        };

        public static class Mes
        {
            public static int CoModulo = EnuSeguridad.Modulo.Maestro.CoModulo;
            public static int CoGrupo = 3;
        };

        public static class Amio
        {
            public static int CoModulo = EnuSeguridad.Modulo.Maestro.CoModulo;
            public static int CoGrupo = 4;
        };

        public static class EstadoCivil
        {
            public static int CoModulo = EnuSeguridad.Modulo.Maestro.CoModulo;
            public static int CoGrupo = 5;
        };

        public static class DiaSemana
        {
            public static int CoModulo = EnuSeguridad.Modulo.Maestro.CoModulo;
            public static int CoGrupo = 10;
        };
        public static class EstadoRegistro
        {
            public static int CoModulo = EnuSeguridad.Modulo.Maestro.CoModulo;
            public static int CoGrupo = 29;
        };

        public static class EtiquetaRedSocial
        {
            public static int CoModulo = EnuSeguridad.Modulo.Maestro.CoModulo;
            public static int CoGrupo = 17;

            public const int Personal = 1;
            public const int Trabajo = 2;
            public const int Otro = 3;
        };
        public static class RedSocial
        {
            public static int CoModulo = EnuSeguridad.Modulo.Maestro.CoModulo;
            public static int CoGrupo = 25;

            public const int TelefonoMovil = 1;
            public const int CorreoElectronico = 2;
            public const int TelefonoFijo = 3;
            public const int Url = 4;

        };
        public static class UrlRedSocial
        {
            public static int CoModulo = EnuSeguridad.Modulo.Maestro.CoModulo;
            public static int CoGrupo = 27;

            public const int PaginaWeb = 1;
            public const int Facebook = 2;
            public const int X = 3;
            public const int Youtube = 4;
            public const int LinkedLn = 5;
            public const int Instagram = 6;
            public const int Tiktok = 7;
        };

        public static class Tabla
        {
            public static int CoModulo = EnuSeguridad.Modulo.Maestro.CoModulo;
            public static int CoGrupo = 38;

            public const int Seguridad_Empresa = 1;
            public const int Maestro_Persona = 2;
        };


    }


    public static class Seguridad
    {
        public static class Pagina
        {
            public static int CoModulo = EnuSeguridad.Modulo.Seguridad.CoModulo;
            public static int CoGrupo = 7;

            public const int Menu = 0;
            public const int Paginaa = 1;
            public const int SubPagina = 2;
            public const int VistaFlotante = 3;
            public const int ServicioWeb = 4;
        };

    }

    public static class Urbano
    {
        public static class Cuota
        {
            public static int CoModulo = EnuSeguridad.Modulo.Urbano.CoModulo;
            public static int CoGrupo = 28;

            public const int Inicial = 1;
            public const int Separación = 2;
            public const int Penalidad = 3;
            public const int Abono = 4;
            public const int CuotaAutomatico = 5; //Cuota Automatica
            public const int CuotaManual = 6; //Cuota Automatica
        };

        public static class Manzana
        {
            public static int CoModulo = EnuSeguridad.Modulo.Urbano.CoModulo;
            public static int CoGrupo = 32;

            public const int Lote = 1;
            public const int Jardin = 2;
            public const int Mercado = 3;
            public const int Colegio = 4;
            public const int Universidad = 5;
        };

        public static class MotivoAnulacionCredito
        {
            public static int CoModulo = EnuSeguridad.Modulo.Urbano.CoModulo;
            public static int CoGrupo = 31;

            public const int CambioTitularidad = 1;
            public const int CambioInmueble = 2;
        };


    }

    public static class Condominio
    {
        public static class CalculoServicio
        {
            public static int CoModulo = EnuSeguridad.Modulo.Condominio.CoModulo;
            public static int CoGrupo = 8;

            public const int Calculo1 = 1;
            public const int Calculo2 = 2;
            public const int Calculo3 = 3;
            public const int Calculo4 = 4;
            public const int Calculo5 = 5;
            public const int Calculo6 = 6;
            public const int Calculo7 = 7;
            public const int Calculo8 = 8;
            public const int Calculo9 = 9;
        };

        public static class Predio
        {
            public static int CoModulo = EnuSeguridad.Modulo.Condominio.CoModulo;
            public static int CoGrupo = 9;

            public const int Condominio = 1;
            public const int Edificio = 2;
            public const int Apartamento = 3;
            public const int Cochera = 4;
        };

    }

    public static class Contabilidad
    {
        public static class Moneda
        {
            public static int CoModulo = EnuSeguridad.Modulo.Contabilidad.CoModulo;
            public static int CoGrupo = 12;

            public const int Sol = 604;
            public const int Dolar = 840;
        };

        public static class DocumentoTributario
        {
            public static int CoModulo = EnuSeguridad.Modulo.Contabilidad.CoModulo;
            public static int CoGrupo = 13;

            public const int Factura = 1;
            public const int Boleta = 3;
            public const int NotaCredito = 7;
            public const int NotaDebito = 8;
            public const int TicketMaquinaRegistradora = 12;
        };


        public static class DocumentoTributarioInventario
        {
            public static int CoModulo = EnuSeguridad.Modulo.Contabilidad.CoModulo;
            public static int CoGrupo = 20;
        };

        public static class DocumentoTributarioTransporteCompraVenta
        {
            public static int CoModulo = EnuSeguridad.Modulo.Contabilidad.CoModulo;
            public static int CoGrupo = 21;
        };

        public static class OperacionVenta
        {
            public static int CoModulo = EnuSeguridad.Modulo.Contabilidad.CoModulo;
            public static int CoGrupo = 14;
        };

        public static class ModoPago
        {
            public static int CoModulo = EnuSeguridad.Modulo.Contabilidad.CoModulo;
            public static int CoGrupo = 15;

            public const int Credito = 1;
            public const int Contado = 2;
        };

        public static class MedioPago
        {
            public static int CoModulo = EnuSeguridad.Modulo.Contabilidad.CoModulo;
            public static int CoGrupo = 16;

            public const int DepositoCuenta = 1;
            public const int Giro = 2;
            public const int Transferencias = 3;
            public const int OrdenPago = 4;
            public const int TarjetaDebito = 5;
            public const int TarjetaCredito = 6;
            public const int Cheque = 7;
            public const int Efectivo = 8;
            public const int EfectivoOperaciones = 9;
            public const int OtrosMedios = 999;
        };




        public static class Impuesto
        {
            public static int CoModulo = EnuSeguridad.Modulo.Contabilidad.CoModulo;
            public static int CoGrupo = 30;

            public const int IGV = 1;
            public const int ITF = 2;
        };


        public static class NotaCredito
        {
            public static int CoModulo = EnuSeguridad.Modulo.Contabilidad.CoModulo;
            public static int CoGrupo = 33;

            public const int AnulacioDeLaOperacion = 1;
            public const int AnulacionPorErrorEnElRUC = 2;
            public const int CorreccionPorErrorEnLaDescripción = 3;
            public const int DescuentoGlobal = 4;
            public const int DescuentoPorItem = 5;
            public const int DevolucionTotal = 6;
            public const int DevolucionParcial = 7;
            public const int Bonificacion = 8;
            public const int DisminucionEnElValor = 9;
        };







    }

    public static class Inventario
    {
        public static class TipoInventario
        {
            public static int CoModulo = EnuSeguridad.Modulo.Inventario.CoModulo;
            public static int CoGrupo = 19;
        };


    }
}
