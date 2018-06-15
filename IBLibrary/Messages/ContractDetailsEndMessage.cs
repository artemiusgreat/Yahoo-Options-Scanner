/* Copyright (C) 2018 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */

namespace IBLibrary.Messages
{
  public class ContractDetailsEndMessage
  {
    private int requestId;

    public ContractDetailsEndMessage(int requestId)
    {
      RequestId = requestId;
    }

    public int RequestId
    {
      get { return requestId; }
      set { requestId = value; }
    }
  }
}
