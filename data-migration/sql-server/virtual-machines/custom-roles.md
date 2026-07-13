---
title: "Custom Roles: Online SQL Server to Azure Virtual Machines Migrations with DMS"
titleSuffix: Azure Database Migration Service
description: Learn to use the custom roles for SQL Server to Azure VM's migrations.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/09/2026
ms.service: azure-database-migration-service
ms.topic: how-to
ms.collection:
  - sql-migration-content
---

# Custom roles for SQL Server to Azure Virtual Machines migrations using ADS

This article explains how to set up a custom role in Azure for SQL Server database migrations. The custom role is configured with only the permissions required to initiate and execute migrations using an instance of Azure Database Migration Service, targeting Azure Virtual Machine. To provision a new instance of Azure Database Migration Service, the user must be assigned either the **Owner** or **Contributor** role at the subscription level.

[!INCLUDE [custom-roles-intro](../../includes/custom-roles-intro.md)]

> [!NOTE]
> To configure the migration by using **Azure portal**, you also need **Storage Blob Data Reader** access on the **blob container**.

For **Custom** role:

Use the `AssignableScopes` section of the role definition JSON string to control where the permissions appear in the **Add role assignment** UI in the Azure portal. To avoid cluttering the UI with extra roles, you might want to define the role at the level of the resource group, or even the level of the resource. The resource that the custom role applies to doesn't perform the actual role assignment.

```json
{
    "properties": {
        "roleName": "DmsCustomRoleDemoForVM",
        "description": "",
        "assignableScopes": [
            "/subscriptions/<storageSubscription>/resourceGroups/<storageAccountRG>",
            "/subscriptions/<ManagedInstanceSubscription>/resourceGroups/<virtualMachineRG>",
            "/subscriptions/<DMSSubscription>/resourceGroups/<dmsServiceRG>"
        ],
        "permissions": [
            {
                "actions": [
                    "Microsoft.Storage/storageAccounts/read",
                    "Microsoft.Storage/storageAccounts/listkeys/action",
                    "Microsoft.Storage/storageAccounts/blobServices/read",
                    "Microsoft.Storage/storageAccounts/blobServices/write",
                    "Microsoft.Storage/storageAccounts/blobServices/containers/read",
                    "Microsoft.SqlVirtualMachine/sqlVirtualMachines/read",
                    "Microsoft.SqlVirtualMachine/sqlVirtualMachines/write",
                    "Microsoft.DataMigration/locations/operationResults/read",
                    "Microsoft.DataMigration/locations/operationStatuses/read",
                    "Microsoft.DataMigration/locations/sqlMigrationServiceOperationResults/read",
                    "Microsoft.DataMigration/databaseMigrations/write",
                    "Microsoft.DataMigration/databaseMigrations/read",
                    "Microsoft.DataMigration/databaseMigrations/delete",
                    "Microsoft.DataMigration/databaseMigrations/cancel/action",
                    "Microsoft.DataMigration/databaseMigrations/cutover/action",
                    "Microsoft.DataMigration/sqlMigrationServices/write",
                    "Microsoft.DataMigration/sqlMigrationServices/delete",
                    "Microsoft.DataMigration/sqlMigrationServices/read",
                    "Microsoft.DataMigration/sqlMigrationServices/listAuthKeys/action",
                    "Microsoft.DataMigration/sqlMigrationServices/regenerateAuthKeys/action",
                    "Microsoft.DataMigration/sqlMigrationServices/deleteNode/action",
                    "Microsoft.DataMigration/sqlMigrationServices/listMonitoringData/action",
                    "Microsoft.DataMigration/sqlMigrationServices/listMigrations/read",
                    "Microsoft.DataMigration/sqlMigrationServices/MonitoringData/read",
                    "Microsoft.DataMigration/SqlMigrationServices/tasks/read",
                    "Microsoft.DataMigration/SqlMigrationServices/tasks/write",
                    "Microsoft.DataMigration/SqlMigrationServices/tasks/delete",
                    "Microsoft.DataMigration/sqlMigrationServices/validateIR/action"
                ],
                "notActions": [],
                "dataActions": [
                    "Microsoft.Storage/storageAccounts/blobServices/containers/blobs/read"
                ],
                "notDataActions": []
            }
        ]
    }
}
```

You can use either the Azure portal, Azure PowerShell, Azure CLI, or Azure REST API to create the roles.

For more information, see [Create or update Azure custom roles using the Azure portal](/azure/role-based-access-control/custom-roles-portal) and [Azure custom roles](/azure/role-based-access-control/custom-roles).

