---
title: "sp_db_increased_partitions | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_db_increased_partitions_TSQL"
  - "sp_db_increased_partitions"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_db_increased_partitions"
ms.assetid: a8c043ec-b504-4929-ac0e-8babaa99d989
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_db_increased_partitions
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Enables or disables support for up to 15,000 partitions for the specified database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_dp_increased_partitions   
[ @dbname ] = 'database_name'   
[ , [ @increased_partitions = ] 'increased_partitions' ] [;]  
```  
  
## Arguments  
 [ @dbname= ] '*database_name*'  
 Is the name of the database. *dbname* is **sysname** with a default value of NULL. If *dbname* is not specified, the current database is used.  
  
 [ @increased_partitions= ] '*increased_partitions*'  
 Enables or disables support for 15,000 partitions on the specified database. *increased_partitions* is **varchar(6)** with a default of NULL. Accepted values are 'ON' or 'TRUE' to enable support and 'OFF' or 'FALSE' to disable support. If *increased_partitions* is not specified, the procedure returns 1 to indicate support is enabled for the specified database or 0 to indicate support is disabled.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Permissions  
 Requires ALTER DATABASE permission on the specified database.  
  
  
