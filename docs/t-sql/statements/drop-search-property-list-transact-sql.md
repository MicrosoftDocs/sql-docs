---
title: "DROP SEARCH PROPERTY LIST (Transact-SQL)"
description: DROP SEARCH PROPERTY LIST (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP_SEARCH_PROPERTY_LIST_TSQL"
  - "DROP SEARCH PROPERTY LIST"
helpviewer_keywords:
  - "full-text search [SQL Server], search property lists"
  - "DROP SEARCH PROPERTY LIST statement"
  - "search property lists [SQL Server], dropping"
  - "search property lists [SQL Server], deleting"
dev_langs:
  - "TSQL"
---
# DROP SEARCH PROPERTY LIST (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Drops a property list from the current database if the search property list is currently not associated with any full-text index in the database.  
  
## Syntax  
  
```syntaxsql
DROP SEARCH PROPERTY LIST property_list_name  
;  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *property_list_name*  
 Is the name of the search property list to be dropped. *property_list_name* is an identifier.  
  
 To view the names of the existing property lists, use the [sys.registered_search_property_lists](../../relational-databases/system-catalog-views/sys-registered-search-property-lists-transact-sql.md) catalog view, as follows:  
  
```sql  
SELECT name FROM sys.registered_search_property_lists;  
```  
  
## Remarks  
 You cannot drop a search property list from a database while the list is associated with any full-text index, and attempts to do so fail. To drop a search property list from a given full-text index, use the [ALTER FULLTEXT INDEX](../../t-sql/statements/alter-fulltext-index-transact-sql.md) statement, and specify the SET SEARCH PROPERTY LIST clause with either OFF or the name of another search property list.  
  
 **To view the property lists on a server instance**  
  
-   [sys.registered_search_property_lists &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-registered-search-property-lists-transact-sql.md)  
  
 **To view the property lists associated with full-text indexes**  
  
-   [sys.fulltext_indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-indexes-transact-sql.md)  
  
 **To remove a property list from a full-text index**  
  
-   [ALTER FULLTEXT INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-index-transact-sql.md)  
  
##  <a name="Permissions"></a> Permissions  
 Requires CONTROL permission on the search property list.  
  
> [!NOTE]  
>  The property list owner can grant CONTROL permissions on the list. By default, the user who creates a search property list is its owner. The owner can be changed by using the [ALTER AUTHORIZATION](../../t-sql/statements/alter-authorization-transact-sql.md)[!INCLUDE[tsql](../../includes/tsql-md.md)] statement.  
  
## Examples  
 The following example drops the `JobCandidateProperties` property list from the `AdventureWorks2012` database.  
  
```sql  
DROP SEARCH PROPERTY LIST JobCandidateProperties;  
GO  
```  
  
## See Also  
 [ALTER SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](../../t-sql/statements/alter-search-property-list-transact-sql.md)   
 [CREATE SEARCH PROPERTY LIST &#40;Transact-SQL&#41;](../../t-sql/statements/create-search-property-list-transact-sql.md)   
 [Search Document Properties with Search Property Lists](../../relational-databases/search/search-document-properties-with-search-property-lists.md)   
 [sys.registered_search_properties &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-registered-search-properties-transact-sql.md)   
 [sys.registered_search_property_lists &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-registered-search-property-lists-transact-sql.md)   
 [sys.registered_search_property_lists &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-registered-search-property-lists-transact-sql.md)  
  
  
