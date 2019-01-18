---
title: "ALTER SEARCH PROPERTY LIST (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/10/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_SEARCH_PROPERTY_TSQL"
  - "ALTER_SEARCH_PROPERTY_LIST_TSQL"
  - "ALTER SEARCH PROPERTY LIST"
  - "ALTER SEARCH PROPERTY"
  - "ALTER_SEARCH_TSQL"
  - "ALTER SEARCH"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "full-text search [SQL Server], search property lists"
  - "search property lists [SQL Server], altering"
  - "ALTER SEARCH PROPERTY LIST statement"
ms.assetid: 0436e4a8-ca26-4d23-93f1-e31e2a1c8bfb
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# ALTER SEARCH PROPERTY LIST (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Adds a specified search property to, or drops it from the specified search property list.  
  
## Syntax  
  
```  
ALTER SEARCH PROPERTY LIST list_name  
{  
   ADD 'property_name'  
     WITH   
      (   
          PROPERTY_SET_GUID = 'property_set_guid'  
        , PROPERTY_INT_ID = property_int_id  
      [ , PROPERTY_DESCRIPTION = 'property_description' ]  
      )  
 | DROP 'property_name'   
}  
;  
```  
  
## Arguments  
 *list_name*  
 Is the name of the property list being altered. *list_name* is an identifier.  
  
 To view the names of the existing property lists, use the [sys.registered_search_property_lists](../../relational-databases/system-catalog-views/sys-registered-search-property-lists-transact-sql.md) catalog view, as follows:  
  
```  
SELECT name FROM sys.registered_search_property_lists;  
```  
  
 ADD  
 Adds a specified search property to the property list specified by *list_name*. The property is registered for the search property list . Before newly added properties can be used for property searching, the associated full-text index or indexes must be repopulated. For more information, see [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-index-transact-sql.md).  
  
> [!NOTE]  
>  To add a given search property to a search property list, you must provide its property-set GUID (*property_set_guid*) and property int ID (*property_int_id*). For more information, see "Obtaining Property Set GUIDS and Identifiers," later in this topic.  
  
 *property_name*  
 Specifies the name to be used to identify the property in full-text queries. *property_name* must uniquely identify the property within the property set. A property name can contain internal spaces. The maximum length of *property_name* is 256 characters. This name can be a user-friendly name, such as Author or Home Address, or it can be the Windows canonical name of the property, such as **System.Author** or **System.Contact.HomeAddress**.  
  
 Developers will need to use the value you specify for *property_name* to identify the property in the [CONTAINS](../../t-sql/queries/contains-transact-sql.md) predicate. Therefore, when adding a property it is important to specify a value that meaningfully represents the property defined by the specified property set GUID (*property_set_guid*) and property identifier (*property_int_id*). For more information about property names, see "Remarks," later in this topic.  
  
 To view the names of properties that currently exist in a search property list of the current database, use the [sys.registered_search_properties](../../relational-databases/system-catalog-views/sys-registered-search-properties-transact-sql.md) catalog view, as follows:  
  
```  
SELECT property_name FROM sys.registered_search_properties;  
```  
  
 PROPERTY_SET_GUID ='*property_set_guid*'  
 Specifies the identifier of the property set to which the property belongs. This is a globally unique identifier (GUID). For information about obtaining this value, see "Remarks," later in this topic.  
  
 To view the property set GUID of any property that exists in a search property list of the current database, use the [sys.registered_search_properties](../../relational-databases/system-catalog-views/sys-registered-search-properties-transact-sql.md) catalog view, as follows:  
  
```  
SELECT property_set_guid FROM sys.registered_search_properties;  
```  
  
 PROPERTY_INT_ID =*property_int_id*  
 Specifies the integer that identifies the property within its property set. For information about obtaining this value, see "Remarks."  
  
 To view the integer identifier of any property that exists in a search property list of the current database, use the [sys.registered_search_properties](../../relational-databases/system-catalog-views/sys-registered-search-properties-transact-sql.md) catalog view, as follows:  
  
```  
SELECT property_int_id FROM sys.registered_search_properties;  
```  
  
> [!NOTE]  
>  A given combination of *property_set_guid* and *property_int_id* must be unique in a search property list. If you try to add an existing combination, the ALTER SEARCH PROPERTY LIST operation fails and issues an error. This means that you can define only one name for a given property.  
  
 PROPERTY_DESCRIPTION ='*property_description*'  
 Specifies a user-defined description of the property. *property_description* is a string of up to 512 characters. This option is optional.  
  
 DROP  
 Drops the specified property from the property list specified by *list_name*. Dropping a property unregisters it, so it is no longer searchable.  
  
## Remarks  
 Each full-text index can have only one search property list.  
  
 To enable querying on a given search property, you must add it to the search property list of the full-text index and then repopulate the index.  
  
 When specifying a property you can arrange the PROPERTY_SET_GUID, PROPERTY_INT_ID, and PROPERTY_DESCRIPTION clauses in any order, as a comma-separated list within parentheses, for example:  
  
