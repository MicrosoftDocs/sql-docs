---
title: "Rename Views | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "table-view-index, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
helpviewer_keywords: 
  - "views [SQL Server], renaming"
  - "renaming views"
ms.assetid: 5eed0488-81d2-40e8-8fdf-b0a640a591d0
author: stevestein
ms.author: sstein
ms.manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Rename Views
[!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-all-md.md)]
  You can rename a view in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
> [!WARNING]  
>  If you rename a view, code and applications that depend on the view may fail. These include other views, queries, stored procedures, user-defined functions, and client applications. Note that these failures will cascade.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Prerequisites](#Prerequisites)  
  
     [Security](#Security)  
  
-   **To rename a view, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
-   **Follow Up:**  [After renaming a view](#FollowUp)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Prerequisites"></a> Prerequisites  
 Obtain a list of all dependencies on the view. Any objects, scripts or applications that reference the view must be modified to reflect the new name of the view. For more information, see [Get Information About a View](../../relational-databases/views/get-information-about-a-view.md). We recommend that you drop the view and recreate it with a new name instead of renaming the view. By recreating the view, you update the dependency information for the objects that are referenced in the view.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on SCHEMA or CONTROL permission on OBJECT is required, and CREATE VIEW permission in the database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To rename a view  
  
1.  In **Object Explorer**, expand the database that contains the view you wish to rename and then expand the **View** folder.  
  
2.  Right-click the view you wish to rename and select **Rename**.  
  
3.  Enter the view's new name.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To rename a view**  
  
 While you can use **sp_rename** to change the name of the view, we recommend that you delete the existing view and then re-create it with the new name.  
  
 For more information, see [CREATE VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/create-view-transact-sql.md) and [DROP VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/drop-view-transact-sql.md).  
  
##  <a name="FollowUp"></a> Follow Up: After Renaming a View  
 Ensure that all objects, scripts, and applications that reference the view's old name now use the new name.  
  
  
