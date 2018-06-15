/* Copyright (C) 2018 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBLibrary.Messages
{
    public class HistoricalTickMessage
    {
        public int ReqId { get; private set; }
        public long Time { get; private set; }
        public double Price { get; private set; }
        public long Size { get; private set; }

        public HistoricalTickMessage(int reqId, long time, double price, long size)
        {
            ReqId = reqId;
            Time = time;
            Price = price;
            Size = size;
        }
    }
}
