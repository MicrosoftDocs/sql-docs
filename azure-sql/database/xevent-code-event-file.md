---
title: Create a session with an event_file target in Azure Storage
description: Provides example steps to create a database-scoped event session in Azure SQL, using Azure Storage for the event_file target.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: wiassaf, mathoma, randolphwest
ms.date: 10/22/2023
ms.service: sql-database
ms.subservice: performance
ms.topic: sample
ms.custom: sqldbrb=1
ms.devlang: PowerShell
monikerRange: "=azuresql || =azuresql-db"
---
# Create a session with an event_file target in Azure Storage

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

[!INCLUDE [sql-database-xevents-selectors-1-include](../includes/sql-database-xevents-selectors-1-include.md)]

The high-level steps in this walkthrough are:

1. Create an Azure Storage account, or find an existing suitable account to use
1. Create a container in this storage account
1. Create a SAS token with the required access for this container
1. Create a credential to store the SAS token in the database where you create the event session
1. Create, start, and use an event session

## Create a storage account and container

For a detailed description of how to create a storage account in Azure Storage, see [Create a storage account](/azure/storage/common/storage-account-create). You learn how to create a storage account using Azure portal, PowerShell, Azure SQL, an ARM template, or a Bicep template.

We recommended you use an account that:

- Is a `Standard general-purpose v2` account.
- Has its redundancy type matching the redundancy of the Azure SQL database or elastic pool where event sessions are created.
  - For [locally redundant](high-availability-sla.md#locally-redundant-availability) Azure SQL resources, use LRS, GRS, or RA-GRS. For [zone-redundant](high-availability-sla.md#zone-redundant-availability) Azure SQL resources, use ZRS, GZRS, or RA-GZRS. For more information, see [Azure Storage redundancy](/azure/storage/common/storage-redundancy).
- Uses the `Hot` [blob access tier](/azure/storage/blobs/access-tiers-overview).
- Is in the same Azure region as the Azure SQL database or elastic pool.

Next, [create a container](/azure/storage/blobs/blob-containers-portal#create-a-container) in this storage account using Azure portal. You can also create a container [using PowerShell](/azure/storage/blobs/blob-containers-powershell#create-a-container), or [using Azure CLI](/azure/storage/blobs/blob-containers-cli#create-a-container).

Note the names of the *storage account* and *container* you use.

## Create a SAS token

The [!INCLUDE [ssde-md](../../docs/includes/ssde-md.md)] running the event session needs specific access to the storage container. You grant this access by creating a [SAS token](/azure/storage/common/storage-sas-overview#sas-token) for the container. This token must satisfy the following requirements:

- Have the `rwl` (`Read`, `Write`, `List`) permissions
- Have the start time and expiry time that encompass the lifetime of the event session
- Have no IP address restrictions

In Azure portal, find the storage account and container that you created. Select the container, and navigate to **Settings > Shared access tokens**. Set **Permissions** to `Read`, `Write`, `List`, and set the **Start** and **Expiry** date and time. The SAS token you create only works within this time interval.

Select the **Generate SAS token and URL** button. The SAS token is in the **Blob SAS token** box. You can copy it to use in the next step.

> [!IMPORTANT]  
> The SAS token provides read and write access to this container. Treat it as you would treat a password or any other secret.

:::image type="content" source="media/xevents/create-sas-token.png" alt-text="Screenshot of the Shared Access Tokens screen for an Azure Storage container, with a generated SAS token for an example container.":::

## Create a database-scoped credential

Next, store the SAS token in a database-scoped [credential](/sql/relational-databases/security/authentication-access/credentials-database-engine). Using a client tool such as SSMS or ADS, connect to the database where you create the event session, and paste the following T-SQL batch into a new query window. Make sure you're connected to your user database, and not to the `master` database.

> [!NOTE]  
> Executing the following T-SQL batch requires the `CONTROL` database permission, which is held by the database owner (`dbo`), by the members of the `db_owner` database role, and by the administrator of the logical server.

```sql
/*
Create a master key to protect the secret of the credential
*/
IF NOT EXISTS (
              SELECT 1
              FROM sys.symmetric_keys
              WHERE name = '##MS_DatabaseMasterKey##'
              )
CREATE MASTER KEY;

/*
(Re-)create a database scoped credential.
The name of the credential must match the URL of the blob container.
*/
IF EXISTS (
          SELECT *
          FROM sys.database_credentials
          WHERE name = 'https://exampleaccount4xe.blob.core.windows.net/xe-example-container'
          )
    DROP DATABASE SCOPED CREDENTIAL [https://exampleaccount4xe.blob.core.windows.net/xe-example-container];

/*
The secret is the SAS token for the container. The Read, Write, and List permissions are set.
*/
CREATE DATABASE SCOPED CREDENTIAL [https://exampleaccount4xe.blob.core.windows.net/xe-example-container]
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
     SECRET = 'sp=rwl&st=2023-10-17T23:28:32Z&se=2023-10-18T07:28:32Z&spr=https&sv=2022-11-02&sr=c&sig=REDACTED';
```

Before executing this batch, make the following changes:

- In all three occurrences of `https://exampleaccount4xe.blob.core.windows.net/xe-example-container`, replace `exampleaccount4xe` with the name of your storage account, and replace `xe-example-container` with the name of your container.
- Replace the entire string between the single quotes in the `SECRET` clause with the SAS token you copied in the previous step.

## Create, start, and stop an Event session

Once the credential with the SAS token is created, you can create the event session. Creating an event session doesn't require the `CONTROL` permission. If the credential with the correct SAS token already exists in the database, you can create event sessions even if you have a more restricted set of permissions. See [permissions](xevent-db-diff-from-svr.md#permissions) for the specific permissions needed.

To create a new event session in SSMS, expand the same database in Object Explorer and then expand **Extended Events**. Right-click on the **Sessions** folder, and select **New Session...**. On the **General** page, enter a name for the session, which is `example-session` in this example. On the **Events** page, select one or more events to add to the session. In this example, we selected the `sql_batch_starting` event.

:::image type="content" source="media/xevents/create-event-session-events.png" alt-text="Screenshot of the New Session SSMS dialog showing the event selection page with the sql_batch_starting event selected.":::

On the **Data Storage** page, select `event_file` as the target type, and paste the URL of the storage container in the **Storage URL** box. Type a forward slash (`/`) at the end of this URL, followed by the file (blob) name. In our example, the blob name is `example-session.xel`, and the entire URL is `https://exampleaccount4xe.blob.core.windows.net/xe-example-container/example-session.xel`.

:::image type="content" source="media/xevents/create-event-session-data-storage-event-file.png" alt-text="Screenshot of the New Session SSMS dialog showing the data storage selection page with an event_file target selected and an entered storage URL.":::

Now that the session is configured, you can optionally select the **Script** button to create a T-SQL script of the session, to save it for later. Here's the script for our example session:

```sql
CREATE EVENT SESSION [example-session] ON DATABASE
ADD EVENT sqlserver.sql_batch_starting
ADD TARGET package0.event_file(SET filename=N'https://exampleaccount4xe.blob.core.windows.net/xe-example-container/example-session.xel')
GO
```

Select **OK** to create the session.

In Object Explorer, expand the **Sessions** folder to see the event session you created. By default, the session isn't started when it's created. To start the session, right-click on the session name, and select **Start Session**. You can later stop it by similarly selecting **Stop Session**, once the session is running.

As T-SQL batches are executed in this database, the session writes events to the `example-session.xel` blob in the `xe-example-container` storage container.

To stop the session, right-click it in Object Explorer, and select **Stop Session**.

## View event data

You can view event data in the SQL Server Management Studio (SSMS) event viewer UI, where you can use filters and aggregations to analyze the data you captured. To do that, download the `xel` blob for the session from the storage container and save it as a local file. In Azure portal, find the storage account you used, select **Containers** under **Data storage**, and select the container you created for your event session. The blob for the session has the session name as the first part of its name, with a numeric suffix. Select the ellipsis (**...**) to show the context menu for the blob, and select **Download**.

You can install [Azure Storage Explorer](https://azure.microsoft.com/products/storage/storage-explorer/) to download multiple `xel` blobs in one operation.

Once the `xel` file is downloaded, open it in SSMS. On the SSMS main menu, go to **File** and select **Open**. If you have a single `xel` file, select **File...** and browse to the file you downloaded. If you have multiple `xel` files generated by the same event session (known as rollover files), you can use the **Merge Extended Event Files...** dialog to open all of them in the event viewer.

For more information on using the event viewer in SSMS, see [View event data in SSMS](/sql/relational-databases/extended-events/advanced-viewing-of-target-data-from-extended-events-in-sql-server).

To read event session data using T-SQL, use the [sys.fn_xe_file_target_read_file()](/sql/relational-databases/extended-events/targets-for-extended-events-in-sql-server#sysfn_xe_file_target_read_file-function) function. To use this function in a database different from the one where the event session is created, [create a database-scoped credential](#create-a-database-scoped-credential) to give the [!INCLUDE [ssde-md](../../docs/includes/ssde-md.md)] access to the storage container with the event blobs.

For a more detailed walkthrough, see [Create an event session in SSMS](/sql/relational-databases/extended-events/quick-start-extended-events-in-sql-server#create-an-event-session-in-ssms).

## Related content

- [Extended Events in Azure SQL Database](xevent-db-diff-from-svr.md)
- [Extended Events](/sql/relational-databases/extended-events/extended-events)
- [event_file target](/sql/relational-databases/extended-events/targets-for-extended-events-in-sql-server#event_file-target)
- [CREATE EVENT SESSION (Transact-SQL)](/sql/t-sql/statements/create-event-session-transact-sql)
- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](/sql/t-sql/statements/create-database-scoped-credential-transact-sql)
