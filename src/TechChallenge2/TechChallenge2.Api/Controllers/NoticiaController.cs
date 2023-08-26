using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Transactions;
using TechChallenge2.Domain.Entities;
using TechChallenge2.Domain.Entities.Request;
using TechChallenge2.Domain.Entities.Response;
using TechChallenge2.Domain.Exceptions;
using TechChallenge2.Domain.Interfaces.Services;

namespace TechChallenge2.Api.Controllers
{
    [ApiController]
    [Route("api/v1/noticia")]
    public class NoticiaController : ControllerBase
    {
        private readonly INoticiaService _noticiaService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Controle responsável pela noticia
        /// </summary>
        /// <param name="noticiaService"></param>
        /// <param name="mapper"></param>
        public NoticiaController(INoticiaService noticiaService, IMapper mapper)
        {
            _noticiaService = noticiaService;
            _mapper = mapper;
        }

        /// <summary>
        /// Endpoint responsável por cadastrar nova noticia
        /// </summary>
        /// <param name="noticiaRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("create")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Create([FromForm] NoticiaRequest noticiaRequest)
        {
            try
            {
                var noticia = _mapper.Map<Noticia>(noticiaRequest);
                var noticiaCreated = await _noticiaService.Create(noticia);

                return Ok(new BaseResponse
                {
                    Message = "Solicitação cadastrada com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = noticiaCreated
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponsesExceptions.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponsesExceptions.ApplicationErrorMessage());
            }
        }

        /// <summary>
        /// Endpoint responsável por alterar noticia
        /// </summary>
        /// <param name="noticiaRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPut("update")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Update([FromForm][Required] int id,[FromForm] NoticiaRequest noticiaRequest)
        {
            try
            {
                var noticia = _mapper.Map<Noticia>(noticiaRequest);
                noticia.Id = id;
                var noticiaUpdated = await _noticiaService.Update(noticia);

                return Ok(new BaseResponse
                {
                    Message = "Notícia alterada com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = noticiaUpdated
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponsesExceptions.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponsesExceptions.ApplicationErrorMessage());
            }
        }

        /// <summary>
        /// Endpoint responsável por buscar todas as noticias
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("get")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Get()
        {
            try
            {
                var listaNoticiaS = await _noticiaService.Get();

                return Ok(new BaseResponse
                {
                    Message = "Notícias resgatadas com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = listaNoticiaS
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponsesExceptions.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponsesExceptions.ApplicationErrorMessage());
            }
        }

        /// <summary>
        /// Endpoint responsável por buscar noticia pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("getById")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {

                var noticia = await _noticiaService.GetById(id);

                return Ok(new BaseResponse
                {
                    Message = "Notícia resgatada com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = noticia
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponsesExceptions.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponsesExceptions.ApplicationErrorMessage());
            }
        }

        ///// <summary>
        ///// Endpoint responsável por cadastrar nova noticia
        ///// </summary>
        ///// <param name="noticiaRequest"></param>
        ///// <returns></returns>
        //[ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        //[HttpPatch("deletar")]
        //[Consumes("multipart/form-data")]
        //public async Task<ActionResult> Delete([FromForm] NoticiaRequest noticiaRequest)
        //{
        //    try
        //    {
        //        var noticia = _mapper.Map<Noticia>(noticiaRequest);
        //        var noticiaCreated = await _noticiaService.Create(noticia);

        //        return Ok(new BaseResponse
        //        {
        //            Message = "Solicitação cadastrada com sucesso!",
        //            Success = true,
        //            Errors = null,
        //            Data = noticiaCreated
        //        });
        //    }
        //    catch (DomainException ex)
        //    {
        //        return BadRequest(ResponsesExceptions.DomainErrorMessage(ex.Message, ex.Errors));
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, ResponsesExceptions.ApplicationErrorMessage());
        //    }
        //}
    }
}
