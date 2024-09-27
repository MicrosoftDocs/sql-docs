---
title: "Enable and disable RDL sandboxing for Reporting Services in SharePoint integrated mode"
description: With RDL sandboxing, you can detect and restrict the usage of types of resources by tenants where multiple tenants use a single web farm of report servers.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server-sharepoint
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Enable and disable RDL sandboxing for Reporting Services in SharePoint integrated mode

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../../includes/ssrs-appliesto-2016.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../../includes/ssrs-appliesto-not-pbirs.md)]

[!INCLUDE [ssrs-previous-versions](../../includes/ssrs-previous-versions.md)]

The Report Definition Language (RDL) sandboxing feature lets you detect and restrict the usage of specific types of resources, by individual tenants, in an environment of multiple tenants that use a single web farm of report servers. An example is a hosting services scenario where you might maintain a single web farm of report servers that are used by multiple tenants, and different companies. As a report server administrator, you can enable this feature to help achieve the following objectives:  
  
-   Restrict external resource sizes. External resources include images, .xslt files, and map data.  
  
-   At report publish time, limit types and members that are used in expression text.  
  
-   At report processing time, limit the length of the text and the size of the return value for expressions.

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.

 When RDL Sandboxing is enabled, the following features are disabled:  
  
-   Custom code in the ```<Code>``` element of a report definition.  
  
-   RDL backward compatibility mode for [!INCLUDE[ssRSversion2005](../../includes/ssrsversion2005-md.md)] custom report items.  
  
