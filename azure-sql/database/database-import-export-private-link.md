---
title: Import or Export a Database Using Private Link
description: Import or export an Azure SQL Database using Private Link without requiring Azure services to access the server.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
ms.date: 11/06/2025
ms.service: azure-sql-database
ms.subservice: migration
ms.topic: how-to
ms.collection:
  - sql-migration-content
ms.custom:
  - sqldbrb=1
  - sfi-image-nochange
---

# Import or export an Azure SQL Database by using private link

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article explains how to import or export an Azure SQL Database by using [Private Link](private-endpoint-overview.md) with the Azure portal or Azure PowerShell. Set *Allow Access to Azure Services* to **Off** on the Azure SQL [logical server](logical-servers.md).

By using Private Link, you can perform import and export operations securely without needing to set [Allow Access to Azure Services](network-access-controls-overview.md) to **On**, which is otherwise required for standard import and export operations.

> [!NOTE]  
> Import and export by using private link for Azure SQL Database is currently in preview.

## Prerequisites

Before using import and export with Private Link, ensure you have the following resources and configurations:

- An Azure SQL logical server with private endpoint connectivity configured to a virtual network
- An Azure Storage account with a blob container that contains your BACPAC file (for import) or stores the exported BACPAC file
- The Azure SQL logical server and Storage account must have private endpoints configured within the same or peered virtual networks
- Appropriate permissions to approve private endpoint connections on both the Azure SQL logical server and the Storage account

## What is import/export private link?

The database import/export private link is a service managed private endpoint created by Microsoft. When you enable the **Use private link** option during import or export, the service automatically creates private endpoints to facilitate secure communication between the database import/export process, your Azure SQL database, and the Azure Storage service.

These service-managed private endpoints are created within the same virtual network and subnet where your existing Azure SQL logical server private endpoint is located. The endpoints receive private IP addresses from that subnet's address space and must be manually approved before the import/export operation can proceed.

> [!IMPORTANT]  
> If your Azure SQL logical server or Storage account already has private endpoints configured, the import/export service creates **additional** service-managed private endpoints specifically for the import/export operation. These private endpoints are separate from your existing private endpoints and require separate approval.

You must manually approve the private endpoint for both the Azure SQL logical server and the Azure Blob storage account in separate steps. This tutorial includes the details.

:::image type="content" source="media/database-import-export-private-link/import-export-private-link.png" alt-text="Diagram of Import Export Private link architecture." lightbox="media/database-import-export-private-link/import-export-private-link.png":::

## Configure Import-Export Private Link using the Azure portal

You can configure Import/Export Private Link through the Azure portal, PowerShell, or REST API.

> [!NOTE]  
> The service automatically creates private endpoints in the same virtual network and subnet as your Azure SQL logical server's existing private endpoint. You don't need to manually specify the virtual network or subnet during the import/export process. If your SQL server doesn't have a private endpoint configured, you must set one up before using import/export with Private Link.

### 1. Configure Import/Export private link using the Azure portal

<a id="create-import-private-link"></a>

#### Enable Import private link

You need to enable and approve the import private link. Azure automatically creates the private link.

1. Go to the **Overview** page of the Azure SQL logical server where you want to import the database. Select **Import database** from the toolbar.

1. In the **Import database** page, select the **Use private link** option checkbox.

1. Enter the storage account, authentication credentials, database details, and select **OK**.

<a id="create-export-private-link"></a>

#### Enable export private link

You need to enable and approve the export private link. Azure automatically creates the private link.

1. Go to the **Overview** page of the Azure SQL database that you want to export. Select **Export** from the toolbar.
1. In the **Export database** page, select the **Use private link** option checkbox.
1. Enter the storage account, authentication credentials, database details, and select **OK**.

### 2. Approve private link for Azure SQL logical server

The user must approve the new private endpoint in the Private Link Center or in the Azure SQL Database. You can approve both private links individually in their respective Azure portal locations.

