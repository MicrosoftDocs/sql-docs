---
title: "MSSQLSERVER_2501 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "2501 (Database Engine error)"
ms.assetid: 895aafe3-a4e7-4ed8-acc5-93be76ef3664
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_2501
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|2501|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC_NO_SUCH_TABLE_NAME|  
|Message Text|Cannot find a table or object with the name 'NAME'. Check the system catalog.|  
  
## Explanation  
The specified object cannot be found.  
  
### Possible Causes  
This error can be caused by one of the following problems:  
  
-   The object is not specified correctly.  
  
-   The object does not exist, or the object was dropped before a statement tried to use it.  
  
-   The object might exist, but could not be exposed to the user. For example, the user might not have permissions on the object, or the object is an internal table that could not be seen by a user.  
  
## User Action  
  
-   Verify the current database context is correct. For more information, see [USE &#40;Transact-SQL&#41;](~/t-sql/language-elements/use-transact-sql.md).  
  
-   Verify that the table or object name is spelled correctly.  
  
-   Verify the schema name that contains the object. If the object belongs to a schema other than the default (**dbo**) schema, you must specify the table or object name by using the two-part format *schema_name.object_name*.  
  
-   Verify that the object exists in the system tables. To verify whether a table or other schema-scoped object exists, query the [sys.objects](~/relational-databases/system-catalog-views/sys-objects-transact-sql.md) catalog view. If the object is not in the system tables, the object has been deleted, or the user does not have permissions to view the object metadata. For more information about how to view object metadata, see [Metadata Visibility Configuration](~/relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
[Catalog Views &#40;Transact-SQL&#41;](~/relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
