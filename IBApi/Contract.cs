/* Copyright (C) 2018 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IBApi
{
    /**
     * @class Contract
     * @brief class describing an instrument's definition
     * @sa ContractDetails
     */
    public class Contract
    {
        private int conId;
        private string symbol;
        private string secType;
        private string lastTradeDateOrContractMonth;
        private double strike;
        private string right;
        private string multiplier;
        private string exchange;
        private string currency;
        private string localSymbol;
        private string primaryExch;
        private string tradingClass;
        private bool includeExpired;
        private string secIdType;
        private string secId;
        private string comboLegsDescription;
        private List<ComboLeg> comboLegs;
        private DeltaNeutralContract deltaNeutralContract;


        /**
        * @brief The unique IB contract identifier
        */
        public int ConId
        {
            get { return conId; }
            set { conId = value; }
        }


        /**
         * @brief The underlying's asset symbol
         */
        public string Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        /**
         * @brief The security's type:
         *      STK - stock (or ETF)
         *      OPT - option
         *      FUT - future
         *      IND - index
         *      FOP - futures option
         *      CASH - forex pair
         *      BAG - combo
         *      WAR - warrant
         *      BOND- bond
         *      CMDTY- commodity
         *      NEWS- news
         *		FUND- mutual fund
		 */
        public string SecType
        {
            get { return secType; }
            set { secType = value; }
        }

        /**
        * @brief The contract's last trading day or contract month (for Options and Futures). Strings with format YYYYMM will be interpreted as the Contract Month whereas YYYYMMDD will be interpreted as Last Trading Day.
        */
        public string LastTradeDateOrContractMonth
        {
            get { return lastTradeDateOrContractMonth; }
            set { lastTradeDateOrContractMonth = value; }
        }

        /**
         * @brief The option's strike price
         */
        public double Strike
        {
            get { return strike; }
            set { strike = value; }
        }

        /**
         * @brief Either Put or Call (i.e. Options). Valid values are P, PUT, C, CALL. 
         */
        public string Right
        {
            get { return right; }
            set { right = value; }
        }

        /**
         * @brief The instrument's multiplier (i.e. options, futures).
         */
        public string Multiplier
        {
            get { return multiplier; }
            set { multiplier = value; }
        }

        /**
         * @brief The destination exchange.
         */
        public string Exchange
        {
            get { return exchange; }
            set { exchange = value; }
        }

        /**
         * @brief The underlying's currency
         */
        public string Currency
        {
            get { return currency; }
            set { currency = value; }
        }

        /**
         * @brief The contract's symbol within its primary exchange
		 * For options, this will be the OCC symbol
         */
        public string LocalSymbol
        {
            get { return localSymbol; }
            set { localSymbol = value; }
        }

        /**
         * @brief The contract's primary exchange.
		 * For smart routed contracts, used to define contract in case of ambiguity. 
		 * Should be defined as native exchange of contract, e.g. ISLAND for MSFT
		 * For exchanges which contain a period in name, will only be part of exchange name prior to period, i.e. ENEXT for ENEXT.BE
         */
        public string PrimaryExch
        {
            get { return primaryExch; }
            set { primaryExch = value; }
        }

        /**
         * @brief The trading class name for this contract.
         * Available in TWS contract description window as well. For example, GBL Dec '13 future's trading class is "FGBL"
         */
        public string TradingClass
        {
            get { return tradingClass; }
            set { tradingClass = value; }
        }

        /**
        * @brief If set to true, contract details requests and historical data queries can be performed pertaining to expired futures contracts.
        * Expired options or other instrument types are not available.
        */
        public bool IncludeExpired
        {
            get { return includeExpired; }
            set { includeExpired = value; }
        }

        /**
         * @brief Security's identifier when querying contract's details or placing orders
         *      ISIN - Example: Apple: US0378331005
         *      CUSIP - Example: Apple: 037833100
         */
        public string SecIdType
        {
            get { return secIdType; }
            set { secIdType = value; }
        }

        /**
        * @brief Identifier of the security type
        * @sa secIdType
        */
        public string SecId
        {
            get { return secId; }
            set { secId = value; }
        }

         /**
         * @brief Description of the combo legs.
         */
        public string ComboLegsDescription
        {
            get { return comboLegsDescription; }
            set { comboLegsDescription = value; }
        }

        /**
         * @brief The legs of a combined contract definition
         * @sa ComboLeg
         */
        public List<ComboLeg> ComboLegs
        {
            get { return comboLegs; }
            set { comboLegs = value; }
        }

        /**
         * @brief Delta and underlying price for Delta-Neutral combo orders.
         * Underlying (STK or FUT), delta and underlying price goes into this attribute.
         * @sa DeltaNeutralContract
         */
        public DeltaNeutralContract DeltaNeutralContract
        {
            get { return deltaNeutralContract; }
            set { deltaNeutralContract = value; }
        }

        public override string ToString()
        {
            return SecType + " " + Symbol + " " + Currency + " " + Exchange;
        }
    }
}
