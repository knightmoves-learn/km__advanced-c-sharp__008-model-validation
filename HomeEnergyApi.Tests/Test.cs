using System.Text;
using System.Text.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

[TestCaseOrderer("HomeEnergyApi.Tests.Extensions.PriorityOrderer", "HomeEnergyApi.Tests")]
public class Test
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private string testHomeValid = JsonSerializer.Serialize(new Home(1, "Test", "123 Test St.", "Test City", 123));
    private string testHomeNoId = JsonSerializer.Serialize(new Home(2, "Testy", "456 Assert St.", "Unitville", 456)).Replace("\"Id\":2", "\"Id\":null");
    private string testHomeNoOwnerLastName = JsonSerializer.Serialize(new Home(3, "Test", "123 Test St.", "Test City", 123)).Replace("\"OwnerLastName\":\"Test\"", "\"OwnerLastName\":null");
    private string testHomeInvalidStreetAddress = JsonSerializer.Serialize(new Home(3, "Tester", "123456789123456789123456789123456789 Avenue.", "Integration Town", 789));
    private string testHomeInvalidMonthlyElectricUsage = JsonSerializer.Serialize(new Home(3, "Tester", "789 Theory St.", "Integration Town", 75000));

    public Test(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Theory, TestPriority(1)]
    [InlineData("/Homes")]
    public async Task HomeEnergyApiReturnsSuccessfulHTTPResponseCodeOnGETHomes(string url)
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync(url);

        Assert.True(response.IsSuccessStatusCode, $"HomeEnergyApi did not return successful HTTP Response Code on GET request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");
    }

    [Theory, TestPriority(2)]
    [InlineData("/Homes")]
    public async Task HomeEnergyApiReturns201CreatedHTTPResponseOnAddingValidHomeThroughPOST(string url)
    {
        var client = _factory.CreateClient();

        HttpRequestMessage sendRequest = new HttpRequestMessage(HttpMethod.Post, url);
        sendRequest.Content = new StringContent(testHomeValid,
                                                Encoding.UTF8,
                                                "application/json");

        var response = await client.SendAsync(sendRequest);
        Assert.True((int)response.StatusCode == 201, $"HomeEnergyApi did not return \"201: Created\" HTTP Response Code on POST request at {url}; instead received {(int)response.StatusCode}: {response.StatusCode}");

        string responseContent = await response.Content.ReadAsStringAsync();
        Assert.True(responseContent.ToLower() == testHomeValid.ToLower(), $"HomeEnergyApi did not return the home being added as a response from the POST request at {url}; \n Expected : {testHomeValid.ToLower()} \n Received : {responseContent.ToLower()} \n");
    }

    [Theory, TestPriority(3)]
    [InlineData("/Homes")]
    public async Task HomeEnergyApiReturnsValidationErrorWhenHomeHasNullId(string url)
    {
        var client = _factory.CreateClient();

        HttpRequestMessage sendRequest = new HttpRequestMessage(HttpMethod.Post, url);
        sendRequest.Content = new StringContent(testHomeNoId,
                                                Encoding.UTF8,
                                                "application/json");

        var response = await client.SendAsync(sendRequest);
        string responseContent = await response.Content.ReadAsStringAsync();

        string expectedNoIdPhrase = "\"errors\":{\"home\":[\"The home field is required.\"],\"$.Id\":[\"The JSON value could not be converted to Home. Path: $.Id";
        bool hasNoIdValidationError = responseContent.Contains(expectedNoIdPhrase);

        Assert.True(hasNoIdValidationError, $"HomeEnergyApi did not return the expected error after submitting home with null Id on POST request at {url}; \n Error Message Expected : {expectedNoIdPhrase} \n Content Received : {responseContent} \n");
    }

    [Theory, TestPriority(4)]
    [InlineData("/Homes")]
    public async Task HomeEnergyApiReturnsValidationErrorWhenHomeHasNullOwnerLastName(string url)
    {
        var client = _factory.CreateClient();

        HttpRequestMessage sendRequest = new HttpRequestMessage(HttpMethod.Post, url);
        sendRequest.Content = new StringContent(testHomeNoOwnerLastName,
                                                Encoding.UTF8,
                                                "application/json");

        var response = await client.SendAsync(sendRequest);
        string responseContent = await response.Content.ReadAsStringAsync();

        string expectedNoOwnerLastNamePhrase = "\"errors\":{\"OwnerLastName\":[\"The OwnerLastName field is required.\"]}";
        bool hasNoOwnerLastNameValidationError = responseContent.Contains(expectedNoOwnerLastNamePhrase);

        Assert.True(hasNoOwnerLastNameValidationError, $"HomeEnergyApi did not return the expected error after submitting home with null OwnerLastName on POST request at {url}; \n Error Message Expected : {expectedNoOwnerLastNamePhrase} \n Content Received : {responseContent} \n");
    }

    [Theory, TestPriority(5)]
    [InlineData("/Homes")]
    public async Task HomeEnergyApiReturnsValidationErrorWhenHomeStreetAddressIsTooLong(string url)
    {
        var client = _factory.CreateClient();

        HttpRequestMessage sendRequest = new HttpRequestMessage(HttpMethod.Post, url);
        sendRequest.Content = new StringContent(testHomeInvalidStreetAddress,
                                                Encoding.UTF8,
                                                "application/json");

        var response = await client.SendAsync(sendRequest);
        string responseContent = await response.Content.ReadAsStringAsync();

        string expectedStreetAddressErrorPhrase = "400,\"errors\":{\"StreetAddress\":[\"The field StreetAddress must be a string with a maximum length of 40.\"]}";
        bool hasExpectedStreetAddressError = responseContent.Contains(expectedStreetAddressErrorPhrase);

        Assert.True(hasExpectedStreetAddressError, $"HomeEnergyApi did not return the expected error after submitting home with StreetAddress above 40 characters on POST request at {url}; \n Error Message Expected : {expectedStreetAddressErrorPhrase} \n Content Received : {responseContent} \n");
    }

    [Theory, TestPriority(6)]
    [InlineData("/Homes")]
    public async Task HomeEnergyApiReturnsValidationErrorWhenHomeMonthlyEnergyUsageIsTooLarge(string url)
    {
        var client = _factory.CreateClient();

        HttpRequestMessage sendRequest = new HttpRequestMessage(HttpMethod.Post, url);
        sendRequest.Content = new StringContent(testHomeInvalidMonthlyElectricUsage,
                                                Encoding.UTF8,
                                                "application/json");

        var response = await client.SendAsync(sendRequest);
        string responseContent = await response.Content.ReadAsStringAsync();

        string expectedMonthlyElectricError = "\"errors\":{\"MonthlyElectricUsage\":[\"Monthly electric usage is limited to positive numbers of 50,000 kWh or less\"]}";
        bool hasExpectedMonthlyElectricError = responseContent.Contains(expectedMonthlyElectricError);

        Assert.True(hasExpectedMonthlyElectricError, $"HomeEnergyApi did not return the expected error after submitting home with monthly electric usage above 50,000 on POST request at {url}; \n Error Message Expected : {expectedMonthlyElectricError} \n Content Received : {responseContent} \n");
    }
}
