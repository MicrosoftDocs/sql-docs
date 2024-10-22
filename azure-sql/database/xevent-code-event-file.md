---
title: Create an event session with an event_file target
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: Provides example steps to create an event session in Azure SQL, using Azure Storage for the event_file target.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: wiassaf, mathoma, randolphwest
ms.date: 10/21/2024
ms.service: azure-sql
ms.subservice: performance
ms.topic: sample
ms.custom: sqldbrb=1
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---
# Create an event session with an event_file target in Azure Storage

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

[!INCLUDE [sql-database-xevents-selectors-1-include](../includes/sql-database-xevents-selectors-1-include.md)]

The high-level steps in this walkthrough are:

1. Create an Azure Storage account, or find an existing suitable account to use.
1. Create a container in this storage account.
1. Grant the required access to the container using either an RBAC role assignment, or a SAS token.
1. Create a credential in the database or managed instance where you create the event session.
1. Create, start, and use an event session.

## Create a storage account and container

For a detailed description of how to create a storage account in Azure Storage, see [Create a storage account](/azure/storage/common/storage-account-create). You learn how to create a storage account using Azure portal, PowerShell, Azure SQL, an ARM template, or a Bicep template.

We recommended you use an account that:

- Is a `Standard general-purpose v2` account.
- Has its redundancy type matching the redundancy of the Azure SQL database, elastic pool, or managed instance where event sessions are created.
  - For [locally redundant](high-availability-sla-local-zone-redundancy.md#locally-redundant-availability) Azure SQL resources, use LRS, GRS, or RA-GRS. For [zone-redundant](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability) Azure SQL resources, use ZRS, GZRS, or RA-GZRS. For more information, see [Azure Storage redundancy](/azure/storage/common/storage-redundancy).
- Uses the `Hot` [blob access tier](/azure/storage/blobs/access-tiers-overview).
- Is in the same Azure region as the Azure SQL database, elastic pool, or managed instance.

Next, [create a container](/azure/storage/blobs/blob-containers-portal#create-a-container) in this storage account using Azure portal. You can also create a container [using PowerShell](/azure/storage/blobs/blob-containers-powershell#create-a-container), or [using Azure CLI](/azure/storage/blobs/blob-containers-cli#create-a-container).

Note the names of the *storage account* and *container* you use.

## Grant access to the container

To read and write event data, the [!INCLUDE [ssde-md](../../docs/includes/ssde-md.md)] requires specific access to the container. You can grant this access in one of two ways, depending on your choice of authentication type:

  - If using managed identity with Microsoft Entra authentication, you assign the **Storage Blob Data Contributor** RBAC role to the [managed identity](authentication-azure-ad-user-assigned-managed-identity.md) of the Azure SQL logical server or Azure SQL managed instance on the container.

    > [!NOTE]
    >
    > The use of managed identity with extended event sessions is in preview.

  - If using secret-based authentication, you create a [SAS token](/azure/storage/common/storage-sas-overview#sas-token) for the container.

    To use this authentication type, the **Allow storage account key access** option must be enabled. For more information, see [Prevent Shared Key authorization for an Azure Storage account](/azure/storage/common/shared-key-authorization-prevent).

### Grant access using managed identity

1. In the Azure portal, navigate to the **Identity** page of your Azure SQL logical server or Azure SQL managed instance, and make sure that a managed identity is assigned. For more information, see [Managed identities in Microsoft Entra for Azure SQL](authentication-azure-ad-user-assigned-managed-identity.md).

    Note the name of the managed identity of your logical server or SQL managed instance. If the system assigned identity is enabled, use the name of the logical server or SQL managed instance. If the system assigned identity is disabled and a user assigned identity is added and designated as the primary identity, use the name of that user assigned identity.

1. In the Azure portal, navigate to the storage container where you want to store event data. On the **Access Control (IAM)** page, select **Add** to assign the **Storage Blob Data Contributor** RBAC role to the managed identity of the logical server or SQL managed instance. For more information, see [Assign an Azure role for access to blob data](/azure/storage/blobs/assign-azure-role-data-access).

1. Create a credential to instruct the [!INCLUDE [ssde-md](../../docs/includes/ssde-md.md)] to use managed identity to authenticate to Azure Storage.

    # [SQL Database](#tab/sqldb)

    Create a database-scoped [credential](/sql/relational-databases/security/authentication-access/credentials-database-engine). Using a client tool such as SSMS or ADS, open a new query window, connect to the database where you create the event session, and paste the following T-SQL batch. Make sure you're connected to your user database, and not to the `master` database.

    > [!NOTE]  
    > Executing the following T-SQL batch requires the `CONTROL` database permission, which is held by the database owner (`dbo`), by the members of the `db_owner` database role, and by the administrator of the logical server.

    ```sql
    /*
    Create a master key
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
              SELECT 1
              FROM sys.database_credentials
              WHERE name = 'https://exampleaccount4xe.blob.core.windows.net/xe-example-container'
              )
        DROP DATABASE SCOPED CREDENTIAL [https://exampleaccount4xe.blob.core.windows.net/xe-example-container];

    /*
    When using managed identity, the credential does not contain a secret
    */
    CREATE DATABASE SCOPED CREDENTIAL [https://exampleaccount4xe.blob.core.windows.net/xe-example-container]
    WITH IDENTITY = 'MANAGED IDENTITY';
    ```

    Before executing this batch, make the following changes:

    - In all three occurrences of `https://exampleaccount4xe.blob.core.windows.net/xe-example-container`, replace `exampleaccount4xe` with the name of your storage account, and replace `xe-example-container` with the name of your container.

    # [SQL Managed Instance](#tab/sqlmi)

    Create a server-scoped [credential](/sql/relational-databases/security/authentication-access/credentials-database-engine). Using a client tool such as SSMS or ADS, open a new query window, connect it to the `master` database on the managed instance where you create the event session, and paste the following T-SQL batch.

    > [!NOTE]
    > Executing the following T-SQL batch requires the `CONTROL` database permission in the `master` database, which is held by the members of the `db_owner` database role in `master`, and by the members of the `sysadmin` server role on the managed instance.

    ```sql
    /*
    Create a master key
    */
    IF NOT EXISTS (
                  SELECT 1
                  FROM sys.symmetric_keys
                  WHERE name = '##MS_DatabaseMasterKey##'
                  )
    CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'password-placeholder';

    /*
    (Re-)create a credential.
    The name of the credential must match the URL of the blob container.
    */
    IF EXISTS (
              SELECT 1
              FROM sys.credentials
              WHERE name = 'https://exampleaccount4xe.blob.core.windows.net/xe-example-container'
              )
        DROP CREDENTIAL [https://exampleaccount4xe.blob.core.windows.net/xe-example-container];

    /*
    When using managed identity, the credential does not contain a secret
    */
    CREATE CREDENTIAL [https://exampleaccount4xe.blob.core.windows.net/xe-example-container]
    WITH IDENTITY = 'MANAGED IDENTITY';
    ```

    Before executing this batch, make the following changes:

    - In the `CREATE MASTER KEY` statement, replace `password-placeholder` with an actual password that will protect the master key. For more information, see [CREATE MASTER KEY](/sql/t-sql/statements/create-master-key-transact-sql).
    - In all three occurrences of `https://exampleaccount4xe.blob.core.windows.net/xe-example-container`, replace `exampleaccount4xe` with the name of your storage account, and replace `xe-example-container` with the name of your container.

    ---

### Grant access using a SAS token

1. In Azure portal, find the storage account and container that you created. Select the container, and navigate to **Settings > Shared access tokens**. Set **Permissions** to `Read`, `Write`, `List` and set the **Start** and **Expiry** date and time. The SAS token you create only works within this time interval.

    The SAS token must satisfy the following additional requirements:

      - Have the start time and expiry time that encompass the lifetime of the event session
      - Have no IP address restrictions

    Select the **Generate SAS token and URL** button. The SAS token is in the **Blob SAS token** box. You can copy it to use in the next step.

    > [!IMPORTANT]  
    > The SAS token provides read and write access to this container. Treat it as you would treat a password or any other secret.

    :::image type="content" source="media/xevents/create-sas-token.png" alt-text="Screenshot of the Shared Access Tokens screen for an Azure Storage container, with a generated SAS token for an example container.":::

1. Create a credential to store the SAS token

    # [SQL Database](#tab/sqldb)

    Store the SAS token in a database-scoped [credential](/sql/relational-databases/security/authentication-access/credentials-database-engine). Using a client tool such as SSMS or ADS, open a new query window, connect to the database where you create the event session, and paste the following T-SQL batch. Make sure you're connected to your user database, and not to the `master` database.

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
              SELECT 1
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

    # [SQL Managed Instance](#tab/sqlmi)

    Store the SAS token in a server-scoped [credential](/sql/relational-databases/security/authentication-access/credentials-database-engine). Using a client tool such as SSMS or ADS, open a new query window, connect it to the `master` database on the managed instance where you create the event session, and paste the following T-SQL batch.

    > [!NOTE]
    > Executing the following T-SQL batch requires the `CONTROL` database permission in the `master` database, which is held by the members of the `db_owner` database role in `master`, and by the members of the `sysadmin` server role on the managed instance.

    ```sql
    /*
    Create a master key to protect the secret of the credential
    */
    IF NOT EXISTS (
                  SELECT 1
                  FROM sys.symmetric_keys
                  WHERE name = '##MS_DatabaseMasterKey##'
                  )
    CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'password-placeholder';

    /*
    (Re-)create a credential.
    The name of the credential must match the URL of the blob container.
    */
    IF EXISTS (
              SELECT 1
              FROM sys.credentials
              WHERE name = 'https://exampleaccount4xe.blob.core.windows.net/xe-example-container'
              )
        DROP CREDENTIAL [https://exampleaccount4xe.blob.core.windows.net/xe-example-container];

    /*
    The secret is the SAS token for the container. The Read, Write, and List permissions are set.
    */
    CREATE CREDENTIAL [https://exampleaccount4xe.blob.core.windows.net/xe-example-container]
    WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
        SECRET = 'sp=rwl&st=2023-10-17T23:28:32Z&se=2023-10-18T07:28:32Z&spr=https&sv=2022-11-02&sr=c&sig=REDACTED';
    ```

    Before executing this batch, make the following changes:

    - In the `CREATE MASTER KEY` statement, replace `password-placeholder` with an actual password that will protect the master key. For more information, see [CREATE MASTER KEY](/sql/t-sql/statements/create-master-key-transact-sql).
    - In all three occurrences of `https://exampleaccount4xe.blob.core.windows.net/xe-example-container`, replace `exampleaccount4xe` with the name of your storage account, and replace `xe-example-container` with the name of your container.
    - Replace the entire string between the single quotes in the `SECRET` clause with the SAS token you copied in the previous step.

    ---

## Create, start, and stop an event session

Once the credential is created, you can create the event session. Creating an event session doesn't require the `CONTROL` permission. Once the credential is created, you can create event sessions even if you have a more restricted set of permissions. See [permissions](xevent-db-diff-from-svr.md#permissions) for the specific permissions needed.

To create a new event session in SSMS, expand the **Extended Events** node. This node is under the database folder in Azure SQL Database, and under the **Management** folder in Azure SQL Managed Instance. Right-click on the **Sessions** folder, and select **New Session...**. On the **General** page, enter a name for the session, which is `example-session` in this example. On the **Events** page, select one or more events to add to the session. In this example, we selected the `sql_batch_starting` event.

:::image type="content" source="media/xevents/create-event-session-events.png" alt-text="Screenshot of the New Session SSMS dialog showing the event selection page with the sql_batch_starting event selected.":::

On the **Data Storage** page, select `event_file` as the target type, and paste the URL of the storage container in the **Storage URL** box. Type a forward slash (`/`) at the end of this URL, followed by the file (blob) name. In our example, the blob name is `example-session.xel`, and the entire URL is `https://exampleaccount4xe.blob.core.windows.net/xe-example-container/example-session.xel`.

> [!NOTE]
> For SQL Managed Instance, instead of pasting the storage container URL on the **Data storage** page, use the **Script** button to create a T-SQL script of the session. Specify the container URL as the value for the `filename` argument, similar to the SQL Managed Instance example below, and execute the script to create the session.

:::image type="content" source="media/xevents/create-event-session-data-storage-event-file.png" alt-text="Screenshot of the New Session SSMS dialog showing the data storage selection page with an event_file target selected and an entered storage URL.":::

Now that the session is configured, you can select the **Script** button to create a T-SQL script of the session, to save it for later. Here's the script for our example session:

# [SQL Database](#tab/sqldb)

```sql
CREATE EVENT SESSION [example-session] ON DATABASE
ADD EVENT sqlserver.sql_batch_starting
ADD TARGET package0.event_file(SET filename=N'https://exampleaccount4xe.blob.core.windows.net/xe-example-container/example-session.xel')
GO
```

# [SQL Managed Instance](#tab/sqlmi)

```sql
CREATE EVENT SESSION [example-session] ON SERVER
ADD EVENT sqlserver.sql_batch_starting
ADD TARGET package0.event_file(SET filename=N'https://exampleaccount4xe.blob.core.windows.net/xe-example-container/example-session.xel')
GO
```

---

Select **OK** to create the session.

In Object Explorer, expand the **Sessions** folder to see the event session you created. By default, the session isn't started when it's created. To start the session, right-click on the session name, and select **Start Session**. You can later stop it by similarly selecting **Stop Session**, once the session is running.

As T-SQL batches are executed in this database or managed instance, the session writes events to the `example-session.xel` blob in the `xe-example-container` storage container.

To stop the session, right-click it in Object Explorer, and select **Stop Session**.

## View event data

You can view event data in the SQL Server Management Studio (SSMS) event viewer UI, where you can use filters and aggregations to analyze the data you captured. For more information on using the event viewer in SSMS, see [View event data in SSMS](/sql/relational-databases/extended-events/advanced-viewing-of-target-data-from-extended-events-in-sql-server).

### Download xel files from Azure storage

> [!TIP]
> If you use SSMS v19.2 or later, you do not need to download `xel` files as described in this section. In these versions, SSMS reads the `xel` files for each session directly from Azure storage. For more information, see the [Improving Extended Events in Azure SQL](https://techcommunity.microsoft.com/t5/azure-sql-blog/improving-extended-events-in-azure-sql/ba-p/3980918) blog.

Download the `xel` blob for the session from the storage container and save it as a local file. In Azure portal, find the storage account you used, select **Containers** under **Data storage**, and select the container you created for your event session. The blob for the session has the session name as the first part of its name, with a numeric suffix. Select the ellipsis (**...**) to show the context menu for the blob, and select **Download**.

You can install [Azure Storage Explorer](https://azure.microsoft.com/products/storage/storage-explorer/) to download multiple `xel` blobs in one operation.

Once the `xel` file is downloaded, open it in SSMS. On the SSMS main menu, go to **File** and select **Open**. If you have a single `xel` file, select **File...** and browse to the file you downloaded. If you have multiple `xel` files generated by the same event session (known as rollover files), you can use the **Merge Extended Event Files...** dialog to open all of them in the event viewer.

### View event data using T-SQL

To read event session data using T-SQL, use the [sys.fn_xe_file_target_read_file()](/sql/relational-databases/extended-events/targets-for-extended-events-in-sql-server#sysfn_xe_file_target_read_file-function) function. To use this function in a database or managed instance different from the one where the event session is created, [grant access](#grant-access-to-the-container) to the [!INCLUDE [ssde-md](../../docs/includes/ssde-md.md)] on the storage container with the event data blobs.

For a more detailed walkthrough, see [Create an event session in SSMS](/sql/relational-databases/extended-events/quick-start-extended-events-in-sql-server#create-an-event-session-in-ssms).

## Related content

- [Extended Events in Azure SQL Database](xevent-db-diff-from-svr.md)
- [Extended Events](/sql/relational-databases/extended-events/extended-events)
- [event_file target](/sql/relational-databases/extended-events/targets-for-extended-events-in-sql-server#event_file-target)
- [CREATE EVENT SESSION (Transact-SQL)](/sql/t-sql/statements/create-event-session-transact-sql)
- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](/sql/t-sql/statements/create-database-scoped-credential-transact-sql)
- [CREATE CREDENTIAL (Transact-SQL)](/sql/t-sql/statements/create-credential-transact-sql)
