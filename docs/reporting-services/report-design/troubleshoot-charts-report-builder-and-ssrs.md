---
title: "Troubleshoot charts in a paginated report | Microsoft Docs"

description: Use fields with numeric data types along the value axis, rather than formatted numbers, to display a numeric value in a paginated report.
ms.date: 01/17/2018
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: 3a327ffa-3b69-40d6-8015-cc01cfae9161
author: maggiesMSFT
ms.author: maggies
---
# Troubleshoot charts in a paginated report (Report Builder)


[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  These issues can be helpful when working with charts in a paginated report.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Why does my chart count, not sum, the values on the value axis?  
 Most chart types require numeric values along the value axis, which is typically the y-axis, in order to draw correctly. If the data type of your value field is **String**, the chart cannot display a numeric value, even if there are numerals in the fields. Instead, the chart displays a count of the total number of rows that contain a value in that field. To avoid this behavior, make sure that the fields that you use for value series have numeric data types, as opposed to strings that contain formatted numbers.  

## Need more help?  
   
  Try:  
 * [SQL Server Reporting Services](https://stackoverflow.com/questions/tagged/reporting-services) on Stack Overflow  
 * Share [ideas for SQL](https://feedback.azure.com/forums/908035-sql-server).  
  
## See Also  
 [Charts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)  
  
  
