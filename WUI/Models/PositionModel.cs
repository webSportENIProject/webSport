using System;
using System.Threading;
using System.Threading.Tasks;

namespace WUI.Models
{

    /// <summary>
    /// Class Position
    /// </summary>
    public class PositionModel
    {
        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>The timestamp.</value>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the GPS coordinates
        /// </summary>
        /// <value>The GPS coordinates.</value>
        public CoordGpsModel GpsCoordinates { get; set; }

        /// <summary>
        /// Gets or sets the heading in degrees relative to true North.
        /// </summary>
        /// <value>The heading.</value>
        public double? Heading { get; set; }

        /// <summary>
        /// Gets or sets the speed in meters per second.
        /// </summary>
        /// <value>The speed.</value>
        public double Speed { get; set; }
    }

}