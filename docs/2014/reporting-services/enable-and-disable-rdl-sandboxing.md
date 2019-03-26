---
title: "Enable and Disable RDL Sandboxing | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: d5619e9f-ec5b-4376-9b34-1f74de6fade7
author: markingmyname
ms.author: maghan
manager: kfile
---
# Enable and Disable RDL Sandboxing
  The RDL (Report Definition Language) Sandboxing feature lets you detect and restrict the usage of specific types of resources, by individual tenants, in an environment of multiple tenants that use a single web farm of report servers. An example of this is a hosting services scenario where you might maintain a single web farm of report servers that are used by multiple tenants, and perhaps different companies. As a report server administrator, you can enable this feature to help achieve the following objectives:  
  
-   Restrict external resource sizes. External resources include images, .xslt files, and map data.  
  
-   At report publish time, limit types and members that are used in expression text.  
  
-   At report processing time, limit the length of the text and the size of the return value for expressions.  
  
 When RDL Sandboxing is enabled, the following features are disabled:  
  
-   Custom code in the **\<Code>** element of a report definition.  
  
-   RDL backward compatibility mode for [!INCLUDE[ssRSversion2005](../includes/ssrsversion2005-md.md)] custom report items.  
  
-   Named parameters in expressions.  
  
 This topic describes each element in the <`RDLSandboxing`> element in the RSReportServer.Config file. For more information about how to modify this file, see [Modify a Reporting Services Configuration File &#40;RSreportserver.config&#41;](report-server/modify-a-reporting-services-configuration-file-rsreportserver-config.md). A server trace log records activity related to the RDL Sandboxing feature. For more information about trace logs, see [Report Server Service Trace Log](report-server/report-server-service-trace-log.md).  
  
## Example Configuration  
 The following example shows the settings and example values for the <`RDLSandboxing`> element in the RSReportServer.Config file.  
  
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
  
## Configuration Settings  
 The following table provides information about configuration settings. Settings are presented in the order in which they appear in the configuration file.  
  
|Setting|Description|  
|-------------|-----------------|  
|**MaxExpressionLength**|Maximum number of characters allowed in RDL expressions.<br /><br /> Default: 1000|  
|**MaxResourceSize**|Maximum number of KB allowed for an external resource.<br /><br /> Default: 100|  
|**MaxStringResultLength**|Maximum number of characters allowed in a return value for an RDL expression.<br /><br /> Default: 1000|  
|**MaxArrayResultLength**|Maximum number of items allowed in an array return value for an RDL expression.<br /><br /> Default: 100|  
|**Types**|The list of members to allow within RDL expressions.|  
|**Allow**|A type or set of types to allow in RDL expressions.|  
|**Namespace**|Attribute for **Allow** that is the namespace that contains one or more types that apply to Value. This property is case-insensitive.|  
|`AllowNew`|Boolean attribute for **Allow** that controls whether new instances of the type are allowed to be created in RDL expressions or in an RDL **\<Class>** element.<br /><br /> Note: When `RDLSandboxing` is enabled, new arrays cannot be created in RDL expressions, regardless of the setting of `AllowNew`.|  
|**Value**|Value for **Allow** that is the name of the type to allow in RDL expressions. The value **\*** indicates that all types in the namespace are allowed. This property is case-insensitive.|  
|**Members**|For the list of types that are include in the **\<Types>** element, the list of member names that are not allowed in RDL expressions.|  
|**Deny**|The name of a member that is not allowed in RDL expressions. This property is case-insensitive.<br /><br /> Note: When **Deny** is specified for a member, all members with this name for all types are not allowed.|  
  
## Working with Expressions when RDL Sandboxing is Enabled  
 You can modify the RDL Sandboxing feature to help manage the resources that are used by an expression in the following ways:  
  
-   Restrict the number of characters that are used for an expression.  
  
-   Restrict the size of the result returned by an expression.  
  
-   Allow a specific list of types that can be used in an expression.  
  
-   Restrict the list of members by name for the list of allowed types that can be used in an expression.  
  
-   The RDL Sandboxing feature enables you to create a list of approved types and a list of denied members. The list of approved types is called an allow list. The list of denied members is called a block list.  
  
> [!NOTE]  
>  In the report definition, a computer cannot know the type of each instances of an expression reference. When you add a member to the block list, you are denying all members of that name across all types in the allow list.  
  
 RDL expression results are verified at run time. RDL expressions are verified in the report definition when the report is published. Monitor the report server trace log for violations. For more information, see [Report Server Service Trace Log](report-server/report-server-service-trace-log.md).  
  
### Working with Types  
 When you add a type to the allow list, you are controlling the following entry points to access RDL expressions:  
  
-   Static members of a type.  
  
-   The [!INCLUDE[vbprvb](../includes/vbprvb-md.md)] `New` method.  
  
-   The **\<Classes>** element in the report definition.  
  
-   Members that you have added to the block list for a type in the allow list.  
  
 The allow list does not control the following entry points:  
  
-   Report datasets. Fields in report datasets that are returned from queries might contain any valid RDL type.  
  
-   Report parameters. User-supplied parameter values might contain any valid RDL type.  
  
-   Members of an enabled type that are not in the block list. By default, all members of all types in the allow list are enabled. When you add a member name to the block list, you are denying all members with that name across all types that are in the allow list.  
  
 To enable a member of one type but deny a member with the same name for a different type, you must do the following:  
  
-   Add a **\<Deny>** element for the member name.  
  
-   Create a proxy member with a different name on a class in a custom assembly for the member that you want to enable.  
  
-   Add that new class to the allow list.  
  
 To add [!INCLUDE[vbprvb](../includes/vbprvb-md.md)] .NET Framework functions to the allow list, add the corresponding types from the Microsoft.VisualBasic namespace to the allow list.  
  
 To add [!INCLUDE[vbprvb](../includes/vbprvb-md.md)] .NET Framework type keywords to the allow list, add the corresponding CLR type to the allow list. For example, to use the [!INCLUDE[vbprvb](../includes/vbprvb-md.md)] .NET Framework keyword `Integer`, add the following XML fragment to the **\<RDLSandboxing>** element:  
  
```  
<Allow Namespace="System">Int32</Allow>  
```  
  
 To add a generic or a [!INCLUDE[vbprvb](../includes/vbprvb-md.md)] .NET Framework nullable type to the allow list, you must do the following:  
  
-   Create a proxy type for the generic or [!INCLUDE[vbprvb](../includes/vbprvb-md.md)] .NET Framework nullable type.  
  
-   Add the proxy type to the allow list.  
  
 Adding a type from a custom assembly to the allow list does not implicitly grant execute permission on the assembly. You must specifically modify the code access security file and provide execute permission to your assembly. For more information, see [Code Access Security in Reporting Services](extensions/secure-development/code-access-security-in-reporting-services.md).  
  
#### Maintaining the \<Deny> List of Members  
 When you add a new type to the allow list, use the following list to determine when you might have to update the block list of members:  
  
-   When you update a custom assembly with a version that introduces new types.  
  
-   When you add members to the types in the allow list.  
  
-   When you update the [!INCLUDE[dnprdnshort](../includes/dnprdnshort-md.md)] on the report server.  
  
-   When you upgrade the report server to a later version of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)].  
  
-   When you update a report server to handle a later RDL schema, because new members might have been added to RDL types.  
  
### Working with Operators and New  
 By default, [!INCLUDE[vbprvb](../includes/vbprvb-md.md)] .NET Framework language operators, except for `New`, are always allowed. The `New` operator is controlled by the `AllowNew` attribute on the **\<Allow>** element. Other language operators, such as the default collection accessor operator `!` and [!INCLUDE[vbprvb](../includes/vbprvb-md.md)] .NET Framework cast macros such as `CInt`, are always allowed.  
  
 Adding operators to a block list, including custom operators, is not supported. To exclude operators for a type, you must do the following:  
  
-   Create a proxy type that does not implement the operators that you want to exclude.  
  
-   Add the proxy type to the allow list.  
  
 To create a new array in an RDL expression, create the array in a method on a class that you define, and add that class to the allow list.  
  
 To create a new array in an RDL expression, you must do the following:  
  
-   Define a new class and create the array in a method on that class.  
  
-   Add the class to the allow list.  
  
## See Also  
 [RSReportServer Configuration File](report-server/rsreportserver-config-configuration-file.md)   
 [Report Server Service Trace Log](report-server/report-server-service-trace-log.md)  
  
  
