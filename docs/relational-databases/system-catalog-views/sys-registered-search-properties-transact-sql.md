---
title: "sys.registered_search_properties (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.registered_search_properties"
  - "registered_search_properties"
  - "sys.registered_search_properties_TSQL"
  - "registered_search_properties_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "full-text search [SQL Server], search property lists"
  - "search properties [SQL Server]"
  - "property searching [SQL Server], viewing registered properties"
  - "search property lists [SQL Server], viewing registered properties"
  - "sys.registered_search_properties catalog view"
ms.assetid: 1b9a7a5c-8c05-4819-83c3-7487dd08fcf7
author: pmasl
ms.author: pelopes
ms.reviewer: mikeray
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.registered_search_properties (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Contains a row for each search property contained by any search property list on the current database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**property_list_id**|**int**|ID of the search property list to which this property belongs.|  
|**property_set_guid**|**uniqueidentifier**|Globally unique identifier GUID that identifies the property set to which the search property belongs.|  
|**property_int_id**|**int**|Integer that identifies this search property within the property set. **property_int_id** is unique within the property set.|  
|**property_name**|**nvarchar(64)**|Name that uniquely identifies this search property in the search property list.<br /><br /> Note: To search on a property, specify this property name in the [CONTAINS](../../t-sql/queries/contains-transact-sql.md) predicate.|  
|**property_description**|**nvarchar(512)**|Description of the property.|  
|**property_id**|**int**|Internal property ID of the search property within the search property list identified by the **property_list_id** value.<br /><br /> When a given property is added to a given search property list, the Full-Text Engine registers the property and assigns it an internal property ID that is specific to that property list. The internal property ID, which is an integer, is unique to a given search property list. If a given property is registered for multiple search property lists, a different internal property ID might be assigned for each search property list.<br /><br /> Note: The internal property ID is distinct from the property integer identifier that is specified when adding the property to the search property list. For more information, see [Search Document Properties with Search Property Lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md).<br /><br /> To view all property-related content in the full-text index: <br />                  [sys.dm_fts_index_keywords_by_property &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-by-property-transact-sql.md)|  
  
## Remarks  
 For more information about search property lists, see [Search Document Properties with Search Property Lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md).  
  
## Permissions  
 Visibility of the metadata for search properties is limited to those that are in search property lists that you either own or on which you have been granted some REFERENCE permission.  
  
> [!NOTE]  
>  The search property list owner can grant REFERENCE or CONTROL permissions on the list. Users with CONTROL permission can also grant REFERENCE permission to other users.  
  
## Examples  
 The following example lists all of the metadata for registered search properties.  
  
```  
USE AdventureWorks2012;  
GO  
SELECT * FROM sys.registered_search_properties;   
GO  
```  
  
## See Also  
 [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-index-transact-sql.md)   
 [sys.fulltext_indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-indexes-transact-sql.md)   
 [Search Document Properties with Search Property Lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md)  
  
  
