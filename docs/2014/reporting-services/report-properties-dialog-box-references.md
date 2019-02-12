---
title: "Report Properties Dialog Box, References | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "10530"
  - "sql12.rtp.rptdesigner.reportproperties.references.f1"
ms.assetid: 4639d368-9918-4bb1-9953-7a724ca78dea
author: maggiesmsft
ms.author: maghan
manager: kfile
---
# Report Properties Dialog Box, References
  Select **References** on the **Report Properties** dialog box to add or remove references to custom or other external assemblies and custom class instances that are used by expressions in the report definition.  
  
## Options  
 **Add or remove assemblies**  
 Lists the assemblies that the report references. The assembly must be available on the computer on which the tool you are using to design the report is installed and on the report server. The name of the reference must match the contents of **\<CodeModule>** tags in the Report Definition Language (.rdl) file exactly.  
  
 **Add**  
 Click to add an assembly. Click the ellipsis (...) button to open the **Open** dialog box and select the assemblies necessary to complete report processing and expression evaluation.  
  
 **Delete**  
 To remove an assembly reference from the list, select the assembly name and click the **Remove** button.  
  
 **Add or remove classes**  
 Lists the class instances that are used by the report. The class list is used only by instance-based members, not static members.  
  
 **Add**  
 Click to add a class reference. Click the ellipsis (...) button to open the **Open** dialog box and select the classes necessary to complete report processing and expression evaluation.  
  
 **Delete**  
 To delete the class instance, select it and click the **Remove** button.  
  
 **Up**  
 For classes that have dependencies, you can move this reference higher in the list.  
  
 **Down**  
 For classes that have dependencies, you can move this reference lower in the list.  
  
## See Also  
 [Custom Code and Assembly References in Expressions in Report Designer &#40;SSRS&#41;](report-design/custom-code-and-assembly-references-in-expressions-in-report-designer-ssrs.md)   
 [Report and Group Variables Collections References &#40;Report Builder and SSRS&#41;](report-design/built-in-collections-report-and-group-variables-references-report-builder.md)   
 [Expression Examples &#40;Report Builder and SSRS&#41;](report-design/expression-examples-report-builder-and-ssrs.md)  
  
  
