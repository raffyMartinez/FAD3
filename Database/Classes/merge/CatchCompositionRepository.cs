using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
namespace FAD3.Database.Classes.merge
{
    public class CatchCompositionRepository
    {
        private FADEntities _fadEntities;
        public List<CatchComposition> CatchCompositions { get; set; }

        public CatchCompositionRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            CatchCompositions = getCatchCompositions();
        }

        private List<CatchComposition> getCatchCompositions()
        {
            List<CatchComposition> thisList = new List<CatchComposition>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $"Select * from tblCatchComp";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        thisList.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {

                            var nametype = Identification.LocalName;
                            string nameGuid = dr["NameGUID"].ToString();
                            CatchLocalName ln = null;
                            Species sp = null;
                            Identification NameType = (Identification)Enum.Parse(typeof(Identification),dr["NameType"].ToString());
                            switch (NameType)
                            {
                                case Identification.LocalName:
                                    ln = _fadEntities.CatchLocalNameViewModel.GetCatchLocalName(nameGuid);
                                    break;
                                case Identification.Scientific:
                                    nametype = Identification.Scientific;
                                    sp = _fadEntities.SpeciesViewModel.GetSpecies(nameGuid);
                                    break;
                            }
                            CatchComposition cc = new CatchComposition
                            {
                                CatchName = new CatchName { CatchNameType = nametype, Species = sp, CatchLocalName = ln },
                                NameGUID = nameGuid,
                                RowGUID = dr["RowGuid"].ToString(),
                                NameType = nametype,
                                SamplingID = dr["SamplingGUID"].ToString(),
                                FADEntities = _fadEntities,
                                Sequence = string.IsNullOrEmpty(dr["Sequence"].ToString())?null:(int?)dr["Sequence"]
                            };

                            thisList.Add(cc);
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

        public bool Add(CatchComposition cc)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Insert into tblCatchComp (NameGUID, NameType,RowGUID,SamplingGUID,Sequence)
                           Values 
                           ({{{cc.NameGUID}}},{(int)cc.NameType}, {{{cc.RowGUID}}}, {{{cc.Sampling.RowID}}},{(cc.Sequence==null?"null":cc.Sequence.ToString())})";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    try
                    {
                        success = update.ExecuteNonQuery() > 0;
                    }
                    catch (OleDbException dbex)
                    {
                        Logger.LogMerge(dbex.Message,true,cc);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex);
                    }
                }
            }
            return success;
        }

        public bool Update(CatchComposition cc)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update tblCatchComp set
                                NameGUID = {{{cc.NameGUID}}},
                                NameType = {(int)cc.NameType},
                                SamplingGUID={{{cc.Sampling.RowID}}}
                            WHERE RowGUID = {{{cc.RowGUID}}}";
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
                var sql = $"Delete * from tblCatchComp where RowGUID={{{id}}}";
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
