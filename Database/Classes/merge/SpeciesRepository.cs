using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using FAD3.GUI.Classes;

namespace FAD3.Database.Classes.merge
{
   public class SpeciesRepository
    {
        private FADEntities _fadEntities;
        public List<Species> SpeciesList { get; set; }

        public SpeciesRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
            SpeciesList = getSpecies();
        }

        private List<Species> getSpecies()
        {
            List<Species> listSpecies= new List<Species>();
            var dt = new DataTable();
            using (var conection = new OleDbConnection(_fadEntities.ConnectionString))
            {
                try
                {
                    conection.Open();
                    string query = $"Select * from tblAllSpecies";


                    var adapter = new OleDbDataAdapter(query, conection);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        listSpecies.Clear();
                        foreach (DataRow dr in dt.Rows)
                        {
                            Species  sp = new Species();
                            sp.Generic = dr["Genus"].ToString();
                            sp.Specific = dr["species"].ToString();
                            sp.SpeciesID = dr["SpeciesGUID"].ToString();
                            sp.ListedInFishbase = (Boolean)dr["ListedFB"];

                            string fbSPNo = dr["FBSPNo"].ToString();
                            if(fbSPNo.Length>0)
                            {
                                sp.FishbaseSpeciesID = int.Parse(fbSPNo); 
                            }
                            sp.Taxa = _fadEntities.TaxaViewModel.GetTaxa(Convert.ToInt32( dr["TaxaNo"]));
                            listSpecies.Add(sp);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);

                }
                return listSpecies;
            }
        }

        public bool Add(Species sp)
        { string sql;
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                if (sp.ListedInFishbase)
                {
                    sql = $@"Insert into tblAllSpecies (Genus, species, ListedFB, TaxaNo, FBSpNo, SpeciesGUID) Values 
                           ('{sp.Generic}', '{sp.Specific}', {sp.ListedInFishbase}, {sp.Taxa.TaxaID}, {sp.FishbaseSpeciesID}, {{{sp.SpeciesID}}})";
                }
                else
                {
                    sql = $@"Insert into tblAllSpecies (Genus, species, ListedFB, TaxaNo,  SpeciesGUID) Values 
                           ('{sp.Generic}', '{sp.Specific}', {sp.ListedInFishbase}, {sp.Taxa.TaxaID}, {{{sp.SpeciesID}}})";
                }
                
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    try
                    {
                        success = update.ExecuteNonQuery() > 0;
                    }
                    catch(OleDbException dbex)
                    {
                        Logger.LogMerge(dbex.Message,true,sp);
                    }
                    catch(Exception ex)
                    {
                        Logger.Log(ex);
                    }

                }
            }
            return success;
        }

        public bool Update(Species sp)
        {
            string sql;
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                    sql = $@"Update  tblAllSpecies set
                                Genus= '{sp.Generic}',
                                species = '{sp.Specific}',
                                ListedFB = {sp.ListedInFishbase},
                                TaxaNo = {sp.Taxa.TaxaID},
                                FBSpNo={(sp.FishbaseSpeciesID==null? "null":((int)sp.FishbaseSpeciesID).ToString())}
                            WHERE SpeciesGUID = {sp.SpeciesID}";
                
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
                var sql = $"Delete * from tblAllSpecies where SpeciesGUID={{{id}}}";
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
