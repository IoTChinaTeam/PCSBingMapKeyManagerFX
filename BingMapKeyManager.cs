using System;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PCSBingMapKeyManagerFX.Properties;

namespace PCSBingMapKeyManager
{
    internal class BingMapKeyManager
    {
        private const string BingMapKeyKey = "BingMapKey";

        private readonly DocumentClient documentClient;
        private readonly string databaseId;
        private readonly string collectionId;
        private readonly string documentId;
        private readonly string logicCollectionId;
        private readonly string logicKey;
        private readonly string collectionLink;
        private readonly string documentLink;

        public BingMapKeyManager()
        {
            var url = Settings.Default.Url;
            var key = Settings.Default.Key;
            databaseId = Settings.Default.DatabaseId;
            collectionId = Settings.Default.CollectionId;
            documentId = Settings.Default.DocumentId;

            var parts = documentId.Split('.');
            if (parts.Length != 2)
            {
                throw new InvalidConfigurationException($"Incorrect DocumentId: {documentId}. It must be in form <collection>.<key>");
            }

            logicCollectionId = parts[0];
            logicKey = parts[1];

            collectionLink = $"/dbs/{databaseId}/colls/{collectionId}";
            documentLink = $"{collectionLink}/docs/{documentId}";

            documentClient = new DocumentClient(new Uri(url), key);
        }

        public async Task<string> GetAsync()
        {
            var root = await ReadDocumentAsync();

            return root?[BingMapKeyKey]?.ToString();
        }

        public async Task<bool> SetAsync(string key)
        {
            var root = await ReadDocumentAsync() ?? new JObject();

            root[BingMapKeyKey] = key;
            return await WriteDocumentAsync(root);
        }

        private async Task<JToken> ReadDocumentAsync()
        {
            try
            {
                var response = await documentClient.ReadDocumentAsync<ThemeDocument>(documentLink);
                return JsonConvert.DeserializeObject(response.Document.Data) as JToken;
            }
            catch
            {
                return null;
            }
        }

        private async Task<bool> WriteDocumentAsync(JToken root)
        {
            try
            {
                await documentClient.ReadDocumentCollectionAsync(collectionLink);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to set the key (collection is not available): {ex.Message}");
                return false;
            }

            var document = new ThemeDocument
            {
                Id = $"{documentId}",
                CollectionId = logicCollectionId,
                Key = logicKey,
                Data = JsonConvert.SerializeObject(root)
            };

            try
            {
                await documentClient.UpsertDocumentAsync(collectionLink, document);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to set the key: {ex.Message}");
                return false;
            }

            return true;
        }
    }
}