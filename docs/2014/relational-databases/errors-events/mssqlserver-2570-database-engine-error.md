---
title: "MSSQLSERVER_2570 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "2570 (Database Engine error)"
ms.assetid: 29800aa9-81aa-4371-992c-487dbb617f46
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_2570
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|2570|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC_COLUMN_VALUE_OUT_OF_RANGE|  
|Message Text|Page P_ID, slot S_ID in object ID O_ID, index ID I_ID, partition ID PN_ID, alloc unit ID A_ID (type TYPE). Column COLUMN_NAME value is out of range for data type "DATATYPE". Update column to a legal value.|  
  
## Explanation  
 The column value that is contained in the specified column is outside the range of possible values for the column data type.  
  
## User Action  
 The error is not repairable. Update the column to a value within the range for the data type of the column and run the command again.  For more information, see this KB article [923247](https://support.microsoft.com/kb/923247).  
  
## See Also  
 [UPDATE &#40;Transact-SQL&#41;](/sql/t-sql/queries/update-transact-sql)   
 [Data Types &#40;Transact-SQL&#41;](/sql/t-sql/data-types/data-types-transact-sql)  
  
  
