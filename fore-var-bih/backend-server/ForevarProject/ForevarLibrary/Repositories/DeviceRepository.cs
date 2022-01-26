using ForevarLibrary.Entities;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForevarLibrary.Repositories
{
    public class DeviceRepository
    {
        private CloudTable deviceTable = null;
        private string connectionString = Environment.GetEnvironmentVariable("CNN_STR");

        public DeviceRepository()
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);

            var tableClient = storageAccount.CreateCloudTableClient();

            deviceTable = tableClient.GetTableReference("Device");
        }

        public IEnumerable<DeviceEntity> GetAll()
        {
            var query = new TableQuery<DeviceEntity>();

            var entities = deviceTable.ExecuteQuery(query);

            return entities;
        }

        public DeviceEntity GetById(string partitionKey, string rowKey)
        {
            var operation = TableOperation.Retrieve<DeviceEntity>(partitionKey, rowKey);

            var result = deviceTable.Execute(operation);

            return result.Result as DeviceEntity;
        }

        public IEnumerable<DeviceEntity> GetByPlaceId(string placeId)
        {
            string filter = TableQuery.GenerateFilterCondition("PlaceId", QueryComparisons.Equal, placeId);

            var query = new TableQuery<DeviceEntity>().Where(filter);

            var entity = deviceTable.ExecuteQuery(query);

            return entity;
        }

        public IEnumerable<DeviceEntity> GetByDeviceId(string deviceId)
        {
            string filter = TableQuery.GenerateFilterCondition("DeviceId", QueryComparisons.Equal, deviceId);
     
            var query = new TableQuery<DeviceEntity>().Where(filter);

            var entity = deviceTable.ExecuteQuery(query);

            return entity;
        }

        public IEnumerable<DeviceEntity> GetByCityId(string cityId)
        {
            string filter = TableQuery.GenerateFilterCondition("CityId", QueryComparisons.Equal,cityId);

            var query = new TableQuery<DeviceEntity>().Where(filter);

            var entity = deviceTable.ExecuteQuery(query);

            return entity;
        }

        public void Create(DeviceEntity entity)
        {
            var operation = TableOperation.Insert(entity);

            deviceTable.Execute(operation);
        }

        public void Update(DeviceEntity entity)
        {
            var operation = TableOperation.Replace(entity);

            deviceTable.Execute(operation);
        }

        public void Delete(DeviceEntity entity)
        {
            var operation = TableOperation.Delete(entity);

            deviceTable.Execute(operation);
        }
    }
}
