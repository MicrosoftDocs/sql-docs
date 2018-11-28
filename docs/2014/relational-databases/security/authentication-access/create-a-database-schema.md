---
title: "Create a Database Schema | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.schemas.general.f1"
helpviewer_keywords: 
  - "creating schemas with Management Studio"
  - "CREATE SCHEMA [Management Studio]"
  - "database schemas"
  - "schemas [SQL Server], creating"
ms.assetid: ed2a5522-f4d2-4111-95a4-d3e1e5081739
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Create a Database Schema
  This topic describes how to create a schema in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To create a schema, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The new schema is owned by one of the following database-level principals: database user, database role, or application role. Objects created within a schema are owned by the owner of the schema, and have a NULL **principal_id** in **sys.objects**. Ownership of schema-contained objects can be transferred to any database-level principal, but the schema owner always retains CONTROL permission on objects within the schema.  
  
-   When creating a database object, if you specify a valid domain principal (user or group) as the object owner, the domain principal will be added to the database as a schema. The new schema will be owned by that domain principal.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
  
-   Requires CREATE SCHEMA permission on the database.  
  
-   To specify another user as the owner of the schema being created, the caller must have IMPERSONATE permission on that user. If a database role is specified as the owner, the caller must have one of the following: membership in the role or ALTER permission on the role.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
##### To create a schema  
  
1.  In Object Explorer, expand the **Databases** folder.  
  
2.  Expand the database in which to create the new database schema.  
  
3.  Right-click the **Security** folder, point to **New**, and select **Schema**.  
  
4.  In the **Schema - New** dialog box, on the **General** page, enter a name for the new schema in the **Schema name** box.  
  
5.  In the **Schema owner** box, enter the name of a database user or role to own the schema. Alternately, click **Search** to open the **Search Roles and Users** dialog box.  
  
6.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
### Additional Options  
 The **Schema- New** dialog box also offers options on two additional pages: **Permissions** and **Extended Properties**.  
  
-   The **Permissions** page lists all possible securables and the permissions on those securables that can be granted to the login.  
  
-   The **Extended properties** page allows you to add custom properties to database users.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To create a schema  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    -- Creates the schema Sprockets owned by Annik that contains table NineProngs.   
    -- The statement grants SELECT to Mandar and denies SELECT to Prasanna.  
  
    CREATE SCHEMA Sprockets AUTHORIZATION Annik  
        CREATE TABLE NineProngs (source int, cost int, partnumber int)  
        GRANT SELECT ON SCHEMA::Sprockets TO Mandar  
        DENY SELECT ON SCHEMA::Sprockets TO Prasanna;  
    GO  
    ```  
  
 For more information, see [CREATE SCHEMA &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-schema-transact-sql).  
  
  
