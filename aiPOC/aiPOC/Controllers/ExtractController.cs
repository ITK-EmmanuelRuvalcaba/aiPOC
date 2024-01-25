using aiPOC.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace aiPOC.Controllers;

[ApiController]
[Route("extract")]
[AllowAnonymous]
public class ExtractController : ControllerBase
{
	private const string ExtractDocumentServiceUrl = "https://localhost:7159/v2/Documents/ServiceOfProcess";
	private readonly IConfiguration _configuration;

	public ExtractController(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	//POST extract serve details
	[HttpPost("serve")]
	public async Task<ActionResult<ProofOfServiceExtraction>> ExtractServeDetails([FromForm] IFormFile file)
	{
		using var httpClient = new HttpClient();
		var multipartContent = new MultipartFormDataContent
		{
			{ new StreamContent(file.OpenReadStream()), "file", file.FileName }
		};
		var response = await httpClient.PostAsync("https://localhost:7159/v2/Documents/ServiceOfProcess", multipartContent);
		if (response.IsSuccessStatusCode)
		{
			var responseBody = await response.Content.ReadFromJsonAsync<ExtractionResponse>();
			return Ok(responseBody.Extraction.Data);
		}

		return Ok();
	}

	[HttpPost("recipient")]
    public async Task<ActionResult<PersonDescription>> ExtractRecipientDetails([FromForm] DefaultRequest request)
    {
        try
        {
            using var httpClient = new HttpClient();
            //query openai vision api
            //base64 encode image
            using var ms = new MemoryStream();
            await request.File.CopyToAsync(ms);
            var fileBytes = ms.ToArray();
            var base64String = Convert.ToBase64String(fileBytes);
            
            //json content
            //grab api key from configuration
            var key = _configuration.GetSection("OpenAI").GetSection("ApiKey").Value;
            if (key == null)
            {
                return BadRequest("Could not complete request.");
            }

            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {key}");
            var response =
                await httpClient.PostAsync("https://api.openai.com/v1/chat/completions", new StringContent(JsonSerializer.Serialize(new {
                    model = "gpt-4-vision-preview",
                    messages = new [] {
                        new {
                            role = "user",
                            content = new dynamic[] {
                                new {
                                    type = "text",
                                    text = "You are a helpful assistant who is good at describing people. If you cannot determine a value, please insert a default value or null. Please describe the person in the supplied photograph, in the following format: { eye_color: string, hair_color: string, gender: string, age: string, ethnicity: string, other: string }. Please render valid json with an opening brace."
                                },
                                new {
                                    type = "image_url",
                                    image_url = new {
                                        url = $"data:image/png;base64,{base64String}"
                                    }
                                }
                            }
                        }
                    },
                    temperature = 0,
                    max_tokens = 4096
                }), Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }
            
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadFromJsonAsync<Root>();
                var content = responseBody.Choices[0].Message.Content.Replace("\n", "");
                var deserialized = JsonSerializer.Deserialize<PersonDescription>(content);
                return Ok(deserialized);
            }
            return BadRequest();

        } catch (Exception ex) 
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
    }
}
