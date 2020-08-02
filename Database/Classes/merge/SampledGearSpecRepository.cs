using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
namespace FAD3.Database.Classes.merge
{
    public class SampledGearSpecRepository
    {
        private FADEntities _fadEntities;
        public List<SampledGearSpec> SampledGearSpecs{ get; set; }

        public SampledGearSpecRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            SampledGearSpecs = getSampledGearSpecs();
        }

        private List<SampledGearSpec> getSampledGearSpecs()
        {
            List<SampledGearSpec> thisList = new List<SampledGearSpec>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = @"SELECT tblSampledGearSpec.*
                                    FROM tblGearSpecs INNER JOIN tblSampledGearSpec ON tblGearSpecs.RowID = tblSampledGearSpec.SpecID
                                    WHERE tblGearSpecs.Version = '2' ";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        thisList.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            SampledGearSpec sgc = new SampledGearSpec();
                            //sgc.Sampling = _fadEntities.SamplingViewModel.GetSampling(dr["SamplingGUID"].ToString());
                            sgc.SamplingID = dr["SamplingGUID"].ToString();
                            sgc.RowID = dr["RowID"].ToString();
                            sgc.GearSpec = _fadEntities.GearSpecViewModel.getGearSpec(dr["SpecID"].ToString());
                            sgc.Value = dr["Value"].ToString();
                            sgc.FADEntities = _fadEntities;
                            thisList.Add(sgc);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);

                }
                return thisList;
            }
        }

        public bool Add(SampledGearSpec sgc)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Insert into tblSampledGearSpec (SamplingGUID, RowID,SpecID, [Value])
                           Values 
                           ({{{sgc.Sampling.RowID}}},{{{sgc.RowID}}},{{{sgc.GearSpec.RowGUID}}},'{sgc.Value}')";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public bool Update(SampledGearSpec sgc)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblSampledGearSpec set
                                SpecID = {{{sgc.GearSpec.RowGUID}}},
                                Value = '{sgc.Value}',
                                SamplingGUID = {{{sgc.Sampling.RowID}}}
                            WHERE RowID = {{{sgc.RowID}}}";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public bool Delete(string id)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $"Delete * from tblSampledGearSpec where RowID={{{id}}}";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    try
                    {
                        success = update.ExecuteNonQuery() > 0;
                    }
                    catch (OleDbException)
                    {
                        success = false;
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex);
                        success = false;
                    }
                }
            }
            return success;
        }
    }
}
