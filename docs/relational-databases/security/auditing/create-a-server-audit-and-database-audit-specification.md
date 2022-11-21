---
title: "Create a server audit & database audit specification"
description: Learn to create a SQL Server audit and a database audit specification by using SQL Server Management Studio or Transact-SQL (T-SQL).
ms.custom: seo-lt-2019
ms.date: "03/23/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.sqlaudit.dbaudit.general.f1"
helpviewer_keywords: 
  - "audits [SQL Server], creating database specification"
  - "database audit [SQL Server]"
ms.assetid: 26ee85de-6e97-4318-b526-900924d96e62
author: sravanisaluru
ms.author: srsaluru
---
# Create a server audit and database audit specification
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  This article describes how to create a server audit and a database audit specification in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)].  
  
 Auditing an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] or a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database involves tracking and logging events that occur on the system. The *SQL Server Audit* object collects a single instance of server-level or database-level actions and groups of actions to monitor. The audit is at the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance level. You can have multiple audits per [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance. The *Database-Level Audit Specification* object belongs to an audit. You can create one database audit specification per SQL Server database per audit. For more information, see [SQL Server audit &#40;Database Engine&#41;](../../../relational-databases/security/auditing/sql-server-audit-database-engine.md).  
  
 ##  <a name="BeforeYouBegin"></a> Before you begin  
  
###  <a name="Restrictions"></a> Limitations and restrictions  
 Database audit specifications are non-securable objects that reside in a given database. When a database audit specification is created, it's in a disabled state.  
  
 When you're creating or modifying a database audit specification in a user database, don't include audit actions on server-scope objects, like the system views. If you include server-scoped objects, the audit will be created. But the server-scoped objects won't be included, and no error will return. To audit server-scoped objects, use a database audit specification in the master database.  
  
 Database audit specifications reside in the database where they're created, except for the **TempDB** system database.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
  
-   Users who have the ALTER ANY DATABASE AUDIT permission can create database audit specifications and bind them to any audit.  
  
-   After a database audit specification is created, principals who have CONTROL SERVER or ALTER ANY DATABASE AUDIT permissions can view it. The sysadmin account can also view it.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To create a server audit  
  
1.  In Object Explorer, expand the **Security** folder.  
  
2.  Right-click the **Audits** folder and select **New Audit**. For more information, see [Create a server audit and server audit specification](../../../relational-databases/security/auditing/create-a-server-audit-and-server-audit-specification.md).  
  
3.  When you finish selecting options, select **OK**.  

#### To create a database-level audit specification  
  
1.  In Object Explorer, expand the database where you want to create the audit specification.  
  
2.  Expand the **Security** folder.  
  
3.  Right-click the **Database Audit Specifications** folder and select **New Database Audit Specification**.  
  
     These options are available in the **Create Database Audit Specification** dialog box:  
  
     **Name**  
     The name of the database audit specification. A name is generated automatically when you create a server audit specification. The name is editable.  
  
     **Audit**  
     The name of an existing server audit object. Either type in the name of the audit or select it from the list.  
  
     **Audit Action Type**  
     Specifies the database-level audit action groups and audit actions to capture. For a list of database-level audit action groups and audit actions and descriptions of the events they contain, see [SQL Server audit action groups and actions](../../../relational-databases/security/auditing/sql-server-audit-action-groups-and-actions.md).  
  
     **Object Schema**  
     Displays the schema for the specified **Object Name**.  
  
     **Object Name**  
     The name of the object to audit. This option is available only for audit actions. It doesn't apply to audit groups.  
  
     **Ellipsis (...)**  
     Opens the **Select Objects** dialog box so you can browse for and select an available object, based on the specified **Audit Action Type**.  
  
     **Principal Name**  
     The account to filter the audit by for the object being audited.  
  
     **Ellipsis (...)**  
     Opens the **Select Objects** dialog box so you can browse for and select an available object, based on the specified **Object Name**.  
  
4.  When you finish selecting options, select **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To create a server audit  
  
1.  In Object Explorer, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
2.  On the Standard bar, select **New Query**.  
  
3.  Paste the following example into the query window and then select **Execute**.  
  
    ```  
    USE master ;  
    GO  
    -- Create the server audit.   
    CREATE SERVER AUDIT Payrole_Security_Audit  
        TO FILE ( FILEPATH =   
    'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA' ) ;   
    GO  
    -- Enable the server audit.   
    ALTER SERVER AUDIT Payrole_Security_Audit   
    WITH (STATE = ON) ;  
    ```  
  
#### To create a database-level audit specification  
  
1.  In Object Explorer, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
2.  On the Standard bar, select **New Query**.  
  
3.  Paste the following example into the query window and then select **Execute**. This example creates a database audit specification called `Audit_Pay_Tables`. It audits SELECT and INSERT statements by the `dbo` user for the `HumanResources.EmployeePayHistory` table, based on the server audit defined in the previous section.  
  
    ```  
    USE AdventureWorks2012 ;   
    GO  
    -- Create the database audit specification.   
    CREATE DATABASE AUDIT SPECIFICATION Audit_Pay_Tables  
    FOR SERVER AUDIT Payrole_Security_Audit  
    ADD (SELECT , INSERT  
         ON HumanResources.EmployeePayHistory BY dbo )   
    WITH (STATE = ON) ;   
    GO  
  
    ```  
  
 For more information, see [CREATE SERVER AUDIT &#40;Transact-SQL&#41;](../../../t-sql/statements/create-server-audit-transact-sql.md) and [CREATE DATABASE AUDIT SPECIFICATION &#40;Transact-SQL&#41;](../../../t-sql/statements/create-database-audit-specification-transact-sql.md).  
  
  
