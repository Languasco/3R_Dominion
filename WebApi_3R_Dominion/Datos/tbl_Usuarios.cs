//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Usuarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Usuarios()
        {
            this.tbl_Aceesos_Evento = new HashSet<tbl_Aceesos_Evento>();
        }
    
        public int id_Usuario { get; set; }
        public Nullable<int> id_Personal { get; set; }
        public string nrodoc_usuario { get; set; }
        public string apellidos_usuario { get; set; }
        public string nombres_usuario { get; set; }
        public string email_usuario { get; set; }
        public Nullable<int> id_TipoUsuario { get; set; }
        public int id_Perfil { get; set; }
        public string fotourl { get; set; }
        public string login_usuario { get; set; }
        public string contrasenia_usuario { get; set; }
        public int estado { get; set; }
        public Nullable<int> usuario_creacion { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<int> usuario_edicion { get; set; }
        public Nullable<System.DateTime> fecha_edicion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Aceesos_Evento> tbl_Aceesos_Evento { get; set; }
        public virtual tbl_Perfil tbl_Perfil { get; set; }
    }
}
