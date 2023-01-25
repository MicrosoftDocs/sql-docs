---
description: "Troubleshooting Oracle Publishers"
title: "Troubleshooting Oracle Publishers | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Oracle publishing [SQL Server replication], troubleshooting"
  - "troubleshooting [SQL Server replication], Oracle publishing"
ms.assetid: be94f1c1-816b-4b1d-83f6-2fd6f5807ab7
author: "MashaMSFT"
ms.author: "mathoma"
---
# Troubleshooting Oracle Publishers
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  This topic lists a number of issues that might arise when configuring and using an Oracle Publisher.  
  
## An Error Is Raised Regarding Oracle Client and Networking Software  
 The account under which [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] runs on the Distributor must be granted read and execute permissions for the directory (and all subdirectories) in which the Oracle client networking software is installed. If the permissions are not granted or the Oracle client components are not installed properly, you will receive the following error message:  
  
 "Connection to server failed with [Microsoft OLE DB Provider for Oracle]. Oracle client and networking components were not found. These components are supplied by Oracle Corporation and are part of the Oracle Version 7.3.3 or later client software installation. Provider is unable to function until these components are installed."  
  
 If an appropriate Oracle client has been installed at the Distributor, ensure that [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] was stopped and then restarted after the client installation completed. This is required in order for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to recognize the client components.  
  
 If you have verified that permissions are granted and that components are installed correctly, but this error persists, verify that the registry settings at HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\MSDTC\MTxOCI are correct:  
  
-   For Oracle 10g, the correct settings are  
  
    -   OracleOciLib = oci.dll  
  
    -   OracleSqlLib = orasql10.dll  
  
    -   OracleXaLib = oraclient10.dll  
  
-   For Oracle 9i, the correct settings are  
  
    -   OracleOciLib = oci.dll  
  
    -   OracleSqlLib = orasql9.dll  
  
    -   OracleXaLib = oraclient9.dll  
  
## The SQL Server Distributor Cannot Connect to the Oracle Database Instance  
 If the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor cannot connect to the Oracle Publisher, ensure that:  
  
-   The necessary Oracle software is installed on the Distributor.  
  
-   The Oracle database is online and you can connect to it using a tool like SQL*Plus.  
  
-   The login replication uses to connect to the Oracle Publisher has sufficient permissions. For more information, see [Configure an Oracle Publisher](../../../relational-databases/replication/non-sql/configure-an-oracle-publisher.md).  
  
-   The TNS names defined during configuration of the Oracle Publisher are listed in the tnsnames.ora file.  
  
-   The correct Oracle Home and path are used. Even if you have only one set of Oracle binaries installed on the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor, ensure that the environment variables related to the Oracle Home are set properly. If you change environment variable values, you must stop and restart [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] for the change to take effect.  
  
 For more information about configuring and testing connectivity, see "Installing and Configuring Oracle Client Networking Software on the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor" in [Configure an Oracle Publisher](../../../relational-databases/replication/non-sql/configure-an-oracle-publisher.md).  
  
