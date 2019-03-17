---
title: "MSSQLSERVER_1904 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "1904 (Database Engine error)"
ms.assetid: 2a35d57d-74e2-45a2-8f67-3f2e51d69712
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_1904
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|1904|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|KEYCOUNT|  
|Message Text|The %S_MSG '%.*ls' on table '%.\*ls' has %d column names in %S_MSG key list. The maximum limit for index or statistics key column list is %d.|  
  
## Explanation  
The key column list for the specified index or statistics exceeds the maximum number of columns allowed.  
  
## User Action  
Modify the key column list to include no more than the specified maximum number of columns.  
  
For nonclustered indexes, consider using the INCLUDE clause in the CREATE INDEX statement to add columns to the index as nonkey columns. This method avoids exceeding the current index size limitation of a maximum of 16 key columns. For more information, see [Create Indexes with Included Columns](~/relational-databases/indexes/create-indexes-with-included-columns.md).  
  
## See Also  
[CREATE INDEX &#40;Transact-SQL&#41;](~/t-sql/statements/create-index-transact-sql.md)  
[CREATE STATISTICS &#40;Transact-SQL&#41;](~/t-sql/statements/create-statistics-transact-sql.md)  
  
