using aiPOC.Client.Pages;
using aiPOC.Shared.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace aiPOC.Client.Shared;

public partial class DocumentUpload
{
	[Parameter]
	public bool DisableDocContinue { get; set; }
	[Parameter]
	public EventCallback<bool> DisableDocContinueChanged { get; set; }
	[Parameter]
	public EventCallback<string> FormUrlSet { get; set; }

	[Inject]
	private HttpClient Http { get; set; }
	[Inject]
	public ILocalStorageService LocalStorage { get; set; }

	private readonly List<IBrowserFile> Files = [];
	private List<UploadResult> UploadResults = [];

	private bool IsLoading = false;
	private bool IsComplete = false;

	private async Task LoadFileAsync(InputFileChangeEventArgs e)
	{
		IsLoading = true;
		await DisableDocContinueChanged.InvokeAsync(true);

		IsComplete = false;
		Files.Clear();

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
			var response = await Http.PostAsync("/extract/serve", content);

			var newUploadResults = await response.Content.ReadFromJsonAsync<ProofOfServiceExtraction>();
			await LocalStorage.SetItemAsync<ProofOfServiceExtraction>(jobNumber, newUploadResults);
			await FormUrlSet.InvokeAsync(jobNumber);
		}

		IsLoading = false;
		await DisableDocContinueChanged.InvokeAsync(false);

		IsComplete = true;
	}
}
