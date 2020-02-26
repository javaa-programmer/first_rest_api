using System;
using System.Globalization;

namespace first_rest_api.Utilities  {
    public static class ConversionUtils {
        public static int ToInt(this object value)
        {
            int result;
            if(value != null)
            {
                if(int.TryParse(Convert.ToString(value, new CultureInfo("en-US")), out result))
                {
                    return result;
                }
                else
                {
                    throw new InvalidCastException();
                }
            }
            else
            {
                throw new InvalidCastException();
            }
            
        }
    }
}