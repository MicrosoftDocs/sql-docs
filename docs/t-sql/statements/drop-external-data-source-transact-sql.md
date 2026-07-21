---
title: "DROP EXTERNAL DATA SOURCE (Transact-SQL)"
description: DROP EXTERNAL DATA SOURCE removes an external data source used for PolyBase and data virtualization.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: hudequei, wiassaf
ms.date: 05/13/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =azuresqledge-current"
---
# DROP EXTERNAL DATA SOURCE (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw.md)]

  Removes an external data source used for PolyBase and data virtualization features.

 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  

## Syntax

```syntaxsql
-- Drop an external data source  
DROP EXTERNAL DATA SOURCE external_data_source_name  
[;]  
```  

## Arguments

#### external_data_source_name

 The name of the external data source to drop.  

## Metadata

 To view a list of external data sources, use the `sys.external_data_sources` system view.  

```sql
SELECT * FROM sys.external_data_sources;  
```  

## Permissions

 Requires ALTER ANY EXTERNAL DATA SOURCE.  

## Locking

 Takes a shared lock on the external data source object.  

## Remarks

 Dropping an external data source does not remove the external data.  

## Examples

<a id="a-using-basic-syntax"></a>

### A. Use basic syntax

```sql
DROP EXTERNAL DATA SOURCE mydatasource;  
```  

## Related content

- [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](create-external-data-source-transact-sql.md)
- [sys.external_data_sources (Transact-SQL)](../../relational-databases/system-catalog-views/sys-external-data-sources-transact-sql.md)
