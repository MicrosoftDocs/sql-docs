---
title: "The Oracle CDC Service"
description: "The Oracle CDC Service"
author: chugugrace
ms.author: chugu
ms.reviewer: mikeray
ms.date: 02/24/2026
ms.service: sql
ms.subservice: integration-services
ms.topic: concept-article
---
# The Oracle CDC Service

[!INCLUDE [oracle-cdc-retirement](../includes/attunity-oracle-cdc-retirement.md)]

The Oracle CDC Service is a Windows service running the program xdbcdcsvc.exe. The Oracle CDC Service can be configured to run multiple Windows services on the same computer, each one with a different Windows service name. Creating multiple Oracle CDC Windows services on a single computer is often done to achieve a better separation between them, or when each needs to work with a different [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance.

An Oracle CDC Service is created using the Oracle CDC Service Configuration console or is defined through the command-line interface built into the xdbcdcsvc.exe program. In both cases, each Oracle CDC Service created is associated with a single [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance (which might be clustered or mirrored with **Always On** setup) and the connection information (connect string and access credentials) are part of the service configuration.

When an Oracle CDC Service is started, it tries to connect to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance it's associated with, get the list of Oracle CDC instances it needs to handle, and performs an initial environment validation. Errors during the service startup and any start/stop information are always written to the Windows application event log. When a connection to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is established, all errors and information messages are written to the **dbo.xdbcdc_trace** table in the MSXDBCDC database of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance. One of the checks made during startup is verification that no other Oracle CDC Service with the same name is currently working. If a service with the same name is currently connected from a different computer, the Oracle CDC Service enters a wait loop, waiting for the other service to disconnect before proceeding to handle the Oracle CDC work.

When the Oracle CDC Service passes all the startup verifications, it checks the **dbo.xdbcdc_databases** table in the MSXDBCDC database for any enabled Oracle CDC Instances. For every enabled Oracle CDC Instance, the service starts a sub-process to handle that Oracle CDC Instance.

When an Oracle CDC Instance starts, it accesses the SQL Server CDC database with the same name as the CDC Instance and retrieves its state from the previous run. It also verifies that everything is running properly. It then resumes processing changes; Reading the Oracle transaction logs and writing changes to the CDC database.

The Oracle CDC Service periodically monitors the **dbo.xdbcdc_tables** table in the MSXDBCDC database to determine if there were any configuration changes to any of the Oracle CDC Instance configurations. If a change is found, the Oracle CDC Service notifies the Oracle CDC Instance that it should check its configuration for changes. Most configuration changes, such as adding and removing capture instances can be applied while the Oracle CDC instance is enabled, others require the Oracle CDC Instance to be restarted.

When using the Oracle CDC Designer console, changes are automatically detected. When updating the Oracle CDC configuration directly using SQL, the following procedure should be called for the Oracle CDC Service to notice the configuration change:

```sql
DECLARE @dbname nvarchar(128) = 'HRcdc'
EXECUTE [MSXDBCDC].[dbo].[xdbcdc_update_config_version] @dbname
GO
```

The Oracle CDC Instance process updates its status in the system table **cdc.xdbcdc_state** and writes error information to the **cdc.xdbcdc_trace** table. The **xdbcdc_state** table is useful for monitoring the state of the Oracle CDC Instance. It provides up-to-date status, various counters (such as number of changes read from Oracle, number of changes written to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], number of committed transactions written and the current number of in-flight transactions) and latency indication.

The Oracle CDC Instance configuration is saved in the **cdc.xdbcdc_config** table, which is the table that the Oracle CDC Designer console works with. Because the entire configuration of an Oracle CDC Instance is found in the target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance and CDC databases, it's possible to create [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] deployment scripts for an Oracle CDC Instance. This is done using the Oracle CDC Service Configuration and Oracle CDC Designer consoles.

## Security Considerations

The following describes the security requirements necessary to work with the CDC Service for Oracle.

### Protection of Source Oracle Data

The Oracle CDC service doesn't require access to Oracle source data and is protected by ensuring that the log-mining credentials don't give SELECT permission on customer Oracle tables.

### Protection of Source Oracle Change Data

The Oracle CDC service is provided with log-mining credentials that let the service capture changes made to any table in the Oracle database. Change data doesn't have the granular access permissions regular tables have, therefore accessing change data bypasses the built-in Oracle data access controls.

Captured source Oracle tables have empty mirror tables with the same schema and table name in CDC database. The captured data is stored in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] capture instances and offer the same protection as is provided for changes captured from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database. To gain access to the change data that is associated with a capture instance, the user must be granted select access to all the captured columns of the associated mirror table. In addition, if a gating role is specified when the capture instance is created, the caller must also be a member of the specified gating role. Other general change data capture functions for accessing metadata is accessible to all database users through the public role, although access to the returned metadata is usually also gated by using select access to the underlying source tables, and by membership in any defined gating roles.