## The Oracle Publisher Is Associated with Another Distributor  
 An Oracle Publisher can only be associated with one [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor. If a different Distributor is associated with the Oracle Publisher, it must be dropped before another Distributor can be used. If the Distributor is not dropped first, you will receive one of the following error messages:  
  
-   "Oracle server instance '\<*OraclePublisherName*>' has been previously configured to use '\<*SQLServerDistributorName*>' as its Distributor. To begin using '\<*NewSQLServerDistributorName*>' as its Distributor, you must remove the current replication configuration on the Oracle server instance, which will delete all publications on that server instance."  
  
-   "Oracle server '\<*OracleServerName*>' is already defined as publisher '\<*OraclePublisherName*>' on distributor '\<*SQLServerDistributorName*>.*\<DistributionDatabaseName>*'. Drop the publisher or drop the public synonym '*\<SynonymName>*' to recreate."  
  
 When an Oracle Publisher is dropped, the replication objects in the Oracle database are automatically cleaned up. However, manual cleanup of the Oracle replication objects is necessary in some cases. To manually clean up Oracle replication objects created by replication:  
  
1.  Connect to the Oracle publisher with DBA permissions.  
  
2.  Issue the SQL command `DROP PUBLIC SYNONYM MSSQLSERVERDISTRIBUTOR;`.  
  
3.  Issue the SQL command `DROP USER <replication_administrative_user_schema>``CASCADE;`.  

## SQL Server Error 21663 Is Raised Regarding the Lack of a Primary Key  
 Articles in transactional publications must have a valid primary key. If they do not have a valid primary key, you will receive the following error message when you attempt to add an article:  
  
 "No valid primary key found for source table [\<*TableOwner*>].[\<*TableName*>]"  
  
 For information about requirements for primary keys, see the section "Unique Indexes and Constraints" in the topic [Design Considerations and Limitations for Oracle Publishers](../../../relational-databases/replication/non-sql/design-considerations-and-limitations-for-oracle-publishers.md).  
  
## SQL Server Error 21642 Is Raised Regarding a Duplicate Linked Server Login  
 When an Oracle Publisher is initially configured, a linked server entry is created for the connection between the Publisher and the Distributor. The linked server has the same name as the Oracle TNS service name. If you attempt to create a linked server with the same name, the following error message is shown:  
  
 "Heterogeneous publishers require a linked server. A linked server named '*\<LinkedServerName>*' already exists. Please remove linked server or choose a different publisher name."  
  
 This error can occur if you attempt to create the linked server directly or if you have previously dropped the relationship between the Oracle Publisher and the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor, and you are now attempting to reconfigure it. If you receive this error while attempting to reconfigure the Publisher, drop the linked server with [sp_dropserver &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-dropserver-transact-sql.md).  
  
 If you need to connect to the Oracle Publisher over a linked server connection, create another TNS service name, and then use this name when calling [sp_addlinkedserver &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md). For information about creating TNS service names, see the Oracle documentation.  
  
## SQL Server Error 21617 Is Raised  
 Oracle publishing uses the Oracle application SQL*PLUS to download the package of Publisher support code to the Oracle database. Before attempting to configure the Oracle Publisher, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] verifies that SQL\*PLUS is accessible through the system path on the Distributor. If SQL\*PLUS cannot be loaded, the following error message is shown:  
  
 "Unable to run SQL*PLUS. Make certain that a current version of the Oracle client code is installed at the distributor."  
  
 Try to locate SQL\*PLUS on the Distributor. For an Oracle 10g client install, the name of this executable is sqlplus.exe. It is typically installed in %ORACLE_HOME%/bin. To verify that the path of SQL\*PLUS appears in the system path, examine the value of the system variable **Path**:  
  
1.  Right-click **My Computer**, and then click **Properties**.  
  
2.  Click the **Advanced** tab, and then click **Environment variables**.  
  
3.  In the **Environment Variables** dialog box, in the **System variables** list, select the **Path** variable, and then click **Edit**.  
  
4.  In the **Edit System Variable** dialog box: if the path to the folder that contains sqlplus.exe is not present in the **Variable value** text box, edit the string to include it.  
  
5.  Click **OK** on each open dialog box to exit and save changes.  
  
 If you cannot locate sqlplus.exe on the Distributor, install a current version of the Oracle client software at the Distributor. For more information, see [Configure an Oracle Publisher](../../../relational-databases/replication/non-sql/configure-an-oracle-publisher.md).  
  
## SQL Server Error 21620 Is Raised  
 If you are connecting to an Oracle database earlier than version 8.1, Oracle publishing requires that the Oracle client software installed on the Distributor be from version 9. If you are connecting to an Oracle database that is version 8.1 or later, we recommend that the Oracle client software be version 10 or later.  
  
 Before attempting to configure the Oracle Publisher, Oracle publishing verifies that the version of SQL*PLUS accessible through the system path on the Distributor is version 9 or later. If it is not, the following error message is shown:  
  
 "The version of SQL*PLUS that is accessible through the system Path variable is not current enough to support Oracle publishing. Make certain that a current version of the Oracle client code is installed at the distributor."  
  
 If you have multiple versions of the Oracle client software installed on the Distributor, make sure that the most current version is at least version 9 and that the system path variable refers first to this version (references to other versions can appear as long as the most recent appears first). For more information about editing the system path variable, see the section "SQL Server Error 21617 is Raised" earlier in this topic.  
  
