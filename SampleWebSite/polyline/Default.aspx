﻿<%@ Page Language="C#" MasterPageFile="~/polyline/Polyline.master" AutoEventWireup="false" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>GoogleMap - GooglePolyline</title>
    <meta name="description" content="GoogleMap Control - GooglePolyline sample." />
    <meta name="keywords" content="asp.net artem googlemap control polyline" />
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="main" runat="Server">
    <h1>
        Google Polyline
    </h1>
    <p>
        Common GooglePolyline usage and settings.
        <br />
        Change settings in the panel bellow and then click 'Show' to see how they are applied.
    </p>
    <div class="map-wrap">
        <artem:GoogleMap ID="GoogleMap1" runat="server" Width="640px" Height="600px" Latitude="42.1229"
            Longitude="24.7879" Zoom="7" CssClass="map">
        </artem:GoogleMap>
        <artem:GooglePolyline TargetControlID="GoogleMap1" runat="server" StrokeWeight="10"
            OnClientClick="handleClick" Clickable="true">
            <artem:LatLng Latitude="42.14304" Longitude="24.74967" />
            <artem:LatLng Latitude="42.69649" Longitude="23.32601" />
        </artem:GooglePolyline>
    </div>
    <fieldset>
        <legend>Settings</legend>
        <asp:Button runat="server" Text="Show" />
    </fieldset>
    <script type="text/javascript">
        function handleClick(sender, e) {
            if (console) console.dir(e);
            alert("GooglePolylien click event was fired!");
        }
    </script>
</asp:Content>
