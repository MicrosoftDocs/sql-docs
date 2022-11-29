---
title: "Set and configure measurement units in a paginated report | Microsoft Docs"
description: Learn how to set and configure measurement units in a paginated report using expressions in Report Builder.
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: a15a96c3-7d2c-433e-a440-4ea051e967a9
author: maggiesMSFT
ms.author: maggies
---
# Set and configure measurement units in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  In a paginated report, indicators use one of two measurement units: percentage or numeric.   
    
  By default, indicators are configured to use percentages as the measurement unit. This means that the indicator values assigned to each icon in the indicator set are determined by a percentage range. The percentage ranges are evenly divided across the icons in the indicator set. Each icon represents an indicator state. You can change the percentages for each icon in the indicator set by specifying different start and end percentages. Indicators also automatically detect the minimum and maximum values in the data.  
  
 You can change the measurement unit to be a numeric value. In this case, you do not specify minimum or maximum for the data; instead, you provide only the start and end values for each icon that the indicator uses.  
  
 Options such as measurement units can be set by using expressions. For more information, see [Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md).  
  
## To use the numeric state measurement unit  
  
1.  Right-click the indicator you want to change and click **Indicator Properties**.  
  
2.  Click **Values and States** in the left pane.  
  
3.  In the **States Measurement Unit** list, click **Numeric**.  
  
     Optionally, click the **Expression** (*fx*) button to edit an expression that sets the value of the option.  
  
4.  For each icon in the indicator set, update the values in the **Start** and **End** text boxes.  
  
     Optionally, click the **Expression** (*fx*) button to edit an expression that sets the values of the **Start** and **End** options.  
  
    > [!NOTE]  
    >  The values in the **Start** and **End** text boxes must be numeric.  
  
5.  Select **OK**.
  
## To use the percentage measurement unit  
  
1.  Right-click the indicator you want to change and click **Indicator Properties**.  
  
2.  Click **Values and States** in the left pane.  
  
3.  In the **States Measurement Unit** list, click **Percentage**.  
  
     Optionally, click the **Expression** (*fx*) button to edit an expression that sets the value of the option.  
  
4.  Optionally, change the **Minimum** and **Maximum** options to use specific values instead of automatically detecting the minimum and maximum values of the data that the indicator uses. The value of **Minimum** must be smaller than the value of **Maximum**.  
  
    > [!NOTE]  
    >  If you explicitly set minimum and maximum values, that value range is used by the indicator regardless of the actual the minimum and maximum values in the data. This means that values lower than the minimum and higher than the maximum are excluded from the evaluation that determine what indictor icon to show in the report.  
  
     Optionally, click the **Expression** (*fx*) button to edit an expression that sets the values of the option.  
  
5.  For each icon in the indicator set, update the values in the **Start** and **End** text boxes.  
  
     Optionally, click the **Expression** (*fx*) button to edit an expression that sets the values of the **Start** and **End** options.  
  
    > [!NOTE]  
    >  The values in the **Start** and **End** text boxes must be numeric.  
  
6.  Select **OK**.
  
## See Also  
 [Indicators &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/indicators-report-builder-and-ssrs.md)  
  
  
