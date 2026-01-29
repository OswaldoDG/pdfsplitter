using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfInspector.Separador.models
{
    public enum EstadoRevision
    {
        Pendiente = 0,
        EnCurso = 1,
        Finalizada = 2,
        Cancelada = 3,
        SeparadoEnPdfs = 4,
        ProcesoPDF = 5,
        SinPartes = 6,
        Excluido = 10,
        Reproceso = 11,
        SinPDF = 12,
        FinalizadaConErrores = 13

    }
}
