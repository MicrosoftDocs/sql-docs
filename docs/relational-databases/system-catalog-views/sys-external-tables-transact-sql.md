---
title: "sys.external_tables (Transact-SQL)"
description: sys.external_tables (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: hudequei, wiassaf
ms.date: 05/13/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "external_tables"
  - "sys.external_tables"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# sys.external_tables (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw-fabricdw-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw-fabricdw-fabricsqldb.md)]

 Contains a row for each external table in the current database.  

|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|\<inherited columns>||For a list of columns that this view inherits, see [sys.objects (Transact-SQL)](sys-objects-transact-sql.md).||  
|`max_column_id_used`|**int**|Maximum column ID ever used for this table.||  
|`uses_ansi_nulls`|**bit**|Table was created with the `SET ANSI_NULLS` database option ON.||  
|`data_source_id`|**int**|Object ID for the external data source.||  
|`file_format_id`|**int**|For external tables over a HADOOP external data source, this is the `object_id` for the external file format.||  
|`location`|**nvarchar(4000)**|For external tables over a HADOOP external data source, this is the path of the external data in HDFS.||
|`reject_type`|**tinyint**|For external tables over a HADOOP external data source, this is the way rejected rows are counted when querying external data.|`VALUE` - the number of rejected rows.<br /><br /> `PERCENTAGE` - the percentage of rejected rows.|  
|`reject_value`|**float**|For external tables over a HADOOP external data source:<br /><br /> For *reject_type =* value, this is the number of row rejections to allow before failing the query.<br /><br /> For `reject_type = PERCENTAGE`, this is the percentage of row rejections to allow before failing the query.||  
|`reject_sample_value`|**int**|For `reject_type = PERCENTAGE`, this is the number of rows to load, either successfully or unsuccessfully, before calculating the percentage of rejected rows.|`NULL` if `reject_type` = `VALUE`.|  
|`distribution_type`|**int**|For external tables over a SHARD_MAP_MANAGER external data source, this is the data distribution of the rows across the underlying base tables.|`0` - Sharded<br /><br /> `1` - Replicated<br /><br /> `2` - Round robin|  
|`distribution_desc`|**nvarchar(120)**|For external tables over a SHARD_MAP_MANAGER external data source, this is the distribution type displayed as a string.||  
|`sharding_column_id`|**int**|For external tables over a SHARD_MAP_MANAGER external data source and a sharded distribution, this is the column ID of the column that contains the sharding key values.||  
|`remote_schema_name`|**sysname**|For external tables over a SHARD_MAP_MANAGER external data source, this is the schema where the base table is located on the remote databases (if different from the schema where the external table is defined).||  
|`remote_object_name`|**sysname**|For external tables over a SHARD_MAP_MANAGER external data source, this is the name of the base table on the remote databases (if different from the name of the external table).||  

## Permissions

 The visibility of the metadata in catalog views is limited to securables that a user either owns or on which the user has been granted some permission. For more information, see [Metadata Visibility Configuration](../security/metadata-visibility-configuration.md).  

## Related content

- [sys.external_file_formats (Transact-SQL)](sys-external-file-formats-transact-sql.md)
- [sys.external_data_sources (Transact-SQL)](sys-external-data-sources-transact-sql.md)
- [CREATE EXTERNAL TABLE (Transact-SQL)](../../t-sql/statements/create-external-table-transact-sql.md)
