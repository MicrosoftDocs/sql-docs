---
title: sys.identity_columns (Transact-SQL)
description: sys.identity_columns (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.reviewer: wiassaf
ms.date: 08/18/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "identity_columns"
  - "sys.identity_columns"
  - "sys.identity_columns_TSQL"
  - "identity_columns_TSQL"
helpviewer_keywords:
  - "sys.identity_columns catalog view"
dev_langs:
  - TSQL
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# sys.identity_columns (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricdw-fabricsqldb.md)]

 The `sys.identity_columns` view contains a row for each column that is an identity column.

 The `sys.identity_columns` view inherits rows from the `sys.columns` view. The `sys.identity_columns` view returns the columns in the `sys.columns` view, plus the `seed_value`, `increment_value`, `last_value`, and `is_not_for_replication` columns. For more information, see [Catalog Views (Transact-SQL)](catalog-views-transact-sql.md).  

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
| `<columns inherited from sys.columns>` ||The `sys.identity_columns` view returns all columns in the `sys.columns` view. It also returns the additional columns described in the following rows. For a description of the columns that the `sys.identity_columns` view inherits from `sys.columns`, see [sys.columns (Transact-SQL)](sys-columns-transact-sql.md).|  
| `seed_value` |**sql_variant**|Seed value for this identity column. The data type of the seed value is the same as the data type of the column itself.|  
| `increment_value` |**sql_variant**|Increment value for this identity column. The data type of the seed value is the same as the data type of the column itself.|  
| `last_value` |**sql_variant**|Last value generated for this identity column. The data type of the seed value is the same as the data type of the column itself.|  
| `is_not_for_replication` |**bit**|Identity column is declared `NOT FOR REPLICATION`. **Note:** This column doesn't apply to Azure Synapse Analytics.|  

> [!NOTE]  
>  To create an automatically incrementing number that you can use in multiple tables or call from applications without referencing any table, see [Sequence Numbers](../sequence-numbers/sequence-numbers.md).  

## Permissions

 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../security/metadata-visibility-configuration.md).  

## Related content

- [Object Catalog Views (Transact-SQL)](object-catalog-views-transact-sql.md)
- [Catalog Views (Transact-SQL)](catalog-views-transact-sql.md)
- [Querying the SQL Server System Catalog FAQ](querying-the-sql-server-system-catalog-faq.yml)