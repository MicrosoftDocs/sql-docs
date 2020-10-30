---
title: "Tutorial: Always Encrypted with secure enclaves in Azure SQL Database using SSMS"
description: This tutorial teaches you how to create a basic environment for Always Encrypted with secure enclaves in Azure SQL Database and how to encrypt data in-place, and issue rich confidential queries against encrypted columns using SQL Server Management Studio (SSMS). 
ms.custom: seo-lt-2019
ms.date: 12/09/2020
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: vanto
ms.suite: "sql"
ms.technology: security
ms.tgt_pltfrm: ""
ms.topic: tutorial
author: jaszymas
ms.author: jaszymas
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---
# Tutorial: Always Encrypted with secure enclaves in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]

[!INCLUDE [asdb](../../includes/applies-to-version/asdb.md)]

This tutorial teaches you how to get started with [Always Encrypted with secure enclaves](encryption/always-encrypted-enclaves.md) in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. It will show you:
- How to create a basic environment for testing and evaluating Always Encrypted with secure enclaves.
- How to encrypt data in-place and issue rich confidential queries against encrypted columns using SQL Server Management Studio (SSMS).

## Prerequisites

To get started with Always Encrypted with secure enclaves, you need at least two computers (they can be virtual machines):

- The [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]  computer to host [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]  and SSMS.
- The HGS computer to run Host Guardian Service, which is needed for enclave attestation.

## Step 4: Create a sample database
In this step, you will create a database with some sample data, which you will encrypt later.

1. Using the SSMS instance from the previous step, execute the below statement in a query window to create a new database, named **ContosoHR**.

    ```sql
    CREATE DATABASE [ContosoHR];
    ```

1. Create a new table, named **Employees**.

    ```sql
    USE [ContosoHR];
    GO

    CREATE TABLE [dbo].[Employees]
    (
        [EmployeeID] [int] IDENTITY(1,1) NOT NULL,
        [SSN] [char](11) NOT NULL,
        [FirstName] [nvarchar](50) NOT NULL,
        [LastName] [nvarchar](50) NOT NULL,
        [Salary] [money] NOT NULL
    ) ON [PRIMARY];
    ```

1. Add a few employee records to the **Employees** table.

    ```sql
    USE [ContosoHR];
    GO

    INSERT INTO [dbo].[Employees]
            ([SSN]
            ,[FirstName]
            ,[LastName]
            ,[Salary])
        VALUES
            ('795-73-9838'
            , N'Catherine'
            , N'Abel'
            , $31692);

    INSERT INTO [dbo].[Employees]
            ([SSN]
            ,[FirstName]
            ,[LastName]
            ,[Salary])
        VALUES
            ('990-00-6818'
            , N'Kim'
            , N'Abercrombie'
            , $55415);
    ```

## Step 5: Provision enclave-enabled keys

In this step, you will create a column master key and a column encryption key that allow enclave computations.

1. Using the SSMS instance from the previous step, in **Object Explorer**, expand your database and navigate to **Security** > **Always Encrypted Keys**.
1. Provision a new enclave-enabled column master key:
    1. Right-click **Always Encrypted Keys** and select **New Column Master Key...**.
    2. Select your column master key name: **CMK1**.
    3. Make sure you select either **Windows Certificate Store (Current User or Local Machine)** or **Azure Key Vault**.
    4. Select **Allow enclave computations**.
    5. If you selected Azure Key Vault, sign in to Azure and select your key vault. For more information on how to create a key vault for Always Encrypted, see [Manage your key vaults from Azure portal](/archive/blogs/kv/manage-your-key-vaults-from-new-azure-portal).
    6. Select your certificate or Azure Key Value key if it already exists, or click the **Generate Certificate** button to create a new one.
    7. Select **OK**.

        ![Allow enclave computations](encryption/media/always-encrypted-enclaves/allow-enclave-computations.png)

1. Create a new enclave-enabled column encryption key:

    1. Right-click **Always Encrypted Keys** and select **New Column Encryption Key**.
    2. Enter a name for the new column encryption key: **CEK1**.
    3. In the **Column master key** dropdown, select the column master key you created in the previous steps.
    4. Select **OK**.