```  
ALTER SEARCH PROPERTY LIST CVitaProperties  
ADD 'System.Author'   
WITH (   
      PROPERTY_DESCRIPTION = 'Author or authors of a given document.',  
      PROPERTY_SET_GUID   = 'F29F85E0-4FF9-1068-AB91-08002B27B3D9',   
      PROPERTY_INT_ID = 4   
      );  
```  
  
> [!NOTE]  
>  This example uses the property name, `System.Author`, which is similar to the concept of canonical property names introduced in Windows Vista (Windows canonical name).  
  
## Obtaining Property Values  
 Full-text search maps a search property to a full-text index by using its property set GUID and property integer ID. For information about how to obtain these for properties that have been defined by Microsoft, see [Find Property Set GUIDs and Property Integer IDs for Search Properties](../../relational-databases/search/find-property-set-guids-and-property-integer-ids-for-search-properties.md). For information about properties defined by an independent software vendor (ISV), see the documentation of that vendor.  
  
## Making Added Properties Searchable  
 Adding a search property to a search property list registers the property. A newly added property can be immediately specified in [CONTAINS](../../t-sql/queries/contains-transact-sql.md) queries. However, property-scoped full-text queries on a newly added property will not return documents until the associated full-text index is repopulated. For example, the following property-scoped query on a newly added property, *new_search_property*, will not return any documents until the full-text index associated with the target table (*table_name*) is repopulated:  
  
```  
SELECT column_name  
FROM table_name  
WHERE CONTAINS( PROPERTY( column_name, 'new_search_property' ), 
               'contains_search_condition');  
GO   
```  
  
 To start a full population, use the following [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-index-transact-sql.md) statement:  
  
```  
USE database_name;  
GO  
ALTER FULLTEXT INDEX ON table_name START FULL POPULATION;  
GO  
```  
  
> [!NOTE]  
>  Repopulation is not needed after a property is dropped from a property list, because only the properties that remain in the search property list are available for full-text querying.  
  
## Related References  
 **To create a property list**  
  
-   [CREATE SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](../../t-sql/statements/create-search-property-list-transact-sql.md)  
  
 **To drop a property list**  
  
-   [DROP SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](../../t-sql/statements/drop-search-property-list-transact-sql.md)  
  
 **To add or remove a property list on a full-text index**  
  
-   [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-index-transact-sql.md)  
  
 **To run a population on a full-text index**  
  
-   [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-index-transact-sql.md)  
  
##  <a name="Permissions"></a> Permissions  
 Requires CONTROL permission on the property list.  
  
## Examples  
  
### A. Adding a property  
 The following example adds several properties-`Title`, `Author`, and `Tags`-to a property list named `DocumentPropertyList`.  
  
> [!NOTE]  
>  For an example that creates `DocumentPropertyList` property list, see [CREATE SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](../../t-sql/statements/create-search-property-list-transact-sql.md).  
  
```  
ALTER SEARCH PROPERTY LIST DocumentPropertyList  
   ADD 'Title'   
   WITH ( PROPERTY_SET_GUID = 'F29F85E0-4FF9-1068-AB91-08002B27B3D9', PROPERTY_INT_ID = 2,   
      PROPERTY_DESCRIPTION = 'System.Title - Title of the item.' );  
  
ALTER SEARCH PROPERTY LIST DocumentPropertyList   
    ADD 'Author'  
   WITH ( PROPERTY_SET_GUID = 'F29F85E0-4FF9-1068-AB91-08002B27B3D9', PROPERTY_INT_ID = 4,   
      PROPERTY_DESCRIPTION = 'System.Author - Author or authors of the item.' );  
  
ALTER SEARCH PROPERTY LIST DocumentPropertyList   
    ADD 'Tags'  
   WITH ( PROPERTY_SET_GUID = 'F29F85E0-4FF9-1068-AB91-08002B27B3D9', PROPERTY_INT_ID = 5,   
      PROPERTY_DESCRIPTION = 
          'System.Keywords - Set of keywords (also known as tags) assigned to the item.' );  
```  
  
> [!NOTE]  
>  You must associate a given search property list with a full-text index before using it for property-scoped queries. To do so, use an [ALTER FULLTEXT INDEX](../../t-sql/statements/alter-fulltext-index-transact-sql.md) statement and specify the SET SEARCH PROPERTY LIST clause.  
  
### B. Dropping a property  
 The following example drops the `Comments` property from the `DocumentPropertyList` property list.  
  
```  
ALTER SEARCH PROPERTY LIST DocumentPropertyList  
DROP 'Comments' ;  
```  
  
## See Also  
 [CREATE SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](../../t-sql/statements/create-search-property-list-transact-sql.md)   
 [DROP SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](../../t-sql/statements/drop-search-property-list-transact-sql.md)   
 [sys.registered_search_properties &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-registered-search-properties-transact-sql.md)   
 [sys.registered_search_property_lists &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-registered-search-property-lists-transact-sql.md)   
 [sys.dm_fts_index_keywords_by_property &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-by-property-transact-sql.md)   
 [Search Document Properties with Search Property Lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md)   
 [Find Property Set GUIDs and Property Integer IDs for Search Properties](../../relational-databases/search/find-property-set-guids-and-property-integer-ids-for-search-properties.md)  
  
  
