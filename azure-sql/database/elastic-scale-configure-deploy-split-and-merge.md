---
title: Deploy a split-merge service
description: Use the split-merge too to move data between sharded databases.
author: bgavrilMS
ms.author: bogavril
ms.reviewer: wiassaf, mathoma, randolphwest
ms.date: 06/25/2024
ms.service: azure-sql-database
ms.subservice: scale-out
ms.topic: how-to
ms.custom:
  - sqldbrb=1
---
# Deploy a split-merge service to move data between sharded databases

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

The split-merge tool lets you move data between sharded databases. See [Moving data between scaled-out cloud databases](elastic-scale-overview-split-and-merge.md).

> [!NOTE]  
> The split-merge tool is intended to work with Cloud Services (Classic) and not App Services.

## Download the Split-Merge packages

1. Download the latest NuGet version from [NuGet](/nuget/install-nuget-client-tools).

1. Open a command prompt and navigate to the directory where you downloaded **nuget.exe**. The download includes PowerShell commands.

1. Download the latest Split-Merge package into the current directory with the following command:

   ```cmd
   nuget install Microsoft.Azure.SqlDatabase.ElasticScale.Service.SplitMerge
   ```

The files are placed in a directory named `Microsoft.Azure.SqlDatabase.ElasticScale.Service.SplitMerge.<x.x.xxx.x>` where `<x.x.xxx.x>` reflects the version number. Find the split-merge service files in the `content\splitmerge\service` subdirectory, and the Split-Merge PowerShell scripts (and required client DLLs) in the `content\splitmerge\powershell` subdirectory.

## Prerequisites

