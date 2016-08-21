﻿namespace Velyo.Google.Map.UI
{
    /// <summary>
    /// Options for the rendering of the navigation control. 
    /// </summary>
    public class NavigationControlOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationControlOptions"/> class.
        /// </summary>
        public NavigationControlOptions()
        {
            this.Position = ControlPosition.TopLeft;
        }


        /// <summary>
        /// Position id. Used to specify the position of the control on the map. 
        /// The default position is <c>TopLeft</c>.
        /// </summary>
        /// <value>The position.</value>
        public ControlPosition Position { get; set; }

        /// <summary>
        /// Style id. Used to select what style of navigation control to display.
        /// </summary>
        /// <value>The style.</value>
        public NavigationControlStyle ViewStyle { get; set; }

    }
}
