$(function () {
    $('#map').usmap({

        // The mouseclick action
        click: function (event, data) {
            $.ajax({
                url: "/State",
                data: {
                    state: data.name,
                    year: 2017
                },
                type: "GET",
                dataType: "json",
            })
                .done(function (json) {
                    console.log(json['name']);
                    if (json['name'] == 'DISTRICT OF COLUMBIA') {
                        $("#map").usmap('changeStateColor');
                        $("#header").html(json.name);
                        $("#outcomes").html("<span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Cancer Deaths</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Cardiovascular Deaths</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Diabetes</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Disparity in Health Status</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Frequent Mental Distress</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Frequent Physical Distress</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Infant Mortality</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Premature Death</a>");
                        $("#behaviors").html("<span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Drug Deaths</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Excessive Drinking</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>High School Graduation</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Obesity</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Physical Inactivity</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Smoking</a>");
                        $("#clinical_care").html("<span class=\"spacer_text_head\">00&nbsp;&nbsp;</span><a>Dentists</a><br><span class=\"spacer_text_head\">00&nbsp;&nbsp;</span><a>Low Birthweight</a><br><span class=\"spacer_text_head\">00&nbsp;&nbsp;</span><a>Preventable Hospitalizations</a><br><span class=\"spacer_text_head\">00&nbsp;&nbsp;</span><a>Primary Care Physicians</a>");
                        $("#policy").html("<span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Immunization Meningococcal</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Immunization Tdap</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Immunizations - Adolescents</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Immunizations - Children</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Public Health Funding</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Uninsured</a>");
                        $("#community").html("<span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Air Pollution</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Children in Poverty</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Infectious Disease</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Chlamydia</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Pertussis</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Salmonella</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Occupational Fatalities</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Violent Crime</a><span class=\"spacer_text\">00<br>00<br>00<br>00<br></span>");
                        $("#source").html("Source: <i>" + json.source + "</i>");
                    }
                    else {
                        $("#map").usmap('changeStateColor');
                        format = {};
                        chartData = {};
                        for (x in json['measureAndRank']) {
                            if (json['measureAndRank'][x] == null) {
                                continue;
                            } else {
                                if (json['measureAndRank'][x] < 10) {
                                    spacerTextBefore = "&nbsp;&nbsp;";
                                    spacerTextAfter = "</b>&nbsp;&nbsp;<a>";
                                } else {
                                    spacerTextBefore = "";
                                    spacerTextAfter = "</b>&nbsp;&nbsp;<a>";
                                }

                                if (json['measureAndRank'][x] > 45) {
                                    format[x] = spacerTextBefore + "<span class=\"bad\"><b>" + json['measureAndRank'][x] + spacerTextAfter + x + "</a></span><br>";
                                } else if (json['measureAndRank'][x] < 6) {
                                    format[x] = spacerTextBefore + "<span class=\"good\"><b>" + json['measureAndRank'][x] + spacerTextAfter + x + "</a></span><br>";
                                } else {
                                    format[x] = spacerTextBefore + "<b>" + json['measureAndRank'][x] + spacerTextAfter + x + "</a><br>";
                                }

                                /*
                                if (getLength(json[x]) == 2) {
                                    
                                } else {
                                    if (json["state"] == 'DISTRICT OF COLUMBIA') {
                                        format[x] = "<span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>" + x + "</a><br>";
                                    } else if (json[x] > 45) {
                                        format[x] = "<span class=\"bad\"><b>" + json[x] + "</b></span>&nbsp;&nbsp;&nbsp;&nbsp;<a class=\"bad\">" + x + "</a><br>";
                                    } else if (json[x] < 6) {
                                        format[x] = "<span class=\"good\"><b>" + json[x] + "</b></span>&nbsp;&nbsp;&nbsp;&nbsp;<a class=\"good\">" + x + "</a><br>";
                                    } else {
                                        format[x] = "<b>" + json[x] + "</b>" + "&nbsp;&nbsp;&nbsp;&nbsp;<a>" + x + "</a><br>";
                                    }
                                }
                                */
                            }
                        }

                        $("#header").html(json.name);
                        $("#outcomes").html(format["Cancer Deaths"] + format["Cardiovascular Deaths"] + format["Diabetes"] + format["Disparity in Health Status"] + format["Frequent Mental Distress"] + format["Frequent Physical Distress"] + format["Infant Mortality"] + format["Premature Death"]);
                        $("#behaviors").html(format["Drug Deaths"] + format["Excessive Drinking"] + format["High School Graduation"] + format["Obesity"] + format["Physical Inactivity"] + format["Smoking"]);
                        $("#clinical_care").html(format["Dentists"] + format["Low Birthweight"] + /* format["Mental Health Providers"] +*/ format["Preventable Hospitalizations"] + format["Primary Care Physicians"]);
                        $("#policy").html(/*format["Immunization HPV Females"] + format["Immunization HPV Males"] +*/ format["Immunization Meningococcal"] + format["Immunization Tdap"] + format["Immunizations - Adolescents"] + format["Immunizations - Children"] + format["Public Health Funding"] + format["Uninsured"]);
                        $("#community").html(format["Air Pollution"] + format["Children in Poverty"] + format["Infectious Disease"] + format["Chlamydia"] + format["Pertussis"] + format["Salmonella"] + format["Occupational Fatalities"] + format["Violent Crime"]);
                        $("#this_rank").html("<h2 class=\"rank\">Rank: " + json['currentNtlRank'] + "</h2>");
                        $("#source").html("Source: <i>America's Health Rankings Composite Measure</i><br><br>Rank based on weighted sum of the number of standard deviations each core measure is from the national average.");
                        $("#stateInfo").html("");

                        chartData['reverse'] = true;
                        chartData['max'] = 50;
                        chartData['legend'] = 'national rank';
                        chartData['values'] = json['annualNtlRank'];

                        addData(myChart, chartData);
                    }

                })
                .fail(function (xhr, status, errorThrown) {
                    alert("Sorry, there was a problem!");
                    console.log("Error: " + errorThrown);
                    console.log("Status: " + status);
                    console.dir(xhr);
                })
        },

        // The mouseover action
        mouseover: function (event, data) {
            $.ajax({
                url: "/Map",
                data: {
                    stateAbbr: data.name,
                    headerText: $("#header").text()
                },
                type: "GET",
                dataType: "text",
            })
                .done(function (text) {
                    if (text == undefined) {

                    } else {
                        $("#stateInfo").html(text);
                    }
                    
                })
                .fail(function (xhr, status, errorThrown) {
                    alert("Sorry, there was a problem!");
                    console.log("Error: " + errorThrown);
                    console.log("Status: " + status);
                    console.dir(xhr);
                })
        }

    });

    $('body').on('click', 'a', function () {
        $("#header").html(this.innerHTML.toUpperCase());

        $.ajax({
            url: "/Measures",
            data: { measure: this.innerHTML },
            type: "GET",
            dataType: "json",
        })
            .done(function (json) {
                addData(myChart, json);
                $("#map").usmap('changeStateColor', json["stateColors"]);
                $("#outcomes").html("<span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Cancer Deaths</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Cardiovascular Deaths</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Diabetes</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Disparity in Health Status</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Frequent Mental Distress</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Frequent Physical Distress</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Infant Mortality</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Premature Death</a>");
                $("#behaviors").html("<span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Drug Deaths</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Excessive Drinking</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>High School Graduation</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Obesity</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Physical Inactivity</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Smoking</a>");
                $("#clinical_care").html("<span class=\"spacer_text_head\">00&nbsp;&nbsp;</span><a>Dentists</a><br><span class=\"spacer_text_head\">00&nbsp;&nbsp;</span><a>Low Birthweight</a><br><span class=\"spacer_text_head\">00&nbsp;&nbsp;</span><a>Preventable Hospitalizations</a><br><span class=\"spacer_text_head\">00&nbsp;&nbsp;</span><a>Primary Care Physicians</a>");
                $("#policy").html("<span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Immunization Meningococcal</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Immunization Tdap</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Immunizations - Adolescents</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Immunizations - Children</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Public Health Funding</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Uninsured</a>");
                $("#community").html("<span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Air Pollution</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Children in Poverty</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Infectious Disease</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Chlamydia</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Pertussis</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Salmonella</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Occupational Fatalities</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Violent Crime</a><span class=\"spacer_text\">00<br>00<br>00<br>00<br></span>");
                $("#source").html("Source: <i>" + json.source + "</i>");
                $("#stateInfo").html("");
                // <span class=\"spacer_text_head\">00&nbsp;&nbsp;</span><a>Mental Health Providers</a><br> taken out from Clinical Care because of no data
                // <span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Immunization HPV Females</a><br><span class=\"spacer_text\">00&nbsp;&nbsp;</span><a>Immunization HPV Males</a><br>
            })
            .fail(function (xhr, status, errorThrown) {
                alert("Sorry, there was a problem!");
                console.log("Error: " + errorThrown);
                console.log("Status: " + status);
                console.dir(xhr);
            });

    });
});

$.fn.digits = function () {
    return this.each(function () {
        $(this).text($(this).text().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
    });
};

function removeData(chart) {
    var index;
    for (index = 50; index > 0; --index) {
        chart.data.labels.pop();
        chart.data.datasets[0].data.pop();
    }

    chart.update();
}

function addData(chart, data) {

    if (data['reverse'] == "false") {
        chart.options['scales']['yAxes'][0]['ticks']['reverse'] = false;
        chart.options['scales']['yAxes'][0]['ticks']['max'] = data['max'];
    } else {
        chart.options['scales']['yAxes'][0]['ticks']['reverse'] = data['reverse'];
        chart.options['scales']['yAxes'][0]['ticks']['max'] = data['max'];
    }

    chart.data.datasets[0].data = data['values'];
    chart.data.datasets[0].label = data['legend'];
    chart.data.datasets[0].label = data['legend'];

    chart.update();
}

function getLength(number) {
    return number.toString().length;
}