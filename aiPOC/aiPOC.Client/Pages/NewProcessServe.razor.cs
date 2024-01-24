﻿
using aiPOC.Shared.Models;

namespace aiPOC.Client.Pages;

public partial class NewProcessServe
{
	//string[] States = ["Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "Florida",
	//	"Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland",
	//	"Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey",
	//	"New Mexico", "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina",
	//	"South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming"];

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

	private void Submit()
	{

	}

	private void AddRecipient()
	{
		Request.Recipients.Add(new());
	}
}
