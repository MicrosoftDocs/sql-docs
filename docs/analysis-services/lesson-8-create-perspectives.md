---
title: "Lesson 8 Create Perspectives | Microsoft Docs"
ms.date: 08/22/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: tutorial
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Lesson 8: Create Perspectives
[!INCLUDE[ssas-appliesto-sql2016-later-aas](../includes/ssas-appliesto-sql2016-later-aas.md)]

In this lesson, you will create an Internet Sales perspective. A perspective defines a viewable subset of a model that provides focused, business-specific, or application-specific viewpoints. When a user connects to a model by using a perspective, they see only those model objects (tables, columns, measures, hierarchies, and KPIs) as fields defined in that perspective.  
  
The Internet Sales perspective you create in this lesson will exclude the DimCustomer table object. When you create a perspective that excludes certain objects from view, that object still exists in the model; however, it is not visible in a reporting client field list. Calculated columns and measures either included in a perspective or not can still calculate from object data that is excluded.  
  
The purpose of this lesson is to describe how to create perspectives and become familiar with the tabular model authoring tools. If you later expand this model to include additional tables, you can create additional perspectives to define different viewpoints of the model, for example, Inventory and Sales. To learn more, see [Perspectives](../analysis-services/tabular-models/perspectives-ssas-tabular.md).  
  
Estimated time to complete this lesson: **5 minutes**  
  
## Prerequisites  
This topic is part of a tabular modeling tutorial, which should be completed in order. Before performing the tasks in this lesson, you should have completed the previous lesson: [Lesson 7: Create Key Performance Indicators](../analysis-services/lesson-7-create-key-performance-indicators.md).  
  
## Create perspectives  
  
#### To create an Internet Sales perspective  
  
1.  Click the **Model** menu > **Perspectives** > **Create and Manage**.  
  
2.  In the **Perspectives** dialog box, click **New Perspective**.  
  
3.  Double-click the **New Perspective** column heading, and then rename **Internet Sales**.  
  
4.  Select the all of the tables *except* **DimCustomer**.  
  
    ![as-tabular-lesson8-perspectives](../analysis-services/media/as-tabular-lesson8-perspectives.png)
  
    In a later lesson, you will use the Analyze in Excel feature to test this perspective. The Excel PivotTable Fields List will include each table except the DimCustomer table.  

## What's next?
Go to the next lesson: [Lesson 9: Create Hierarchies](../analysis-services/lesson-9-create-hierarchies.md).
  
  
  
  
