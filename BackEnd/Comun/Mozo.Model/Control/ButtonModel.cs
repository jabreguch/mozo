using Mozo.Helper.Enu;

namespace Mozo.Model.Control;

public class ArgumentsModel : EventArgs
{

    //    public static T DeepCopyReflection<T>(T input)
    //    {
    //        var type = input.GetType();
    //        var properties = type.GetProperties();
    //        T clonedObj = (T)Activator.CreateInstance(type);
    //        foreach (var property in properties)
    //        {
    //            if (property.CanWrite)
    //            {
    //                object value = property.GetValue(input);
    //                if (value != null && value.GetType().IsClass && !value.GetType().FullName.StartsWith("System."))
    //                {
    //                    property.SetValue(clonedObj, DeepCopyReflection(value));
    //                }
    //                else
    //                {
    //                    property.SetValue(clonedObj, value);
    //                }
    //            }
    //        }
    //        return clonedObj;
    //    }



    public string? Method { get; set; }

    //private object? model;

    //despues borrar
    public object? Model { get; set; }
    public List<object>? ModelLst { get; set; }


    public string? Url { get; set; }



}

public class ButtonModel
{  //<i class="bi bi-check"></i>
    public class Setting
    {
        //public int CoAppearance { get; set; }
        public string? CssBtnLg { get; set; }
        public string? CssBtnStandart { get; set; }
        public string? CssBtnSmall { get; set; }
        public string? ValueDefault { get; set; }
        public string? ValueContrary { get; set; }
        public string? TooltipDefault { get; set; }
        public string? TooltipContrary { get; set; }

