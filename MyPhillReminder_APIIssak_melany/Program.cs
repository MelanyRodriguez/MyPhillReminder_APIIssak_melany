using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyPhillReminder_APIIssak_melany.Models;

namespace MyPhillReminder_APIIssak_melany
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //vamos a leer la etiqueta CNNSTR de appsettings.json para configurar la conexion 
            //a la base de datos
            var CnnStrBiulder= new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("CNNSTR"));

            //eliminamos del CNNSTR el dato del password ya que seria muy sencillo obtener
            //la info de conexion del usuario de SQL Server del archivo de config appsettings.json

            CnnStrBiulder.Password = "1234567";

            //CnnStrBuilder es un objeto que permite la construccion de cadenas de conexion a base de datos
            //se pueden modificar cada parte de la misma, pero al final debemos extraer un string con la info final
            string cnnStr = CnnStrBiulder.ConnectionString;

            //ahora conectamos el proyecto con la base de datos usando cnnStr
            builder.Services.AddDbContext<MyPhillReminderBDContext>(options => options.UseSqlServer(cnnStr));


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}