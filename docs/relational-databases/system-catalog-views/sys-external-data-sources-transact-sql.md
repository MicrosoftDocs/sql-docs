---
title: "sys.external_data_sources (Transact-SQL)"
description: "The sys.external_data_sources system catalog view contains a row for each external data source in the current database."
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/28/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"

monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.external_data_sources (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa-pdw.md)]

  Contains a row for each external data source in the current database for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE[ssSDS](../../includes/sssds-md.md)], and [!INCLUDE[ssSDW](../../includes/sssdw-md.md)].

 Contains a row for each external data source on the server for [!INCLUDE[ssPDW](../../includes/sspdw-md.md)].

|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|data_source_id|**int**|Object ID for the external data source.||  
|name|**sysname**|Name of the external data source.||  
|location|**nvarchar(4000)**|The connection string, which includes the protocol, IP address, and port for the external data source.||  
|type_desc|**nvarchar(255)**|Data source type displayed as a string.|HADOOP, RDBMS, SHARD_MAP_MANAGER, REMOTE_DATA_ARCHIVE, BLOB_STORAGE, NONE|  
|type|**tinyint**|Data source type displayed as a number.|0 - HADOOP<br /><br /> 1 - RDBMS<br /><br /> 2 - SHARD_MAP_MANAGER<br /><br /> 3 - REMOTE_DATA_ARCHIVE<br /><br /> 4 - *internal use only*<br /><br /> 5 - BLOB_STORAGE<br /><br /> 6 - NONE |  
|resource_manager_location|**nvarchar(4000)**|For type HADOOP, the IP and port location of the Hadoop Resource Manager. The `resource_manager_location` is used for submitting a job on a Hadoop data source.<br /><br /> `NULL` for other types of external data sources.||  
|credential_id|**int**|The object ID of the database scoped credential used to connect to the external data source.||  
|database_name|**sysname**|For type RDBMS, the name of the remote database. For type SHARD_MAP_MANAGER, the name of the shard map manager database. NULL for other types of external data sources.||  
|shard_map_name|**sysname**|For type SHARD_MAP_MANAGER, the name of the shard map. NULL for other types of external data sources.||  
|connection_options|**nvarchar(4000)**|**Applies to:** [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] and later.  The `connection_options` will contain the same string from your CONNECTION_OPTIONS parameter from [CREATE EXTERNAL DATA SOURCE CONNECTION_OPTIONS](../../t-sql/statements/create-external-data-source-connection-options.md).<BR /><BR />In [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], this is a semicolon-separated string.<BR />In [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], this can also be a JSON-formatted string. | |
|pushdown|**nvarchar(256)**|*Applies to:* [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] and later.<br /><br />NOT NULL. Whether pushdown is enabled. For more information, see [Pushdown computations in PolyBase](../polybase/polybase-pushdown-computation.md).|ON, OFF |

## Permissions

 The visibility of the metadata in catalog views is limited to securables that a user either owns or on which the user has been granted some permission. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).

## Remarks

SQL Server support for HDFS Cloudera (CDP) and Hortonworks (HDP) external data sources are retired and not included in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]. There's no need to use the `CREATE EXTERNAL DATA SOURCE ... TYPE` argument in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)].

## Next steps

- [sys.external_file_formats (Transact-SQL)](../../relational-databases/system-catalog-views/sys-external-file-formats-transact-sql.md)
- [sys.external_tables (Transact-SQL)](../../relational-databases/system-catalog-views/sys-external-tables-transact-sql.md)
- [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md)
- [CREATE EXTERNAL DATA SOURCE (Transact-SQL) CONNECTION_OPTIONS](../../t-sql/statements/create-external-data-source-connection-options.md)
- [Pushdown computations in PolyBase](../polybase/polybase-pushdown-computation.md)
- [Introducing data virtualization with PolyBase](../polybase/polybase-guide.md)
