---
title: "Add an assembly reference to a paginated report"
description: Learn how to provide an assembly reference to a report so that the report processor can resolve names in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/14/2017
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "code [Reporting Services]"
  - "custom assemblies [Reporting Services], referencing"
  - "custom code [Reporting Services]"
  - "adding assembly references"
  - "assemblies [Reporting Services], references"
---
# Add an assembly reference to a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  You can embed custom code that contains references to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] classes that aren't in <xref:System.Math> or <xref:System.Convert>. When you embed the code, you must provide an assembly reference to the report so that the report processor can resolve the names. For more information, see [Add code to a report](../../reporting-services/report-design/add-code-to-a-report-ssrs.md).  
  
### Add an assembly reference to a report  
  
1.  In **Design** view, right-click the design surface outside the border of the report and select **Report Properties**.  
  
1.  Select **References**.  
  
1.  In **Add or remove assemblies**, select **Add**, and then choose the ellipsis button to browse to the assembly.  
  
1.  In **Add or remove classes**, select **Add**, and then enter name of the class and provide an instance name to use within the report.  
  
    > [!NOTE]  
    >  Specify a class and instance name only for instance-based members. Don't specify static members in the **Classes** list. For more information, see [Custom code and assembly references in expressions in Report Designer](../../reporting-services/report-design/custom-code-and-assembly-references-in-expressions-in-report-designer-ssrs.md).  
  
1.  Select **OK**.
  
## Related content 
 [Use custom assemblies with reports](../../reporting-services/custom-assemblies/using-custom-assemblies-with-reports.md)   
 [Report properties dialog, references](./custom-code-and-assembly-references-in-expressions-in-report-designer-ssrs.md)  
  