-   Named parameters in expressions.  
  
 This article describes each element in the ```<RDLSandboxing>``` element in the RSReportServer.Config file. For more information about how to modify this file, see [Modify a Reporting Services Configuration File &#40;RSreportserver.config&#41;](../../reporting-services/report-server/modify-a-reporting-services-configuration-file-rsreportserver-config.md). A server trace log records activity related to the RDL Sandboxing feature. For more information about trace logs, see [Report Server Service Trace Log](../../reporting-services/report-server/report-server-service-trace-log.md).  
  
## Example configuration
 The following example shows the settings and example values for the ```<RDLSandboxing>``` element in the RSReportServer.Config file.  
  
```  
<RDLSandboxing>  
   <MaxExpressionLength>5000</MaxExpressionLength>  
   <MaxResourceSize>5000</MaxResourceSize>  
   <MaxStringResultLength>3000</MaxStringResultLength>  
   <MaxArrayResultLength>250</MaxArrayResultLength>  
   <Types>  
      <Allow Namespace="System.Drawing" AllowNew="True">Bitmap</Allow>  
      <Allow Namespace="TypeConverters.Custom" AllowNew="True">*</Allow>  
   </Types>  
   <Members>  
      <Deny>Format</Deny>  
      <Deny>StrDup</Deny>  
   </Members>  
</RDLSandboxing>  
```

## Configuration settings

 The following table provides information about configuration settings. Settings are presented in the order in which they appear in the configuration file.  
  
|Setting|Description|  
|-------------|-----------------|  
|**MaxExpressionLength**|Maximum number of characters allowed in RDL expressions.<br /><br /> Default: 1000|  
|**MaxResourceSize**|Maximum number of KB allowed for an external resource.<br /><br /> Default: 100|  
|**MaxStringResultLength**|Maximum number of characters allowed in a return value for an RDL expression.<br /><br /> Default: 1000|  
|**MaxArrayResultLength**|Maximum number of items allowed in an array return value for an RDL expression.<br /><br /> Default: 100|  
|**Types**|The list of members to allow within RDL expressions.|  
|**Allow**|A type or set of types to allow in RDL expressions.|  
|**Namespace**|Attribute for ```Allow``` that is the namespace that contains one or more types that apply to Value. This property is case-insensitive.|  
|**AllowNew**|Boolean attribute for ```Allow``` that controls whether new instances of the type are allowed in RDL expressions or in an RDL **\<Class>** element.<br /><br /> When ```RDLSandboxing``` is enabled, new arrays can't be created in RDL expressions, regardless of the setting of **AllowNew**.|  
|**Value**|Value for ```Allow``` that is the name of the type to allow in RDL expressions. The value ```*``` indicates that all types in the namespace are allowed. This property is case-insensitive.|  
|**Members**|Value for the list of types that are in the ```<Types>``` element, and the list of member names that aren't allowed in RDL expressions.|  
|**Deny**|The name of a member that isn't allowed in RDL expressions. This property is case-insensitive.<br /><br /> When ```Deny``` is specified for a member, all members with this name for all types aren't allowed.|  
  
## Working with expressions when RDL sandboxing is enabled

You can modify the RDL Sandboxing feature to help manage the resources that are used by an expression in the following ways:  
  
-   Restrict the number of characters that are used for an expression.  
  
-   Restrict the size of the result returned by an expression.  
  
-   Allow a specific list of types that can be used in an expression.  
  
-   Restrict the list of members by name for the list of allowed types that can be used in an expression.  
  
-   The RDL Sandboxing feature enables you to create a list of approved types and a list of denied members. The list of approved types is called an allowlist. The list of denied members is called a blocklist.  
  
> [!NOTE]  
>  In the report definition, a computer cannot know the type of each instances of an expression reference. When you add a member to the block list, you are denying all members of that name across all types in the allowlist.  
  
 RDL expression results are verified at run time. RDL expressions are verified in the report definition when the report is published. Monitor the report server trace log for violations. For more information, see [Report Server Service Trace Log](../../reporting-services/report-server/report-server-service-trace-log.md).  
  
### Working with types

 When you add a type to the allowlist, you're controlling the following entry points to access RDL expressions:  
  
-   Static members of a type.  
  
-   The [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] ```New`` method.  
  
-   The ```<Classes>``` element in the report definition.  
  
-   Members that you added to the blocklist for a type in the allowlist.  
  
 The allowlist doesn't control the following entry points:  
  
-   Report datasets. Fields in report datasets that are returned from queries might contain any valid RDL type.  
  
-   Report parameters. User-supplied parameter values might contain any valid RDL type.  
  
-   Members of an enabled type that aren't in the blocklist. By default, all members of all types in the allowlist are enabled. When you add a member name to the blocklist, you're denying all members with that name across all types that are in the allowlist.  
  
 To enable a member of one type but deny a member with the same name for a different type, you must do the following actions:  
  
-   Add a ```<Deny>``` element for the member name.  
  
-   Create a proxy member with a different name on a class in a custom assembly for the member that you want to enable.  
  
-   Add that new class to the allowlist.  
  
 To add [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] .NET Framework functions to the allowlist, add the corresponding types from the ```Microsoft.VisualBasic``` namespace to the allowlist.  
  
 To add [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] .NET Framework type keywords to the allowlist, add the corresponding CLR type to the allowlist. For example, to use the [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] .NET Framework keyword **Integer**, add the following XML fragment to the ```<RDLSandboxing>``` element:  
  
```  
<Allow Namespace="System">Int32</Allow>  
```  
  
 To add a generic or a [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] .NET Framework nullable type to the allowlist, you must do the following actions:  
  
-   Create a proxy type for the generic or [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] .NET Framework nullable type.  
  
-   Add the proxy type to the allowlist.  
  
 Adding a type from a custom assembly to the allowlist doesn't implicitly grant execute permission on the assembly. You must specifically modify the code access security file and provide execute permission to your assembly. For more information, see [Code Access Security in Reporting Services](../../reporting-services/extensions/secure-development/code-access-security-in-reporting-services.md).  
  
#### Maintaining the \<Deny> list of members

 When you add a new type to the allowlist, use the following list to determine when you might have to update the blocklist of members:  
  
-   When you update a custom assembly with a version that introduces new types.  
  
-   When you add members to the types in the allowlist.  
  
-   When you update the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] on the report server.  
  
-   When you upgrade the report server to a later version of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
-   When you update a report server to handle a later RDL schema, because new members might have been added to RDL types.  
  
### Working with operators and New

 By default, [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] .NET Framework language operators, except for ```New```, are always allowed. The ```AllowNew``` attribute on the ```<Allow>``` element controls the ```New``` operator. Other language operators, such as the default collection accessor operator ```!``` and [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] .NET Framework cast macros such as **CInt**, are always allowed.  
  
 Adding operators to a blocklist, including custom operators, isn't supported. To exclude operators for a type, you must do the following actions:  
  
-   Create a proxy type that doesn't implement the operators that you want to exclude.  
  
-   Add the proxy type to the allowlist.  
  
 To create a new array in an RDL expression, create the array in a method on a class that you define, and add that class to the allowlist.  
  
 To create a new array in an RDL expression, you must do the following actions:  
  
-   Define a new class and create the array in a method on that class.  
  
-   Add the class to the allowlist.  
  
## Related content

- [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)
- [Report server service trace log](../../reporting-services/report-server/report-server-service-trace-log.md)
- [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
