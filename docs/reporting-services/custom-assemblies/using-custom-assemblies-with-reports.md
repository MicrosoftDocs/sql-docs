---
title: "Using Custom Assemblies with Reports | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: custom-assemblies


ms.topic: reference
helpviewer_keywords: 
  - "custom assemblies [Reporting Services]"
  - "assemblies [Reporting Services], custom"
  - "custom assemblies [Reporting Services], about custom assemblies"
ms.assetid: 53d141d0-2185-466a-84dc-7b90d284da3d
author: maggiesMSFT
ms.author: maggies
---
# Using Custom Assemblies with Reports
  In [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], you can write custom code for report item values, styles, and formatting. For example, you can use custom code to format currencies based on locale, flag certain values with special formatting, or apply other business rules that are in practice for your company. One way to include this code in your reports is to create a custom code assembly using the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] that you can reference from within your report definition files. The server calls the functions in your custom assemblies when a report is run. Custom assemblies can be used to retrieve specialized functions that you plan to use in your reports.  
  
## In This Section  
 [Referencing Assemblies in an RDL File](../../reporting-services/custom-assemblies/referencing-assemblies-in-an-rdl-file.md)  
 Describes how to reference your custom assemblies in a report definition language file.  
  
 [Deploying a Custom Assembly](../../reporting-services/custom-assemblies/deploying-a-custom-assembly.md)  
 Describes how to deploy a custom assembly to Report Designer and the report server.  
  
 [Using Strong-Named Custom Assemblies](../../reporting-services/custom-assemblies/using-strong-named-custom-assemblies.md)  
 Describes how to use custom assemblies with strong names.  
  
 [Asserting Permissions in Custom Assemblies](../../reporting-services/custom-assemblies/asserting-permissions-in-custom-assemblies.md)  
 Describes how to deploy custom assemblies with limited and specific permissions and how to assert those permissions in code.  
  
 [Accessing Custom Assemblies Through Expressions](../../reporting-services/custom-assemblies/accessing-custom-assemblies-through-expressions.md)  
 Describes how to call custom assembly methods as report expressions in your report definitions.  
  
 [Initializing Custom Assembly Objects](../../reporting-services/custom-assemblies/initializing-custom-assembly-objects.md)  
 Describes how to initialize values for custom assembly objects called from a report.  
  
 [How to: Debug Custom Assemblies](../../reporting-services/custom-assemblies/how-to-debug-custom-assemblies.md)  
 Describes how to debug your custom assembly code.  
  
## See Also  
 [Report Definition Language &#40;SSRS&#41;](../../reporting-services/reports/report-definition-language-ssrs.md)  
  
  
