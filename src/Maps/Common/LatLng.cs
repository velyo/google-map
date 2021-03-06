﻿using System;
using System.Collections.Generic;

namespace Velyo.Google.Maps
{
    /// <summary>
    /// LatLng is a point in geographical coordinates, latitude and longitude.
    /// Notice that although usual map projections associate longitude with the x-coordinate of the map, and latitude with the y-coordinate, the latitude coordinate is always written first, followed by the longitude.
    /// Notice also that you cannot modify the coordinates of a LatLng. If you want to compute another point, you have to create a new one.
    /// </summary>
    public class LatLng : IScriptDataConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class.
        /// </summary>
        public LatLng() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> struct.
        /// </summary>
        /// <param name="lat">The lat.</param>
        /// <param name="lng">The LNG.</param>
        public LatLng(double lat, double lng)
        {
            Latitude = lat;
            Longitude = lng;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> struct.
        /// </summary>
        /// <param name="source">The source.</param>
        public LatLng(LatLng source)
        {
            Latitude = source.Latitude;
            Longitude = source.Longitude;
        }


        /// <summary>
        /// Gets or sets the latitude in degrees.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude in degrees.
        /// </summary>
        /// <value>The longitude.</value>
        public double Longitude { get; set; }


        /// <summary>
        /// Retrieves an instance from script data.
        /// </summary>
        /// <param name="scriptObject">The script object.</param>
        /// <returns></returns>
        public static LatLng FromScriptData(object scriptObject)
        {
            var data = scriptObject as IDictionary<string, object>;
            if (data != null)
            {
                var result = new LatLng();
                object value;

                if (data.TryGetValue("lat", out value))
                    result.Latitude = Convert.ToDouble(value);
                //else if (data.TryGetValue("Ma", out value))
                //    result.Latitude = (double)(decimal)value;
                if (data.TryGetValue("lng", out value))
                    result.Longitude = Convert.ToDouble(value);
                //else if (data.TryGetValue("Na", out value))
                //    result.Longitude = (double)(decimal)value;

                return result;
            }
            return null;
        }

        /// <summary>
        /// Parses the specified pair.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public static LatLng Parse(string point)
        {
            double lat = 0D;
            double lng = 0D;

            if (!string.IsNullOrEmpty(point))
            {
                point = point.Trim('(', ')');
                string[] pair = point.Split(',');
                if (pair.Length >= 2)
                {
                    lat = JsUtility.ToDouble(pair[0]);
                    lng = JsUtility.ToDouble(pair[1]);
                }
            }

            return new LatLng(lat, lng);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Velyo.Google.UI.LatLng"/>.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator LatLng(string point)
        {
            return Parse(point);
        }

        /// <summary>
        /// Returns the instance as a script data.
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, object> ToScriptData()
        {
            return new Dictionary<string, object> { { "lat", Latitude }, { "lng", Longitude } };
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0},{1}",
                JsUtility.Encode(Latitude), JsUtility.Encode(Longitude));
        }
    }
}
