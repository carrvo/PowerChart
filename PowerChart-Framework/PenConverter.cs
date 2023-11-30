using System;
using System.Drawing;
using System.Management.Automation;

namespace PowerChart
{
    /// <summary>
    /// For converting a <see cref="String"/> to a <see cref="Pen"/>.
    /// </summary>
    public sealed class PenConverter : PSTypeConverter
    {
        /// <inheritdoc/>
        public override bool CanConvertFrom(object sourceValue, Type destinationType)
            => sourceValue.GetType() == typeof(String) && destinationType == typeof(Pen);

        /// <inheritdoc/>
        public override bool CanConvertTo(object sourceValue, Type destinationType)
            => throw new NotImplementedException();

        /// <inheritdoc/>
        public override object ConvertFrom(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
            => new Pen(Color.FromName((String)sourceValue));

        /// <inheritdoc/>
        public override object ConvertTo(object sourceValue, Type destinationType, IFormatProvider formatProvider, bool ignoreCase)
            => throw new NotImplementedException();
    }
}
