﻿using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;
using System;
using System.Runtime.InteropServices;

namespace LegacySystem
{
    /**
     * Helpers for missing functions of classes, turned into extensions instead...
    */
    public static class MissingExtensions
    {
        /**
         * Helping Metro convert from System.Format(string,Object) to Windows App Store's System.Format(string,Object[]).
         */
        public static string Format(string format, System.Object oneParam)
        {
            return System.String.Format(format, new System.Object[] { oneParam });
        }

        /**
         * StringBuilder.AppendFormat(arg0,arg1,arg2) isn't implemented on WP8, so use this instead.
         */
        public static StringBuilder AppendFormatEx(this StringBuilder sb, string format, System.Object arg0, System.Object arg1 = null, System.Object arg2 = null)
        {
            return sb.AppendFormat(format, new object[] { arg0, arg1, arg2 });
        }
    }
}

