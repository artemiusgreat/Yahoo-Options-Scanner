/* Copyright (C) 2018 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBSampleApp.ui
{
    public class SmartComponentsMessage
    {
        public int BitNumber { get; private set; }
        public string Exchange { get; private set; }
        public char ExchangeChar { get; private set; }

        public SmartComponentsMessage(int bitNumber, string exchange, char exchangeChar)
        {
            BitNumber = bitNumber;
            Exchange = exchange;
            ExchangeChar = exchangeChar;
        }
    }
}
