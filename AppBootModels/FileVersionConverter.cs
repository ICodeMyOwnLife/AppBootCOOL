using System;
using System.ComponentModel;
using System.Globalization;


namespace AppBootModels
{
    public class FileVersionConverter: TypeConverter
    {
        #region Override
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || sourceType == typeof(Version) ||
                   base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string) || destinationType == typeof(Version) ||
                   base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var stringValue = value as string;
            if (stringValue != null) return new FileVersion(stringValue);

            var versionValue = value as Version;
            if (versionValue != null) return new FileVersion(versionValue);

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            var fileVersion = value as FileVersion;

            return destinationType == typeof(string)
                       ? fileVersion?.ToString() : destinationType == typeof(Version) ? fileVersion?.Version :
                                                       base.ConvertTo(context, culture, value, destinationType);
        }
        #endregion
    }
}