---
description: "sys.syscharsets (Transact-SQL)"
title: "sys.syscharsets (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sys.syscharsets"
  - "syscharsets"
  - "sys.syscharsets_TSQL"
  - "syscharsets_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "syscharsets system table"
  - "sys.syscharsets compatibility view"
ms.assetid: f16d987c-bd19-4668-9ef7-785b8fb9ff5b
author: rwestMSFT
ms.author: randolphwest
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.syscharsets (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Contains one row for each character set and sort order defined for use by the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. One of the sort orders is marked in **sysconfigures** as the default sort order. This is the only one actually being used.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**type**|**smallint**|Type of entity this row represents:<br /><br /> 1001 = Character set.<br /><br /> 2001 = Sort order.|  
|**id**|**tinyint**|Unique ID for the character set or sort order. Note sort orders and character sets cannot share the same ID number. The ID range of 1 through 240 is reserved for use by the [!INCLUDE[ssDE](../../includes/ssde-md.md)].|  
|**csid**|**tinyint**|If the row represents a character set, this field is unused. If the row represents a sort order, this field is the ID of the character set that the sort order is built on. It is assumed a character set row with this ID exists in this table.|  
|**status**|**smallint**|Internal system status information bits.|  
|**name**|**sysname**|Unique name for the character set or sort order. This field must contain only the letters A-Z or a-z, numbers 0 - 9, and underscores(_); and it must start with a letter.|  
|**description**|**nvarchar(255)**|Optional description of the features of the character set or sort order.|  
|**binarydefinition**|**varbinary(6000)**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**definition**|**image**|Internal definition of the character set or sort order. The structure of the data in this field depends on the type.|  
  
## See Also  
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)   
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)  
  
  
