using System;
using System.Threading;
using System.Threading.Tasks;

namespace BO
{

    /// <summary>
    /// Class Position
    /// Issue de la classe gérant les localisations sur un projet Xamarin : 
    /// https://github.com/XLabs/Xamarin-Forms-Labs/blob/7a58dc349f17351813afa24df97ef7fea545a833/src/Platform/XLabs.Platform/Services/GeoLocation/Position.cs
    /// Utilisée une classe dérivée de la classe Point pour sauvegarder une coordonnée GPS
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Position" /> class.
        /// </summary>
        public Position()
        {
        }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>The timestamp.</value>
        public DateTimeOffset Timestamp { get; set; }

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