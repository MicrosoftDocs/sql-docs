---
title: "Tutorial: Getting Started Using Always Encrypted with Intel SGX Enclaves"
description: Tutorial on how to create a basic environment for Always Encrypted with Intel SGX enclaves in Azure SQL Database, how to encrypt data in-place, and issue rich confidential queries against encrypted columns using SQL Server Management Studio (SSMS).
author: Pietervanhove
ms.author: pivanho
ms.reviewer: vanto, mathoma
ms.date: 06/17/2026
ms.service: azure-sql-database
ms.subservice: security
ms.topic: tutorial
ms.custom:
  - devx-track-azurepowershell
  - sfi-image-nochange
---
# Tutorial: Getting started using Always Encrypted with Intel SGX enclaves in Azure SQL Database

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

This tutorial shows you how to get started with [Always Encrypted with secure enclaves](/sql/relational-databases/security/encryption/always-encrypted-enclaves) in Azure SQL Database. You use [Intel Software Guard Extensions (Intel SGX) enclaves](https://www.intel.com/content/www/us/en/products/docs/accelerator-engines/software-guard-extensions.html). The tutorial covers:

> [!div class="checklist"]
>
> - How to create an environment for testing and evaluating Always Encrypted with Intel SGX enclaves.
> - How to encrypt data in-place and issue rich confidential queries against encrypted columns using SQL Server Management Studio (SSMS).

## Prerequisites

- An active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn). You need to be a member of the Contributor role or the Owner role for the subscription to be able to create resources and configure an attestation policy.
- Optional, but recommended for storing your column master key for Always Encrypted: a key vault in Azure Key Vault. For information on how to create a key vault, see [Quickstart: Create a key vault using the Azure portal](/azure/key-vault/general/quick-create-portal). 
  - If your key vault uses the access policy permissions model, make sure you have the following key permissions in the key vault: `get`, `list`, `create`, `unwrap key`, `wrap key`, `verify`, `sign`. For more information, see [Assign a Key Vault access policy](/azure/key-vault/general/assign-access-policy).
  - If you're using the Azure role-based access control (RBAC) permission model, make sure you're a member of the [Key Vault Crypto Officer](/azure/role-based-access-control/built-in-roles#key-vault-crypto-officer) role for your key vault. See [Provide access to Key Vault keys, certificates, and secrets with an Azure role-based access control](/azure/key-vault/general/rbac-migration).
- The latest version of [SQL Server Management Studio (SSMS)](/ssms/install/install).

### PowerShell requirements

> [!NOTE]
> The prerequisites listed in this section apply only if you choose to use PowerShell for some of the steps in this tutorial. If you plan to use the Azure portal instead, you can skip this section. Microsoft recommends using PowerShell 7 or later when running Always Encrypted PowerShell scripts. PowerShell 7 provides improved cross-platform support, better performance, and the latest compatibility with the SqlServer module (v22+), which is required for many Always Encrypted scenarios.

Az PowerShell module version 9.3.0 or later is required. For details on how to install the Az PowerShell module, see [Install the Azure Az PowerShell module](/powershell/azure/install-az-ps). To determine the version of the Az PowerShell module that is installed on your machine, run the following command from PowerShell.

```powershell
Get-InstalledModule -Name Az
```


## Step 1: Create and configure a server and a DC-series database

In this step, you'll create a new Azure SQL Database logical server and a new database using DC-series hardware, required for Always Encrypted with secure enclaves. For more information, see [DC-series](service-tiers-sql-database-vcore.md#dc-series).

# [Portal](#tab/azure-portal)

1. Go to [Azure SQL hub at aka.ms/azuresqlhub](https://aka.ms/azuresqlhub). 
1. In the resource menu, expand **Azure SQL Database** and select **SQL databases**.
1. From the **+ Create** dropdown list, select **SQL database**.

   :::image type="content" source="media/always-encrypted-enclaves-getting-started-sgx/create-sql-database.png" alt-text="Screenshot from the Azure portal showing the SQL databases page, the Create button, and the SQL database option." lightbox="media/always-encrypted-enclaves-getting-started-sgx/create-sql-database.png":::

1. On the **Basics** tab of the **Create SQL Database** form, under **Project details**, select the desired Azure **Subscription**.
1. For **Resource group**, select **Create new**, enter a name for your resource group, and select **OK**.
1. For **Database name** enter *ContosoHR*.
1. For **Server**, select **Create new**, and fill out the **New server** form with the following values:
   - **Server name**: Enter *mysqlserver*, and add some characters for uniqueness. Server names must be globally unique across Azure (not just within a subscription), so use something like `mysqlserver135` - the portal tells you whether it's available.
   - **Location**: Select a location from the dropdown list.
      > [!IMPORTANT]
      > You need to select a location (an Azure region) that supports both the DC-series hardware and Microsoft Azure Attestation. For the list of regions supporting DC-series, see [DC-series availability](service-tiers-sql-database-vcore.md#dc-series). See the [regional availability of Microsoft Azure Attestation](https://azure.microsoft.com/explore/global-infrastructure/products-by-region/?products=azure-attestation).
   - **Authentication method**: Select **Use SQL Authentication**
   - **Server admin login**: Enter an admin login name, for example: *azureuser*.
   - **Password**: Enter a password that meets requirements, and enter it again in the **Confirm password** field.
   - Select **OK**.
1. Leave **Want to use SQL elastic pool** set to **No**.
1. Under **Compute + storage**, select **Configure database**, and select **Change configuration**.

   :::image type="content" source="media/always-encrypted-enclaves-getting-started-sgx/portal-configure-database.png" alt-text="Screenshot of Azure portal hardware configuration, and where to configure database." lightbox="media/always-encrypted-enclaves-getting-started-sgx/portal-configure-database.png":::

1. Select the **DC-series** hardware configuration, and then select **OK**.
1. Select **Apply**. 
1. Back on the **Basics** tab, verify **Compute + storage** is set to **General Purpose**, **DC, 2 vCores, 32 GB storage**.
1. For **Backup storage redundancy** select **Locally-redundant backup storage**.
1. Select **Next: Networking** at the bottom of the page.

   :::image type="content" source="media/always-encrypted-enclaves-getting-started-sgx/portal-configure-dc-series-database-basics.png" alt-text="Screenshot of Azure portal, showing Configure DC-series database - basics." lightbox="media/always-encrypted-enclaves-getting-started-sgx/portal-configure-dc-series-database-basics.png":::

1. On the **Networking** tab, for **Connectivity method**, select **Public endpoint**.
1. For **Firewall rules**, set **Add current client IP address** to **Yes**. Leave **Allow Azure services and resources to access this server** set to **No**.
1. For **Connection policy**, leave it set to **Default - Uses Redirect policy for all client connections originating inside of Azure and Proxy for all client connections originating outside Azure**.
1. For **Encrypted connections**, leave **Minimum TLS version** to **TLS 1.2**.
1. Select **Review + create** at the bottom of the page.

   :::image type="content" source="media/always-encrypted-enclaves-getting-started-sgx/portal-configure-database-networking.png" alt-text="Screenshot of the Azure portal Create SQL Database, networking tab." lightbox="media/always-encrypted-enclaves-getting-started-sgx/portal-configure-database-networking.png":::

1. On the **Review + create** page, after reviewing, select **Create**.

# [PowerShell](#tab/azure-powershell)

1. Open a PowerShell console and import the required version of the Az PowerShell module.

   ```powershell
   Import-Module "Az" -MinimumVersion "9.3.0"
   ```

1. Sign into Azure. If needed, [switch to the subscription](/powershell/azure/manage-subscriptions-azureps) you're using for this tutorial.

   ```powershell
   Connect-AzAccount
   $subscriptionId = "<your subscription ID>"
   $context = Set-AzContext -Subscription $subscriptionId
   ```

1. Create a new resource group.

   > [!IMPORTANT]
   > You need to create your resource group in a region (location) that supports both the DC-series hardware and Microsoft Azure Attestation. For the list of regions supporting DC-series, see [DC-series availability](service-tiers-sql-database-vcore.md#dc-series). See the [regional availability of Microsoft Azure Attestation](https://azure.microsoft.com/explore/global-infrastructure/products-by-region/?products=azure-attestation).

   ```powershell
   $resourceGroupName = "<your new resource group name>"
   $location = "<Azure region supporting DC-series and Microsoft Azure Attestation>"
   New-AzResourceGroup -Name $resourceGroupName -Location $location
   ```

1. Create an Azure SQL logical server. When prompted, enter the server administrator name and a password. Make sure you remember the admin name and the password - you'll need them later to connect to the server.

   ```powershell
   $serverName = "<your server name>" 
   New-AzSqlServer -ServerName $serverName -ResourceGroupName $resourceGroupName -Location $location -SqlAdministratorCredentials (Get-Credential)
   ```

1. Create a server firewall rule that allows access from the specified IP range.

   ```powershell
   $startIp = "<start of IP range>"
   $endIp = "<end of IP range>"
   $serverFirewallRule = New-AzSqlServerFirewallRule -ResourceGroupName $resourceGroupName `
    -ServerName $serverName `
    -FirewallRuleName "AllowedIPs" -StartIpAddress $startIp -EndIpAddress $endIp
   ```

1. Create a DC-series database.

   ```powershell
   $databaseName = "ContosoHR"
   $edition = "GeneralPurpose"
   $vCore = 2
   $generation = "DC"
   New-AzSqlDatabase -ResourceGroupName $resourceGroupName `
    -ServerName $serverName `
    -DatabaseName $databaseName `
    -Edition $edition `
    -Vcore $vCore `
    -ComputeGeneration $generation
   ```

---

## Step 2: Configure an attestation provider

In this step, you'll create and configure an attestation provider in Microsoft Azure Attestation. This is needed to attest the secure enclave your database uses.

# [Portal](#tab/azure-portal)

1. Browse to the [Create attestation provider](https://portal.azure.com/#create/Microsoft.Attestation) page.
1. On the **Create attestation provider** page, provide the following inputs:

   - **Subscription**: Choose the same subscription you created the Azure SQL logical server in.
   - **Resource Group**: Choose the same resource group you created the Azure SQL logical server in.
   - **Name**: Enter *myattestprovider*, and add some characters for uniqueness. We can't provide an exact attestation provider name to use because names must be globally unique. So enter something like myattestprovider12345, and the portal lets you know if it's available or not.
   - **Location**: Choose the same location as your Azure SQL logical server.
   - **Policy signer certificates file**: Leave this field empty, as you'll configure an unsigned policy.

1. After you provide the required inputs, select **Review + create**.

   :::image type="content" source="media/always-encrypted-enclaves-getting-started-sgx/portal-create-attestation-provider-basics.png" alt-text="Screenshot of the Azure portal Create attestation provider menu." lightbox="media/always-encrypted-enclaves-getting-started-sgx/portal-create-attestation-provider-basics.png":::

1. Select **Create**.
1. Once the attestation provider is created, select **Go to resource**.
1. On the **Overview** tab for the attestation provider, copy the value of the **Attest URI** property to clipboard and save it in a file. This is the attestation URL, you'll need in later steps.  
1. Select **Policy** on the resource menu on the left side of the window or on the lower pane.
1. Set **Attestation Type** to **SGX-IntelSDK**.
1. Select **Configure** on the upper menu.

   :::image type="content" source="media/always-encrypted-enclaves-getting-started-sgx/portal-configure-attestation-policy.png" alt-text="Screenshot of configuring attestation policy in the Azure portal." lightbox="media/always-encrypted-enclaves-getting-started-sgx/portal-configure-attestation-policy.png":::

1. Set **Policy Format** to **Text**. Leave  **Policy options** set to **Enter policy**.
1. In the **Policy text** field, replace the default policy with the following policy. For more information, see [Create and configure an attestation provider](always-encrypted-enclaves-configure-attestation.md#create-and-configure-an-attestation-provider).

   ```output
   version= 1.0;
   authorizationrules 
   {
          [ type=="x-ms-sgx-is-debuggable", value==false ]
           && [ type=="x-ms-sgx-product-id", value==4639 ]
           && [ type=="x-ms-sgx-svn", value>= 2 ]
           && [ type=="x-ms-sgx-mrsigner", value=="e31c9e505f37a58de09335075fc8591254313eb20bb1a27e5443cc450b6e33e5"] 
       => permit();

          [ type=="x-ms-sgx-is-debuggable", value==false ]
           && [ type=="x-ms-sgx-product-id", value==4639 ]
           && [ type=="x-ms-sgx-svn", value>= 2 ]
           && [ type=="x-ms-sgx-mrsigner", value=="a0f8e7f72092fb6a5d5752ffccd47fb3af7027ffb589b24e921e81f5703f3a9a"]
       => permit();
   };
   ```

1. Select **Save**.

   :::image type="content" source="media/always-encrypted-enclaves-getting-started-sgx/portal-edit-attestation-policy.png" alt-text="Screenshot of editing an attestation policy in the Azure portal." lightbox="media/always-encrypted-enclaves-getting-started-sgx/portal-edit-attestation-policy.png":::

1. Select **Refresh** on the upper menu to view the configured policy.

# [PowerShell](#tab/azure-powershell)

1. Copy the following attestation policy and save the policy in a text file (txt). For more information, see [Create and configure an attestation provider](always-encrypted-enclaves-configure-attestation.md#create-and-configure-an-attestation-provider).

   ```output
   version= 1.0;
   authorizationrules 
   {
          [ type=="x-ms-sgx-is-debuggable", value==false ]
           && [ type=="x-ms-sgx-product-id", value==4639 ]
           && [ type=="x-ms-sgx-svn", value>= 2 ]
           && [ type=="x-ms-sgx-mrsigner", value=="e31c9e505f37a58de09335075fc8591254313eb20bb1a27e5443cc450b6e33e5"] 
       => permit();

          [ type=="x-ms-sgx-is-debuggable", value==false ]
           && [ type=="x-ms-sgx-product-id", value==4639 ]
           && [ type=="x-ms-sgx-svn", value>= 2 ]
           && [ type=="x-ms-sgx-mrsigner", value=="a0f8e7f72092fb6a5d5752ffccd47fb3af7027ffb589b24e921e81f5703f3a9a"]
       => permit();
   };
   ```

1. Create an attestation provider.

   ```powershell
   $attestationProviderName = "<your attestation provider name>" 
   New-AzAttestationProvider -Name $attestationProviderName -ResourceGroupName $resourceGroupName -Location $location
   ```
1. Assign yourself to the Attestation Contributor role for the attestation provider, to ensure you have permissions to configure an attestation policy.

   ```powershell
   New-AzRoleAssignment -SignInName $context.Account.Id `
    -RoleDefinitionName "Attestation Contributor" `
    -ResourceName $attestationProviderName `
    -ResourceType "Microsoft.Attestation/attestationProviders" `
    -ResourceGroupName $resourceGroupName
   ```

1. Configure your attestation policy.

   ```powershell
   $policyFile = "<the pathname of the file from step 1 in this section>"
   $teeType = "SgxEnclave"
   $policyFormat = "Text"
   $policy=Get-Content -path $policyFile -Raw
   Set-AzAttestationPolicy -Name $attestationProviderName `
    -ResourceGroupName $resourceGroupName `
    -Tee $teeType `
    -Policy $policy `
    -PolicyFormat  $policyFormat
   ```

1. Retrieve the attestation URL (the Attest URI of your attestation provider).

   ```powershell
   $attestationUrl = (Get-AzAttestationProvider -Name $attestationProviderName -ResourceGroupName $resourceGroupName).AttestUri
   Write-Host "Your attestation URL is: $attestationUrl"
   ```

   The attestation URL should look like this: `https://myattestprovider12345.eus.attest.azure.net`.

---

## Step 3: Populate your database

In this step, you'll create a table and populate it with some data that you'll later encrypt and query.

1. Open SSMS and connect to the **ContosoHR** database in the Azure SQL logical server you created **without** Always Encrypted enabled in the database connection.
    1. In the **Connect to Server** dialog, specify the fully qualified name of your server (for example, *myserver135.database.windows.net*), and enter the administrator user name and the password you specified when you created the server.
    1. Select **Options >>** and select the **Connection Properties** tab. Make sure to select the **ContosoHR** database (not the default, `master` database). 
    1. Select the **Always Encrypted** tab.
    1. Make sure the **Enable Always Encrypted (column encryption)** checkbox is **not** selected.

        :::image type="content" source="./media/always-encrypted-enclaves-getting-started-sgx/ssms-connect-disabled.png" alt-text="Screenshot of Connect to Server using SSMS without Always Encrypted enabled.":::

    5. Select **Connect**.

1. Create a new table, named `Employees`.

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

1. Add a few employee records to the `Employees` table.

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
    1. Enter a name for the new column master key: `CMK1`.
    1. Verify **Allow enclave computations** is selected. (It's selected by default if a secure enclave is enabled for the database. It should be enabled since your database uses the DC-series hardware configuration.)
    1. Select either **Azure Key Vault** (recommended) or **Windows Certificate Store** (**Current User** or **Local Machine**).
       - If you select Azure Key Vault, sign into Azure, select an Azure subscription containing a key vault you want to use, and select your key vault. Select **Generate Key** to create a new key.
       - If you select Windows Certificate Store, select the **Generate Certificate** button to create a new certificate.
         :::image type="content" source="./media/always-encrypted-enclaves/ssms-new-cmk-enclave-key-vault.png" alt-text="Screenshot of the allow enclave computations selection in SSMS when creating a new column master key.":::
    5. Select **OK**.

1. Create a new enclave-enabled column encryption key:

    1. Right-click **Always Encrypted Keys** and select **New Column Encryption Key**.
    1. Enter a name for the new column encryption key: `CEK1`.
    1. In the **Column master key** dropdown list, select the column master key you created in the previous steps.
    1. Select **OK**.

## Step 5: Encrypt some columns in place

In this step, you'll encrypt the data stored in the `SSN` and `Salary` columns inside the server-side enclave, and then test a `SELECT` query on the data.

1. Open a new SSMS instance and connect to your database **with** Always Encrypted enabled for the database connection.
    1. Start a new instance of SSMS.
    1. In the **Connect to Server** dialog, specify the fully qualified name of your server (for example, `<server name>.database.windows.net`), and enter the administrator user name and the password you specified when you created the server.
    1. Select **Options >>** and select the **Connection Properties** tab. Make sure to select the **ContosoHR** database (not the default, `master` database).
    1. Select the **Always Encrypted** tab.
    1. Select the **Enable Always Encrypted (column encryption)** checkbox.
    1. Select **Enable secure enclaves**. (This step applies to SSMS 19 or later.)
    1. Set **Protocol** to **Microsoft Azure Attestation**. (This step applies to SSMS 19 or later.)
    1. Specify your enclave attestation URL that you've obtained by following the steps in [Step 2: Configure an attestation provider](#step-2-configure-an-attestation-provider).

        :::image type="content" source="media/always-encrypted-enclaves-getting-started-sgx/ssms-connect-microsoft-azure-attestation.png" alt-text="Screenshot of the SSMS Connect to Server dialog Always Encrypted tab, with attestation enabled.":::

    1. Select **Connect**.
    1. If you're prompted to enable Parameterization for Always Encrypted queries, select **Enable**.

1. Using the same SSMS instance (with Always Encrypted enabled), open a new query window and encrypt the `SSN` and `Salary` columns by running the following T-SQL statements:

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
    > Notice the `ALTER DATABASE SCOPED CONFIGURATION CLEAR PROCEDURE_CACHE` statement to clear the query plan cache for the database in the above script. After you have altered the table, you need to clear the plans for all batches and stored procedures that access the table to refresh parameters encryption information. 

1. To verify the `SSN` and `Salary` columns are now encrypted, open a new query window in the SSMS instance **without** Always Encrypted enabled for the database connection and execute the following statement. The query window should return encrypted values in the `SSN` and `Salary` columns. If you execute the same query using the SSMS instance with Always Encrypted enabled, you should see the data decrypted.

    ```sql
    SELECT * FROM [HR].[Employees];
    ```

## Step 6: Run rich queries against encrypted columns

You can run rich queries against the encrypted columns. Some query processing will be performed inside your server-side enclave.

1. In the SSMS instance **with** Always Encrypted enabled, make sure Parameterization for Always Encrypted is also enabled.
    1. Select **Tools** from the main menu of SSMS.
    1. Select **Options...**.
    1. Navigate to **Query Execution** > **SQL Server** > **Advanced**.
    1. Ensure that **Enable Parameterization for Always Encrypted** is checked.
    1. Select **OK**.
1. Open a new query window, paste in the following query, and execute. The query should return plaintext values and rows meeting the specified search criteria.

    ```sql
    DECLARE @SSNPattern [char](11) = '%6818';
    DECLARE @MinSalary [money] = $1000;
    SELECT * FROM [HR].[Employees]
    WHERE SSN LIKE @SSNPattern AND [Salary] >= @MinSalary;
    ```

1. Try the same query again in the SSMS instance that doesn't have Always Encrypted enabled. A failure should occur.

<a id="next-steps"></a>

## Tutorials

After completing this tutorial, you can go to one of the following tutorials:

- [Tutorial: Develop a .NET application using Always Encrypted with secure enclaves](/sql/connect/ado-net/sql/tutorial-always-encrypted-enclaves-develop-net-apps)
- [Tutorial: Develop a .NET Framework application using Always Encrypted with secure enclaves](/sql/relational-databases/security/tutorial-always-encrypted-enclaves-develop-net-framework-apps)
- [Tutorial: Creating and using indexes on enclave-enabled columns using randomized encryption](/sql/relational-databases/security/tutorial-creating-using-indexes-on-enclave-enabled-columns-using-randomized-encryption)

## Related content

- [Configure and use Always Encrypted with secure enclaves](/sql/relational-databases/security/encryption/configure-always-encrypted-enclaves)
