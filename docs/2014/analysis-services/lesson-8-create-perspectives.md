---
title: "Lesson 9: Create Perspectives | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 55b0f0d0-1cdf-4876-9c3d-d0c848be3f5d
author: minewiskan
ms.author: owend
manager: craigg
---
# Lesson 9: Create Perspectives
  In this lesson, you will create an Internet Sales perspective. A perspective defines a viewable subset of a model that provides focused, business-specific, or application-specific viewpoints. When a user connects to a model using a perspective, they see only those model objects (tables, columns, measures, hierarchies, and KPIs) as fields defined in that perspective.  
  
 The Internet Sales perspective you create in this lesson will exclude the Customer table object. When you create a perspective that excludes certain objects from view, that object still exists in the model; however, it is not visible in a reporting client field list. Calculated columns and measures either included in a perspective or not can still calculate from object data that is excluded.  
  
 The purpose of this lesson is to describe how to create perspectives and become familiar with the tabular model authoring tools. If you later expand this model to include additional tables, you can create additional perspectives to define different viewpoints of the model, for example, Inventory and Sales Force.  
  
 To learn more, see [Perspectives &#40;SSAS Tabular&#41;](tabular-models/perspectives-ssas-tabular.md).  
  
 Estimated time to complete this lesson: **5 minutes**  
  
## Prerequisites  
 This topic is part of a tabular modeling tutorial, which should be completed in order. Before performing the tasks in this lesson, you should have completed the previous lesson: [Lesson 8: Create Key Performance Indicators](lesson-7-create-key-performance-indicators.md).  
  
## Create Perspectives  
  
#### To create an Internet Sales perspective  
  
1.  In the model designer, click the **Model** menu, and then click **Perspectives**.  
  
2.  In the **Perspectives** dialog box, click **New Perspective**.  
  
3.  To rename the perspective, double-click the **New Perspective 1** column heading, and then type `Internet Sales`.  
  
4.  In **Fields**, select the following tables **Date**, **Geography**, **Product**, **Product Category**, **Product Subcategory**, and `Internet Sales`.  
  
     Notice you excluded the Customer table and all of its columns from this perspective. Later, in Lesson 12, you will use the Analyze in Excel feature to test this perspective. The Excel PivotTable Field List will include each table except the Customer table.  
  
5.  Verify your selections, making sure the **Customer** table is not checked, and then click **OK**  
  
## Next Steps  
 To continue this tutorial, go to the next lesson: [Lesson 10: Create Hierarchies](lesson-9-create-hierarchies.md).  
  
  
