---
title: Configure automatic database digests
description: This article discusses how to configure automatic database digest in Azure SQL Database using the Azure portal, PowerShell, and the Azure CLI.
ms.service: sql-database
ms.subservice: security
ms.devlang:
ms.topic: how-to
author: VanMSFT
ms.author: vanto
ms.reviewer: kendralittle, mathoma
ms.date: "04/05/2022"
---

# Configure automatic database digests

[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]

> [!NOTE]
> Azure SQL Database ledger is currently in public preview.

In this article, we will describe how you can configure automatic generation and storage of database digests through the Azure portal, PowerShell, or the Azure CLI.

## Prerequisites

- Have an active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/free/).
- An Azure SQL Database

## Enable automatic digest storage

# [Portal](#tab/Portal)

1. Open the [Azure portal](https://portal.azure.com/) and locate the database for which you want to enable automatic digest storage. Select that database in SQL Database.
1. In **Security**, select the **Ledger** option. :::image type="content" source="media/ledger/ledger-portal-manage-ledger.png" alt-text="Screenshot that shows the Azure portal with the Security Ledger tab selected."::: 
1. In the **Ledger** pane, select **Enable automatic digest storage**. Select the storage type. You can choose between Azure Storage or Azure Confidential Ledger (ACL). Depending on the storage type you picked, you have to select an existing storage account or ACL, or create a new one. Note that the storage container name is fixed and cannot be modified. :::image type="content" source="media/ledger/automatic-digest-management.png" alt-text="Screenshot that shows the selections for enabling digest storage.":::
1. Click **Save** to save your automatic digest storage configuration. 

# [PowerShell](#tab/PowerShell)
### Enable database digest uploads

Update the database to start uploading ledger digests to the storage account, by using the [Enable-AzSqlDatabaseLedgerDigestUpload](/powershell/module/az.sql/enable-azsqldatabaseledgerdigestupload) cmdlet. The database server will create a new container, named **sqldbledgerdigests**, within the storage account and it will start writing ledger digests to the container.  
*Note: Make sure you modify the parameters ResourceGroupName, ServerName, DatabaseName and Endpoint*

```azurepowershell-interactive
Write-host "Enabling ledger digest upload..." 
$ledgerDigestUploadConfig = Enable-AzSqlDatabaseLedgerDigestUpload `
     -ResourceGroupName "ResourceGroup01" `
     -ServerName "Server01" `
     -DatabaseName "Database01" `
     -Endpoint "https://mystorage.blob.core.windows.net"
$ledgerDigestUploadConfig
```
# [Azure CLI](#tab/AzureCLI)
### Enable database digest uploads

Update the database to start uploading ledger digests to the storage account by using the [az sql db ledger-digest-uploads enable](/cli/azure/sql/db) command.  *Note: Make sure you modify the parameters name,resource-group, server and endpoint*

```azurecli-interactive
az sql db ledger-digest-uploads enable \
    --name Database01 \
    --resource-group ResourceGroup01 \
    --server Server01 \
    --endpoint https://mystorage.blob.core.windows.net
```
---

## Next steps

- [Azure SQL Database ledger overview](ledger-overview.md)
- [SQL Database ledger](ledger-database-ledger.md)
- [Digest management](ledger-digest-management.md)
- [Database verification](ledger-database-verification.md)
- [Append-only ledger tables](ledger-append-only-ledger-tables.md)
- [Updatable ledger tables](ledger-updatable-ledger-tables.md)
- [Access the digests stored in Azure Confidential Ledger](ledger-how-to-access-acl-digest.md)
