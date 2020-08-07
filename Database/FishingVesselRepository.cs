using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
namespace FAD3.Database.Classes.merge
{
    public class FishingVesselRepository
    {
        FADEntities _fadEntities;
        public List<FishingVessel> FishingVessels { get; set; }
        public FishingVesselRepository(FADEntities fadEntities)
        {
            _fadEntities = fadEntities;
        }

        public bool Add(FishingVessel fv)
        {
            int vesType = 4;
            if(fv.VesselType!=VesselType.NotDetermined)
            {
                vesType = (int)fv.VesselType;
            }
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update Sampling set
                            wdt = {(fv.Breadth == null ? "null" : fv.Breadth.ToString())},
                            len = {(fv.Length == null ? "null" : fv.Length.ToString())},
                            hgt= {(fv.Depth == null ? "null" : fv.Depth.ToString())},
                            hp= {(fv.EngineHorsePower == null ? "null" : fv.EngineHorsePower.ToString())},
                            VesType = {vesType},
                            Engine - '{fv.Engine}'
                            Where SamplingGUID = {{{fv.SamplingGUID}}}";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public bool Update(FishingVessel fv)
        {
            return Add(fv);
        }

        public bool Delete(string id)
        {
            bool success = false;
            using (OleDbConnection conn = new OleDbConnection(_fadEntities.ConnectionString))
            {
                conn.Open();
                var sql = $@"Update Sampling set
                            wdt = {"null"},
                            len = {"null"},
                            hgt= {"null"},
                            hp= {"null"},
                            VesType = {(int)VesselType.NotDetermined},
                            Engine - '{string.Empty}'
                            Where SamplingGUID = {{{id}}}";
                using (OleDbCommand update = new OleDbCommand(sql, conn))
                {
                    success = update.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }
    }
}
