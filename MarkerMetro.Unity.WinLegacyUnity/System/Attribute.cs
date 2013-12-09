using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MarkerMetro.Unity.WinLegacy
{
#if NETFX_CORE
#pragma warning disable 109
#endif

    public abstract class Attribute : global::System.Attribute
    {
        public static new global::System.Attribute GetCustomAttribute(MemberInfo element, Type attributeType)
        {
#if NETFX_CORE
            throw new NotImplementedException();
#else
            throw new PlatformNotSupportedException();
#endif
        }

        //
        // Summary:
        //     Determines whether any custom attributes are applied to a member of a type.
        //     Parameters specify the member, and the type of the custom attribute to search
        //     for.
        //
        // Parameters:
        //   element:
        //     An object derived from the System.Reflection.MemberInfo class that describes
        //     a constructor, event, field, method, type, or property member of a class.
        //
        //   attributeType:
        //     The type, or a base type, of the custom attribute to search for.
        //
        // Returns:
        //     true if a custom attribute of type attributeType is applied to element; otherwise,
        //     false.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     element or attributeType is null.
        //
        //   System.ArgumentException:
        //     attributeType is not derived from System.Attribute.
        //
        //   System.NotSupportedException:
        //     element is not a constructor, method, property, event, type, or field.
        public static new bool IsDefined(MemberInfo element, Type attributeType)
        {
#if NETFX_CORE
            throw new NotImplementedException();
#else
            throw new NotImplementedException();
#endif
        }
    }
}
