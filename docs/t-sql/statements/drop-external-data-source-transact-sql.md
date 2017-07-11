---
title: "DROP EXTERNAL DATA SOURCE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 3f65a2f5-a6c6-4be5-8ca4-6057078fe10e
caps.latest.revision: 14
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
---
# DROP EXTERNAL DATA SOURCE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-asdw-pdw_md](../../includes/tsql-appliesto-ss2016-xxxx-asdw-pdw-md.md)]

  Removes a PolyBase external data source.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure SQL Data Warehouse, Parallel Data Warehouse  
  
-- Drop an external data source  
DROP EXTERNAL DATA SOURCE external_data_source_name  
[;]  
```  
  
## Arguments  
 *external_data_source_name*  
 The name of the external data source to drop.  
  
## Metadata  
 To view a list of external data sources use the sys.external_data_sources system view.  
  
```  
SELECT * FROM sys.external_data_sources;  
```  
  
## Permissions  
 Requires ALTER ANY EXTERNAL DATA SOURCE.  
  
## Locking  
 Takes a shared lock on the external data source object.  
  
## General Remarks  
 Dropping an external data source does not remove the external data.  
  
## Examples  
  
### A. Using basic syntax  
  
```  
DROP EXTERNAL DATA SOURCE mydatasource;  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
  
### B. Using basic syntax  
  
```  
DROP EXTERNAL DATA SOURCE mydatasource;  
```  
  
## See Also  
 [CREATE EXTERNAL DATA SOURCE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-data-source-transact-sql.md)  
  
  

