---
description: "MSSQLSERVER_511"
title: "MSSQLSERVER_511 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "511 (Database Engine error)"
ms.assetid: 0c85686a-53c1-4180-ba8c-2000e68a0d63
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_511
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|511|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|ROW_TOOBIG|  
|Message Text|Cannot create a row of size %d which is greater than the allowable maximum of %d.|  
  
## Explanation  
The operation you have tried has exceeded the maximum size of a row. Usually, the maximum size of a row is 8,060 bytes. Some storage formats contain overhead that can reduce the row size that is available for data. For example, when you use sparse columns, the maximum size of a row is 8,018 bytes. Some operations that add or remove rows and some operations that change the data type of a column require the row to be rewritten on the data page, and then the original row is removed. In these operations, the effective limit to the size of the row is half the maximum limit. This is because the original row and the modified row must both be contained on the data page for a short period.  
  
## User Action  
If it is possible, reduce the size of the row.  
  
If you think the problem is caused by an in-place update of the row, you must change the table in multiple steps. Create a new table, and transfer the data into the new table. Then, either delete the original table and rename the new table, or truncate the original table, modify the rows in the original table, and then move the data back into it.  
  
