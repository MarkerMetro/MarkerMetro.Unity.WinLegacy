using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkerMetro.Unity.WinLegacy.Reflection
{
    // Summary:
    //     Provides custom attributes for reflection objects that support them.
    // [ComVisible(true)]
    public interface ICustomAttributeProvider
    {
        // Summary:
        //     Returns an array of all of the custom attributes defined on this member,
        //     excluding named attributes, or an empty array if there are no custom attributes.
        //
        // Parameters:
        //   inherit:
        //     When true, look up the hierarchy chain for the inherited custom attribute.
        //
        // Returns:
        //     An array of Objects representing custom attributes, or an empty array.
        //
        // Exceptions:
        //   System.TypeLoadException:
        //     The custom attribute type cannot be loaded.
        //
        //   System.Reflection.AmbiguousMatchException:
        //     There is more than one attribute of type attributeType defined on this member.
        object[] GetCustomAttributes(bool inherit);
        //
        // Summary:
        //     Returns an array of custom attributes defined on this member, identified
        //     by type, or an empty array if there are no custom attributes of that type.
        //
        // Parameters:
        //   attributeType:
        //     The type of the custom attributes.
        //
        //   inherit:
        //     When true, look up the hierarchy chain for the inherited custom attribute.
        //
        // Returns:
        //     An array of Objects representing custom attributes, or an empty array.
        //
        // Exceptions:
        //   System.TypeLoadException:
        //     The custom attribute type cannot be loaded.
        //
        //   System.Reflection.AmbiguousMatchException:
        //     There is more than one attribute of type attributeType defined on this member.
        object[] GetCustomAttributes(Type attributeType, bool inherit);
        //
        // Summary:
        //     Indicates whether one or more instance of attributeType is defined on this
        //     member.
        //
        // Parameters:
        //   attributeType:
        //     The type of the custom attributes.
        //
        //   inherit:
        //     When true, look up the hierarchy chain for the inherited custom attribute.
        //
        // Returns:
        //     true if the attributeType is defined on this member; false otherwise.
        bool IsDefined(Type attributeType, bool inherit);
    }

    public static class CustomAttributeProviderExtensions
    {
        public static ICustomAttributeProvider ToICustomAttributeProvider(this System.Reflection.FieldInfo fieldInfo)
        {
            throw new NotSupportedException();
        }
    }
}
