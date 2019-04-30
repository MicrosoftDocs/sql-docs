---
title: "sys.xml_schema_components (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "xml_schema_components"
  - "sys.xml_schema_components_TSQL"
  - "sys.xml_schema_components"
  - "xml_schema_components_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.xml_schema_components catalog view"
ms.assetid: 70142d3a-f8b5-4ee2-8287-3935f0f67aa2
author: pmasl
ms.author: pelopes
ms.reviewer: mikeray
manager: craigg
---
# sys.xml_schema_components (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a row per component of an XML schema. The pair (**collection_id**, **namespace_id**) is a compound foreign key to the containing namespace. For named components, the values for **symbol_space**, **name**, **scoping_xml_component_id**, **is_qualified**, **xml_namespace_id**, **xml_collection_id** are unique.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**xml_component_id**|**int**|Unique ID of the XML schema component in the database.|  
|**xml_collection_id**|**int**|ID of the XML schema collection that contains the namespace of this component.|  
|**xml_namespace_id**|**int**|ID of the XML namespace within the collection.|  
|**is_qualified**|**bit**|1 = This component has an explicit namespace qualifier.<br /><br /> 0 = This is a locally scoped component. In this case, the pair, **namespace_id**, **collection_id**, refers to the "no namespace" **targetNamespace**.<br /><br /> For wildcard components this value will be equal to 1.|  
|**name**|**nvarchar**<br /><br /> **(4000)**|Unique name of the XML schema component. Is NULL if the component is unnamed.|  
|**symbol_space**|**char(1)**|Space in which this symbol name is unique, based on **kind**:<br /><br /> N = None<br /><br /> T = Type<br /><br /> E = Element<br /><br /> M = Model-Group<br /><br /> A = Attribute<br /><br /> G = Attribute-Group|  
|**symbol_space_desc**|**nvarchar**<br /><br /> **(60)**|Description of space in which this symbol name is unique, based on **kind**:<br /><br /> NONE<br /><br /> TYPE<br /><br /> ELEMENT<br /><br /> MODEL_GROUP<br /><br /> ATTRIBUTE<br /><br /> ATTRIBUTE_GROUP|  
|**kind**|**char(1)**|Kind of XML schema component.<br /><br /> N = Any Type (special intrinsic component)<br /><br /> Z = Any Simple Type (special intrinsic component)<br /><br /> P = Primitive Type (intrinsic types)<br /><br /> S = Simple Type<br /><br /> L = List Type<br /><br /> U = Union Type<br /><br /> C = Complex Simple Type (derived from Simple)<br /><br /> K = Complex Type<br /><br /> E = Element<br /><br /> M = Model-Group<br /><br /> W = Element-Wildcard<br /><br /> A = Attribute<br /><br /> G = Attribute-Group<br /><br /> V = Attribute-Wildcard|  
|**kind_desc**|**nvarchar**<br /><br /> **(60)**|Description of the kind of XML schema component:<br /><br /> ANY_TYPE<br /><br /> ANY_SIMPLE_TYPE<br /><br /> PRIMITIVE_TYPE<br /><br /> SIMPLE_TYPE<br /><br /> LIST_TYPE<br /><br /> UNION_TYPE<br /><br /> COMPLEX_SIMPLE_TYPE<br /><br /> COMPLEX_TYPE<br /><br /> ELEMENT<br /><br /> MODEL_GROUP<br /><br /> ELEMENT_WILDCARD<br /><br /> ATTRIBUTE<br /><br /> ATTRIBUTE_GROUP<br /><br /> ATTRIBUTE_WILDCARD|  
|**derivation**|**char(1)**|Derivation method for derived types:<br /><br /> N = None (not derived)<br /><br /> X = Extension<br /><br /> R = Restriction<br /><br /> S = Substitution|  
|**derivation_desc**|**nvarchar**<br /><br /> **(60)**|Description of derivation method for derived types:<br /><br /> NONE<br /><br /> EXTENSION<br /><br /> RESTRICTION<br /><br /> SUBSTITUTION|  
|**base_xml_component_id**|**int**|ID of the component from which this component is derived. NULL if there is none.|  
|**scoping_xml_component_id**|**int**|Unique ID of the scoping component. NULL if there is none (global scope).|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [XML Schemas &#40;XML Type System&#41; Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/xml-schemas-xml-type-system-catalog-views-transact-sql.md)  
  
  
