---
title: "Select Calendars (Dimension Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.dimensionwizard.serverSpecialCalendars.f1"
ms.assetid: 6e28a020-2586-4b13-9333-b499fb1b33af
author: minewiskan
ms.author: owend
manager: craigg
---
# Select Calendars (Dimension Wizard)
  Use the **Select Calendars** page to create additional hierarchies representing fiscal, reporting, manufacturing, or International Standards Organization (ISO) 8601 calendars for the time dimension.  
  
> [!NOTE]  
>  This page will appear only if you have selected **Server time dimension** on the **Select the Dimension Type** page, or if you selected **Build the dimension without using a data source** on the **Dimension Definition** page and selected **Time dimension** on the **Select the Dimension Type** page.  
  
## Options  
 **Fiscal calendar**  
 Select to create a time hierarchy based on a fiscal calendar.  
  
 **Start day and month**  
 Select the day and month on which the fiscal calendar begins.  
  
> [!NOTE]  
>  This option is available only when **Fiscal calendar** is selected.  
  
 **Fiscal calendar naming convention**  
 Select the naming convention used by the fiscal calendar. Select either **Calendar year name** or **Calendar year name +1**.  
  
> [!NOTE]  
>  This option is available only when **Fiscal calendar** is selected.  
  
 **Reporting (or marketing) calendar**  
 Select to create a time hierarchy based on a reporting calendar.  
  
 **Start week and month**  
 Select the week and month on which the reporting calendar begins.  
  
> [!NOTE]  
>  This option is available when **Reporting (or marketing) calendar** is selected.  
  
 **Week by month pattern**  
 Select the week by month pattern used by the reporting calendar.  
  
> [!NOTE]  
>  This option is available when **Reporting (or marketing) calendar** is selected.  
  
 The following table lists the options available for the week by month pattern.  
  
|Value|Description|  
|-----------|-----------------|  
|**Week 445**|The first month in the quarter has 4 weeks, the second month in the quarter has 4 weeks, and the third month in the quarter has 5 weeks.|  
|**Week 454**|The first month in the quarter has 4 weeks, the second month in the quarter has 5 weeks, and the third month in the quarter has 4 weeks.|  
|**Week 544**|The first month in the quarter has 5 weeks, the second month in the quarter has 4 weeks, and the third month in the quarter has 4 weeks.|  
  
 **Manufacturing calendar**  
 Select to create a time hierarchy based on a manufacturing calendar.  
  
 **Start week and month**  
 Select the week and month on which the manufacturing calendar begins.  
  
> [!NOTE]  
>  This option is available when **Manufacturing calendar** is selected.  
  
 **Quarter with extra periods**  
 Select or type the quarter that will contain the extra periods.  
  
> [!NOTE]  
>  This option is available when **Manufacturing calendar** is selected.  
  
 **ISO 8601 calendar**  
 Select to create a hierarchy based on the ISO 8601 calendar.  
  
## See Also  
 [Dimension Wizard F1 Help](dimension-wizard-f1-help.md)   
 [Dimensions &#40;Analysis Services - Multidimensional Data&#41;](multidimensional-models-olap-logical-dimension-objects/dimensions-analysis-services-multidimensional-data.md)   
 [Dimensions in Multidimensional Models](multidimensional-models/dimensions-in-multidimensional-models.md)  
  
  
