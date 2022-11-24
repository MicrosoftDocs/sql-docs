---
title: "CREATE FULLTEXT CATALOG (Transact-SQL)"
description: CREATE FULLTEXT CATALOG (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "05/24/2022"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CATALOG_TSQL"
helpviewer_keywords:
  - "full-text catalogs [SQL Server], creating"
  - "CREATE FULLTEXT CATALOG statement"
dev_langs:
  - "TSQL"
---

# CREATE FULLTEXT CATALOG (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Creates a full-text catalog for a database. One full-text catalog can have several full-text indexes, but a full-text index can only be part of one full-text catalog. Each database can contain zero or more full-text catalogs.  
 
You cannot create full-text catalogs in the **master**, **model**, or **tempdb** databases.  
  
> [!IMPORTANT]  
>  Beginning with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], a full-text catalog is a virtual object and does not belong to any filegroup. A full-text catalog is a logical concept that refers to a group of full-text indexes.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
  
CREATE FULLTEXT CATALOG catalog_name  
     [ON FILEGROUP filegroup ]  
     [IN PATH 'rootpath']  
     [WITH <catalog_option>]  
     [AS DEFAULT]  
     [AUTHORIZATION owner_name ]  
  
<catalog_option>::=  
     ACCENT_SENSITIVITY = {ON|OFF}  
  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

*catalog_name*  

Is the name of the new catalog. The catalog name must be unique among all catalog names in the current database. Also, the name of the file that corresponds to the full-text catalog (see ON FILEGROUP) must be unique among all files in the database. If the name of the catalog is already used for another catalog in the database, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns an error.  
  
The length of the catalog name cannot exceed 120 characters.  
  
ON FILEGROUP *filegroup*  
Beginning with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], this clause has no effect.  
  
IN PATH **'**_rootpath_**'**

> [!NOTE]  
> [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  
Beginning with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], this clause has no effect.  
  
ACCENT_SENSITIVITY = {ON|OFF}  
Specifies that the catalog is accent sensitive or accent insensitive for full-text indexing. When this property is changed, the index must be rebuilt. The default is to use the accent-sensitivity specified in the database collation. To display the database collation, use the **sys.databases** catalog view.  
  
To determine the current accent-sensitivity property setting of a full-text catalog, use the FULLTEXTCATALOGPROPERTY function with the **accentsensitivity** property value against *catalog_name*. If the value returned is '1', the full-text catalog is accent sensitive; if the value is '0', the catalog is not accent-sensitive.  
  
AS DEFAULT  
Specifies that the catalog is the default catalog. When full-text indexes are created without a full-text catalog explicitly specified, the default catalog is used. If an existing full-text catalog is already marked AS DEFAULT, setting this new catalog AS DEFAULT will make this catalog the default full-text catalog.  
  
AUTHORIZATION *owner_name*  
Sets the owner of the full-text catalog to the name of a database user or role. If *owner_name* is a role, the role must be the name of a role that the current user is a member of, or the user running the statement must be the database owner or system administrator.
  
If *owner_name* is a user name, the user name must be one of the following:
  
- The name of the user running the statement.
  
- The name of a user that the user executing the command has impersonate permissions for.
  
- Or, the user executing the command must be the database owner or system administrator.
  
*owner_name* must also be granted TAKE OWNERSHIP permission on the specified full-text catalog.
  
## Remarks  

Full-text catalog IDs start at 00005 and are incremented by one for each new catalog created.
  
## Permissions  

User must have CREATE FULLTEXT CATALOG permission on the database, or be a member of the **db_owner**, or **db_ddladmin** fixed database roles.
  
## Examples  

The following example creates a full-text catalog and also a full-text index.
  
```sql  
USE AdventureWorks2012;  
GO  
CREATE FULLTEXT CATALOG ftCatalog AS DEFAULT;  
GO  
CREATE FULLTEXT INDEX ON HumanResources.JobCandidate(Resume) KEY INDEX PK_JobCandidate_JobCandidateID;  
GO  
```  
  
## See Also

- [sys.fulltext_catalogs &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-catalogs-transact-sql.md)   
- [ALTER FULLTEXT CATALOG &#40;Transact-SQL&#41;](../../t-sql/statements/alter-fulltext-catalog-transact-sql.md)   
- [DROP FULLTEXT CATALOG &#40;Transact-SQL&#41;](../../t-sql/statements/drop-fulltext-catalog-transact-sql.md)   
- [Full-Text Search](../../relational-databases/search/full-text-search.md)   
 
  
  
