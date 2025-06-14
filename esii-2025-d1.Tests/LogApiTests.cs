using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace esii_2025_d1.Tests;

[TestFixture]
public class LogApiTests
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;
    private const string BaseUrl = "/api/Log";

    [SetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
    }

    [TearDown]
    public void TearDown()
    {
        _client?.Dispose();
        _factory?.Dispose();
    }

    [Test]
    public async Task GetLogs_ReturnsOkWithJsonContent()
    {
        var response = await _client.GetAsync(BaseUrl);
        
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Content.Headers.ContentType?.MediaType, 
            Is.EqualTo("application/json"));
        
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString
        };
        
        var content = await response.Content.ReadAsStringAsync();
        var logs = JsonSerializer.Deserialize<LogResponse[]>(content, options);
        
        Assert.That(logs, Is.Not.Null);
        Assert.That(logs, Is.Not.Empty);
    }
}

public class LogResponse
{
    public int Id { get; set; }
    public object action { get; set; }
    public DateTime created_at { get; set; }
    public int? entity_id { get; set; }
    public string entity_name { get; set; }
    public string user_id { get; set; }
}