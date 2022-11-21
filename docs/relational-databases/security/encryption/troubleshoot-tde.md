---
title: Common errors with customer-managed keys in Azure Key Vault
description: Learn how to identify and resolve access issues and common errors with transparent data encryption (TDE) and customer-managed keys in Azure Key Vault.
ms.custom: seo-lt-2019
helpviewer_keywords: 
  - "troublshooting, tde akv"
  - "tde akv configuration, troubleshooting"
  - "tde troubleshooting"
author: rwestMSFT
ms.author: randolphwest
ms.service: sql
ms.subservice: security
ms.reviewer: vanto
ms.topic: conceptual
ms.date: 03/04/2022
monikerRange: "= azuresqldb-current || = azure-sqldw-latest"
---
# Common errors for transparent data encryption with customer-managed keys in Azure Key Vault

[!INCLUDE[asdb-asdbmi-asa](../../../includes/applies-to-version/asdb-asdbmi-asa.md)]

This article describes how to identify and resolve Azure Key Vault key access issues that caused a database configured to use [transparent data encryption (TDE) with customer-managed keys in Azure Key Vault](/azure/sql-database/transparent-data-encryption-byok-azure-sql) to become inaccessible.

## Introduction
When TDE is configured to use a customer-managed key in Azure Key Vault, continuous access to this TDE Protector is required for the database to stay online.  If the logical SQL server or managed instance loses access to the customer-managed TDE protector in Azure Key Vault, a database will start denying all connections with the appropriate error message and change its state to *Inaccessible* in the Azure portal.

For the first 30 minutes, if the underlying Azure key vault key access issue is resolved, the database will auto-heal and come online automatically. This means that for all intermittent and temporary network outage scenarios, no user action is required, and the database will come online automatically. In most cases, user action is required to resolve the underlying key vault key access issue. 

If an inaccessible database is no longer needed, it can be deleted immediately to stop incurring costs. All other actions on the database are not permitted until access to the Azure key vault key has been restored and the database is back online. Changing the TDE option from customer-managed to service-managed keys on the server is also not possible while a database encrypted with customer-managed keys is inaccessible. This is necessary to protect the data from unauthorized access while permissions to the TDE Protector have been revoked. 

After a database has been inaccessible for more than 30 minutes, it will no longer auto-heal. If the required Azure key vault key access has been restored after that period, you must re-validate the access to the key manually, to bring the database back online. Bringing the database back online in this case can take a significant amount of time depending on the size of the database. Once the database is back online, previously configured settings such as [failover group](/azure/sql-database/sql-database-auto-failover-group), PITR history, and any tags **will be lost**. Therefore, we recommend implementing a notification system using [Action Groups](/azure/azure-monitor/platform/action-groups) that allows to become aware of and address the underlying key vault key access issues as soon as possible. 

## Common errors causing databases to become inaccessible

Most issues that occur when you use TDE with Key Vault are caused by one of the following misconfigurations:

### The key vault is unavailable or doesn't exist

- The key vault was accidentally deleted.
- The firewall was configured for Azure Key Vault, but it doesn't allow access to Microsoft services.
- An intermittent network error causes the key vault to be unavailable.

### No permissions to access the key vault or the key doesn't exist

