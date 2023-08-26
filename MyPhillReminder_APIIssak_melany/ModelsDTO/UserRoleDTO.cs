namespace MyPhillReminder_APIIssak_melany.ModelsDTO
{
    //Un DTO sirve para simplificar la estructura de los json que se envian y llegan 
    //a los end points de los controllers quitando compocisiones innesesarias que 
    //solo lo haran los json mas pesados. Oculta la estructura real de los models
    //y por tanto de las tablas de base de datos
    public class UserRoleDTO
    {
        public int UserRole { get; set; }
        public string Description { get; set; } = null!;
    }
}