## SQL Server Error 21624 or Error 21629 Is Raised  
 For 64-bit Distributors, Oracle publishing uses the Oracle OLEDB Provider for Oracle (OraOLEDB.Oracle). Make sure that the Oracle OLEDB provider is installed and registered on the Distributor. If the provider is not installed and registered, one or both of the following error messages is shown:  
  
-   "Unable to locate the registered Oracle OLEDB provider, OraOLEDB.Oracle, at distributor '%s'. Make certain that a current version of the Oracle OLEDB provider is installed and registered at the distributor."  
  
-   "The CLSID registry key indicating that the Oracle OLEDB Provider for Oracle, OraOLEDB.Oracle, has been registered is not present at the distributor. Make certain that the Oracle OLEDB provider is installed and registered at the distributor."  
  
 If you are using Oracle client software version 10g, the provider is OraOLEDB10.dll; for version 9i, it is OraOLEDB.dll. The provider is installed in %ORACLE_HOME%\BIN (for example, C:\oracle\product\10.1.0\Client_1\bin). If you determine that the Oracle OLEDB provider is not installed on the Distributor, install it from the Oracle client software install disc provided by Oracle. For more information, see [Configure an Oracle Publisher](../../../relational-databases/replication/non-sql/configure-an-oracle-publisher.md).  
  
 If the Oracle OLEDB provider is installed, make sure that it is registered. To register the provider DLL, execute the following command from the directory in which the DLL is installed, and then stop and restart the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance:  
  
1.  `regsvr32 OraOLEDB10.dll` or `regsvr32 OraOLEDB.dll`.  
  
## SQL Server Error 21626 or Error 21627 Is Raised  
 To verify that the Oracle publishing environment is configured properly, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] tries to connect to the Oracle Publisher with the login credentials you specified during configuration. If the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributor cannot connect to the Oracle Publisher, the following error message is shown:  
  
-   "Unable to connect to Oracle database server '%s' using the Oracle OLEDB provider OraOLEDB.Oracle."  
  
 If this error message is shown, verify connectivity to the Oracle database by running SQL*PLUS directly using the same login and password specified during configuration of the Oracle Publisher. For more information, see the section "The SQL Server Distributor Cannot Connect to the Oracle Database Instance" earlier in this topic.  
  
## SQL Server Error 21628 Is Raised  
 For 64-bit Distributors, Oracle publishing uses the Oracle OLEDB Provider for Oracle (OraOLEDB.Oracle). [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] creates a registry entry to allow the Oracle provider to run in process with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. If there is a problem reading or writing this registry entry, the following error message is shown:  
  
 "Unable to update the registry of distributor '%s' to allow Oracle OLEDB provider OraOLEDB.Oracle to run in process with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Make certain that current login is authorized to modify [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] owned registry keys."  
  
 Oracle publishing requires the registry entry to exist and to be set to **1** for 64 bit Distributors. If the entry does not exist, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] will attempt to create it. If the entry exists, but is set to **0**, the setting will not be changed; the configuration of the Oracle Publisher will fail.  
  
 To view and modify the registry setting:  
  
1.  Click **Start**, and then click **Run**.  
  
2.  In the **Run** dialog box, type **regedit**, and then click **OK**.  
  
3.  Navigate to HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\\*\<InstanceName>*\Providers.  
  
     Included under Providers should be a folder named OraOLEDB.Oracle. Within this folder should be the DWORD value name **AllowInProcess**, with a value of **1**.  
  
4.  If you determine that **AllowInProcess** is set to **0**, update the registry entry to **1**:  
  
    1.  Right-click the entry, and then click **Modify**.  
  
    2.  In the **Edit String** dialog box, type **1** in the **Value data** field.  
  
