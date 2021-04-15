using System.Collections.Generic;
using System.Linq;
using Tools.SqlTransact;
using WebService.Models;

namespace WebService.DataBase.Sgtin
{
    public class SgtinDataBase : DataBase, ISgtinDataBase
    {
        public SgtinDataBase(string connectionString) : base(connectionString)
        {

        }

        public void AddScannedSgtins(int gtinId, string[] sgtins)
        {
            var commandText = "Insert Into chz_scanned_sgtins(chz_gtin_id,sgtin) Values ";

            for (var i = 0; i < sgtins.Length; i++)
            {
                commandText += "(" + gtinId + ",'" + sgtins[i] + "')";
                if (i < sgtins.Length - 1)
                {
                    commandText += ",";
                }
            }

            var transaction = Transact<object>.Create(ConnectionString, commandText);
            transaction.ExecuteNonQuery();
        }

        public IEnumerable<SgtinModel> Get(int gtinId)
        {
            const string commandText = @"Select chz_sgtin_id,sgtin
            From chz_sgtins
            Where chz_gtin_id=1";

            var parameter = new GtinIdParameter() { Id = gtinId };
            var transaction = Transact<SgtinModel>.Create(ConnectionString, commandText, parameter: parameter);
            return transaction.ExecuteReader();
        }

        public bool MatchCheck(int gtin)
        {
            const string commandText = @"Select Count(*) As count, 
            (Select Count(*) From chz_sgtins Inner Join chz_scanned_sgtins On chz_sgtins.sgtin=chz_scanned_sgtins.sgtin) As match_count
            From chz_sgtins
            Where chz_sgtins.chz_gtin_id=@chz_gtin_id";

            var parameter = new GtinIdParameter() { Id = gtin };
            var transaction = Transact<MatchCheckResult>.Create(ConnectionString, commandText, parameter: parameter);
            var result = transaction.ExecuteReader().First();

            return result.Count == result.MatchCount;
        }

        public void Reset(int gtinId)
        {
            const string commandText = @"Delete From chz_scanned_sgtins
            Where chz_gtin_id =  @chz_gtin_id";

            var parameter = new GtinIdParameter() { Id = gtinId };
            var transaction = Transact<SgtinModel>.Create(ConnectionString, commandText, parameter: parameter);
            transaction.ExecuteNonQuery();
        }
    }
}