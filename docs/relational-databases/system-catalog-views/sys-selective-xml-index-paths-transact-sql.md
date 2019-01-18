---
title: "sys.selective_xml_index_paths (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "xml_schema_attributes_TSQL"
  - "xml_schema_attributes"
  - "sys.xml_schema_attributes_TSQL"
  - "sys.xml_schema_attributes"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.xml_schema_attributes catalog view"
ms.assetid: 07a73d71-ec3e-4894-947a-5859ca62c606
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# sys.selective_xml_index_paths (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Available beginning in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Service Pack 1, each row in sys.selective_xml_index_paths represents one promoted path for particular selective xml index.  
  
 If you create a selective xml index on xmlcol of table T using following statement,  
  
```  
CREATE SELECTIVE XML INDEX sxi1 ON T(xmlcol)   
FOR ( path1 = '/a/b/c' AS XQUERY 'xs:string',  
      path2 = '/a/b/d' AS XQUERY 'xs:double'  
    )  
```  
  
 There will be two new rows in sys.selective_xml_index_paths corresponding to the index sxi1.  

  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|ID of table with XML column.|  
|**index_id**|**int**|Unique id of the selective xml index.|  
|**path_id**|**int**|Promoted XML path id.|  
|**path**|**nvarchar(4000)**|Promoted path. For example, '/a/b/c/d/e'.|  
|**name**|**sysname**|Path name.|  
|**path_type**|**tinyint**|0 = XQUERY<br /><br /> 1 = SQL|  
|**path_type_desc**|**sysname**|Based on **path_type** value 'XQUERY' or 'SQL'.|  
|**xml_component_id**|**int**|Unique ID of the XML schema component in the database.|  
|**xquery_type_description**|**nvarchar(4000)**|Name of the specified xsd type.|  
|**is_xquery_type_inferred**|**bit**|1 = type is inferred.|  
|**xquery_max_length**|**smallint**|Max length (in character of xsd type).|  
|**is_xquery_max_length_inferred**|**bit**|1 = maximum length is inferred.|  
|**is_node**|**bit**|0 = node() hint not present.<br /><br /> 1 = node() optimization hint applied.|  
|**system_type_id**|**tinyint**|ID of the system type of the column.|  
|**user_type_id**|**tinyint**|ID of the user type of the column.|  
|**max_length**|**smallint**|Max Length (in bytes) of the type.<br /><br /> -1 = Column data type is varchar(max), nvarchar(max), varbinary(max), or xml.|  
|**precision**|**tinyint**|Maximum precision of the type if it is numeric-based. Otherwise 0.|  
|**scale**|**tinyint**|Maximum scale of the type if it is numeric-based. Otherwise, 0.|  
|**collation_name**|**sysname**|Name of the collation of the type if it is character-based. Otherwise, NULL.|  
|**is_singleton**|**bit**|0 = SINGLETON hint not present.<br /><br /> 1 = SINGLETON optimization hint applied.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [XML Schemas &#40;XML Type System&#41; Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/xml-schemas-xml-type-system-catalog-views-transact-sql.md)  
  
  
