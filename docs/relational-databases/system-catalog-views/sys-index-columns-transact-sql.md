---
title: "sys.index_columns (Transact-SQL)"
description: "sys.index_columns contains one row per column that is part of an index or unordered table (heap)."
author: rwestMSFT
ms.author: randolphwest
ms.date: 02/28/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.index_columns"
  - "sys.index_columns_TSQL"
  - "index_columns"
  - "index_columns_TSQL"
helpviewer_keywords:
  - "sys.index_columns catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.index_columns (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

  Contains one row per column that is part of an index or unordered table (heap).

|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|**object_id**|**int**|ID of the object the index is defined on.|
|**index_id**|**int**|ID of the index in which the column is defined.|
|**index_column_id**|**int**|ID of the index column. `index_column_id` is unique only within `index_id`.|
|**column_id**|**int**|ID of the column in `object_id`.<br /><br />`0` = Row Identifier (RID) in a nonclustered index.<br /><br />`column_id` is unique only within `object_id`.|
|**key_ordinal**|**tinyint**|Ordinal (1-based) within set of key-columns.<br /><br />0 = Not a key column, or is an XML index, columnstore index, spatial index or JSON index.<br /><br />Note: An XML or spatial or JSON index can't be a key because the underlying columns aren't comparable, meaning that their values can't be ordered.|
|**partition_ordinal**|**tinyint**|Ordinal (1-based) within set of partitioning columns. A clustered columnstore index can have at most one partitioning column.<br /><br />0 = Not a partitioning column.|
|**is_descending_key**|**bit**|`1` = Index key column has a descending sort direction.<br /><br />`0` = Index key column has an ascending sort direction, or the column is part of a columnstore or hash index.|
|**is_included_column**|**bit**|`1` = Column is a nonkey column added to the index by using the CREATE INDEX INCLUDE clause, or the column is part of a columnstore index.<br /><br />`0` = Column isn't an included column.<br /><br />Columns implicitly added because they're part of the clustering key aren't listed in `sys.index_columns`.<br /><br />Columns implicitly added because they're a partitioning column are returned as `0`.|
|**column_store_order_ordinal**|**tinyint**|**Applies to**: [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazure-sqlmi-autd](../../includes/applies-to-version/ssazure-sqlmi-autd.md)]<br /><br />Ordinal (1-based) within set of order columns in an ordered columnstore index. For more on ordered columnstore indexes, see [Performance tuning with ordered columnstore indexes](../indexes/ordered-columnstore-indexes.md).|
| `data_clustering_ordinal` | **tinyint** | 0 = Not a columnstore index & data clustering ordinal doesn't apply <br /> **Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] |

## Permissions

[!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).

## Examples

 The following example returns all indexes and index columns for the table `Production.BillOfMaterials`.

```sql
USE AdventureWorks2022;
GO
SELECT i.name AS index_name
    ,COL_NAME(ic.object_id,ic.column_id) AS column_name
    ,ic.index_column_id
    ,ic.key_ordinal
,ic.is_included_column
FROM sys.indexes AS i
INNER JOIN sys.index_columns AS ic
    ON i.object_id = ic.object_id AND i.index_id = ic.index_id
WHERE i.object_id = OBJECT_ID('Production.BillOfMaterials');
```

[!INCLUDE[ssResult](../../includes/ssresult-md.md)]

```output
index_name                                                 column_name        index_column_id key_ordinal is_included_column
---------------------------------------------------------- -----------------  --------------- ----------- -------------
AK_BillOfMaterials_ProductAssemblyID_ComponentID_StartDate ProductAssemblyID  1               1           0
AK_BillOfMaterials_ProductAssemblyID_ComponentID_StartDate ComponentID        2               2           0
AK_BillOfMaterials_ProductAssemblyID_ComponentID_StartDate StartDate          3               3           0
PK_BillOfMaterials_BillOfMaterialsID                       BillOfMaterialsID  1               1           0
IX_BillOfMaterials_UnitMeasureCode                         UnitMeasureCode    1               1           0
  
(5 row(s) affected)
```

## Next steps

- [Object Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)
- [Catalog Views (Transact-SQL)](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)
- [sys.indexes (Transact-SQL)](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)
- [sys.objects (Transact-SQL)](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)
- [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md)
- [sys.columns (Transact-SQL)](../../relational-databases/system-catalog-views/sys-columns-transact-sql.md)
- [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.yml)
