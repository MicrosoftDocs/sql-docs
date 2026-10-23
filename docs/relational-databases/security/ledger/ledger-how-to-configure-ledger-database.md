---
title: Configure a Ledger Database
description: This article discusses how to configure a ledger database in Azure SQL Database and SQL Server 2022
author: VanMSFT
ms.author: vanto
ms.reviewer: vanto
ms.date: 01/26/2026
ms.service: sql
ms.subservice: security
ms.topic: how-to
ms.custom:
  - devx-track-azurecli
  - ignite-2023
zone_pivot_groups: as1-azuresql-sql
monikerRange: "=azuresqldb-current || >=sql-server-ver16 || >=sql-server-linux-ver16 || =azuresqldb-mi-current"
---

# Configure a ledger database

[!INCLUDE [SQL Server 2022 Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

::: zone pivot="as1-azure-sql-database"

This article provides information on configuring a [ledger database](ledger-overview.md) using the Azure portal, T-SQL, PowerShell, or the Azure CLI for **Azure SQL Database**. For information on creating a ledger database in [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] or Azure SQL Managed Instance, use the switch at the top of this page.

## Prerequisites

- An active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn).
- A logical server.

## Enable ledger database

> [!NOTE]  
> When you enable the ledger functionality at the database level, you convert all tables in this database into updatable ledger tables. You can't change this option after the database is created. If you try to create a table with the option `LEDGER = OFF`, you get an error message.

# [Portal](#tab/Portal)

1. Open the [Azure portal](https://portal.azure.com/) and [create an Azure SQL Database](/azure/azure-sql/database/single-database-create-quickstart?tabs=azure-portal).

1. On the **Security** tab, select **Configure ledger**.

   :::image type="content" source="media/ledger/ledger-portal-manage-ledger.png" alt-text="Screenshot that shows the Azure portal with the Security Ledger tab selected." lightbox="media/ledger/ledger-portal-manage-ledger.png":::

1. On the **Configure ledger** pane, select **Enable for all future tables in this database**.

   :::image type="content" source="media/ledger/enable-ledger-database.png" alt-text="Screenshot that shows the selection for enabling a ledger database.":::

1. Select **Apply** to save this setting.

# [T-SQL](#tab/t-sql)

## Enable ledger database using T-SQL

Open a query editor, like [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms) or [SQL Server Data Tools (SSDT)](../../../ssdt/download-sql-server-data-tools-ssdt.md) in Visual Studio, and connect to your logical SQL Server. The following example creates a General Purpose database. The `WITH LEDGER=ON` clause creates the ledger database.

```sql
CREATE DATABASE Database01 (
    EDITION = 'GeneralPurpose',
    SERVICE_OBJECTIVE = 'GP_Gen5_2',
    MAXSIZE = 2 GB
)
WITH LEDGER = ON;
```

# [PowerShell](#tab/PowerShell)

## Enable ledger database using PowerShell

Create a single ledger database with the [New-AzSqlDatabase](/powershell/module/az.sql/New-AzSqlDatabase) cmdlet.

The following example creates a serverless database. The parameter `-EnableLedger` creates the ledger database. Modify the `ServerName` and `DatabaseName` parameters.

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

## Enable ledger database using the Azure CLI

Create a ledger database with the [az sql db create](/cli/azure/sql/db) command. The following command creates a serverless database with ledger enabled. Modify the `resource-group`, `server`, and `name` parameters.

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

::: zone-end

::: zone pivot="as1-azure-sql-managed-instance"

This article provides information on configuring a [ledger database](ledger-overview.md) using T-SQL, PowerShell, or the Azure CLI for **Azure SQL Managed Instance**. For information on creating a ledger database in [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] or Azure SQL Database, use the switch at the top of this page.

## Prerequisites

- An active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn).
- An Azure SQL Managed Instance.

## Enable ledger database

> [!NOTE]  
> When you enable the ledger functionality at the database level, you convert all tables in this database into updatable ledger tables. You can't change this option after the database is created. If you try to create a table with the option `LEDGER = OFF`, you get an error message.

# [T-SQL](#tab/t-sql2)

## Enable ledger database using T-SQL

1. Sign in to your managed instance using SQL Server Management Studio (SSMS), Visual Studio Code, or SQL Server Data Tools (SSDT).

1. Create a ledger database using the following T-SQL statement:

   ```sql
   CREATE DATABASE MyLedgerDB
   WITH LEDGER = ON;
   ```

For more information, see [CREATE DATABASE](../../../t-sql/statements/create-database-transact-sql.md).

# [PowerShell](#tab/PowerShell2)

## Enable ledger database using PowerShell

Create a single ledger database with the [New-AzSqlInstanceDatabase](/powershell/module/az.sql/New-AzSqlInstanceDatabase) cmdlet.

The following example creates a ledger database on a specified instance. The `-EnableLedger` parameter creates the ledger database. Modify the `ResourceGroupName`, `InstanceName`, and `Name` parameters.

```azurepowershell-interactive
Write-host "Creating a ledger database..."
$database = New-AzSqlInstanceDatabase -ResourceGroupName "ResourceGroup01" `
    -InstanceName  "ManagedInstance1" `
    -Name "Database01" `
    -EnableLedger
$database
```

# [Azure CLI](#tab/AzureCLI2)

## Enable ledger database using the Azure CLI

Create a ledger database with the [az sql midb create](/cli/azure/sql/midb) command. The following example creates a ledger database on a specified instance. Modify the `resource-group`, `managed-instance`, and `name` parameters.

```azurecli-interactive
az sql midb create \
    --resource-group ResourceGroup01 \
    --managed-instance Server01 \
    --name Database01 \
    --ledger-on
```

---

::: zone-end

::: zone pivot="as1-sql-server"

This article provides information on creating a [ledger database](ledger-overview.md) by using T-SQL in **[!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)]**. For information on creating a ledger database in Azure SQL Database or Azure SQL Managed Instance, use the switch at the top of this page.

## Prerequisites

- [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] or a later version
- [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms) or [SQL Server Data Tools](../../../ssdt/download-sql-server-data-tools-ssdt.md)

## Create a ledger database using T-SQL

1. Sign in to your [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] instance by using SSMS or SSDT.
1. Create a ledger database using the following T-SQL statement:

   ```sql
   CREATE DATABASE MyLedgerDB
       WITH LEDGER = ON;
   ```

For more information, see [CREATE DATABASE](../../../t-sql/statements/create-database-transact-sql.md).

::: zone-end

## Related content

- [Ledger overview](ledger-overview.md)
- [Append-only ledger tables](ledger-append-only-ledger-tables.md)
- [Updatable ledger tables](ledger-updatable-ledger-tables.md)
- [Enable automatic digest storage](ledger-how-to-enable-automatic-digest-storage.md)
