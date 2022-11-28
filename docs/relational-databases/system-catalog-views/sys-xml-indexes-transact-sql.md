---
title: "sys.xml_indexes (Transact-SQL)"
description: sys.xml_indexes (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.xml_indexes_TSQL"
  - "xml_indexes_TSQL"
  - "sys.xml_indexes"
  - "xml_indexes"
helpviewer_keywords:
  - "sys.xml_indexes catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 3408de72-b067-4fda-b5d5-8e856dfd9db3
---
# sys.xml_indexes (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns one row per XML index.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**\<inherited columns>**||Inherits columns from [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md).|  
|**using_xml_index_id**|**int**|NULL = Primary XML index.<br /><br /> Nonnull = Secondary XML index.<br /><br /> Nonnull is a self-join reference to the primary XML index.|  
|**secondary_type**|**char(1)**|Type description of secondary index:<br /><br /> P = PATH secondary XML index<br /><br /> V = VALUE secondary XML index<br /><br /> R = PROPERTY secondary XML index<br /><br /> NULL = Primary XML index|  
|**secondary_type_desc**|**nvarchar(60)**|Type description of secondary index:<br /><br /> PATH = PATH secondary XML index<br /><br /> VALUE = VALUE secondary XML index<br /><br /> PROPERTY = PROPERTY secondary xml indexes.<br /><br /> NULL = Primary XML index|  
|**xml_index_type**|**tinyint**|Index type:<br /><br /> 0 = Primary XML index<br /><br /> 1 = Secondary XML index<br /><br /> 2 = Selective XML index<br /><br /> 3 = Secondary selective  XML index|  
|**xml_index_type_description**|**nvarchar(60)**|Description of index type:<br /><br /> PRIMARY_XML<br /><br /> Secondary XML Index<br /><br /> Selective XML Index<br /><br /> Secondary Selective  XML index|  
|**path_id**|**int**|NULL for all XML indexes except secondary selective XML index.<br /><br /> Else, the ID of the promoted path over which the secondary selective XML index is built. This value is the same value as path_id from sys.selective_xml_index_paths system view.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)  
  
  
