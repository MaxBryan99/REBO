﻿using Bicimoto.Comun.Dto.Intercambio;
using Bicimoto.Firmado;
using Bicimoto.Servicio;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Bicimoto.WebApi.Controllers
{
    public class ConsultarTicketController : ApiController
    {
        private readonly IServicioSunatDocumentos _servicioSunatDocumentos;
        private readonly ISerializador _serializador;

        public ConsultarTicketController(IServicioSunatDocumentos servicioSunatDocumentos, ISerializador serializador)
        {
            _servicioSunatDocumentos = servicioSunatDocumentos;
            _serializador = serializador;
        }

        public async Task<EnviarDocumentoResponse> Post([FromBody] ConsultaTicketRequest request)
        {
            var response = new EnviarDocumentoResponse();

            try
            {
                _servicioSunatDocumentos.Inicializar(new ParametrosConexion
                {
                    Ruc = request.Ruc,
                    UserName = request.UsuarioSol,
                    Password = request.ClaveSol,
                    EndPointUrl = request.EndPointUrl
                });

                var resultado = _servicioSunatDocumentos.ConsultarTicket(request.NroTicket);

                if (!resultado.Exito)
                {
                    response.Exito = false;
                    response.MensajeRespuesta = resultado.MensajeError;
                }
                else
                    response = await _serializador.GenerarDocumentoRespuesta(resultado.ConstanciaDeRecepcion);
            }
            catch (Exception ex)
            {
                response.MensajeError = ex.Message;
                response.Pila = ex.StackTrace;
                response.Exito = false;
            }

            return response;
        }
    }
}