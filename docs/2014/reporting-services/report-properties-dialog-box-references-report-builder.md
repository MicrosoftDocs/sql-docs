---
title: "Report Properties Dialog Box, References (Report Builder) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "10082"
ms.assetid: 3414c857-8ea6-4fc4-a6d5-b4883c039efa
author: maggiesmsft
ms.author: douglasl
manager: craigg
---
# Report Properties Dialog Box, References (Report Builder)
  Select **References** on the **Report Properties** dialog box to add or remove references to custom or other external assemblies and custom class instances that are used by expressions in the report definition. Custom assemblies are not supported in local mode in Report Builder. To author reports that use custom assemblies, use Report Designer in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)].  
  
## Options  
 **Add or remove assemblies**  
 Lists the assemblies that the report references. The assembly must be available on the computer on which the tool you are using to design the report is installed and on the report server. The name of the reference must match the contents of **\<CodeModule>** tags in the Report Definition Language (.rdl) file exactly.  
  
 **Add**  
 Click to add an assembly. Click the ellipsis (...) button to open the **Open** dialog box and select the assemblies necessary to complete report processing and expression evaluation.  
  
 **Remove**  
 To remove an assembly reference from the list, select the assembly name and click the **Remove** button.  
  
 **Add or remove classes**  
 Lists the class instances that are used by the report. The class list is used only by instance-based members, not static members.  
  
 **Add**  
 Click to add a class reference. Click the ellipsis (...) button to open the **Open** dialog box and select the classes necessary to complete report processing and expression evaluation.  
  
 **Remove**  
 To delete the class instance, select it and click the **Remove** button.  
  
 **Up**  
 For classes that have dependencies, you can move this reference higher in the list.  
  
 **Down**  
 For classes that have dependencies, you can move this reference lower in the list.  
  
## See Also  
 [Report Builder Help for Dialog Boxes, Panes, and Wizards](../../2014/reporting-services/report-builder-help-for-dialog-boxes-panes-and-wizards.md)   
 [Expressions &#40;Report Builder and SSRS&#41;](report-design/expressions-report-builder-and-ssrs.md)  
  
  
