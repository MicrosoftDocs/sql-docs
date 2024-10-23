---
title: Create and configure a database watcher
titleSuffix: Azure SQL Database & SQL Managed Instance
description: Setup and configuration details for database watcher
author: dimitri-furman
ms.author: dfurman
ms.reviewer: wiassaf
ms.date: 10/23/2024
ms.service: azure-sql
ms.subservice: monitoring
ms.topic: how-to
ms.custom:
  - subject-monitoring
monikerRange: "=azuresql||=azuresql-db||=azuresql-mi"
---

# Create and configure a database watcher (preview)

[!INCLUDE [sqldb-sqlmi](./includes/appliesto-sqldb-sqlmi.md)]

This article contains detailed steps to create, configure, and start a database watcher in the Azure portal for Azure SQL Database and Azure SQL Managed Instance. 

Database watcher does not require you to deploy and maintain any monitoring agents or other monitoring infrastructure. You can enable in-depth database monitoring of your Azure SQL resources in minutes.

For a simplified step-by-step example to create and configure a database watcher, see [Quickstart: Create a database watcher to monitor Azure SQL](database-watcher-quickstart.md).

To see how you can create and configure a database watcher with [Bicep](/azure/azure-resource-manager/bicep/overview) or an ARM template, see the [Create a database watcher](/samples/azure/azure-quickstart-templates/create-watcher/) code sample.

To manage database watchers programmatically, see the database watcher [REST API](/rest/api/databasewatcher) documentation.

> [!NOTE]
> Database watcher is currently in preview.

## Prerequisites

To use database watcher, the following prerequisites are required.

- You'll need an active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/free/). You need to be a member of the **Contributor** role or the **Owner** role for the subscription or a resource group to be able to create resources.

- To configure and start a database watcher, you'll need an existing SQL target: an Azure SQL database, elastic pool, or SQL managed instance.
    - If you don't already have an Azure SQL database created, visit [Quickstart: Create a single database](database/single-database-create-quickstart.md). Look for the option to use your offer to [try Azure SQL Database for free (preview)](database/free-offer.md).
    - Alternatively, [Try Azure SQL Managed Instance for free (preview)](managed-instance/free-offer.md).

