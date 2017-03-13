using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WUI.Models
{
    public class CoordGpsModel
    {
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the altitude in meters relative to sea level.
        /// </summary>
        /// <value>The altitude.</value>
        public double Altitude { get; set; }

        /// <summary>
        /// Gets or sets the potential position error radius in meters.
        /// </summary>
        /// <value>The accuracy.</value>
        public double? Accuracy { get; set; }

        /// <summary>
        /// Gets or sets the potential altitude error range in meters.
        /// </summary>
        /// <value>The altitude accuracy.</value>
        /// <remarks>Not supported on Android, will always read 0.</remarks>
        public double? AltitudeAccuracy { get; set; }
    }
}