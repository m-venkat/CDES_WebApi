namespace PhoneNumberEnrichmentService.Models
{

    public interface IPhoneEnriched
    {

        PhoneInputToEnrich InputPhone { get; set; }

        bool IsValidPhone { get; set; }

        string RawInputPhone { get; set; }
        string RFC3966Format { get; set; }
        string PhoneLocation { get; set; }
        string NationalFormat { get; set; }
        string InternationalFormat { get; set; }
        string E164Format { get; set; }
    }
}