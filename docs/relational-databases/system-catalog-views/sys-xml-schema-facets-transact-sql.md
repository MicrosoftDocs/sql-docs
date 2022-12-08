---
title: "sys.xml_schema_facets (Transact-SQL)"
description: sys.xml_schema_facets (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.xml_schema_facets"
  - "xml_schema_facets_TSQL"
  - "sys.xml_schema_facets_TSQL"
  - "xml_schema_facets"
helpviewer_keywords:
  - "sys.xml_schema_facets catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 4402dde9-1877-4872-8550-140dc2a177d2
---
# sys.xml_schema_facets (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a row per facet (restriction) of an xml-type definition (corresponds to **sys.xml_types**).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**xml_component_id**|**int**|ID of XML component (type) to which this facet belongs.|  
|**facet_id**|**int**|ID (1-based ordinal) of facet, unique within component-id.|  
|**kind**|**char(2)**|Kind of facet:<br /><br /> LG = Length<br /><br /> LN = Minimum Length<br /><br /> LX = Maximum Length<br /><br /> PT = Pattern (regular expression)<br /><br /> EU = Enumeration<br /><br /> IN = Minimum Inclusive value<br /><br /> IX = Maximum Inclusive value<br /><br /> EN = Minimum Exclusive value<br /><br /> EX = Maximum Exclusive value<br /><br /> DT = Total Digits<br /><br /> DF = Fraction Digits<br /><br /> WS = White Space normalization|  
|**kind_desc**|**nvarchar (60)**|Description of kind of facet:<br /><br /> LENGTH<br /><br /> MINIMUM_LENGTH<br /><br /> MAXIMUM_LENGTH<br /><br /> PATTERN<br /><br /> ENUMERATION<br /><br /> MINIMUM_INCLUSIVE_VALUE<br /><br /> MAXIMUM_INCLUSIVE_VALUE<br /><br /> MINIMUM_EXCLUSIVE_VALUE<br /><br /> MAXIMUM_EXCLUSIVE_VALUE<br /><br /> TOTAL_DIGITS<br /><br /> FRACTION_DIGITS<br /><br /> WHITESPACE_NORMALIZATION|  
|**is_fixed**|**bit**|1 = Facet has a fixed, prespecified value.<br /><br /> 0 = No fixed value. (default)|  
|**value**|**nvarchar (4000)**|Fixed, pre-specified value of the facet.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [XML Schemas &#40;XML Type System&#41; Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/xml-schemas-xml-type-system-catalog-views-transact-sql.md)  
  
  