## Step 6: Encrypt some columns in place

In this step, you will encrypt the data stored in the **SSN** and **Salary** columns inside the server-side enclave, and then test a SELECT query on the data.

1. Open a new SSMS instance and connect to your SQL Server instance **with** Always Encrypted enabled for the database connection.
    1. Start a new instance of SSMS.
    1. In the **Connect to Server** dialog, specify your server name, select an authentication method and specify your credentials.
    1. Click **Options >>** and select the **Always Encrypted** tab.
    1. Select the **Enable Always Encrypted (column encryption)** checkbox and specify your enclave attestation URL (for example, ht<span>tp://</span>hgs.bastion.local/Attestation).
    1. Select **Connect**.
    1. If you are prompted to enable Parameterization for Always Encrypted queries, select **Enable**.

1. Using the same SSMS instance (with Always Encrypted enabled), open a new query window and encrypt the **SSN** and **Salary** columns by running the below queries.

    ```sql
    USE [ContosoHR];
    GO

    ALTER TABLE [dbo].[Employees]
    ALTER COLUMN [SSN] [char] (11) COLLATE Latin1_General_BIN2
    ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = [CEK1], ENCRYPTION_TYPE = Randomized, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256') NOT NULL
    WITH
    (ONLINE = ON);

    ALTER TABLE [dbo].[Employees]
    ALTER COLUMN [Salary] [money]
    ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = [CEK1], ENCRYPTION_TYPE = Randomized, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256') NOT NULL
    WITH
    (ONLINE = ON);

    ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE;
    ```

    > [!NOTE]
    > Notice the ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE statement to clear the query plan cache for the database in the above script. After you have altered the table, you need to clear the plans for all batches and stored procedures that access the table, to refresh parameters encryption information. 

1. To verify the **SSN** and **Salary** columns are now encrypted, open a new query window in the SSMS instance **without** Always Encrypted enabled for the database connection and execute the below statement. The query window should return encrypted values in the **SSN** and **Salary** columns. If you execute the same query using the SSMS instance with Always Encrypted enabled, you should see the data decrypted.

    ```sql
    SELECT * FROM [dbo].[Employees];
    ```

## Step 7: Run rich queries against encrypted columns

Now, you can run rich queries against the encrypted columns. Some query processing will be performed inside your server-side enclave. 

1. In the SSMS instance **with** Always Encrypted enabled, make sure Parameterization for Always Encrypted is also enabled.
    1. Select **Tools** from the main menu of SSMS.
    2. Select **Options...**.
    3. Navigate to **Query Execution** > **SQL Server** > **Advanced**.
    4. Ensure that **Enable Parameterization for Always Encrypted** is checked.
    5. Select **OK**.
2. Open a new query window, paste in and execute the below query. The query should return plaintext values and rows meeting the specified search criteria.

    ```sql
    DECLARE @SSNPattern [char](11) = '%6818';
    DECLARE @MinSalary [money] = $1000;
    SELECT * FROM [dbo].[Employees]
    WHERE SSN LIKE @SSNPattern AND [Salary] >= @MinSalary;
    ```

3. Try the same query again in the SSMS instance that does not have Always Encrypted enabled, and note the failure that occurs.

## Next Steps
After completing this tutorial, you can go to one of the following tutorials:
- [Tutorial: Develop a .NET application using Always Encrypted with secure enclaves](../../connect/ado-net/sql/tutorial-always-encrypted-enclaves-develop-net-apps.md)
- [Tutorial: Develop a .NET Framework application using Always Encrypted with secure enclaves](tutorial-always-encrypted-enclaves-develop-net-framework-apps.md)
- [Tutorial: Creating and using indexes on enclave-enabled columns using randomized encryption](./tutorial-creating-using-indexes-on-enclave-enabled-columns-using-randomized-encryption.md)

## See Also
- [Configure and use Always Encrypted with secure enclaves](encryption/configure-always-encrypted-enclaves.md)