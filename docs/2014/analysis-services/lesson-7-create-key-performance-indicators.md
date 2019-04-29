---
title: "Lesson 8: Create Key Performance Indicators | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: a6c8ac2b-64ba-456f-b418-7bf0afe145d1
author: minewiskan
ms.author: owend
manager: craigg
---
# Lesson 8: Create Key Performance Indicators
  In this lesson, you will create Key Performance Indicators (KPIs). KPIs are used to gauge performance of a value, defined by a *Base* measure, against a *Target* value, also defined by a measure or by an absolute value. In reporting client applications, KPIs can provide business professionals a quick and easy way to understand a summary of business success or to identify trends. To learn more, see [KPIs &#40;SSAS Tabular&#41;](tabular-models/kpis-ssas-tabular.md).  
  
 Estimated time to complete this lesson: **15 minutes**  
  
## Prerequisites  
 This topic is part of a tabular modeling tutorial, which should be completed in order. Before performing the tasks in this lesson, you should have completed the previous lesson: [Lesson 7: Create Measures](lesson-6-create-measures.md).  
  
## Create Key Performance Indicators  
  
#### To create an Internet Current Quarter Sales Performance KPI  
  
1.  In the model designer, click the **Internet Sales** table (tab).  
  
2.  In the measure grid, click an empty cell.  
  
3.  In the formula bar, above the table, type the following formula:  
  
     `Internet Current Quarter Sales Performance :=IFERROR([Internet Current Quarter Sales]/[Internet Previous Quarter Sales Proportion to QTD],BLANK())`  
  
     When you have finished building the formula, press ENTER.  
  
     This measure will serve as the Base measure for the KPI.  
  
4.  In the measure grid, right-click the **Internet Current Quarter Sales Performance** measure, and then click **Create KPI**.  
  
     The **Key Performance Indicator** dialog box opens.  
  
5.  In the **Key Performance Indicator** dialog box, in **Define Target Value**, select the **Absolute Value** option.  
  
6.  In the **Absolute Value** field, type `1.1`, and then press ENTER.  
  
7.  In **Define Status Thresholds**, in the left (low) slider field, type `1`, and then in the right (high) slider field, type `1.07`.  
  
8.  In **Select Icon Style**, select the diamond (red), triangle (yellow), circle (green) icon type.  
  
    > [!TIP]  
    >  Notice the **Descriptions** expandable field below the available icon styles. You can type descriptions for the various KPI elements to make them more identifiable in client applications.  
  
9. Click **OK** to complete the KPI.  
  
     In the measure grid, notice the icon next to the **Internet Current Quarter Sales Performance** measure. This icon indicates that this measure serves as a Base value for a KPI.  
  
#### To create an Internet Current Quarter Margin Performance KPI  
  
1.  In the measure grid for the **Internet Sales** table, click an empty cell.  
  
2.  In the formula bar, above the table, type the following formula:  
  
     `Internet Current Quarter Margin Performance :=IF([Internet Previous Quarter Margin Proportion to QTD]<>0,([Internet Current Quarter Margin]-[Internet Previous Quarter Margin Proportion to QTD])/[Internet Previous Quarter Margin Proportion to QTD],BLANK())`  
  
     When you have finished building the formula, press ENTER.  
  
3.  In the measure grid, right-click the **Internet Current Quarter Margin Performance** measure, and then click **Create KPI**.  
  
4.  In the **Key Performance Indicator** dialog box, in **Define Target Value**, select the **Absolute Value** option.  
  
5.  In the **Absolute Value** field, type `1.25`.  
  
6.  In **Define Status Thresholds**, slide the left (low) slider field until the field displays **0.8**, and then slide the right (high) slider field, until the field displays **1.03**.  
  
7.  In **Select Icon Style**, select the diamond (red), triangle (yellow), circle (green) icon type, and then click **OK**.  
  
## Next Step  
 To continue this tutorial, go to the next lesson: [Lesson 9: Create Perspectives](lesson-8-create-perspectives.md).  
  
  