This means that users with the **sysadmin** fixed server role or the **db_owner** fixed database role have (by default) full access to the captured data, and further access can be granted either through gating roles or by granting select access to the captured columns.

### Protection of Source Oracle Log Mining Credentials

The Oracle CDC service configuration, stored in the CDC database (in the cdc.xdbcdc_config table) includes the log mining user name and its associated password.

The log mining password is stored encrypted by means of an asymmetric key with the fixed name `xdbcdc_asym_key` that is automatically created with the following command:

```sql
USE [<cdc-database-name>]
CREATE ASYMMETRIC KEY xdbcdc_asym_key
    WITH ALGORITHM = RSA_1024
    ENCRYPTION BY PASSWORD = '<cdc-database-name><asym-key-password>'
```

If a different algorithm is used, this key can be dropped and a new one by the same name and encrypted by the same password can be created.

The asymmetric key password is the master password that is saved in the registry under the path **HKLM\Software\Microsoft\XDBCDCSVC\\\\<service-name\>**. That key is accessible only to local administrators and to the Oracle CDC Windows service account. The key contains an encrypted binary value **AsymmetricKeyPassword** that stored the asymmetric key password. Access to this registry key is required to be able to access to the Oracle log mining credentials.

To use the ENCRYPTION BY PASSWORD clause, the password must meet the Windows password policy requirements for the computer running the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance. This is done by selecting the asymmetric key password according to that policy.

If the asymmetric key password is lost, the log mining credentials for each of the Oracle CDC instances must be specified again in the Oracle CDC Service Designer.

The asymmetric key is automatically created in the CDC database when the CDC service detects an Oracle instance CDC database that doesn't have this asymmetric key or when the key exists but the password doesn't match.

### Oracle CDC Service Windows Service Account

The service account used with the Oracle CDC Windows service doesn't require any additional privileges. This account must be able to use both the Oracle Native Client API and the SQL Server Native Client ODBC API. It also needs to be able to access the service configuration key in the registry (this CDC Service Configuration console sets up the ACL for that).

## In this section

- [High Availability Support](high-availability-support.md)

- [SQL Server Connection Required Permissions for the CDC Service](sql-server-connection-required-permissions-for-the-cdc-service.md)

- [User Roles](user-roles.md)

- [Working with the Oracle CDC Service](working-with-the-oracle-cdc-service.md)

## Connection to SQL Server

When a login without a database role that includes write permission (for example the **db_owner** role) to the MSXDBCDC database attempts to create an Oracle CDC instance, the Connect to SQL Server dialog box is displayed.

In this dialog box you must enter the credentials for a login that has write permission to the MSXDBCDC database, such as the **db_owner** database role to create the new Oracle CDC instance.

Enter the following information in the Connect to SQL Server dialog box.

### Server Name

Type the name of the server where the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is located.

### Authentication

Select one of the following:

- Windows Authentication

- **SQL Server Authentication**: If you select this option, you must type the **Login** and **Password** for the user in the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] you're connecting to.

### Options

Select the arrow to view available options to be configured. You can choose to leave these options with their default value. The available options are:

- **Connection Timeout**: Type the time (in seconds) the program waits for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] connection to be established before producing a timeout error. The default value is `15`.

- **Execution Timeout**: Type the time (in seconds) that the program waits for SQL command execution to finish before producing a timeout error. The default value is `30`.

- **Encrypt Connection**: Select **Encrypt Connection** to ensure that the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] connection that being established is encrypted to guarantee privacy.

- **Advanced**: Select **Advanced** and type any additional connection properties into the Advanced Connection Properties dialog box, if necessary.

## Connection to SQL Server for Delete

When a login without a database role that includes write permission (for example the **db_owner** role) to the MSXDBCDC database attempts to delete an Oracle CDC instance, the Connect to SQL Server dialog box is displayed.

In this dialog box you must enter the credentials for a login that has write permission to the MSXDBCDC database, such as the **db_owner** database role to delete the Oracle CDC instance.

Enter the following information in the Connect to SQL Server dialog box.

**Server Name**  
Type the name of the server where the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is located.

**Authentication**  
Select one of the following:

- **Windows Authentication**

