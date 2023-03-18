using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MicroHttp.Tests
{
    public class MicroHttpTests
    {
        private readonly Mock<IHttpClientFactory> _mockFactory;
        private readonly MicroHttp _microHttp;

        public MicroHttpTests()
        {
            _mockFactory = new Mock<IHttpClientFactory>();
            _microHttp = new MicroHttp(_mockFactory.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnContent()
        {
            // Arrange
            var expectedResponse = new { Message = "Hello World" };
            var responseContent = JsonConvert.SerializeObject(expectedResponse);
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            responseMessage.Content = new StringContent(responseContent, Encoding.UTF8, "application/json");
            var mockHttpClient = new Mock<HttpClient>();
            mockHttpClient
                .Setup(c => c.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(responseMessage);
            _mockFactory
                .Setup(cf => cf.CreateClient(It.IsAny<string>()))
                .Returns(mockHttpClient.Object);

            // Act
            var result = await _microHttp.GetAsync<object>("http://localhost/api/test");

            // Assert
            Assert.NotNull(result);
            var resultContent = JsonConvert.SerializeObject(result);
            Assert.Equal(responseContent, resultContent);
        }

        [Fact]
        public async Task PostAsync_ShouldReturnContent()
        {
            // Arrange
            var expectedResponse = new { Message = "Hello World" };
            var requestContent = new { Name = "Test" };
            var responseContent = JsonConvert.SerializeObject(expectedResponse);
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            responseMessage.Content = new StringContent(responseContent, Encoding.UTF8, "application/json");
            var mockHttpClient = new Mock<HttpClient>();
            mockHttpClient
                .Setup(c => c.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(responseMessage);
            _mockFactory
                .Setup(cf => cf.CreateClient(It.IsAny<string>()))
                .Returns(mockHttpClient.Object);

            // Act
            var result = await _microHttp.PostAsync<object>("http://localhost/api/test", requestContent);

            // Assert
            Assert.NotNull(result);
            var resultContent = JsonConvert.SerializeObject(result);
            Assert.Equal(responseContent, resultContent);
        }

        [Fact]
        public async Task PutAsync_ShouldReturnContent()
        {
            // Arrange
            var expectedResponse = new { Message = "Hello World" };
            var requestContent = new { Name = "Test" };
            var responseContent = JsonConvert.SerializeObject(expectedResponse);
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            responseMessage.Content = new StringContent(responseContent, Encoding.UTF8, "application/json");
            var mockHttpClient = new Mock<HttpClient>();
            mockHttpClient
                .Setup(c => c.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(responseMessage);
            _mockFactory
                .Setup(cf => cf.CreateClient(It.IsAny<string>()))
                .Returns(mockHttpClient.Object);

            // Act
            var result = await _microHttp.PutAsync<object>("http://localhost/api/test", requestContent);

            // Assert
            Assert.NotNull(result);
            var resultContent = JsonConvert.SerializeObject(result);
            Assert.Equal(responseContent, resultContent);
        }

        [Fact]
        public async Task PatchAsync_ShouldReturnContent()
        {
            // Arrange
            var expectedResponse = new { Message = "Hello World" };
            var requestContent = new { Name = "Test" };
            var responseContent = JsonConvert.SerializeObject(expectedResponse);
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            responseMessage.Content = new StringContent(responseContent, Encoding.UTF8, "application/json");
            var mockHttpClient = new Mock<HttpClient>();
            mockHttpClient
                .Setup(c => c.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(responseMessage);
            _mockFactory
                .Setup(cf => cf.CreateClient(It.IsAny<string>()))
                .Returns(mockHttpClient.Object);

            // Act
            var result = await _microHttp.PatchAsync<object>("http://localhost/api/test", requestContent);

            // Assert
            Assert.NotNull(result);
            var resultContent = JsonConvert.SerializeObject(result);
            Assert.Equal(responseContent, resultContent);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnSuccessStatusCode()
        {
            // Arrange
            var clientFactory = new Mock<IHttpClientFactory>();
            var client = new Mock<HttpClient>();
            clientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client.Object);

            var service = new MicroHttp(clientFactory.Object);

            var url = "http://test.com/api/items/1";

            var expectedResponse = new HttpResponseMessage(HttpStatusCode.NoContent);

            client
                .Setup(_ => _.SendAsync(
                    It.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Delete && r.RequestUri == new Uri(url)),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            await service.DeleteAsync(url);

            // Assert
            clientFactory.Verify(_ => _.CreateClient("test"), Times.Once);

            client.Verify(
                _ => _.SendAsync(
                    It.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Delete && r.RequestUri == new Uri(url)),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}