using ForevarLibrary.Entities;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForevarLibrary.Repositories
{
    public class PlaceRepository
    {
        private CloudTable placeTable = null;
        private string connectionString = Environment.GetEnvironmentVariable("CNN_STR");

        public PlaceRepository()
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);

            var tableClient = storageAccount.CreateCloudTableClient();

            placeTable = tableClient.GetTableReference("Place");

        }

        public IEnumerable<PlaceEntity> GetAll()
        {
            var query = new TableQuery<PlaceEntity>();

            var entities = placeTable.ExecuteQuery(query);

            return entities;
        }

        public PlaceEntity GetById(string partitionKey, string rowKey)
        {
            var operation = TableOperation.Retrieve<PlaceEntity>(partitionKey, rowKey);
           
            var result = placeTable.Execute(operation);

            return result.Result as PlaceEntity;
        }

        public IEnumerable<PlaceEntity> GetById(string placeId)
        {
            string filter = TableQuery.GenerateFilterCondition("PlaceId", QueryComparisons.Equal, placeId);
         
            var query = new TableQuery<PlaceEntity>().Where(filter);

            var entity = placeTable.ExecuteQuery(query);

            return entity;
        }

        public IEnumerable<PlaceEntity> GetByCityId(string cityId)
        {
            string filter = TableQuery.GenerateFilterCondition("CityId", QueryComparisons.Equal, cityId);
            
            var query = new TableQuery<PlaceEntity>().Where(filter);

            var entity = placeTable.ExecuteQuery(query);

            return entity;
        }

        public IEnumerable<PlaceEntity> GetByPlaceName(string placeName)
        {
            string filter = TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, placeName);
           
            var query = new TableQuery<PlaceEntity>().Where(filter);

            var entity = placeTable.ExecuteQuery(query);

            return entity;
        }

        public void Create (PlaceEntity entity)
        {
            var operation = TableOperation.Insert(entity);

            placeTable.Execute(operation);
        }

        public void Update (PlaceEntity entity)
        {
            var operation = TableOperation.Replace(entity);

            placeTable.Execute(operation);
        }

        public void Delete (PlaceEntity entity)
        {
            var operation = TableOperation.Delete(entity);

            placeTable.Execute(operation);
        }
    }
}
