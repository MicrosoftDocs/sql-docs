---
title: "Rename Views"
description: "Rename Views"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 06/16/2023
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
helpviewer_keywords:
  - "views [SQL Server], renaming"
  - "renaming views"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Rename Views
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-pdw-md](../../includes/appliesto-ss-asdb-xxxx-pdw-md.md)]

  You can rename a view in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
> [!WARNING]  
> If you rename a view, code and applications that depend on the view may fail. These include other views, queries, stored procedures, user-defined functions, and client applications. Note that these failures will cascade.  

## <a id="Prerequisites"></a> Prerequisites

 1. Obtain a list of all dependencies on the view. Any objects, scripts, or applications that reference the view must be modified to reflect the new name of the view. For more information, see [Get Information About a View](../../relational-databases/views/get-information-about-a-view.md). 
 1. We recommend that you drop the view and recreate it with a new name instead of renaming the view. By recreating the view, you update the dependency information for the objects that are referenced in the view.
     1. Dropping and re-creating a view will remove any granular SQL permissions that had been assigned to the view. If any permissions had been assigned specifically, you will need to GRANT the permissions to the view again. To retrieve the granular permissions granted on an object before it is dropped, see the examples in [sys.database_permissions (Transact-SQL)](../system-catalog-views/sys-database-permissions-transact-sql.md). To grant permissions, see [Grant a Permission to a Principal](../security/authentication-access/grant-a-permission-to-a-principal.md).
  

## <a id="Permissions"></a> Permissions

 Requires ALTER permission on SCHEMA or CONTROL permission on OBJECT is required, and CREATE VIEW permission in the database.  
  
## <a id="SSMSProcedure"></a> Use SQL Server Management Studio
  
#### Rename a view
  
1. In **Object Explorer**, expand the database that contains the view you wish to rename and then expand the **View** folder.  
  
1. Right-click the view you wish to rename and select **Rename**.  
  
1. Enter the view's new name.  

## <a id="TsqlProcedure"></a> Use Transact-SQL
  
 While you can use `sp_rename` to change the name of the view, we recommend that you delete the existing view and then re-create it with the new name.

This is because renaming a stored procedure, function, view, or trigger doesn't change the name of the corresponding object in the definition column of the [sys.sql_modules](../system-catalog-views/sys-sql-modules-transact-sql.md) catalog view, leading to future confusion. Therefore, we recommend that `sp_rename` not be used to rename these object types. Instead, drop and re-create the object with its new name.
  
 For example:

 ```sql
 DROP VIEW [dbo].[vOrders];
 GO

 CREATE VIEW [dbo].[vOrders]
 AS 
 <select_statement>
 GO
 ```

 For more information, see [CREATE VIEW (Transact-SQL)](../../t-sql/statements/create-view-transact-sql.md) and [DROP VIEW (Transact-SQL)](../../t-sql/statements/drop-view-transact-sql.md).
  
## <a id="FollowUp"></a> Follow up: after renaming a view

 1. Ensure that all objects, scripts, and applications that reference the view's old name now use the new name.
 1. Dropping and re-creating a view will remove any granular SQL permissions that had been assigned to the view. If any permissions had been assigned specifically, you will need to GRANT the permissions to the view again. To retrieve the granular permissions granted on an object before it is dropped, see the examples in [sys.database_permissions (Transact-SQL)](../system-catalog-views/sys-database-permissions-transact-sql.md). To grant permissions, see [Grant a Permission to a Principal](../security/authentication-access/grant-a-permission-to-a-principal.md).