﻿using System;

namespace FAD3.Mapping.Classes
{
    public class CreateInlandGridEventArgs : EventArgs
    {
        public string Status { get; set; }
        public string StatusDescription { get; set; }
        public int GridCount { get; set; }
        public int ShapesCount { get; set; }
    }
}