> [!TIP]  
> If you already have private endpoints configured for your Azure SQL logical server and Storage account, you see the new service-managed private endpoints appear as **pending connections**. These private endpoints are specifically for the import/export operation and are separate from your existing private endpoints. Look for private endpoints with names indicating they were created by the import/export service.

#### Option 1: Approve private endpoints in Private Link Center in Azure portal

1. Navigate to the Private Link Center in the Azure portal. In the Azure search box, search for *Private Link*. Select **Private Link**.

1. In the Private Link Center, select **Pending connections**.

1. Select both private endpoints you created by using the Import/Export service. Select **Approve**.

   :::image type="content" source="media/database-import-export-private-link/approve-private-link-center.png" alt-text="Screenshot from the Private Link Center of the Azure portal showing how to approve both private links." lightbox="media/database-import-export-private-link/approve-private-link-center.png":::

#### Option 2: Approve private endpoint connections separately in Azure portal

1. Go to the Azure SQL logical server that hosts the database.

1. In the resource menu under **Security**, select **Networking**.

1. Select the **Private access** tab.

1. In the list under **Private endpoint connections**, select the private endpoints you created by using the Import/Export service.

1. Select **Approve**.

   :::image type="content" source="media/database-import-export-private-link/approve-private-link.png" alt-text="Screenshot from the Azure portal that shows how to approve Azure SQL Database Private Link." lightbox="media/database-import-export-private-link/approve-private-link.png":::

1. Go to the storage account that hosts the blob container where the BACPAC (`.bacpac`) file exists.

1. Under **Security + Networking**, select **Networking**.

1. Select the **Private endpoint connections** tab.

1. In the list, select the private endpoint for the import/export service.

1. Select **Approve** to approve the connection.

   :::image type="content" source="media/database-import-export-private-link/approve-private-link-storage.png" alt-text="Screenshot from the Azure portal that shows how to approve Azure Storage Private Link in Azure Storage." lightbox="media/database-import-export-private-link/approve-private-link-storage.png":::

### 3. Check import/export status

1. After you approve the private endpoints in both the Azure SQL server and the Azure Storage account, the database import or export job begins. Until then, the jobs are on hold.

1. You can check the status of database import or export jobs in **Import/Export History** page under **Data Management** section in Azure SQL server page.

   :::image type="content" source="media/database-import-export-private-link/import-export-status.png" alt-text="Screenshot from the Azure portal that shows how to check Import Export Jobs Status." lightbox="media/database-import-export-private-link/import-export-status.png":::

## Configure import/export private link using PowerShell

When you use PowerShell, the service-managed private endpoints are automatically created in the same virtual network and subnet as your Azure SQL logical server's existing private endpoint. Ensure your SQL server has private endpoint connectivity configured before running these commands.

### Import a database using private link in PowerShell

