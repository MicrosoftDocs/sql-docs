---
title: "Set a Snapping Interval on a Gauge (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 0ece7297-6e2f-47fb-835d-b9e9cce53fe2
author: maggiesmsft
ms.author: douglasl
manager: craigg
---
# Set a Snapping Interval on a Gauge (Report Builder and SSRS)
  A snapping interval defines the multiple to which values are rounded. By default, the gauge will point to the exact value of the field you have specified in the data pane. However, you may want to round the exact value up or down so that the pointer will snap to a preset interval. For example, if the value on your gauge is 34.2 and you specify a snapping interval of 5, the gauge pointer will point to 35. If the value on your gauge is 31.2 and you specify a snapping interval of 5, the gauge pointer will point to 30.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../includes/ssrbrddup-md.md)]  
  
### To set a snapping interval on a gauge  
  
1.  Click anywhere on the numbers of the gauge to highlight the scale.  
  
2.  Open the Properties pane.  
  
    > [!NOTE]  
    >  If you do not see the Properties pane, click the **View** tab and then select the **Properties** checkbox.  
  
3.  In the **Pointers** property, click the (...) button. The Pointer Collection Editor opens.  
  
4.  Set the **SnappingEnabled** property to `True`.  
  
5.  Set the **SnappingInterval** to a value that represents the snapping interval. The pointer will be snapped to the nearest round multiple of the value that you have specified.  
  
## See Also  
 [Formatting Scales on a Gauge &#40;Report Builder and SSRS&#41;](report-design/formatting-scales-on-a-gauge-report-builder-and-ssrs.md)   
 [Formatting Pointers on a Gauge &#40;Report Builder and SSRS&#41;](report-design/formatting-pointers-on-a-gauge-report-builder-and-ssrs.md)   
 [Gauges &#40;Report Builder and SSRS&#41;](report-design/gauges-report-builder-and-ssrs.md)  
  
  
