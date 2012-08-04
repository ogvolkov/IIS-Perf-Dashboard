<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IISPerfDashboard.Models.UrlStatsPageModel>" %>

<html>
<head>
    <title>Stats</title>
    <link href="Content/blue/style.css" rel="stylesheet">
    <script type="text/javascript" src="Scripts/jquery-1.4.1.js"></script>
    <script type="text/javascript" src="Scripts/jquery.tablesorter.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#stats").tablesorter({
                headers: {
                    1: { sortInitialOrder: 'desc' },
                    2: { sortInitialOrder: 'desc' },
                    3: { sortInitialOrder: 'desc' },
                    4: { sortInitialOrder: 'desc' }
                }
            });
        }
            );
    </script>
</head>
<body>    
    <table>
        <tr>
            <td>From</td>
            <td><%:Model.MinDate.ToString("r") %></td>
        </tr>
        <tr>
            <td>To</td>
            <td> <%:Model.MaxDate.ToString("r") %></td>
        </tr>
        <tr>            
            <td>Average response time</td>
            <td><b><%:Model.AverageResponse %>ms</b></td>
        </tr>
        <tr>
            <td>Average requests per second:</td>
            <td><%:Model.RequestsPerSecond.ToString("F2") %></td>
        </tr>
        <tr>
            <td>Average load:</td>
            <td><%:Model.AverageLoad.ToString("F1") %>%</td>
        </tr>
        <tr>
            <td>Total requests:</td>
            <td><%:Model.TotalRequests %></td>
        </tr>
    </table>
    <br />
    <table id="stats" border="1" class="tablesorter" border="0" cellpadding="0" cellspacing="1">
        <thead>
            <tr>
                <th>
                    Url
                </th>
                <th>
                    Usage percentage
                </th>
                <th>
                    Average time taken (ms)
                </th>
                <th>
                    Number of requests
                </th>
                <th>
                    Total time taken (ms)
                </th>
            </tr>
        </thead>
        <tbody>
            <% foreach (var url in Model.Urls)
               {%>
            <tr>
                <td>
                    <%:url.Url %>
                </td>
                <td>
                    <%:url.Percentage %>
                </td>
                <td>
                    <%:url.AverageTimeTaken %>
                </td>
                <td>
                    <%:url.Count %>
                </td>
                <td>
                    <%:url.TotalTimeTaken %>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>
</body>
</html>
