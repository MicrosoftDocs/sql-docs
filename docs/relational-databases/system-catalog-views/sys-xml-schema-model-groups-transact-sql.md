---
title: "sys.xml_schema_model_groups (Transact-SQL)"
description: sys.xml_schema_model_groups (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.xml_schema_model_groups"
  - "xml_schema_model_groups"
  - "sys.xml_schema_model_groups_TSQL"
  - "xml_schema_model_groups_TSQL"
helpviewer_keywords:
  - "sys.xml_schema_model_groups catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 566556dc-a8c8-465c-9196-c7e0ae092a8a
---
# sys.xml_schema_model_groups (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a row per XML schema component that is a Model-Group, **symbol_space** of **M**..  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**\<inherited columns>**||Inherits columns from [sys.xml_schema_components](../../relational-databases/system-catalog-views/sys-xml-schema-components-transact-sql.md).|  
|**compositor**|**char(1)**|Compositor kind of group:<br /><br /> A = XSD \<all> Group<br /><br /> C = XSD \<choice> Group<br /><br /> S = XSD \<sequence> Group|  
|**compositor_desc**|**nvarchar (60)**|Description of compositor kind of group:<br /><br /> XSD_ALL_GROUP<br /><br /> XSD_CHOICE_GROUP<br /><br /> XSD_SEQUENCE_GROUP|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [XML Schemas &#40;XML Type System&#41; Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/xml-schemas-xml-type-system-catalog-views-transact-sql.md)  
  
  
