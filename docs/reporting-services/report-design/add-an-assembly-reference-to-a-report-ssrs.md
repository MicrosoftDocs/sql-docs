---
title: "Add an Assembly Reference to a Report (SSRS) | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
helpviewer_keywords: 
  - "code [Reporting Services]"
  - "custom assemblies [Reporting Services], referencing"
  - "custom code [Reporting Services]"
  - "adding assembly references"
  - "assemblies [Reporting Services], references"
ms.assetid: 0a03939e-48ce-4c30-b227-98533f2e0ccb
author: markingmyname
ms.author: maghan
---
# Add an Assembly Reference to a Report (SSRS)
  When you embed custom code that contains references to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] classes that are not in <xref:System.Math> or <xref:System.Convert>, you must provide an assembly reference to the report so that the report processor can resolve the names. For more information, see [Add Code to a Report &#40;SSRS&#41;](../../reporting-services/report-design/add-code-to-a-report-ssrs.md).  
  
### To add an assembly reference to a report  
  
1.  In **Design** view, right-click the design surface outside the border of the report and click **Report Properties**.  
  
2.  Click **References**.  
  
3.  In **Add or remove assemblies**, click **Add** and then click the ellipsis button to browse to the assembly.  
  
4.  In **Add or remove classes**, click **Add** and then type name of the class and provide an instance name to use within the report.  
  
    > [!NOTE]  
    >  Specify a class and instance name only for instance-based members. Do not specify static members in the **Classes** list. For more information, see [Custom Code and Assembly References in Expressions in Report Designer &#40;SSRS&#41;](../../reporting-services/report-design/custom-code-and-assembly-references-in-expressions-in-report-designer-ssrs.md).  
  
5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## See Also  
 [Using Custom Assemblies with Reports](../../reporting-services/custom-assemblies/using-custom-assemblies-with-reports.md)   
 [Report Properties Dialog Box, References](https://msdn.microsoft.com/library/4639d368-9918-4bb1-9953-7a724ca78dea)  
  
  
