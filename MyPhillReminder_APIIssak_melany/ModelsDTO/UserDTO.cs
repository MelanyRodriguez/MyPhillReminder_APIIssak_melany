namespace MyPhillReminder_APIIssak_melany.ModelsDTO
{
    public class UserDTO
    {
        public int UsuarioID { get; set; }
        public string Correo { get; set; } = null!;
        public string Contrasenia { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string RespaldoCorreo { get; set; } = null!;
        public string NumeroTelefono { get; set; } = null!;
        public string? Direccion { get; set; }
        public bool Activado { get; set; }
        public bool EstaBloqueado { get; set; }
        public int RolUsuarioID { get; set; }
        public string DescripcionRol { get; set; } = null!;

        
    }
}
