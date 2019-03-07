---
title: "MSSQLSERVER_208 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "208 (Database Engine error)"
ms.assetid: 4b1895f5-3197-4da1-af86-954c93507956
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_208
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|208|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SQ_BADOBJECT|  
|Message Text|Invalid object name '%.*ls'.|  
  
## Explanation  
The specified object cannot be found.  
  
### Possible Causes  
This error can be caused by one of the following problems:  
  
-   The object is not specified correctly.  
  
-   The object does not exist in the current database or in the specified database.  
  
-   The object exists, but could not be exposed to the user. For example, the user might not have permissions on the object or the object is created within an EXECUTE statement but accessed outside the scope of the EXECUTE statement.  
  
## User Action  
Verify the following information and correct the statement as appropriate.  
  
-   The object name is spelled correctly.  
  
-   The current database context is correct. If a database name for the object is not specified, the object must exist in the current database. For more information about setting the database context, see [USE &#40;Transact-SQL&#41;](~/t-sql/language-elements/use-transact-sql.md).  
  
-   The object exists in the system tables. To verify whether a table or other schema-scoped object exists, query the **sys.objects** catalog view. If the object is not in the system tables, the object has been deleted, or the user does not have permissions to view the object metadata. For more information about permissions to view object metadata, see [Metadata Visibility Configuration](~/relational-databases/security/metadata-visibility-configuration.md).  
  
-   The object is contained in the default schema of the user. If it is not, the object must be specified using the two-part format *schema_name.object_name*. Note that scalar-valued functions must always be invoked by using at least a two-part name.  
  
-   The case sensitivity of the database collation.  
  
    When a database uses a case-sensitive collation, the object name must match the case of the object in the database. For example, when an object is specified as **MyTable** in a database with a case sensitive collation, queries that refer to the object as **mytable** or **Mytable** will cause error 208 to return because the object names do not match.  
  
    You can verify the database collation by running the following statement.  
  
    ```  
    SELECT collation_name FROM sys.databases WHERE name = 'database_name';  
    ```  
  
    The abbreviation CS in the collation name indicates the collation is case sensitive. For example, Latin1_General_CS_AS is a case sensitive, accent sensitive collation. CI indicates a case insensitive collation.  
  
-   The user has permission to access the object. To verify the permissions the user has on the object, use the **Has_Perms_By_Name** system function.  
  
## See Also  
[USE &#40;Transact-SQL&#41;](~/t-sql/language-elements/use-transact-sql.md)  
[Metadata Visibility Configuration](~/relational-databases/security/metadata-visibility-configuration.md)  
[HAS_PERMS_BY_NAME &#40;Transact-SQL&#41;](~/t-sql/functions/has-perms-by-name-transact-sql.md)  
  
