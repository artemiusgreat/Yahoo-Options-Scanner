/* Copyright (C) 2018 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */

namespace IBLibrary.Messages
{
  public class ScannerParametersMessage
  {
    private string xmlData;

    public ScannerParametersMessage(string data)
    {
      XmlData = data;
    }

    public string XmlData
    {
      get { return xmlData; }
      set { xmlData = value; }
    }
  }
}