- The key was accidentally deleted, disabled or the key expired.
- The server's managed identity (system-assigned or user-assigned) was accidentally deleted.
- The server was moved to a different subscription. A new managed identity (system-assigned or user-assigned) must be assigned to the server when it is moved to a different subscription.
- Permissions granted to the server's managed identity for the keys aren't sufficient (they don't include Get, Wrap, and Unwrap permissions).
- Permissions for the server's managed identity were revoked from the key vault.

## Identify and resolve common errors

In this section, we list troubleshooting steps for the most common errors.

### Missing server identity

**Error message**

_401 AzureKeyVaultNoServerIdentity - The server identity is not correctly configured on server. Please contact support.

**Detection**

Use the following cmdlet or command to ensure that an identity has been assigned to the server:

- Azure PowerShell: [Get-AzSqlServer](/powershell/module/Az.Sql/Get-AzSqlServer) 

- Azure CLI: [az-sql-server-show](/cli/azure/sql/server#az-sql-server-show)

**Mitigation**

Use the following cmdlet or command to configure an Azure AD identity for the server:

- Azure PowerShell: [Set-AzSqlServer](/powershell/module/az.sql/set-azsqlserver) with the `-AssignIdentity` option.

- Azure CLI: [az sql server update](/cli/azure/sql/server#az-sql-server-update) with the `--assign_identity` option.

In the Azure portal, go to the key vault, and then go to **Access policies**. Complete these steps: 

 1. Use the **Add New** button to add the AppId for the server you created in the preceding step. 
 1. Assign the following key permissions: Get, Wrap, and Unwrap 

To learn more, see [Assign an Azure AD identity to your server](/azure/sql-database/transparent-data-encryption-byok-azure-sql-configure#assign-an-azure-ad-identity-to-your-server).

> [!IMPORTANT]
> If the logical SQL Server or managed instance was moved to a new tenant after the initial configuration of TDE with Key Vault, repeat the step to configure the Azure AD identity to assign a new managed identity (system-assigned or user-assigned) to the server. Then, add the managed identity to the key vault and assign the correct permissions to the key. 
>

### Missing key vault

**Error message**

_503 AzureKeyVaultConnectionFailed - The operation could not be completed on the server because attempts to connect to Azure Key Vault have failed._

**Detection**

To identify the key URI and the key vault:

1. Use the following cmdlet or command to get the key URI of a specific server instance:

    - Azure PowerShell: [Get-AzSqlServerKeyVaultKey](/powershell/module/az.sql/get-azsqlserverkeyvaultkey)

    - Azure CLI: [az-sql-server-tde-key-show](/cli/azure/sql/server/tde-key#az-sql-server-tdekey-show) 

1. Use the key URI to identify the key vault:

    - Azure PowerShell: You can inspect the properties of the $MyServerKeyVaultKey variable to get details about the key vault.

    - Azure CLI: Inspect the returned server encryption protector for details about the key vault.

**Mitigation**

Confirm that the key vault is available:

- Ensure that the key vault is available and that the server has access.
- If the key vault is behind a firewall, ensure that the check box to allow Microsoft services to access the key vault is selected.
- If the key vault has been accidentally deleted, you must complete the configuration from the start.


### Missing key

**Error messages**

_404 ServerKeyNotFound - The requested server key was not found on the current subscription._ 

_409 ServerKeyDoesNotExists - The server key does not exist._

**Detection**

To identify the key URI and the key vault:

- Use the cmdlet or commands in [Missing key vault](#missing-key-vault) to identify the key URI that's added to the logical SQL Server instance. Running the commands returns the list of keys.

**Mitigation**

Confirm that the TDE protector is present in Key Vault:

1. Identify the key vault, then go to the key vault in the Azure portal.
1. Ensure that the key identified by the key URI is present.

### Missing permissions

**Error message**

_401 AzureKeyVaultMissingPermissions - The server is missing required permissions on the Azure Key Vault._

**Detection**

To identify the key URI and key vault: 

- Use the cmdlet or commands in [Missing key vault](#missing-key-vault) to identify the key vault that the logical SQL Server instance uses.

**Mitigation**

Confirm that the server has permissions to the key vault and the correct permissions to access the key:

- In the Azure portal, go to the key vault > **Access policies**. Find the server's managed identity (system-assigned or user-assigned).  
- If the server identity is present, ensure that it has the following key permissions: Get, WrapKey, and UnwrapKey.
- If the server identity isn't present, add it by using the **Add New** button. 

## Getting TDE status from the Activity log

To allow for monitoring of the database status due to Azure Key Vault key access issues, the following events will be logged to the [Activity Log](/azure/service-health/alerts-activity-log-service-notifications) for the resource ID based on the Azure Resource Manager URL.

> [!NOTE]
> Events may take at least 15-30 mins to appear in the Activity Log from the time key vault access issue occurs. 

**Event when the service loses access to the Azure Key Vault key**

**Azure SQL Database**

EventName: MakeDatabaseInaccessible

Status: Started 

Description: Database {database_name} on Server {server_name} has lost access to Azure Key Vault Key and is now transitioning to inaccessible state.  
  
**Azure SQL Managed Instance**

EventName: MakeManagedDatabaseInaccessible

Status: Started 

Description: Database { database_name} on managed server {server_name} has lost access to Azure Key Vault Key and is now transitioning to inaccessible state.


**Event when the issue wasn't resolved within 30 minutes and Azure Key Vault key access has to be validated manually** 

**Azure SQL Database**
  
EventName: MakeDatabaseInaccessible 

Status: Succeeded 

Description: Database is inaccessible and requires user to resolve Azure key vault errors and reestablish access to Azure key vault key using Re-validate key. 

**Azure SQL Managed Instance**
  
EventName: MakeManagedDatabaseInaccessible 

Status: Succeeded 

Description: Database { database_name} on managed server {server_name} is inaccessible and requires user to re-establish access to Azure Key Vault Key.


**Event when re-validation of Azure Key Vault key access has succeeded and the db is coming back online**

**Azure SQL Database**
  
EventName: MakeDatabaseAccessible 

Status: Started 

Description: Access to Azure Key Vault Key has been re-established, operation to make database {database_name} on server {server_name} accessible started. 

**Azure SQL Managed Instance**
  
EventName: MakeManagedDatabaseAccessible 

Status: Started 

Description: Access to Azure Key Vault Key has been re-established, operation to make database {database_name} on managed server {server_name} accessible started.

  
**Event when the database has successfully come back online**

**Azure SQL Database**
  
EventName: MakeDatabaseAccessible

Status: Succeeded 

Description: Access to Azure Key Vault Key has been re-established and database {database_name} on server {server_name} is now online.

**Azure SQL Managed Instance**
  
EventName: MakeManagedDatabaseAccessible

Status: Succeeded 

Description: Access to Azure Key Vault Key has been re-established and database {database_name} on managed server {server_name} is now online.  
  

**Event when re-validation of Azure Key Vault key access has failed**

**Azure SQL Database**
  
EventName: MakeDatabaseAccessible 

Status: Failed 

Description: Access to Azure Key Vault Key has been re-established, operation to make database {database_name} accessible on server {server_name} failed

**Azure SQL Managed Instance**
  
EventName: MakeManagedDatabaseAccessible 

Status: Failed 

Description: Access to Azure Key Vault Key has been re-established, operation to make database {database_name} accessible on managed server {server_name} failed 
  

## Next steps

- Learn about [Azure Resource Health](/azure/service-health/resource-health-overview).
- Set up [Action Groups](/azure/azure-monitor/platform/action-groups) to receive notifications and alerts based on your preferences, e.g. Email/SMS/Push/Voice, Logic App, Webhook, ITSM, or Automation Runbook.
