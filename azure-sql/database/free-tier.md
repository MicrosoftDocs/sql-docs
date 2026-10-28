---
title: Try Azure SQL Database for free
description: Guidance on how to deploy the Azure SQL Database free tier for up to 10 databases.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: ivujic, reneamoso, amapatil, alpowers
ms.date: 10/12/2026
ms.service: azure-sql-database
ms.subservice: service-overview
ms.topic: how-to
ai-usage: ai-assisted
monikerRange: "=azuresql || =azuresql-db"
---
# Deploy Azure SQL Database for free

> [!div class="op_single_selector"]
> * [Azure SQL Database](free-tier.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/free-offer.md?view=azuresql-mi&preserve-view=true)

Try Azure SQL Database at no cost with the free tier. For each database, you get 100,000 vCore seconds, 32 GB of data, and 32 GB of backup storage free per month for the lifetime of your subscription. Each Azure subscription allows you to create up to 10 free tier General Purpose databases.

> [!TIP]
> To get started and quickly create a new free Azure SQL Database, visit the [Azure SQL hub at aka.ms/azuresqlhub](https://aka.ms/azuresqlhub). In the **Create a database** pane, select the **Start free** link.
>
> To create the same database from an agent or the command line, use [az sql db create](/cli/azure/sql/db#az-sql-db-create) with the free-limit parameters. The `--use-free-limit` parameter makes the database a free tier database. Without it, you get a regular billable serverless database.
>
> ```agent
> Create an Azure SQL Database on the free tier.
> 1. Confirm the active Azure subscription and resource group. Verify that the logical
>    server exists, and create it with `az sql server create` if it doesn't.
> 2. Create the database in that logical server with `az sql db create`, using the General Purpose, serverless
>    configuration and the free-limit parameters:
>    `--edition GeneralPurpose --family Gen5 --capacity 2 --compute-model Serverless
>     --use-free-limit true --free-limit-exhaustion-behavior AutoPause`
> 3. Add a server firewall rule for the client IP address used to connect to the database.
> 4. Use `AutoPause` to pause the database when the monthly free limits are reached, or
>    `BillOverUsage` to stay online and pay for usage beyond the free limits.
> Format all commands for my current shell. Don't use Bash `\` line continuation in PowerShell.
> Confirm the parameters against the official reference at
> https://learn.microsoft.com/cli/azure/sql/db#az-sql-db-create before running the commands.
> ```

<a id="prerequisites"></a>

## Create a database

You need an Azure account and subscription. Unlike the [previous Azure free account offer](free-sql-db-free-account-how-to-deploy.md), the free tier is available with most Azure subscription types. Microsoft Azure for Students Starter isn't supported.

Create a free tier database with the Azure portal, Azure CLI, or Azure PowerShell.

# [Portal](#tab/azure-portal)

Use the Azure portal to create the new free Azure SQL Database. To create a free tier database, follow these steps:

1. Go to [Azure SQL hub at aka.ms/azuresqlhub](https://aka.ms/azuresqlhub).
1. In the pane **Create a database**, select **Start free**.
1. On the **Basics** tab:
    1. Under **Project details**:
        - Select your **Subscription**. Your current subscription is already selected.
        - An existing **Resource Group** is already selected. You can select a different resource group or create a new one.
    1. Under **Database details**:
        - Use the automatically created unique **Database name**, or provide a new name.
        - An existing logical **Server** is already selected. You can select a different logical server or create a new one.

   The **Cost summary** card on the right side of the page shows an **Estimated Cost/Month** of zero. The database is created with default settings, including the option to pause the database when the free limits are reached. You can change these database settings later.

1. Select **Review + create**.
1. Review and select **Create**.

# [Azure CLI](#tab/azure-cli)

Use an existing logical server in the active subscription, or create a new one. The resource group must already exist. For the complete server setup, see [Create a single database](single-database-create-quickstart.md?tabs=azure-cli).

If you need a new logical server, create it first:

```azurecli
az sql server create --name <server-name> --resource-group <resource-group> --location <region> --admin-user <admin-login> --admin-password <admin-password>
```

If you plan to use an existing server, verify the server name and resource group:

```azurecli
az sql server show --name <server-name> --resource-group <resource-group> --output table
```

Then create the database in that server with [az sql db create](/cli/azure/sql/db#az-sql-db-create) and `--use-free-limit true`. Without this parameter, you create a billable serverless database.

```azurecli
az sql db create --name <database-name> --server <server-name> --resource-group <resource-group> --edition GeneralPurpose --family Gen5 --capacity 2 --compute-model Serverless --use-free-limit true --free-limit-exhaustion-behavior AutoPause
```

The command is on one line so you can use it in PowerShell, Bash, or another supported shell without changing line-continuation characters.

To connect from your computer, add its public IP address to the logical server's firewall. Replace `<client-ip-address>` with your public IPv4 address:

```azurecli
az sql server firewall-rule create --name AllowClientIp --server <server-name> --resource-group <resource-group> --start-ip-address <client-ip-address> --end-ip-address <client-ip-address>
```

Use `AutoPause` to pause at the monthly free limit, or `BillOverUsage` to continue with charges. For more information, see [Considerations and limitations](#considerations-and-limitations).

# [PowerShell](#tab/azure-powershell)

Install the [Az.Sql PowerShell module](/powershell/azure/install-az-ps) and sign in with `Connect-AzAccount`. Use an existing logical server in the active subscription, or create a new one. The resource group must already exist.

If you need a new logical server, create it first. The command prompts you for the server administrator credentials:

```powershell
New-AzSqlServer -ResourceGroupName "<resource-group>" -ServerName "<server-name>" -Location "<region>" -ServerVersion "12.0" -SqlAdministratorCredentials (Get-Credential)
```

If you plan to use an existing server, verify the server name and resource group:

```powershell
Get-AzSqlServer -ResourceGroupName "<resource-group>" -ServerName "<server-name>"
```

Then create the database in that server. `-UseFreeLimit` is a switch in Azure PowerShell, so don't add a value after it:

```powershell
New-AzSqlDatabase -ResourceGroupName "<resource-group>" -ServerName "<server-name>" -DatabaseName "<database-name>" -Edition GeneralPurpose -VCore 2 -ComputeGeneration Gen5 -ComputeModel Serverless -UseFreeLimit -FreeLimitExhaustionBehavior AutoPause
```

To connect from your computer, add its public IP address to the logical server's firewall. Replace `<client-ip-address>` with your public IPv4 address:

```powershell
New-AzSqlServerFirewallRule -ResourceGroupName "<resource-group>" -ServerName "<server-name>" -FirewallRuleName "AllowClientIp" -StartIpAddress "<client-ip-address>" -EndIpAddress "<client-ip-address>"
```

Use `AutoPause` to pause at the monthly free limit, or `BillOverUsage` to continue with charges. For more information, see [Considerations and limitations](#considerations-and-limitations).

---

## Query the database

After you create the database, connect to it with the tool or language of your choice. For a guided first query, see [Query the database](single-database-create-quickstart.md?tabs=azure-portal#query-the-database).

- Use the [query editor in the Azure portal](query-editor.md) to run queries without installing a tool.
- Use [SQL Server Management Studio](/ssms/sql-server-management-studio-ssms), the [MSSQL extension for Visual Studio Code](/sql/tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code), or [`sqlcmd`](/sql/tools/sqlcmd/sqlcmd-utility) for interactive queries.
- To query the database from an application, choose a driver or provider from [Microsoft SQL drivers and frameworks](/sql/connect/sql-connection-libraries/). For example, use `Microsoft.Data.SqlClient` for .NET or `mssql-python` for Python. Then follow the [language quickstarts](connect-query-content-reference-guide.md#libraries) to connect and run your first query.

## Monitor and track service usage

### Track free usage

Use the Azure portal to track the free amount remaining or consumed:

1. On the database **Overview** tab, you see a **Free monthly vCore amount** entry:

    :::image type="content" source="media/free-offer/free-monthly-vcore-amount-remaining.png" alt-text="Screenshot from the Azure portal of free monthly vCore seconds amount remaining.":::

1. Select the **seconds remaining** amount. The **Metrics** chart launches where you can look at **Free amount remaining** or **Free amount consumed** metrics.

On the **Metrics** tab, you can also [create an alert rule](/azure/azure-monitor/alerts/tutorial-metric-alert) at no cost. Use the **Free amount remaining** metric to send an alert when the amount is less than 10,000 vCore seconds (10% of the monthly limit), so you know when you're running out for the month.

### Monitor with Database Hub (preview)

[Database Hub in Microsoft Fabric](/fabric/database/hub/overview) (preview) gives you an estate-wide view of Azure SQL databases that you have permission to access. It's available with a Fabric Free license and doesn't require Fabric capacity. Your databases stay in their existing subscriptions, resource groups, and regions; you don't move data into Fabric.

Use Database Hub to:

- Find this database and your other Azure SQL databases across subscriptions and resource groups.
- Review **What needs attention?** for issues and suggestions that require investigation.
- Inspect CPU, memory, storage, I/O, and workload pressure after you enable performance monitoring.
- Continue to the Azure portal or Visual Studio Code for configuration changes and query diagnostics.

Open the [Database Hub](https://powerbi.com/workloads/fdh/databaseHub), then follow [Monitor a Microsoft SQL database](/fabric/database/hub/monitor-sql) for the guided workflow. To add performance data for this database, follow [Enable performance monitoring for Microsoft SQL](/fabric/database/hub/add-sql).

> [!IMPORTANT]
> Database Hub and Microsoft SQL performance monitoring are in preview. Review the linked documentation for current prerequisites, supported regions, and setup steps.

> [!TIP]
> Use the [Database Hub agent skills](https://github.com/microsoft/microsoft-sql/tree/main/plugins/microsoft-sql-fdh) to explore your SQL estate:
>
> ```agent
> Review my Azure SQL database estate in Database Hub. Identify databases that need
> attention, summarize the issues and suggestions for my free tier database, and show
> me where to investigate its performance in the Azure portal or Visual Studio Code.
> ```

## Modernize your database with Hyperscale

Start with the free tier to develop and validate your application, then move your existing database to [Hyperscale](service-tier-hyperscale.md) as your workload grows. You don't need to create a replacement database or move application data manually.

The upgrade requires two separate updates:

1. Remove the free offer and wait for the update to complete.
1. Convert the database to Hyperscale.

Allowing paid overages doesn't remove the free offer. Removal is permanent for this database, and paid General Purpose usage can accrue even if the subsequent Hyperscale conversion fails.

For portal instructions, Azure CLI and PowerShell scripts, and an agent prompt, see [Upgrade a free tier database to Hyperscale](free-tier-upgrade-to-hyperscale.md).

<a id="tips-on-managing-vcore-seconds"></a>
<a id="monthly-free-limits"></a>
<a id="offer-limitations"></a>

## Considerations and limitations

The following considerations and limitations apply to Azure SQL Database free tier databases:

- The free tier uses the same fully managed General Purpose infrastructure as a paid database, but it doesn't include a service-level agreement (SLA). Use it primarily for development and proof-of-concept scenarios.
- Choose the behavior for when you reach the monthly free limit:
  - **Auto-pause the database until next month** stops charges, but the database is inaccessible until the allowance resets at the start of the next calendar month.
  - **Continue using database for additional charges** keeps the database online and bills overages at standard General Purpose serverless rates. You can't switch back to the auto-pause option.
- The free tier databases automatically pause after 15 minutes of inactivity. The auto-pause delay is fixed and can't be changed, regardless of the selected behavior when the monthly free limit is reached.
- Disconnect querying tools such as [SQL Server Management Studio](/ssms/sql-server-management-studio-ssms), including the Object Explorer, when you're done. An open connection prevents auto-pause and continues to consume vCore seconds. If the database doesn't auto-pause, review [features that prevent auto-pause](serverless-tier-overview.md#review-interoperability-that-prevents-auto-pause).
- You can apply the free tier only to a new database. You can't restore or convert an existing database to the free tier.
- If you use **Advanced configuration**, the region selected for the first free tier database applies to all free tier databases in that subscription and can't be changed.

For backup, scaling, copying, and feature compatibility details, see the [Azure SQL Database free tier FAQ](free-tier-faq.yml).

## Related content

- [Azure SQL Database free tier FAQ](free-tier-faq.yml)
- [Connect and query your database](connect-query-content-reference-guide.md)
- [Serverless compute tier for Azure SQL Database](serverless-tier-overview.md)
- [Microsoft SQL drivers and frameworks](/sql/connect/sql-connection-libraries/)
- [Database Hub in Microsoft Fabric (preview)](/fabric/database/hub/overview)
- [Try Azure SQL Database free with Azure free account (old offer)](free-sql-db-free-account-how-to-deploy.md)
