﻿using System;
using System.Collections.Generic;

namespace Velyo.Google.Map.UI
{
    /// <summary>
    /// Class for containing map's event data.
    /// </summary>
    public class MapEventArgs : EventArgs, IScriptDataConverter
    {
        /// <summary>
        /// Gets the map bounds.
        /// </summary>
        /// <value>The bounds.</value>
        public Bounds Bounds { get; protected set; }

        /// <summary>
        /// Gets the map center.
        /// </summary>
        /// <value>The center.</value>
        public LatLng Center { get; protected set; }

        /// <summary>
        /// Gets the type of the map.
        /// </summary>
        /// <value>The type of the map.</value>
        public MapType MapType { get; protected set; }

        /// <summary>
        /// Gets the map zoom.
        /// </summary>
        /// <value>The zoom.</value>
        public int Zoom { get; protected set; }


        /// <summary>
        /// Retrieves an instance from script data.
        /// </summary>
        /// <param name="scriptObject">The script object.</param>
        /// <returns></returns>
        public static MapEventArgs FromScriptData(object scriptObject)
        {

            var data = scriptObject as IDictionary<string, object>;
            if (data != null)
            {
                var args = new MapEventArgs();
                object value;

                if (data.TryGetValue("bounds", out value)) args.Bounds = Bounds.FromScriptData(value);
                if (data.TryGetValue("center", out value)) args.Center = LatLng.FromScriptData(value);
                if (data.TryGetValue("mapType", out value))
                {
                    string name = value as string;
                    if (name != null)
                    {
                        MapType type;
                        if (Enum.TryParse<MapType>(name, true, out type)) args.MapType = type;
                    }
                }
                if (data.TryGetValue("zoom", out value)) args.Zoom = (int)value;

                return args;
            }
            return null;
        }

        /// <summary>
        /// Returns the instance as a script data.
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, object> ToScriptData()
        {

            var data = new Dictionary<string, object>();

            if (Bounds != null) data["bounds"] = Bounds.ToScriptData();
            if (Center != null) data["center"] = Center.ToScriptData();
            data["mapType"] = MapType;
            data["zoom"] = Zoom;

            return data;
        }
    }
}
