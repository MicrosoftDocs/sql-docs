---
title: "Create a Database Schema | Microsoft Docs"
ms.custom: ""
ms.date: "07/05/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.schemas.general.f1"
helpviewer_keywords: 
  - "creating schemas with Management Studio"
  - "CREATE SCHEMA [Management Studio]"
  - "database schemas"
  - "schemas [SQL Server], creating"
ms.assetid: ed2a5522-f4d2-4111-95a4-d3e1e5081739
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create a Database Schema
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  This topic describes how to create a schema in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)].  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The new schema is owned by one of the following database-level principals: database user, database role, or application role. Objects created within a schema are owned by the owner of the schema, and have a NULL **principal_id** in **sys.objects**. Ownership of schema-contained objects can be transferred to any database-level principal, but the schema owner always retains CONTROL permission on objects within the schema.  
  
-   When creating a database object, if you specify a valid domain principal (user or group) as the object owner, the domain principal is added to the database as a schema. The new schema is owned by that domain principal.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
  
-   Requires CREATE SCHEMA permission on the database.  
  
-   To specify another user as the owner of the schema being created, the caller must have IMPERSONATE permission on that user. If a database role is specified as the owner, the caller must meet one of the following criteria: membership in the role or ALTER permission on the role.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
##### To create a schema  
  
1.  In Object Explorer, expand the **Databases** folder.  
  
2.  Expand the database in which to create the new database schema.  
  
3.  Right-click the **Security** folder, point to **New**, and select **Schema**.  
  
4.  In the **Schema - New** dialog box, on the **General** page, enter a name for the new schema in the **Schema name** box.  
  
5.  In the **Schema owner** box, enter the name of a database user or role to own the schema. Alternately, click **Search** to open the **Search Roles and Users** dialog box.  
  
6.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  

> [!NOTE]
> A dialog box will not appear if you are creating a Schema using SSMS against an **Azure SQL Database** or an **Azure SQL Data Warehouse**. You will need to run the Create Schema Template T-SQL Statement that is generated.
  
### Additional Options  
 The **Schema- New** dialog box also offers options on two additional pages: **Permissions** and **Extended Properties**.  
  
-   The **Permissions** page lists all possible securables and the permissions on those securables that can be granted to the login.  
  
-   The **Extended properties** page allows you to add custom properties to database users.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To create a schema  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  The following example creates a schema named `Chains`, and then creates a table named `Sizes`.  
    ```sql  
    CREATE SCHEMA Chains;
    GO
    CREATE TABLE Chains.Sizes (ChainID int, width dec(10,2));
    ```

4.  Additional options can be performed in a single statement. The following example creates the schema `Sprockets` owned by Annik that contains table `NineProngs`. The statement grants `SELECT` to Mandar and denies `SELECT` to Prasanna.  

    ```sql  
    CREATE SCHEMA Sprockets AUTHORIZATION Annik  
        CREATE TABLE NineProngs (source int, cost int, partnumber int)  
        GRANT SELECT ON SCHEMA::Sprockets TO Mandar  
        DENY SELECT ON SCHEMA::Sprockets TO Prasanna;  
    GO  
    ```  
5. Execute the following statement, to view the schemas in this database:

   ```sql
   SELECT * FROM sys.schemas;
   ```

 For more information, see [CREATE SCHEMA &#40;Transact-SQL&#41;](../../../t-sql/statements/create-schema-transact-sql.md).  
  
  