- **SQL Server Authentication**: If you select this option, you must type the **Login** and **Password** for the user in the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] you're connecting to.

**Options**  
Select the arrow to view available options to be configured. You can choose to leave these options with their default value. The available options are:

- **Connection Timeout**: Type the time (in seconds) the program waits for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] connection to be established before producing a timeout error. The default value is `15`.

- **Execution Timeout**: Type the time (in seconds) that the program waits for SQL command execution to finish before producing a timeout error. The default value is `30`.

- **Encrypt Connection**: Select **Encrypt Connection** to ensure that the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] connection that being established is encrypted to guarantee privacy.

- **Advanced**: Select **Advanced** and type any additional connection properties into the Advanced Connection Properties dialog box, if necessary.

## Create and Edit an Oracle CDC Service

You create and edit a new Oracle CDC Windows Service from the CDC Service Configuration Console.

To create a new Oracle CDC Windows service, select **Local CDC Services** from the left pane, then select **New Service** from the **Actions** pane. You can also right-click **Local CDC Services** and select **New Service**. The New Oracle CDC Windows Service dialog box opens.

**OR**

To edit the CDC service properties, select the service that you want to edit the properties for and select **Properties** from the **Actions** pane. You can also right-click the service you're working with and select **Properties**. The CDC Service Properties dialog box opens.

Enter the following information in the New Oracle CDC Windows Service dialog box or the CDC Service Properties dialog box.

**Service Name**  
Type the name of the new Oracle CDC Windows Service. You shouldn't use long names, if possible. The characters / and \ can't be used in the service name.

> [!NOTE]  
> This option isn't available when editing the service. You can't change the name of a Windows service that already exists.

**Description**  
Type a description of the service to help identify it.

**Service Account**  
Select one of the following to determine under which account to run the service:

- **Local System Account**

  This isn't recommended because it gives too many permissions to the service.

- **This Account**

  On Windows Vista or Windows Server 2008, the default service account is the NETWORK SERVICE account.

  On Windows 7, Windows Server 2008 R2 and later, the default service account is NT Service\\\\<service-name\>.

  Using these accounts lets you work without using passwords because a password isn't necessary for these accounts. In addition these accounts provide only the necessary permissions required for the Oracle CDC Service to run.

  You can use a local or domain Windows account for the service account. In this case, you must enter the **Password** for that account. This account can be for the local host or a domain account. Be sure to update the password when it's changed using Local Services in the Windows Control Panel.

**Server name**: Select the target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance to connect to (for example, **\\\\<computer_name>\\<instance_name>**). The server instance last connected to is displayed by default.

**Authentication**  
Select one of the following:

- **Windows Authentication**: If you select this option, the Oracle CDC service connects to the target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance using the service account identity. If the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance is running on a different computer, Windows Authentication must be used with domain accounts.

- **SQL Server Authentication**: If you select this option, you must type the **User Name** and **Password** for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login you want to use. The Oracle CDC service uses these credentials when connecting to the target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance.

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login used by the Oracle CDC Service only needs to be a member of the public fixed-server role, no other privileges are needed. Once new Oracle CDC Instances are added, that login will gain **db_owner** access to the associated [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] CDC databases.

To create the Oracle CDC Windows Service definition, the program needs update access to the MSXDBCDC database in the associated [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance. When you select **OK**, a dialog box prompts you to enter a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login with an update access to the MSXDBCDC database.

**Options**  
Select the arrow to view available options to be configured. You can choose to leave these options with their default value. The available options are:

- **Connection Timeout**: Type the time (in seconds) that the CDC Service for Oracle waits for a connection to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] before timing out. The default value is `15`.

- **Execution Timeout**: Type the time (in seconds) that the Oracle CDC Windows Service waits for a command to execute before timing out. The default value is `30`.

- **Encrypt Connection**: Select **Encrypt Connection** for communication between the Oracle CDC Service and the target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance using an encrypted connection.

- **Advanced**: Type any additional connection properties, if necessary.

**Master Password**  
Enter a password to be used by the Oracle CDC Windows Service to protect the Oracle log-mining credentials.

The same master password must also be used when other instances of the same service are configured on other nodes on a cluster in high-availability configuration. If you lose or modify the master password, all log mining passwords stored in Oracle CDC Instance databases must be re-entered using the CDC Designer console.

## Manage an Oracle CDC Service

You can use the CDC Service Configuration Console to manage a specific CDC Service.

**To select the CDC service you want to work with**

1. From the left pane in the CDC Service Configuration Console, expand **Local CDC Services**.

