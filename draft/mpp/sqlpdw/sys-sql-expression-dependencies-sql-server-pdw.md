---
title: "sys.sql_expression_dependencies (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: b041d21f-7d56-4d86-a8b0-12a7afd3b32a
caps.latest.revision: 10
author: BarbKess
---
# sys.sql_expression_dependencies (SQL Server PDW)
Contains one row for each by-name dependency on a user-defined entity in the current database. A dependency between two entities is created when one entity (called the reference entity) appears by name in a persisted SQL expression of another entity (called the referencing entity). For example, when a table is referenced in the definition of a view, the view (as the referencing entity) depends on the table (the referenced entity). If the table is dropped, the view is unusable.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|referencing_id|**int**|ID of the referencing entity. Is not nullable.||  
|referencing_minor_id|**int**|Column ID when the referencing entity is a column; otherwise 0. Is not nullable.||  
|referencing_class|**tinyint**|Class of the referencing entity.<br /><br />1 = Object or column|Always 1.|  
|referencing_class_desc|**nvarchar(60)**|Description of the class of referencing entity.|OBJECT_OR_COLUMN|  
|is_schema_bound_reference|**bit**|1 = Referenced entity is schema-bound.<br /><br />0 = Referenced entity is non-schema-bound.||  
|referenced_class|**tinyint**|Class of the referenced entity.<br /><br />1 = Object or column<br /><br />6 = Type<br /><br />10 = XML schema collection<br /><br />21 = Partition function||  
|referenced_class_desc|**nvarchar(60)**|Description of class of referenced entity.<br /><br />OBJECT_OR_COLUMN<br /><br />TYPE<br /><br />XML_SCHEMA_COLLECTION<br /><br />PARTITION_FUNCTION||  
|referenced_server_name|**sysname**||Always NULL.|  
|referenced_database_name|**sysname**|Name of the database of the referenced entity.<br /><br />NULL for non-schema-bound references when specified using a one-part or two-part name.<br /><br />NULL for schema-bound entities because they must be in the same database and therefore can only be defined using a two-part (schema.object) name.||  
|referenced_schema_name|**sysname**|Schema in which the referenced entity belongs.<br /><br />NULL for non-schema-bound references in which the entity was referenced without specifying the schema name.<br /><br />Never NULL for schema-bound references because schema-bound entities must be defined and referenced by using a two-part name.||  
|referenced_entity_name|**sysname**|Name of the referenced entity. Is not nullable.||  
|referenced_id|**int**|ID of the referenced entity.<br /><br />NULL for references within the database if the ID cannot be determined. For non-schema-bound references, the ID cannot be resolved in the following cases:<br /><br />The referenced entity does not exist in the database.<br /><br />The schema of the referenced entity depends on the schema of the caller and is resolved at run time. In this case, is_caller_dependent is set to 1.<br /><br />Never NULL for schema-bound references.||  
|referenced_minor_id|**int**|ID of the referenced column when the referencing entity is a column; otherwise 0. Is not nullable. A referenced entity is a column when a column is identified by name in the referencing entity, or when the parent entity is used in a SELECT * statement.||  
|is_caller_dependent|**bit**||Always 0.|  
|is_ambiguous|**bit**|1 = Reference is ambiguous.<br /><br />0 = Reference is unambiguous or the entity can be successfully bound when the view is called.<br /><br />Always 0 for schema bound references.|Always 0.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
