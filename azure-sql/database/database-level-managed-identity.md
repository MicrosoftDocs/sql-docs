---
title: Use Database-Level Managed Identity with Azure SQL Database
titleSuffix: Azure SQL Database
description: Configure a database-level user-assigned managed identity so each Azure SQL database authenticates to storage with its own identity, including cross-tenant access.
author: VanMSFT
ms.author: vanto
ms.reviewer: prdasgu, aadhityai
ms.date: 10/22/2026
ai-usage: ai-assisted
ms.service: azure-sql
ms.subservice: security
ms.topic: how-to
monikerRange: "=azuresql || =azuresql-db"
---

# Use database-level managed identity with Azure SQL Database (preview)

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

Database-level managed identity gives an individual database in Azure SQL Database its own user-assigned managed identity (UAMI). A database that has one authenticates to Azure Storage as itself, rather than as the logical server that hosts it.

Without a database-level identity, every database on a logical server shares that server's managed identity, so they all reach the same storage. A database-level identity narrows that access to a single database, so you can grant each database only the storage it needs.

You select the identity in a database-scoped credential, the same credential that [data virtualization](data-virtualization-overview.md) queries use to read Parquet, CSV, and Delta files from Azure Blob Storage or Azure Data Lake Storage (ADLS) Gen2. Choose between two credential identity values:

| Credential identity | Use it when |
| --- | --- |
| `DATABASE MANAGED IDENTITY` | The storage account is in the same Microsoft Entra tenant as the database. |
| `DATABASE FEDERATED CLIENT IDENTITY` | The storage account is in a different tenant, reached through a federated identity credential (FIC). |

