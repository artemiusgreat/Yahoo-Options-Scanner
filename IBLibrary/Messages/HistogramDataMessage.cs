/* Copyright (C) 2018 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBLibrary.Messages
{
    public class HistogramDataMessage
    {
        public int ReqId { get; private set; }
        public IBApi.HistogramEntry[] Data { get; private set; }

        public HistogramDataMessage(int reqId, IBApi.HistogramEntry[] data)
        {
            ReqId = reqId;
            Data = data;
        }
    }
}
