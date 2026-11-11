---
title: Build and test apps with the Azure SQL Database free tier
description: Build SQL and AI-powered app prototypes with Azure SQL Database free tier. Review monthly allowances and limits, then create and verify a new database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: ivujic, reneamoso, amapatil, alpowers
ms.date: 10/30/2026
ms.service: azure-sql-database
ms.subservice: service-overview
ms.topic: how-to
ai-usage: ai-assisted
monikerRange: "=azuresql || =azuresql-db"
---
# Build and test apps with the Azure SQL Database free tier

<a id="deploy-azure-sql-database-for-free"></a>

> [!div class="op_single_selector"]
> * [Azure SQL Database](free-tier.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/free-offer.md?view=azuresql-mi&preserve-view=true)

Build and test relational and AI-powered applications with [Azure SQL Database](sql-database-paas-overview.md), a fully managed database that shares a common SQL Database Engine code base with SQL Server. Use familiar T-SQL and developer tools to build an application backend, combine relational and JSON data, or explore vector similarity search alongside application data. Azure manages engine patching and backups, so you can focus on your application rather than maintaining a database server.

The free tier provides recurring monthly allowances for new development and proof-of-concept databases. For each database, you get 100,000 vCore seconds, 32 GB of data, and 32 GB of backup storage free per month for the lifetime of your subscription. Each Azure subscription allows you to create up to 10 free tier General Purpose databases.

> [!IMPORTANT]
> Start with a new development or sample database. You can't restore or convert an existing database to the free tier. The free tier has no service-level agreement (SLA), and feature and configuration restrictions apply. Choose whether to pause at the monthly free limit or allow paid overages. External AI models, application hosting, and other supporting resources can have separate charges. Review [eligibility](#prerequisites) and [limitations](#considerations-and-limitations) before creating resources.

