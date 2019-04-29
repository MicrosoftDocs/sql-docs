---
title: "Lesson 4: Mark as Date Table | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: c32cc336-b7d8-4122-9d62-4936344d2315
author: minewiskan
ms.author: owend
manager: craigg
---
# Lesson 4: Mark as Date Table
  In Lesson 2: Add Data, you imported a dimension table named DimDate. You then renamed the DimDate table, in Lesson 3: Rename Columns, to simply, Date. While in your model this table is now named Date, it can also be known as a *Date table*, in that it contains date and time data.  
  
 Whenever you use Time Intelligence functions in calculations, as you will do when you create measures a little later, you must specify date table properties, which include a *Date table* and a unique identifier *Date column* in that table. You can then create valid relationships between other tables and the Date table; necessary for calculations using DAX time intelligence functions.  
  
 In this lesson, you will mark the imported and renamed Date table as the *Date table* and the Date column (in the Date table) as the *Date column* (unique identifier). All the use of the name Date can get kind of confusing, but you'll soon get the idea.  
  
 Estimated time to complete this lesson: **3 minutes**  
  
## Prerequisites  
 This topic is part of a tabular modeling tutorial, which should be completed in order. Before performing the tasks in this lesson, you should have completed the previous lesson: [Lesson 3: Rename Columns](rename-columns.md).  
  
### To set Mark as Date Table  
  
1.  In the model designer, click the **Date** table (tab).  
  
2.  Click the **Table** menu, then click **Date**, and then click **Mark as Date Table**.  
  
3.  In the **Mark as Date Table** dialog box, in the **Date** listbox, select the **Date** column as the unique identifier.  
  
## Next Steps  
 To continue this tutorial, go to the next lesson: [Lesson 5: Create Relationships](lesson-4-create-relationships.md).  
  
  
