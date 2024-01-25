using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aiPOC.Shared.Models;

public static class ContractMapping
{
	public static void MapRequestFromExtract(this ProcessServingRequest request, ProofOfServiceExtraction extract)
	{
		request.State = extract.Court.State;
		request.CaseNumber = extract.CaseNumber;
		request.Court = extract.Court.BranchName;
		request.Plaintiff = extract.PlaintiffName;
		request.Defendant = extract.DefendantName;

		request.Recipients.FirstOrDefault().Name = extract.RecipientName;
		request.Recipients.FirstOrDefault().Address.Address1 = extract.RecipientAddress.Address1;
		request.Recipients.FirstOrDefault().Address.Address2 = extract.RecipientAddress.Address2;
		request.Recipients.FirstOrDefault().Address.City = extract.RecipientAddress.City;
		request.Recipients.FirstOrDefault().Address.State = extract.RecipientAddress.State;
		request.Recipients.FirstOrDefault().Address.PostalCode = extract.RecipientAddress.PostalCode;
	}
	public static void MapRequestFromPhotoExtract(this ProcessServingRequest request, PersonDescription extract)
	{
		request.Recipients.FirstOrDefault().PhysicalDetails.Eyes = extract.EyeColor;
		request.Recipients.FirstOrDefault().PhysicalDetails.Hair = extract.HairColor;
		request.Recipients.FirstOrDefault().PhysicalDetails.Gender = extract.Gender;
		request.Recipients.FirstOrDefault().PhysicalDetails.Age = extract.Age;
		request.Recipients.FirstOrDefault().PhysicalDetails.Ethnicity = extract.Ethnicity;
	}
}
