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
