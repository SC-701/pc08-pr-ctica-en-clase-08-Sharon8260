using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class ProductoBase
    {
        [Required(ErrorMessage ="El nombre es obligatorio")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\s]+$",
        ErrorMessage = "El nombre solo puede contener letras, números y espacios")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "El código de barras es obligatorio")]
        [StringLength(20, MinimumLength = 8,
            ErrorMessage = "El código de barras debe tener entre 8 y 20 caracteres")]
        [RegularExpression(@"^[0-9]+$",
            ErrorMessage = "El código de barras solo puede contener números")]
        public string CodigoBarras { get; set; }
    }

        public class ProductoRequest : ProductoBase
        {
            public Guid IdSubCategoria { get; set; }
        }

        public class ProductoResponse : ProductoBase
        {
            public Guid Id { get; set; }
            public string SubCategoria { get; set; }
            public string Categoria { get; set; }
            public string PrecioUSD { get; set; }

    }
}