1. Select the CDC service you want to work with.

   You can also right-click the CDC service you want to work with and select the desired action.

**OR**

1. Select **Local CDC Services** from the left pane in the CDC Service Configuration Console.

1. From the middle section of the CDC Service Configuration Console, select the service you want to work with.

   You can also right-click the CDC service you want to work with and select the desired action.

### Actions when working with a CDC service

You can carry out the following actions when working with a CDC service.

#### Delete the service

From the **Actions** pane on the right side of the CDC Service Configuration Console, select **Delete** to delete the service.

You can also right-click the CDC service you want to delete and select **Delete**.

> [!NOTE]
> If the service is running when deleting the service, the service is stopped before being deleted.

To delete the Oracle CDC Windows Service definition, the program needs update access to the MSXDBCDC database in the associated [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance. When you select OK to delete the service, the program attempts to delete the Oracle CDC Service registration in the MSXDBCDC database. If the program can't delete the Oracle CDC Service registration because it doesn't have the proper permissions, it prompts you to enter a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login with update permissions to the MSXDBCDC database.

#### Edit the CDC Service Properties

From the **Actions** pane on the right side of the CDC Service Configuration Console, select **Properties**.

You can also right-click the CDC service where you want to edit the properties and select **Properties**.

## Prepare SQL Server for CDC

The Oracle CDC service requires all target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances to contain the MSXDBCDC database. You create this database using the Prepare SQL Server action in the CDC Service Configuration Console. This creates a special script that is run to create the required tables, stored procedures, and other required artifacts for this database. This task is done one time only for each target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance.

For more information about the MSXDBCDC database, see The MSXDBCDC Database.

In the CDC Service Configuration Console, select **Prepare SQL Server**. If this option isn't available, make sure that **Local CDC Services** is selected in the left pane of the console.

### Options

**Server Name**  
Type the name of the server where the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is located.

**Authentication**  
Select one of the following:

- **Windows Authentication**

- **SQL Server Authentication**: If you select this option, you must type the **User Name** and **Password** for the user in the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] you're connecting to.

To prepare the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance for Oracle CDC, the login must have write permission to the MSXDBCDC database. Enter the credentials for a login that has write permission to the MSXDBCDC database, such as a member of the `sysasmin` role.

**Options**  
Select the arrow to view available options to be configured. You can choose to leave these options with their default value. The available options are:

- **Connection Timeout**: Type the time (in seconds) that the CDC Service for Oracle waits for a connection to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] before timing out. The default value is `15`.

- **Execution Timeout**: Type the time (in seconds) that the Oracle CDC Windows Service waits for a command to execute before timing out. The default value is `30`.

- **Encrypt Connection**: Select **Encrypt Connection** for communication between the Oracle CDC Service and the target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance using an encrypted connection.

- **Advanced**: Type any additional connection properties, if necessary.

**View Script**  
Select **View Script** to view a read-only version of the setup script. A [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] system administrator can copy this script into the SQL Server Management Console to edit it, if necessary.

## Prepare SQL Server for Oracle CDC-View Script

This dialog box shows the Prepare SQL script that creates the MSXDBCDC database. This database must be on a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance for it to be used with Oracle CDC for SQL Server.

Do the following in the Prepare SQL Server Script dialog box.

**Save As**  
Saves the script in a text file that you can save in any location you want. You can then run the scripts at a later time by pasting the script into the [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)].

**Copy**  
Copies the script to the clipboard. You can then paste the script into the [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to run them and create the MSXDBCDC database.

## Work with CDC Services

You can use the CDC Service Configuration Console to create a new CDC service and to prepare a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database for CDC.

**Prepare SQL Server**  
Select this option from the **Actions** pane on the right side of the CDC Service Configuration Console.

You can also right-click **Local CDC Services** and select **Prepare SQL Server**.

The Preparing SQL Server Instance for Oracle CDC dialog box opens.

For information on how to use this dialog box, see [Prepare SQL Server for CDC](#prepare-sql-server-for-cdc). For information on how to enable a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance for CDC, see [How to Prepare SQL Server for CDC](how-to-prepare-sql-server-for-cdc.md).

**Create a new CDC service**  
Select **New Service** from the **Actions** pane on the right side of the CDC Service Configuration Console.

You can also right-click **Local CDC Services** and select **New Service**.

The New Oracle CDC Service dialog box opens.

## Related content

- [How to Manage a Local CDC Service](how-to-manage-a-local-cdc-service.md)
- [SQL Server Connection Required Permissions for the CDC Service](sql-server-connection-required-permissions-for-the-cdc-service.md)
