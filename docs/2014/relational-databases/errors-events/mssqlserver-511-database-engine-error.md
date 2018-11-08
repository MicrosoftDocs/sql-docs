---
title: "MSSQLSERVER_511 | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2016"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "511 (Database Engine error)"
ms.assetid: 0c85686a-53c1-4180-ba8c-2000e68a0d63
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_511
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|511|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|ROW_TOOBIG|  
|Message Text|Cannot create a row of size %d which is greater than the allowable maximum of %d.|  
  
## Explanation  
 The operation you have tried has exceeded the maximum size of a row. Usually, the maximum size of a row is 8,060 bytes. Some storage formats contain overhead that can reduce the row size that is available for data. For example, when you use sparse columns, the maximum size of a row is 8,018 bytes. Some operations that add or remove rows and some operations that change the data type of a column require the row to be rewritten on the data page, and then the original row is removed. In these operations, the effective limit to the size of the row is half the maximum limit. This is because the original row and the modified row must both be contained on the data page for a short period.  
  
> [!WARNING]  
>  Each non-null  **varchar(max)** or **nvarchar(max)** column requires 24 bytes of additional fixed allocation which counts against the 8,060 byte row limit during a sort operation. This can create an implicit limit to the number of non-null **varchar(max)** or **nvarchar(max)** columns that can be created in a table. No special error is provided when the table is created (beyond the usual warning that the maximum row size exceeds the allowed maximum of 8060 bytes) or at the time of data insertion. This large row size can cause errors (such as error 512) during some normal operations, such as a clustered index key update, or sorts of the full column set, which users cannot anticipate until performing an operation.  
  
## User Action  
 If it is possible, reduce the size of the row.  
  
 If you think the problem is caused by an in-place update of the row, you must change the table in multiple steps. Create a new table, and transfer the data into the new table. Then, either delete the original table and rename the new table, or truncate the original table, modify the rows in the original table, and then move the data back into it.  
  
  
