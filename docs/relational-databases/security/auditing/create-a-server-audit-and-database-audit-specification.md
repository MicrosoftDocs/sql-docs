---
title: "Create a Server Audit and Database Audit Specification | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: security
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.sqlaudit.dbaudit.general.f1"
helpviewer_keywords: 
  - "audits [SQL Server], creating database specification"
  - "database audit [SQL Server]"
ms.assetid: 26ee85de-6e97-4318-b526-900924d96e62
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Create a Server Audit and Database Audit Specification
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to create a server audit and database audit specification in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)].  
  
 *Auditing* an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] or a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database involves tracking and logging events that occur on the system. The *SQL Server Audit* object collects a single instance of server- or database-level actions and groups of actions to monitor. The audit is at the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance level. You can have multiple audits per [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance. The *Database-Level Audit Specification* object belongs to an audit. You can create one database audit specification per SQL Server database per audit. For more information, see [SQL Server Audit &#40;Database Engine&#41;](../../../relational-databases/security/auditing/sql-server-audit-database-engine.md).  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To create a server audit and database audit specification, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
 Database audit specifications are non-securable objects that reside in a given database. When a database audit specification is created, it is in a disabled state.  
  
 When you are creating or modifying a database audit specification in a user database, do not include audit actions on server-scope objects, such as the system views. If server-scoped objects are included, the audit will be created. However, the server-scoped objects will not be included, and no error will be returned. To audit server-scope objects, use a database audit specification in the master database.  
  
 Database audit specifications reside in the database where they are created, with the exception of the **tempdb** system database.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
  
-   Users with the ALTER ANY DATABASE AUDIT permission can create database audit specifications and bind them to any audit.  
  
-   After a database audit specification is created, it can be viewed by principals with the CONTROL SERVER,  ALTER ANY DATABASE AUDIT permissions, or the sysadmin account.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To create a server audit  
  
1.  In Object Explorer, expand the **Security** folder.  
  
2.  Right-click the **Audits** folder and select **New Audit...**. For more information, see [Create a Server Audit and Server Audit Specification](../../../relational-databases/security/auditing/create-a-server-audit-and-server-audit-specification.md).  
  
3.  When you are finished selecting options, click **OK**.  
  
#### To create a database-level audit specification  
  
1.  In Object Explorer, expand the database where you want to create an audit specification.  
  
2.  Expand the **Security** folder.  
  
3.  Right-click the **Database Audit Specifications** folder and select **New Database Audit Specification...**.  
  
     The following options are available on the **Create Database Audit Specification** dialog box.  
  
     **Name**  
     The name of the database audit specification. This is generated automatically when you create a new server audit specification but is editable.  
  
     **Audit**  
     The name of an existing database audit. Either type in the name of the audit or select it from the list.  
  
     **Audit Action Type**  
     Specifies the database-level audit action groups and audit actions to capture. For the list of database-level audit action groups and audit actions and a description of the events they contain, see [SQL Server Audit Action Groups and Actions](../../../relational-databases/security/auditing/sql-server-audit-action-groups-and-actions.md).  
  
     **Object Schema**  
     Displays the schema for the specified **Object Name**.  
  
     **Object Name**  
     The name of the object to audit. This is only available for audit actions; it does not apply to audit groups.  
  
     **Ellipsis (...)**  
     Opens the **Select Objects** dialog to browse for and select an available object, based on the specified **Audit Action Type**.  
  
     **Principal Name**  
     The account to filter the audit by for the object being audited.  
  
     **Ellipsis (...)**  
     Opens the **Select Objects** dialog to browse for and select an available object, based on the specified **Object Name**.  
  
4.  When you are finished selecting option, click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To create a server audit  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
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
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. The example creates a database audit specification called `Audit_Pay_Tables` that audits SELECT and INSERT statements by the `dbo` user, for the `HumanResources.EmployeePayHistory` table based on the server audit defined above.  
  
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
  
  