Use the [New-AzSqlDatabaseImport](/PowerShell/module/az.sql/new-azsqldatabaseimport) cmdlet to submit an import database request to Azure. Depending on database size, the import might take some time to complete. The DTU-based provisioning model supports select database max size values for each tier. When importing a database, [use the supported edition and service objective values for Azure SQL Database](/sql/t-sql/statements/create-database-transact-sql?view=azuresqldb-current&preserve-view=true#create-a-database).

Provide your own `<values>` in the following PowerShell code sample, which imports a .bacpac file named `sample.bacpac` and creates an Azure SQL Database with two General Purpose vCores.

```powershell
$importRequestParams = @{
   ResourceGroupName                      = "<resourceGroupName>"
   ServerName                             = "<serverName>"
   DatabaseName                           = "<databaseName>"
   DatabaseMaxSizeBytes                   = "<databaseSizeInBytes>"
   StorageKeyType                         = "StorageAccessKey"
   StorageKey                             = $(Get-AzStorageAccountKey -ResourceGroupName $resourceGroupName -StorageAccountName "<storageAccountName>").Value[0]
   StorageUri                             = "https://myStorageAccount.blob.core.windows.net/importsample/sample.bacpac"
   Edition                                = "GeneralPurpose"
   ServiceObjectiveName                   = "GP_Gen5_2"
   UseNetworkIsolation                    = $true
   StorageAccountResourceIdForPrivateLink = "/subscriptions/<subscriptionId>/resourceGroups/<resource_group_name>/providers/Microsoft.Storage/storageAccounts/<storage_account_name>"
   SqlServerResourceIdForPrivateLink      = "/subscriptions/<subscriptionId>/resourceGroups/<resource_group_name>/providers/Microsoft.Sql/servers/<server_name>"
   AdministratorLogin                     = "<userID>"
   AdministratorLoginPassword             = $(ConvertTo-SecureString -String "<password>" -AsPlainText -Force)
}

$importRequest = New-AzSqlDatabaseImport @importRequestParams
```

### Export a database using private link in PowerShell

Use the [New-AzSqlDatabaseExport](/PowerShell/module/az.sql/new-azsqldatabaseexport) cmdlet to submit an export database request to the Azure SQL Database service. Depending on the size of your database, the export operation might take some time to complete.

Provide your own `<values>` in the following PowerShell code sample:

```powershell
$exportRequestParams = @{
   ResourceGroupName                      = "<resourceGroupName>"
   ServerName                             = "<serverName>"
   DatabaseName                           = "<databaseName>"
   StorageKeyType                         = "StorageAccessKey"
   StorageKey                             = $(Get-AzStorageAccountKey -ResourceGroupName $resourceGroupName -StorageAccountName "<storageAccountName>").Value[0]
   StorageUri                             = "https://myStorageAccount.blob.core.windows.net/importsample/sample.bacpac"
   UseNetworkIsolation                    = $true
   StorageAccountResourceIdForPrivateLink = "/subscriptions/<subscriptionId>/resourceGroups/<resource_group_name>/providers/Microsoft.Storage/storageAccounts/<storage_account_name>"
   SqlServerResourceIdForPrivateLink      = "/subscriptions/<subscriptionId>/resourceGroups/<resource_group_name>/providers/Microsoft.Sql/servers/<server_name>"
   AdministratorLogin                     = "<userID>"
   AdministratorLoginPassword             = $(ConvertTo-SecureString -String "<password>" -AsPlainText -Force)
}

$exportRequest = New-AzSqlDatabaseExport @exportRequestParams
```

## Create import/export private link using REST API

Existing APIs to perform Import and Export jobs support Private Link. Refer to [Import Database API](/rest/api/sql/servers/import-database).

## Limitations

Currently, the private link for import/export has the following limitations:

- To use Private Link with Import/Export, the user database and the Azure Storage blob container must be on the same type of Azure Cloud. For example, both must be in Azure Commercial or both in Azure Gov. Hosting across cloud types isn't supported.

- Import or Export of a database from [Azure SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md) by using PowerShell isn't currently supported.

- Don't use Import/Export Private Link when the user Database or the Azure Storage account are protected by Resource locks.

- You need to manually approve the new private endpoint connection to complete the export operation. After starting the export, go to **pending connections** in the Private Link Center and approve the connections, which can take several minutes to appear.

- Import using Private Link doesn't support specifying a backup storage redundancy while creating a new database and creates with the default geo-redundant backup storage redundancy. As a workaround, first create an empty database with desired backup storage redundancy by using Azure portal or PowerShell, then import the BACPAC file into this empty database.

- Import using REST API with private link can only be completed for an existing database since the API uses database extensions. As a workaround, create an empty database with the desired name, then call the Import REST API with private link.

## Related content

- [Import or export an Azure SQL Database without allowing Azure services to access the server](database-import-export-azure-services-off.md)
- [Quickstart: Import a bacpac file to a database in Azure SQL Database or Azure SQL Managed Instance](database-import.md)
