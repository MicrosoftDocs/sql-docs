---
title: "sp_db_increased_partitions"
description: "sp_db_increased_partitions"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_db_increased_partitions_TSQL"
  - "sp_db_increased_partitions"
helpviewer_keywords:
  - "sp_db_increased_partitions"
dev_langs:
  - "TSQL"
---
# sp_db_increased_partitions
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Enables or disables support for up to 15,000 partitions for the specified database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
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
  
  
