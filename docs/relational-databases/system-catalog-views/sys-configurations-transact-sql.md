---
title: "sys.configurations (Transact-SQL)"
description: sys.configurations contains a row for each server-wide configuration option value in the system.
author: rwestMSFT
ms.author: randolphwest
ms.date: 02/05/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.configurations_TSQL"
  - "configurations"
  - "sys.configurations"
  - "configurations_TSQL"
helpviewer_keywords:
  - "sys.configurations catalog view"
dev_langs:
  - "TSQL"
---
# sys.configurations (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a row for each server-wide configuration option value in the system.

| Column name | Data type | Description |  
| --- | --- | --- |
| `configuration_id` | **int** | Unique ID for the configuration value. |
| `name` | **nvarchar(35)** | Name of the configuration option. |
| `value` | **sql_variant** | Configured value for this option. |
| `minimum` | **sql_variant** | Minimum value for the configuration option. |
| `maximum` | **sql_variant** | Maximum value for the configuration option. |
| `value_in_use` | **sql_variant** | Running value currently in effect for this option. |
| `description` | **nvarchar(255)** | Description of the configuration option. |
| `is_dynamic` | **bit** | 1 = The variable that takes effect when the RECONFIGURE statement is executed. |
| `is_advanced` | **bit** | 1 = The variable is displayed only when the **show advancedoption** is set. |  
  
## Remarks

For a list of all server configuration options, see [Server Configuration Options (SQL Server)](../../database-engine/configure-windows/server-configuration-options-sql-server.md).  
  
> [!NOTE]  
>  For database-level configuration options, see [ALTER DATABASE SCOPED CONFIGURATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md). To configure Soft-NUMA, see [Soft-NUMA &#40;SQL Server&#41;](../../database-engine/configure-windows/soft-numa-sql-server.md).  
 
The sys.configurations catalog view can be used to determine the config_value (the value column), the run_value (the value_in_use column), and whether the configuration option is dynamic (doesn't require a server engine restart or the is_dynamic column).

> [!NOTE]
> The config_value in the result set of sp_configure is equivalent to the **sys.configurations.value** column. The **run_value** is equivalent to the **sys.configurations.value_in_use** column.

The following query can be used to determine if any configured values haven't been installed:

```SQL
select * from sys.configurations where value != value_in_use
```

If the value equals the change for the configuration option you made but the **value_in_use** isn't the same, either the RECONFIGURE command wasn't run or has failed, or the server engine must be restarted.

There are configuration options where the value and value_in_use might not be the same and this is expected behavior. For example:

"max server memory (MB)" - The default configured value of 0 shows up as **value_in_use** = 2147483647<br>

"min server memory (MB)" - The default configured value of 0 might show up as **value_in_use** = 8 (32bit) or 16 (64bit). In some cases, the **value_in_use** is 0. In this situation, the "true" **value_in_use** is 8 (32bit) or 16 (64bit).


The **is_dynamic** column can be used to determine if the configuration option requires a restart. is_dynamic=1 means that when the RECONFIGURE(T-SQL) command is executed, the new value takes effect "immediately" (in some cases the server engine might not evaluate the new value immediately but does so in the normal course of its execution). is_dynamic=0 means the changed configuration value doesn't take effect until the server is restarted even though the RECONFIGURE(T-SQL) command was executed.

For a configuration option that isn't dynamic there's no way to tell if the RECONFIGURE(T-SQL) command has been run to perform the first step of installing the configuration change. Before you restart SQL Server to install a configuration change, run the RECONFIGURE(T-SQL) command to ensure all configuration changes take effect after a SQL Server restart. 
 
 
## Permissions

Requires membership in the **public** role.

### Permissions for SQL Server 2022 and later

Requires VIEW SERVER PERFORMANCE STATE permission on the server.

## Related content

- [Server-wide Configuration Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/server-wide-configuration-catalog-views-transact-sql.md)
- [Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