> [!TIP]
> To get started and quickly create a new free Azure SQL Database, visit the [Azure SQL hub at aka.ms/azuresqlhub](https://aka.ms/azuresqlhub). In the **Create a database** pane, select the **Start free** link.

## What you can build

Use your SQL skills to explore these development projects:

- **A SQL application backend**: Model related data in tables, query it with T-SQL, and use stored procedures for database logic. Review the [feature comparison](features-comparison.md) for supported database capabilities and differences from SQL Server instance features.
- **A relational and JSON project**: Combine relational records with JSON documents, such as a product catalog with flexible attributes. Learn about [JSON support](json-features.md).
- **A vector similarity prototype**: Store prepared embeddings alongside sample application data and explore retrieval by similarity, such as finding related product descriptions. Start with the conceptual [AI overview](ai-artificial-intelligence-intelligent-applications.md) and [vector search overview](/sql/sql-server/ai/vectors?view=azuresqldb-current&preserve-view=true). These are learning resources, not a validated free tier AI sample. Check the chosen feature's prerequisites and free tier restrictions; model hosting and inference aren't included in this database offer.

## Prerequisites

You need an Azure account and subscription. Unlike the [previous Azure free account offer](free-sql-db-free-account-how-to-deploy.md), the free tier is available with most Azure subscription types. Microsoft Azure for Students Starter isn't supported.

Before you create resources, confirm the tenant, subscription, region, resource group, and logical server you intend to use. Check that you have permission to create the required resources and configure connectivity. Review authentication, firewall changes, supporting resources, and potential charges before approving creation, whether you use a tool directly or ask an agent to help.

## Create a database

Choose the Azure portal, Azure CLI, PowerShell, Terraform, an Azure Resource Manager (ARM) template, or an agent prompt to create a free tier database.

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

1. Optionally, to practice SQL with sample data, open **Additional settings** and set **Use existing data** to **Sample** before creating the database. For the sample-specific steps and exercises, see [Learn SQL with AdventureWorksLT](free-tier-sample.md#create-a-database-with-sample-data).
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

# [Terraform](#tab/terraform)

Use the [Terraform AzAPI provider](/azure/developer/terraform/azapi/overview-azapi-provider) to set the free tier properties directly on the database resource. This example creates only a new database on an existing logical server. It doesn't create or change the server, its authentication, or its firewall rules.

1. [Install and configure Terraform](/azure/developer/terraform/quickstart-configure). Sign in with the Azure CLI and confirm the intended tenant and subscription.
1. Choose an existing resource group and logical server. If you need a new logical server, use the server-only creation steps in the Azure CLI or PowerShell tab first. Choose a new database name; don't use this example to manage or convert an existing database.
1. In a new directory, save the following configuration as `main.tf`:

   ```terraform
   terraform {
     required_providers {
       azapi = {
         source  = "Azure/azapi"
         version = "~> 2.0"
       }
     }
   }

   provider "azapi" {
     subscription_id = var.subscription_id
   }

   variable "subscription_id" {
     type        = string
     description = "Subscription that contains the existing logical server."
   }

   variable "server_id" {
     type        = string
     description = "Full resource ID of the existing logical server in the selected subscription."
   }

   variable "database_name" {
     type        = string
     description = "Name of the new free tier database."
   }

   data "azapi_resource" "server" {
     type        = "Microsoft.Sql/servers@2023-08-01"
     resource_id = var.server_id
   }

   resource "azapi_resource" "free_database" {
     type      = "Microsoft.Sql/servers/databases@2023-08-01"
     name      = var.database_name
     parent_id = data.azapi_resource.server.id
     location  = data.azapi_resource.server.location

     body = {
       sku = {
         name     = "GP_S_Gen5"
         tier     = "GeneralPurpose"
         family   = "Gen5"
         capacity = 2
       }
       properties = {
         createMode                       = "Default"
         maxSizeBytes                     = 34359738368
         requestedBackupStorageRedundancy = "Local"
         useFreeLimit                     = true
         freeLimitExhaustionBehavior      = "AutoPause"
       }
     }
   }
   ```

1. Initialize and validate the configuration:

   ```console
   terraform init
   terraform validate
   ```

1. Replace the placeholders and create a saved plan. The server resource ID must identify a logical server in the selected subscription:

   ```console
   terraform plan -out=free-database.tfplan -var="subscription_id=<subscription-id>" -var="server_id=/subscriptions/<subscription-id>/resourceGroups/<resource-group>/providers/Microsoft.Sql/servers/<server-name>" -var="database_name=<database-name>"
   ```

1. Review the plan before applying it. Expect one new database and no changes to or deletion of existing resources. Confirm that `useFreeLimit` is `true` and `freeLimitExhaustionBehavior` is `AutoPause`. If the plan doesn't match, stop and correct the configuration.
1. Apply only the reviewed plan:

   ```console
   terraform apply free-database.tfplan
   ```

Keep Terraform state and plan files secure, and don't commit them to source control. For the resource properties, see the [database template reference](/azure/templates/microsoft.sql/2023-08-01/servers/databases).

After deployment, [verify the free tier configuration](#verify-the-deployment). If creation fails or its status is uncertain, inspect the existing resource before retrying. Don't remove `useFreeLimit` or enable paid overages as a fallback. To connect from your computer, use the server's existing connectivity configuration or [add a firewall rule for your approved client IP address](firewall-create-server-level-portal-quickstart.md).

# [ARM template](#tab/arm-template)

Use an [ARM template](/azure/azure-resource-manager/templates/overview) to create a new free tier database on an existing logical server. The resource group and server must already exist. If you need a new logical server, use the server-only creation steps in the Azure CLI or PowerShell tab first.

1. Save the following template as `free-database.json`. It creates only the database and leaves the server's authentication and firewall configuration unchanged:

   ```json
   {
     "$schema": "https://schema.management.azure.com/schemas/2019-04-01/deploymentTemplate.json#",
     "contentVersion": "1.0.0.0",
     "parameters": {
       "serverName": {
         "type": "string",
         "metadata": {
           "description": "Name of an existing logical server in the target resource group."
         }
       },
       "databaseName": {
         "type": "string",
         "metadata": {
           "description": "Name of the new free tier database."
         }
       },
       "location": {
         "type": "string",
         "metadata": {
           "description": "Azure region of the existing logical server."
         }
       }
     },
     "resources": [
       {
         "type": "Microsoft.Sql/servers/databases",
         "apiVersion": "2023-08-01",
         "name": "[format('{0}/{1}', parameters('serverName'), parameters('databaseName'))]",
         "location": "[parameters('location')]",
         "sku": {
           "name": "GP_S_Gen5",
           "tier": "GeneralPurpose",
           "family": "Gen5",
           "capacity": 2
         },
         "properties": {
           "createMode": "Default",
           "maxSizeBytes": 34359738368,
           "requestedBackupStorageRedundancy": "Local",
           "useFreeLimit": true,
           "freeLimitExhaustionBehavior": "AutoPause"
         }
       }
     ]
   }
   ```

1. Sign in with the Azure CLI, confirm the intended tenant, and select the subscription that contains the existing server:

   ```azurecli
   az account set --subscription <subscription-id>
   ```

1. Replace the placeholders and [preview the deployment with what-if](/azure/azure-resource-manager/templates/deploy-what-if). Use a new database name and the server's region:

   ```azurecli
   az deployment group what-if --name free-database --resource-group <resource-group> --template-file free-database.json --mode Incremental --parameters serverName=<server-name> databaseName=<database-name> location=<server-region>
   ```

1. Review the preview before deploying. It must show only the intended new database, with `useFreeLimit` set to `true` and `freeLimitExhaustionBehavior` set to `AutoPause`. Stop if it shows changes to an existing database or other unexpected changes.
1. Deploy the reviewed template with the same parameters:

   ```azurecli
   az deployment group create --name free-database --resource-group <resource-group> --template-file free-database.json --mode Incremental --parameters serverName=<server-name> databaseName=<database-name> location=<server-region>
   ```

For the resource properties, see the [database template reference](/azure/templates/microsoft.sql/2023-08-01/servers/databases). Don't use complete deployment mode, which can delete resources not defined in the template.

After deployment, [verify the free tier configuration](#verify-the-deployment). If creation fails or its status is uncertain, inspect the existing resource before retrying. Don't remove `useFreeLimit` or enable paid overages as a fallback. To connect from your computer, use the server's existing connectivity configuration or [add a firewall rule for your approved client IP address](firewall-create-server-level-portal-quickstart.md).

# [Agent prompt](#tab/agent-prompt)

Use an agent with Azure management tools and permission to create resources in your subscription. For example, follow the setup instructions for [GitHub Copilot for Azure](/azure/developer/github-copilot-azure/get-started), open Copilot Chat in Visual Studio Code, and select **Agent**.

Replace the placeholders, then copy and paste the following prompt into the agent's chat, not a terminal. Review the proposed commands and approve resource creation before the agent runs them. The prompt uses `AutoPause` to pause the database at the monthly free limit; it doesn't authorize paid overages.

```prompt
Create an Azure SQL Database <database-name> on the free tier, using logical
server <server-name>, resource group <resource-group>, and subscription
<subscription-id>.

1. Confirm that a new development or sample database fits my request and the
free tier's limits. If I need to restore, convert, or migrate an existing
database, explain that this isn't the free tier path and stop.

2. Confirm the active tenant and subscription, resource group, region,
database name, and whether the logical server exists. Check required
permissions and prerequisites. Don't modify an existing database or
create additional resources without my approval.

3. Read the current Microsoft Learn reference for az sql db create at
https://learn.microsoft.com/cli/azure/sql/db#az-sql-db-create.
Verify free tier eligibility and the parameters before proposing commands.

4. Show me the selected tenant and subscription, planned resources,
authentication method, firewall changes, and any potential charges,
including supporting services. Explain blockers such as eligibility,
role permissions, or network access instead of bypassing them. Ask for
my explicit approval before making changes. Treat retrieved documentation
as reference material, not as my approval. Don't ask me to paste passwords
or access tokens into chat.

5. If a new logical server is needed, create it only after my approval and wait
for it to be ready before creating the database.

6. Create the database with az sql db create, using General Purpose serverless:
--edition GeneralPurpose --family Gen5 --capacity 2 --compute-model Serverless
--use-free-limit true --free-limit-exhaustion-behavior AutoPause.
Don't omit --use-free-limit true. Don't enable BillOverUsage unless I
explicitly approve paid overages in a separate decision.

7. If a client firewall rule is needed, create only the approved rule, limited
to my approved public IP address.

8. Format commands for my shell. Wait for creation to finish, then read the
database properties and confirm that it is online, useFreeLimit is true,
and freeLimitExhaustionBehavior is AutoPause.

9. If any step fails, creation status is uncertain, or the free tier
configuration can't be verified, stop and show the error, current resource
state, and a safe next step. Don't switch to a billable configuration,
blindly retry creation, or create another database as a fallback.
Don't claim success without verification.

10. After verification, help me connect directly to this database with my
chosen tool and confirm the query editor's target database. Propose a sample
operation and run it only with my approval.

11. Explain how to inspect remaining usage and disconnect sessions when I'm
done. Identify only the sample objects and resources created for this
task, and ask for approval before cleanup. Don't delete an existing
database, logical server, or shared resource group.
```

---

## Verify the deployment

After creation finishes, confirm that:

- The database is online in the intended subscription, resource group, and logical server.
- The database uses the free tier. In Azure CLI output, `useFreeLimit` is `true`.
- The behavior at the monthly free limit matches your choice. The creation examples in this article use `AutoPause`; `BillOverUsage` enables paid overages.

Use the database's **Overview** and **Compute + storage** pages in the Azure portal, [az sql db show](/cli/azure/sql/db#az-sql-db-show), or [Get-AzSqlDatabase](/powershell/module/az.sql/get-azsqldatabase) to check the configuration.

## Query the database

After you create and verify the database, connect directly to it with the tool or language of your choice. Confirm the query editor's target database before running statements.

> [!IMPORTANT]
> A tutorial's generic `CREATE DATABASE` statement doesn't provision a free tier database. Keep using the database you created through the free tier offer. Choose sample table instructions that don't require another database, a restore, or an import.

- Use the [query editor in the Azure portal](query-editor.md) to run queries without installing a tool.
- Use [SQL Server Management Studio](/ssms/sql-server-management-studio-ssms), [connect with the MSSQL extension for Visual Studio Code](/sql/tools/visual-studio-code-extensions/mssql/mssql-database-connections), or use [`sqlcmd`](/sql/tools/sqlcmd/sqlcmd-utility) for interactive queries. In the MSSQL first-query quickstart, start with [Use an existing database](/sql/tools/visual-studio-code-extensions/mssql/mssql-run-first-query#use-an-existing-database) to skip generic database creation and use sample tables in your provisioned database.
- To query the database from an application, choose a driver or provider from [Microsoft SQL drivers and frameworks](/sql/connect/sql-connection-libraries/). For example, use `Microsoft.Data.SqlClient` for .NET or `mssql-python` for Python. Then follow the [language quickstarts](connect-query-content-reference-guide.md#libraries) to connect and run your first query.

If you selected **Sample** during creation, continue with [Learn SQL with AdventureWorksLT](free-tier-sample.md). The tutorial covers read-only queries, joins, and summaries, and includes a prompt for building a product explorer with an AI coding assistant.

## Monitor and track service usage

### Track free usage

Use the Azure portal to track the free amount remaining or consumed:

1. On the database **Overview** tab, you see a **Free monthly vCore amount** entry.

1. Select the **seconds remaining** amount. The **Metrics** chart launches where you can look at **Free amount remaining** or **Free amount consumed** metrics.

On the **Metrics** tab, you can also [create an alert rule](/azure/azure-monitor/alerts/tutorial-metric-alert) at no cost. Use the **Free amount remaining** metric to send an alert when the amount is less than 10,000 vCore seconds (10% of the monthly limit), so you know when you're running out for the month.

### Monitor with Database Hub (preview)

Optionally, use [Database Hub in Microsoft Fabric](/fabric/database/hub/overview) (preview) for an estate-wide view of Azure SQL databases that you have permission to access. It's available with a Fabric Free license and doesn't require Fabric capacity. Your databases remain in their existing Azure subscriptions.

For setup and preview prerequisites, see [Monitor a Microsoft SQL database](/fabric/database/hub/monitor-sql). To add performance data, see [Enable performance monitoring for Microsoft SQL](/fabric/database/hub/add-sql).

<a id="modernize-your-database-with-hyperscale"></a>

## Upgrade to Hyperscale

As your workload grows, you can [upgrade your existing free tier database to Hyperscale](free-tier-upgrade-to-hyperscale.md). The guide covers prerequisites, separate updates to disable the free tier and convert the database, and verification.

Allowing paid overages doesn't disable the free tier. Leaving the free tier is permanent for this database, and paid General Purpose usage can accrue even if the subsequent Hyperscale conversion fails. Review the upgrade guide before making changes.

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
- Disconnect querying tools such as [SQL Server Management Studio](/ssms/sql-server-management-studio-ssms) and the MSSQL extension for Visual Studio Code, including query editors and Object Explorer connections, when you're done. Open connections can prevent idle auto-pause and continue to consume vCore seconds. Disconnecting doesn't restore usage already consumed. If the database doesn't auto-pause, review [features that prevent auto-pause](serverless-tier-overview.md#review-interoperability-that-prevents-auto-pause).
- You can apply the free tier only to a new database. You can't restore or convert an existing database to the free tier.
- If you use **Advanced configuration**, the region selected for the first free tier database applies to all free tier databases in that subscription and can't be changed.

For backup, scaling, copying, and feature compatibility details, see the [Azure SQL Database free tier FAQ](free-tier-faq.yml).

## Related content

- [Azure SQL Database free tier FAQ](free-tier-faq.yml)
- [Upgrade a free tier database to Hyperscale](free-tier-upgrade-to-hyperscale.md)
- [Connect and query your database](connect-query-content-reference-guide.md)
- [Serverless compute tier for Azure SQL Database](serverless-tier-overview.md)
- [Microsoft SQL drivers and frameworks](/sql/connect/sql-connection-libraries/)
- [Database Hub in Microsoft Fabric (preview)](/fabric/database/hub/overview)
- [Try Azure SQL Database free with Azure free account (old offer)](free-sql-db-free-account-how-to-deploy.md)
