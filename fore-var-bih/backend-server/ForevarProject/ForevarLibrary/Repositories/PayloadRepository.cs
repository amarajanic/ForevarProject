using ForevarLibrary.Entities;
using ForevarLibrary.Models;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForevarLibrary.Repositories
{
    public class PayloadRepository
    {
        private CloudTable payloadTable = null;
        private CloudTable deviceTable = null;
        private string connectionString = Environment.GetEnvironmentVariable("CNN_STR");
        public PayloadRepository()
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);

            var tableClient = storageAccount.CreateCloudTableClient();

            payloadTable = tableClient.GetTableReference("Observations");
            deviceTable = tableClient.GetTableReference("Device");
        }

        public IEnumerable<PayloadEntity> GetAll()
        {
            var query = new TableQuery<PayloadEntity>();

            var entities = payloadTable.ExecuteQuery(query);

            return entities;
        }

        public IEnumerable<PayloadEntity> GetByDeviceId(string deviceId)
        {
            string filter = TableQuery.GenerateFilterCondition("DeviceId", QueryComparisons.Equal, deviceId);

            var query = new TableQuery<PayloadEntity>().Where(filter);

            var entity = payloadTable.ExecuteQuery(query);

            return entity;
        }

        public void Create(PayloadEntity entity)
        {
            entity.PartitionKey = Guid.NewGuid().ToString();
            entity.RowKey = Guid.NewGuid().ToString();

            var operation = TableOperation.Insert(entity);

            payloadTable.Execute(operation);
        }

        public void UpdateDevice(PayloadEntity payload)
        {
            string filter = TableQuery.GenerateFilterCondition("DeviceId", QueryComparisons.Equal,payload.DeviceId);

            var query = new TableQuery<DeviceEntity>().Where(filter);

            var entity = deviceTable.ExecuteQuery(query).FirstOrDefault();
         
            if (entity != null)
            {
                var newEntity = new DeviceEntity
                {
                    PartitionKey = entity.PartitionKey,
                    RowKey = entity.RowKey,
                    DeviceId = entity.DeviceId,
                    PlaceId = entity.PlaceId,
                    RelativeHumidity = payload.Humidity,
                    RelativeTemperature = payload.Temperature,
                    PlaceName = entity.PlaceName,
                    AdministrationUnit = entity.AdministrationUnit,
                    CityId = entity.CityId,
                    CityName = entity.CityName,
                    DeviceLat = entity.DeviceLat,
                    DeviceLong = entity.DeviceLong
                };

                var operation = TableOperation.InsertOrReplace(newEntity);

                deviceTable.Execute(operation);
            }
            else
            {
                Console.WriteLine("Entity not found!");
            }

        }
    }
}
