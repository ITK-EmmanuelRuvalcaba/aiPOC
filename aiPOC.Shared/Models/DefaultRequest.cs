using Microsoft.AspNetCore.Http;

namespace aiPOC.Shared.Models;

public class DefaultRequest
{
	public MultipartFormDataContent File { get; set; }
}
