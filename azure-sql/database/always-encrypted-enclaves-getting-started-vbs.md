---
title: "Tutorial: Getting started using Always Encrypted with VBS enclaves"
description: Tutorial on how to create a basic environment for Always Encrypted with VBS enclaves in Azure SQL Database, how to encrypt data in-place, and issue rich confidential queries against encrypted columns using SQL Server Management Studio (SSMS).
author: Pietervanhove
ms.author: pivanho
ms.reviewer: vanto, mathoma
ms.date: 11/14/2023
ms.service: sql-database
ms.subservice: security
ms.custom: ignite-2023
ms.topic: tutorial
---
# Tutorial: Getting started using Always Encrypted with VBS enclaves in Azure SQL Database

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

This tutorial teaches you how to get started with [Always Encrypted with secure enclaves](/sql/relational-databases/security/encryption/always-encrypted-enclaves) in Azure SQL Database using [virtualization-based security (VBS) enclaves](https://www.microsoft.com/security/blog/2018/06/05/virtualization-based-security-vbs-memory-enclaves-data-protection-through-isolation/). It will show you:

> [!div class="checklist"]
>
> - How to create an environment for testing and evaluating Always Encrypted with VBS enclaves.
> - How to encrypt data in-place and issue rich confidential queries against encrypted columns using SQL Server Management Studio (SSMS).

## Prerequisites

- An active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/free/). You need to be a member of the Contributor role or the Owner role for the subscription to be able to create resources.
- Optional, but recommended for storing your column master key for Always Encrypted: a key vault in Azure Key Vault. For information on how to create a key vault, see [Quickstart: Create a key vault using the Azure portal](/azure/key-vault/general/quick-create-portal).
  - If your key vault uses the access policy permissions model, make sure you have the following key permissions in the key vault: `get`, `list`, `create`, `unwrap key`, `wrap key`, `verify`, `sign`. See [Assign a Key Vault access policy](/azure/key-vault/general/assign-access-policy).
  - If you're using the Azure role-based access control (RBAC) permission model, make you sure you're a member of the [Key Vault Crypto Officer](/azure/role-based-access-control/built-in-roles#key-vault-crypto-officer) role for your key vault. See [Provide access to Key Vault keys, certificates, and secrets with an Azure role-based access control](/azure/key-vault/general/rbac-migration).

### Tool requirements

SQL Server Management Studio (SSMS) is required for this tutorial. You can choose to use either PowerShell or the Azure CLI to enable VBS enclaves.

# [SSMS](#tab/ssmsrequirements)

Download the latest version of [SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms).

# [PowerShell](#tab/azure-powershellrequirements)

Az PowerShell module version 9.3.0 or later is required. For details on how to install the Az PowerShell module, see [Install the Azure Az PowerShell module](/powershell/azure/install-az-ps). To determine the version of the Az PowerShell module that is installed on your machine, run the following command from PowerShell.

```powershell
Get-InstalledModule -Name Az
```

# [Azure CLI](#tab/azure-clirequirements)

Make sure that Azure CLI 2.44.0 or later is installed on your machine. For details on how to install Azure CLI, see [How to install the Azure CLI](/cli/azure/install-azure-cli). To find your installed version and see if you need to update, run az version.

```azurecli-interactive
az version
```

---

## Step 1: Create and configure a server and a database

In this step, you'll create a new Azure SQL Database logical server and a new database.

Go to [Quickstart: Create a single database - Azure SQL Database](single-database-create-quickstart.md) and follow the instructions in the [Create a single database](single-database-create-quickstart.md#create-a-single-database) section to create a new Azure SQL Database logical server and a new database.

> [!IMPORTANT]
> Make sure that you create an **empty database** with the name **ContosoHR** (and not a sample database).

## Step 2: Enable a VBS enclave

In this step, you'll enable a VBS enclave in the database, which is required for Always Encrypted with secure enclaves. To enable VBS enclaves in your database, you need to set the **preferredEnclaveType** [database property](/azure/templates/microsoft.sql/2022-05-01-preview/servers/databases?pivots=deployment-language-bicep#databaseproperties) to **VBS**.

# [Portal](#tab/azure-portal)
1. Open the [Azure portal](https://portal.azure.com/) and locate the database for which you want to enable secure enclaves.
1. In the **Security** settings, select **Data Encryption**.
1. In the **Data Encryption** menu, select the **Always Encrypted** tab.
1. Set **Enable secure enclaves** to **ON**. If it is already set to **ON** proceed with the next step.

    :::image type="content" source="./media/always-encrypted-enclaves/portal-enable-secure-enclaves-existing-database.png" alt-text="Screenshot of enabling secure enclaves on an existing database in the Azure portal.":::
    
1. Select **Save** to save your Always Encrypted configuration.

# [SSMS](#tab/SSMS)
1. Open SSMS and connect logical server where you want to modify your database.
2. Right-click on your database and select **Properties**. 
3. In the **Configure SLO** page, set the option **Enable Secure Enclaves** to **ON**. If it is already set to **ON**, proceed with the next step.
4. Select **OK** to save your database properties.

# [PowerShell](#tab/azure-powershell)

1. Open a PowerShell console and import the required version of the Az PowerShell module.

   ```azurepowershell-interactive
   Import-Module "Az" -MinimumVersion "9.3.0"
   ```

1. Sign into Azure. If needed, [switch to the subscription](/powershell/azure/manage-subscriptions-azureps) you're using for this tutorial.

   ```azurepowershell-interactive
   Connect-AzAccount
   $subscriptionId = "<your subscription ID>"
   $context = Set-AzContext -Subscription $subscriptionId
   ```

1. Enable a VBS enclave in your database.

   ```azurepowershell-interactive
   $resourceGroupName = "<your resource group name>"
   $serverName = "<your server name>"
   $databaseName = "ContosoHR"
    
   Set-AzSqlDatabase -ResourceGroupName $resourceGroupName -ServerName $serverName -DatabaseName $databaseName -PreferredEnclaveType VBS
   ```

# [Azure CLI](#tab/azure-cli)

1. Open either a Windows Command Prompt (CMD) or PowerShell and sign into Azure. If needed, [switch to the subscription](/cli/azure/manage-azure-subscriptions-azure-cli) you're using for this tutorial.

   ```azurecli-interactive
   az login
   $subscriptionId = "<your subscription ID>"
   az account set --subscription $subscriptionId
   ```

1. Enable a VBS enclave in your database.

   ```azurecli-interactive
   $resourceGroupName = "<your resource group name>"
   $serverName = "<your server name>"
   $databaseName = "ContosoHR"

   az sql db update -g $resourceGroupName `
       -s $serverName `
       -n $databaseName `
       --preferred-enclave-type VBS
   ```

---

## Step 3: Populate your database

In this step, you'll create a table and populate it with some data that you'll later encrypt and query.

1. Open SSMS and connect to the **ContosoHR** database in the Azure SQL logical server you created **without** Always Encrypted enabled in the database connection.
    1. In the **Connect to Server** dialog, specify the fully qualified name of your server (for example, *myserver135.database.windows.net*), and enter the administrator user name and the password you specified when you created the server.
    2. Select **Options >>** and select the **Connection Properties** tab. Make sure to select the **ContosoHR** database (not the default, `master` database).
    3. Select the **Always Encrypted** tab.
    4. Make sure the **Enable Always Encrypted (column encryption)** checkbox is **not** selected.

       :::image type="content" source="./media/always-encrypted-enclaves/ssms-connect-disabled.png" alt-text="Screenshot of Connect to Server using SSMS without Always Encrypted enabled.":::

    5. Select **Connect**.

2. Create a new table, named **Employees**.

    ```sql
    CREATE SCHEMA [HR];
    GO

    CREATE TABLE [HR].[Employees]
    (
        [EmployeeID] [int] IDENTITY(1,1) NOT NULL,
        [SSN] [char](11) NOT NULL,
        [FirstName] [nvarchar](50) NOT NULL,
        [LastName] [nvarchar](50) NOT NULL,
        [Salary] [money] NOT NULL
    ) ON [PRIMARY];
    GO
    ```

3. Add a few employee records to the **Employees** table.

    ```sql
    INSERT INTO [HR].[Employees]
            ([SSN]
            ,[FirstName]
            ,[LastName]
            ,[Salary])
        VALUES
            ('795-73-9838'
            , N'Catherine'
            , N'Abel'
            , $31692);

    INSERT INTO [HR].[Employees]
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

## Step 4: Provision enclave-enabled keys

In this step, you'll create a column master key and a column encryption key that allow enclave computations.

1. Using the SSMS instance from the previous step, in **Object Explorer**, expand your database and navigate to **Security** > **Always Encrypted Keys**.
1. Provision a new enclave-enabled column master key:
    1. Right-click **Always Encrypted Keys** and select **New Column Master Key...**.
    2. Enter a name for the new column master key: **CMK1**.
    3. Verify **Allow enclave computations** is selected. (It's selected by default if a secure enclave is enabled for the database - it should be enabled since your database uses the DC-series hardware configuration.)
    4. Select either **Azure Key Vault** (recommended) or **Windows Certificate Store** (**Current User** or **Local Machine**).
       - If you select Azure Key Vault, sign into Azure, select an Azure subscription containing a key vault you want to use, and select your key vault. Select **Generate Key** to create a new key.
       - If you select Windows Certificate Store, select the **Generate Certificate** button to create a new certificate.
       :::image type="content" source="./media/always-encrypted-enclaves/ssms-new-cmk-enclave-key-vault.png" alt-text="Screenshot of the allow enclave computations selection in SSMS when creating a new column master key.":::
    5. Select **OK**.

1. Create a new enclave-enabled column encryption key:

    1. Right-click **Always Encrypted Keys** and select **New Column Encryption Key**.
    2. Enter a name for the new column encryption key: **CEK1**.
    3. In the **Column master key** dropdown, select the column master key you created in the previous steps.
    4. Select **OK**.

## Step 5: Encrypt some columns in place

In this step, you'll encrypt the data stored in the **SSN** and **Salary** columns inside the server-side enclave, and then test a SELECT query on the data.

1. Open a new SSMS instance and connect to your database **with** Always Encrypted enabled for the database connection.
    1. Start a new instance of SSMS.
    2. In the **Connect to Server** dialog, specify the fully qualified name of your server (for example, *myserver135.database.windows.net*), and enter the administrator user name and the password you specified when you created the server.
    3. Select **Options >>** and select the **Connection Properties** tab. Make sure to select the **ContosoHR** database (not the default, `master` database). 
    4. Select the **Always Encrypted** tab.
    5. Select the **Enable Always Encrypted (column encryption)** checkbox.
    6. Select **Enable secure enclaves**.
    7. Set **Protocol** to **None**. See the below screenshot.

       :::image type="content" source="./media/always-encrypted-enclaves/ssms-connect-vbs-protocol-none.png" alt-text="Screenshot of the SSMS Connect to Server dialog Always Encrypted tab, with attestation protocol set to None.":::

    8. Select **Connect**.
    9. If you're prompted to enable Parameterization for Always Encrypted queries, select **Enable**.

1. Using the same SSMS instance (with Always Encrypted enabled), open a new query window and encrypt the **SSN** and **Salary** columns by running the below statements.

    ```sql
    ALTER TABLE [HR].[Employees]
    ALTER COLUMN [SSN] [char] (11) COLLATE Latin1_General_BIN2
    ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = [CEK1], ENCRYPTION_TYPE = Randomized, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256') NOT NULL
    WITH
    (ONLINE = ON);

    ALTER TABLE [HR].[Employees]
    ALTER COLUMN [Salary] [money]
    ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = [CEK1], ENCRYPTION_TYPE = Randomized, ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256') NOT NULL
    WITH
    (ONLINE = ON);

    ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE;
    ```

    > [!NOTE]
    > The ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE statement clears the query plan cache for the database in the above script. After you have altered the table, you need to clear the plans for all batches and stored procedures that access the table to refresh parameters encryption information.

1. To verify the **SSN** and **Salary** columns are now encrypted, open a new query window in the SSMS instance **without** Always Encrypted enabled for the database connection and execute the below statement. The query window should return encrypted values in the **SSN** and **Salary** columns. If you execute the same query using the SSMS instance with Always Encrypted enabled, you should see the data decrypted.

   ```sql
   SELECT * FROM [HR].[Employees];
   ```

## Step 6: Run rich queries against encrypted columns

You can run rich queries against the encrypted columns. Some query processing will be performed inside your server-side enclave.

1. In the SSMS instance **with** Always Encrypted enabled, make sure Parameterization for Always Encrypted is also enabled.
    1. Select **Tools** from the main menu of SSMS.
    2. Select **Options...**.
    3. Navigate to **Query Execution** > **SQL Server** > **Advanced**.
    4. Ensure that **Enable Parameterization for Always Encrypted** is checked.
    5. Select **OK**.
2. Open a new query window, paste in the below query, and execute. The query should return plaintext values and rows meeting the specified search criteria.

   ```sql
   DECLARE @SSNPattern [char](11) = '%6818';
   DECLARE @MinSalary [money] = $1000;
   SELECT * FROM [HR].[Employees]
   WHERE SSN LIKE @SSNPattern AND [Salary] >= @MinSalary;
   ```

3. Try the same query again in the SSMS instance that doesn't have Always Encrypted enabled. A failure should occur.

## Next steps

After completing this tutorial, you can go to one of the following tutorials:

- [Tutorial: Develop a .NET application using Always Encrypted with secure enclaves](/sql/connect/ado-net/sql/tutorial-always-encrypted-enclaves-develop-net-apps)
- [Tutorial: Develop a .NET Framework application using Always Encrypted with secure enclaves](/sql/relational-databases/security/tutorial-always-encrypted-enclaves-develop-net-framework-apps)
- [Tutorial: Creating and using indexes on enclave-enabled columns using randomized encryption](/sql/relational-databases/security/tutorial-creating-using-indexes-on-enclave-enabled-columns-using-randomized-encryption)

## See also

- [Configure and use Always Encrypted with secure enclaves](/sql/relational-databases/security/encryption/configure-always-encrypted-enclaves)
