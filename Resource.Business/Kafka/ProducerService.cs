using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Resource.Repository.Abstraction;
using Resource.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Kafka.Abstractions.Clients;
using Utility.Kafka.Services;

namespace Resource.Business.Kafka
{
    public class ProducerService : ProducerService<KafkaTopicsOutput>
    {
        public ProducerService(ILogger<ProducerService<KafkaTopicsOutput>> logger, IProducerClient producerClient, IAdministatorClient adminClient, IOptions<KafkaTopicsOutput> optionsTopics, IOptions<KafkaProducerServiceOptions> optionsProducerService, IServiceScopeFactory serviceScopeFactory) : 
            base(logger, producerClient, adminClient, optionsTopics, optionsProducerService, serviceScopeFactory)
        {
        }

        protected override async Task OperationsAsync(CancellationToken cancellation)
        {
            using IServiceScope scope = ServiceScopeFactory.CreateScope();
            IRepository repository = scope.ServiceProvider.GetRequiredService<IRepository>();

            Logger.LogInformation("Acquisizione TransitionalOutbox");
            IEnumerable<TransactionalOutbox> transactionalOutboxes = (await repository.GetAllTransactionalOutbox(cancellation)).OrderBy(x => x.ID);
            if(!transactionalOutboxes.Any())
            {
                Logger.LogInformation($"Nessun TransitionalOutbox");
                return;
            }

            Logger.LogInformation("{Count} TransitionalOutbox", transactionalOutboxes.Count());

            foreach (TransactionalOutbox item in transactionalOutboxes)
            {
                string message = $"del record {nameof(TransactionalOutbox)
                    } con {nameof(TransactionalOutbox.ID)} = {item.ID}, {nameof(TransactionalOutbox.Tabella)
                    } = '{item.Tabella}' e {nameof(TransactionalOutbox.Messaggio)} = '{item.Messaggio}'";

                Logger.LogInformation("Elaborazione {message}", message);

                try
                {
                    Logger.LogInformation("Topic...");
                    string topic = item.Tabella switch
                    {
                        nameof(ResourceDb) => KafkaTopics.Resource,
                        _ => throw new ArgumentOutOfRangeException($"La tabella {item.Tabella} non è prevista come topic nel Producer")
                    };

                    Logger.LogInformation("Scrittura sul topic: {topic}", topic);
                    await ProducerClient.ProduceAsync(topic, item.Messaggio, cancellation);

                    Logger.LogInformation("Eliminazione {message}", message);
                    await repository.DeleteTransactionalOutbox(item.ID, cancellation);

                    await repository.SaveChangesAsync(cancellation);
                    Logger.LogInformation("Record Eliminato");
                } catch (Exception ex)
                {
                    Logger.LogError(ex, "Errore nel metodo ProducerService.OperationsAsync durante l'elaborazione {message} : {ex}", message, ex);
                }
            }
        }
    }
}
