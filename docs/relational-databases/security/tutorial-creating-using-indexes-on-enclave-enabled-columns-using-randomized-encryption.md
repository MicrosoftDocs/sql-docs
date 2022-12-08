---
title: "Indexes on enclave-enabled columns with randomized encryption (Tutorial)"
description: This tutorial teaches you how to create and use index on enclave-enabled columns using randomized encryption supported in Always Encrypted with secure enclaves for SQL Server and Azure SQL Database. 
ms.custom:
- seo-lt-2019
- event-tier1-build-2022
ms.date: 05/24/2022
ms.service: sql
ms.reviewer: vanto
ms.suite: "sql"
ms.subservice: security
ms.tgt_pltfrm: ""
ms.topic: tutorial
author: jaszymas
ms.author: jaszymas
monikerRange: ">= sql-server-ver15"
---
# Tutorial: Create and use indexes on enclave-enabled columns using randomized encryption

[!INCLUDE [sqlserver2019-windows-only-asdb](../../includes/applies-to-version/sqlserver2019-windows-only-asdb.md)]

This tutorial teaches you how to create and use indexes on enclave-enabled columns using randomized encryption supported in [Always Encrypted with secure enclaves](encryption/always-encrypted-enclaves.md). It will show you:

> [!div class="checklist"]
> - How to create an index when you have access to the keys (the column master key and the column encryption key) protecting the column.
> - How to create an index when you don't have access to the keys protecting the column.

## Prerequisites

Make sure you've completed one of the below tutorials before following the below steps in this tutorial:

- [Tutorial: Getting started with Always Encrypted with secure enclaves in SQL Server](tutorial-getting-started-with-always-encrypted-enclaves.md)
- [Tutorial: Getting started with Always Encrypted with secure enclaves in Azure SQL Database](/azure/azure-sql/database/always-encrypted-enclaves-getting-started)

## Step 1: Enable Accelerated Database Recovery (ADR) in your database

> [!NOTE]
> This step applies only to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. If you're using [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], skip this step. ADR is automatically enabled in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and disabling it is not supported.

