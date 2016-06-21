using CDES_WebApi.Models;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CDES_WebApi.Utils
{
    /// <summary>
    /// Utility Class to Validate & Enrich Phone Number
    /// </summary>
    public sealed class PhoneValidationUtil
    {
        private PhoneNumbers.PhoneNumberUtil _util = null;
        private PhoneNumberOfflineGeocoder _geocoder = null;
        private Dictionary<string, string> _countryNameToISOMapping = new Dictionary<string, string>()
        {
            {"AFGHANISTAN","AF"},
            {"ÅLAND ISLANDS","AX"},
            {"ALBANIA","AL"},
            {"ALGERIA","DZ"},
            {"AMERICAN SAMOA","AS"},
            {"ANDORRA","AD"},
            {"ANGOLA","AO"},
            {"ANGUILLA","AI"},
            {"ANTARCTICA","AQ"},
            {"ANTIGUA AND BARBUDA","AG"},
            {"ARGENTINA","AR"},
            {"ARMENIA","AM"},
            {"ARUBA","AW"},
            {"AUSTRALIA","AU"},
            {"AUSTRIA","AT"},
            {"AZERBAIJAN","AZ"},
            {"BAHAMAS","BS"},
            {"BAHRAIN","BH"},
            {"BANGLADESH","BD"},
            {"BARBADOS","BB"},
            {"BELARUS","BY"},
            {"BELGIUM","BE"},
            {"BELIZE","BZ"},
            {"BENIN","BJ"},
            {"BERMUDA","BM"},
            {"BHUTAN","BT"},
            {"BOLIVIA (PLURINATIONAL STATE OF)","BO"},
            {"BONAIRE SINT EUSTATIUS AND SABA","BQ"},
            {"BOSNIA AND HERZEGOVINA","BA"},
            {"BOTSWANA","BW"},
            {"BOUVET ISLAND","BV"},
            {"BRAZIL","BR"},
            {"BRITISH INDIAN OCEAN TERRITORY","IO"},
            {"BRUNEI DARUSSALAM","BN"},
            {"BULGARIA","BG"},
            {"BURKINA FASO","BF"},
            {"BURUNDI","BI"},
            {"CABO VERDE","CV"},
            {"CAMBODIA","KH"},
            {"CAMEROON","CM"},
            {"CANADA","CA"},
            {"CAYMAN ISLANDS","KY"},
            {"CENTRAL AFRICAN REPUBLIC","CF"},
            {"CHAD","TD"},
            {"CHILE","CL"},
            {"CHINA","CN"},
            {"CHRISTMAS ISLAND","CX"},
            {"COCOS (KEELING) ISLANDS","CC"},
            {"COLOMBIA","CO"},
            {"COMOROS","KM"},
            {"CONGO","CG"},
            {"CONGO (DEMOCRATIC REPUBLIC OF THE)","CD"},
            {"COOK ISLANDS","CK"},
            {"COSTA RICA","CR"},
            {"CÔTE D'IVOIRE","CI"},
            {"CROATIA","HR"},
            {"CUBA","CU"},
            {"CURAÇAO","CW"},
            {"CYPRUS","CY"},
            {"CZECH REPUBLIC","CZ"},
            {"DENMARK","DK"},
            {"DJIBOUTI","DJ"},
            {"DOMINICA","DM"},
            {"DOMINICAN REPUBLIC","DO"},
            {"ECUADOR","EC"},
            {"EGYPT","EG"},
            {"EL SALVADOR","SV"},
            {"EQUATORIAL GUINEA","GQ"},
            {"ERITREA","ER"},
            {"ESTONIA","EE"},
            {"ETHIOPIA","ET"},
            {"FALKLAND ISLANDS (MALVINAS)","FK"},
            {"FAROE ISLANDS","FO"},
            {"FIJI","FJ"},
            {"FINLAND","FI"},
            {"FRANCE","FR"},
            {"FRENCH GUIANA","GF"},
            {"FRENCH POLYNESIA","PF"},
            {"FRENCH SOUTHERN TERRITORIES","TF"},
            {"GABON","GA"},
            {"GAMBIA","GM"},
            {"GEORGIA","GE"},
            {"GERMANY","DE"},
            {"GHANA","GH"},
            {"GIBRALTAR","GI"},
            {"GREECE","GR"},
            {"GREENLAND","GL"},
            {"GRENADA","GD"},
            {"GUADELOUPE","GP"},
            {"GUAM","GU"},
            {"GUATEMALA","GT"},
            {"GUERNSEY","GG"},
            {"GUINEA","GN"},
            {"GUINEA-BISSAU","GW"},
            {"GUYANA","GY"},
            {"HAITI","HT"},
            {"HEARD ISLAND AND MCDONALD ISLANDS","HM"},
            {"HOLY SEE","VA"},
            {"HONDURAS","HN"},
            {"HONG KONG","HK"},
            {"HUNGARY","HU"},
            {"ICELAND","IS"},
            {"INDIA","IN"},
            {"INDONESIA","ID"},
            {"IRAN (ISLAMIC REPUBLIC OF)","IR"},
            {"IRAQ","IQ"},
            {"IRELAND","IE"},
            {"ISLE OF MAN","IM"},
            {"ISRAEL","IL"},
            {"ITALY","IT"},
            {"JAMAICA","JM"},
            {"JAPAN","JP"},
            {"JERSEY","JE"},
            {"JORDAN","JO"},
            {"KAZAKHSTAN","KZ"},
            {"KENYA","KE"},
            {"KIRIBATI","KI"},
            {"KOREA (DEMOCRATIC PEOPLE'S REPUBLIC OF)","KP"},
            {"KOREA (REPUBLIC OF)","KR"},
            {"KUWAIT","KW"},
            {"KYRGYZSTAN","KG"},
            {"LAO PEOPLE'S DEMOCRATIC REPUBLIC","LA"},
            {"LATVIA","LV"},
            {"LEBANON","LB"},
            {"LESOTHO","LS"},
            {"LIBERIA","LR"},
            {"LIBYA","LY"},
            {"LIECHTENSTEIN","LI"},
            {"LITHUANIA","LT"},
            {"LUXEMBOURG","LU"},
            {"MACAO","MO"},
            {"MACEDONIA (THE FORMER YUGOSLAV REPUBLIC OF)","MK"},
            {"MADAGASCAR","MG"},
            {"MALAWI","MW"},
            {"MALAYSIA","MY"},
            {"MALDIVES","MV"},
            {"MALI","ML"},
            {"MALTA","MT"},
            {"MARSHALL ISLANDS","MH"},
            {"MARTINIQUE","MQ"},
            {"MAURITANIA","MR"},
            {"MAURITIUS","MU"},
            {"MAYOTTE","YT"},
            {"MEXICO","MX"},
            {"MICRONESIA (FEDERATED STATES OF)","FM"},
            {"MOLDOVA (REPUBLIC OF)","MD"},
            {"MONACO","MC"},
            {"MONGOLIA","MN"},
            {"MONTENEGRO","ME"},
            {"MONTSERRAT","MS"},
            {"MOROCCO","MA"},
            {"MOZAMBIQUE","MZ"},
            {"MYANMAR","MM"},
            {"NAMIBIA","NA"},
            {"NAURU","NR"},
            {"NEPAL","NP"},
            {"NETHERLANDS","NL"},
            {"NEW CALEDONIA","NC"},
            {"NEW ZEALAND","NZ"},
            {"NICARAGUA","NI"},
            {"NIGER","NE"},
            {"NIGERIA","NG"},
            {"NIUE","NU"},
            {"NORFOLK ISLAND","NF"},
            {"NORTHERN MARIANA ISLANDS","MP"},
            {"NORWAY","NO"},
            {"OMAN","OM"},
            {"PAKISTAN","PK"},
            {"PALAU","PW"},
            {"PALESTINE STATE OF","PS"},
            {"PANAMA","PA"},
            {"PAPUA NEW GUINEA","PG"},
            {"PARAGUAY","PY"},
            {"PERU","PE"},
            {"PHILIPPINES","PH"},
            {"PITCAIRN","PN"},
            {"POLAND","PL"},
            {"PORTUGAL","PT"},
            {"PUERTO RICO","PR"},
            {"QATAR","QA"},
            {"RÉUNION","RE"},
            {"ROMANIA","RO"},
            {"RUSSIAN FEDERATION","RU"},
            {"RWANDA","RW"},
            {"SAINT BARTHÉLEMY","BL"},
            {"SAINT HELENA ASCENSION AND TRISTAN DA CUNHA","SH"},
            {"SAINT KITTS AND NEVIS","KN"},
            {"SAINT LUCIA","LC"},
            {"SAINT MARTIN (FRENCH PART)","MF"},
            {"SAINT PIERRE AND MIQUELON","PM"},
            {"SAINT VINCENT AND THE GRENADINES","VC"},
            {"SAMOA","WS"},
            {"SAN MARINO","SM"},
            {"SAO TOME AND PRINCIPE","ST"},
            {"SAUDI ARABIA","SA"},
            {"SENEGAL","SN"},
            {"SERBIA","RS"},
            {"SEYCHELLES","SC"},
            {"SIERRA LEONE","SL"},
            {"SINGAPORE","SG"},
            {"SINT MAARTEN (DUTCH PART)","SX"},
            {"SLOVAKIA","SK"},
            {"SLOVENIA","SI"},
            {"SOLOMON ISLANDS","SB"},
            {"SOMALIA","SO"},
            {"SOUTH AFRICA","ZA"},
            {"SOUTH GEORGIA AND THE SOUTH SANDWICH ISLANDS","GS"},
            {"SOUTH SUDAN","SS"},
            {"SPAIN","ES"},
            {"SRI LANKA","LK"},
            {"SUDAN","SD"},
            {"SURINAME","SR"},
            {"SVALBARD AND JAN MAYEN","SJ"},
            {"SWAZILAND","SZ"},
            {"SWEDEN","SE"},
            {"SWITZERLAND","CH"},
            {"SYRIAN ARAB REPUBLIC","SY"},
            {"TAIWAN PROVINCE OF CHINA[A]","TW"},
            {"TAJIKISTAN","TJ"},
            {"TANZANIA UNITED REPUBLIC OF","TZ"},
            {"THAILAND","TH"},
            {"TIMOR-LESTE","TL"},
            {"TOGO","TG"},
            {"TOKELAU","TK"},
            {"TONGA","TO"},
            {"TRINIDAD AND TOBAGO","TT"},
            {"TUNISIA","TN"},
            {"TURKEY","TR"},
            {"TURKMENISTAN","TM"},
            {"TURKS AND CAICOS ISLANDS","TC"},
            {"TUVALU","TV"},
            {"UGANDA","UG"},
            {"UKRAINE","UA"},
            {"UNITED ARAB EMIRATES","AE"},
            {"UNITED KINGDOM OF GREAT BRITAIN AND NORTHERN IRELAND","GB"},
            {"UNITED KINGDOM","GB"},
            {"UK","GB"},
            {"UNITED STATES OF AMERICA","US"},
            {"UNITED STATES","US"},
            {"USA","US"},
            {"US","US"},
            {"UNITED STATES MINOR OUTLYING ISLANDS","UM"},
            {"URUGUAY","UY"},
            {"UZBEKISTAN","UZ"},
            {"VANUATU","VU"},
            {"VENEZUELA (BOLIVARIAN REPUBLIC OF)","VE"},
            {"VIET NAM","VN"},
            {"VIRGIN ISLANDS (BRITISH)","VG"},
            {"VIRGIN ISLANDS (U.S.)","VI"},
            {"WALLIS AND FUTUNA","WF"},
            {"WESTERN SAHARA","EH"},
            {"YEMEN","YE"},
            {"ZAMBIA","ZM"},
            {"ZIMBABWE","ZW"}

        };
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="phoneUtil">PhoneNumbers.PhoneUtil instance</param>
        public PhoneValidationUtil(PhoneNumbers.PhoneNumberUtil phoneUtil, PhoneNumberOfflineGeocoder geoCoder)
        {
            _util = phoneUtil;
            _geocoder = geoCoder;
        }

        /// <summary>
        /// Valdates and Enriches the given Input Phone Object and returns PhoneEnrichedResult
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PhoneEnrichedResult ValidateEnrich(PhoneInputToEnrich input)
        {
            PhoneNumber phonenumberObject = null;
            PhoneNumber businessPhonenumberObject = null;
            PhoneEnriched regularPhone = new PhoneEnriched();
            PhoneEnriched businessPhone = new PhoneEnriched();
            try
            {
                string isoCountryCodeFromInput = GetISOCountryCode(input.CountryName);
                Task validateRegularPhone = null;
                Task validateBusinesPhone = null;
                if (!string.IsNullOrEmpty(input.RegularPhone))
                {
                    validateRegularPhone = Task.Run(() => { phonenumberObject = _util.Parse(input.RegularPhone, isoCountryCodeFromInput); });
                }
                if (!string.IsNullOrEmpty(input.RegularPhone))
                {
                    validateBusinesPhone = Task.Run(() => { businessPhonenumberObject = _util.Parse(input.BusinessPhone, isoCountryCodeFromInput); });
                }

                List<Task> tasks = new List<Task>();
                if (validateBusinesPhone != null)
                    tasks.Add(validateRegularPhone);
                if (validateBusinesPhone != null)
                    tasks.Add(validateBusinesPhone);
                Task.WhenAll(tasks).Wait();


                if (phonenumberObject != null)
                {
                    string regionCoundFromPhoneNumber = _util.GetRegionCodeForCountryCode(phonenumberObject.CountryCode);
                    regularPhone.E164Format = _util.Format(phonenumberObject, PhoneNumberFormat.E164);
                    regularPhone.IsValidPhone = regionCoundFromPhoneNumber == isoCountryCodeFromInput && _util.IsValidNumber(phonenumberObject);
                    regularPhone.RawInputPhone = input.RegularPhone;
                    regularPhone.RFC3966Format = _util.Format(phonenumberObject, PhoneNumberFormat.RFC3966);
                    regularPhone.PhoneLocation = _geocoder.GetDescriptionForNumber(phonenumberObject, Locale.ENGLISH);
                    regularPhone.NationalFormat = _util.Format(phonenumberObject, PhoneNumberFormat.NATIONAL);
                    regularPhone.InternationalFormat = _util.Format(phonenumberObject, PhoneNumberFormat.INTERNATIONAL);
                }


                if (businessPhonenumberObject != null)
                {
                    string regionCoundFromPhoneNumber = _util.GetRegionCodeForCountryCode(businessPhonenumberObject.CountryCode);
                    businessPhone.E164Format = _util.Format(businessPhonenumberObject, PhoneNumberFormat.E164);
                    businessPhone.IsValidPhone = regionCoundFromPhoneNumber == isoCountryCodeFromInput && _util.IsValidNumber(businessPhonenumberObject);
                    businessPhone.RawInputPhone = input.RegularPhone;
                    businessPhone.RFC3966Format = _util.Format(businessPhonenumberObject, PhoneNumberFormat.RFC3966);
                    businessPhone.PhoneLocation = _geocoder.GetDescriptionForNumber(businessPhonenumberObject, Locale.ENGLISH);
                    businessPhone.NationalFormat = _util.Format(businessPhonenumberObject, PhoneNumberFormat.NATIONAL);
                    businessPhone.InternationalFormat = _util.Format(businessPhonenumberObject, PhoneNumberFormat.INTERNATIONAL);
                }
            }
            catch(Exception ex)
            {
                /*
                 Gracefully handle exception, If any exception occurs
                 default object will be returned where as the IsValid will be false                 
                */
                
            }

            
            PhoneEnrichedResult result = new PhoneEnrichedResult()
            {
                RegularPhone = regularPhone,
                BusinessPhone = businessPhone
            };

            return result;
        }
        /// <summary>
        /// Resolve the Input CountryName to two digit ISO CountryCode
        /// </summary>
        /// <param name="CountryName"></param>
        private string GetISOCountryCode(string CountryName)
        {            
            return CountryName ==null ? string.Empty : Convert.ToString(_countryNameToISOMapping[CountryName.ToUpper()]);
        }


    }
}