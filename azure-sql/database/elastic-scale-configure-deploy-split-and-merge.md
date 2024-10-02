---
title: Deploy a split-merge service
description: Use the split-merge too to move data between sharded databases.
author: bgavrilovicMS
ms.author: bgavrilovic
ms.reviewer: wiassaf, mathoma, randolphwest
ms.date: 08/13/2024
ms.service: azure-sql-database
ms.subservice: scale-out
ms.topic: how-to
ms.custom:
  - sqldbrb=1
---
# Deploy a split-merge service to move data between sharded databases

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

The split-merge tool lets you move data between sharded databases. See [Moving data between scaled-out cloud databases](elastic-scale-overview-split-and-merge.md).

> [!NOTE]
> The split-merge tool is intended for Azure Web Apps. The end of life for the Cloud Services (Classic) is August 31, 2024. If you were using the split-merge tool on Cloud Services (Classic), migrate to Azure Web Apps before August 31, 2024.

## Prerequisites

1. Create a SQL database to be used as the split-merge status database. Go to the [Azure portal](https://portal.azure.com). Create a new **SQL database**. Name the database and create a new administrator and password. Be sure to record the name and password for later use.

1. Ensure that your [logical server in Azure](logical-servers.md) allows Azure Services to connect to it. In the Azure portal, in the **Firewall Settings** for your logical server, ensure the **Allow access to Azure Services** setting is set to **On**. Select the **Save** icon.

1. Create an Azure Storage account for diagnostics output.

1. Use the public split-merge docker images, or push split-merge docker images to either Azure Container Service or your docker registry of choice.

## Create two Azure Web Apps for your service

Create two Web Apps - a `worker` and `UI` web app.

### Worker web app

1. Create a [Web App in the Azure portal](https://portal.azure.com/#create/Microsoft.WebSite).

1. In the **Publish** field, select **Container**. 

1. For **Operating System**, select **Windows**. 

1. Proceed to the **Docker** tab. 

1. Populate the following information:  
**Image source**: `Docker hub`  
**Access type**: `Public`  
**Image and tag**: `mcr.microsoft.com/splitmerge/splitmergeworker:20240812.1`

1. Use **Review + create** to create the web app. 

### UI Web App

To create the UI web app, follow the same steps you used to create the Worker web app with one difference: 
- A different docker image in the **Image and tag** field: `mcr.microsoft.com/splitmerge/splitmergeweb:20240812.1`

## Configure your Split-Merge web apps

### Configure security

For detailed instructions to configure the security of the service, refer to the [Split-Merge security configuration](elastic-scale-split-merge-security-configuration.md).

For the purposes of a simple test deployment for this tutorial, a minimal set of configuration steps are performed to get the service up and running. These steps enable only the one machine/account executing them to communicate with the service.

### Create a self-signed certificate and PFX file

Use PowerShell to create a self-signed certificate and PFX file. 

First, create a new directory. Then replace the inline values accordingly and run the following PowerShell commands from the new directory: 

```powershell
  $cert = New-SelfSignedCertificate -Subject "CN=*.cloudapp.net" -CertStoreLocation "Cert:\CurrentUser\My" -KeyExportPolicy Exportable -KeySpec Signature -KeyLength 2048 -KeyAlgorithm RSA -HashAlgorithm SHA256
  $mypwd = ConvertTo-SecureString -String "{myPassword}" -Force -AsPlainText  ## Replace {myPassword}
  Export-PfxCertificate -Cert $cert -FilePath "C:\Users\admin\Desktop\$certname.pfx" -Password $mypwd   ## Specify your preferred location
```

### Upload the PFX file to the web apps and enable certificate usage

Repeat the following steps for both `worker` and `UI` Web Apps.

1. Go to the [Azure portal](https://portal.azure.com).
1. Select **App Services**.
1. Select the Web App you created above for the split-merge tool.
1. Select **Certificates** from the menu.
1. Select **Bring your own certificates (.pfx)**.
1. Select **Add certificate** from the bar.
1. Select the PFX file and enter the same password as above.
1. Once completed, copy the certificate thumbprint from the new entry in the list.
1. In the Web App menu, open **Settings** / **Configuration**. 
1. Set **Client certificate mode** to `Require`. 

### Web app configuration

Repeat the following steps for both `worker` and `UI` web apps.

1. Open the deployed Web App and go to **Settings** >  **Environment variables** > **App settings**. Select **Add**. 

1. Add a variable with the name **ElasticScaleMetadata** and the value with the connection string for the previously deployed status database.

   > [!IMPORTANT]  
   > At this time, the status database must use the Latin collation (`SQL\_Latin1\_General\_CP1\_CI\_AS`). For more information, see [Windows Collation Name](/sql/t-sql/statements/windows-collation-name-transact-sql).

   With Azure SQL Database, the connection string typically is in the form:

   `Server=<serverName>.database.windows.net; Database=<databaseName>;User ID=<userId>; Password=<password>; Encrypt=True; Connection Timeout=30`

1. Add additional variables:

    | Name | Value |
    |----------|----------|
    | WorkerRoleSynchronizationStorageAccountConnectionString    | Valid connection string to the previously created Azure storage.    |
    | DataEncryptionPrimaryCertificateThumbprint    | Previously generated certificate thumbprint.  |
    | MetadataExpirationPeriodInMinutes    | 20160  |
    | MaxRetryCount    | 5  |
    | WEBSITE_LOAD_CERTIFICATES    | *  |
    | WEBSITE_PULL_IMAGE_OVER_VNET    | 0 |

1. Select **Apply** and restart the application. 

1. Repeat the same steps for both `worker` and `UI` web app.

## Troubleshoot the deployment

If your web role fails to come online, it's likely a problem with the security configuration. Check that the TLS/SSL is configured as described previously.

If your worker role fails to come online, but your web role succeeds, it's most likely a problem connecting to the status database that you created earlier.

- Make sure that the connection string is accurate.
- Check that the server and database exist, and that the user ID and password are correct.
- For Azure SQL Database, the connection string should be in the form:

   `Server=<serverName>.database.windows.net; Database=<databaseName>;User ID=<user>; Password=<password>; Encrypt=True; Connection Timeout=30`

- Ensure that the server name doesn't begin with `https://`.
- Ensure that your server allows Azure Services to connect to it. To do this, open your database in the portal and ensure that the **Allow access to Azure Services** setting is set to **On**.

## Test the service deployment

### Connect with a web browser

Go to the **Overview** of your `UI` Web App and select **Browse**. Choose the correct certificate, if prompted.

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

## Known errors

You might see the following message when running the sample PowerShell scripts:

`Invoke-WebRequest : The underlying connection was closed: Could not establish trust relationship for the SSL/TLS secure channel.`

This error means that your TLS/SSL certificate isn't configured correctly. Follow the instructions in the section [Connect with a web browser](#connect-with-a-web-browser).

If you can't submit requests, you might see this:

`[Exception] System.Data.SqlClient.SqlException (0x80131904): Could not find stored procedure 'dbo.InsertRequest'.`

In this case, check your configuration file, in particular the setting for `WorkerRoleSynchronizationStorageAccountConnectionString`. This error typically indicates that the worker role couldn't successfully initialize the metadata database on first use.

[!INCLUDE [elastic-scale-include](../includes/elastic-scale-include.md)]
