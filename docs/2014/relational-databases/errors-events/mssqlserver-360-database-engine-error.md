---
title: "MSSQLSERVER_360 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "360 (Database Engine error)"
ms.assetid: e2b7c1b2-3679-4206-9b25-6bd55ef96a2c
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_360
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|360|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DML_UPDATE_SPARSE_AND_COLSET|  
|Message Text|The target column list of an INSERT, UPDATE, or MERGE statement cannot contain both a sparse column and the column set that contains the sparse column. Rewrite the statement to include either the sparse column or the column set, but not both.|  
  
## Explanation  
 A column set is an untyped XML representation that combines some columns of a table into a structured output. An attempt was made to modify both the column set and a column that is included in the column set, causing two references to the same column.  
  
## User Action  
 Rewrite the statement to include references to either the column or the column set, but do not include both in the statement.  
  
  
