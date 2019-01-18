---
title: "CREATE SEARCH PROPERTY LIST (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/10/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "CREATE_SEARCH_PROPERTY_LIST_TSQL"
  - "CREATE SEARCH"
  - "CREATE_SEARCH_TSQL"
  - "CREATE_SEARCH_PROPERTY_TSQL"
  - "CREATE SEARCH PROPERTY"
  - "CREATE SEARCH PROPERTY LIST"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "full-text search [SQL Server], search property lists"
  - "search property lists [SQL Server], creating"
  - "CREATE SEARCH PROPERTY LIST statement"
ms.assetid: 5440cbb8-3403-4d27-a2f9-8e1f5a1bc12b
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# CREATE SEARCH PROPERTY LIST (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Creates a new search property list. A search property list is used to specify one or more search properties that you want to include in a full-text index.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
CREATE SEARCH PROPERTY LIST new_list_name  
   [ FROM [ database_name. ] source_list_name ]  
   [ AUTHORIZATION owner_name ]  
;  
```  
  
## Arguments  
 *new_list_name*  
 Is the name of the new search property list. *new_list_name* is an identifier with a maximum of 128 characters. *new_list_name* must be unique among all property lists in the current database, and conform to the rules for identifiers. *new_list_name* will be used when the full-text index is created.  
  
 *database_name*  
 Is the name of the database where the property list specified by *source_list_name* is located. If not specified, *database_name* defaults to the current database.  
  
 *database_name* must specify the name of an existing database. The login for the current connection must be associated with an existing user ID in the database specified by *database_name*. You must also have the required [permissions](#Permissions) on the database.  
  
 *source_list_name*  
 Specifies that the new property list is created by copying an existing property list from *database_name*. If *source_list_name* does not exist, CREATE SEARCH PROPERTY LIST fails with an error. The search properties in *source_list_name* are inherited by *new_list_name*.  
  
 AUTHORIZATION *owner_name*  
 Specifies the name of a user or role to own of the property list. *owner_name* must either be the name of a role of which the current user is a member, or the current user must have IMPERSONATE permission on *owner_name*. If not specified, ownership is given to the current user.  
  
> [!NOTE]  
>  The owner can be changed by using the [ALTER AUTHORIZATION](../../t-sql/statements/alter-authorization-transact-sql.md)[!INCLUDE[tsql](../../includes/tsql-md.md)] statement.  
  
## Remarks  
  
> [!NOTE]  
>  For information about property lists in general, see [Search Document Properties with Search Property Lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md).  
  
 By default, a new search property list is empty and you must alter it to manually to add one or more search properties. Alternatively, you can copy an existing search property list. In this case, the new list inherits the search properties of its source, but you can alter the new list to add or remove search properties. Any properties in the search property list at the time of the next full population are included in the full-text index.  
  
 A CREATE SEARCH PROPERTY LIST statement fails under any of the following conditions:  
  
-   If the database specified by *database_name* does not exist.  
  
-   If the list specified by *source_list_name* does not exist.  
  
-   If you do not have the correct permissions.  
  
 **To add or remove properties from a list**  
  
-   [ALTER SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](../../t-sql/statements/alter-search-property-list-transact-sql.md)  
  
-   **To drop a property list**  
  
-   [DROP SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](../../t-sql/statements/drop-search-property-list-transact-sql.md)  
  
##  <a name="Permissions"></a> Permissions  
 Requires CREATE FULLTEXT CATALOG permissions in the current database and REFERENCES permissions on any database from which you copy a source property list.  
  
> [!NOTE]  
>  REFERENCES permission is required to associate the list with a full-text index. CONTROL permission is required to add and remove properties or drop the list. The property list owner can grant REFERENCES or CONTROL permissions on the list. Users with CONTROL permission can also grant REFERENCES permission to other users.  
  
## Examples  
  
### A. Creating an empty property list and associating it with an index  
 The following example creates a new search property list named `DocumentPropertyList`. The example then uses an [ALTER FULLTEXT INDEX](../../t-sql/statements/alter-fulltext-index-transact-sql.md) statement to associate the new property list with the full-text index of the `Production.Document` table in the `AdventureWorks` database, without starting a population.  
  
> [!NOTE]  
>  For an example that adds several predefined, well-known search properties to this search property list, see [ALTER SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](../../t-sql/statements/alter-search-property-list-transact-sql.md). After adding search properties to the list, the database administrator would need to use another ALTER FULLTEXT INDEX statement with the START FULL POPULATION clause.  
  
```  
CREATE SEARCH PROPERTY LIST DocumentPropertyList;  
GO  
USE AdventureWorks2012;  
ALTER FULLTEXT INDEX ON Production.Document   
   SET SEARCH PROPERTY LIST DocumentPropertyList  
   WITH NO POPULATION;   
GO   
```  
  
### B. Creating a property list from an existing one  
 The following example creates a new the search property list,  `JobCandidateProperties`, from the list created by Example A, `DocumentPropertyList`, which is associated with a full-text index in the `AdventureWorks2012` database. The example then uses an ALTER FULLTEXT INDEX statement to associate the new property list with the full-text index of the `HumanResources.JobCandidate` table in the `AdventureWorks2012` database. This ALTER FULLTEXT INDEX statement starts a full population, which is the default behavior of the SET SEARCH PROPERTY LIST clause.  
  
```  
CREATE SEARCH PROPERTY LIST JobCandidateProperties 
FROM AdventureWorks2012.DocumentPropertyList;  
GO  
ALTER FULLTEXT INDEX ON HumanResources.JobCandidate   
   SET SEARCH PROPERTY LIST JobCandidateProperties;  
GO  
  
```  
  
## See Also  
 [ALTER SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](../../t-sql/statements/alter-search-property-list-transact-sql.md)   
 [DROP SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](../../t-sql/statements/drop-search-property-list-transact-sql.md)   
 [sys.registered_search_properties &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-registered-search-properties-transact-sql.md)   
 [sys.registered_search_property_lists &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-registered-search-property-lists-transact-sql.md)   
 [sys.dm_fts_index_keywords_by_property &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-fts-index-keywords-by-property-transact-sql.md)   
 [Search Document Properties with Search Property Lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md)   
 [Find Property Set GUIDs and Property Integer IDs for Search Properties](../../relational-databases/search/find-property-set-guids-and-property-integer-ids-for-search-properties.md)  
  
  