> [!NOTE]
> As a preview feature, the technology presented in this article is subject to [Supplemental Terms of Use for Microsoft Azure Previews](https://azure.microsoft.com/support/legal/preview-supplemental-terms/).

## Prerequisites

- An Azure SQL Database on a logical server. All service tiers and all Azure regions support this feature.
- A user-assigned managed identity in the same subscription. For background, see [Managed identities in Microsoft Entra for Azure SQL](authentication-azure-ad-user-assigned-managed-identity.md).
- Permission to assign roles on the target storage account, such as **Owner** or **User Access Administrator**.
- An Azure Blob Storage or ADLS Gen2 account holding the files you want to query.

> [!IMPORTANT]
> A database can have only one user-assigned managed identity assigned at the database level, unlike a logical server, which can have several. Plan the identity per database before you assign it.
>
> If the database uses [customer-managed keys at the database level](transparent-data-encryption-byok-database-level-overview.md), it already has a database-level identity, and for cross-tenant keys, a federated client ID. This feature uses the same two settings. Grant storage access to the existing identity or application instead of assigning new ones. If you assign a different identity or application, first give it access to the encryption key. Otherwise, the update can fail, or the database can lose access to its key.

## Choose same-tenant or cross-tenant

Both walkthroughs create a database-scoped credential and query external data. They differ in how the identity reaches the storage account.

- If the database and storage account are in the same tenant, follow [Use a database-level identity in the same tenant](#use-a-database-level-identity-in-the-same-tenant).
- If the storage account is in a different tenant, follow [Use a federated client identity across tenants](#use-a-federated-client-identity-across-tenants).

## Use a database-level identity in the same tenant

### Step 1: Create a user-assigned managed identity

Create the identity and note both its **Resource ID** and its **Object (principal) ID**. You need the Resource ID to assign the identity to the database. You need the Object (principal) ID only for the cross-tenant walkthrough, where it identifies the subject of the federated identity credential.

```azurecli
az identity create \
    --name <identity-name> \
    --resource-group <resource-group> \
    --location <region>
```

For other methods, see [Manage user-assigned managed identities](/entra/identity/managed-identities-azure-resources/how-manage-user-assigned-managed-identities).

### Step 2: Assign the identity to the database

Assign the UAMI to the **database**, not to the logical server. This assignment uses the same mechanism as database-level transparent data encryption (TDE) with customer-managed keys, described in [Database-level customer-managed keys](transparent-data-encryption-byok-database-level-basic-actions.md). For the full set of assignment methods, see [Get or set a managed identity for a database](authentication-azure-ad-user-assigned-managed-identity.md#get-or-set-a-managed-identity-for-a-database).

To assign the identity in the Azure portal:

1. Go to your SQL database.
1. Under **Security**, select **Identity**.
1. Under **User-Assigned Managed Identity**, select **Add**.
1. Select the identity you created, and then select **Add**.
1. Select **Save**.

To assign the identity with the Azure CLI, use [az sql db update](/cli/azure/sql/db):

```azurecli
az sql db update \
    --resource-group <resource-group> \
    --server <server-name> \
    --name <database-name> \
    --assign-identity \
    --user-assigned-identity-id <identity-resource-id>
```

To assign it with PowerShell, use [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase):

```powershell
Set-AzSqlDatabase -ResourceGroupName <ResourceGroupName> -ServerName <ServerName> -DatabaseName <DatabaseName> -AssignIdentity -UserAssignedIdentityId <UserAssignedIdentityId>
```

The identity **Resource ID** looks like `/subscriptions/<subscription-id>/resourceGroups/<resource-group>/providers/Microsoft.ManagedIdentity/userAssignedIdentities/<identity-name>`. Find it under **Properties** on the managed identity in the Azure portal.

### Step 3: Grant the identity access to storage

Give the managed identity read access to the storage account:

1. Go to the storage account.
1. Select **Access Control (IAM)**.
1. Select **Add** > **Add role assignment**.
1. Select the **Storage Blob Data Reader** role, and then select **Next**.
1. For **Assign access to**, select **Managed identity**.
1. Select **Select members**, choose your user-assigned managed identity, and then select **Select**.
1. Select **Review + assign**.

For finer-grained access on ADLS Gen2, set access control lists (ACLs) on specific directories instead of assigning a role at the account level. For more information, see [Access control lists in Azure Data Lake Storage](/azure/storage/blobs/data-lake-storage-access-control).

### Step 4: Create the database-scoped credential

Connect to your **user database**, not `master`, and create the credential:

```sql
CREATE DATABASE SCOPED CREDENTIAL MyDBIdentityCred
WITH IDENTITY = 'DATABASE MANAGED IDENTITY';
```

This credential needs no secret and no database master key. The engine resolves the identity assigned to the database at query time.

The server-level equivalent uses a different identity value. Compare:

```sql
-- Server-level identity: every database on the logical server shares this access
CREATE DATABASE SCOPED CREDENTIAL MyServerIdentityCred
WITH IDENTITY = 'MANAGED IDENTITY';

-- Database-level identity: only this database has this access
CREATE DATABASE SCOPED CREDENTIAL MyDBIdentityCred
WITH IDENTITY = 'DATABASE MANAGED IDENTITY';
```

For the full syntax, see [CREATE DATABASE SCOPED CREDENTIAL](/sql/t-sql/statements/create-database-scoped-credential-transact-sql).

### Step 5: Create the external data source

Create an external data source that points to your container, and attach the credential. Use the prefix that matches your storage type:

| Storage type | Location prefix |
| --- | --- |
| Azure Blob Storage | `abs://` |
| ADLS Gen2 | `adls://` |

```sql
CREATE EXTERNAL DATA SOURCE MyExternalStorage
WITH (
    LOCATION = 'abs://<container>@<storage-account>.blob.core.windows.net',
    CREDENTIAL = MyDBIdentityCred
);
```

### Step 6: Query the data

Query the files ad hoc with `OPENROWSET`:

```sql
SELECT TOP 100 *
FROM OPENROWSET(
    BULK 'sales/2026/*.parquet',
    DATA_SOURCE = 'MyExternalStorage',
    FORMAT = 'parquet'
) AS externalData;
```

Or define an external table for repeated queries:

```sql
CREATE EXTERNAL FILE FORMAT ParquetFormat
WITH (FORMAT_TYPE = PARQUET);

CREATE EXTERNAL TABLE dbo.Sales
(
    OrderId int,
    OrderDate date,
    Amount decimal(10, 2)
)
WITH (
    LOCATION = 'sales/2026/',
    DATA_SOURCE = MyExternalStorage,
    FILE_FORMAT = ParquetFormat
);

SELECT TOP 100 * FROM dbo.Sales;
```

## Use a federated client identity across tenants

Use this path when your database is in one tenant and the storage account is in another. A multitenant application registration carries the trust between them: the database's UAMI is the subject of a federated identity credential on that application, and the storage tenant grants the application's service principal access to the data.

The pattern matches [cross-tenant customer-managed keys for transparent data encryption](transparent-data-encryption-byok-cross-tenant.md), so the setup looks familiar if you already use that feature.

### Step 1: Register a multitenant application

In the tenant that holds the database, register a multitenant application in Microsoft Entra ID and note its **Application (client) ID**.

For more information, see [Register an application with the Microsoft identity platform](/entra/identity-platform/quickstart-register-app).

### Step 2: Add a federated identity credential

Add a federated identity credential to that application so it trusts the database's user-assigned managed identity. The application and the managed identity must belong to the same Microsoft Entra tenant.

1. In the app registration, select **Certificates & secrets** > **Federated credentials** > **Add credential**.
1. For **Federated credential scenario**, select **Managed Identity**.
1. Select the managed identity you created in [Step 1](#step-1-create-a-user-assigned-managed-identity) of the same-tenant walkthrough.
1. Verify that the automatically populated **Issuer** identifies your tenant. For the Azure public cloud, the issuer is `https://login.microsoftonline.com/<tenant-id>/v2.0`.
1. Make sure **Audience** is `api://AzureADTokenExchange`, the value for the Azure public cloud.
1. Enter a unique name for the credential, and then save it.

The credential's subject is the managed identity's **Object (principal) ID**, not its client ID. If you configure the credential manually, copy the Object (principal) ID exactly, because the subject is case-sensitive. Microsoft Entra ID accepts incorrect issuer, subject, or audience values when you save the credential, but the token exchange fails later.

For more information, see [Configure an application to trust a managed identity](/entra/workload-id/workload-identity-federation-config-app-trust-managed-identity).

### Step 3: Consent to the application and grant storage access

In the tenant that holds the storage account:

1. An administrator consents to the multitenant application, which creates a service principal in that tenant.
1. Assign the **Storage Blob Data Reader** role to that service principal on the storage account. Follow [Step 3: Grant the identity access to storage](#step-3-grant-the-identity-access-to-storage), but for **Assign access to**, select **User, group, or service principal**, and then select the application's service principal.

### Step 4: Set the federated client ID on the database

In the tenant that holds the database, assign the user-assigned managed identity to the database, and set the Application (client) ID from Step 1 as the **Federated Client ID**:

1. Go to your SQL database.
1. Under **Security**, select **Identity**.
1. Under **User-Assigned Managed Identity**, check that the identity from [Step 1](#step-1-create-a-user-assigned-managed-identity) of the same-tenant walkthrough is listed. If it isn't, select **Add**, select the identity, and then select **Add**.
1. Under **Federated client identity**, select the application, and then select **Save**.

To set it with the Azure CLI:

```azurecli
az sql db update \
    --resource-group <resource-group> \
    --server <server-name> \
    --name <database-name> \
    --assign-identity \
    --user-assigned-identity-id <identity-resource-id> \
    --federated-client-id <application-client-id>
```

To set it with PowerShell:

```powershell
Set-AzSqlDatabase -ResourceGroupName <ResourceGroupName> -ServerName <ServerName> -DatabaseName <DatabaseName> -AssignIdentity -UserAssignedIdentityId <UserAssignedIdentityId> -FederatedClientId <ApplicationClientId>
```

### Step 5: Create the credential and query

```sql
CREATE DATABASE SCOPED CREDENTIAL MyCrossTenantCred
WITH IDENTITY = 'DATABASE FEDERATED CLIENT IDENTITY';

CREATE EXTERNAL DATA SOURCE PartnerStorage
WITH (
    LOCATION = 'adls://<container>@<storage-account>.dfs.core.windows.net',
    CREDENTIAL = MyCrossTenantCred
);
```

Then create an external table or run `OPENROWSET` against `PartnerStorage` exactly as in [Step 6: Query the data](#step-6-query-the-data).

## Compare server-level and database-level identities

| Characteristic | Server-level managed identity | Database-level managed identity |
| --- | --- | --- |
| Identity scope | Logical server | Single database |
| Credential syntax | `IDENTITY = 'MANAGED IDENTITY'` | `IDENTITY = 'DATABASE MANAGED IDENTITY'` |
| Cross-tenant storage access | Not supported. Use a shared access signature instead | Federated client ID set on the database, used with `IDENTITY = 'DATABASE FEDERATED CLIENT IDENTITY'` |
| Storage RBAC granularity | One identity for every database on the server | One identity per database |
| Scope of exposure | Every database on the server shares the access | Limited to the one database |
| Identity types | System-assigned or user-assigned | User-assigned only |
| Database master key required | No | No |

Both types of credentials can coexist on the same logical server and within its databases. Choose the credential that best fits your security requirements.

## Geo-replication and failover

You assign the identity to a database, so each replica carries its own assignment. Configure the geo-secondary the same way as the primary before you fail over:

- Assign the same user-assigned managed identity that the primary uses to the secondary database.
- Grant that identity the same storage access described in [Step 3: Grant the identity access to storage](#step-3-grant-the-identity-access-to-storage).
- For cross-tenant access, set the same **Federated client identity** on the secondary database.

When you configure both replicas this way, queries that use a database-scoped credential keep working after a failover. The requirement matches [database-level customer-managed keys](transparent-data-encryption-byok-database-level-overview.md#geo-replication-and-high-availability), which also need the identity in place on each replica.

The same configuration applies to both [active geo-replication](active-geo-replication-overview.md) and [failover groups](failover-group-sql-db.md).

## Limitations

- Azure SQL Database only. Azure SQL Managed Instance, SQL Server, and Azure Synapse Analytics don't support these credential identities.
- Outbound authentication only. This feature doesn't change how users and applications authenticate *to* the database.
- Not supported in the `master` database. Create the credential in a user database.
- User-assigned managed identities only. A system-assigned managed identity is scoped to the logical server, so it can't act as a database-level identity.
- One user-assigned managed identity per database.
- This preview covers data virtualization only: `CREATE EXTERNAL TABLE` and `OPENROWSET` with `DATA_SOURCE`. Other outbound features, such as `sp_invoke_external_rest_endpoint`, aren't in scope.

## Troubleshoot

Creating a database-scoped credential doesn't confirm that the database identity can get a token or access storage. Errors appear later, when you create an external data source or run a query that uses the credential.

### Database identity isn't configured (error 15858)

You can create a credential with `IDENTITY = 'DATABASE MANAGED IDENTITY'` before you assign a user-assigned managed identity to the database. If a query then uses the credential and the database has no identity to get a token with, the query fails with error 15858:

```output
Cannot obtain a Microsoft Entra ID token to access storage. Error message: 'Azure SQL Server or Database identity is not configured. Please follow the steps in "Assign a Microsoft Entra managed identity to your Azure SQL" (https://aka.ms/SQLDBMSI)'.
```

Assign a user-assigned managed identity to the database, as described in [Step 2](#step-2-assign-the-identity-to-the-database), give it access to the storage account, and then retry the query. Assigning an identity only to the logical server doesn't configure a database-level identity. If the database assignment is in place and the error persists, contact Microsoft Support to investigate database identity provisioning.

> [!NOTE]
> Trace flag 4675 and the delete-and-recreate step in [Backup and restore to URL using managed identities](../virtual-machines/windows/backup-restore-to-url-using-managed-identities.md) apply to SQL Server on Azure VMs. They don't apply to database-level managed identity in Azure SQL Database.

### Credential can't be found or referenced (error 46516)

When you create an external data source, Azure SQL Database checks that the credential exists in the current database and that you have permission to reference it. If either check fails, you get error 46516:

```output
The specified credential cannot be found or the user does not have permission to perform this action.
```

Create the credential in the database that contains the external data source, check the credential name, and make sure you have permission to reference the credential.

### Token acquisition fails (error 15858)

When the database can't get a Microsoft Entra token and the token provider returns a reason, error 15858 includes that reason:

```output
Cannot obtain a Microsoft Entra ID token to access storage. Error message: '<details>'.
```

Use the nested error message to find out why the token request failed. For cross-tenant access, check the application, the admin consent in the storage tenant, and the federated identity credential, as described in [Configure an application to trust a managed identity](/entra/workload-id/workload-identity-federation-config-app-trust-managed-identity).

To read external data, the identity also needs a storage data role. For same-tenant access, grant **Storage Blob Data Reader** on the container or storage account to the database's user-assigned managed identity. For cross-tenant access, grant it to the application's service principal in the storage tenant. For more information, see [Assign an Azure role for access to blob data](/azure/storage/blobs/assign-azure-role-data-access).

### File doesn't exist or can't be accessed (error 4860)

When a file is missing or can't be read, you get error 4860:

```output
Cannot bulk load. The file "<path>" does not exist or you don't have file access rights. See https://go.microsoft.com/fwlink/?LinkId=2374938 for more information.
```

Check that the file exists at the configured location. Then check that the identity the request uses can access it, through a storage data role or, for ADLS Gen2, access control lists (ACLs) on the path.

### Network access is blocked (errors 42121 and 16539)

When the logical server is associated with a network security perimeter that doesn't allow outbound traffic to the storage account, you get error 42121:

```output
The operation could not be completed because attempts to connect were denied by the configured Network Security Perimeter.
```

Add an outbound access rule for the storage account's host name to the perimeter profile, or associate the storage account with the same perimeter. For more information, see [Network security perimeter for Azure SQL Database](network-security-perimeter.md).

When outbound networking is restricted on the logical server and the storage account isn't in the server's outbound firewall rules, you get error 16539:

```output
Operation failed since the external data source '<external-data-source>' has underlying storage account that is not in the list of Outbound Firewall Rules on the server. Please add this storage account to the list of Outbound Firewall Rules on your server and retry the operation.
```

Add an outbound firewall rule for the exact storage host name in the external data source's location. For more information, see [Outbound firewall rules for Azure SQL Database and Azure Synapse Analytics](outbound-firewall-rule-overview.md).

### Cross-tenant configuration is missing or incorrect (errors 16561 and 16562)

A cross-tenant query can fail with either of these errors, or with [error 4860](#file-doesnt-exist-or-cant-be-accessed-error-4860):

```output
External table '<table-name>' is not accessible because content of directory cannot be listed.
```

```output
External table '<table-name>' is not accessible because location does not exist or it is used by another process.
```

These errors mean the query couldn't reach the files. The identity might not have the required storage role, network access might be blocked, the storage location might not resolve, or the token might be for the wrong tenant. To fix it:

- Grant the appropriate storage data role.
- Allow network access to the storage account.
- Include the container in the external data source location.
- Use `DATABASE FEDERATED CLIENT IDENTITY`, and set the **Federated Client ID** on the database, as described in [Step 4](#step-4-set-the-federated-client-id-on-the-database).
- Confirm that the federated identity credential's subject exactly matches the managed identity's Object (principal) ID.

### Queries fail after a failover

If queries fail with token or authorization errors after a failover, the new primary might not have the same user-assigned managed identity that the original primary used. Or that identity might not have a role assignment on the storage account. Assign the same identity to both replicas and grant it storage access, as described in [Geo-replication and failover](#geo-replication-and-failover).

## Frequently asked questions

**Can server-level and database-level credentials coexist on the same server?**

Yes, on the same logical server and in the same database. Each credential's `IDENTITY` value decides which identity a query uses: `MANAGED IDENTITY` uses the server's identity, and `DATABASE MANAGED IDENTITY` uses the database's identity.

**Can I use a system-assigned managed identity?**

No. A system-assigned managed identity belongs to the logical server, so you can't scope it to one database. Use a user-assigned managed identity.

**Which service tiers and regions support this preview?**

All Azure SQL Database service tiers, in all Azure regions.

**Do I need a database master key?**

No. Neither `DATABASE MANAGED IDENTITY` nor `DATABASE FEDERATED CLIENT IDENTITY` takes a secret, so no master key is required.

**Does this feature work with `sp_invoke_external_rest_endpoint`?**

Not in this preview. The preview covers data virtualization.

## Related content

- [Data virtualization with Azure SQL Database](data-virtualization-overview.md)
- [Managed identities in Microsoft Entra for Azure SQL](authentication-azure-ad-user-assigned-managed-identity.md)
- [Database-level customer-managed keys](transparent-data-encryption-byok-database-level-basic-actions.md)
- [Cross-tenant customer-managed keys](transparent-data-encryption-byok-cross-tenant.md)
- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](/sql/t-sql/statements/create-database-scoped-credential-transact-sql)
- [CREATE EXTERNAL TABLE (Transact-SQL)](/sql/t-sql/statements/create-external-table-transact-sql)
