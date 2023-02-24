---
description: "sys.syscolumns (Transact-SQL)"
title: "sys.syscolumns (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sys.syscolumns"
  - "sys.syscolumns_TSQL"
  - "syscolumns_TSQL"
  - "syscolumns"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "syscolumns system table"
  - "sys.syscolumns compatibility view"
ms.assetid: 863fd87b-ff33-4ac5-9aa9-df21140681da
author: rwestMSFT
ms.author: randolphwest
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.syscolumns (Transact-SQL)
[!INCLUDE [sql-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdbmi-asa-pdw.md)]

  Returns one row for every column in every table and view, and a row for each parameter in a stored procedure in the database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssnoteCompView](../../includes/ssnotecompview-md.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Name of the column or procedure parameter.|  
|**id**|**int**|Object ID of the table to which this column belongs, or the ID of the stored procedure with which this parameter is associated.|  
|**xtype**|**tinyint**|Physical storage type from **sys.types**.|  
|**typestat**|**tinyint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**xusertype**|**smallint**|ID of extended user-defined data type. Overflows or returns NULL if the number of data types exceeds 32,767.|  
|**length**|**smallint**|Maximum physical storage length from **sys**.**types**.|  
|**xprec**|**tinyint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**xscale**|**tinyint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**colid**|**smallint**|Column or parameter ID.|  
|**xoffset**|**smallint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**bitpos**|**tinyint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**reserved**|**tinyint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**colstat**|**smallint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**cdefault**|**int**|ID of the default for this column.|  
|**domain**|**int**|ID of the rule or CHECK constraint for this column.|  
|**number**|**smallint**|Subprocedure number when the procedure is grouped.<br /><br /> 0 = Nonprocedure entries|  
|**colorder**|**smallint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**autoval**|**varbinary(8000)**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**offset**|**smallint**|Offset into the row in which this column appears.|  
|**collationid**|**int**|ID of the collation of the column. NULL for noncharacter-based columns.|  
|**status**|**tinyint**|Bitmap used to describe a property of the column or the parameter:<br /><br /> 0x08 = Column allows null values.<br /><br /> 0x10 = ANSI padding was in effect when **varchar** or **varbinary** columns were added. Trailing blanks are preserved for **varchar** and trailing zeros are preserved for **varbinary** columns.<br /><br /> 0x40 = Parameter is an OUTPUT parameter.<br /><br /> 0x80 = Column is an identity column.|  
|**type**|**tinyint**|Physical storage type from **sys**.**types**.|  
|**usertype**|**smallint**|ID of user-defined data type from **sys.types**. Overflows or returns NULL if the number of data types exceeds 32,767.|  
|**printfmt**|**varchar(255)**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**prec**|**smallint**|Level of precision for this column.<br /><br /> -1 = **xml** or large value type.|  
|**scale**|**int**|Scale for this column.<br /><br /> NULL = Data type is nonnumeric.|  
|**iscomputed**|**int**|Flag indicating whether the column is computed:<br /><br /> 0 = Noncomputed<br /><br /> 1 = Computed|  
|**isoutparam**|**int**|Indicates whether the procedure parameter is an output parameter:<br /><br /> 1 = True<br /><br /> 0 = False|  
|**isnullable**|**int**|Indicates whether the column allows null values:<br /><br /> 1 = True<br /><br /> 0 = False|  
|**collation**|**sysname**|Name of the collation of the column. NULL if not a character-based column.|  
  
## See Also  
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)   
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)  
  
  
