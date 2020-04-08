using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using USHD.Models;

namespace USHD.Controllers
{
    public class StateController : Controller
    {
        private readonly USHDContext _context;

        public StateController(USHDContext context)
        {
            _context = context;
        }

        private class StateData
        {
            public string name = "";
            public int?[] annualNtlRank = new int?[18];
            public int? currentNtlRank;
            public Dictionary<string, int?> measureAndRank = new Dictionary<string, int?>();
        }

        public string Index(string state, string year)
        {
            StateData stateData = new StateData();

            stateData.name = unAbbr(state).ToUpper();

            var AHRdata = from m in _context.Ahr
                          where m.State.Contains(stateData.name)
                          select m;

            var trendData = from m in _context.Trends
                            where m.State.Contains(state)
                            select m;

            var AHRdatalist = AHRdata.ToList();

            foreach (var line in trendData)
            {
                stateData.annualNtlRank[0] = line.Yr00;
                stateData.annualNtlRank[1] = line.Yr01;
                stateData.annualNtlRank[2] = line.Yr02;
                stateData.annualNtlRank[3] = line.Yr03;
                stateData.annualNtlRank[4] = line.Yr04;
                stateData.annualNtlRank[5] = line.Yr05;
                stateData.annualNtlRank[6] = line.Yr06;
                stateData.annualNtlRank[7] = line.Yr07;
                stateData.annualNtlRank[8] = line.Yr08;
                stateData.annualNtlRank[9] = line.Yr09;
                stateData.annualNtlRank[10] = line.Yr10;
                stateData.annualNtlRank[11] = line.Yr11;
                stateData.annualNtlRank[12] = line.Yr12;
                stateData.annualNtlRank[13] = line.Yr13;
                stateData.annualNtlRank[14] = line.Yr14;
                stateData.annualNtlRank[15] = line.Yr15;
                stateData.annualNtlRank[16] = line.Yr16;
                stateData.annualNtlRank[17] = line.Yr17;
                stateData.currentNtlRank = line.Yr17;
            }

            foreach (var line in AHRdatalist)
            {
                if (state == "VA" && line.State == "West Virginia")
                {
                } else
                {
                    stateData.measureAndRank[line.Measure] = line.Rank;
                }
                
            }

            return JsonConvert.SerializeObject(stateData);
        }

        private string unAbbr(string state)
        {
            var states = new Dictionary<string, string>();

            states.Add("AK", "Alaska"); states.Add("AL", "Alabama"); states.Add("AR", "Arkansas"); states.Add("AZ", "Arizona"); states.Add("CA", "California"); states.Add("CO", "Colorado"); states.Add("CT", "Connecticut"); states.Add("DE", "Delaware"); states.Add("DC", "District of Columbia"); states.Add("FL", "Florida");
            states.Add("GA", "Georgia"); states.Add("HI", "Hawaii"); states.Add("IA", "Iowa"); states.Add("ID", "Idaho"); states.Add("IL", "Illinois"); states.Add("IN", "Indiana"); states.Add("KS", "Kansas"); states.Add("KY", "Kentucky"); states.Add("LA", "Louisiana"); states.Add("MA", "Massachusetts"); states.Add("MD", "Maryland");
            states.Add("ME", "Maine"); states.Add("MI", "Michigan"); states.Add("MN", "Minnesota"); states.Add("MO", "Missouri"); states.Add("MS", "Mississippi"); states.Add("MT", "Montana"); states.Add("NC", "North Carolina"); states.Add("ND", "North Dakota"); states.Add("NE", "Nebraska"); states.Add("NH", "New Hampshire");
            states.Add("NJ", "New Jersey"); states.Add("NM", "New Mexico"); states.Add("NV", "Nevada"); states.Add("NY", "New York"); states.Add("OH", "Ohio"); states.Add("OK", "Oklahoma"); states.Add("OR", "Oregon"); states.Add("PA", "Pennsylvania"); states.Add("RI", "Rhode Island"); states.Add("SC", "South Carolina");
            states.Add("SD", "South Dakota"); states.Add("TN", "Tennessee"); states.Add("TX", "Texas"); states.Add("UT", "Utah"); states.Add("VA", "Virginia"); states.Add("VT", "Vermont"); states.Add("WA", "Washington"); states.Add("WI", "Wisconsin"); states.Add("WV", "West Virginia"); states.Add("WY", "Wyoming");

            return states[state];
        }
    }
}