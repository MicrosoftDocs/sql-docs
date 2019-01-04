---
title: "Custom Code and Assembly References in Expressions in Report Designer (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "items [Reporting Services], expressions"
  - "data [Reporting Services], expressions"
  - "expressions [Reporting Services], about expressions"
  - "expressions [Reporting Services]"
  - "SSRS, expressions"
  - "formulas [Reporting Services]"
  - "data manipulation [Reporting Services]"
  - "SQL Server Reporting Services, expressions"
ms.assetid: ae8a0166-2ccc-45f4-8d28-c150da7b73de
author: markingmyname
ms.author: maghan
manager: craigg
---
# Custom Code and Assembly References in Expressions in Report Designer (SSRS)
  You can add references to custom code embedded in a report or to custom assemblies that you build and save to your computer and deploy to the report server. Use embedded code for custom constants, complex functions or functions that are used multiple times in a single report. Use custom code assemblies to maintain code in a single place and share it for use by multiple reports. Custom code can include new custom constants, variables, functions, or subroutines. You can include read-only references to built-in collections such as the Parameters collection. However, you cannot pass sets of report data values to custom functions; specifically, custom aggregates are not supported.  
  
> [!IMPORTANT]  
>  For time-sensitive calculations that are evaluated once at run-time and that you want to remain the same value throughout report processing, consider whether to use a report variable or group variable. For more information, see [Report and Group Variables Collections References &#40;Report Builder and SSRS&#41;](built-in-collections-report-and-group-variables-references-report-builder.md).  
  
 Report Designer is the preferred authoring environment to use to add custom code to a report. Report Builder supports processing reports that have valid expressions, or that include references to custom assemblies on a report server. Report Builder does not provide a way to add a reference to a custom assembly.  
  
> [!NOTE]  
>  Be aware that during an upgrade of a report server, reports that depend on custom assemblies might require additional steps to complete the upgrade. For more information, see [Use Upgrade Advisor to Prepare for Upgrades](../../sql-server/install/use-upgrade-advisor-to-prepare-for-upgrades.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
##  <a name="RB3"></a> Working with Custom Code in Report Builder  
 In Report Builder, you can open a report from a report server that includes references to custom assemblies. For example, you can edit reports that are created and deployed by using Report Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. The custom assemblies must be deployed to the report server.  
  
 You cannot do the following:  
  
1.  Add references or class member instances to a report.  
  
2.  Preview a report with references to custom assemblies in local mode.  
  
##  <a name="Common"></a> Including References to Commonly Used Functions  
 Use the **Expression** dialog box to view a categorized list of common functions built-in to [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. When you expand **Common Functions** and click a category, the **Item** pane displays the list of functions that you include in an expression. The common functions include classes from the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] <xref:System.Math> and <xref:System.Convert> namespaces and [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] run-time library functions. For convenience, you can view the most commonly used functions in the **Expression** dialog box, where they are listed by category: Text, Date and Time, Math, Inspection, Program Flow, Aggregate, Financial, Conversion, and Miscellaneous. Less commonly used functions do not appear in the list but can still be used in an expression.  
  
 To use a built-in function, double-click the function name in the Item pane. A description of the function appears in the Description pane and an example of the function call appears in the Example pane. In the code pane, when you type the function name followed by a left parenthesis **(**, the IntelliSense help displays each valid syntax for the function call. For example, to calculate the maximum value for a field named `Quantity` in a table, add the simple expression `=Max(` to the Code pane, and then use the smart tags to view all possible valid syntaxes for the function call. To complete this example, type `=Max(Fields!Quantity.Value)`.  
  
 For more information about each function, see <xref:System.Math>, <xref:System.Convert>, and [Visual Basic Runtime Library Members](https://go.microsoft.com/fwlink/?LinkId=198941) on MSDN.  
  
##  <a name="NotCommon"></a> Including References to Less Commonly Used Functions  
 To include a reference to other less commonly used CLR namespaces, you must use a fully qualified reference, for example, <xref:System.Text.StringBuilder>. IntelliSense is not supported in the code pane of the **Expression** dialog box for these less commonly used functions.  
  
 For more information, see [Visual Basic Runtime Library Members](https://go.microsoft.com/fwlink/?LinkId=198941) on MSDN.  
  
##  <a name="External"></a> Including References to External Assemblies  
 To include a reference to a class in an external assembly, you must identify the assembly for the report processor. Use the **References** page of the **Report Properties** dialog box to specify the fully qualified name of the assembly to add to the report. In your expression, you must use the fully qualified name for the class in the assembly. Classes in an external assembly do not appear in the **Expression** dialog box; you must provide the correct name for the class. A fully qualified name includes the namespace, the class name, and the member name.  
  
##  <a name="Embedded"></a> Including Embedded Code  
 To add embedded code to a report, use the Code tab of the **Report Properties** dialog box. The code block you create can contain multiple methods. Methods in embedded code must be written in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] and must be instance-based. The report processor automatically adds references for the System.Convert and System.Math namespaces. Use the **References** page of the **Report Properties** dialog box to add additional assembly references. For more information, see [Add an Assembly Reference to a Report &#40;SSRS&#41;](add-an-assembly-reference-to-a-report-ssrs.md).  
  
 Methods in embedded code are available through a globally defined `Code` member. You access these by referring to the `Code` member and the method name. The following example calls the method `ToUSD`, which converts the value in the `StandardCost` field to a dollar value:  
  
```  
=Code.ToUSD(Fields!StandardCost.Value)  
```  
  
 To reference built-in collections in your custom code, include a reference to the built-in `Report` object:  
  
```  
=Report.Parameters!Param1.Value  
```  
  
 The following examples show how to define some custom constants and variables.  
  
```  
Public Const MyNote = "Authored by Bob"  
Public Const NCopies As Int32 = 2  
Public Dim  MyVersion As String = "123.456"  
Public Dim MyDoubleVersion As Double = 123.456  
```  
  
 Although custom constants do not appear in the **Constants** category in the **Expression** dialog box (which only displays built-in constants), you can add references to them from any expression, as shown in the following examples. In an expression, a custom constant is treated as a `Variant`.  
  
```  
=Code.MyNote  
=Code.NCopies  
=Code.MyVersion  
=Code.MyDoubleVersion  
```  
  
 The following example includes both the code reference and the code implementation of the function `FixSpelling`, which substitutes the text `"Bicycle"` for all occurrences of the text "Bike" in the `SubCategory` field.  
  
 `=Code.FixSpelling(Fields!SubCategory.Value)`  
  
 The following code, when embedded in a report definition code block, shows an implementation of the `FixSpelling` method. This example shows you how to use a fully qualified reference to the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] `StringBuilder` class.  
  
```vb  
Public Function FixSpelling(ByVal s As String) As String  
   Dim strBuilder As New System.Text.StringBuilder(s)  
   If s.Contains("Bike") Then  
      strBuilder.Replace("Bike", "Bicycle")  
      Return strBuilder.ToString()  
      Else : Return s  
   End If  
End Function  
```  
  
 For more information about built-in object collections and initialization, see [Built-in Globals and Users References &#40;Report Builder and SSRS&#41;](built-in-collections-built-in-globals-and-users-references-report-builder.md) and [Initializing Custom Assembly Objects](../custom-assemblies/initializing-custom-assembly-objects.md).  
  
##  <a name="Parameters"></a> Including References to Parameters from Code  
 You can reference the global parameters collection via custom code in a Code block of the report definition or in a custom assembly that you provide. The parameters collection is read-only and has no public iterators. You cannot use a [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] `For Each` construct to step through the collection. You need to know the name of the parameter defined in the report definition before you can reference it in your code. You can, however, iterate through all the values of a multivalue parameter.  
  
 The following table includes examples of referencing the built-in collection `Parameters` from custom code:  
  
|Description|Reference in Expression|Custom Code definition|  
|-----------------|-----------------------------|----------------------------|  
|Passing an entire global parameter collection to custom code.<br /><br /> This function returns the value of a specific report parameter *MyParameter*.|`=Code.DisplayAParameterValue(Parameters)`|`Public Function DisplayAParameterValue(`<br /><br /> `ByVal parameters as Parameters) as Object`<br /><br /> `Return parameters("MyParameter").Value`<br /><br /> `End Function`|  
|Passing an individual parameter to custom code.<br /><br /> This example returns the value of the parameter passed in. If the parameter is a multivalue parameter, the return string is a concatenation of all the values.|`=Code.ShowParametersValues(Parameters!DayOfTheWeek)`|`Public Function ShowParameterValues(ByVal parameter as Parameter)` <br />  `as String` <br /> `Dim s as String`  <br />  `If parameter.IsMultiValue then` <br />  `s = "Multivalue: "`  <br /> `For i as integer = 0 to parameter.Count-1` <br />  `s = s + CStr(parameter.Value(i)) + " "`  <br />  `Next` <br /> `Else` <br /> `s = "Single value: " + CStr(parameter.Value)` <br /> `End If` <br />  `Return s` <br /> `End Function`|  
  
##  <a name="Custom"></a> Including References to Code from Custom Assemblies  
 To use custom assemblies in a report, you must first create the assembly, make it available to Report Designer, add a reference to the assembly in the report, and then use an expression in the report to refer to the methods contained in that assembly. When the report is deployed to the report server, you must also deploy the custom assembly to the report server.  
  
 For information about creating a custom assembly and making it available to [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], see [Using Custom Assemblies with Reports](../custom-assemblies/using-custom-assemblies-with-reports.md).  
  
 To refer to custom code in an expression, you must call the member of a class within the assembly. How you do this depends on whether the method is static or instance-based. Static methods within a custom assembly are available globally within the report. You can access static methods in expressions by specifying the namespace, class, and method name. The following example calls the method `ToGBP`, which converts the value of the **StandardCost** value from dollar to pounds sterling:  
  
```  
=CurrencyConversion.DollarCurrencyConversion.ToGBP(Fields!StandardCost.Value)  
```  
  
 Instance-based methods are available through a globally defined `Code` member. You access these by referring to the `Code` member, followed by the instance and method name. The following example calls the instance method `ToEUR`, which converts the value of **StandardCost** from dollar to euro:  
  
```  
=Code.m_myDollarCoversion.ToEUR(Fields!StandardCost.Value)  
```  
  
> [!NOTE]  
>  In Report Designer, a custom assembly is loaded once and is not unloaded until you close [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)]. If you preview a report, make changes to a custom assembly used in the report, and then preview the report again, the changes will not appear in the second preview. To reload the assembly, close and reopen [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] and then preview the report.  
  
 For more information about accessing your code, see [Accessing Custom Assemblies Through Expressions](../custom-assemblies/accessing-custom-assemblies-through-expressions.md).  
  
##  <a name="collections"></a> Passing Built-in Collections into Custom Assemblies  
 If you want to pass built-in collections, such as the *Globals* or *Parameters* collection, into a custom assembly for processing, you must add an assembly reference in your code project to the assembly that defines the built-in collections and access the correct namespace. Depending on whether you are developing the custom assembly for a report that is run on a report server (server report) or a report that is run locally in a .NET application (local report), the assembly you need to reference is different. See below for details.  
  
-   **Namespace:** Microsoft.ReportingServices.ReportProcessing.ReportObjectModel  
  
-   **Assembly (local report):** Microsoft.ReportingServices.ProcessingObjectModel.dll  
  
-   **Assembly (server report):** Microsoft.ReportViewer.ProcessingObjectModel.dll  
  
 Since the content of the *Fields* and *ReportItems* collections can change dynamically at runtime, you should not hold onto them across calls into the custom assembly (for example, in a member variable). The same recommendation applies generally to all built-in collections.  
  
## See Also  
 [Add Code to a Report &#40;SSRS&#41;](add-code-to-a-report-ssrs.md)   
 [Using Custom Assemblies with Reports](../custom-assemblies/using-custom-assemblies-with-reports.md)   
 [Add an Assembly Reference to a Report &#40;SSRS&#41;](add-an-assembly-reference-to-a-report-ssrs.md)   
 [Reporting Services Tutorials &#40;SSRS&#41;](../reporting-services-tutorials-ssrs.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](expression-examples-report-builder-and-ssrs.md)   
 [Report Samples (Report Builder and SSRS)](https://go.microsoft.com/fwlink/?LinkId=198283)  
  
  