## SQL Server Error 21684 Is Raised  
 If the administrative user account does not have sufficient privileges, the following error message is shown:  
  
 "The permissions associated with the administrator login for Oracle publisher '%s' are not sufficient."  
  
 To verify the permissions granted to the user, execute the following query: `SELECT * from session_privs`. The output should be similar to the following:  
  
 `PRIVILEGE`  
  
 `------------------`  
  
 `CREATE SESSION`  
  
 `CREATE TABLE`  
  
 `CREATE PUBLIC SYNONYM`  
  
 `DROP PUBLIC SYNONYM`  
  
 `CREATE VIEW`  
  
 `CREATE SEQUENCE`  
  
 `CREATE PROCEDURE`  
  
 `CREATE TRIGGER`  
  
## You Encounter Permissions Issues for the Replication User Schema  
 The replication user schema must have the permissions described in "Creating the User Schema Manually" in [Configure an Oracle Publisher](../../../relational-databases/replication/non-sql/configure-an-oracle-publisher.md).  
  
## Oracle Error ORA-01000  
 Replication uses cursors on the Oracle Publisher during the process of adding articles to a publication. It is possible to exceed the maximum number of cursors available on the Publisher during this process. If this occurs, the following error is raised:  
  
 "ORA-01000: maximum open cursors exceeded"  
  
 To avoid this problem, ensure that the **max_open_cursors** setting in the Oracle databases is set to a sufficiently high number (at least 1000). For more information about this setting, see the Oracle documentation.  
  
## Oracle Error ORA-01555  
 The following Oracle database error is not related to snapshot replication; it is related to how Oracle constructs read-consistent views of data:  
  
 "ORA-01555: Snapshot too old"  
  
 Using objects known as rollback segments, Oracle constructs read-consistent views of data as of the point in time a SQL statement is issued. A "snapshot too old" error might occur when rollback information is overwritten by other concurrent sessions. Prior to Oracle 9i, the recommended method of reducing the frequency of this error was to increase the size and/or number of rollback segments, and to assign large transactions to a specific rollback segment.  
  
 In Oracle 9i, Oracle introduced the UNDO tablespace concept, which replaces the rollback segment. To prevent the "snapshot too old" error in Oracle 9i, it is recommended that you:  
  
-   Create an UNDO tablespace with an appropriate amount of free space.  
  
-   Set the retention guarantee on the tablespace (Oracle 10G and greater).  
  
-   Configure the Oracle initialization parameters UNDO_MANAGEMENT and UNDO_RETENTION.  
  
 For further details about avoiding the "snapshot too old" error, consult the Oracle documentation.  
  
## Oracle Error ORA-22285  
 If a table includes a BFILE column, the data for the column is stored in the file system. The replication administrative user account must be granted access to the directory in which the data is stored using the following syntax:  
  
 `GRANT READ ON DIRECTORY <directory_name> TO <replication_administrative_user_schema>`  
  
 If access is not granted, the following error is raised by the Log Reader Agent:  
  
 "ORA-22285: non-existent directory or file for FILEOPEN operation"  
  
## Changes Are Made That Require Reconfiguration of the Publisher  
 Changes to replication metadata tables or procedures require that you drop and reconfigure the Publisher. To reconfigure the Publisher, you must drop the Publisher and configure it again using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], Transact-SQL, or RMO. For information about configuring the Publisher, see [Configure an Oracle Publisher](../../../relational-databases/replication/non-sql/configure-an-oracle-publisher.md).  
  
 **To drop an Oracle Publisher (**SQL Server Management Studio**)**  
  
1.  Connect to the Distributor for the Oracle Publisher in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] and expand the server node.  
  
2.  Right-click **Replication**, and then click **Distributor Properties**.  
  
3.  On the **Publishers** page of the **Distributor Properties** dialog box, clear the check box for the Oracle Publisher.  
  
4.  Click **OK**.  
  
 **To drop an Oracle Publisher (Transact-SQL)**  
  
-   Execute **sp_dropdistpublisher**. For more information, see [sp_dropdistpublisher &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-dropdistpublisher-transact-sql.md).  
  
## See Also  
 [Configure an Oracle Publisher](../../../relational-databases/replication/non-sql/configure-an-oracle-publisher.md)   
 [Oracle Publishing Overview](../../../relational-databases/replication/non-sql/oracle-publishing-overview.md)  
  
  
