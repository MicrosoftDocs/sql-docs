---
title: "Referencing Assemblies in an RDL File | Microsoft Docs"
description: Learn to reference assemblies in a Report Definition Language (RDL) file, specifically in the CodeModules element and Classes element.
ms.date: 03/03/2017
ms.prod: reporting-services
ms.technology: custom-assemblies


ms.topic: reference
helpviewer_keywords: 
  - "RDL [Reporting Services], referencing assemblies"
  - "referencing custom assemblies"
  - "custom assemblies [Reporting Services], referencing"
  - "Report Definition Language, referencing assemblies"
  - "report definition files [Reporting Services]"
ms.assetid: 9a48e552-7d47-4243-9be1-894990c506d9
author: maggiesMSFT
ms.author: maggies
---
# Referencing Assemblies in an RDL File
  To support the use of custom code assemblies in report definition files, two Report Definition Language (RDL) elements are included in the RDL specification: the **CodeModules** element and the **Classes** element.  
  
 The **CodeModules** element enables you to refer to managed code assemblies in report expressions. **CodeModules** is a top-level element that contains the reference to the assembly that you use in your report definition files to call specialized functions. An entry in a report definition that supports the use of a custom assembly might look like the following:  
  
```  
<CodeModules>  
   <CodeModule>CurrencyConversion, Version=1.0.1363.31103, Culture=neutral, PublicKeyToken=null</CodeModule>  
</CodeModules>  
```  
  
 Instead of calling <xref:System.Reflection.Assembly.Load%2A> from your custom code, register your custom assemblies by either manually adding **CodeModule** elements to your RDL file or by using the **References** tab of the **Report Properties** dialog. For more information, see [Custom Code and Assembly References in Expressions in Report Designer &#40;SSRS&#41;](../../reporting-services/report-design/custom-code-and-assembly-references-in-expressions-in-report-designer-ssrs.md).  
  
 The **Classes** element supports the use of instance members in a report definition. **Classes** is a top-level element that contains a reference to the class name and an instance name. An entry in a report definition that supports the use of instance members might look like the following:  
  
```  
<Classes>  
   <Class>  
      <ClassName>CurrencyConversion.DollarCurrencyConversion</ClassName>  
      <InstanceName>m_myDollarConversion</InstanceName>  
   </Class>  
</Classes>  
```  
  
 For more information, see [Accessing Custom Assemblies Through Expressions](../../reporting-services/custom-assemblies/accessing-custom-assemblies-through-expressions.md).  
  
## See Also  
 [Using Custom Assemblies with Reports](../../reporting-services/custom-assemblies/using-custom-assemblies-with-reports.md)  
  
  
