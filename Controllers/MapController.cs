using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using USHD.Models;
using System.Globalization;

namespace USHD.Controllers
{
    public class MapController : Controller
    {
        private readonly USHDContext _context;

        public MapController(USHDContext context)
        {
            _context = context;
        }

        public string Index(string stateAbbr, string headerText)
        {
            if (stateAbbr == "DC" || headerText == "DISTRICT OF COLUMBIA")
            {
                return "";
            }
            else
            {
                var indicatorFirstLetters = new List<string> { "CAN", "CAR", "DIA", "DIS", "FRE", "INF", "PRE", "DRU", "EXC", "HIG", "OBE", "PHY", "SMO", "DEN", "LOW", "PRI", "IMM", "PUB", "UNI", "AIR", "CHI", "CHL", "PER", "SAL", "OCC", "VIO" };
                bool match = false;
                foreach (var item in indicatorFirstLetters)
                {
                    if (!String.IsNullOrEmpty(headerText) && headerText.StartsWith(item))
                    {
                        match = true;
                    }
                }

                if (match)
                {
                    TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
                    headerText = myTI.ToLower(headerText);
                    headerText = myTI.ToTitleCase(headerText);

                    var Mdfs = from m in _context.Ahr
                               where m.Measure.Contains(headerText) && m.State.Contains(unAbbr(stateAbbr))
                               select m.Value;

                    var MeasureDataForState = Mdfs.ToList();

                    var Mu = from m in _context.Measures
                             where m.Name.Contains(headerText)
                             select m.Legend;

                    var MeasureUnit = Mu.ToList();

                    return stateAbbr + " " + MeasureUnit[0] + ": " + MeasureDataForState[0];

                }
                else
                {
                    var r = from m in _context.Trends
                            where m.State.Contains(stateAbbr)
                            select m.Yr17;

                    var rank = r.ToList();

                    return stateAbbr + " rank: " + rank[0];
                }
            }
    
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