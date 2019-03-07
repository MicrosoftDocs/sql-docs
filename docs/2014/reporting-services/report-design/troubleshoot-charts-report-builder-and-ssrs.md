---
title: "Troubleshoot Charts (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 3a327ffa-3b69-40d6-8015-cc01cfae9161
author: markingmyname
ms.author: maghan
manager: kfile
---
# Troubleshoot Charts (Report Builder and SSRS)
  These issues can be helpful when working with charts.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Why does my chart count, not sum, the values on the value axis?  
 Most chart types require numeric values along the value axis, which is typically the y-axis, in order to draw correctly. If the data type of your value field is `String`, the chart cannot display a numeric value, even if there are numerals in the fields. Instead, the chart displays a count of the total number of rows that contain a value in that field. To avoid this behavior, make sure that the fields that you use for value series have numeric data types, as opposed to strings that contain formatted numbers.  
  
## See Also  
 [Charts &#40;Report Builder and SSRS&#41;](charts-report-builder-and-ssrs.md)  
  
  
