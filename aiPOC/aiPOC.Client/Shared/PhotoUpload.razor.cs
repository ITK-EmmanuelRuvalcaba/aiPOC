using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace aiPOC.Client.Shared;

public partial class PhotoUpload
{
	[Parameter]
	public bool DisablePhotoContinue { get; set; }
	[Parameter]
	public EventCallback<bool> DisablePhotoContinueChanged { get; set; }

	private List<IBrowserFile> Files = [];
	private bool IsLoading = false;
	private bool IsComplete = false;

	private async Task LoadFileAsync(InputFileChangeEventArgs e)
	{
		IsLoading = true;
		await DisablePhotoContinueChanged.InvokeAsync(true);

		IsComplete = false;
		Files.Clear();

		foreach (var file in e.GetMultipleFiles(1))
		{
			try
			{
				Files.Add(file);
			}
			catch (Exception)
			{
				throw;
			}
		}

		await Task.Delay(5000);

		IsLoading = false;
		await DisablePhotoContinueChanged.InvokeAsync(false);

		IsComplete = true;
	}
}
