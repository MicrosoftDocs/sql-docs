---
title: "The Oracle CDC Service | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 47759ddc-358d-405b-acb9-189ada76ea6d
author: janinezhang
ms.author: janinez
manager: craigg
---
# The Oracle CDC Service
  The Oracle CDC Service is a Windows service running the program xdbcdcsvc.exe. The Oracle CDC Service can be configured to run multiple Windows services on the same computer, each one with a different Windows service name. Creating multiple Oracle CDC Windows services on a single computer is often done to achieve a better separation between them, or when each needs to work with a different [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
 An Oracle CDC Service is created using the Oracle CDC Service Configuration console or is defined through the command-line interface built into the xdbcdcsvc.exe program. In both cases, each Oracle CDC Service created is associated with a single [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance (which may be clustered or mirrored with **AlwaysOn** setup) and the connection information (connect string and access credentials) are part of the service configuration.  
  
 When an Oracle CDC Service is started, it tries to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance it is associated with, get the list of Oracle CDC instances it needs to handle, and performs an initial environment validation. Errors during the service startup and any start/stop information are always written to the Windows application event log. When a connection to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is established, all errors and information messages are written to the **dbo.xdbcdc_trace** table in the MSXDBCDC database of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. One of the checks made during startup is verification that no other Oracle CDC Service with the same name is currently working. If a service with the same name is currently connected from a different computer, the Oracle CDC Service enters a wait loop, waiting for the other service to disconnect before proceeding to handle the Oracle CDC work.  
  
 When the Oracle CDC Service passes all the startup verifications, it checks the **dbo.xdbcdc_databases** table in the MSXDBCDC database for any enabled Oracle CDC Instances. For every enabled Oracle CDC Instance, the service starts a sub-process to handle that Oracle CDC Instance.  
  
 When an Oracle CDC Instance starts, it accesses the SQL Server CDC database with the same name as the CDC Instance and retrieves its state from the previous run. It also verifies that everything is running properly. It then resumes processing changes; Reading the Oracle transaction logs and writing changes to the CDC database.  
  
 The Oracle CDC Service periodically monitors the **dbo.xdbcdc_tables** table in the MSXDBCDC database to determine if there were any configuration changes to any of the Oracle CDC Instance configurations. If a change is found, the Oracle CDC Service notifies the Oracle CDC Instance that it should check its configuration for changes. Most configuration changes, such as adding and removing capture instances can be applied while the Oracle CDC instance is enabled, others require the Oracle CDC Instance to be restarted.  
  
 When using the Oracle CDC Designer console, changes are automatically detected. When updating the Oracle CDC configuration directly using SQL, the following procedure should be called for the Oracle CDC Service to notice the configuration change:  
  
```  
DECLARE @dbname nvarchar(128) = 'HRcdc'  
EXECUTE [MSXDBCDC].[dbo].[xdbcdc_update_config_version] @dbname  
GO  
  
```  
  
 The Oracle CDC Instance process updates its status in the system table **cdc.xdbcdc_state** and writes error information to the **cdc.xdbcdc_trace** table. The **xdbcdc_state** table is useful for monitoring the state of the Oracle CDC Instance. It provides up-to-date status, various counters (such as number of changes read from Oracle, number of changes written to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], number of committed transaction written and the current number of in-flight transactions) and latency indication.  
  
 The Oracle CDC Instance configuration is saved in the **cdc.xdbcdc_config** table, which is the table that the Oracle CDC Designer console works with. Because the entire configuration of an Oracle CDC Instance is found in the target [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance and CDC databases, it is possible to create [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] deployment scripts for an Oracle CDC Instance. This is done using the Oracle CDC Service Configuration and Oracle CDC Designer consoles.  
  
## Security Considerations  
 The following describes the security requirements necessary to work with the CDC Service for Oracle.  
  
### Protection of Source Oracle Data  
 The Oracle CDC service does not require access to Oracle source data and is protected by ensuring that the log-mining credentials do not give SELECT permission on customer Oracle tables.  
  
### Protection of Source Oracle Change Data  
 The Oracle CDC service is provided with log-mining credentials that let the service capture changes made to any table in the Oracle database. Change data does not have the granular access permissions regular tables have, therefore accessing change data bypasses the built-in Oracle data access controls.  
  
 Captured source Oracle tables have empty mirror tables with the same schema and table name in CDC database. The captured data is stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] capture instances and offer the same protection as is provided for changes captured from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. To gain access to the change data that is associated with a capture instance, the user must be granted select access to all the captured columns of the associated mirror table. In addition, if a gating role is specified when the capture instance is created, the caller must also be a member of the specified gating role. Other general change data capture functions for accessing metadata is accessible to all database users through the public role, although access to the returned metadata is usually also gated by using select access to the underlying source tables, and by membership in any defined gating roles.  
  
 This means that users with the **sysadmin** fixed server role or the **db_owner** fixed database role have (by default) full access to the captured data, and further access can be granted either through gating roles or by granting select access to the captured columns.  
  
### Protection of Source Oracle Log Mining Credentials  
 The Oracle CDC service configuration, stored in the CDC database (in the cdc.xdbcdc_config table) includes the log mining user name and its associated password.  
  
 The log mining password is stored encrypted by means of an asymmetric key with the fixed name `xdbcdc_asym_key` that is automatically created with the following command:  
  
```  
USE [<cdc-database-name>]  
CREATE ASYMMETRIC KEY xdbcdc_asym_key  
    WITH ALGORITHM = RSA_1024  
    ENCRYPTION BY PASSWORD = '<cdc-database-name><asym-key-password>'  
  
```  
  
 If a different algorithm is used, this key can be dropped and a new one by the same name and encrypted by the same password can be created.  
  
 The asymmetric key password is the master password that is saved in the registry under the path **HKLM\Software\Microsoft\XDBCDCSVC\\<service-name>**. That key is accessible only to local administrators and to the Oracle CDC Windows service account. The key contains an encrypted binary value **AsymmetricKeyPassword** that stored the asymmetric key password. Access to this registry key is required to be able to access to the Oracle log mining credentials.  
  
 To use the ENCRYPTION BY PASSWORD clause, the password must meet the Windows password policy requirements for the computer running the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. This is done by selecting the asymmetric key password according to that policy.  
  
 If the asymmetric key password is lost, the log mining credentials for each of the Oracle CDC instances must be specified again in the Oracle CDC Service Designer.  
  
 The asymmetric key is automatically created in the CDC database when the CDC service detects an Oracle instance CDC database that does not have this asymmetric key or when the key exists but the password does not match.  
  
### Oracle CDC Service Windows Service Account  
 The service account used with the Oracle CDC Windows service does not require any additional privileges. This account must be able to use both the Oracle Native Client API and the SQL Server Native Client ODBC API. It also needs to be able to access the service configuration key in the registry (this CDC Service Configuration console sets up the ACL for that).  
  
## In This Section  
  
-   [High Availability Support](high-availability-support.md)  
  
-   [SQL Server Connection Required Permissions for the CDC Service](sql-server-connection-required-permissions-for-the-cdc-service.md)  
  
-   [User Roles for Change Data Capture Service for Oracle by Attunity](user-roles.md)  
  
-   [Working with the Oracle CDC Service](the-oracle-cdc-service.md)  
  
## See Also  
 [How to Manage a Local CDC Service](how-to-manage-a-local-cdc-service.md)   
 [Manage an Oracle CDC Service](manage-an-oracle-cdc-service.md)  
  
  
