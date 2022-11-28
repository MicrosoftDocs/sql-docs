---
title: "Tutorial: Getting started with Always Encrypted"
description: This tutorial teaches you how to encrypt columns using Always Encrypted and how to query encrypted columns in SQL Server, Azure SQL Database, and Azure SQL Managed Instance.
ms.custom:
ms.date: 11/17/2022
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: tutorial
author: jaszymas
ms.author: jaszymas
---
# Tutorial: Getting started with Always Encrypted

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This tutorial teaches you how to get started with [Always Encrypted](always-encrypted-database-engine.md). It will show you:

> [!div class="checklist"]
>
> - How to encrypt selected columns in your database.
> - How to query encrypted columns.

> [!NOTE]
> If you're looking for information on [Always Encrypted with secure enclaves](always-encrypted-enclaves.md), see the following tutorials instead:
>
> - [Tutorial: Getting started with Always Encrypted with secure enclaves in Azure SQL Database](/azure/azure-sql/database/always-encrypted-enclaves-getting-started)
> - [Tutorial: Getting started with Always Encrypted with secure enclaves in SQL Server](../tutorial-getting-started-with-always-encrypted-enclaves.md)

## Prerequisites

For this tutorial, you need:

- An **empty** database in Azure SQL Database, Azure SQL Managed Instance, or SQL Server. The below instructions assume the database name is **ContosoHR**. You need to be an owner of the database (a member of the **db_owner** role). For information on how to create a database, see [Quickstart: Create a single database - Azure SQL Database](/azure/azure-sql/database/single-database-create-quickstart) or [Create a database in SQL Server](../../databases/create-a-database.md).
- Optional, but recommended, especially if your database is in Azure: a key vault in Azure Key Vault. For information on how to create a key vault, see [Quickstart: Create a key vault using the Azure portal](/azure/key-vault/general/quick-create-portal).
  - If your key vault uses the access policy permissions model, make sure you have the following key permissions in the key vault: `get`, `list`, `create`, `unwrap key`, `wrap key`, `verify`, `sign`. See [Assign a Key Vault access policy](/azure/key-vault/general/assign-access-policy).
  - If you're using the Azure role-based access control (RBAC) permission model, make you sure you're a member of the [Key Vault Crypto Officer](/azure/role-based-access-control/built-in-roles#key-vault-crypto-officer) role for your key vault. See [Provide access to Key Vault keys, certificates, and secrets with an Azure role-based access control](/azure/key-vault/general/rbac-migration).
- The latest version of [SQL Server Management Studio (SSMS)](../../../ssms/download-sql-server-management-studio-ssms.md) or the latest version of the [SqlServer](../../../powershell/download-sql-server-ps-module.md) and [Az](/powershell/azure/new-azureps-module-az) PowerShell modules. The Az PowerShell module is required only if you're using Azure Key Vault.

## Step 1: Create and populate the database schema

In this step, you'll create the **HR** schema and the **Employees** table. Then, you'll populate the table with some data.

# [SSMS](#tab/ssms)

1. Connect to your database. For instructions on how to connect to a database from SSMS, see [Quickstart: Connect and query an Azure SQL Database or an Azure SQL Managed Instance using SQL Server Management Studio (SSMS)](../../../ssms/quickstarts/ssms-connect-query-azure-sql.md) or [Quickstart: Connect and query a SQL Server instance using SQL Server Management Studio (SSMS)](../../../ssms/quickstarts/ssms-connect-query-sql-server.md).
1. Open a new query window for the **ContosoHR** database.
1. Paste in and execute the below statements to create a new table, named **Employees**.

    ```sql
    CREATE SCHEMA [HR];
    GO
    
    CREATE TABLE [HR].[Employees]
    (
        [EmployeeID] [int] IDENTITY(1,1) NOT NULL
        , [SSN] [char](11) NOT NULL
        , [FirstName] [nvarchar](50) NOT NULL
        , [LastName] [nvarchar](50) NOT NULL
        , [Salary] [money] NOT NULL
    ) ON [PRIMARY];
    ```

1. Paste in and execute the below statements to add a few employee records to the **Employees** table.

    ```sql
    INSERT INTO [HR].[Employees]
    (
        [SSN]
        , [FirstName]
        , [LastName]
        , [Salary]
    )
    VALUES
    (
        '795-73-9838'
        , N'Catherine'
        , N'Abel'
        , $31692
    );

    INSERT INTO [HR].[Employees]
    (
        [SSN]
        , [FirstName]
        , [LastName]
        , [Salary]
    )
    VALUES
    (
        '990-00-6818'
        , N'Kim'
        , N'Abercrombie'
        , $55415
    );
    ```

# [PowerShell](#tab/powershell)

In a PowerShell session, execute the following commands. Make sure you update the connection string with the address of your server and authentication settings that are valid for your database.

```powershell
Import-Module "SqlServer"

# Set your database connection string
$connectionString = "Server = myServerAddress; Database = ContosoHR; ..."

# Create a new table, named Employees.
$query = @'
    CREATE SCHEMA [HR];
    GO
    
    CREATE TABLE [HR].[Employees]
    (
        [EmployeeID] [int] IDENTITY(1,1) NOT NULL
        , [SSN] [char](11) NOT NULL
        , [FirstName] [nvarchar](50) NOT NULL
        , [LastName] [nvarchar](50) NOT NULL
        , [Salary] [money] NOT NULL
    ) ON [PRIMARY];
'@
Invoke-SqlCmd -ConnectionString $connectionString -Query $query

# Add a few rows to the Employees table.
$query = @'
    INSERT INTO [HR].[Employees]
    (
        [SSN]
        , [FirstName]
        , [LastName]
        , [Salary]
    )
    VALUES
    (
        '795-73-9838'
        , N'Catherine'
        , N'Abel'
        , $31692
    );

    INSERT INTO [HR].[Employees]
    (
        [SSN]
        , [FirstName]
        , [LastName]
        , [Salary]
    )
    VALUES
    (
        '990-00-6818'
        , N'Kim'
        , N'Abercrombie'
        , $55415
    );
'@
Invoke-SqlCmd -ConnectionString $connectionString -Query $query
```

---

## Step 2: Encrypt columns

In this step, you'll provision a column master key and a column encryption key for Always Encrypted. Then, you'll encrypt the **SSN** and **Salary** columns in the **Employees** table.

# [SSMS](#tab/ssms)

SSMS provides a wizard that helps you easily configure Always Encrypted by setting up a column master key, a column encryption key, and encrypt selected columns.

1. In **Object Explorer**, expand **Databases** > **ContosoHR** > **Tables**.
1. Right-click the **Employees** table and select **Encrypt Columns** to open the Always Encrypted wizard.

   :::image type="content" source="media/always-encrypted-database-engine/always-encrypted-wizard-opening.png" alt-text="Screenshot of opening the Always Encrypted Wizard.":::

1. Select **Next** on the **Introduction** page of the wizard.
1. On the **Column Selection** page.
   1. Select the **SSN** and **Salary** columns. Choose deterministic encryption for the **SSN** column and randomized encryption for the **Salary** column. Deterministic encryption supports queries, such as point lookup searches that involve equality comparisons on encrypted columns. Randomized encryption doesn't support any computations on encrypted columns.
   1. Leave **CEK-Auto1 (New)** as the column encryption key for both columns. This key doesn't exist yet and will be generated by the wizard.
   1. Select **Next**.

   :::image type="content" source="media/always-encrypted-database-engine/always-encrypted-wizard-column-selection.png" alt-text="Screenshot of the Always Encrypted Wizard column selection.":::

1. On the **Master Key Configuration** page, configure a new column master key that will be generated by the wizard. First, you need to select where you want to store your column master key. The wizard supports two key store types:

   - Azure Key Vault - recommended if your database is in Azure
   - Windows certificate store

   In general, Azure Key Vault is the recommended option, especially if your database is in Azure.

   - To use Azure Key Vault:
     1. Select **Azure Key Vault**.
     1. Select **Sign in** and complete signing in to Azure. 
     1. After you've signed in, the page will display the list of subscriptions and key vaults, you have access to. Select an Azure subscription containing the key vault, you want to use.
     1. Select your key vault.
     1. Select **Next**.

     :::image type="content" source="media/always-encrypted-database-engine/always-encrypted-wizard-configuring-master-key-in-key-vault.png" alt-text="Screenshot of the Always Encrypted Wizard master key selection using Azure Key Vault.":::

   - To use Windows certificate store:
     1. Select **Windows certificate store**.
     1. Leave the default selection of **Current User** - this will instruct the wizard to generate a certificate (your new column master key) in the **Current User** store.

        :::image type="content" source="media/always-encrypted-database-engine/always-encrypted-wizard-configuring-master-key-in-certificate-store.png" alt-text="Screenshot of the Always Encrypted Wizard master key selection using the certificate store.":::

     1. Select **Next**.

1. On the **Run settings** page, you're asked if you want to proceed with encryption or generate a PowerShell script to be executed later. Leave the default settings and select **Next**.
1. On the **Summary** page, the wizard informs you about the actions it will execute. Check all the information is correct and select **Finish**.
1. On the **Results** page, you can monitor the progress of the wizard's operations. Wait until all operations complete successfully and select **Close**.

   :::image type="content" source="media/always-encrypted-database-engine/always-encrypted-wizard-summary.png" alt-text="Screenshot of the Always Encrypted Wizard summary.":::

1. (Optional) Explore the changes the wizard has made in your database.
    1. Expand **ContosoHR** > **Security** > **Always Encrypted Keys** to explore the metadata objects for the column master key and the column encryption that the wizard created.
    1. You can also run the below queries against the system catalog views that contain key metadata.

       ```sql
       SELECT * FROM sys.column_master_keys;
       SELECT * FROM sys.column_encryption_keys
       SELECT * FROM sys.column_encryption_key_values
       ```

    1. In **Object Explorer**, right-click the **Employees** table and select **Script Table as** > **CREATE To** > **New Query Editor Window**. This will open a new query window with the **CREATE TABLE** statement for the **Employees** table. Note the **ENCRYPTED WITH** clause that appears in the definitions of the **SSN** and **Salary** columns.

    1. You can also run the below query against **sys.columns** to retrieve column-level encryption metadata for the two encrypted columns.

       ```sql
       SELECT
       [name]
       , [encryption_type]
       , [encryption_type_desc]
       , [encryption_algorithm_name]
       , [column_encryption_key_id]
       FROM sys.columns
       WHERE [encryption_type] IS NOT NULL;
       ```

# [PowerShell](#tab/powershell)

1. Create a column master key in your key store.
    - If you're using Azure Key Vault, execute the below commands to create an asymmetric key in your key vault. Make sure you provide the correct ID of your subscription, the name of the resource group containing your key vault, and your key vault name.

      ```powershell
      Import-Module "Az"
      Connect-AzAccount
      $subscriptionId = "<your Azure subscription ID"
      $resourceGroup = "your resource group name containing your key vault"
      $keyVaultName = "your vault name"
      $keyVaultKeyName = "your key name"

      # Switch to your subscription.
      Set-AzConteXt -SubscriptionId $subscriptionId

      # To validate the above key vault settings, get the key vault properties.
      Get-AzKeyVault -VaultName $keyVaultName -ResourceGroupName $resourceGroup 

      # Create a key in the key vault.
      $keyVaultKey = Add-AzKeyVaultKey -VaultName $keyVaultName -Name $keyVaultKeyName -Destination "Software"
      $keyVaultKey
      ```

    - If you're using Windows certificate store, execute the below commands to create a certificate in your Current User store.

      ```powershell
      $cert = New-SelfSignedCertificate -Subject "HRCMK" -CertStoreLocation Cert:CurrentUser\My -KeyExportPolicy Exportable -Type DocumentEncryptionCert -KeyUsage DataEncipherment -KeySpec KeyExchange
      ```

1. Connect to your database, using the SqlServer PowerShell module. Make sure you provide a valid connection string for your database.

   ```powershell
   $database = Get-SqlDatabase -ConnectionString $connectionString
   $database
   ```

1. Provision a column master key metadata object (that references the physical column master key that you've created in your key store) in your database.

   - If you're using Azure Key Vault, execute the below commands.

     ```powershell
     # Sign in to Azure for the SqlServer PowerShell module
     Add-SqlAzureAuthenticationContext -Interactive

     # Create a SqlColumnMasterKeySettings in-memory object referencing the key you've created in your key vault. 
     $cmkSettings = New-SqlAzureKeyVaultColumnMasterKeySettings -KeyURL $keyVaultKey.Key.Kid

     # Create column master key metadata object (referencing your certificate), named CMK1, in the database.
     $cmkName = "CMK1"
     New-SqlColumnMasterKey -Name $cmkName -InputObject $database -ColumnMasterKeySettings $cmkSettings
     ```

   - If you're using Windows certificate store, execute the below commands.

     ```powershell
     # Create a SqlColumnMasterKeySettings in-memory object referencing your certificate.
     $cmkSettings = New-SqlCertificateStoreColumnMasterKeySettings -CertificateStoreLocation "CurrentUser" -Thumbprint $cert.Thumbprint

     # Create column master key metadata object, named CMK1, in the database.
     $cmkName = "CMK1"
     New-SqlColumnMasterKey -Name $cmkName -InputObject $database -ColumnMasterKeySettings $cmkSettings
     ```

1. Generate a column encryption key, encrypt it with the column master key you've created, and create a column encryption key metadata object in the database.

    ```powershell
    $cekName = "CEK1"
    New-SqlColumnEncryptionKey -Name $cekName -InputObject $database -ColumnMasterKey $cmkName
    ```

1. Encrypt **SSN** and **Salary** columns in the **Employees** Table. Choose deterministic encryption for the **SSN** column and randomized encryption for the **Salary** column. Deterministic encryption supports queries, such as point lookup searches that involve equality comparisons on encrypted columns. Randomized encryption doesn't support any computations on encrypted columns.

    ```powershell
    # Encrypt the SSN and Salary columns 
    $ces = @()
    $ces += New-SqlColumnEncryptionSettings -ColumnName "HR.Employees.SSN" -EncryptionType "Deterministic" -EncryptionKey $cekName
    $ces += New-SqlColumnEncryptionSettings -ColumnName "HR.Employees.Salary" -EncryptionType "Randomized" -EncryptionKey $cekName
    Set-SqlColumnEncryption -InputObject $database -ColumnEncryptionSettings $ces -LogFileDirectory .

   ```

1. (Optional) Explore the changes, you've made in your database.
    - Run the below commands to query system catalog views that contain metadata about the column master key and the column encryption key that you created.

      ```powershell
      $query = @'
      SELECT * FROM sys.column_master_keys;
      SELECT * FROM sys.column_encryption_keys
      SELECT * FROM sys.column_encryption_key_values
      '@

      Invoke-SqlCmd -ConnectionString $connectionString -Query $query
      ```

    - Run the below commands to query **sys.columns**  to retrieve column-level encryption metadata for the two encrypted columns.

      ```powershell
      $query = @'
      SELECT
      [name]
      , [encryption_type]
      , [encryption_type_desc]
      , [encryption_algorithm_name]
      , [column_encryption_key_id]
      FROM sys.columns
      WHERE [encryption_type] IS NOT NULL;
      '@

      Invoke-SqlCmd -ConnectionString $connectionString -Query $query
      ```

---

## Step 3: Query encrypted columns

# [SSMS](#tab/ssms)

1. Connect to your database with Always Encrypted disabled for your connection.
    1. Open a new query window.
    1. Right-click anywhere in the query window and select **Connection** > **Change Connection**. This will open the **Connect to Database Engine** dialog.
    1. Select **Options <<**. This will show additional tabs in the **Connect to Database Engine** dialog.
    1. Select the **Always Encrypted** tab.
    1. Make sure **Enable Always Encrypted (column encryption)** isn't selected.
    1. Select **Connect**.

    :::image type="content" source="media/always-encrypted-database-engine/always-encrypted-ssms-connect-disabled.png" alt-text="Screenshot of the SSMS connection option for Always Encrypted disabled.":::

1. Paste in and execute the following query. The query should return binary encrypted data.

    ```sql
    SELECT [SSN], [Salary] FROM [HR].[Employees]
    ```

    :::image type="content" source="media/always-encrypted-database-engine/always-encrypted-ssms-retrieving-cyphertext.png" alt-text="Screenshot of cyphertext results from encrypted columns.":::

1. Connect to your database with Always Encrypted enabled for your connection.
    1. Right-click anywhere in the query window and select **Connection** > **Change Connection**. This will open the **Connect to Database Engine** dialog.
    1. Select **Options <<**. This will show additional tabs in the **Connect to Database Engine** dialog.
    1. Select the **Always Encrypted** tab.
    1. Select **Enable Always Encrypted (column encryption)**.
    1. Select **Connect**.

    :::image type="content" source="media/always-encrypted-database-engine/always-encrypted-ssms-connect-enabled.png" alt-text="Screenshot of the SSMS connection option for Always Encrypted enabled.":::

1. Rerun the same query. Since you're connected with Always Encrypted enabled for your database connection, the client driver in SSMS will attempt to decrypt data stored in both encrypted columns. If you use Azure Key Vault, you may be prompted to sign into Azure.

    :::image type="content" source="media/always-encrypted-database-engine/always-encrypted-ssms-retrieving-plaintext.png" alt-text="Screenshot of plaintext results from encrypted columns.":::

1. Enable [Parameterization for Always Encrypted](always-encrypted-query-columns-ssms.md#param). This feature allows you to run queries that filter data by encrypted columns (or insert data to encrypted columns).
    1. Select **Query** from the main menu of SSMS.
    2. Select **Query Options...**.
    3. Navigate to **Execution** > **Advanced**.
    4. Make sure **Enable Parameterization for Always Encrypted** is checked.
    5. Select **OK**.

    :::image type="content" source="media/always-encrypted-database-engine/always-encrypted-ssms-enable-parameterization-query.png" alt-text="Screenshot enabling parameterization in an existing query window.":::

1. Paste in and execute the below query, which filters data by the encrypted **SSN** column. The query should return one row containing plaintext values.

    ```sql
    DECLARE @SSN [char](11) = '795-73-9838'
    SELECT [SSN], [Salary] FROM [HR].[Employees]
    WHERE [SSN] = @SSN
    ```

1. Optionally, if you're using Azure Key Vault configured with the access policy permissions model, follow the below steps to see what happens when a user tries to retrieve plaintext data from encrypted columns without having access to the column master key protecting the data.
    1. Remove the key `unwrap` permission for yourself in the access policy for your key vault. For more information, see [Assign a Key Vault access policy](/azure/key-vault/general/assign-access-policy).
    1. Since the client driver in SSMS caches the column encryption keys acquired from a key vault for 2 hours, close SSMS and open it again. This will ensure the key cache is empty.
    1. Connect to your database with Always Encrypted enabled for your connection.
    1. Paste in and execute the following query. The query should fail with the error message indicating you're missing the required `unwrap` permission.

    ```sql
    SELECT [SSN], [Salary] FROM [HR].[Employees]
    ```

# [PowerShell](#tab/powershell)

1. Connect to your database with Always Encrypted disabled and run a query to read data from encrypted columns. The query should return encrypted data as binary arrays.

    ```powershell
    $query = "SELECT [SSN], [Salary] FROM [HR].[Employees]"
    Invoke-SqlCmd -ConnectionString $connectionString -Query $query
    ```

1. Connect to your database with Always Encrypted enabled and run a query to read data from encrypted columns. Since you have access to the column master key protecting your encrypted columns, the query should return plaintext data.

    ```powershell
    $query = "SELECT [SSN], [Salary] FROM [HR].[Employees]"
    Invoke-SqlCmd -ConnectionString "$connectionString; Column Encryption Setting = Enabled" -Query $query
    ```

> [!NOTE]
> **Invoke-SqlCmd** doesn't support queries that can filter by or insert data to encrypted columns. Such queries need to be parameterized, and **Invoke-SqlCmd** doesn't support parameterized queries.

---

## Next steps

- [Develop applications using Always Encrypted](always-encrypted-client-development.md)

## See also

- [Always Encrypted documentation](/azure/azure-sql/database/always-encrypted-landing)
- [Always Encrypted with secure enclaves documentation](/azure/azure-sql/database/always-encrypted-with-secure-enclaves-landing)
- [Provision Always Encrypted keys using SQL Server Management Studio](configure-always-encrypted-keys-using-ssms.md)
- [Configure Always Encrypted using PowerShell](configure-always-encrypted-using-powershell.md)
- [Always Encrypted wizard](./always-encrypted-wizard.md)
- [Query columns using Always Encrypted with SQL Server Management Studio](always-encrypted-query-columns-ssms.md)