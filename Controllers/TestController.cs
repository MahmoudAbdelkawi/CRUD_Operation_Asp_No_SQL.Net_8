using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using Online_Survey.Controllers.Base;
using Online_Survey.Dtos;
using Online_Survey.Global;
using Online_Survey.Models;
using Online_Survey.Responses;
using Online_Survey.Services.Base;
using System.Dynamic;
using System.Text.Json;

namespace Online_Survey.Controllers
{
    public class TestController : BaseController
    {
        private readonly BaseService<TestCollection> _testService;

        public TestController(BaseService<TestCollection> testService)
        {
            _testService = testService;
        }


        

        //[HttpGet]
        //public async Task<IActionResult> Get() =>
        //   Ok(await _testService.GetAsync());

        [HttpGet("{id}")]
        public async Task<object> Get(string id)
        {

            var book = await _testService.GetDocumentAsync(ObjectId.Parse(id));

            if (book is null)
            {
                return NotFound();
            }

            return BsonDocument.Create(book).RawValue;

            //return Ok(new TestCollectionResponse(book));
            //return Result(new BaseResponse<TestCollectionResponse>
            //{
            //    Data = new TestCollectionResponse(book),
            //    Succeeded = true,
            //    Message = "Success",
            //    StatusCode = System.Net.HttpStatusCode.OK
            //});

        }

        [HttpPost]
        public async Task<IActionResult> Post(TestDto newBookDto)
        {
            // convert other data to array of BsonDocument
            var otherData = newBookDto.UnknownObjects.Select(x => BsonDocument.Parse(x.ToString())).ToList();

            await _testService.CreateAsync(new TestCollection
            {
                //Id = ObjectId.GenerateNewId().ToString(),
                Name = newBookDto.Name,
                UnknownObjects = otherData,
            });

            return Ok();
        }

        //[HttpPut("{id:length(24)}")]
        //public async Task<IActionResult> Update(ObjectId id, TestCollection updatedBook)
        //{
        //    var book = await _testService.GetAsync(id);

        //    if (book is null)
        //    {
        //        return NotFound();
        //    }

        //    updatedBook.Id = book.Id;

        //    await _testService.UpdateAsync(id, updatedBook);

        //    return NoContent();
        //}

        //[HttpDelete("{id:length(24)}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    var book = await _testService.GetAsync(id);

        //    if (book is null)
        //    {
        //        return NotFound();
        //    }

        //    await _testService.RemoveAsync(id);

        //    return NoContent();
        //}
    }
}
