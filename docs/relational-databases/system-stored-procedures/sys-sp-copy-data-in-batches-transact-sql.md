---
title: "sys.sp_copy_data_in_batches (Transact-SQL)"
description: "Copies data from the source table to the target table after verifying that their schema is identical in terms of number of columns, column names and their data types."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 05/03/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-ver16 || >=sql-server-linux-ver16"
---
# sys.sp_copy_data_in_batches (Transact-SQL)

[!INCLUDE [SQL Server 2022 Azure SQL Database](../../includes/applies-to-version/sqlserver2022-asdb.md)]

Copies data from the source table to the target table after verifying that their schema is identical in terms of number of columns, column names and their data types. `TRANSACTION ID`, `SEQUENCE NUMBER`, and `GENERATED ALWAYS` columns are ignored since they're system generated and this allows copying data from a regular table to a ledger table and vice versa. Indexes between the tables can be different but the target table can only be a heap or have a clustered index. The data is copied in batches in individual transactions. If the operation fails, the target table is partially populated.

For more information on database ledger, see [Ledger](/azure/azure-sql/database/ledger-overview).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_copy_data_in_batches
     [ @source_table_name = ] N'source_table_name'
     , [ @target_table_name = ] N'target_table_name'
```

## Arguments

#### [ @source_table_name = ] N'*source_table_name*'

The name of the table to be used as the source of the data copy.

#### [ @target_table_name = ] N'*target_table_name*'

The name of the table to be used as the target of the data copy.

## Return code values

0 (success)

## Result set

None.

## Permissions

This operation requires **SELECT** on the source table, **INSERT** in the target table, and **ALTER** on the target table if there are foreign key or check constraints that will be disabled, or an identity column that will be adjusted.

## Related content

- [Ledger considerations and limitations](../security/ledger/ledger-limits.md)
- [Ledger overview](../security/ledger/ledger-overview.md)
- [How to convert regular tables into ledger tables](../security/ledger/ledger-how-to-migrate-data-to-ledger-tables.md)
