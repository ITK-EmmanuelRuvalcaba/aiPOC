namespace aiPOC.Client.Pages;

public partial class Upload
{
	private bool DisableDocContinue { get; set; } = false;
	private bool DisablePhotoContinue { get; set; } = false;
	private bool DisableContinue
	{
		get { return DisableDocContinue || DisablePhotoContinue; }
	}
}