1. Create an Azure SQL Database database that will be used as the split-merge status database. Go to the [Azure portal](https://portal.azure.com). Create a new **SQL Database**. Give the database a name and create a new administrator and password. Be sure to record the name and password for later use.

1. Ensure that your server allows Azure Services to connect to it. In the portal, in the **Firewall Settings**, ensure the **Allow access to Azure Services** setting is set to **On**. Select the **Save** icon.

1. Create an Azure Storage account for diagnostics output.

1. Create an Azure Cloud Service for your Split-Merge service.

## Configure your Split-Merge service

### Split-Merge service configuration

1. In the folder into which you downloaded the Split-Merge assemblies, create a copy of the `ServiceConfiguration.Template.cscfg` file that shipped alongside `SplitMergeService.cspkg` and rename it `ServiceConfiguration.cscfg`.

1. Open `ServiceConfiguration.cscfg` in a text editor such as Visual Studio that validates inputs such as the format of certificate thumbprints.

1. Create a new database or choose an existing database to serve as the status database for Split-Merge operations and retrieve the connection string of that database.

   > [!IMPORTANT]  
   > At this time, the status database must use the Latin collation (`SQL_Latin1_General_CP1_CI_AS`). For more information, see [Windows Collation Name](/sql/t-sql/statements/windows-collation-name-transact-sql).

   With Azure SQL Database, the connection string typically is of the form:

   `Server=<serverName>.database.windows.net; Database=<databaseName>;User ID=<userId>; Password=<password>; Encrypt=True; Connection Timeout=30`

1. Enter this connection string in the `.cscfg` file in both the **SplitMergeWeb** and **SplitMergeWorker** role sections in the ElasticScaleMetadata setting.

1. For the **SplitMergeWorker** role, enter a valid connection string to Azure storage for the `WorkerRoleSynchronizationStorageAccountConnectionString` setting.

### Configure security

For detailed instructions to configure the security of the service, refer to the [Split-Merge security configuration](elastic-scale-split-merge-security-configuration.md).

For the purposes of a simple test deployment for this tutorial, a minimal set of configuration steps are performed to get the service up and running. These steps enable only the one machine/account executing them to communicate with the service.

### Create a self-signed certificate

Create a new directory and from this directory execute the following command using a [Developer Command Prompt for Visual Studio](/dotnet/framework/tools/developer-command-prompt-for-vs) window:

```cmd
makecert ^
-n "CN=*.cloudapp.net" ^
-r -cy end -sky exchange -eku "1.3.6.1.5.5.7.3.1,1.3.6.1.5.5.7.3.2" ^
-a sha256 -len 2048 ^
-sr currentuser -ss root ^
-sv MyCert.pvk MyCert.cer
```

At the password prompt to protect the private key, enter a strong password and confirm it. Enter the password again when prompted. Select **Yes** at the end to import it to the Trusted Certification Authorities Root store.

### Create a PFX file

Execute the following command from the same window where makecert was executed; use the same password that you used to create the certificate:

```cmd
pvk2pfx -pvk MyCert.pvk -spc MyCert.cer -pfx MyCert.pfx -pi <password>
```

### Import the client certificate into the personal store

1. In Windows Explorer, double-click `MyCert.pfx`.
1. In the **Certificate Import Wizard**, select **Current User**, then **Next**.
1. Confirm the file path and select **Next**.
1. Type the password, leave **Include all extended properties** checked, and select **Next**.
1. Leave **Automatically select the certificate store[...]** checked, and select **Next**.
1. Select **Finish** and **OK**.

### Upload the PFX file to the cloud service

1. Go to the [Azure portal](https://portal.azure.com).
1. Select **Cloud Services**.
1. Select the cloud service you created previously for the Split-Merge service.
1. Select **Certificates** on the top menu.
1. Select **Upload** in the bottom bar.
1. Select the PFX file and enter the same password as before.
1. Once completed, copy the certificate thumbprint from the new entry in the list.

### Update the service configuration file

Paste the certificate thumbprint copied previously into the thumbprint/value attribute of these settings.
For the worker role:

```xml
<Setting name="DataEncryptionPrimaryCertificateThumbprint" value="" />
<Certificate name="DataEncryptionPrimary" thumbprint="" thumbprintAlgorithm="sha1" />
```

For the web role:

```xml
<Setting name="AdditionalTrustedRootCertificationAuthorities" value="" />
<Setting name="AllowedClientCertificateThumbprints" value="" />
<Setting name="DataEncryptionPrimaryCertificateThumbprint" value="" />
<Certificate name="SSL" thumbprint="" thumbprintAlgorithm="sha1" />
<Certificate name="CA" thumbprint="" thumbprintAlgorithm="sha1" />
<Certificate name="DataEncryptionPrimary" thumbprint="" thumbprintAlgorithm="sha1" />
```

For production deployments separate certificates should be used for the CA, for encryption, the Server certificate and client certificates. For detailed instructions on this, see [Security Configuration](elastic-scale-split-merge-security-configuration.md).

## Deploy your service

1. Go to the [Azure portal](https://portal.azure.com)
1. Select the cloud service that you created earlier.
1. Select **Overview**.
1. Choose the staging environment, then select **Upload**.
1. In the dialog box, enter a deployment label. For both `Package` and `Configuration`, select `From Local` and choose the `SplitMergeService.cspkg` file and your cscfg file that you configured earlier.
1. Ensure that the checkbox labeled **Deploy even if one or more roles contain a single instance** is checked.
1. Hit the check button in the bottom right to begin the deployment. Expect it to take a few minutes to complete.

## Troubleshoot the deployment

If your web role fails to come online, it's likely a problem with the security configuration. Check that the TLS/SSL is configured as described previously.

If your worker role fails to come online, but your web role succeeds, it's most likely a problem connecting to the status database that you created earlier.

- Make sure that the connection string in your cscfg is accurate.
- Check that the server and database exist, and that the user ID and password are correct.
- For Azure SQL Database, the connection string should be of the form:

  `Server=<serverName>.database.windows.net; Database=<databaseName>;User ID=<user>; Password=<password>; Encrypt=True; Connection Timeout=30`

- Ensure that the server name doesn't begin with `https://`.
- Ensure that your server allows Azure Services to connect to it. To do this, open your database in the portal and ensure that the **Allow access to Azure Services** setting is set to **On****.

## Test the service deployment

### Connect with a web browser

Determine the web endpoint of your Split-Merge service. You can find this in the portal by going to the **Overview** of your cloud service and looking under **Site URL** on the right side. Replace `http://` with `https://`, since the default security settings disable the HTTP endpoint. Load the page for this URL into your browser.

### Test with PowerShell scripts

The deployment and your environment can be tested by running the included sample PowerShell scripts.

> [!IMPORTANT]  
> The sample scripts run on PowerShell 5.1. They don't currently run on PowerShell 6 or later.

The script files included are:

1. `SetupSampleSplitMergeEnvironment.ps1` - sets up a test data tier for split-merge.

    1. Creates a shard map manager database.
    1. Creates two shard databases.
    1. Creates a shard map for those databases (deletes any existing shard maps on those databases).
    1. Creates a small sample table in both the shards, and populates the table in one of the shards.
    1. Declares the SchemaInfo for the sharded table.

1. `ExecuteSampleSplitMerge.ps1` - executes test operations on the test data tier.

    1. Sends a split request to the Split-Merge Service web frontend, which splits half the data from the first shard to the second shard.
    1. Polls the web frontend for the split request status and waits until the request completes.
    1. Sends a merge request to the Split-Merge Service web frontend, which moves the data from the second shard back to the first shard.
    1. Polls the web frontend for the merge request status and waits until the request completes.

1. `GetMappings.ps1` - top-level sample script that prints the current state of the shard mappings.
1. `ShardManagement.psm1` - helper script that wraps the ShardManagement API.
1. `SqlDatabaseHelpers.psm1` - helper script for creating and managing databases in SQL Database.

## Use PowerShell to verify your deployment

1. Open a new PowerShell window and navigate to the directory where you downloaded the Split-Merge package, and then navigate to the "PowerShell" directory.

1. Create a server (or choose an existing server) where the shard map manager and shards will be created.

   > [!NOTE]  
   > The `SetupSampleSplitMergeEnvironment.ps1` script creates all these databases on the same server by default to keep the script simple. This isn't a restriction of the Split-Merge Service itself.

   A SQL authentication login with read/write access to the databases is needed for the Split-Merge service to move data and update the shard map. Since the Split-Merge Service runs in the cloud, it doesn't currently support Integrated Authentication.

   Make sure the server is configured to allow access from the IP address of the machine running these scripts. You can find this setting under SQL server / Firewalls and virtual networks / Client IP addresses.

1. Execute the `SetupSampleSplitMergeEnvironment.ps1` script to create the sample environment.

   Running this script wipes out any existing shard map management data structures on the shard map manager database and the shards. It might be useful to rerun the script if you wish to reinitialize the shard map or shards.

   Sample command line:

   ```cmd
   .\SetupSampleSplitMergeEnvironment.ps1 ^
   -UserName 'mysqluser' -Password 'MySqlPassw0rd' -ShardMapManagerServerName 'abcdefghij.database.windows.net'
   ```

1. Execute the Getmappings.ps1 script to view the mappings that currently exist in the sample environment.

   ```cmd
   .\GetMappings.ps1 ^
   -UserName 'mysqluser' -Password 'MySqlPassw0rd' -ShardMapManagerServerName 'abcdefghij.database.windows.net'
   ```

1. Execute the `ExecuteSampleSplitMerge.ps1` script to execute a split operation (moving half the data on the first shard to the second shard) and then a merge operation (moving the data back onto the first shard). If you configured TLS and left the http endpoint disabled, ensure that you use the https:// endpoint instead.

   Sample command line:

   ```cmd
   .\ExecuteSampleSplitMerge.ps1 ^
   -UserName 'mysqluser' -Password 'MySqlPassw0rd' ^
   -ShardMapManagerServerName 'abcdefghij.database.windows.net' ^
   -SplitMergeServiceEndpoint 'https://mysplitmergeservice.cloudapp.net' ^
   -CertificateThumbprint '0123456789abcdef0123456789abcdef01234567'
   ```

   If you receive the following error, it's most likely a problem with your Web endpoint's certificate. Try connecting to the Web endpoint with your favorite Web browser and check if there's a certificate error.

   `Invoke-WebRequest : The underlying connection was closed: Could not establish trust relationship for the SSL/TLSsecure channel.`

   If it succeeds, the output should look like the following output:

   ```output
   .\ExecuteSampleSplitMerge.ps1 -UserName 'mysqluser' -Password 'MySqlPassw0rd' -ShardMapManagerServerName 'abcdefghij.database.windows.net' -SplitMergeServiceEndpoint 'http://mysplitmergeservice.cloudapp.net' -CertificateThumbprint 0123456789abcdef0123456789abcdef01234567
   Sending split request
   Began split operation with id dc68dfa0-e22b-4823-886a-9bdc903c80f3
   Polling split-merge request status. Press Ctrl-C to end
   Progress: 0% | Status: Queued | Details: [Informational] Queued request
   Progress: 5% | Status: Starting | Details: [Informational] Starting split-merge state machine for request.
   Progress: 5% | Status: Starting | Details: [Informational] Performing data consistency checks on target     shards.
   Progress: 20% | Status: CopyingReferenceTables | Details: [Informational] Moving reference tables from     source to target shard.
   Progress: 20% | Status: CopyingReferenceTables | Details: [Informational] Waiting for reference tables copy     completion.
   Progress: 20% | Status: CopyingReferenceTables | Details: [Informational] Moving reference tables from     source to target shard.
   Progress: 44% | Status: CopyingShardedTables | Details: [Informational] Moving key range [100:110) of     Sharded tables
   Progress: 44% | Status: CopyingShardedTables | Details: [Informational] Successfully copied key range     [100:110) for table [dbo].[MyShardedTable]
   ...
   ...
   Progress: 90% | Status: Completing | Details: [Informational] Successfully deleted shardlets in table     [dbo].[MyShardedTable].
   Progress: 90% | Status: Completing | Details: [Informational] Deleting any temp tables that were created     while processing the request.
   Progress: 100% | Status: Succeeded | Details: [Informational] Successfully processed request.
   Sending merge request
   Began merge operation with id 6ffc308f-d006-466b-b24e-857242ec5f66
   Polling request status. Press Ctrl-C to end
   Progress: 0% | Status: Queued | Details: [Informational] Queued request
   Progress: 5% | Status: Starting | Details: [Informational] Starting split-merge state machine for request.
   Progress: 5% | Status: Starting | Details: [Informational] Performing data consistency checks on target     shards.
   Progress: 20% | Status: CopyingReferenceTables | Details: [Informational] Moving reference tables from     source to target shard.
   Progress: 44% | Status: CopyingShardedTables | Details: [Informational] Moving key range [100:110) of     Sharded tables
   Progress: 44% | Status: CopyingShardedTables | Details: [Informational] Successfully copied key range     [100:110) for table [dbo].[MyShardedTable]
   ...
   ...
   Progress: 90% | Status: Completing | Details: [Informational] Successfully deleted shardlets in table     [dbo].[MyShardedTable].
   Progress: 90% | Status: Completing | Details: [Informational] Deleting any temp tables that were created     while processing the request.
   Progress: 100% | Status: Succeeded | Details: [Informational] Successfully processed request.
   ```

1. Experiment with other data types. All of these scripts take an optional -ShardKeyType parameter that allows you to specify the key type. The default is Int32, but you can also specify Int64, Guid, or Binary.

## Create requests

The service can be used either by using the web UI, or by importing and using the `SplitMerge.psm1` PowerShell module, which submits your requests through the web role.

The service can move data in both sharded tables and reference tables. A sharded table has a sharding key column and has different row data on each shard. A reference table isn't sharded so it contains the same row data on every shard. Reference tables are useful for data that doesn't change often and is used to JOIN with sharded tables in queries.

In order to perform a split-merge operation, you must declare the sharded tables and reference tables that you want to move. This is accomplished with the **SchemaInfo** API. This API is in the `Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement.Schema` namespace.

1. For each sharded table, create a **ShardedTableInfo** object describing the table's parent schema name (optional, defaults to "dbo"), the table name, and the column name in that table that contains the sharding key.
1. For each reference table, create a **ReferenceTableInfo** object describing the table's parent schema name (optional, defaults to "dbo") and the table name.
1. Add the previous TableInfo objects to a new **SchemaInfo** object.
1. Get a reference to a **ShardMapManager** object, and call **GetSchemaInfoCollection**.
1. Add the **SchemaInfo** to the **SchemaInfoCollection**, providing the shard map name.

An example of this can be seen in the SetupSampleSplitMergeEnvironment.ps1 script.

The Split-Merge service doesn't create the target database (or schema for any tables in the database) for you. They must be precreated before sending a request to the service.

## Troubleshooting

You might see the following message when running the sample PowerShell scripts:

`Invoke-WebRequest : The underlying connection was closed: Could not establish trust relationship for the SSL/TLS secure channel.`

This error means that your TLS/SSL certificate isn't configured correctly. Follow the instructions in the section [Connect with a web browser](#connect-with-a-web-browser).

If you can't submit requests, you might see this:

`[Exception] System.Data.SqlClient.SqlException (0x80131904): Could not find stored procedure 'dbo.InsertRequest'.`

In this case, check your configuration file, in particular the setting for `WorkerRoleSynchronizationStorageAccountConnectionString`. This error typically indicates that the worker role couldn't successfully initialize the metadata database on first use.

[!INCLUDE [elastic-scale-include](../includes/elastic-scale-include.md)]
