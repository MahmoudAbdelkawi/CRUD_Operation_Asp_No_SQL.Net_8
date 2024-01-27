using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using Online_Survey.Models;
using System.Dynamic;

namespace Online_Survey.Responses
{
    public class TestCollectionResponse
    {
        public string Id { get; set; } 
        public string Name { get; set; } = null!;
        public List<object> UnknownObjects { get; set; } = null!;

        public TestCollectionResponse(TestCollection testCollection)
        {
            Id = testCollection.Id.ToString();
            Name = testCollection.Name;
            UnknownObjects = ConvertToTestCollection(testCollection);
        }

        private object GetDynamicValue(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    return GetDynamicObject((JObject)token);
                case JTokenType.Array:
                    return ((JArray)token).Select(GetDynamicValue).ToList();
                default:
                    return ((JValue)token).Value;
            }
        }

        private dynamic GetDynamicObject(JObject json)
        {
            dynamic dynamicObject = new ExpandoObject();
            foreach (var property in json.Properties())
            {
                ((IDictionary<string, object>)dynamicObject)[property.Name] = GetDynamicValue(property.Value);
            }
            return dynamicObject;
        }

        private List<object> ConvertToTestCollection(TestCollection document)
        {
            var jsonList = new List<object>();

            if (document.UnknownObjects != null)
            {
                foreach (var dynamicObject in document.UnknownObjects)
                {
                    var json = JObject.Parse(dynamicObject.ToJson());
                    var castedObject = GetDynamicObject(json);
                    jsonList.Add(castedObject);

                }
            }
            return jsonList;
        }
    }
}
