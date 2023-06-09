﻿using Microsoft.ML.Data;

namespace ML_Stocks.ML.Objects
{
    public class Stock
    {
        [LoadColumn(0)]
        public DateTime Date { get; set; }

        [LoadColumn(1)]
        public float Close { get; set; }
    }
}
