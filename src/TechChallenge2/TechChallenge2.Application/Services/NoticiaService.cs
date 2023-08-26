using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge2.Domain.Entities;
using TechChallenge2.Domain.Interfaces.Repositories;
using TechChallenge2.Domain.Interfaces.Services;

namespace TechChallenge2.Application.Services
{
    public class NoticiaService : INoticiaService
    {
        private readonly IMapper _mapper;
        private readonly INoticiaRepository _noticiaRepository;


        public NoticiaService(IMapper mapper, INoticiaRepository noticiaRepository)
        {
            _mapper = mapper;
            _noticiaRepository = noticiaRepository;
        }
        public async Task<Noticia> Create(Noticia noticiaDTO)
        {
            var noticia = _mapper.Map<Noticia>(noticiaDTO);

            return await _noticiaRepository.Create(noticia);
        }

        public async Task<Noticia> Get(int id)
        {
            return await _noticiaRepository.Get(id);
        }

        public async Task<List<Noticia>> Get()
        {
            return await _noticiaRepository.Get();
        }

        public async Task Remove(int id)
        {
            await _noticiaRepository.Remove(id);
        }

        public async Task<Noticia> GetById(int id)
        {
            return await _noticiaRepository.GetById(id);
        }

        public async Task<Noticia> Update(Noticia noticiaDto)
        {
            var noticia = _mapper.Map<Noticia>(noticiaDto);

            return await _noticiaRepository.Update(noticia);
        }
    }
}
