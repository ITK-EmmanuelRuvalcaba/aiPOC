using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using aiPOC.Shared.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace aiPOC.Client.Shared;

public partial class PhotoUpload
{
	[Parameter]
	public bool DisablePhotoContinue { get; set; }
	[Parameter]
	public EventCallback<bool> DisablePhotoContinueChanged { get; set; }
	[Inject]
	private HttpClient Http { get; set; }
	[Inject]
	public ILocalStorageService LocalStorage { get; set; }

	private List<IBrowserFile> Files = [];
	private bool IsLoading = false;
	private bool IsComplete = false;
	
	private List<UploadResult> UploadResults = [];

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
		
		long maxFileSize = 1024 * 15000;
		string jobNumber = string.Empty;
		var upload = false;
		using var content = new MultipartFormDataContent();

		if (UploadResults.SingleOrDefault(f => f.FileName == e.File.Name) is null)
		{
			try
			{
				Files.Add(e.File);

				var fileContent =
					new StreamContent(e.File.OpenReadStream(maxFileSize));

				fileContent.Headers.ContentType =
					new MediaTypeHeaderValue(e.File.ContentType);

				content.Add(
					content: fileContent,
					name: "\"file\"",
					fileName: e.File.Name);

				jobNumber = e.File.Name.Split("_")[0];

				upload = true;
			}
			catch (Exception ex)
			{
				UploadResults.Add(
					new()
					{
						FileName = e.File.Name,
						ErrorCode = 6,
						Uploaded = false
					});
			}
		}

		if (upload)
		{
			var response = await Http.PostAsync("/extract/recipient", content);

			var newUploadResults = await response.Content.ReadFromJsonAsync<PersonDescription>();
			await LocalStorage.SetItemAsync<PersonDescription>(jobNumber, newUploadResults);
		}

		IsComplete = true;
	}
}
