---
title: "sys.sp_rda_get_rpo_duration (Transact-SQL)"
description: Use sys.sp_rda_get_rpo_duration to get the number of hours of migrated data that SQL Server retains in a staging table to fully restore the remote Azure database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/25/2022
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
f1_keywords:
  - "sys.sp_rda_get_rpo_duration"
  - "sys.sp_rda_get_rpo_duration_TSQL"
helpviewer_keywords:
  - "sys.sp_rda_get_rpo_duration stored procedure"
dev_langs:
  - "TSQL"
---
# sys.sp_rda_get_rpo_duration (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

  Gets the number of hours of migrated data that SQL Server retains in a staging table to help ensure a full restore of the remote Azure database, if a point in time restore is necessary. 
  
  For more info, see [Stretch Database reduces the risk of data loss for your Azure data by retaining migrated rows temporarily](../../sql-server/stretch-database/backup-stretch-enabled-databases-stretch-database.md#stretchRPO).  

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]
      
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)    
    
## Syntax    
    
```    
    
sp_rda_get_rpo_duration @durationinhours output    
    
```    
    
## Output parameter    
 *\@durationinhours*    
  Is the number of hours (a non-null integer value) of migrated data that SQL Server retains for the current Stretch-enabled database.    
    
## Permissions    
 Requires db_owner permissions.    
    
## Remarks    
 Change the value by running [sys.sp_rda_set_rpo_duration &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-rda-set-rpo-duration-transact-sql.md).    
    
## See Also    
 [sys.sp_rda_set_rpo_duration &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-rda-set-rpo-duration-transact-sql.md)     
 [Restore Stretch-enabled databases (Stretch Database)](../../sql-server/stretch-database/restore-stretch-enabled-databases-stretch-database.md)    
 [Stretch Database](../../sql-server/stretch-database/stretch-database.md)    
    
  
