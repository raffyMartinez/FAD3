﻿using System;

namespace FAD3.Database.Classes
{
    public class EffortEventArg : EventArgs
    {
        public DateTime SamplingDate { get; internal set; }
        public string GearVarGuid { get; internal set; }
        public string LandingSiteGuid { get; internal set; }
        public double? CatchWeight { get; set; }
        public double? SampleWeight { get; set; }

        public EffortEventArg(DateTime samplingDate, string gearVarGuid, string landingSiteGuid)
        {
            SamplingDate = samplingDate;
            GearVarGuid = gearVarGuid;
            LandingSiteGuid = landingSiteGuid;
        }
    }
}