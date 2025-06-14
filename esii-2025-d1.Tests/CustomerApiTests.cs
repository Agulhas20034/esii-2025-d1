using System.Net;
using System.Net.Http.Json;
using esii_2025_d1.Dtos.CustomersDtos;
using esii_2025_d1.Dtos.ProjectDtos;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace esii_2025_d1.Tests;

[TestFixture]
public class CustomerApiTests
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;
    private const string BaseUrl = "/api/Customer";
    private int _testCustomerId;

    [SetUp]
    public async Task Setup()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
        
        var newCustomer = new 
        {
            Name = "Test Customer",
            Email = "test@example.com",
            PhoneNumber = "123456789",
            Projects = new List<object>()
        };
        
        var response = await _client.PostAsJsonAsync(BaseUrl, newCustomer);
        var customer = await response.Content.ReadFromJsonAsync<CustomerResponseDto>();
        _testCustomerId = customer.Id;
    }

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [Test]
    public async Task GetAllCustomers_ReturnsOk()
    {
        var response = await _client.GetAsync(BaseUrl);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task GetCustomerById_ReturnsOk()
    {
        var response = await _client.GetAsync($"{BaseUrl}/{_testCustomerId}");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task CreateCustomer_ReturnsOk() 
    {
        var newCustomer = new 
        {
            Name = "New Customer",
            Email = "new@example.com",
            PhoneNumber = "987654321",
            Projects = new List<object>()
        };

        var response = await _client.PostAsJsonAsync(BaseUrl, newCustomer);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));  
    }

    [Test]
    public async Task UpdateCustomer_ReturnsNoContent()
    {
        var updateData = new 
        {
            Name = "Updated Customer",
            Email = "updated@example.com",
            PhoneNumber = "555555555",
            Projects = new List<object>()
        };

        var response = await _client.PutAsJsonAsync($"{BaseUrl}/{_testCustomerId}", updateData);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }

    [Test]
    public async Task DeleteCustomer_ReturnsNoContent()
    {
        var response = await _client.DeleteAsync($"{BaseUrl}/{_testCustomerId}");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }
}