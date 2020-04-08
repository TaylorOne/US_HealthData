using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using USHD.Models;

namespace USHD.Controllers
{
    public class MeasuresController : Controller
    {
        private readonly USHDContext _context;

        public MeasuresController(USHDContext context)
        {
            _context = context;
        }

        private class Data
        {
            public string legend;
            public string source;
            public decimal?[] values = new decimal?[18];
            public bool reverse = false;
            public decimal? max = 0;
            public Dictionary<string, Dictionary<string, string>> stateColors = new Dictionary<string, Dictionary<string, string>>();
        }

        public string Index(string measure)
        {
            System.Diagnostics.Debug.WriteLine("MEASURE: " + measure);
            System.Diagnostics.Debug.WriteLine("");

            Data data = new Data();

            var measureInfo = from m in _context.Measures
                              where m.Name.Contains(measure)
                              select m;

            var measureInfo2 = from m in _context.Ahr
                               where m.Measure.Contains(measure)
                               select m;
            /*
            if (!String.IsNullOrEmpty(measure))
            {
                measureInfo = measureInfo.Where(s => s.Name.Contains(measure));
                measureInfo2 = measureInfo2.Where(s => s.Measure.Contains(measure));
            }
            */

            var MI = measureInfo.ToList();
            var MI2 = measureInfo2.ToList();

            foreach (var line in MI2)
            {
                int? rank = line.Rank;
                string state = line.State;

                if (rank == null)
                {
                    continue;
                }
                else if (rank > 0 && rank <= 10)
                {
                    var Color = new Dictionary<string, string>();
                    Color["fill"] = "#F5B7B1";
                    data.stateColors[abbr(state)] = Color;
                }
                else if (rank > 10 && rank <= 20)
                {
                    var Color = new Dictionary<string, string>();
                    Color["fill"] = "#F1948A";
                    data.stateColors[abbr(state)] = Color;
                }
                else if (rank > 20 && rank <= 30)
                {
                    var Color = new Dictionary<string, string>();
                    Color["fill"] = "#EC7063";
                    data.stateColors[abbr(state)] = Color;
                }
                else if (rank > 30 && rank <= 40)
                {
                    var Color = new Dictionary<string, string>();
                    Color["fill"] = "#E74C3C";
                    data.stateColors[abbr(state)] = Color;
                }
                else if (rank > 40 && rank <= 50)
                {
                    var Color = new Dictionary<string, string>();
                    Color["fill"] = "#CB4335";
                    data.stateColors[abbr(state)] = Color;
                }
                
            }

            foreach (var line in MI)
            {
                data.values[0] = line.Yr00;
                data.values[1] = line.Yr01;
                data.values[2] = line.Yr02;
                data.values[3] = line.Yr03;
                data.values[4] = line.Yr04;
                data.values[5] = line.Yr05;
                data.values[6] = line.Yr06;
                data.values[7] = line.Yr07;
                data.values[8] = line.Yr08;
                data.values[9] = line.Yr09;
                data.values[10] = line.Yr10;
                data.values[11] = line.Yr11;
                data.values[12] = line.Yr12;
                data.values[13] = line.Yr13;
                data.values[14]= line.Yr14;
                data.values[15] = line.Yr15;
                data.values[16] = line.Yr16;
                data.values[17] = line.Yr17;
            }

            for (int i = 0; i < data.values.Length; i++)
            {
                if (data.values[i] > data.max)
                {
                    data.max = (decimal)data.values[i];
                }
            }

            data.legend = MI[0].Legend;
            data.source = MI[0].Source;
            data.max = roundNum(data.max);

            return JsonConvert.SerializeObject(data);
        }

        private string abbr(string state)
        {
            var states = new Dictionary<string, string>();

            states.Add("Alaska", "AK"); states.Add("Alabama", "AL"); states.Add("Arkansas", "AR"); states.Add("Arizona", "AZ"); states.Add("California", "CA"); states.Add("Colorado", "CO"); states.Add("Connecticut", "CT"); states.Add("Delaware", "DE"); states.Add("District of Columbia", "DC"); states.Add("Florida", "FL");
            states.Add("Georgia", "GA"); states.Add("Hawaii", "HI"); states.Add("Iowa", "IA"); states.Add("Idaho", "ID"); states.Add("Illinois", "IL"); states.Add("Indiana", "IN"); states.Add("Kansas", "KS"); states.Add("Kentucky", "KY"); states.Add("Louisiana", "LA"); states.Add("Massachusetts", "MA"); states.Add("Maryland", "MD");
            states.Add("Maine", "ME"); states.Add("Michigan", "MI"); states.Add("Minnesota", "MN"); states.Add("Missouri", "MO"); states.Add("Mississippi", "MS"); states.Add("Montana", "MT"); states.Add("North Carolina", "NC"); states.Add("North Dakota", "ND"); states.Add("Nebraska", "NE"); states.Add("New Hampshire", "NH");
            states.Add("New Jersey", "NJ"); states.Add("New Mexico", "NM"); states.Add("Nevada", "NV"); states.Add("New York", "NY"); states.Add("Ohio", "OH"); states.Add("Oklahoma", "OK"); states.Add("Oregon", "OR"); states.Add("Pennsylvania", "PA"); states.Add("Rhode Island", "RI"); states.Add("South Carolina", "SC");
            states.Add("South Dakota", "SD"); states.Add("Tennessee", "TN"); states.Add("Texas", "TX"); states.Add("Utah", "UT"); states.Add("Virginia", "VA"); states.Add("Vermont", "VT"); states.Add("Washington", "WA"); states.Add("Wisconsin", "WI"); states.Add("West Virginia", "WV"); states.Add("Wyoming", "WY");

            return states[state];
        }

        private decimal? roundNum(decimal? value)
        {
            if (value == 0)
            {
                return 8;
            }
            else
            {
                double copy = (double)value;
                double length = Math.Floor(Math.Log10(copy) + 1);

                if (length == 4)
                {
                    copy = (Math.Truncate(copy / 1000) + 1) * 1000;
                }
                else if (length == 3)
                {
                    copy = (Math.Truncate(copy / 100) + 1) * 100;
                }
                else if (length == 2)
                {
                    copy = (Math.Truncate(copy / 10) + 1) * 10;
                }
                else if (length == 1)
                {
                    if (copy > 5)
                    {
                        copy = 12;
                    }
                    else
                    {
                        copy = 8;
                    }
                }

                return (decimal?)copy;
            }
            
        }
    }
}
 