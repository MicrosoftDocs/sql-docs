---
title: Enable automatic digest storage
description: This article discusses how to enable automatic digest storage in Azure SQL Database using the Azure portal, PowerShell, and the Azure CLI.
author: VanMSFT
ms.author: vanto
ms.reviewer: mathoma
ms.date: 02/07/2024
ms.service: azure-sql-database
ms.subservice: security
ms.custom: devx-track-azurecli, ignite-2023
ms.topic: how-to
zone_pivot_groups: as1-azuresql-sql
monikerRange: "= azuresqldb-current||>= sql-server-ver16||>= sql-server-linux-ver16||=azuresqldb-mi-current"
---

# Enable automatic digest storage

[!INCLUDE [SQL Server 2022 Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

::: zone pivot="as1-azure-sql-database"

In this article, we'll describe how you can configure automatic generation and storage of database digests through the Azure portal, PowerShell, or the Azure CLI.

## Prerequisites

- Have an active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/free/).
- An Azure SQL Database

## Enable automatic digest storage using the Azure portal

# [Portal](#tab/Portal)

1. Open the [Azure portal](https://portal.azure.com/) and locate the database for which you want to enable automatic digest storage. Select that database in SQL Database.

   > [!NOTE]
   > Enable automatic digest storage can also be configured when creating a new database.

1. In **Security**, select the **Ledger** option. 

1. In the **Ledger** pane, select **Enable automatic digest storage**. Select the storage type. You can choose between Azure Storage or Azure Confidential Ledger (ACL). Depending on the storage type you picked, you have to select an existing storage account or ACL, or create a new one. The storage container name is fixed and can't be modified.

   :::image type="content" source="media/ledger/automatic-digest-management.png" alt-text="Screenshot that shows the selections for enabling digest storage.":::

1. Select **Save** to save your automatic digest storage configuration.

# [PowerShell](#tab/PowerShell)

## Enable database digest uploads using PowerShell

Update the database to start uploading ledger digests to the Azure Blob Storage account or Azure Confidential Ledger, by using the [Enable-AzSqlDatabaseLedgerDigestUpload](/powershell/module/az.sql/enable-azsqldatabaseledgerdigestupload) cmdlet. When the endpoint parameter is an Azure Blob Storage endpoint, the database server will create a new container, named **sqldbledgerdigests**, within the storage account and it will start writing ledger digests to the container.
  
In the following script, be sure to modify the following parameters: *ResourceGroupName, ServerName, DatabaseName and Endpoint (ACL endpoint or Azure Storage endpoint)*: 


```azurepowershell-interactive
Write-host "Enabling ledger digest upload..." 
$ledgerDigestUploadConfig = Enable-AzSqlDatabaseLedgerDigestUpload `
     -ResourceGroupName "ResourceGroup01" `
     -ServerName "Server01" `
     -DatabaseName "Database01" `
     -Endpoint "https://ledgerstorage.blob.core.windows.net"
$ledgerDigestUploadConfig
```

# [Azure CLI](#tab/AzureCLI)

## Enable database digest uploads using the Azure CLI

Update the database to start uploading ledger digests to the Azure Blob Storage account or Azure Confidential Ledger, by using the [az sql db ledger-digest-uploads enable](/cli/azure/sql/db) command.  

*Make sure you modify the parameters resource-group, server, name and endpoint (ACL endpoint or Azure Storage endpoint)*

```azurecli-interactive
az sql db ledger-digest-uploads enable \
    --resource-group ResourceGroup01 \
    --server Server01 \
    --name Database01 \
    --endpoint https://ledgerstorage.blob.core.windows.net
```

---
::: zone-end

::: zone pivot="as1-azure-sql-managed-instance"

In this article, we'll describe how you can configure automatic generation and storage of database digests through the Azure portal, PowerShell, or the Azure CLI.

## Prerequisites

- Have an active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/free/).
- An Azure SQL Managed Instance

## Enable automatic digest storage using the Azure portal

# [Portal](#tab/Portal2)

1. Open the [Azure portal](https://portal.azure.com/) and locate the managed database for which you want to enable automatic digest storage.
   
   > [!NOTE]
   > Enable automatic digest storage can also be configured when creating a new database.

1. In **Security**, select the **Ledger** option. 

1. In the **Ledger** pane, select **Enable automatic digest storage**. Select the storage type. You can choose between Azure Storage or Azure Confidential Ledger (ACL). Depending on the storage type you picked, you have to select an existing storage account or ACL, or create a new one. The storage container name is fixed and can't be modified.

   :::image type="content" source="media/ledger/automatic-digest-management-sql-managed-instance.png" alt-text="Screenshot that shows the selections for enabling digest storage.":::

1. Select **Save** to save your automatic digest storage configuration.

# [PowerShell](#tab/PowerShell2)

## Enable database digest uploads using PowerShell

Update the database to start uploading ledger digests to the Azure Blob Storage account or Azure Confidential Ledger. When the endpoint parameter is an Azure Blob Storage endpoint, the database server will create a new container, named **sqldbledgerdigests**, within the storage account and it will start writing ledger digests to the container.
  
> [!NOTE]
> Make sure you modify the parameters *ResourceGroupName, InstanceName, DatabaseName and Endpoint (ACL endpoint or Azure Storage endpoint)*.

```azurepowershell-interactive
Write-host "Enabling ledger digest upload..." 
$ledgerDigestUploadConfig = Enable-AzSqlInstanceDatabaseLedgerDigestUpload `
     -ResourceGroupName "ResourceGroup01" `
     -InstanceName "ManagedInstance01" `
     -DatabaseName "Database01" `
     -Endpoint "https://ledgerstorage.blob.core.windows.net"
$ledgerDigestUploadConfig
```

# [Azure CLI](#tab/AzureCLI2)

## Enable database digest uploads using the Azure CLI

Update the database to start uploading ledger digests to the Azure Blob Storage account or Azure Confidential Ledger, by using the [az sql midb ledger-digest-uploads enable](/cli/azure/sql/midb) command.  

> [!NOTE]
> Make sure you modify the parameters *resource-group, managed-instance, name and endpoint (ACL endpoint or Azure Storage endpoint)*

```azurecli-interactive
az sql midb ledger-digest-uploads enable \
    --resource-group ResourceGroup01 \
    --managed-instance ManagedInstance01 \
    --name Database01 \
    --endpoint https://ledgerstorage.blob.core.windows.net
```

---
::: zone-end

::: zone pivot="as1-sql-server"

In this article, we'll describe how you can configure automatic generation and storage of database digests through using T-SQL in **[!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)]**. For information on configuring automatic generation and storage of database digests in Azure SQL Database, use the switch at the top of this page to toggle over to Azure SQL Database.

## Prerequisites

- [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)]
- [SQL Server Management Studio (SSMS)](../../../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](../../../azure-data-studio/download-azure-data-studio.md)
- An Azure Blob Storage
- An Azure Storage container
- A [SQL Server credential](../authentication-access/credentials-database-engine.md). For more information, see [Digest Management](ledger-digest-management.md).  

## Enable database digest uploads using T-SQL

To enable uploading ledger digests, specify the endpoint of an Azure Blob storage account. To disable uploading ledger digests, set the option value to `OFF`. The default is `OFF`.

1. Sign into your [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] instance using SSMS or Azure Data Studio.
1. Configure automatic generation and storage of database digests using the following T-SQL statement:

   ```sql
   ALTER DATABASE SCOPED CONFIGURATION
    SET LEDGER_DIGEST_STORAGE_ENDPOINT = 'https://ledgerstorage.blob.core.windows.net';
   ```

For more information, see [ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../../../t-sql/statements/create-database-transact-sql.md).

::: zone-end

## Related content

- [Ledger overview](ledger-overview.md)
- [Digest management](ledger-digest-management.md)
- [Verify a ledger table to detect tampering](ledger-verify-database.md)
- [sys.database_ledger_digest_locations](../../system-catalog-views/sys-database-ledger-digest-locations-transact-sql.md)
- [sp_verify_database_ledger_from_digest_storage](../../system-stored-procedures/sys-sp-verify-database-ledger-from-digest-storage-transact-sql.md)
