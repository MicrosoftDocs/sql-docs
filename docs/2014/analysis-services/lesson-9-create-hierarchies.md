---
title: "Lesson 10: Create Hierarchies | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 1e2561d3-4890-4495-a9cd-84eb88508938
author: minewiskan
ms.author: owend
manager: craigg
---
# Lesson 10: Create Hierarchies
  In this lesson, you will create hierarchies. Hierarchies are groups of columns arranged in levels; for example, a Geography hierarchy might have sub-levels for Country, State, County, and City. Hierarchies can appear separate from other columns in a reporting client application field list, making them easier for client users to navigate and include in a report. To learn more, see [Hierarchies &#40;SSAS Tabular&#41;](tabular-models/hierarchies-ssas-tabular.md).  
  
 To create hierarchies, you will use the model designer in *Diagram View*. Creating and managing hierarchies is not supported in the model designer in Data View.  
  
 Estimated time to complete this lesson: **20 minutes**  
  
## Prerequisites  
 This topic is part of a tabular modeling tutorial, which should be completed in order. Before performing the tasks in this lesson, you should have completed the previous lesson: [Lesson 9: Create Perspectives](lesson-8-create-perspectives.md).  
  
## Create Hierarchies  
  
#### To create a Category hierarchy in the Product table  
  
1.  In the model designer, click on the `Model` menu, then point to **Model View**, and then click **Diagram View**.  
  
    > [!TIP]  
    >  Use the Minimap controls at the top-right of the model designer to change how you can view objects in Diagram View. If you reposition objects in Diagram View, that view will be retained when you save the project.  
  
2.  In the model designer, right-click the `Product` table, and then click **Create Hierarchy**. A new hierarchy appears at the bottom of the table window.  
  
3.  In the hierarchy name, rename the hierarchy by typing `Category`, and then press ENTER.  
  
4.  In the `Product` table, click the **Product Category Name** column, then drag it to the `Category` hierarchy, and then release on top of the `Category` name.  
  
5.  In the `Category` hierarchy, right-click the **Product Category Name** column, then click **Rename**, and then type `Category`.  
  
    > [!NOTE]  
    >  Renaming a column in a hierarchy does not rename that column in the table. A column in a hierarchy is just a representation of the column in the table.  
  
6.  In the `Product` table, right-click the **Product Subcategory Name** column, then in the context menu, point to **Add to Hierarchy**, and then click `Category`.  
  
7.  Rename **Product Subcategory Name** to `Subcategory`.  
  
8.  By using click and drag, or by using the **Add to Hierarchy** command in the context menu, add the **Model Name** and **Product Name** columns (in order) and place them beneath the **Product Subcategory Name** column. Rename these columns `Model` and `Product`, respectively.  
  
#### To create hierarchies in the Date table  
  
1.  In the model designer, right-click the **Date** table, and then click **Create Hierarchy**.  
  
2.  Rename the hierarchy to **Calendar**.  
  
3.  Add the following columns, in-order, and then rename them:  
  
    |Column|Rename to:|  
    |------------|----------------|  
    |Calendar Year|Year|  
    |Calendar Semester|Semester|  
    |Calendar Quarter|Quarter|  
    |Month Calendar|Month|  
    |Day Of Month|Day|  
  
4.  In the **Date** table, repeat the above steps, creating a **Fiscal** hierarchy, including the following columns:  
  
    |Column|Rename to:|  
    |------------|----------------|  
    |Fiscal Year|Year|  
    |Fiscal Semester|Semester|  
    |Fiscal Quarter|Quarter|  
    |Month Calendar|Month|  
    |Day Of Month|Day|  
  
5.  Finally, in the **Date** table, repeat the above steps, creating a **Production Calendar** hierarchy, including the following columns:  
  
    |Column|Rename to:|  
    |------------|----------------|  
    |Calendar Year|Year|  
    |Week Number Of Year|Week|  
    |Day Of Week|Day|  
  
## Next Steps  
 To continue this tutorial, go to the next lesson: [Lesson 11: Create Partitions](lesson-10-create-partitions.md).  
  
  
