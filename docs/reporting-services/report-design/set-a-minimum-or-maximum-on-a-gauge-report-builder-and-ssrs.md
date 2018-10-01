---
title: "Set a Minimum or Maximum on a Gauge (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: b4c260c0-5a88-4f30-8977-eb5cc78fc146
author: maggiesMSFT
ms.author: maggies
---
# Set a Minimum or Maximum on a Gauge (Report Builder and SSRS)
  Unlike charts in a [!INCLUDE[ssRSnoversion_md](../../includes/ssrsnoversion-md.md)] paginated report, where multiple groups are defined, gauges show only one value. Because Report Builder and Report Designer determine the context or relative significance of the one value that you are trying to show on the gauge, you must define the minimum and maximum of the scale.   
    
  For example, if your data values are rankings between 0 and 10, you will want to set the minimum to 0 and maximum to 10. The interval numbers are calculated automatically based on the values specified for the minimum and maximum. By default, the minimum and maximum are set to 0 and 100, but this is an arbitrary value that you should change. The application does not calculate your value as a percentage.  
  
 If the range of your values is large, for example from 0 to 10000, consider using a multiplier to reduce the number of zeroes on the gauge. The multiplier will only reduce the scale of the numbers on the gauge, not the value itself.  
  
 You can use expressions to set the values of the **Minimum** and **Maximum** options. For more information, see [Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md).  
  
## To set the minimum and maximum on the gauge  
  
1.  Right-click on the scale and select **Scale Properties**. The **Scale Properties** dialog box appears.  
  
2.  In **General**, specify a value for **Minimum**. By default, this value is 0. Optionally, click the **Expression** (*fx*) button to edit the expression that sets the value of the option.  
  
3.  Specify a value for **Maximum**. By default, this value is 100. Optionally, click the **Expression** (*fx*) button to edit the expression that sets the value of the option.  
  
4.  (Optional) If the values for your minimum and maximum are large, specify a value for the **Multiply scale labels by** option. To specify a multiplier that reduces your scale, use a decimal number. For example, if you have a scale from 0 to 1000, you can specify a multiplier value of 0.01 to reduce the scale to read 0 to 10.  
  
## See Also  
 [Formatting Scales on a Gauge &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-scales-on-a-gauge-report-builder-and-ssrs.md)   
 [Formatting Pointers on a Gauge &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-pointers-on-a-gauge-report-builder-and-ssrs.md)   
 [Gauges &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/gauges-report-builder-and-ssrs.md)  
  
  
