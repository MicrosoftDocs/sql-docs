---
title: "sys.external_file_formats (Transact-SQL)"
description: sys.external_file_formats (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
ms.assetid: a89efb2c-0a3a-4b64-9284-6e93263e29ac
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.external_file_formats (Transact-SQL)
[!INCLUDE [sqlserver2016-asdbmi-asa-pdw](../../includes/applies-to-version/sqlserver2016-asdbmi-asa-pdw.md)]

  Contains a row for each external file format in the current database for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE[ssSDS](../../includes/sssds-md.md)], and [!INCLUDE[ssSDW](../../includes/sssdw-md.md)].  
  
 Contains a row for each external file format on the server for [!INCLUDE[ssPDW](../../includes/sspdw-md.md)].  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|file_format_id|**int**|Object ID for the external file format.||  
|name|**sysname**|Name of the file format. in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDW](../../includes/sssdw-md.md)], this is unique for the database. In [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], this is unique for the server.||  
|format_type|**tinyint**|The file format type.|DELIMITEDTEXT, RCFILE, ORC, PARQUET|  
|field_terminator|**nvarchar(10)**|For format_type = DELIMITEDTEXT, this is the field terminator.||  
|string_delimiter|**nvarchar(10)**|For format_type = DELIMITEDTEXT, this is the string delimiter.||  
|date_format|**nvarchar(50)**|For format_type = DELIMITEDTEXT, this is the user-defined date and time format.||  
|use_type_default|**bit**|For format_type = DELIMITED TEXT, specifies how to handle missing values when PolyBase is importing data from HDFS text files into [!INCLUDE[ssSDW](../../includes/sssdw-md.md)].|0 - store missing values as the string 'NULL'.<br /><br /> 1 - store missing values as the column default value.|  
|serde_method|**nvarchar(255)**|For format_type = RCFILE, this is the serialization/deserialization method.||  
|row_terminator|**nvarchar(10)**|For format_type = DELIMITEDTEXT, this is the character string that terminates each row in the external Hadoop file.|Always '\n'.|  
|encoding|**nvarchar(10)**|For format_type = DELIMITEDTEXT, this is the encoding method for the external Hadoop file.|Always 'UTF8'.|  
|data_compression|**nvarchar(255)**|The data compression method for the external data.|For format_type = DELIMITEDTEXT:<br /><br /> -   'org.apache.hadoop.io.compress.DefaultCodec'<br />-   'org.apache.hadoop.io.compress.GzipCodec'<br /><br /> For format_type = RCFILE:<br /><br /> -   'org.apache.hadoop.io.compress.DefaultCodec'<br /><br /> For format_type = ORC:<br /><br /> -   'org.apache.hadoop.io.compress.DefaultCodec'<br />-   'org.apache.hadoop.io.compress.SnappyCodec'<br /><br /> For format_type = PARQUET:<br /><br /> -   'org.apache.hadoop.io.compress.GzipCodec'<br />-   'org.apache.hadoop.io.compress.SnappyCodec'|  
  
## Permissions  
 The visibility of the metadata in catalog views is limited to securables that a user either owns or on which the user has been granted some permission. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [sys.external_data_sources &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-external-data-sources-transact-sql.md)   
 [sys.external_tables &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-external-tables-transact-sql.md)   
 [CREATE EXTERNAL FILE FORMAT &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-file-format-transact-sql.md)  
  
  
