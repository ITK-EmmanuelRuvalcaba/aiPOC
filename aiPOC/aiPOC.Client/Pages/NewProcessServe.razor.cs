
using aiPOC.Shared.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace aiPOC.Client.Pages;

public partial class NewProcessServe
{
	[Parameter]
	public int JobNumber { get; set; }

	[Inject]
	public ILocalStorageService LocalStorage { get; set; }

	Dictionary<string, string> States = new() {
		{"AL", "Alabama"}, {"AK", "Alaska"}, {"AZ", "Arizona"}, {"AR", "Arkansas"}, {"CA", "California"},
		{"CO", "Colorado"}, {"CT", "Connecticut"}, {"DE", "Delaware"}, {"FL", "Florida"}, {"GA", "Georgia"},
		{"HI", "Hawaii"}, {"ID", "Idaho"}, {"IL", "Illinois"}, {"IN", "Indiana"}, {"IA", "Iowa"},
		{"KS", "Kansas"}, {"KY", "Kentucky"}, {"LA", "Louisiana"}, {"ME", "Maine"}, {"MD", "Maryland"},
		{"MA", "Massachusetts"}, {"MI", "Michigan"}, {"MN", "Minnesota"}, {"MS", "Mississippi"}, {"MO", "Missouri"},
		{"MT", "Montana"}, {"NE", "Nebraska"}, {"NV", "Nevada"}, {"NH", "New Hampshire"}, {"NJ", "New Jersey"},
		{"NM", "New Mexico"}, {"NY", "New York"}, {"NC", "North Carolina"}, {"ND", "North Dakota"}, {"OH", "Ohio"},
		{"OK", "Oklahoma"}, {"OR", "Oregon"}, {"PA", "Pennsylvania"}, {"RI", "Rhode Island"}, {"SC", "South Carolina"},
		{"SD", "South Dakota"}, {"TN", "Tennessee"}, {"TX", "Texas"}, {"UT", "Utah"}, {"VT", "Vermont"},
		{"VA", "Virginia"}, {"WA", "Washington"}, {"WV", "West Virginia"}, {"WI", "Wisconsin"}, {"WY", "Wyoming"}
	};

	public ProcessServingRequest Request { get; set; } = new();

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		base.OnAfterRender(firstRender);
		var extract = await LocalStorage.GetItemAsync<ProofOfServiceExtraction>(JobNumber.ToString());

		var extractPersonDetails = await LocalStorage.GetItemAsync<PersonDescription>(JobNumber.ToString());
	}

	private void Submit()
	{

	}

	private void AddRecipient()
	{
		Request.Recipients.Add(new());
	}
}
