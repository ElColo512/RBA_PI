using Microsoft.EntityFrameworkCore;
using RBA_PI.Infrastructure.Persistence.Entities;
using RBA_PI.Infrastructure.Persistence.ReadModels;

namespace RBA_PI.Infrastructure.Persistence;

public partial class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public virtual DbSet<CLIENTE> CLIENTEs { get; set; } = null!;

    public virtual DbSet<COMPROBANTES_ARCA> COMPROBANTES_ARCAs { get; set; } = null!;

    public virtual DbSet<CONFIGURACION> CONFIGURACIONs { get; set; } = null!;

    public virtual DbSet<ESTADOS_COMPROBANTE> ESTADOS_COMPROBANTEs { get; set; } = null!;

    public virtual DbSet<USUARIO> USUARIOs { get; set; } = null!;

    public virtual DbSet<VW_COMPROBANTES_ARCA> VW_COMPROBANTES_ARCAs { get; set; } = null!;

    public virtual DbSet<VW_RBA_ANALISIS_ARCA_TANGO> VW_RBA_ANALISIS_ARCA_TANGOs { get; set; } = null!;

    public virtual DbSet<VW_RBA_ANALISIS_ARCA_TANGO22> VW_RBA_ANALISIS_ARCA_TANGO22s { get; set; } = null!;

    public DbSet<AnalisisFacturaRow> AnalisisFacturaRows { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CLIENTE>(entity =>
        {
            entity.HasKey(e => e.COD_CLIENTE);

            entity.ToTable("CLIENTES");

            entity.Property(e => e.COD_CLIENTE).HasColumnType("numeric(6, 0)");
            entity.Property(e => e.CUIT)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.DOMICILIO)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LOCALIDAD)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NOTAS)
                .HasMaxLength(4000)
                .IsUnicode(false);
            entity.Property(e => e.RAZON_SOCIAL)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.TELEFONO)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<COMPROBANTES_ARCA>(entity =>
        {
            entity.HasKey(e => e.ID_COMPROBANTE_AFIP).HasName("PK_COMPROBANTES_AFIP");

            entity.ToTable("COMPROBANTES_ARCA");

            entity.HasIndex(e => new { e.COD_AUTORIZACION, e.COD_CLIENTE }, "IX_COMPROBANTES_AFIP").IsUnique();

            entity.Property(e => e.ID_COMPROBANTE_AFIP)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.COD_AUTORIZACION)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.COD_CLIENTE).HasColumnType("numeric(6, 0)");
            entity.Property(e => e.COD_ESTADO_COMPROBANTE)
                .HasDefaultValue(1m, "DF_COMPROBANTES_ARCA_COD_ESTADO_COMPROBANTE")
                .HasColumnType("numeric(18, 0)");
            entity.Property(e => e.IMP_EXENTO).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.IMP_NETO).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.IMP_NO_GRAVADO).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.IMP_TOTAL).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.IVA).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.IVA_105).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.IVA_21).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.IVA_25).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.IVA_27).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.IVA_5).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.MONEDA)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NCOMP_IN_C).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.NETO_105).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.NETO_21).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.NETO_25).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.NETO_27).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.NETO_5).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.NETO_GRAVADO_0).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.NRO_COMPROBANTE).HasColumnType("numeric(8, 0)");
            entity.Property(e => e.NRO_DOCUMENTO)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OTROS_TRIBUTOS).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.PTO_VENTA).HasColumnType("numeric(5, 0)");
            entity.Property(e => e.RAZON_SOCIAL)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TIPO)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TIPO_CAMBIO).HasColumnType("numeric(1, 0)");
            entity.Property(e => e.TIPO_DOC)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CONFIGURACION>(entity =>
        {
            entity.HasKey(e => e.COD_CLIENTE).HasName("PK_RBA_CONFIGURACION_PI");

            entity.ToTable("CONFIGURACION");

            entity.Property(e => e.COD_CLIENTE).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.CC_BASE_DATOS)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CC_PASSWORD)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CC_SERVIDOR)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CC_USUARIO)
                .HasMaxLength(50)
                .IsUnicode(false);        
            entity.Property(e => e.COD_IVA_105)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.COD_IVA_21)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.COD_IVA_27)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.COND_COMPRA)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.ID_ASIENTO_MODELO_CP).HasColumnType("numeric(2, 0)");
        });

        modelBuilder.Entity<ESTADOS_COMPROBANTE>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ESTADOS_COMPROBANTES");

            entity.Property(e => e.COD_ESTADO_COMPROBANTE).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.DESC_ESTADO_COMPROBANTE)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<USUARIO>(entity =>
        {
            entity.HasKey(e => e.ID_USUARIO);

            entity.ToTable("USUARIOS");

            entity.Property(e => e.ID_USUARIO).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.COD_CLIENTE).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.MAIL)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NOMBRE_COMPLETO)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.PASSWORD)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VW_COMPROBANTES_ARCA>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_COMPROBANTES_ARCA");

            entity.Property(e => e.COD_CLIENTE).HasColumnType("numeric(6, 0)");
            entity.Property(e => e.COD_ESTADO_COMPROBANTE).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.COMPROBANTE)
                .HasMaxLength(19)
                .IsUnicode(false);
            entity.Property(e => e.CUIT)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(63)
                .IsUnicode(false);
            entity.Property(e => e.FECHA_A_MOSTRAR)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ID_COMPROBANTE_AFIP).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.IMP_EXENTO)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("IMP.EXENTO");
            entity.Property(e => e.IMP_NETO)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("IMP.NETO");
            entity.Property(e => e.IMP_NO_GRAVADO)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("IMP.NO_GRAVADO");
            entity.Property(e => e.IMP_TOTAL).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.IVA).HasColumnType("numeric(23, 2)");
            entity.Property(e => e.IVA_TOTAL).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.MONEDA)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NCOMP_IN_C).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.OTROS_TRIBUTOS).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.RAZON_SOCIAL)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.n_COMPROBANTE)
                .HasMaxLength(14)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VW_RBA_ANALISIS_ARCA_TANGO>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_RBA_ANALISIS_ARCA_TANGO");

            entity.Property(e => e.COD_CLIENTE).HasColumnType("numeric(6, 0)");
            entity.Property(e => e.COD_ESTADO_COMPROBANTE).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.CUIT)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FECHA_A_MOSTRAR)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ID_COMPROBANTE_AFIP).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.IMP_TOTAL).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.IVA).HasColumnType("numeric(23, 2)");
            entity.Property(e => e.IVA_TOTAL).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.RAZON_SOCIAL)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.comprobante)
                .HasMaxLength(19)
                .IsUnicode(false);
            entity.Property(e => e.moneda)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VW_RBA_ANALISIS_ARCA_TANGO22>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_RBA_ANALISIS_ARCA_TANGO22");

            entity.Property(e => e.COD_CLIENTE).HasColumnType("numeric(6, 0)");
            entity.Property(e => e.COD_ESTADO_COMPROBANTE).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.CUIT)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(63)
                .IsUnicode(false);
            entity.Property(e => e.FECHA_A_MOSTRAR)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ID_COMPROBANTE_AFIP).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.IMP_TOTAL).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.IVA).HasColumnType("numeric(23, 2)");
            entity.Property(e => e.IVA_TOTAL).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.RAZON_SOCIAL)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.comprobante)
                .HasMaxLength(19)
                .IsUnicode(false);
            entity.Property(e => e.moneda)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AnalisisFacturaRow>().HasNoKey().ToView(null);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
