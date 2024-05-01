---
title: "Filter Settings"
description: "Filter Settings"
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: replication
ms.topic: ui-reference
ms.custom: updatefrequency5
f1_keywords:
  - "sql13.rep.monitor.filtersettings.f1"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Filter Settings
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  The **Filter Settings** dialog box lets you define filters for grids in Replication Monitor. For example, to show only those subscriptions that are active on the **All Subscriptions** tab, select **Status** from the **Column Name** column, **Equals** from the **Operator** column, and **Active** from the **Value1** column. After you define a filter that is based one or more columns, the filter is applied so that the grid displays only the subset of rows that match the filter criteria.  
  
## Options  
 **Column Name**  
 Select the name of the column on which you want to filter. You can base a filter on one or more columns.  
  
 **Operator**  
 Select an operator for the filter, such as **Less than or Equal to**.  
  
 **Value1** and **Value2**  
 Enter or select a value for the filter. Most operators only require a value in the **Value1** column, but the **Between** and **Not Between** operators also require a value in the **Value2** column.  
  
 **Clear Filter**  
 Click this button to clear all filters that have been defined. To remove a single filter, select the filter row and press the Delete key.  
  
## See Also  
 [Monitoring Replication](../../relational-databases/replication/monitor/monitoring-replication.md)  
  
  
