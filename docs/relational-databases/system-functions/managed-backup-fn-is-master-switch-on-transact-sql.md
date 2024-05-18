---
title: "managed_backup.fn_is_master_switch_on (Transact-SQL)"
description: "managed_backup.fn_is_master_switch_on (Transact-SQL)"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "fn_is_master_switch_on"
  - "fn_is_master_switch_on_TSQL"
  - "smart_admin.fn_is_master_switch_on"
  - "smart_admin.fn_is_master_switch_on_TSQL"
helpviewer_keywords:
  - "smart_admin.fn_is_master_switch_on"
  - "fn_is_master_switch_on"
dev_langs:
  - "TSQL"
---
# managed_backup.fn_is_master_switch_on (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

  Returns the state of the [!INCLUDE[ss-managed-backup](../../includes/ss-managed-backup-md.md)] operations on the instance of SQL Server.  
  
 Use this function to get the current state of [!INCLUDE[ss-managed-backup](../../includes/ss-managed-backup-md.md)].  
  
 
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```sql  
SELECT managed_backup.fn_is_master_switch_on()  
```  
  
##  <a name="Arguments"></a> Arguments  
 None  
  
## Return Type  
 **BIT**  
  
 1 = [!INCLUDE[ss-managed-backup](../../includes/ss-managed-backup-md.md)] is active, 0 = [!INCLUDE[ss-managed-backup](../../includes/ss-managed-backup-md.md)] is paused.  
  
## Security  
  
### Permissions  
 Requires SELECT permissions on the function.  
  
## See Also  
 [SQL Server Managed Backup to Microsoft Azure](../../relational-databases/backup-restore/sql-server-managed-backup-to-microsoft-azure.md)  
  
  
