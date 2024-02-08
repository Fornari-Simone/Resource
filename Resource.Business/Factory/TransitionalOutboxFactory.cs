using Confluent.Kafka.Admin;
using Resource.Repository.Model;
using Resource.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utility.Kafka.Constants;
using Utility.Kafka.Messages;

namespace Resource.Business.Factory
{
    public static class TransitionalOutboxFactory
    {
        public static TransitionalOutbox CreatInsert(ResourceDTO resourceDTO)
        {
            return Create(resourceDTO, Operations.Insert);
        }

        private static TransitionalOutbox Create(ResourceDTO resourceDTO, string insert)
        {
            return Create(nameof(ResourceDb), resourceDTO, insert);
        }

        private static TransitionalOutbox Create<DTO>(string v, DTO resourceDTO, string insert) where DTO : class, new()
        {
            OperationMessage<DTO> operationMessage = new OperationMessage<DTO>()
            {
                Dto = resourceDTO,
                Operation = insert
            };
            operationMessage.CheckMessage();

            return new TransitionalOutbox()
            {
                Tabella = v,
                Messaggio = JsonSerializer.Serialize(operationMessage)
            };
        }
    }
}
