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

# Configure a ledger database

[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]

> [!NOTE]
> Azure SQL Database ledger is currently in public preview.

In this article, we will describe how you can configure a [ledger database](ledger-overview.md)  through the Azure portal, PowerShell, or the Azure CLI.

## Prerequisites

- Have an active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/free/).
- An Azure SQL Database

## Enable ledger database

# [Portal](#tab/Portal)

1. Open the [Azure portal](https://portal.azure.com/) and locate the database for which you want to enable automatic digest storage. Select that database in SQL Database.
1. In **Security**, select the **Ledger** option. :::image type="content" source="media/ledger/ledger-portal-manage-ledger.png" alt-text="Screenshot that shows the Azure portal with the Security Ledger tab selected."::: 
1. In the **Ledger** pane, select **Enable for all future tables in this database**. :::image type="content" source="media/ledger/enable-ledger-database.png" alt-text="Screenshot that shows the selection for enabling a ledger database.":::
1. Click **Apply** to save this setting. 

# [PowerShell](#tab/PowerShell)
### Enable ledger database

Create a single ledger database with the [New-AzSqlDatabase](/powershell/module/az.sql/New-AzSqlDatabase) cmdlet.
The below example creates a serverless database. The parameter -EnableLedger will create the ledger database.
*Note: Make sure you modify the parameters ServerName and DatabaseName*

```azurepowershell-interactive
Write-host "Creating a gen5 2 vCore serverless ledger database..."
$database = New-AzSqlDatabase  -ResourceGroupName $resourceGroupName `
    -ServerName "Server01" `
    -DatabaseName "Database01" `
    -Edition GeneralPurpose `
    -ComputeModel Serverless `
    -ComputeGeneration Gen5 `
    -VCore 2 `
    -MinimumCapacity 2 `
    -EnableLedger
$database
```
# [Azure CLI](#tab/AzureCLI)
### Enable ledger database

Create a ledger database with the [az sql db create](/cli/azure/sql/db) command. The following command creates a serverless database with ledger enabled. *Note: Make sure you modify the parameters resource-group, server and name*

```azurecli-interactive
az sql db create \
    --resource-group ResourceGroup01 \
    --server Server01 \
    --name Database01 \
    --edition GeneralPurpose \
    --family Gen5 \
    --capacity 2 \
    --compute-model Serverless \
    --ledger-on
```
---

## Next steps

- [Azure SQL Database ledger overview](ledger-overview.md)
- [SQL Database ledger](ledger-database-ledger.md)
- [Digest management](ledger-digest-management.md)
- [Database verification](ledger-database-verification.md)
- [Append-only ledger tables](ledger-append-only-ledger-tables.md)
- [Updatable ledger tables](ledger-updatable-ledger-tables.md)