        public string? IconDefault { get; set; }
        public string? IconColor { get; set; }
        public string? IconContrary { get; set; }

    }
    private static readonly Dictionary<int, Setting> AppearanceButtonList
        = new()
        {
            {
               EnuCommon.Button.Theme.Accept,
                new Setting
                {
                    CssBtnLg = "btn btn-primary btn-lg me-2",
                    CssBtnStandart = "btn btn-primary me-2",
                    CssBtnSmall = "btn btn-primary btn-sm me-2",
                    ValueDefault = "Aceptar",
                    TooltipDefault = "Aceptar",
                    IconDefault = "bi bi-check",
                    IconColor = "cornflowerblue"
                }
            },

            {
                EnuCommon.Button.Theme.Export,
                new Setting
                {
                    CssBtnLg = "btn btn-outline-primary btn-lg me-2",
                    CssBtnStandart = "btn btn-outline-primary me-2",
                    CssBtnSmall = "btn btn-outline-primary btn-sm me-2",
                    ValueDefault = "Exportar",
                    TooltipDefault = "Exportar registros",
                    IconDefault = "bi bi-cloud-download-fill",
                    IconColor = "cornflowerblue"
                }
            },

            {
               EnuCommon. Button.Theme.New,
                new Setting
                {
                    CssBtnLg = "btn btn-success btn-lg me-2",
                    CssBtnStandart = "btn btn-success me-2",
                    CssBtnSmall = "btn btn-success btn-sm me-2",
                    ValueDefault = "Nuevo",
                    TooltipDefault = "Nuevo registro",
                    IconDefault = "bi bi-plus-lg",
                    IconColor = "success"
                }
            },
            {
                EnuCommon.Button.Theme.Save,
                new Setting
                {
                    CssBtnLg = "btn btn-primary btn-lg me-2",
                    CssBtnStandart = "btn btn-primary me-2",
                    CssBtnSmall = "btn btn-primary btn-sm me-2",
                    ValueDefault = "Grabar",
                    TooltipDefault = "Grabar registro",
                    IconDefault = "bi bi-floppy-fill",
                    IconColor = "blue"
                }
            },
            {
                EnuCommon.Button.Theme.Delete,
                new Setting
                {
                    CssBtnLg = "btn btn-danger btn-lg me-2",
                    CssBtnStandart = "btn btn-danger me-2",
                    CssBtnSmall = "btn btn-danger me-2",
                    ValueDefault = "Eliminar",
                    TooltipDefault = "Eliminar registro",
                    IconDefault = "bi bi-trash-fill",
                    IconColor = "red"
                }
            },

            {
                EnuCommon.Button.Theme.Search,
                new Setting
                {
                    CssBtnLg = "btn btn-primary btn-lg me-2",
                    CssBtnStandart = "btn btn-primary me-2",
                    CssBtnSmall = "btn btn-primary btn-sm me-2",
                    ValueDefault = "Buscar",
                    TooltipDefault = "Buscar registro(s)",
                    IconDefault =  "bi bi-search",
                    IconColor = "blue"
                }
            },

            {
                EnuCommon.Button.Theme.Report,
                new Setting
                {
                    CssBtnLg = "btn btn-outline-info btn-lg me-2",
                    CssBtnStandart = "btn btn-outline-info me-2",
                    CssBtnSmall = "btn btn-outline-info btn-sm me-2",
                    ValueDefault = "Reporte",
                    TooltipDefault = "Reporte",
                    IconDefault = "bi bi-file-text",
                     IconColor = "royalblue"
                }
            },
            {
                EnuCommon.Button.Theme.GoOut,
                new Setting
                {
                    CssBtnLg = "btn btn-warning btn-lg me-2",
                    CssBtnStandart = "btn btn-warning me-2",
                    CssBtnSmall = "btn btn-warning btn-sm me-2",
                    ValueDefault = "Salir",
                    TooltipDefault = "Salir",
                    IconDefault = "bi bi-box-arrow-right",
                    IconColor = "goldenrod"
                }
            },
            {
                EnuCommon.Button.Theme.Refresh,
                new Setting
                {
                    CssBtnLg = "btn btn-outline-success btn-lg me-2", CssBtnStandart = "btn btn-outline-success me-2",
                    CssBtnSmall = "btn btn-outline-success btn-sm me-2",
                    ValueDefault = "Actualizar",
                    TooltipDefault = "Actualizar información",
                    IconDefault = "bi bi-arrow-clockwise",
                    IconColor = "green"
                }
            },
            {
                EnuCommon.Button.Theme.Execute,
                new Setting
                {
                    CssBtnLg = "btn btn-outline-secondary btn-lg me-2",
                    CssBtnStandart = "btn btn-outline-secondary me-2",
                    CssBtnSmall = "btn btn-outline-secondary btn-sm me-2",
                    ValueDefault = "Ejecutar",
                    TooltipDefault = "Ejecutar",
                    IconDefault =   "bi bi-play-fill",
                    IconColor = "darkslategray"
                }
            },
            {
                EnuCommon.Button.Theme.Edit,
                new Setting
                {
                    CssBtnLg = "btn btn-outline-primary btn-lg me-2",
                    CssBtnStandart = "btn btn-outline-primary me-2",
                    CssBtnSmall = "btn btn-outline-primary btn-sm me-2",
                    ValueDefault = "Editar",
                    TooltipDefault = "Editar registro",
                    IconDefault = "bi bi-pencil-square",
                    IconColor = "blue"
                }
            },

            {
                EnuCommon.Button.Theme.Attach,
                new Setting
                {
                    CssBtnLg = "btn btn-info btn-lg me-2",
                    CssBtnStandart = "btn btn-info me-2",
                    CssBtnSmall = "btn btn-info btn-sm me-2",
                    ValueDefault = "Adjuntar",
                    TooltipDefault = "Adjuntar archivo",
                    IconDefault = "bi bi-paperclip",
                    IconColor = "teal"
                }
            },
            {
                EnuCommon.Button.Theme.Transfer,
                new Setting
                {
                    CssBtnLg = "btn btn-outline-dark btn-lg me-2",
                    CssBtnStandart = "btn btn-outline-dark me-2",
                    CssBtnSmall = "btn btn-outline-dark btn-sm me-2",
                    ValueDefault = "Traspasar",
                    TooltipDefault = "Traspasar",
                    IconDefault =   "bi bi-arrow-left-right",
                     IconColor = "black"
                }
            },
            {
                EnuCommon.Button.Theme.Select,
                new Setting
                {
                    CssBtnLg = "btn btn-outline-success btn-lg me-2", CssBtnStandart = "btn btn-outline-success me-2",
                    CssBtnSmall = "btn btn-outline-success btn-sm me-2",
                    ValueDefault = "Seleccionar",
                    TooltipDefault = "Selecionar registro",
                    IconDefault = "bi bi-hand-thumbs-up-fill",
                     IconColor = "green"
                }
            },

            {
                EnuCommon.Button.Theme.More,
                new Setting
                {
                    CssBtnLg = "btn btn-secondary btn-lg me-2",
                    CssBtnStandart = "btn btn-secondary me-2",
                    CssBtnSmall = "btn btn-secondary  btn-sm me-2",
                    ValueDefault = "Aumentar",
                    TooltipDefault = "Aumentar",
                    IconDefault = "bi bi-plus-circle-fill",
                     IconColor = "black"
                }
            },
            {
                EnuCommon.Button.Theme.Less,
                new Setting
                {
                    CssBtnLg = "btn btn-danger btn-lg me-2",
                    CssBtnStandart = "btn btn-danger me-2",
                    CssBtnSmall = "btn  btn-danger btn-sm me-2",
                    ValueDefault = "Disminuir",
                    TooltipDefault = "Disminuir",
                    IconDefault = "bi bi-dash-circle-fill" ,
                     IconColor = "red"
                }
            },
            {
               EnuCommon.Button.Theme.Check,
                new Setting
                {
                    CssBtnLg = null,
                    CssBtnStandart = null,
                    CssBtnSmall = null,
                    ValueDefault = "Activar",
                    ValueContrary = "Inactivar",
                    TooltipDefault = "Activo",
                    TooltipContrary = "Inactivo",
                    IconDefault = "bi bi-check-square",
                    IconContrary = "bi bi-square",
                    IconColor = "black"
                }
            },
            {
                EnuCommon.Button.Theme.Separator,
                new Setting
                {
                    CssBtnLg = null,
                    CssBtnStandart = null,
                    CssBtnSmall = null,
                    ValueDefault = null,
                }
            },
            {
                EnuCommon.Button.Theme.Link,
                new Setting
                {
                    CssBtnLg = null,
                    CssBtnStandart = null,
                    CssBtnSmall = null,
                    ValueDefault = "Link",
                    TooltipDefault = "Link",
                    IconDefault =  "bi bi-box-arrow-up-right",
                    IconColor = "blue"
                }
            },
            {
                EnuCommon.Button.Theme.Question,
                new Setting
                {
                    CssBtnLg = null,
                    CssBtnStandart = null,
                    CssBtnSmall = null,
                    ValueDefault = null,
                    TooltipDefault = null,
                    IconDefault = "bi bi-question-circle-fill",
                    IconColor = "teal"
                }
            },
            {
               EnuCommon.Button.Theme.ToggleOff,
                new Setting
                {
                    CssBtnLg = null, CssBtnStandart = null, CssBtnSmall = null,
                    ValueDefault = "Cambiar activo",
                    TooltipDefault = "Activo",
                    IconDefault = "bi bi-toggle-off",
                     IconColor = "black"
                }
            },
            {
                EnuCommon.Button.Theme.ToggleOn,
                new Setting
                {
                    CssBtnLg = null, CssBtnStandart = null, CssBtnSmall = null,
                    ValueDefault = "Cambiar inactivo",
                    TooltipDefault = "Inactivo",
                    IconDefault = "bi bi-toggle-on",
                       IconColor = "black"
                }
            },

            {
                EnuCommon.Button.Theme.Filter,
                new Setting
                {
                    CssBtnLg = "btn btn-outline-primary btn-lg me-2",
                    CssBtnStandart = "btn btn-outline-primary me-2",
                    CssBtnSmall = "btn  btn-outline-primary btn-sm me-2",
                    ValueDefault = "Filtro",
                    TooltipDefault = "Filtrar registros",
                    IconDefault = "bi bi-funnel-fill",
                       IconColor = "blue"
                }
            },
            {
                EnuCommon.Button.Theme.Reset,
                new Setting
                {
                    CssBtnLg = "btn btn-light btn-lg me-2",
                    CssBtnStandart = "btn btn-light me-2",
                    CssBtnSmall = "btn  btn-light btn-sm me-2",
                    ValueDefault = "Restaurar",
                    TooltipDefault = "Restaurar registros",
                    IconDefault = "bi bi-x-diamond-fill",
                      IconColor = "purple"
                }
            },
             {
                EnuCommon.Button.Theme.Back,
                new Setting
                {
                     CssBtnLg = "btn btn-warning btn-lg me-2",
                    CssBtnStandart = "btn btn-warning me-2",
                    CssBtnSmall = "btn btn-warning btn-sm me-2",
                    ValueDefault = "Atras",
                    TooltipDefault = "Atras",
                    IconDefault = "bi bi-arrow-left-circle-fill",
                    IconColor = "goldenrod"
                }
            }


            ////btn btn-sm btn-white btn-active-light-primary me-2
        };


