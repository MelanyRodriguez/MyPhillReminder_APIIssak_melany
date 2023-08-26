using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using MyPhillReminder_APIIssak_melany.Attributes;
using MyPhillReminder_APIIssak_melany.Models;
using MyPhillReminder_APIIssak_melany.ModelsDTO;

namespace MyPhillReminder_APIIssak_melany.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiKey]

    public class UsersController : ControllerBase
    {
        private readonly MyPhillReminderBDContext _context;

        public UsersController(MyPhillReminderBDContext context)
        {
            _context = context;
        }

        //Este get valida el usuario que quiere ingresar a la app
        // GET: api/Users
        [HttpGet("ValidateLogin")]

        public async Task<ActionResult<User>> ValidateLogin(string username, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(e => e.Email.Equals(username) && 
                                                                 e.Password==password);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }





        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        // GET: api/Users/
        [HttpGet("GetUserInfoByEmail")]
        public ActionResult<IEnumerable<UserDTO>> GetUserInfoByEmail(string Pemail)
        {
            //creamos el linq que conbina la info de las dos entidades y la agreaga
            //en el objeto DTO del usuario
            var query = (from u in _context.Users
                         join ur in _context.UserRoles on
                         u.UserID equals ur.UserRoleId
                         where u.Email == Pemail && u.Active == true &&
                         u.IsBlocked == false
                         select new
                         {
                             usuarioId=u.UserID,
                             correo=u.Email,
                             contrasenia=u.Password,
                             nombre=u.Name,
                             respaldocorreo=u.BackUpEmail,
                             numerotelefono=u.PhoneNumber,
                             direccion=u.Address,
                             activado=u.Active,
                             establoqueado=u.IsBlocked,
                             rolusuarioID=ur.UserRoleId,
                             descripcionrol=ur.Description
                         }).ToList();

            //creamos un objeto que retorna la funcion
            List<UserDTO> list = new List<UserDTO>();

            foreach (var item in query)
            {
                UserDTO NewItem = new UserDTO()
                {
                    UsuarioID = item.usuarioId,
                    Correo = item.correo,
                    Contrasenia = item.contrasenia,
                    Nombre = item.nombre,
                    RespaldoCorreo = item.respaldocorreo,
                    NumeroTelefono = item.numerotelefono,
                    Direccion = item.direccion,
                    Activado = item.activado,
                    EstaBloqueado = item.establoqueado,
                    RolUsuarioID = item.rolusuarioID,
                    DescripcionRol = item.descripcionrol
                };
                list.Add(NewItem);
            }
            if (list == null) { return NotFound(); }
            return list;
        }


        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDTO user)
        {
            if (id != user.UsuarioID)
            {
                return BadRequest();
            }

            User? NewEFUser = GetUserByID(id);

            if (NewEFUser != null)
            {
                NewEFUser.Email = user.Correo;
                NewEFUser.Name = user.Nombre;
                NewEFUser.BackUpEmail = user.RespaldoCorreo;
                NewEFUser.PhoneNumber = user.NumeroTelefono;
                NewEFUser.Address = user.Direccion;

                _context.Entry(NewEFUser).State = EntityState.Modified;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }

                else
                {
                    throw;
                }
              
            }
            return Ok();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'MyPhillReminderBDContext.Users'  is null.");
          }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserID }, user);
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserID == id)).GetValueOrDefault();
        }

        private User? GetUserByID(int id)
        {
            var user = _context.Users?.Find(id);
            return user;
        }
    }
}