Microsoft strongly recommends you enable ADR in your database before creating the first index on an enclave-enabled column using randomized encryption. See the [Database Recovery](./encryption/always-encrypted-enclaves.md#database-recovery) section in [Always Encrypted with secure enclaves](./encryption/always-encrypted-enclaves.md).



1. Close any SSMS instances, you used in the previous tutorial. This will close database connections you've opened, which is required to enable ADR.
1. Open a new instance of SSMS and connect to your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance as sysadmin **without** Always Encrypted enabled for the database connection.
    1. Start SSMS.
    1. In the **Connect to Server** dialog, specify your server name, select an authentication method, and specify your credentials.
    1. Click **Options >>** and select the **Always Encrypted** tab.
    1. Make sure the **Enable Always Encrypted (column encryption)** checkbox is **not** selected.
    1. Select **Connect**.
1. Open a new query window and execute the below statement to enable ADR.

   ```sql
   ALTER DATABASE ContosoHR SET ACCELERATED_DATABASE_RECOVERY = ON;
   ```

## Step 2: Create and test an index without role separation

In this step, you'll create and test an index on an encrypted column. You'll be acting as a single user who is assuming the roles of both a DBA, who manages the database, and the data owner who has access to the keys, protecting the data.

1. Open a new SSMS instance and connect to your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance **with** Always Encrypted enabled for the database connection.
   1. Start a new instance of SSMS.
   1. In the **Connect to Server** dialog, specify your server name, select an   authentication method, and specify your credentials.
   1. Click **Options >>** and select the **Always Encrypted** tab.
   1. Select the **Enable Always Encrypted (column encryption)** checkbox and specify your enclave attestation URL (for example, `http://hgs.bastion.local/Attestation` or `https://MyAttestationProvider.us.attest.azure.net/attest/SgxEnclave`).
   1. Select **Connect**.
   1. If prompted to enable parameterization for Always Encrypted queries, click **Enable**.
1. If you weren't prompted to enable Parameterization for Always Encrypted, verify it's enabled.
   1. Select **Tools** from the main menu of SSMS.
   2. Select **Options...**.
   3. Navigate to **Query Execution** > **SQL Server** > **Advanced**.
   4. Ensure that **Enable Parameterization for Always Encrypted** is checked.
   5. Select **OK**.
1. Open a query window and execute the below statements to encrypt the **LastName** column in the **Employees** table. You'll create and use an index on that column in later steps.

   ```sql  
   ALTER TABLE [HR].[Employees]
   ALTER COLUMN [LastName] [nvarchar](50) COLLATE Latin1_General_BIN2 
   ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = [CEK1], ENCRYPTION_TYPE = Randomized, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256') NOT NULL;
   GO   
   ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE;
   GO
   ```

1. Create an index on the **LastName** column. Since you're connected to the database with Always Encrypted enabled, the client driver inside SSMS transparently provides **CEK1** (the column encryption key, protecting the **LastName** column) to the enclave, which is needed to create the index.

   ```sql
   CREATE INDEX IX_LastName ON [HR].[Employees] ([LastName])
   INCLUDE ([EmployeeID], [FirstName], [SSN], [Salary]);
   GO
   ```

1. Run a rich query on the **LastName** column and verify SQL Server uses the index when executing the query.
   1. In the same or a new query window, make sure the **Include Live Query Statistics** button on the toolbar is on.
   1. Execute the below query.

       ```sql
       DECLARE @LastNamePrefix NVARCHAR(50) = 'Aber%';
       SELECT * FROM [HR].[Employees] WHERE [LastName] LIKE @LastNamePrefix;
       GO
      ```

   1. In the **Live Query Statistics** tab (in the bottom part of the query window), observe that the query uses the index.

## Step 3: Create an index with role separation

In this step, you'll create an index on an encrypted column, pretending to be two different users. One user is a DBA, who needs to create an index, but doesn't have access to the keys. The other user is a data owner, who has access to the keys.

1. Using the SSMS instance **without** Always Encrypted enabled, execute the below statement to drop the index on the **LastName** column.

   ```sql
   DROP INDEX IX_LastName ON [HR].[Employees]; 
   GO
   ```

1. Acting as a data owner (or an application that has access to the keys), populate the cache inside the enclave with **CEK1**.

   > [!NOTE]
   > Unless you have restarted your SQL Server instance after **Step 2: Create and test an index without role separation**, this step is redundant as the **CEK1** is already present in the cache. We have added it to demonstrate how a data owner can provide a key to the enclave, if it is not already present in the enclave.

   1. In the SSMS instance **with** Always Encrypted enabled, execute the below statements in a query window. The statement sends all enclave-enabled column encryption keys to the enclave. See [sp_enclave_send_keys](../system-stored-procedures/sp-enclave-send-keys-sql.md) for details.

        ```sql
        EXEC sp_enclave_send_keys;
        GO
        ```

   1. As an alternative to executing the above stored procedure, you can run a DML query that uses the enclave against the **LastName** column. This will populate the enclave only with **CEK1**.

        ```sql
        DECLARE @LastNamePrefix NVARCHAR(50) = 'Aber%';
        SELECT * FROM [HR].[Employees] WHERE [LastName] LIKE @LastNamePrefix;
        GO
        ```

1. Acting as a DBA, create the index.
    1. In the SSMS instance **without** Always Encrypted enabled, execute the below statements in a query window.

        ```sql
        CREATE INDEX IX_LastName ON [HR].[Employees] ([LastName])
        INCLUDE ([EmployeeID], [FirstName], [SSN], [Salary]);
        GO
        ```

1. As a data owner, run a rich query on the **LastName** column and verify SQL Server uses the index when executing the query.
   1. In the SSMS instance **with** Always Encrypted enabled, select an existing query window or open a new query window, and make sure the **Include Live Query Statistics** button on the toolbar is on.
   1. Execute the below query. 

        ```sql
        DECLARE @LastNamePrefix NVARCHAR(50) = 'Aber%';
        SELECT * FROM [HR].[Employees] WHERE [LastName] LIKE @LastNamePrefix;
        GO
        ```

   1. In the **Live Query Statistics** (in the bottom part of the query window), observe that the query uses the index.

## Next steps
- [Tutorial: Develop a .NET application using Always Encrypted with secure enclaves](../../connect/ado-net/sql/tutorial-always-encrypted-enclaves-develop-net-apps.md)
- [Tutorial: Develop a .NET Framework application using Always Encrypted with secure enclaves](tutorial-always-encrypted-enclaves-develop-net-framework-apps.md)

## See also
- [Create and use indexes on columns using Always Encrypted with secure enclaves](encryption/always-encrypted-enclaves-create-use-indexes.md)