    private string? _txTooltip;
    private string? _txValue;

    private readonly Setting Settingg;

    public int CoTheme { get; set; }
    public int? CoEstReg { get; set; }
    public ButtonModel(int coTheme, int? coEstReg = null)
    {
        CoTheme = coTheme;
        Settingg = AppearanceButtonList[coTheme];
        CoEstReg = coEstReg;
    }







    //public string? TxNameSpace { get; set; }


    public string? TxTarget { get; set; }

    // public object? Model { get; set; }
    public string? TxDataCodigo { get; set; }
    public int? LinkTarget { get; set; } = EnuCommon.Button.Theme._Link.Blank;

    /// <summary>
    /// despues borrar
    /// </summary>
    public ArgumentsModel? Arguments { get; set; } = null;
    //public Action<ArgumentsModel> Action { get; set; } = e => { };
    /// <summary>
    ///  fin despues borrar
    /// </summary>
   // public string? Method { get; set; }

    //public ModalModel? Modal { get; set; } = null;
    public int Size { get; set; } = EnuCommon.Button.Size.Standart;
    public int Tipo { get; set; } = EnuCommon.Button.Tipo.Button;

    public string? GetTipo
    {
        get
        {
            if (Tipo == EnuCommon.Button.Tipo.Button)
                return "button";
            if (Tipo == EnuCommon.Button.Tipo.Submit)
                return "submit";
            if (Tipo == EnuCommon.Button.Tipo.Reset)
                return "reset";
            return "Submit";

        }
    }