> [!NOTE]  
> When migrating to Azure SQL Managed Instance or Azure SQL Virtual Machine via **Azure portal**, make sure the signed in user has **Storage Blob Data Reader** access on the *blob container* that contains the backup files. This permission is needed to list folders and files in the blob container during migration setup via Azure portal only.

## Description of permissions needed to migrate to a virtual machine

| Permission Action | Description |
| --- | --- |
| `Microsoft.Storage/storageAccounts/read` | Returns the list of storage accounts or gets the properties for the specified storage account. |
| `Microsoft.Storage/storageAccounts/listkeys/action` | Returns the access keys for the specified storage account. |
| `Microsoft.Storage/storageAccounts/blobServices/read` | List blob services. |
| `Microsoft.Storage/storageAccounts/blobServices/write` | Returns the result of put blob service properties. |
| `Microsoft.Storage/storageAccounts/blobServices/containers/read` | Returns list of containers. |
| `Microsoft.Sql/managedInstances/read` | Return the list of managed instances or gets the properties for the specified managed instance. |
| `Microsoft.Sql/managedInstances/write` | Creates a managed instance with the specified parameters or update the properties or tags for the specified managed instance. |
| `Microsoft.Sql/managedInstances/databases/read` | Gets existing managed database. |
| `Microsoft.Sql/managedInstances/databases/write` | Creates a new database or updates an existing database. |
| `Microsoft.Sql/managedInstances/databases/delete` | Deletes an existing managed database. |
| `Microsoft.DataMigration/locations/operationResults/read` | Get the status of a long-running operation related to a 202 Accepted response. |
| `Microsoft.DataMigration/locations/operationStatuses/read` | Get the status of a long-running operation related to a 202 Accepted response. |
| `Microsoft.DataMigration/locations/sqlMigrationServiceOperationResults/read` | Retrieve Service Operation Results. |
| `Microsoft.DataMigration/databaseMigrations/write` | Create or Update Database Migration resource. |
| `Microsoft.DataMigration/databaseMigrations/read` | Retrieve the Database Migration resource. |
| `Microsoft.DataMigration/databaseMigrations/delete` | Delete Database Migration resource. |
| `Microsoft.DataMigration/databaseMigrations/cancel/action` | Stop ongoing migration for the database. |
| `Microsoft.DataMigration/databaseMigrations/cutover/action` | Cutover online migration operation for the database. |
| `Microsoft.DataMigration/sqlMigrationServices/write` | Create a new or change properties of existing Service |
| `Microsoft.DataMigration/sqlMigrationServices/delete` | Delete existing Service. |
| `Microsoft.DataMigration/sqlMigrationServices/read` | Retrieve details of Migration Service. |
| `Microsoft.DataMigration/sqlMigrationServices/listAuthKeys/action` | Retrieve the List of Authentication Keys. |
| `Microsoft.DataMigration/sqlMigrationServices/regenerateAuthKeys/action` | Regenerate the Authentication Keys. |
| `Microsoft.DataMigration/sqlMigrationServices/deleteNode/action` | De-register the IR node. |
| `Microsoft.DataMigration/sqlMigrationServices/listMonitoringData/action` | Lists the Monitoring Data for all migrations. |
| `Microsoft.DataMigration/sqlMigrationServices/listMigrations/read` | Lists the migrations for the user. |
| `Microsoft.DataMigration/sqlMigrationServices/MonitoringData/read` | Retrieve the Monitoring Data. |
| `Microsoft.SqlVirtualMachine/sqlVirtualMachines/read` | Retrieve details of SQL virtual machine. |
| `Microsoft.SqlVirtualMachine/sqlVirtualMachines/write` | Create a new or change properties of existing SQL virtual machine. |
| `Microsoft.DataMigration/SqlMigrationServices/tasks/read` | Get Migration Service Task details |
| `Microsoft.DataMigration/SqlMigrationServices/tasks/write` | Create or Update Migration Service Task |
| `Microsoft.DataMigration/SqlMigrationServices/tasks/delete` | Delete Migration Service Task |
| `Microsoft.Storage/storageAccounts/blobServices/containers/blobs/read` | Read blob containers in an Azure Storage account |
| `Microsoft.DataMigration/sqlMigrationServices/validateIR/action` | Validate Integration Runtime. |

[!INCLUDE [custom-roles-shared](../../includes/custom-roles-shared.md)]

## Related content

- [Database Migration Guide](../../index.yml)
