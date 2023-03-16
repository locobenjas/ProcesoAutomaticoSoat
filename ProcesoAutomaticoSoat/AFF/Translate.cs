using System;
using System.Data;
using System.Text;
using System.Configuration;

namespace GrandesCuentas.AFF
{
    public  class Translate
    {
        public Acoount BetweenReaderAndAcoount(IDataReader reader)
        {
            //string SADDRESS = string.Empty;

            //try
            //{
            //    SADDRESS = reader["SADDRESS"].ToString().Trim();
            //}
            //catch
            //{
            //    SADDRESS = "Inconsistencia en la dirección del cliente..!";
            //}

            //Acoount item = new Acoount
            //{
            //    SCOMPANY = reader["SCOMPANY"].ToString().Trim(),
            //    SCOMPANY_NAME = reader["SCOMPANY_NAME"].ToString().Trim(),
            //    SSYSTEM = reader["SSYSTEM"].ToString().Trim(),
            //    SREGION = reader["SREGION"].ToString().Trim(),
            //    NOFFICE = intNotNull(reader["NOFFICE"]),
            //    SOFFICE = reader["SOFFICE"].ToString().Trim(),
            //    NBRANCH = intNotNull(reader["NBRANCH"]),
            //    SBRANCH = reader["SBRANCH"].ToString().Trim(),
            //    NPRODUCT = intNotNull(reader["NPRODUCT"]),
            //    SPRODUCT = reader["SPRODUCT"].ToString().Trim(),
            //    NINTERMED = intNotNull(reader["NINTERMED"]),
            //    SINTERMED = reader["SINTERMED"].ToString().Trim(),
            //    SCLIENT = reader["SCLIENT"].ToString().Trim(),
            //    SCLIENT_NAME = reader["SCLIENT_NAME"].ToString().Trim(),
            //    SADDRESS = SADDRESS,
            //    SPHONE = reader["SPHONE"].ToString().Trim(),
            //    SCELLPHONE = reader["SCELLPHONE"].ToString().Trim(),
            //    SEMAIL = reader["SEMAIL"].ToString().Trim(),
            //    NPOLICY = intNotNull(reader["NPOLICY"]),
            //    NCERTIF = intNotNull(reader["NCERTIF"]),
            //    NQUANTITY_CERTIF = intNotNull(reader["NQUANTITY_CERTIF"]),
            //    SCOLINVOT = reader["SCOLINVOT"].ToString().Trim(),
            //    DSTARTDATE = dttNotNull(reader["DSTARTDATE"]),
            //    DEXPIRDAT = dttNotNull(reader["DEXPIRDAT"]),
            //    SFLAG_VALIDITY = reader["SFLAG_VALIDITY"].ToString().Trim(),
            //    SFLAG_MASSIVE = reader["SFLAG_MASSIVE"].ToString().Trim(),
            //    SALERT = reader["SALERT"].ToString().Trim(),
            //    NCURRENCY = intNotNull(reader["NCURRENCY"]),
            //    SCURRENCY = reader["SCURRENCY"].ToString().Trim(),
            //    NEXCHANGE = decNotNull(reader["NEXCHANGE"]),
            //    SPAYFREQ = reader["SPAYFREQ"].ToString().Trim(),
            //    NPREMIUM_ISSUED = decNotNull(reader["NPREMIUM_ISSUED"]),
            //    NPREMIUM_PAID = decNotNull(reader["NPREMIUM_PAID"]),
            //    NPREMIUM_ISSUED_USD = decNotNull(reader["NPREMIUM_ISSUED_USD"]),
            //    NPREMIUM_PAID_USD = decNotNull(reader["NPREMIUM_PAID_USD"]),
            //};

            Acoount item = new Acoount();

            
                item.SCOMPANY            = reader["SCOMPANY"].ToString().Trim();
                item.SCOMPANY_NAME       = reader["SCOMPANY_NAME"].ToString().Trim();
                item.SSYSTEM             = reader["SSYSTEM"].ToString().Trim();
                item.SREGION             = reader["SREGION"].ToString().Trim();
                item.NOFFICE             = intNotNull(reader["NOFFICE"]);
                item.SOFFICE             = reader["SOFFICE"].ToString().Trim();
                item.NBRANCH             = intNotNull(reader["NBRANCH"]);
                item.SBRANCH             = reader["SBRANCH"].ToString().Trim();
                item.NPRODUCT            = intNotNull(reader["NPRODUCT"]);
                item.SPRODUCT            = reader["SPRODUCT"].ToString().Trim();
                item.NINTERMED           = intNotNull(reader["NINTERMED"]);
                item.SINTERMED           = strSpecial(reader, "SINTERMED");
                item.SCLIENT             = strSpecial(reader, "SCLIENT");
                item.SCLIENT_NAME        = strSpecial(reader, "SCLIENT_NAME");
                item.SADDRESS            = strSpecial(reader, "SADDRESS");
                item.SPHONE              = reader["SPHONE"].ToString().Trim();
                item.SCELLPHONE          = reader["SCELLPHONE"].ToString().Trim();
                item.SEMAIL              = reader["SEMAIL"].ToString().Trim();
                item.NPOLICY             = intNotNull(reader["NPOLICY"]);
                item.NCERTIF             = intNotNull(reader["NCERTIF"]);
                item.NQUANTITY_CERTIF = intNotNull(reader["NQUANTITY_CERTIF"]); 
                item.SCOLINVOT           = reader["SCOLINVOT"].ToString().Trim();
                item.DSTARTDATE          = dttNotNull(reader["DSTARTDATE"]);
                item.DEXPIRDAT           = dttNotNull(reader["DEXPIRDAT"]);
                item.SFLAG_VALIDITY      = reader["SFLAG_VALIDITY"].ToString().Trim();
                item.SFLAG_MASSIVE       = reader["SFLAG_MASSIVE"].ToString().Trim();
                item.SALERT              = reader["SALERT"].ToString().Trim();
                item.NCURRENCY           = intNotNull(reader["NCURRENCY"]);
                item.SCURRENCY           = reader["SCURRENCY"].ToString().Trim();
                item.NEXCHANGE           = decNotNull(reader["NEXCHANGE"]);
                item.SPAYFREQ            = reader["SPAYFREQ"].ToString().Trim();
                item.NPREMIUM_POSITIVE   = decNotNull(reader["NPREMIUM_POSITIVE"]);
                item.NPREMIUM_RGC        = decNotNull(reader["NPREMIUM_RGC"]);
                item.NCREDIT_HEALTH      = decNotNull(reader["NCREDIT_HEALTH"]);    
                item.NPREMIUM_ISSUED_PEN = decNotNull(reader["NPREMIUM_ISSUED_PEN"]);                
                item.NPREMIUM_ISSUED_USD = decNotNull(reader["NPREMIUM_ISSUED_USD"]);
                item.NPREMIUMN_PAID      = decNotNull(reader["NPREMIUMN_PAID"]);
                item.NPREMIUM_RGC_PAID   = decNotNull(reader["NPREMIUM_RGC_PAID"]);
                item.NPREMIUM_PAID_PEN   = decNotNull(reader["NPREMIUM_PAID_PEN"]);
                item.NPREMIUM_PAID_USD   = decNotNull(reader["NPREMIUM_PAID_USD"]);             
                item.NPREMIUM_DEFERRED   = decNotNull(reader["NPREMIUM_DEFERRED"]);
                item.NPREMIUM_LIBERATED  = decNotNull(reader ["NPREMIUM_LIBERATED"]);
                              
            return item;
        }     

