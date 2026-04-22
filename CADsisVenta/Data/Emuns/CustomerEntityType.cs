namespace CADsisVenta.Data.Emuns
{
    using System.ComponentModel.DataAnnotations;

    public enum CustomerEntityType
    {
        [Display(Name = "Persona Natural")]
        PersonaNatural = 1,

        [Display(Name = "Sociedad Anónima (S.A.)")]
        SociedadAnonima = 2,

        [Display(Name = "Compañía de Responsabilidad Limitada (Cía. Ltda.)")]
        ResponsabilidadLimitada = 3,

        [Display(Name = "Sociedad por Acciones Simplificadas (S.A.S.)")]
        SociedadAccionesSimplificada = 4,

        [Display(Name = "Compañía de Economía Mixta")]
        EconomiaMixta = 5,

        [Display(Name = "Sociedad de Hecho")]
        SociedadDeHecho = 6,

        [Display(Name = "Institución Pública")]
        SectorPublico = 7,

        [Display(Name = "Organización Sin Fines de Lucro")]
        SinFinesDeLucro = 8
    }
}
