---
title: "Create a Time Dimension by Generating a Time Table | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "time dimensions [Analysis Services]"
  - "dimensions [Analysis Services], time"
  - "time periods [Analysis Services]"
  - "range-based time dimensions [Analysis Services]"
  - "server time dimensions [Analysis Services]"
  - "calendars [Analysis Services]"
  - "table-based time dimensions [Analysis Services]"
ms.assetid: 58303326-1361-4c0e-9f3d-642ce69c4f6a
author: minewiskan
ms.author: owend
manager: craigg
---
# Create a Time Dimension by Generating a Time Table
  In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], you can use the Dimension Wizard in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] to create a time dimension when no time table is available in the source database. You do this by selecting one of the following options on the **Select Creation Method** page:  
  
-   **Generate a time table in the data source** Select this option when you have permission to create objects in the underlying data source. The wizard will then generate a time table and store this table in the data source. The wizard then creates the time dimension from this time table.  
  
-   **Generate a time table on the server** Select this option when you do not have permission to create objects in the underlying data source. The wizard will then generate and store a table on the server instead of in the data source. (The dimension created from a time table on the server is called a *server time dimension*.) The wizard then creates the server time dimension from this table.  
  
 When you create a time dimension, you specify the time periods, and also the start and end dates for the dimension. The wizard uses the specified time periods to create the time attributes. When you process the dimension, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] generates and stores the data that is required to support the specified dates and periods. The wizard uses the attributes created for a time dimension to recommend hierarchies for the dimension. The hierarchies reflect the relationships between different time periods and take account of different calendars. For example, in a standard calendar hierarchy, a Weeks level appears under a Years level but not under a Months level, because weeks divide evenly into years but not into months. In contrast, in a manufacturing or reporting calendar hierarchy, weeks divide evenly months, so a Weeks level appears under a Months level.  
  
## Define Time Periods  
 You use the **Define Time Periods** page of the wizard to specify the range of dates that you want to include in the dimension. For example, you might select a range that starts on January 1 of the earliest year in your data and that ends one or two years beyond the current year (to allow for future transactions). Transactions that are outside the range either do not appear or appear as unknown members in the dimension, depending on the `UnknownMemberVisible` property setting for the dimension. You can also change the first day of the week used by your data (the default is Sunday).  
  
 Select the time periods to use when the wizard creates the hierarchies that apply to your data, such as Years, Half Years, Quarters, Trimesters, Months, Ten Days, Weeks, or Date. You must always select at least the Date time period. The Date attribute is the key attribute for the dimension, so the dimension cannot function without it.  
  
 Next to **Language for time member names**, select the language to be used to label the members of the dimension.  
  
 After you create a time dimension that is based on a range of dates, you can use Dimension Designer to add or remove time attributes. Because the Date attribute is the key attribute for the dimension, you cannot remove it from the dimension. To hide the Date attribute from users, you can change the `AttributeHierarchyVisible` property on the attribute to `False`.  
  
## Select Calendars  
 The standard (Gregorian) 12-month calendar, starting on January 1 and ending on December 31, is always included when you create a time dimension. On the **Select Calendars** page of the wizard, you can specify additional calendars on which to base hierarchies in the dimension. For descriptions of the calendar types, see [Create a Date type Dimension](database-dimensions-create-a-date-type-dimension.md).  
  
 Depending on which time periods you select on the **Define Time Periods** page of the wizard, the calendar selections determine attributes that are created in the dimension. For example, if you select the **Year** and **Quarter** time periods on the **Define Time Periods** page of the wizard and select **Fiscalcalendar** on the **Select Calendars** page, the FiscalYear, FiscalQuarter, and FiscalQuarterOfYear attributes are created for the fiscal calendar.  
  
 The wizard also creates calendar-specific hierarchies that are composed of the attributes that are created for the calendar. For every calendar, each level in every hierarchy rolls up into the level above it. For example, in the standard 12-month calendar, the wizard creates a hierarchy of Years and Weeks or Years and Months. However, weeks are not contained evenly within months in a standard calendar, so there is no hierarchy of Years, Months, and Weeks. In contrast, weeks in a reporting or manufacturing calendar are evenly divided into months, so in these calendars weeks roll up into months.  
  
## Completing the Dimension Wizard  
 On the **Completing the Wizard** page, review the attributes and hierarchies created by the wizard, and then name the time dimension. Click **Finish** to complete the wizard and create the dimension. After you complete the dimension, you can change it by using Dimension Designer.  
  
## See Also  
 [Data Source Views in Multidimensional Models](data-source-views-in-multidimensional-models.md)   
 [Create a Date type Dimension](database-dimensions-create-a-date-type-dimension.md)   
 [Database Dimension Properties](../multidimensional-models-olap-logical-dimension-objects/database-dimension-properties.md)   
 [Dimension Relationships](../multidimensional-models-olap-logical-cube-objects/dimension-relationships.md)   
 [Create a Dimension by Using an Existing Table](create-a-dimension-by-using-an-existing-table.md)   
 [Create a Dimension by Generating a Non-Time Table in the Data Source](create-a-dimension-by-generating-a-non-time-table-in-the-data-source.md)  
  
  
