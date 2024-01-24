using aiPOC.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace aiPOC.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileUploadController(IHostEnvironment env) : ControllerBase
{
	[HttpPost]
	public async Task<ActionResult<IList<UploadResult>>> PostFile(
		[FromForm] IEnumerable<IFormFile> files)
	{
		var maxAllowedFiles = 1;
		long maxFileSize = 1024 * 15;
		var filesProcessed = 0;
		var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");
		List<UploadResult> uploadResults = [];

		foreach (var file in files)
		{
			UploadResult uploadResult = new();
			string trustedFileNameForFileStorage;
			var untrustedFileName = file.FileName;
			uploadResult.FileName = untrustedFileName;
			var trustedFileNameForDisplay =
				WebUtility.HtmlEncode(untrustedFileName);

			if (filesProcessed < maxAllowedFiles)
			{
				if (file.Length == 0)
				{
					uploadResult.ErrorCode = 1;
				}
				else if (file.Length > maxFileSize)
				{
					uploadResult.ErrorCode = 2;
				}
				else
				{
					try
					{
						trustedFileNameForFileStorage = Path.GetRandomFileName();
						var path = Path.Combine(env.ContentRootPath,
							env.EnvironmentName, "unsafe_uploads",
							trustedFileNameForFileStorage);

						await using FileStream fs = new(path, FileMode.Create);
						await file.CopyToAsync(fs);

						uploadResult.Uploaded = true;
						uploadResult.StoredFileName = trustedFileNameForFileStorage;
					}
					catch (IOException ex)
					{
						uploadResult.ErrorCode = 3;
					}
				}

				filesProcessed++;
			}
			else
			{
				uploadResult.ErrorCode = 4;
			}

			uploadResults.Add(uploadResult);
		}

		return new CreatedResult(resourcePath, uploadResults);
	}
}
