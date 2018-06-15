/* Copyright (C) 2018 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */


namespace IBLibrary.Messages
{
  public class SecurityDefinitionOptionParameterEndMessage
  {
    private int reqId;

    public SecurityDefinitionOptionParameterEndMessage(int reqId)
    {
      RequestId = this.reqId = reqId;
    }

    public int RequestId { get; set; }
  }
}
