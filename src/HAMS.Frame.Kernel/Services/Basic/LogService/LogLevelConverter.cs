using System;
using log4net.Core;
using log4net.Util.TypeConverters;

namespace HAMS.Frame.Kernel.Services
{
    public class LogLevelConverter : IConvertFrom
    {
        public bool CanConvertFrom(Type sourceType)
        {
            return (sourceType == typeof(string));
        }

        public object ConvertFrom(object source)
        {
            string str = source as string;
            Level level = Level.All;

            if (str != null && str.Length > 0)
            {
                try
                {
                    switch (str)
                    {
                        case "All":
                            level = Level.All;
                            break;

                        case "Debug":
                            level = Level.Debug;
                            break;

                        case "Info":
                            level = Level.Info;
                            break;

                        case "Warn":
                            level = Level.Warn;
                            break;

                        case "Error":
                            level = Level.Error;
                            break;

                        case "Fatal":
                            level = Level.Fatal;
                            break;

                        default:
                            level = Level.All;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    throw ConversionNotSupportedException.Create(typeof(Level), source, ex);
                }
            }

            return level;
        }
    }
}