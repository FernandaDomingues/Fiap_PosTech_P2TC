using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge2.Domain.Entities;

namespace TechChallenge2.Domain.Interfaces.Services
{
    public interface INoticiaService
    {
        Task<Noticia> Create(Noticia noticiaDTO);
        Task<Noticia> Update(Noticia noticiaDTO);
        Task Remove(int id);
        Task<Noticia> Get(int id);
        Task<List<Noticia>> Get();
        Task<Noticia> GetById(int id);
    }
}
