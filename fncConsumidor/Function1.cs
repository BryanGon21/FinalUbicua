using System;
using fncConsumidor.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace fncConsumidor
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async System.Threading.Tasks.Task RunAsync([ServiceBusTrigger("mudo", Connection = "MyConn")]string myQueueItem,
            [CosmosDB(
                    databaseName:"dbMudo",
                    collectionName:"Eventos",
                    ConnectionStringSetting = "strCosmos"
                    )]IAsyncCollector<object> datos,

            ILogger log)
        {
            try
            {

                log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
                var data = JsonConvert.DeserializeObject<Mudo>(myQueueItem);
                await datos.AddAsync(data);
            }

            catch (Exception ex)
            {
                log.LogError($"No fue posible insertar datos: {ex.Message}");
            }

        }
    }
}