- The `Microsoft.DatabaseWatcher`, `Microsoft.Kusto`, and `Microsoft.Network` resource providers must be registered in your Azure subscription.
    - To use SQL authentication for connections to your Azure SQL resources, the `Microsoft.KeyVault` resource provider must be registered as well. See [Additional configuration to use SQL authentication](#additional-configuration-to-use-sql-authentication).

  Resource provider registration is automatic if you have the **Owner** or **Contributor** [RBAC](/azure/role-based-access-control/overview) role membership at the subscription level. Otherwise, a user in one of these roles must register resource providers before you can create and configure a watcher. For more information, see [Register resource provider](/azure/azure-resource-manager/management/resource-providers-and-types#register-resource-provider).

- The user who creates and configures the watcher and the Azure Data Explorer cluster resources must be a member of the **Owner** or **Contributor** RBAC role for the resource group or subscription where these resources are created.

  Additionally, if using SQL authentication, the user must either be a member of the **Owner** role for the resource group, or a member of the **Owner** or **User access administrator** role for the key vault that stores SQL authentication credentials.

- The user who configures the watcher must have administrator access to the Azure SQL targets. An administrator grants the watcher limited, specific access to SQL monitoring targets. For more information, see [Grant access to targets](#grant-access-to-sql-targets).

- To grant a watcher access to a SQL target, an administrator needs to execute T-SQL scripts using [SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms), [Azure Data Studio](/azure-data-studio/what-is-azure-data-studio), or Visual Studio Code with the [SQL server mssql](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql) extension.

- To use [Azure Private Link](/azure/private-link/private-link-overview) for private connectivity to Azure resources, the user who approves the private endpoint must be a member of the **Owner** RBAC role, or must have the required RBAC permissions. For more information, see [Approval RBAC for private endpoint](/azure/private-link/rbac-permissions#approval-rbac-for-private-endpoint).

## Create a watcher

1. In the Azure portal, in the navigation menu, select **All services**. Select **Monitor** as the category, and under **Monitoring tools**, select **Database watchers**. Alternatively, you can type *database watcher* in the **Search** box at the top of the portal page, and select **Database watchers**.

    Once the **Database watchers** view opens, select **Create**.

1. On the **Basics** tab, select the subscription and resource group for the watcher, enter the name of the watcher, and select an Azure region.

    > [!TIP]
    > During preview, if database watcher is not yet available in your region, you can create it in a different region. For more information, see [Regional availability](database-watcher-overview.md#regional-availability).

1. On the **Identity** tab, the system assigned managed identity of the watcher is enabled by default. If you want the watcher to use a user assigned managed identity instead, select the **Add** button, find the identity you want to use, and select the **Add** button. To make the user assigned identity effective for the watcher, disable the system assigned managed identity.

    For more information about managed identities in database watcher, see [Modify watcher identity](#modify-watcher-identity).

1. Choose a data store for the watcher.

    By default, creating a watcher also creates an [Azure Data Explorer](/azure/data-explorer/data-explorer-overview) cluster, and adds a database on that cluster as the data store for collected monitoring data.

    - By default, the new Azure Data Explorer cluster uses the **Extra small, Compute optimized** SKU. This is the most economical SKU that still provides a Service Level Agreement (SLA). You can [scale](#scale-azure-data-explorer-cluster) this cluster later as required.

    - Or, you can use a database on an existing Azure Data Explorer cluster, on a [free Azure Data Explorer cluster](/azure/data-explorer/start-for-free), or in [Real-Time Analytics](/fabric/real-time-analytics/overview).
        1. On the **Data store** tab, choose the **Select a data store** option, and select **Add**.
        1. Select a Real-Time Analytics database or an Azure Data Explorer cluster.
        1. If using an existing Azure Data Explorer cluster, you must enable [streaming ingestion](/azure/data-explorer/ingest-data-streaming).
        1. Create a new database or use an existing database.

        > [!NOTE]
        > Any existing database you select **must be empty**, or must be a database that you have previously used as a database watcher data store. Selecting a database that contains any objects not created by database watcher is not supported.

    - Or, you can skip adding a data store at this time and add it later. A data store is required to start the watcher.

1. On the **SQL targets** tab, [add one or more Azure SQL resources](#add-sql-targets-to-a-watcher) to monitor. You can skip adding SQL targets when creating the watcher and add them later. You need to add at least one target before starting the watcher.

1. On the **Review + create** tab, review watcher configuration, and select **Create**. If you select the default option to create a new Azure Data Explorer cluster, the deployment typically takes 15-20 minutes. If you select a database on an existing Azure Data Explorer cluster, on a free Azure Data Explorer cluster, or in Real-Time Analytics, the deployment typically takes up to five minutes.

1. Once the deployment completes, grant the watcher [access to SQL targets](#grant-access-to-sql-targets).
    - Access to a database on a new or existing Azure Data Explorer cluster is granted automatically when the watcher is created if the user creating the watcher is a member of the **Owner** RBAC role for the cluster.
    - However, you must [grant access to data store using a KQL command](#grant-access-to-data-store) if you select a database in:
        - Real-Time Analytics in Microsoft Fabric
        - A free Azure Data Explorer cluster

1. [Create managed private endpoints](#create-a-managed-private-endpoint) if you want to use [private connectivity](database-watcher-overview.md#private-connectivity).

## Start and stop a watcher

When a watcher is created, it is not started automatically because additional configuration might be required.

To start a watcher, it must have:

- A data store.
- At least one target.
- Access to the [data store](#grant-access-to-data-store) and [targets](#grant-access-to-sql-targets).
    - Access to a [key vault](#additional-configuration-to-use-sql-authentication) is also required if SQL authentication was selected for any target.
- Either [private](database-watcher-overview.md#private-connectivity) or [public](database-watcher-overview.md#public-connectivity) connectivity to targets, key vault (if using SQL authentication), and the data store.
    - To use private connectivity, [create private endpoints](#create-a-managed-private-endpoint).

Once a watcher is fully configured, use the **Start** button on the **Overview** page to start data collection. In a few minutes, new [monitoring data](database-watcher-data.md) appears in the data store and on [dashboards](database-watcher-overview.md#dashboards). If you don't see new data within five minutes, see [Troubleshooting](database-watcher-overview.md#troubleshoot).

You can stop the watcher with the **Stop** button if you do not need to monitor your Azure SQL resources for some time.

To restart a watcher, stop it and then start it again.

## Modify a watcher

In the Azure portal, you can add or remove targets, create or delete private endpoints, use a different data store for an existing watcher, or modify the managed identity of the watcher.

> [!NOTE]
> Unless noted differently, the changes you make to watcher configuration become effective after you stop and restart the watcher.

### Add SQL targets to a watcher

To enable database watcher monitoring for an Azure SQL database, elastic pool, or SQL managed instance, you need to add this resource as a SQL target.

1. To add a target, on the **SQL targets** page, select **Add**.
1. Find the Azure SQL resource you want to monitor. Select the resource type and subscription, and then select the SQL target from the list of resources. The SQL target can be in any subscription within the same Microsoft Entra ID tenant as the watcher.
1. To monitor the primary replica and a high availability [secondary replica](./database/read-scale-out.md) of a database, elastic pool, or SQL managed instance, add *two separate targets* for the same resource, and check the **Read intent** box for *one of them*. 
    - Checking the **Read intent** box configures the SQL target for the high availability secondary replica only.
    - Do not check the **Read intent** box if you want to monitor only the primary replica, or if a high availability secondary replica does not exist for this resource, or if the [read scale-out](./database/read-scale-out.md) feature is disabled.

By default, database watcher uses Microsoft Entra authentication when connecting to SQL targets. If you want the watcher to use SQL authentication, check the **Use SQL authentication** box and enter the required details. For more information, see [Additional configuration to use SQL authentication](#additional-configuration-to-use-sql-authentication).

### Remove SQL targets from a watcher

To remove one or more targets, open the **SQL targets** page, select the targets you want to remove in the list, and select **Delete**. 

Removing a target stops monitoring for an Azure SQL resource once the watcher is restarted, but does not delete the actual resource.

If you delete an Azure SQL resource monitored by database watcher, you should remove the corresponding target as well. Because there is a [limit on the number of SQL targets a watcher can have](database-watcher-overview.md#limits), keeping obsolete targets might block you from adding new targets.

### Create a managed private endpoint

You must create managed [private endpoints](/azure/private-link/private-endpoint-overview) if you want to use [private connectivity](database-watcher-overview.md#private-connectivity) for data collection from SQL targets, for ingestion into the data store, and for connecting to key vaults. If you do not create private endpoints, database watcher defaults to using [public connectivity](database-watcher-overview.md#public-connectivity).

> [!NOTE]
>
> Database watcher requires its own managed private endpoints to connect to Azure resources. Any private endpoint that might already exist for an Azure SQL logical server, a SQL managed instance, an Azure Data Explorer cluster, or a key vault cannot be used by a watcher.

To create a managed private endpoint for a watcher:

1. If there is a read-only [lock](/azure/azure-resource-manager/management/lock-resources) on the resource, resource group, or subscription of the resource for which you are creating a managed private endpoint, remove the lock. You can add the lock again after the private endpoint is created successfully.

1. Navigate to a database watcher in Azure portal, open the **Managed private endpoints** page, and select **Add**.

1. Enter a name for the private endpoint.

1. Select the subscription of the Azure resource for which you want to create the private endpoint.

1. Depending on the type of the resource for which you want to create a private endpoint, select the **Resource type** and **Target sub-resource** as follows:

    | Resource | Resource type | Target sub-resource |
    |:--|:--|:--|
    | Logical server | `Microsoft.Sql/servers` | `sqlServer` |
    | SQL managed instance | `Microsoft.Sql/managedInstances`| `managedInstance` |
    | Azure Data Explorer cluster | `Microsoft.Kusto/clusters` | `cluster` |
    | Key vault | `Microsoft.KeyVault/vaults` | `vault` |

1. Select the resource for which you want to create a private endpoint. This can be an Azure SQL logical server or SQL managed instance, an Azure Data Explorer cluster, or a key vault.

    - Creating a private endpoint for an Azure SQL Database logical server enables database watcher private connectivity for all database and elastic pool targets on that server.

1. Optionally, enter the description for the private endpoint. This can help the resource owner approve the request.

1. Select **Create**. It can take one or two minutes to create a private endpoint. A private endpoint is created once its provisioning state changes from **Accepted** or **Running** to **Succeeded**. Refresh the view to see the current provisioning state.

    > [!IMPORTANT]
    > The private endpoint is created in the **Pending** state. It must be approved by the resource owner before database watcher can use it to connect to the resource.
    >
    > To let resource owners control network connectivity, database watcher private endpoints are not approved automatically.

1. The resource owner must approve the private endpoint request.
    - In the Azure portal, the owner of the resource can search for **Private Link** to open the **Private Link Center**. Under **Pending connections**, find the private endpoint you created, confirm its description and details, and select **Approve**.
    - You can also [approve private endpoint requests using Azure CLI](/cli/azure/network/private-endpoint-connection#az-network-private-endpoint-connection-approve).

If a watcher is already running when a private endpoint is approved, it must be restarted to begin using private connectivity.

> [!TIP]
> 
> You need to create an additional private endpoint for your Azure Data Explorer cluster if public connectivity to the cluster is disabled. For more information, see [Private connectivity to the data store](#private-connectivity-to-the-data-store).

### Delete a managed private endpoint

1. If there is a delete or a read-only [lock](/azure/azure-resource-manager/management/lock-resources) on the resource, resource group, or subscription of the resource for which you are deleting a managed private endpoint, remove the lock. You can add the lock again after the private endpoint is deleted successfully.
1. In the Azure portal page for your database watcher, open the **Managed private endpoints** page.
1. Select the private endpoints you want to delete.
1. Select **Delete**.

Deleting a managed private endpoint stops data collection from SQL targets that use this private endpoint. Deleting the managed private endpoint for the Azure Data Explorer cluster stops data collection for all targets. To resume data collection, recreate the private endpoint or enable [public connectivity](database-watcher-overview.md#public-connectivity), and restart the watcher.

### Change the data store for a watcher

A watcher can have only one data store.

To change the current data store, remove the existing data store, then add a new data store.

- To remove the current data store, open the **Data store** page, select the data store in the grid, and select **Delete**.
    - Removing a data store does not delete the actual data store database on an Azure Data Explorer cluster or in Real-Time Analytics in Microsoft Fabric.
    - To stop data collection into a removed data store, stop the watcher.
    - If you remove a data store, you must add a new data store before you can start the watcher again.

- To add a data store, select **Add** on the **Data store** page, and then select or create a database on an Azure Data Explorer cluster, or in Real-Time Analytics.
    - The database you select must be empty, or must be a database that you have previously used as a database watcher data store. Selecting a database that contains any objects not created by database watcher is not supported.
    - Once you add a data store, you must grant the watcher access to use it. For more information, see [Grant access to data store](#grant-access-to-data-store).
    - Once the watcher is restarted, it starts using the new data store.

### Modify watcher identity

A watcher must have a managed identity to authenticate to SQL targets, key vaults, and the data store. Either a system assigned or a user assigned managed identity can be used. For more information about managed identities in Azure, see [What are managed identities for Azure resources?](/entra/identity/managed-identities-azure-resources/overview)

The following considerations help you choose the type of managed identity for a watcher:

- **System assigned**
    - Enabled by default when you create a watcher.
    - Always associated with a single watcher.
    - Created and deleted with the watcher.
    - If you disable a system assigned identity for a watcher, any access granted to that identity is lost. Re-enabling the system assigned identity for the same watcher creates a new identity with a different object (principal) ID. You need to grant access to [SQL targets](#grant-access-to-sql-targets), [key vault](#additional-configuration-to-use-sql-authentication), and the [data store](#grant-access-to-data-store) to this new identity.

- **User assigned**
    - Is in effect only if the system assigned identity is disabled for the watcher.
    - The same user assigned identity can be assigned to multiple watchers to simplify access management when [monitoring large Azure SQL estates](#monitor-large-estates). Instead of granting access to multiple system assigned identities, access can be granted to a single user assigned identity.
    - To support separation of duties, identity management can be separate from watcher management. A user assigned identity can be created and granted access by a different user, before or after the watcher is created.
    - Conversely, when a watcher is deleted, the user assigned identity and its access remain unchanged. The same identity can be then used for a new watcher.
    - Specifying more than one user assigned identity for a watcher is not supported.

To modify the managed identity for a watcher, open the **Identity** page of a watcher.

- To use a system assigned identity, enable the **System assigned identity** toggle.
- To use a user assigned identity, disable the **System assigned identity** toggle. Select the **Add** button to find and add an existing user assigned identity.

    To create a new user assigned identity, see [Create a user assigned managed identity](/entra/identity/managed-identities-azure-resources/how-manage-user-assigned-managed-identities#create-a-user-assigned-managed-identity).

- To remove a user assigned identity from a watcher, select it in the list and select **Remove**. Once a user assigned identity is removed, you need to either add a different user assigned identity, or enable the system assigned identity.

    The removed user assigned identity is not deleted from the Microsoft Entra ID tenant.

Select the **Save** button to save identity changes. You cannot save identity changes if that would result in the watcher having no identity. Watchers without a valid managed identity are not supported.

> [!TIP]
> We recommend that the display name of the watcher managed identity is unique within your Microsoft Entra ID tenant. You can choose a unique name when creating a user assigned identity for watchers.
>
> The display name of the system assigned identity is the same as the watcher name. If you use the system assigned identity, make sure that the watcher name is unique within your Microsoft Entra ID tenant.
>
> If the managed identity display name is not unique, the [T-SQL script](#grant-access-to-microsoft-entra-authenticated-watchers) to grant the watcher access to SQL targets fails with a duplicate display name error. For more information and for a workaround, see [Microsoft Entra logins and users with nonunique display names](./database/authentication-microsoft-entra-create-users-with-nonunique-names.md).

Shortly after identity changes are saved, the watcher reconnects to SQL targets, key vaults (if used), and the data store using its current managed identity.

### Delete a watcher

If there is a delete or a read-only [lock](/azure/azure-resource-manager/management/lock-resources) on the watcher, its resource group, or its subscription, remove the lock. You can add the lock again after the watcher is deleted successfully.

When you delete a watcher that has its system assigned managed identity enabled, the identity is also deleted. This removes any access you granted to this identity. If you recreate the watcher later, you need to grant access to the system assigned managed identity of the new watcher to authenticate to each resource. This includes:

- [Targets](#grant-access-to-sql-targets)
- The [data store](#grant-access-to-data-store)
- And the [key vault](#additional-configuration-to-use-sql-authentication) (if used)

You must grant access to a recreated watcher, even if you use the same watcher name.

When you delete a watcher, the Azure resources referenced as its targets and data store are not deleted. You retain collected SQL monitoring data in the data store, and you can use the same Azure Data Explorer or Real-Time Analytics database as the data store if you create a new watcher later.

## Grant access to SQL targets

To allow a watcher to collect SQL monitoring data, you need to execute a T-SQL script that grants the watcher specific, limited SQL permissions.

- To execute the script in Azure SQL Database, you need server administrator access to the logical server containing databases and elastic pools you want to monitor.
    - In Azure SQL Database, you only need to execute the script once per logical server for every watcher you create. This grants the watcher access to all existing and new databases and elastic pools on that server.

- To execute the script in Azure SQL Managed Instance, you need to be a member of either `sysadmin` or `securityadmin` server role, or have the `CONTROL` server permission on the SQL managed instance.
    - In Azure SQL Managed Instance, you need to execute the script on each instance you want to monitor.

1. Navigate to the watcher in Azure portal, select **SQL targets**, select one of the **Grant access** links to open the T-SQL script, and copy the script. Make sure to choose the correct link for your target type and the authentication type you want to use.

    > [!IMPORTANT]
    > The Microsoft Entra authentication script in Azure portal is specific to a watcher because it includes the name of the managed identity of the watcher. For a generic version of this script that you can customize for each watcher, see [Grant access to SQL targets with T-SQL scripts](#grant-access-to-sql-targets-with-t-sql-scripts).

1. In SQL Server Management Studio, Azure Data Studio, or any other SQL client tool, open a new query window and connect it to the `master` database on an Azure SQL logical server containing the target, or to the `master` database on a SQL managed instance target.

1. Paste and execute the T-SQL script to grant access to the watcher. The script creates a login that the watcher will use to connect, and grants specific, limited permissions to collect monitoring data.

    1. If you use a Microsoft Entra authentication script, and the watcher uses the system assigned managed identity, the watcher must be already created when you execute the script. If the watcher will use a user assigned managed identity, you can execute the script before or after the watcher is created.
    
    You must be connected with Microsoft Entra authentication when executing the T-SQL access scripts that grant access to a managed identity.

If you add new targets to a watcher later, you need to grant access to these targets in a similar fashion unless these targets are on a logical server where access has already been granted.

### Grant access to SQL targets with T-SQL scripts

There are different scripts for Microsoft Entra authentication and SQL authentication, and for Azure SQL Database and Azure SQL Managed Instance targets.

> [!IMPORTANT]
> Always use provided scripts to grant access to database watcher. Granting access in a different way can block data collection. For more information, see [Watcher authorization](database-watcher-overview.md#watcher-authorization).

Before executing a script, replace all instances of placeholders that might be present in the script, such as `login-name-placeholder` and `password-placeholder` with the actual values.

#### Grant access to Microsoft Entra authenticated watchers

# [SQL Database](#tab/sqldb)

This script creates a Microsoft Entra (formerly known as Azure Active Directory) authentication login on a logical server in Azure SQL Database. The login is created for the managed identity of a watcher. The script grants the watcher the necessary and sufficient permissions to collect monitoring data from all databases and elastic pools on the logical server.

If the watcher uses the system assigned managed identity, you must use the watcher name as the login name. If the watcher uses a user assigned managed identity, you must use the display name of the identity as the login name.

The script must be executed in the `master` database on the logical server. You must be logged in using a Microsoft Entra authentication login that is a server administrator.

```sql
CREATE LOGIN [identity-name-placeholder] FROM EXTERNAL PROVIDER;

ALTER SERVER ROLE ##MS_ServerPerformanceStateReader## ADD MEMBER [identity-name-placeholder];
ALTER SERVER ROLE ##MS_DefinitionReader## ADD MEMBER [identity-name-placeholder];
ALTER SERVER ROLE ##MS_DatabaseConnector## ADD MEMBER [identity-name-placeholder];
```

# [SQL Managed Instance](#tab/sqlmi)

This script creates a Microsoft Entra (formerly known as Azure Active Directory) authentication login on a SQL managed instance. The login is created for the managed identity of a watcher. The script grants the watcher the necessary and sufficient permissions to collect monitoring data from the instance.

If the watcher uses the system assigned managed identity, you must use the watcher name as the login name. If the watcher uses a user assigned managed identity, you must use the display name of the identity as the login name.

The script must be executed in the `master` database on the managed instance. You must be logged in using a Microsoft Entra authentication login that is a member of either `sysadmin` or `securityadmin` server role, or has the `CONTROL` server permission.

```sql
USE master;

CREATE LOGIN [identity-name-placeholder] FROM EXTERNAL PROVIDER;

GRANT CONNECT SQL, CONNECT ANY DATABASE, VIEW ANY DATABASE, VIEW ANY DEFINITION, VIEW SERVER PERFORMANCE STATE TO [identity-name-placeholder];

USE msdb;

CREATE USER [identity-name-placeholder] FOR LOGIN [identity-name-placeholder];

GRANT SELECT ON dbo.sysjobactivity TO [identity-name-placeholder];
GRANT SELECT ON dbo.sysjobs TO [identity-name-placeholder];
GRANT SELECT ON dbo.syssessions TO [identity-name-placeholder];
GRANT SELECT ON dbo.sysjobhistory TO [identity-name-placeholder];
GRANT SELECT ON dbo.sysjobsteps TO [identity-name-placeholder];
GRANT SELECT ON dbo.syscategories TO [identity-name-placeholder];
GRANT SELECT ON dbo.sysoperators TO [identity-name-placeholder];
GRANT SELECT ON dbo.suspect_pages TO [identity-name-placeholder];
GRANT SELECT ON dbo.backupset TO [identity-name-placeholder];
GRANT SELECT ON dbo.backupmediaset TO [identity-name-placeholder];
GRANT SELECT ON dbo.backupmediafamily TO [identity-name-placeholder];
```

---

#### Grant access to SQL authenticated watchers

Additional steps are required when using SQL authentication, see [Additional configuration to use SQL authentication](#additional-configuration-to-use-sql-authentication).

# [SQL Database](#tab/sqldb)

This script creates a SQL authentication login on a logical server in Azure SQL Database. It grants the login the necessary and sufficient permissions to collect monitoring data from all databases and elastic pools on that logical server.

The script must be executed in the `master` database on the logical server, using a login that is a logical server administrator.

```sql
CREATE LOGIN [login-name-placeholder] WITH PASSWORD = 'password-placeholder';

ALTER SERVER ROLE ##MS_ServerPerformanceStateReader## ADD MEMBER [login-name-placeholder];
ALTER SERVER ROLE ##MS_DefinitionReader## ADD MEMBER [login-name-placeholder];
ALTER SERVER ROLE ##MS_DatabaseConnector## ADD MEMBER [login-name-placeholder];
```

# [SQL Managed Instance](#tab/sqlmi)

This script creates a SQL authentication login on a SQL managed instance. It grants the login the necessary and sufficient permissions to collect monitoring data from the instance.

The script must be executed in the `master` database on the instance, using a login that is a member of either `sysadmin` or `securityadmin` server role, or has the `CONTROL` server permission.

```sql
USE master;

CREATE LOGIN [login-name-placeholder] WITH PASSWORD = 'password-placeholder';

GRANT CONNECT SQL, CONNECT ANY DATABASE, VIEW ANY DATABASE, VIEW ANY DEFINITION, VIEW SERVER PERFORMANCE STATE TO [login-name-placeholder];

USE msdb;

CREATE USER [login-name-placeholder] FOR LOGIN [login-name-placeholder];

GRANT SELECT ON dbo.sysjobactivity TO [login-name-placeholder];
GRANT SELECT ON dbo.sysjobs TO [login-name-placeholder];
GRANT SELECT ON dbo.syssessions TO [login-name-placeholder];
GRANT SELECT ON dbo.sysjobhistory TO [login-name-placeholder];
GRANT SELECT ON dbo.sysjobsteps TO [login-name-placeholder];
GRANT SELECT ON dbo.syscategories TO [login-name-placeholder];
GRANT SELECT ON dbo.sysoperators TO [login-name-placeholder];
GRANT SELECT ON dbo.suspect_pages TO [login-name-placeholder];
GRANT SELECT ON dbo.backupset TO [login-name-placeholder];
GRANT SELECT ON dbo.backupmediaset TO [login-name-placeholder];
GRANT SELECT ON dbo.backupmediafamily TO [login-name-placeholder];
```

---

#### Additional configuration to use SQL authentication

To store authentication credentials securely, using SQL authentication in database watcher requires additional configuration.

> [!TIP]
> For a more secure, simpler, and less error-prone configuration, we recommend [enabling Microsoft Entra authentication for your Azure SQL resources](database/authentication-aad-configure.md) and using it instead of SQL authentication.

To configure database watcher to connect to a target using SQL authentication, follow these steps:

1. [Create a vault](/azure/key-vault/general/quick-create-portal) in Azure Key Vault, or identify an existing vault you can use. The vault must use the [RBAC permission model](/azure/key-vault/general/rbac-guide). The **RBAC** permission model is the default for new vaults. If you want to use an existing vault, make sure that it is not [configured](/azure/key-vault/general/rbac-access-policy) to use the older **access policy** model.

    If you want to use private connectivity to the vault, create a private endpoint on the **Managed private endpoints** page. Select `Microsoft.KeyVault/vaults` as **Resource type**, and `vault` as **Target sub-resource**. Ensure that the private endpoint is approved before starting the watcher.

    If you want to use public connectivity, the vault must allow public access from all networks. Restricting public vault connectivity to specific networks is not supported in database watcher.

1. Create a SQL authentication login on each Azure SQL logical server or managed instance you want to monitor, and grant the specific, limited permissions using provided [access scripts](#grant-access-to-sql-targets). In the script, replace the login name and password placeholders with the actual values. Use a strong password.
1. In the vault, create *two secrets*: a [secret](/azure/key-vault/secrets/about-secrets) for the login name, and a *separate* secret for the password. Use any valid names as the **secret name**, and enter the login name and password you used in the T-SQL script as the **secret value** for each secret.

    For example, the names for the two secrets might be `database-watcher-login-name` and `database-watcher-password`. The secret values would be a login name and a strong password.

    To create secrets, you need to be a member of the **Key Vault Secrets Officer** RBAC role.

1. [Add a SQL target](#add-sql-targets-to-a-watcher) to a watcher. When adding the target, check the **Use SQL authentication** box, and select the vault where the login name and password secrets are stored. Enter the secret names for login name and password in the corresponding fields.

    When adding a SQL target, *do not* enter the actual login name and password. Using the earlier example, you would enter the `database-watcher-login-name` and `database-watcher-password` secret names.

1. When you add a SQL target in the Azure portal, the managed identity of the watcher is granted the required access to the key vault secrets automatically if the current user is a member of the **Owners** role or the **User access administrator** role for the key vault. Otherwise, follow the next step to grant the required access manually.

1. From the **Access control (IAM)** page of each secret, add a role assignment for the managed identity of the watcher in the **Key Vault Secrets User** RBAC role. To follow the principle of least privilege, add this role assignment for each secret, rather than for the entire vault. The **Access control (IAM)** page appears only if the vault is configured to use the **RBAC** permission model.

If you want to use different SQL authentication credentials on different SQL targets, you need to create multiple pairs of secrets. You can use the same vault or different vaults to store the secrets for each SQL target.

> [!NOTE]
> If you update the secret value for a login name or a password in the key vault while a watcher is running, the watcher reconnects to targets using the new SQL authentication credentials within 15 minutes. If you want to start using the new credentials right away, stop and restart the watcher.

## Grant access to data store

To create and manage database schema in the data store, and to ingest monitoring data, database watcher requires membership in the **Admins** RBAC role in the data store database on an Azure Data Explorer cluster or in Real-Time Analytics. Database watcher does not require any cluster-level access to the Azure Data Explorer cluster, or any access to other databases that might exist on the same cluster.

If you use a database on an Azure Data Explorer cluster as the data store, this access is granted automatically if you are a member of the **Owner** RBAC role for the cluster. Otherwise, access must be granted as described in this section.

If you use a database in Real-Time Analytics or on a free Azure Data Explorer cluster, you need to [grant access using KQL](#grant-access-to-an-azure-data-explorer-database-using-kql).

### Grant access to an Azure Data Explorer database using the Azure portal

You can use the Azure portal to grant access to a database on the Azure Data Explorer cluster:

1. For a *database* on an Azure Data Explorer cluster, in the resource menu under **Security + networking**, select **Permissions**. Do not use the **Permissions** page of the cluster.
1. Select **Add**, and select **Admin**.
1. On the **New Principals** page, select **Enterprise applications**. If the watcher uses the system assigned managed identity, type the name of the watcher in the **Search** box. If the watcher uses a user assigned managed identity, type the display name of that identity in the **Search** box.
1. Select the enterprise application for the managed identity of the watcher.

### Grant access to an Azure Data Explorer database using KQL

Instead of using Azure portal, you can also grant access to the database using a KQL command. Use this method to grant access to a database in Real-Time Analytics or on a free Azure Data Explorer cluster.

1. Connect to a database on the Azure Data Explorer cluster using [Kusto Explorer](/azure/data-explorer/kusto/tools/kusto-explorer) or the Azure Data Explorer [web UI](https://dataexplorer.azure.com/).

1. In the following sample KQL command, replace the three placeholders as noted in the table:

    ```kusto
    .add database [adx-database-name-placeholder] admins ('aadapp=identity-principal-id-placeholder;tenant-primary-domain-placeholder');
    ```

    | Placeholder | Replacement |
    |:--|:--|
    | `adx-database-name-placeholder` | The name of a database on an Azure Data Explorer cluster or in Real-Time Analytics. |
    | `identity-principal-id-placeholder` | The **principal ID** value of a managed identity (a GUID), found on the **Identity** page of the watcher. If the system assigned identity is enabled, use its **principal ID** value. Otherwise, use the **principal ID** value of the user assigned identity.|
    | `tenant-primary-domain-placeholder` | The domain name of the Microsoft Entra ID tenant of the watcher managed identity. Find this on the Microsoft Entra ID **Overview** page in the Azure portal. Instead of tenant primary domain, the **Tenant ID** GUID value can be used as well.</br></br>This part of the command is required if you use a database in Real-Time Analytics or on a free Azure Data Explorer cluster.</br></br>The domain name or tenant ID value (and the preceding semicolon) can be omitted for a database on an Azure Data Explorer cluster because the cluster is always in the same Microsoft Entra ID tenant as the watcher managed identity. |

    For example:

    ```kusto
    .add database [watcher_data_store] admins ('aadapp=9da7bf9d-3098-46b4-bd9d-3b772c274931;contoso.com');
    ```

For more information, see [Kusto role-based access control](/azure/data-explorer/kusto/access-control/role-based-access-control).

### Grant users and groups access to the data store

You can use Azure portal or a KQL command to grant users and groups access to a database on an Azure Data Explorer cluster or in Real-Time Analytics. To grant access, you must be a member of the **Admin** RBAC role in the database.

Use a KQL command to grant access to a database on the free Azure Data Explorer cluster or in Real-Time Analytics. To follow the principle of least privilege, we recommend that you do not add users and groups to any RBAC role other than **Viewer** by default.

> [!IMPORTANT]
> Carefully consider your data privacy and security requirements when granting access to view SQL monitoring data collected by database watcher.
>
> Even though database watcher does not have the ability to collect any data stored in user tables in your SQL databases, certain [datasets](database-watcher-data.md#datasets) such as **Active sessions**, **Index metadata**, **Missing indexes**, **Query runtime statistics**, **Query wait statistics**, **Session statistics**, and **Table metadata** might contain potentially sensitive data, such as table and index names, query text, query parameter values, login names, etc.
>
> By granting view access to the data store to a user who does not have access to view this data in a SQL database, you might enable them to see sensitive data that they wouldn't be able to see otherwise.

### Grant access to the data store using the Azure portal

You can use the Azure portal to grant users and groups access to a database on the Azure Data Explorer cluster:

1. For a *database* in an Azure Data Explorer cluster, in the resource menu under **Security + networking**, select **Permissions**. Do not use the **Permissions** page of the cluster.
1. Select **Add**, and select **Viewers**.
1. On the **New Principals** page, type the name of the user or group in the **Search** box.
1. Select the user or group.

### Grant access to the data store using KQL

Instead of using Azure portal, you can also grant users and groups access to the database using a KQL command. The following example KQL commands grant data read access to the *mary@contoso.com* user, and to the *SQLMonitoringUsers@contoso.com* group in a Microsoft Entra ID tenant with a specific tenant ID value:

```kusto
.add database [watcher_data_store] viewers ('aaduser=mary@contoso.com');

.add database [watcher_data_store] viewers ('aadgroup=SQLMonitoringUsers@contoso.com;8537e70e-7fb8-43d3-aac5-8b30fb3dcc4c');
```

For more information, see [Kusto role-based access control](/azure/data-explorer/kusto/access-control/role-based-access-control).

To grant access to the data store to users and groups from another tenant, you need to enable cross-tenant authentication on your Azure Data Explorer cluster. For more information, see [Allow cross-tenant queries and commands](/azure/data-explorer/kusto/access-control/cross-tenant-query-and-commands).

> [!TIP]
> To let you grant access to users and groups in your Microsoft Entra ID tenant, cross-tenant authentication is enabled in Real-Time Analytics and on free Azure Data Explorer clusters.

## Manage data store

This section describes how you can manage the monitoring data store, including scaling, data retention, and other configuration. The cluster scaling considerations in this section are relevant if you use a database on the Azure Data Explorer cluster. If you use a database [in Real-Time Analytics in Fabric, scaling is managed automatically](/fabric/real-time-analytics/overview#what-makes-real-time-analytics-unique).

### Scale Azure Data Explorer cluster

You can scale your Azure Data Explorer cluster as needed. For example, you can scale down your cluster to the **Extra small, Dev/test** SKU if a service level agreement (SLA) is not required, and if query and data ingestion performance remain acceptable.

For many database watcher deployments, the default **Extra small, Compute optimized** 2-instance cluster SKU will be sufficient indefinitely. In some cases, depending on your configuration and workload changes over time, you might need to scale your cluster to ensure adequate query performance and maintain low data ingestion latency.

Azure Data Explorer supports [vertical](/azure/data-explorer/manage-cluster-vertical-scaling) and [horizontal](/azure/data-explorer/manage-cluster-horizontal-scaling) cluster scaling. With vertical scaling, you change the cluster SKU, which changes the number of vCPUs, memory, and cache per instance (node). With horizontal scaling, the SKU remains the same, but the number of instances in the cluster is increased or decreased.

You need to scale your cluster out (horizontally) or up (vertically) if you notice one or more of the following symptoms:

- Dashboard or ad hoc query performance becomes too slow.
- You run many concurrent queries on your cluster and observe throttling errors.
- Data ingestion latency becomes consistently higher than acceptable.

In general, you do not need to scale your cluster as the amount of data in the data store increases over time. This is because dashboard queries and the most common analytical queries only use the latest data, which is cached in local SSD storage on cluster nodes.

However, if you run analytical queries spanning longer time ranges, they might become slower over time as the total amount of collected data increases and no longer fits into the local SSD storage. Scaling the cluster might be needed to maintain adequate query performance in that case.

- If you need to scale your cluster, we recommend that you scale it **horizontally** first to increase the number of instances. This keeps the cluster available for queries and ingestion during the scaling process.
    - You can enable [optimized autoscale](/azure/data-explorer/manage-cluster-horizontal-scaling#optimized-autoscale-recommended-option) to automatically reduce or increase the number of instances in response to changes in workload or to seasonal trends.

- You might find that even after you scale the cluster out horizontally, some queries still do not perform as expected. This might happen if query performance is bound by the resources available on an instance (node) of the cluster. In that case, scale up the cluster **vertically**.
    - Vertical cluster scaling takes several minutes. During that process, there is a period of downtime, which can stop data collection by the watcher. If that happens, [stop and restart](#start-and-stop-a-watcher) your watcher after the scaling operation is complete.

#### Free Azure Data Explorer cluster

The free Azure Data Explorer cluster has certain [capacity limits](/azure/data-explorer/start-for-free#specifications), including a storage capacity limit on the original uncompressed data. You cannot scale a free Azure Data Explorer cluster to increase its compute or storage capacity. When the cluster is close to reaching its storage capacity, or is at capacity, a warning message appears on the [free cluster page](https://dataexplorer.azure.com/freecluster).

If you reach storage capacity, new monitoring data isn't ingested, but existing data remains accessible on database watcher [dashboards](database-watcher-overview.md#dashboards) and can be [analyzed](database-watcher-analyze.md) using KQL or SQL queries.

If you find that the specifications of the free cluster are insufficient for your requirements, you can [upgrade to a full Azure Data Explorer cluster](/azure/data-explorer/start-for-free-upgrade) and retain all collected data. Because there might be a period of downtime during the upgrade, you might need to stop and restart your watcher to resume data collection once the upgrade is complete.

To continue using the free Azure Data Explorer cluster, [manage data retention](#manage-data-retention) to delete the older data automatically and free up space for new data. Once storage space is available, you might need to [stop and restart](#start-and-stop-a-watcher) your watcher to resume data collection.

### Manage data retention

If you do not require older data, you can configure data retention policies to purge it automatically. By default, [data retention](/azure/data-explorer/kusto/management/retentionpolicy) is set to 365 days in a new database on an Azure Data Explorer cluster or in Real-Time Analytics.

- You can reduce data retention period at the database level, or for individual [tables](/azure/data-explorer/table-retention-policy-wizard) in the database.
- You can also increase retention if you need to store monitoring data for more than one year. There is no upper limit on the data retention period.
- If you configure different data retention periods for different tables, [dashboards](database-watcher-overview.md#dashboards) might not work as expected for the older time ranges. This can happen if data is still present in some tables, but is already purged in other tables for the same time interval.

The amount of SQL monitoring data that is ingested in the data store depends on your SQL workloads and the size of your Azure SQL estate. You can use the following KQL query to view the average amount of data ingested per day, estimate storage consumption over time, and manage data retention policies.

```kusto
.show database extents
| summarize OriginalSize = sum(OriginalSize),
            CompressedSize = sum(CompressedSize)
            by bin(MinCreatedOn, 1d)
| summarize DailyAverageOriginal = format_bytes(avg(OriginalSize)),
            DailyAverageCompressed = format_bytes(avg(CompressedSize));
```

### Schema and access changes in the database watcher data store

Over time, Microsoft might introduce new database watcher [datasets](database-watcher-data.md#datasets), or expand existing datasets. This means that new tables in the data store, or new columns in existing tables might be added automatically.

To do this, the current managed identity of a database watcher must be a member of the **Admins** RBAC role in the data store. Revoking this role membership, or replacing it with membership in any other RBAC role can impact database watcher data collection and schema management, and is not supported.

Similarly, creating any new objects such as tables, external tables, materialized views, functions, etc. in the database watcher data store is not supported. You can use [Cross-cluster and cross-database queries](/azure/data-explorer/kusto/query/cross-cluster-or-database-queries) to query data in your data store from other Azure Data Explorer clusters, or from other databases on the same cluster.

> [!IMPORTANT]
> If you change database watcher access to its data store, or make any database schema or configuration changes that impact data ingestion, you might need to [change the data store](#change-the-data-store-for-a-watcher) for that watcher to a new empty database, and grant the watcher [access](#grant-access-to-data-store) to this new database to resume data collection and revert to a supported configuration.

### Stopped Azure Data Explorer clusters

An Azure Data Explorer cluster can be stopped, for example to save costs. By default, an Azure Data Explorer cluster created in the Azure portal is [stopped automatically](/azure/data-explorer/auto-stop-clusters) after several days of inactivity. For example, this can happen if you stop the watcher that ingests data into the only database on your cluster, and do not run any queries in this database.

If you use the default option to create a new Azure Data Explorer cluster when creating a new watcher, the automatic stop behavior is disabled to allow uninterrupted data collection.

If the cluster is stopped, database watcher data collection stops as well. To resume data collection, you need to start the cluster. Once the cluster is running, restart the watcher.

You can [disable the automatic stop behavior](/azure/data-explorer/auto-stop-clusters#manage-automatic-stop-behavior-on-your-cluster) if you want the cluster to remain available even when it's inactive. This might increase cluster cost.

### Streaming ingestion

Database watcher requires that the Azure Data Explorer cluster containing the data store database has [streaming ingestion](/azure/data-explorer/ingest-data-streaming) enabled. Streaming ingestion is automatically enabled for the new Azure Data Explorer cluster created when you create a new watcher. It is also enabled in Real-Time Analytics and on the free Azure Data Explorer cluster.

If you want to use an existing Azure Data Explorer cluster, make sure to enable streaming ingestion first. This takes a few minutes and restarts the cluster.

### Private connectivity to the data store

If [public access](/azure/data-explorer/security-network-restrict-public-access) on an Azure Data Explorer cluster is disabled, you need to create a private endpoint to connect to the cluster from your browser and see the SQL monitoring data on dashboards, or to query the data directly. This private endpoint is *in addition to* the [managed private endpoint](#create-a-managed-private-endpoint) created to let the watcher ingest monitoring data into a database on the Azure Data Explorer cluster.

- If you are connecting to an Azure Data Explorer cluster from an Azure VM, [create](/azure/data-explorer/security-network-private-endpoint-create) a private endpoint for the Azure Data Explorer cluster in the Azure virtual network where your Azure VM is deployed.

- If you are connecting to an Azure Data Explorer cluster from a machine on premises, you can:

    1. Use [Azure VPN Gateway](/azure/vpn-gateway/vpn-gateway-about-vpngateways) or [Azure ExpressRoute](/azure/expressroute/expressroute-introduction) to establish a private connection from your on-premises network to an Azure virtual network.
    1. [Create](/azure/data-explorer/security-network-private-endpoint-create) a private endpoint for the Azure Data Explorer cluster in the Azure virtual network where the VPN or ExpressRoute connection terminates, or in another Azure virtual network reachable by traffic from your machine on-premises.
    1. [Configure DNS](/azure/private-link/private-endpoint-dns) for that private endpoint.

Private connectivity is not available for free Azure Data Explorer clusters, or for Real-Time Analytics in Microsoft Fabric.

## Monitor large estates

To monitor a large Azure SQL estate, you might need to create multiple watchers.

Each watcher requires a database on an Azure Data Explorer cluster or in Real-Time Analytics as the data store. The watchers you create can use a single database as a common data store, or separate databases as separate data stores. The following considerations can help you make an optimal design choice for your monitoring scenarios and requirements.

Considerations for a **common data store**:

- There is a single-pane-of-glass view of your entire Azure SQL estate.
- The dashboards of any watcher show all data in the data store, even if the data is collected by other watchers.
- Users with access to the data store have access to the monitoring data for your entire Azure SQL estate.

Considerations for **separate data stores**:

- Subsets of your Azure SQL estate are monitored independently. Database watcher dashboards in the Azure portal always show the data from a single data store.
- Users with access to multiple data stores can use [cross-cluster or cross-database](/azure/data-explorer/kusto/query/cross-cluster-or-database-queries) KQL queries to access monitoring data in multiple data stores using a single query.
- Because data access in Azure Data Explorer and in Real-Time Analytics is managed per database, you can manage access to the monitoring data for the subsets of your estate in a granular way.
- You can place multiple databases on the same Azure Data Explorer cluster to share cluster resources and save costs, while still keeping data isolated in each database.
- If you require a complete separation of environments, including network access to Azure Data Explorer clusters, you can place different databases on different clusters.

## Related content

- [Quickstart: Create a database watcher to monitor Azure SQL (preview)](database-watcher-quickstart.md)
- [Monitor Azure SQL workloads with database watcher (preview)](database-watcher-overview.md)
- [Database watcher data collection and datasets (preview)](database-watcher-data.md)
- [Analyze database watcher monitoring data (preview)](database-watcher-analyze.md)
- [Database watcher FAQ](database-watcher-faq.yml)
