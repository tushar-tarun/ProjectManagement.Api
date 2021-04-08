using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProjectManagement.Api;
using ProjectManagement.Shared;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace ProjectManagement.Tests
{
    public class ProjectManagememtControllerTestBase<T> : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFactory;

        private static HttpClient _httpClient;

        public ProjectManagememtControllerTestBase(WebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
            var service = _webApplicationFactory.Services.GetService<IServiceScopeFactory>();
            using (var scope = service.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ProjectManagementDbContext>();

                ProjectManagementDbContext.Initialize(services);
            }
            _httpClient ??= webApplicationFactory.CreateClient();
        }

        public async void TestControllerMethods(string baseUrl)
        {
            //Get all
            var url = $"api/{baseUrl}";
            var response = await _httpClient.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<T>>(responseContent);
            AssertGetAllInformation(data);

            //get one
            response = await _httpClient.GetAsync($@"{url}/{1}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            responseContent = await response.Content.ReadAsStringAsync();
            var testData = JsonConvert.DeserializeObject<T>(responseContent);
            AssertGetOneInformation(testData);

            //try update : should give not found status as the data to update doesnot exist
            var dataToAdd = JsonConvert.SerializeObject(GetDataToAdd());
            var request = new HttpRequestMessage(HttpMethod.Post, $@"{url}/Update")
            {
                Content = new StringContent(dataToAdd, Encoding.UTF8, "application/json")
            };
            var postResponse = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.BadRequest, postResponse.StatusCode);

            //add data: new data should be added
            request = new HttpRequestMessage(HttpMethod.Put, $@"{url}/Add")
            {
                Content = new StringContent(dataToAdd, Encoding.UTF8, "application/json")
            };
            var putResponse = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, putResponse.StatusCode);

            //retry add data : should give bad request as data already esists
            request = new HttpRequestMessage(HttpMethod.Put, $@"{url}/Add")
            {
                Content = new StringContent(dataToAdd, Encoding.UTF8, "application/json")
            };
            putResponse = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.BadRequest, putResponse.StatusCode);

            //get one: fetch the new data
            response = await _httpClient.GetAsync($@"{url}/{5}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            responseContent = await response.Content.ReadAsStringAsync();
            var newEntry = JsonConvert.DeserializeObject<T>(responseContent);
            Assert.NotNull(newEntry);

            //update data: change the value of the new record that is added
            var dataToUpdate = JsonConvert.SerializeObject(GetDataToUpdate(newEntry));
            request = new HttpRequestMessage(HttpMethod.Post, $@"{url}/Update")
            {
                Content = new StringContent(dataToUpdate, Encoding.UTF8, "application/json")
            };
            postResponse = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);

            //get one: fetch the updated data
            response = await _httpClient.GetAsync($@"{url}/{5}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            responseContent = await response.Content.ReadAsStringAsync();
            var updatedEntry = JsonConvert.DeserializeObject<T>(responseContent);
            AssertUpdatedOneInformation(updatedEntry);

            //delete the data 
            request = new HttpRequestMessage(HttpMethod.Delete, $@"{url}/{5}")
            {
                Content = new StringContent(dataToUpdate, Encoding.UTF8, "application/json")
            };
            postResponse = await _httpClient.SendAsync(request);
            Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);

            response = await _httpClient.GetAsync($@"{url}/{5}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        protected virtual void AssertGetAllInformation(List<T> data) { }
        protected virtual void AssertGetOneInformation(T data) { }
        protected virtual void AssertUpdatedOneInformation(T data) { }

        protected virtual T GetDataToAdd() { throw new NotImplementedException();  }
        protected virtual T GetDataToUpdate(T data) {  throw new NotImplementedException(); }
    }
}
