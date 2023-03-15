using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GrandesCuentas.AFF
{
    public class p3_Load
    {
        public void Transfer(List<Acoount> accounts)
        {
            foreach (Acoount item in accounts)
            {
                Insert_TBL_TMP(item);
            }
        }

        public void Delete_TBL_TMP()
        {
            using (SqlConnection cn = new SqlConnection(Settings.Conexion(DB.AFF_SQL)))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = "usp_Delete_GreatAccountsTemp";
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();

                    cmd.ExecuteNonQuery();
                };
            }
        }

        void Insert_TBL_TMP(Acoount item)
        {
            using (SqlConnection cn = new SqlConnection(Settings.Conexion(DB.AFF_SQL)))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = "usp_Insert_GreatAccountsTemp";
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("SCOMPANY", item.SCOMPANY);
                    cmd.Parameters.AddWithValue("SCOMPANY_NAME", item.SCOMPANY_NAME);
                    cmd.Parameters.AddWithValue("SSYSTEM", item.SSYSTEM);
                    cmd.Parameters.AddWithValue("SREGION", item.SREGION);
                    cmd.Parameters.AddWithValue("NOFFICE", item.NOFFICE);
                    cmd.Parameters.AddWithValue("SOFFICE", item.SOFFICE);
                    cmd.Parameters.AddWithValue("NBRANCH", item.NBRANCH);
                    cmd.Parameters.AddWithValue("SBRANCH", item.SBRANCH);
                    cmd.Parameters.AddWithValue("NPRODUCT", item.NPRODUCT);
                    cmd.Parameters.AddWithValue("SPRODUCT", item.SPRODUCT);
                    cmd.Parameters.AddWithValue("NINTERMED", item.NINTERMED);
                    cmd.Parameters.AddWithValue("SINTERMED", item.SINTERMED);
                    cmd.Parameters.AddWithValue("SCLIENT", item.SCLIENT);
                    cmd.Parameters.AddWithValue("SCLIENT_NAME", item.SCLIENT_NAME);
                    cmd.Parameters.AddWithValue("SADDRESS", item.SADDRESS);
                    cmd.Parameters.AddWithValue("SPHONE", item.SPHONE);
                    cmd.Parameters.AddWithValue("SCELLPHONE", item.SCELLPHONE);
                    cmd.Parameters.AddWithValue("SEMAIL", item.SEMAIL);
                    cmd.Parameters.AddWithValue("NPOLICY", item.NPOLICY);
                    cmd.Parameters.AddWithValue("NCERTIF", item.NCERTIF);
                    cmd.Parameters.AddWithValue("NQUANTITY_CERTIF", item.NQUANTITY_CERTIF);
                    cmd.Parameters.AddWithValue("SCOLINVOT", item.SCOLINVOT);
                    cmd.Parameters.AddWithValue("DSTARTDATE", item.DSTARTDATE);
                    cmd.Parameters.AddWithValue("DEXPIRDAT", item.DEXPIRDAT);
                    cmd.Parameters.AddWithValue("SFLAG_VALIDITY", item.SFLAG_VALIDITY);
                    cmd.Parameters.AddWithValue("SFLAG_MASSIVE", item.SFLAG_MASSIVE);
                    cmd.Parameters.AddWithValue("SALERT", item.SALERT);
                    cmd.Parameters.AddWithValue("NCURRENCY", item.NCURRENCY);
                    cmd.Parameters.AddWithValue("SCURRENCY", item.SCURRENCY);
                    cmd.Parameters.AddWithValue("NEXCHANGE", item.NEXCHANGE);
                    cmd.Parameters.AddWithValue("SPAYFREQ", item.SPAYFREQ);
                    cmd.Parameters.AddWithValue("NPREMIUM_POSITIVE", item.NPREMIUM_POSITIVE);
                    cmd.Parameters.AddWithValue("NPREMIUM_RGC", item.NPREMIUM_RGC);
                    cmd.Parameters.AddWithValue("NCREDIT_HEALTH", item.NCREDIT_HEALTH);        
                    cmd.Parameters.AddWithValue("NPREMIUM_ISSUED_PEN", item.NPREMIUM_ISSUED_PEN);                  
                    cmd.Parameters.AddWithValue("NPREMIUM_ISSUED_USD", item.NPREMIUM_ISSUED_USD);
                    cmd.Parameters.AddWithValue("NPREMIUMN_PAID", item.NPREMIUMN_PAID);
                    cmd.Parameters.AddWithValue("NPREMIUM_RGC_PAID", item.NPREMIUM_RGC_PAID);                    
                    cmd.Parameters.AddWithValue("NPREMIUM_PAID_PEN", item.NPREMIUM_PAID_PEN);
                    cmd.Parameters.AddWithValue("NPREMIUM_PAID_USD", item.NPREMIUM_PAID_USD);
                    cmd.Parameters.AddWithValue("NPREMIUM_DEFERRED", item.NPREMIUM_DEFERRED);
                    cmd.Parameters.AddWithValue("NPREMIUM_LIBERATED", item.NPREMIUM_LIBERATED);
                    cn.Open();
                    
                    cmd.ExecuteNonQuery();
                };
            }
        }

        public void Insert_TBL_FIN()
        {
            using (SqlConnection cn = new SqlConnection(Settings.Conexion(DB.AFF_SQL)))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = "usp_Insert_GreatAccounts";
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();

                    cmd.ExecuteNonQuery();
                };
            }
        }
    }
}