    //public bool FlTieneIcono { get; set; } = true;

    public string? TxCss
    {
        get
        {
            if (Size == EnuCommon.Button.Size.Lg) return Settingg.CssBtnLg;
            if (Size == EnuCommon.Button.Size.Standart) return Settingg.CssBtnStandart;
            if (Size == EnuCommon.Button.Size.Small) return Settingg.CssBtnSmall;
            return null;
        }
    }

    public string? TxValue
    {
        get
        {
            if (CoTheme == EnuCommon.Button.Theme.Check)
            {
                if (CoEstReg == 1) return Settingg.ValueContrary;
                return Settingg.ValueDefault;
            }
            else
            {
                if (_txValue == null) return Settingg.ValueDefault;
                return _txValue;
            }
        }
        set => _txValue = value;
    }

    public string? Icon
    {
        get
        {
            if (CoTheme == EnuCommon.Button.Theme.Check)
            {
                if (CoEstReg == 1) return Settingg.IconContrary;
                return Settingg.IconDefault;
            }
            else
                return Settingg.IconDefault;
        }
    }

    public string? IconColor
    {
        get
        {
            return Settingg.IconColor;
        }
    }


    public string? TxTooltip
    {
        get
        {
            if (CoTheme == EnuCommon.Button.Theme.Check)
            {
                if (CoEstReg == 0)
                    return Settingg.ValueContrary;
                else
                    return Settingg.ValueDefault;
            }
            else
            {
                if (_txTooltip == null) return Settingg.TooltipDefault;
                return _txTooltip;
            }
        }
        set => _txTooltip = value;
    }




}