         int intNotNull(object value)
        {
            if (DBNull.Value.Equals(value))
                return 0;
            else
                return Convert.ToInt32(value);
        }

         string dttNotNull(object value)
        {
            if (DBNull.Value.Equals(value))
                return string.Empty;
            else
                return Convert.ToDateTime(value).ToString("dd/MM/yyyy");
        }

         decimal decNotNull(object value)
        {
            if (DBNull.Value.Equals(value))
                return 0;
            else
                return Convert.ToDecimal(value);
        }

         decimal decero(object values)
        {
            return 0;
        }

         string strSpecial(IDataReader reader, string name)
         {
             string field = string.Empty;

             try
             {
                 field = reader[name].ToString().Trim();
             }
             catch
             {
                 //field = string.Format("Err field [{0}]..!", name);
                 //field = string.Format("Sin Datos [{0}]..!");
                 field = "Sin Datos";
             }

             return field;
         }

        //public static string removeSpecialCharacters(object value)
        //{
        //    string str = value.ToString().Trim();

        //    StringBuilder sb = new StringBuilder();

        //    string allowedCharacters = ConfigurationManager.AppSettings["AllowedCharacters"];

        //    foreach (char c in str)
        //    {
        //        if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || allowedCharacters.Contains(c.ToString()))
        //        {
        //            sb.Append(c);
        //        }
        //    }

        //    return sb.ToString();
        //}
    }
}