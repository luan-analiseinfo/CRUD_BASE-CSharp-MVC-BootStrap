//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SM_CUSTEIO_WEB.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Produto
    {
        public Produto()
        {
            this.MateriaPrima = new HashSet<MateriaPrima>();
            this.ProdutoMaterial = new HashSet<ProdutoMaterial>();
        }
    
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Nullable<decimal> Preco { get; set; }
        public int Empresa_Id { get; set; }
    
        public virtual Empresa Empresa { get; set; }
        public virtual ICollection<MateriaPrima> MateriaPrima { get; set; }
        public virtual ICollection<ProdutoMaterial> ProdutoMaterial { get; set; }
    }
}
