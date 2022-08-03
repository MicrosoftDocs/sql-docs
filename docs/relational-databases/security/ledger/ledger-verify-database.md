---
title: Verify a ledger table to detect tampering
description: This article discusses how to verify if a table was tampered with.
ms.service: sql-database
ms.subservice: security
ms.devlang:
ms.topic: how-to
author: VanMSFT
ms.author: vanto
ms.reviewer: kendralittle, mathoma
ms.date: "05/24/2022"
monikerRange: "= azuresqldb-current||>= sql-server-ver16||>= sql-server-linux-ver16"
---

# Verify a ledger table to detect tampering

[!INCLUDE [SQL Server 2022 Azure SQL Database](../../../includes/applies-to-version/sqlserver2022-asdb.md)]

In this article, you'll verify the integrity of the data in your ledger tables. If you've enabled the setting **Enable automatic digest storage** on your Azure SQL Database, follow the *[T-SQL using automatic digest storage](#run-ledger-verification-for-the-database)* section. Otherwise, follow the *[T-SQL using a manual generated digest](#run-ledger-verification-for-the-database)* section.

## Prerequisites

- Have an active Azure subscription if you're using Azure SQL Database. If you don't have one, [create a free account](https://azure.microsoft.com/free/).
- [Create and use updatable ledger tables](ledger-how-to-updatable-ledger-tables.md) or [create and use append-only ledger tables](ledger-how-to-append-only-ledger-tables.md).
- [SQL Server Management Studio](../../../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](../../../azure-data-studio/download-azure-data-studio.md).

## Run ledger verification for the database

# [T-SQL using automatic digest storage](#tab/t-sql-automatic)

> [!NOTE]
> Automatic digest storage is only applicable to Azure SQL Database. If you are using SQL Server, switch over to the *T-SQL using a manual generated digest* tab.

1. Connect to your database by using [SQL Server Management Studio](../../../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](../../../azure-data-studio/download-azure-data-studio.md).

1. Create a new query with the following T-SQL statement:

   ```sql
   DECLARE @digest_locations NVARCHAR(MAX) = (SELECT * FROM sys.database_ledger_digest_locations FOR JSON AUTO, INCLUDE_NULL_VALUES);SELECT @digest_locations as digest_locations;
   BEGIN TRY
       EXEC sys.sp_verify_database_ledger_from_digest_storage @digest_locations;
       SELECT 'Ledger verification succeeded.' AS Result;
   END TRY
   BEGIN CATCH
       THROW;
   END CATCH
   ```

   > [!NOTE]
   > The verification script can also be found in the Azure portal. Open the [Azure portal](https://portal.azure.com/) and locate the database you want to verify. In **Security**, select the **Ledger** option. In the **Ledger** pane, select **</> Verify database**.

1. Execute the query. You'll see that **digest_locations** returns the current location of where your database digests are stored and any previous locations. **Result** returns the success or failure of ledger verification.

   :::image type="content" source="media/ledger/verification_script_exectution.png" alt-text="Screenshot of running ledger verification by using Azure Data Studio.":::

1. Open the **digest_locations** result set to view the locations of your digests. The following example shows two digest storage locations for this database:

   - **path** indicates the location of the digests.
   - **last_digest_block_id** indicates the block ID of the last digest stored in the **path** location.
   - **is_current** indicates whether the location in **path** is the current (true) or previous (false) one.

       ```json
       [
        {
            "path": "https:\/\/digest1.blob.core.windows.net\/sqldbledgerdigests\/janderstestportal2server\/jandersnewdb\/2021-05-20T04:39:47.6570000",
            "last_digest_block_id": 10016,
            "is_current": true
        },
        {
            "path": "https:\/\/jandersneweracl.confidential-ledger.azure.com\/sqldbledgerdigests\/janderstestportal2server\/jandersnewdb\/2021-05-20T04:39:47.6570000",
            "last_digest_block_id": 1704,
            "is_current": false
        }
       ]
       ```

   > [!IMPORTANT]
   > When you run ledger verification, inspect the location of **digest_locations** to ensure digests used in verification are retrieved from the locations you expect. You want to make sure that a privileged user hasn't changed locations of the digest storage to an unprotected storage location, such as Azure Storage, without a configured and locked immutability policy.

1. Verification returns the following message in the **Results** window.

   - If there was no tampering in your database, the message is:

       ```output
       Ledger verification successful
       ```

   - If there was tampering in your database, the following error appears in the **Messages** window:
  
       ```output
       Failed to execute query. Error: The hash of block xxxx in the database ledger doesn't match the hash provided in the digest for this block.
       ```

# [T-SQL using a manual generated digest](#tab/t-sql-manual)

1. Connect to your database by using [SQL Server Management Studio](../../../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](../../../azure-data-studio/download-azure-data-studio.md).
1. Create a new query with the following T-SQL statement:

   ```sql
   EXECUTE sp_generate_database_ledger_digest;
   ```

1. Execute the query. The results contain the latest database digest and represent the hash of the database at the current point in time. Copy the contents of the results to be used in the next step.

   :::image type="content" source="media/ledger/ledger-retrieve-digest.png" alt-text="Screenshot that shows retrieving digest results by using Azure Data Studio.":::

1. Create a new query with the following T-SQL statement. Replace `<YOUR DATABASE DIGEST>` with the digest you copied in the previous step.

   ```sql
   EXECUTE sp_verify_database_ledger N'
   <YOUR DATABASE DIGEST>
   ';
   ```

1. Execute the query. The **Messages** window contains the following success message.

   :::image type="content" source="media/ledger/ledger-verify-message.png" alt-text="Screenshot that shows the message after running T-SQL query for ledger verification by using Azure Data Studio.":::

   > [!TIP]
   > Running ledger verification with the latest digest will only verify the database from the time the digest was generated until the time the verification was run. To verify that the historical data in your database wasn't tampered with, run verification by using multiple database digest files. Start with the point in time for which you want to verify the database. An example of a verification passing multiple digests would look similar to the following query.

   ```sql
   EXECUTE sp_verify_database_ledger N'
   [
       {
           "database_name":  "ledgerdb",
           "block_id":  0,
           "hash":  "0xDC160697D823C51377F97020796486A59047EBDBF77C3E8F94EEE0FFF7B38A6A",
           "last_transaction_commit_time":  "2020-11-12T18:01:56.6200000",
           "digest_time":  "2020-11-12T18:39:27.7385724"
       },
       {
           "database_name":  "ledgerdb",
           "block_id":  1,
           "hash":  "0xE5BE97FDFFA4A16ADF7301C8B2BEBC4BAE5895CD76785D699B815ED2653D9EF8",
           "last_transaction_commit_time":  "2020-11-12T18:39:35.6633333",
           "digest_time":  "2020-11-12T18:43:30.4701575"
       }
   ]';
   ```

> [!NOTE]
> In this example, we call the [sp_generate_database_ledger_digest](../../system-stored-procedures/sys-sp-generate-database-ledger-digest-transact-sql.md) stored procedure to generate the digest and use it immediately for verification. However, when a customer is using a custom trusted storage, they could save the digest in the trusted storage for a later verification.

---

## Next steps

- [Ledger overview](ledger-overview.md)
- [sys.database_ledger_digest_locations](../../system-catalog-views/sys-database-ledger-digest-locations-transact-sql.md)
- [sp_verify_database_ledger_from_digest_storage](../../system-stored-procedures/sys-sp-verify-database-ledger-from-digest-storage-transact-sql.md)
- [sp_verify_database_ledger](../../system-stored-procedures/sys-sp-verify-database-ledger-transact-sql.md)
- [sp_generate_database_ledger_digest](../../system-stored-procedures/sys-sp-generate-database-ledger-digest-transact-sql.md)