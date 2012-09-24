using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows.Markup;

// General Information about an assembly is controlled through the following set of attributes.
// Change these attribute values to modify the information associated with an assembly.
[assembly: AssemblyTitle("T2T")]
[assembly: AssemblyDescription("Tyre Industry MOM System")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Milan Stamenovic")]
[assembly: AssemblyProduct("T2T")]
[assembly: AssemblyCopyright("Copyright © Milan Stamenovic 2012")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: CLSCompliant(true)]
[assembly: NeutralResourcesLanguage("en-US")]

// Setting ComVisible to false makes the types in this assembly not visible to COM components.
// If you need to access a type in this assembly from COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("9a7cb7f0-a7bf-448a-9ae4-0f3cc557a363")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: XmlnsPrefix(GlobalConstants.XmlNamespace, "framework")]

[assembly: XmlnsDefinition(GlobalConstants.XmlNamespace, "T2T.Framework.Navigation")]
[assembly: XmlnsDefinition(GlobalConstants.XmlNamespace, "T2T.Framework.Navigation.ContentLoaders")]
[assembly: XmlnsDefinition(GlobalConstants.XmlNamespace, "T2T.Framework.Views")]

internal static class GlobalConstants
{

    internal const string XmlNamespace = "http://www.milanstamenovic.com/framework";
}
