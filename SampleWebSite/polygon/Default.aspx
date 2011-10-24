﻿<%@ Page Language="C#" MasterPageFile="~/polygon/Polygon.master" AutoEventWireup="false" %>

<asp:Content ContentPlaceHolderID="head" ID="headContent" runat="server">
    <title>GoogleMap - GooglePolygon</title>
    <meta name="description" content="GoogleMap Control - GooglePolygon how to use sample." />
    <meta name="keywords" content="asp.net artem googlemap control polygon" />
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="main" runat="Server">
    <h1>
        GooglePolygon
    </h1>
    <p>
        Wraps Google Maps API class GPolygon.
        <br />
        This is very similar to a GPolyline, except that you can additionally specify a
        fill color and opacity.
    </p>
    <p>
        Common GooglePolygon usage and settings.
        <br />
        Change settings in the panel bellow and then click 'Show' to see how they are applied.
    </p>
    <div class="map-wrap">
        <artem:GoogleMap ID="GoogleMap1" runat="server" Width="640px" Height="600px" Latitude="42.1229"
            Longitude="24.7879" Zoom="4" CssClass="map">
        </artem:GoogleMap>
        <artem:GooglePolygon TargetControlID="GoogleMap1" runat="server">
            <artem:LatLng Latitude="37.97918" Longitude="23.716647" />
            <artem:LatLng Latitude="41.036501" Longitude="28.984895" />
            <artem:LatLng Latitude="44.447924" Longitude="26.097879" />
            <artem:LatLng Latitude="44.802416" Longitude="20.465601" />
            <artem:LatLng Latitude="42.002411" Longitude="21.436097" />
            <artem:LatLng Latitude="37.97918" Longitude="23.716647" />
        </artem:GooglePolygon>
    </div>
    <fieldset>
        <legend>Settings</legend>
        <asp:Button runat="server" Text="Show" />
    </fieldset>
</asp:Content>
