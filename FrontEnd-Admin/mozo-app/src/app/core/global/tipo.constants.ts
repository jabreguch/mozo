
export const TIPO_MAESTRO = {
  general: {
    tipoProducto: {
      grupo: 2
    },
    estadoCivil: {
      grupo: 5
    },
    formatoArchivo: {
      grupo: 6
    },
    pagina: {
      grupo: 7
    },
    calculoServicio: {
      grupo: 8
    },
    predio: {
      grupo: 9
    },
    persona: {
      grupo: 11
    },
    nivelPais: {
      grupo: 12
    },
    modoPago: {
      grupo: 15
    },
    etiquetaRedSocial: {
      grupo: 17
    },
    grupoSoporte: {
      grupo: 18
    },
    operacionInventario: {
      grupo: 19
    },
    situacionEducativa: {
      grupo: 23
    },
    redSocial: {
      grupo: 25,
      items: {
        telefonoMovil: 1,
        correoElectronico: 2,
        telefonoFijo: 3,
        redSocial: 4
      }
    },
    urlRedSocial: {
      grupo: 27
    },
    cuota: {
      grupo: 28
    },

    estadoRegistro: {
      grupo: 29
    },
    motivoAnulacionCredito: {
      grupo: 31
    },
    manzana: {
      grupo: 32
    },
    calidadImagen: {
      grupo: 34,
      items: {
        miniatura: 1,
        mediano: 2,
        grande: 3
      }
    },
    tabla: {
      grupo: 38,
      items: {
        maestro$persona: 1,
        seguridad$empresa: 2,
        empresa$blog: 3,
        empresa$cliente: 4,
        empresa$servicio: 5,
        empresa$imagenweb: 6,
        empresa$serviciocaracteristica: 7,
        condominio$predio: 8,
        condominio$predioalquiler: 9,
        soporte$soporte: 10,
        soporte$seguimiento: 11,
        catalogo$producto: 12,
        expediente$documento: 13,
        expediente$notificacion: 14,
        matricula$matriculapension: 15
      }
    }
  }
} as const;



export const FORMATO_ARCHIVO = {
  condominio: {
    predio: {
      minuta: 1
    },
    predioAlquiler: {
      contrato: 2,
    }
  },
  soporte: {
    soporte: {
      sustento: 3
    },
    seguimiento: {
      sustento: 13
    }
  },
  seguridad: {
    empresa: {
      logo: 4
    }
  },
  maestro: {
    persona: {
      foto: 5
    }
  },
  empresa: {
    blog: {
      imagen: 6
    },
    cliente: {
      foto: 9
    },
    servicio: {
      imagen: 10
    },
    imagenWeb: {
      imagen: 12
    },
    servicioCaracteristica: {
      imagen: 15
    }
  },
  boga: {
    documento: {
      escrito: 7
    },
    notificacion: {
      constancia: 8
    }
  },
  matricula: {
    matriculaPension: {
      recibo: 5
    }
  },
  catalogo: {
    producto: {
      imagen: 14
    }
  }
} as const;

export const TIPOS_GALERIA = new Set<number>([
  FORMATO_ARCHIVO.catalogo.producto.imagen,
  FORMATO_ARCHIVO.soporte.soporte.sustento,
  FORMATO_ARCHIVO.soporte.seguimiento.sustento
]);

export function esTipoGaleria(coTipo: number): boolean {
  return TIPOS_GALERIA.has(coTipo);
}

export const TIPO_PERSONA = {
  boga: {
    abogado: 9,
    notificado: 12,
    parte: 13
  },
  condominio: {
    inquilino: 4,
    propietario: 5
  },
  empresa: {
    bloguero: 10,
    cliente: 11,
    equipo: 20
  },
  facturacion: {
    cliente: 17,
    proveedor: 16,
    vendedor: 23
  },
  gasto: {
    beneficiario: 7,
    proveedor: 8
  },
  maestro: {
    entidad: 21
  },
  matricula: {
    alumno: 14,
    profesor: 15
  },
  seguridad: {
    sistema: 3
  },
  soporte: {
    soporte: 6
  },
  urbano: {
    cliente: 18,
    entidad: 22,
    vendedor: 19
  }
}
