---
title: "RECONFIGURE (Transact-SQL)"
description: "RECONFIGURE (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "05/20/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "RECONFIGURE"
  - "RECONFIGURE_TSQL"
helpviewer_keywords:
  - "reconfiguring configuration options"
  - "configuration options [SQL Server], reconfiguring"
  - "updating configuration options"
  - "RECONFIGURE, RECONFIGURE statement"
  - "RECONFIGURE"
  - "RECONFIGURE, WITH OVERRIDE statement"
dev_langs:
  - "TSQL"
---
# RECONFIGURE (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

  Updates the currently configured value (the **config_value** column in the **sp_configure** result set) of a configuration option changed with the **sp_configure** system stored procedure. Because some configuration options require a server stop and restart to update the currently running value, RECONFIGURE does not always update the currently running value (the **run_value** column in the **sp_configure** result set) for a changed configuration value.    
    
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)    
    
## Syntax    
    
```syntaxsql
RECONFIGURE [ WITH OVERRIDE ]    
```    
    
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 RECONFIGURE    
 Specifies that if the configuration setting does not require a server stop and restart, the currently running value should be updated. RECONFIGURE also checks the new configuration values for either values that are not valid (for example, a sort order value that does not exist in **syscharsets**) or nonrecommended values. With those configuration options not requiring a server stop and restart, the currently running value and the currently configured values for the configuration option should be the same value after RECONFIGURE is specified.    
    
 WITH OVERRIDE    
 Disables the configuration value checking (for values that are not valid or for nonrecommended values) for the **recovery interval** advanced configuration option.    
    
 Almost any configuration option can be reconfigured by using the WITH OVERRIDE option, however some fatal errors are still prevented. For example, the **min server memory** configuration option cannot be configured with a value greater than the value specified in the **max server memory** configuration option.
      
## Remarks    
 **sp_configure** does not accept new configuration option values out of the documented valid ranges for each configuration option.    
    
 RECONFIGURE is not allowed in an explicit or implicit transaction. When you reconfigure several options at the same time, if any of the reconfigure operations fail, none of the reconfigure operations will take effect.    
    
 When reconfiguring the resource governor, see the RECONFIGURE option of [ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-governor-transact-sql.md).    
    
## Permissions    
 RECONFIGURE permissions default to grantees of the ALTER SETTINGS permission. The **sysadmin** and **serveradmin** fixed server roles implicitly hold this permission.    
    
## Examples    
 The following example sets the upper limit for the `recovery interval` configuration option to `75` minutes and uses `RECONFIGURE WITH OVERRIDE` to install it. Recovery intervals greater than 60 minutes are not recommended and disallowed by default. However, because the `WITH OVERRIDE` option is specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not check whether the value specified (`75`) is a valid value for the `recovery interval` configuration option.    
    
```sql    
EXEC sp_configure 'recovery interval', 75    
RECONFIGURE WITH OVERRIDE;    
GO    
```    
    
## See Also    
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)     
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)    
    
  
