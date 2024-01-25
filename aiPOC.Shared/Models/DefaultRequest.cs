using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace aiPOC.Shared.Models;

public class DefaultRequest
{
	public IFormFile File { get; set; }
}

public record ExtractionResponse
{
	[JsonPropertyName("extraction")]
	public ExtractionData Extraction { get; set; }
}

public class ExtractionData
{
	[JsonPropertyName("data")]
	public ProofOfServiceExtraction Data { get; set; }
}

public class ProofOfServiceExtraction
{
	[JsonPropertyName("plaintiffName")]
	public string PlaintiffName { get; set; }
	[JsonPropertyName("defendantName")]
	public string DefendantName { get; set; }
	[JsonPropertyName("recipientAddress")]
	public Address RecipientAddress { get; set; }
	[JsonPropertyName("recipientName")]
	public string RecipientName { get; set; }
	[JsonPropertyName("caseNumber")]
	public string CaseNumber { get; set; }
	[JsonPropertyName("court")]
	public Court Court { get; set; }
}

public class Court
{
	[JsonPropertyName("branchName")]
	public string BranchName { get; set; }
	[JsonPropertyName("county")]
	public string County { get; set; }
	[JsonPropertyName("state")]
	public string State { get; set; }
}

public class Address
{
	[JsonPropertyName("address1")]
	public string Address1 { get; set; }
	[JsonPropertyName("address2")]
	public string Address2 { get; set; }
	[JsonPropertyName("city")]
	public string City { get; set; }
	[JsonPropertyName("state")]
	public string State { get; set; }
	[JsonPropertyName("county")]
	public string County { get; set; }
	[JsonPropertyName("postalCode")]
	public string PostalCode { get; set; }
}

public class Root
{
	[JsonPropertyName("id")]
	public string Id { get; set; }

	[JsonPropertyName("object")]
	public string Object { get; set; }

	[JsonPropertyName("created")]
	public int Created { get; set; }

	[JsonPropertyName("model")]
	public string Model { get; set; }

	[JsonPropertyName("usage")]
	public Usage Usage { get; set; }

	[JsonPropertyName("choices")]
	public List<Choice> Choices { get; set; }
}

public class Usage
{
	[JsonPropertyName("prompt_tokens")]
	public int PromptTokens { get; set; }

	[JsonPropertyName("completion_tokens")]
	public int CompletionTokens { get; set; }

	[JsonPropertyName("total_tokens")]
	public int TotalTokens { get; set; }
}

public class Choice
{
	[JsonPropertyName("message")]
	public Message Message { get; set; }

	[JsonPropertyName("finish_reason")]
	public string FinishReason { get; set; }

	[JsonPropertyName("index")]
	public int Index { get; set; }
}

public class Message
{
	[JsonPropertyName("role")]
	public string Role { get; set; }

	[JsonPropertyName("content")]
	public string Content { get; set; }
}

public class PersonDescription
{
	[JsonPropertyName("eye_color")]
	public string EyeColor { get; set; }

	[JsonPropertyName("hair_color")]
	public string HairColor { get; set; }

	[JsonPropertyName("gender")]
	public string Gender { get; set; }

	[JsonPropertyName("age")]
	public string Age { get; set; }

	[JsonPropertyName("ethnicity")]
	public string Ethnicity { get; set; }

	[JsonPropertyName("other")]
	public string Other { get; set; }
}

