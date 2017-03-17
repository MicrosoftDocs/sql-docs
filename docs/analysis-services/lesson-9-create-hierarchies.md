---
title: "Lesson 10: Create Hierarchies | Microsoft Docs"
ms.custom: ""
ms.date: "03/10/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "get-started-article"
applies_to: 
  - "SQL Server 2016"
ms.assetid: 1e2561d3-4890-4495-a9cd-84eb88508938
caps.latest.revision: 22
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Lesson 9: Create Hierarchies
In this lesson, you will create hierarchies. Hierarchies are groups of columns arranged in levels; for example, a Geography hierarchy might have sub-levels for Country, State, County, and City. Hierarchies can appear separate from other columns in a reporting client application field list, making them easier for client users to navigate and include in a report. To learn more, see [Hierarchies](../analysis-services/tabular-models/hierarchies-ssas-tabular.md).  
  
To create hierarchies, you'll use the model designer in *Diagram View*. Creating and managing hierarchies is not supported in Data View.  
  
Estimated time to complete this lesson: **20 minutes**  
  
## Prerequisites  
This topic is part of a tabular modeling tutorial, which should be completed in order. Before performing the tasks in this lesson, you should have completed the previous lesson.  
  
## Create hierarchies  
  
#### To create a Category hierarchy in the DimProduct table  
  
1.  In the model designer (diagram view), right-click the **DimProduct** table, and then click **Create Hierarchy**. A new hierarchy appears at the bottom of the table window. Rename the hierarchy **Category**.  
  
2.  Click and drag the **ProductCategoryName** column to the new **Category** hierarchy.  
  
3.  In the **Category** hierarchy, right-click the **ProductCategoryName** column, then click **Rename**, and then type **Category**.  
  
    > [!NOTE]  
    > Renaming a column in a hierarchy does not rename that column in the table. A column in a hierarchy is just a representation of the column in the table.  
  
4.  Click and drag the **ProductSubcategoryName** column to the **Category** hierarchy. Rename it **Subcategory**. 
  
5.  Right-click the **ModelName** column, then click **Add to hierarchy**, and then select **Category**. Do the same for **ProductName**. Rename these columns in the hierarchy **Model** and **Product**.  

    ![as-tabular-lesson9-category](../analysis-services/media/as-tabular-lesson9-category.png)
  
#### To create hierarchies in the DimDate table  
  
1.  In the **DimDate** table, create a new hierarchy named **Calendar**.  
  
3.  Add the following columns, in-order, and then rename them:  
  
    |Column|Rename to:|  
    |----------|--------------|  
    |CalendarYear|Year|  
    |CalendarSemester|Semester|  
    |CalendarQuarter|Quarter|  
    |MonthCalendar|Month|  
    |DayOfMonth|Day|  
  
4.  In the **DimDate** table, create a **Fiscal** hierarchy. Include the following columns:  
  
    |Column|Rename to:|  
    |----------|--------------|  
    |FiscalYear|Year|  
    |FiscalSemester|Semester|  
    |FiscalQuarter|Quarter|  
    |MonthCalendar|Month|  
    |DayOfMonth|Day|  
  
5.  Finally, in the **DimDate** table, create a **ProductionCalendar** hierarchy. Include the following columns:  
  
    |Column|Rename to:|  
    |----------|--------------|  
    |CalendarYear|Year|  
    |WeekNumberOfYear|Week|  
    |DayOfWeek|Day|  
  
  
  
  
