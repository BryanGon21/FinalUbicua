using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using ApiProductor.Models;
using Newtonsoft.Json;

namespace ApiProductor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MudoController : ControllerBase
    {
        [HttpPost]
        public async Task<bool> EnviarAsync([FromBody] Mudo mudo)
        {
            string connectionString = "Endpoint=sb://queuemudo.servicebus.windows.net/;SharedAccessKeyName=enviar;SharedAccessKey=tY9+sWBPqWJwdMQGo+h1Hel2dmbGRC2KBXQRWPqdn6M=;EntityPath=mudo";
            string queueName = "mudo";
            String mensaje = JsonConvert.SerializeObject(mudo);
            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                // create a sender for the queue 
                ServiceBusSender sender = client.CreateSender(queueName);

                // create a message that we can send
                ServiceBusMessage message = new ServiceBusMessage(mensaje);

                // send the message
                await sender.SendMessageAsync(message);
                Console.WriteLine($"Sent a single message to the queue: {queueName}");
            }

            return true;
        }
    }
}
