---
title: sp_pdw_database_encryption_regenerate_system_keys (Azure Synapse Analytics)
description: Use sp_pdw_database_encryption_regenerate_system_keys to rotate the certificate and database encryption key for internal databases that are encrypted when TDE is enabled on the appliance.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 06/13/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest"
---
# sp_pdw_database_encryption_regenerate_system_keys (Azure Synapse Analytics)

[!INCLUDE [applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

Use `sp_pdw_database_encryption_regenerate_system_keys` to rotate the certificate and database encryption key for internal databases that are encrypted when TDE is enabled on the appliance. This includes `tempdb`. This will succeed only if TDE is enabled.

## Syntax

Syntax for Azure Synapse Analytics and Analytics Platform System (PDW).

```syntaxsql
sp_pdw_database_encryption_regenerate_system_keys
[ ; ]
```

> [!NOTE]
> [!INCLUDE [synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

The procedure has no parameters.

This procedure should be used when the traffic in the appliance is low.

## Permissions

Requires membership in the **sysadmin** fixed database role, or CONTROL SERVER permission.

## Examples

The following example regenerates the database encryption keys.

```sql
EXEC sys.sp_pdw_database_encryption_regenerate_system_keys;
```

## Related content

- [sp_pdw_database_encryption (Azure Synapse Analytics)](sp-pdw-database-encryption-sql-data-warehouse.md)
- [sp_pdw_log_user_data_masking (Azure Synapse Analytics)](sp-pdw-log-user-data-masking-sql-data-warehouse.md)
