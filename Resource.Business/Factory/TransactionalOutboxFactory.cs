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
    public static class TransactionalOutboxFactory
    {
        public static TransactionalOutbox CreateInsert(ResourceDTO resourceDTO)
        {
            return Create(resourceDTO, Operations.Insert);
        }

        public static TransactionalOutbox CreateDelete(ResourceDTO characterDTO)
        {
            return Create(characterDTO, Operations.Delete);
        }

        public static TransactionalOutbox CreateUpdate(ResourceDTO characterDTO)
        {
            return Create(characterDTO, Operations.Update);
        }
        private static TransactionalOutbox Create(ResourceDTO resourceDTO, string insert)
        {
            return Create(nameof(ResourceDb), resourceDTO, insert);
        }

        private static TransactionalOutbox Create<TDTO>(string v, TDTO resourceDTO, string insert) where TDTO : class, new()
        {
            OperationMessage<TDTO> operationMessage = new OperationMessage<TDTO>()
            {
                Dto = resourceDTO,
                Operation = insert
            };
            operationMessage.CheckMessage();

            return new TransactionalOutbox()
            {
                Tabella = v,
                Messaggio = JsonSerializer.Serialize(operationMessage)
            };
        }
    }
}
