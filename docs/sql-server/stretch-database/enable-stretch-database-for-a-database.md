---
title: Enable Stretch Database for a database
description: Enable Stretch Database for a database
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 07/25/2022
ms.service: sql-server-stretch-database
ms.topic: conceptual
ms.custom: seo-dt-2019
helpviewer_keywords:
  - "Stretch Database, enabling database"
  - "enabling database for Stretch Database"
---
# Enable Stretch Database for a database

[!INCLUDE [sqlserver2016-windows-only](../../includes/applies-to-version/sqlserver2016-windows-only.md)]

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]

To configure an existing database for Stretch Database, select **Tasks > Stretch > Enable** for a database in SQL Server Management Studio to open the **Enable Database for Stretch** wizard. You can also use Transact-SQL to enable Stretch Database for a database.

If you select **Tasks > Stretch > Enable** for an individual table, and you haven't yet enabled the database for Stretch Database, the wizard configures the database for Stretch Database and lets you select tables as part of the process. Follow the steps in this article instead of the steps in [Enable Stretch Database for a table](enable-stretch-database-for-a-table.md).

Enabling Stretch Database on a database or a table requires db_owner permissions. Enabling Stretch Database on a database also requires CONTROL DATABASE permissions.

> [!NOTE]
> Later, if you disable Stretch Database, remember that disabling Stretch Database for a table or for a database does not delete the remote object. If you want to delete the remote table or the remote database, you have to drop it by using the Azure management portal. The remote objects continue to incur Azure costs until you delete them manually.

## Before you get started

- Review [Limitations for Stretch Database](limitations-for-stretch-database.md).

- Stretch Database migrates data to Azure, so you need an Azure account and a subscription for billing. [Sign up](https://azure.microsoft.com/pricing/free-trial/) for an Azure account.

- Have the connection and login info you need to create a new Azure server or to select an existing Azure server.

## <a id="EnableTSQLServer"></a> Prerequisite: Enable Stretch Database on the server

Before you can enable Stretch Database on a database or a table, you have to enable it on the local server. This operation requires sysadmin or serveradmin permissions.

- If you have the required administrative permissions, the **Enable Database for Stretch** wizard configures the server for Stretch Database.

- If you don't have the required permissions,  an administrator has to enable the option manually by running `sp_configure` before you run the wizard, or an administrator has to run the wizard.

To enable Stretch Database on the server manually, run `sp_configure` and turn on the **remote data archive** option. The following example enables the **remote data archive** option by setting its value to 1.

```sql
EXEC sp_configure 'remote data archive' , '1';
GO

RECONFIGURE;
GO
```

For more info, see [Configure the remote data archive Server Configuration Option](../../database-engine/configure-windows/configure-the-remote-data-archive-server-configuration-option.md) and [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md).

## Use the wizard

For info about the Enable Database for Stretch Wizard, including the info that you have to enter and the choices that you have to make, see [Get started by running the Enable Database for Stretch Wizard](get-started-by-running-the-enable-database-for-stretch-wizard.md).

## Use Transact-SQL

Before you can enable Stretch Database on individual tables, you have to enable it on the database.

Enabling Stretch Database on a database or a table requires db_owner permissions. Enabling Stretch Database on a database also requires CONTROL DATABASE permissions.

1. Before you begin, choose an existing Azure server for the data that Stretch Database migrates, or create a new Azure server.

1. On the Azure server, create a firewall rule with the IP address range of the SQL Server that lets SQL Server communicate with the remote server.

   You can easily find the values you need and create the firewall rule by attempting to connect to the Azure server from Object Explorer in SQL Server Management Studio (SSMS). SSMS helps you to create the rule by opening the following dialog box, which already includes the required IP address values.

   > [!IMPORTANT]  
   > Stretch Database support is removed in SQL Server Management Studio v19. To manage Stretch Database, you can use SQL Server Management Studio v18.9.1 or lower.

   :::image type="content" source="media/enable-stretch-database-for-a-database/firewall-rule-for-stretch.png" alt-text="Screenshot showing a firewall rule for Stretch.":::

1. To configure a SQL Server database for Stretch Database, the database has to have a database master key. The database master key secures the credentials that Stretch Database uses to connect to the remote database. The following example creates a new database master key.

   ```sql
   USE <database>;
   GO

   CREATE MASTER KEY ENCRYPTION BY PASSWORD='<password>';
   GO
   ```

   For more info about the database master key, see [CREATE MASTER KEY &#40;Transact-SQL&#41;](../../t-sql/statements/create-master-key-transact-sql.md) and [Create a Database Master Key](../../relational-databases/security/encryption/create-a-database-master-key.md).

1. When you configure a database for Stretch Database, you have to provide a credential for Stretch Database to use for communication between the on premises SQL Server and the remote Azure server. You have two options.

   - You can provide an administrator credential.

     - If you enable Stretch Database by running the wizard, you can create the credential at that time.

     - If you plan to enable Stretch Database by running **ALTER DATABASE**, you have to create the credential manually before you run **ALTER DATABASE** to enable Stretch Database.

       The following example creates a new credential.

       ```sql
       CREATE DATABASE SCOPED CREDENTIAL [<db_scoped_credential_name>]
           WITH IDENTITY = '<identity>' , SECRET = '<secret>';
       GO
       ```

       For more information about the credential, see [CREATE DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-database-scoped-credential-transact-sql.md). Creating the credential requires ALTER ANY CREDENTIAL permissions.

   - You can use a federated service account for the SQL Server to communicate with the remote Azure server when the following conditions are all true.

     - The service account under which the instance of SQL Server is running is a domain account.

     - The domain account belongs to a domain whose Active Directory is federated with Azure Active Directory.

     - The remote Azure server is configured to support Azure Active Directory authentication.

     - The service account under which the instance of SQL Server is running must be configured as a `dbmanager` or `sysadmin` account on the remote Azure server.

1. To configure a database for Stretch Database, run the ALTER DATABASE command.

   1. For the SERVER argument, provide the name of an existing Azure server, including the `.database.windows.net` portion of the name - for example, `MyStretchDatabaseServer.database.windows.net`.

   2. Provide an existing administrator credential with the CREDENTIAL argument, or specify FEDERATED_SERVICE_ACCOUNT = ON. The following example provides an existing credential.

   ```sql
   ALTER DATABASE [<database name>]
       SET REMOTE_DATA_ARCHIVE = ON
           (
               SERVER = '<server_name>' ,
               CREDENTIAL = [<db_scoped_credential_name>]
           );
   GO
   ```

## Next steps

- [Enable Stretch Database for a table](enable-stretch-database-for-a-table.md)
- [Monitor and troubleshoot data migration &#40;Stretch Database&#41;](monitor-and-troubleshoot-data-migration-stretch-database.md)
- [Pause and resume data migration &#40;Stretch Database&#41;](pause-and-resume-data-migration-stretch-database.md)
- [Manage and troubleshoot Stretch Database](manage-and-troubleshoot-stretch-database.md)
- [Back up Stretch-enabled databases](backup-stretch-enabled-databases-stretch-database.md)
- [Restore Stretch-enabled databases](restore-stretch-enabled-databases-stretch-database.md)

## See also

- [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md)
