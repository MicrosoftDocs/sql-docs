---
description: "sp_helpsort (Transact-SQL)"
title: "sp_helpsort (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_helpsort_TSQL"
  - "sp_helpsort"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_helpsort"
ms.assetid: 2a88d079-3755-43cb-8a54-97d0114149e6
author: markingmyname
ms.author: maghan
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_helpsort (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Displays the sort order and character set for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpsort  
```  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 Returns server default collation.  
  
## Remarks  
 If an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed with a collation specified to be compatible with an earlier installation of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], **sp_helpsort** returns blank results. When this behavior occurs, you can determine the collation by querying the SERVERPROPERTY object, such as: `SELECT SERVERPROPERTY ('Collation');`.  
  
## Permissions  
 Requires membership in the **public** role.  
  
## Examples  
 The following example displays the name of the default sort order of the server, its character set, and a table of its primary sort values.  
  
```  
sp_helpsort;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `Server default collation`  
  
 `------------------------`  
  
 `Latin1-General, case-sensitive, accent-sensitive, kanatype-insensitive, width-insensitive for Unicode Data, SQL Server Sort Order 51 on Code Page 1252 for non-Unicode Data.`  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [COLLATE &#40;Transact-SQL&#41;](~/t-sql/statements/collations.md)   
 [sys.fn_helpcollations &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-helpcollations-transact-sql.md)   
 [SERVERPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/serverproperty-transact-sql.md)  
  
  
