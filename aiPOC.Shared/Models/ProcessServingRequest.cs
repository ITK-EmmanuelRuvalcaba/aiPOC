namespace aiPOC.Shared.Models;

public class ProcessServingRequest
{
	public string Matter { get; set; } = string.Empty;
	public string State { get; set; } = string.Empty;
	public string CaseNumber { get; set; } = string.Empty;
	public string Court { get; set; } = string.Empty;
	public string Plaintiff { get; set; } = string.Empty;
	public string Defendant { get; set; } = string.Empty;
	public List<ProcessServingRecipient> Recipients { get; set; } = new List<ProcessServingRecipient>()
	{
		new()
	};
}

public class ProcessServingRecipient
{
	public Guid Id { get; set; } = new Guid();
	public string Name { get; set; } = string.Empty;
	public RecipientAddress Address { get; set; } = new();
	public PhysicalDetails PhysicalDetails { get; set; } = new();
	public string SpecialInstructions { get; set; } = string.Empty;
}

public class PhysicalDetails
{
	public string Age { get; set; } = string.Empty;
	public string Gender { get; set; } = string.Empty;
	public string Ethnicity { get; set; } = string.Empty;
	public string Relationship { get; set; } = string.Empty;
	public string Hair { get; set; } = string.Empty;
	public string Eyes { get; set; } = string.Empty;
	public string Weight { get; set; } = string.Empty;
	public int? HeightFeet { get; set; }
	public int? HeightInches { get; set; }
	public string OtherDetails { get; set; } = string.Empty;
}

public class RecipientAddress
{
	public string AddressType { get; set; } = string.Empty;
	public string Address1 { get; set; } = string.Empty;
	public string Address2 { get; set; } = string.Empty;
	public string City { get; set; } = string.Empty;
	public string State { get; set; } = string.Empty;
	public string PostalCode { get; set; } = string.Empty;
}
