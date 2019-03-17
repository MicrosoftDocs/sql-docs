---
title: "Sort Columns | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.monitor.sortcolumns.f1"
ms.assetid: 66b44b6c-10a5-4e3f-a97b-7568609c88ac
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Sort Columns
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The **Sort Columns** dialog box lets you sort grids in Replication Monitor based on one or more columns. (You can also sort on a single column by clicking the column header in the Replication Monitor grid). For example, to sort subscriptions on the **All Subscriptions** tab based on status and then connection type, follow these steps:  
  
1.  In the first row of the grid, select **Status** from the **Column Name** column and a value from the **Sort Order** column  
  
2.  In the second row of the grid, select **Connection Type** from the **Column Name** column, and a value from the **Sort Order** column.  
  
## Options  
 **Column Name**  
 The name of the column on which you want to sort. You can sort on one or more columns. You cannot sort on the **Current Average Performance** or **Current Worst Performance** columns on the **Publications** tab, because of the way in which these column values are calculated.  
  
 **Sort Order**  
 Specify a value of **Ascending** or **Descending**.  
  
 **Clear All**  
 Remove all rows from the sorting grid. To remove a single row, select the row and press the Delete key.  
  
## See Also  
 [Monitoring Replication](../../relational-databases/replication/monitor/monitoring-replication.md)  
  
  
