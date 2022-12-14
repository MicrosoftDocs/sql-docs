---
title: View Data Differences
description: Find out how to compare two databases and then see how their database objects differ. See how to view records within objects and how to filter the view.
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
f1_keywords: 
  - "sql.data.tools.datacompare.f1"
ms.assetid: f88d3350-2eaf-44cc-96a8-84008b6cd071
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 02/09/2017
---

# How to: View Data Differences

After you compare the data in two databases, you will see each *database object* that you compared and its status. You can also view results for the records within each object, grouped by status.  
  
After you view the differences, you can update the *target* to match the *source* for some or all of the objects or records that are different, missing, or new.  
  
### To view data differences  
  
1.  Compare the data in a source and a target.  
  
2.  (Optional) Do one or both of the following:  
  
    -   By default, the results for all objects appear, regardless of their status. To display only those objects that have a particular status, click an option in the **Filter** list.  
  
    -   To view results for records within a particular object, click the object in the main results pane, and then click a tab in the Records View pane. Each tab displays all records within that object that have a particular status: different, only in source, only in target, and identical. Data appears by record and column.  
  
## See Also  
[How to: Use Schema Compare to Compare Different Database Definitions](../ssdt/how-to-use-schema-compare-to-compare-different-database-definitions.md)  
  
