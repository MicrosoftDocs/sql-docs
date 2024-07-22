---
title: "Tutorial: Migrate SQL Server to SQL Server on Azure Virtual Machine using Azure Data Studio"
titleSuffix: Azure Database Migration Service
description: Learn how to migrate on-premises SQL Server to SQL Server on Azure Virtual Machines online by using Azure Data Studio and Azure Database Migration Service.
author: abhims14
ms.author: abhishekum
ms.reviewer: cawrites, randolphwest
ms.date: 06/26/2024
ms.service: dms
ms.topic: tutorial
ms.custom:
  - sql-migration-content
---

# Tutorial: Migrate SQL Server to SQL Server on Azure Virtual Machines with DMS

You can use Azure Database Migration Service and the Azure SQL Migration extension in Azure Data Studio to migrate databases from an on-premises instance of SQL Server to [SQL Server on Azure Virtual Machines (SQL Server 2016 and later)](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview) with minimal downtime.

For database migration methods that might require some manual configuration, see [SQL Server instance migration to SQL Server on Azure Virtual Machines](/azure/azure-sql/migration-guides/virtual-machines/sql-server-to-sql-on-azure-vm-migration-overview).

In this tutorial, you migrate the `AdventureWorks2022` database from an on-premises instance of SQL Server to a SQL Server on Azure Virtual Machine with minimal downtime by using Azure Data Studio with Azure Database Migration Service.

This tutorial offers both offline and online migration options, including an acceptable downtime during the migration process.

In this tutorial, you learn how to:
> [!div class="checklist"]
> - Launch the Migrate to Azure SQL wizard in Azure Data Studio.
> - Run an assessment of your source SQL Server databases.
> - Collect performance data from your source SQL Server.
> - Get a recommendation of the SQL Server on Azure Virtual Machine SKU best suited for your workload.
> - Specify details of your source SQL Server, backup location and your target SQL Server on Azure Virtual Machine.
> - Create a new Azure Database Migration Service and install the self-hosted integration runtime to access source server and backups.
> - Start and monitor the progress for your migration.
> - Perform the migration cutover when you're ready.

## Prerequisites

Before you begin the tutorial:

- [Download and install Azure Data Studio](/azure-data-studio/download-azure-data-studio).
- [Install the Azure SQL Migration extension](/azure-data-studio/extensions/azure-sql-migration-extension) from Azure Data Studio Marketplace.
- Have an Azure account that's assigned to one of the following built-in roles:

  - Contributor for the target instance of SQL Server on Azure Virtual Machines, and for the storage account where you upload your database backup files from a Server Message Block (SMB) network share

  - Reader role for the Azure resource group that contains the target instance of SQL Server on Azure Virtual Machines or for your Azure Storage account

  - Owner or Contributor role for the Azure subscription

  As an alternative to using one of these built-in roles, you can [assign custom roles](custom-roles.md).

  > [!IMPORTANT]  
  > An Azure account is required only when you configure the migration steps. An Azure account isn't required for the assessment or to view Azure recommendations in the migration wizard in Azure Data Studio.

- Create a target instance of [SQL Server on Azure Virtual Machines](/azure/azure-sql/virtual-machines/windows/create-sql-vm-portal).

    > [!IMPORTANT]  
    > If you have an existing Azure virtual machine, it should be registered with the [SQL IaaS Agent extension in Full management mode](/azure/azure-sql/virtual-machines/windows/sql-server-iaas-agent-extension-automate-management#management-modes).

- Ensure that the logins that you use to connect the source SQL Server instance are members of the **sysadmin** server role or have `CONTROL SERVER` permission.

- Provide an SMB network share, Azure storage account file share, or Azure storage account blob container that contains your full database backup files and subsequent transaction log backup files. Database Migration Service uses the backup location during database migration.

  - The Azure SQL Migration extension for Azure Data Studio doesn't take database backups, and doesn't initiate any database backups on your behalf. Instead, the service uses existing database backup files for the migration.

  - If your database backup files are in an SMB network share, [create an Azure storage account](/azure/storage/common/storage-account-create) that Database Migration Service can use to upload database backup files to and to migrate databases. Make sure you create the Azure storage account in the same region where you create your instance of Database Migration Service.

  - You can write each backup to either a separate backup file or to multiple backup files. Appending multiple backups such as full and transaction logs into a single backup media isn't supported.

  - You can provide compressed backups to reduce the likelihood of experiencing potential issues associated with migrating large backups.

- Ensure that the service account that's running the source SQL Server instance has read and write permissions on the SMB network share that contains database backup files.

- If you're migrating a database that's protected by transparent data encryption (TDE), the certificate from the source SQL Server instance must be migrated to SQL Server on Azure Virtual Machines before you migrate data. To learn more, see [Move a TDE protected database to another SQL Server](/sql/relational-databases/security/encryption/move-a-tde-protected-database-to-another-sql-server).

  > [!TIP]  
  > If your database contains sensitive data that's protected by [Always Encrypted](/sql/relational-databases/security/encryption/configure-always-encrypted-using-sql-server-management-studio), the migration process automatically migrates your Always Encrypted keys to your target instance of SQL Server on Azure Virtual Machines.

- If your database backups are on a network file share, provide a computer on which you can install a [self-hosted integration runtime](/azure/data-factory/create-self-hosted-integration-runtime) to access and migrate database backups. The migration wizard gives you the download link and authentication keys to download and install your self-hosted integration runtime.

  In preparation for the migration, ensure that the computer on which you install the self-hosted integration runtime has the following outbound firewall rules and domain names enabled:

  | Domain names | Outbound port | Description |
  | --- | --- | --- |
  | Public cloud: `{datafactory}.{region}.datafactory.azure.net`<br />or`*.frontend.clouddatahub.net`<br /><br />Azure Government:`{datafactory}.{region}.datafactory.azure.us`<br />Microsoft Azure operated by 21Vianet:`{datafactory}.{region}.datafactory.azure.cn` | 443 | Required by the self-hosted integration runtime to connect to Database Migration Service.<br />For a newly created data factory in a public cloud, locate the fully qualified domain name (FQDN) from your self-hosted integration runtime key, in the format `{datafactory}.{region}.datafactory.azure.net`.<br />For an existing data factory, if you don't see the FQDN in your self-hosted integration key, use `*.frontend.clouddatahub.net` instead. |
  | `download.microsoft.com` | 443 | Required by the self-hosted integration runtime for downloading the updates. If you have disabled autoupdate, you can skip configuring this domain. |
  | `.core.windows.net` | 443 | Used by the self-hosted integration runtime that connects to the Azure storage account to upload database backups from your network share |

  > [!TIP]  
  > If your database backup files are already provided in an Azure storage account, a self-hosted integration runtime isn't required during the migration process.

- If you use a self-hosted integration runtime, make sure that the computer on which the runtime is installed can connect to the source SQL Server instance and the network file share where backup files are located.

- Enable outbound port 445 to allow access to the network file share. For more information, see [recommendations for using a self-hosted integration runtime](/azure/dms/migration-using-azure-data-studio#recommendations-for-using-a-self-hosted-integration-runtime-for-database-migrations).

- If you're using Azure Database Migration Service for the first time, make sure that the `Microsoft.DataMigration` [resource provider is registered in your subscription](/azure/dms/quickstart-create-data-migration-service-portal#register-the-resource-provider).

## [Offline with Azure DMS](#tab/offline-with-extension)

This tutorial describes an offline migration from SQL Server to SQL Server on Azure Virtual Machines.

## Open the Migrate to Azure SQL wizard in Azure Data Studio

To open the Migrate to Azure SQL wizard:

1. In Azure Data Studio, go to **Connections**. Select and connect to your on-premises instance of SQL Server. You also can connect to SQL Server on an Azure virtual machine.

1. Right-click the server connection and select **Manage**.

1. In the server menu under **General**, select **Azure SQL Migration**.

1. In the Azure SQL Migration dashboard, select **Migrate to Azure SQL** to open the migration wizard.

    :::image type="content" source="media/database-migration-service/launch-migrate-to-azure-sql-wizard.png" alt-text="Screenshot that shows how to open the Migrate to Azure SQL wizard." lightbox="media/database-migration-service/launch-migrate-to-azure-sql-wizard.png":::

1. On the first page of the wizard, start a new session or resume a previously saved session.

## Run a database assessment, collect performance data, and get Azure recommendations

1. In **Step 1: Databases for assessment** in the Migrate to Azure SQL wizard, select the databases you want to assess. Then, select **Next**.

1. In **Step 2: Assessment results and recommendations**, complete the following steps:

   1. In **Choose your Azure SQL target**, select **SQL Server on Azure Virtual Machine**.

      :::image type="content" source="media/database-migration-service/assessment-complete-target-selection.png" alt-text="Screenshot that shows an assessment confirmation.":::

   1. Select **View/Select** to view the assessment results.

   1. In the assessment results, select the database, and then review the assessment report to make sure no issues were found.

   1. Select **Get Azure recommendation** to open the recommendations pane.

   1. Select **Collect performance data now**. Select a folder on your local computer to store the performance logs, and then select **Start**.

      Azure Data Studio collects performance data until you either stop data collection or you close Azure Data Studio.

      After 10 minutes, Azure Data Studio indicates that a recommendation is available for SQL Server on Azure Virtual Machines. After the first recommendation is generated, you can select **Restart data collection** to continue the data collection process and refine the SKU recommendation. An extended assessment is especially helpful if your usage patterns vary over time.

   1. In the selected **SQL Server on Azure Virtual Machines** target, select **View details** to open the detailed SKU recommendation report:

   1. In **Review SQL Server on Azure Virtual Machines Recommendations**, review the recommendation. To save a copy of the recommendation, select the **Save recommendation report** checkbox.

1. Select **Close** to close the recommendations pane.

1. Select **Next** to continue your database migration in the wizard.

## Configure migration settings

1. In **Step 3: Azure SQL target** in the Migrate to Azure SQL wizard, select your Azure account, Azure subscription, the Azure region or location, and the resource group that contains the target SQL Server to Azure Virtual Machines instance. Then, select **Next**.

1. In **Step 4: Migration mode**, select **Offline migration**, and then select **Next**.

   > [!NOTE]  
   > In offline migration mode, the source SQL Server database shouldn't be used for write activity while database backup files are restored on the target instance of SQL Server to Azure Virtual Machines. Application downtime persists from the start of the migration process until it's finished.

1. In **Step 5: Data source configuration**, select the location of your database backups. Your database backups can be located either on an on-premises network share or in an Azure storage blob container.

   > [!NOTE]  
   > If your database backups are provided in an on-premises network share, you must set up a self-hosted integration runtime in the next step of the wizard. A self-hosted integration runtime is required to access your source database backups, check the validity of the backup set, and upload backups to Azure storage account.
   >  
   > If your database backups are already in an Azure storage blob container, you don't need to set up a self-hosted integration runtime.

- For backups that are located on a network share, enter or select the following information:

  | Name | Description |
  | --- | --- |
  | **Source Credentials - Username** | The credential (Windows and SQL authentication) to connect to the source SQL Server instance and validate the backup files. |
  | **Source Credentials - Password** | The credential (Windows and SQL authentication) to connect to the source SQL Server instance and validate the backup files. |
  | **Network share location that contains backups** | The network share location that contains the full and transaction log backup files. Any invalid files or backup files in the network share that don't belong to the valid backup set are automatically ignored during the migration process. |
  | **Windows user account with read access to the network share location** | The Windows credential (username) that has read access to the network share to retrieve the backup files. |
  | **Password** | The Windows credential (password) that has read access to the network share to retrieve the backup files. |
  | **Target database name** | You can modify the target database name during the migration process. |

- For backups that are stored in an Azure storage blob container, enter or select the following information:

  | Name | Description |
  | --- | --- |
  | **Target database name** | You can modify the target database name during the migration process. |
  | **Storage account details** | The resource group, storage account, and container where backup files are located. |
  | **Last Backup File** | The file name of the last backup of the database you're migrating. |

  > [!IMPORTANT]  
  > If loopback check functionality is enabled and the source SQL Server and file share are on the same computer, the source won't be able to access the file share by using the FQDN. To fix this issue, [disable loopback check functionality](/troubleshoot/windows-server/networking/accessing-server-locally-with-fqdn-cname-alias-denied).

- The [Azure SQL migration extension for Azure Data Studio](/azure/dms/migration-using-azure-data-studio) no longer requires specific configurations on your Azure Storage account network settings to migrate your SQL Server databases to Azure. However, depending on your database backup location and desired storage account network settings, there are a few steps needed to ensure your resources can access the Azure Storage account. See the following table for the various migration scenarios and network configurations:

  | Scenario | SMB network share | Azure Storage account container |
  | --- | --- | --- |
  | **Enabled from all networks** | No extra steps | No extra steps |
  | **Enabled from selected virtual networks and IP addresses** | [See 1a](#1a---azure-blob-storage-network-configuration) | [See 2a](#2a---azure-blob-storage-network-configuration-private-endpoint) |
  | **Enabled from selected virtual networks and IP addresses + private endpoint** | [See 1b](#1b---azure-blob-storage-network-configuration) | [See 2b](#2b---azure-blob-storage-network-configuration-private-endpoint) |

### 1a - Azure Blob storage network configuration

If you have your Self-Hosted Integration Runtime (SHIR) installed on an Azure VM, see section [1b - Azure Blob storage network configuration](#1b---azure-blob-storage-network-configuration). If you have your Self-Hosted Integration Runtime (SHIR) installed on your on-premises network, you need to add your client IP address of the hosting machine in your Azure Storage account as so:

:::image type="content" source="media/database-migration-service/storage-networking-details.png" alt-text="Screenshot that shows the storage account network details." lightbox="media/database-migration-service/storage-networking-details.png":::

To apply this specific configuration, connect to the Azure portal from the SHIR machine, open the Azure Storage account configuration, select **Networking**, and then mark the **Add your client IP address** checkbox. Select **Save** to make the change persistent. See section [2a - Azure Blob storage network configuration (Private endpoint)](#2a---azure-blob-storage-network-configuration-private-endpoint) for the remaining steps.

### 1b - Azure Blob storage network configuration

If your SHIR is hosted on an Azure VM, you need to add the virtual network of the VM to the Azure Storage account since the Virtual Machine has a nonpublic IP address that can't be added to the IP address range section.

:::image type="content" source="media/database-migration-service/storage-networking-firewall.png" alt-text="Screenshot that shows the storage account network firewall configuration." lightbox="media/database-migration-service/storage-networking-firewall.png":::

To apply this specific configuration, locate your Azure Storage account, from the **Data storage** panel select **Networking**, then mark the **Add existing virtual network** checkbox. A new panel opens up, select the subscription, virtual network, and subnet of the Azure VM hosting the Integration Runtime. This information can be found on the **Overview** page of the Azure Virtual Machine. The subnet might say **Service endpoint required** if so, select **Enable**. Once everything is ready, save the updates. Refer to section [2a - Azure Blob storage network configuration (Private endpoint)a](#2a---azure-blob-storage-network-configuration-private-endpoint) for the remaining required steps.

### 2a - Azure Blob storage network configuration (Private endpoint)

If your backups are placed directly into an Azure Storage Container, all the above steps are unnecessary since there's no Integration Runtime communicating with the Azure Storage account. However, we still need to ensure that the target SQL Server instance can communicate with the Azure Storage account to restore the backups from the container. To apply this specific configuration, follow the instructions in section [1b - Azure Blob storage network configuration](#1b---azure-blob-storage-network-configuration), specifying the target SQL instance Virtual Network when filling out the "Add existing virtual network" popup.

### 2b - Azure Blob storage network configuration (Private endpoint)

If you have a private endpoint set up on your Azure Storage account, follow the steps outlined in section [2a - Azure Blob storage network configuration (Private endpoint)](#2a---azure-blob-storage-network-configuration-private-endpoint). However, you need to select the subnet of the private endpoint, not just the target SQL Server subnet. Ensure the private endpoint is hosted in the same VNet as the target SQL Server instance. If it isn't, create another private endpoint using the process in the Azure Storage account configuration section.

## Create a Database Migration Service instance

In **Step 6: Azure Database Migration Service** in the Migrate to Azure SQL wizard, create a new instance of Azure Database Migration Service or reuse an existing instance that you created earlier.

> [!NOTE]  
> If you previously created a Database Migration Service instance by using the Azure portal, you can't reuse the instance in the migration wizard in Azure Data Studio. You can reuse an instance only if you created the instance by using Azure Data Studio.

### Use an existing instance of Database Migration Service

To use an existing instance of Database Migration Service:

1. In **Resource group**, select the resource group that contains an existing instance of Database Migration Service.

1. In **Azure Database Migration Service**, select an existing instance of Database Migration Service that's in the selected resource group.

1. Select **Next**.

### Create a new instance of Database Migration Service

To create a new instance of Database Migration Service:

1. In **Resource group**, create a new resource group to contain a new instance of Database Migration Service.

1. Under **Azure Database Migration Service**, select **Create new**.

1. In **Create Azure Database Migration Service**, enter a name for your Database Migration Service instance, and then select **Create**.

1. Under **Set up integration runtime**, complete the following steps:

   1. Select the **Download and install integration runtime** link to open the download link in a web browser. Download the integration runtime, and then install it on a computer that meets the prerequisites to connect to the source SQL Server instance.

      When installation is finished, Microsoft Integration Runtime Configuration Manager automatically opens to begin the registration process.

   1. In the **Authentication key** table, copy one of the authentication keys that are provided in the wizard and paste it in Azure Data Studio. If the authentication key is valid, a green check icon appears in Integration Runtime Configuration Manager. A green check indicates that you can continue to **Register**.

      After you register the self-hosted integration runtime, close Microsoft Integration Runtime Configuration Manager.

      > [!NOTE]  
      > For more information about how to use the self-hosted integration runtime, see [Create and configure a self-hosted integration runtime](/azure/data-factory/create-self-hosted-integration-runtime).

1. In **Create Azure Database Migration Service** in Azure Data Studio, select **Test connection** to validate that the newly created Database Migration Service instance is connected to the newly registered self-hosted integration runtime.

1. Return to the migration wizard in Azure Data Studio.

## Start the database migration

In **Step 7: Summary** in the Migrate to Azure SQL wizard, review the configuration you created, and then select **Start migration** to start the database migration.

## Monitor the database migration

1. In Azure Data Studio, in the server menu under **General**, select **Azure SQL Migration** to go to the dashboard for your Azure SQL migrations.

   Under **Database migration status**, you can track migrations that are in progress, completed, and failed (if any), or you can view all database migrations.

   :::image type="content" source="media/database-migration-service/monitor-migration-dashboard.png" alt-text="Screenshot of monitor migration dashboard.":::

1. Select **Database migrations in progress** to view active migrations.

   To get more information about a specific migration, select the database name.

   The migration details pane displays the backup files and their corresponding status:

   | Status | Description |
   | --- | --- |
   | **Arrived** | The backup file arrived in the source backup location and was validated. |
   | **Uploading** | The integration runtime is uploading the backup file to Azure storage. |
   | **Uploaded** | The backup file has been uploaded to Azure storage. |
   | **Restoring** | The service is restoring the backup file to SQL Server on Azure Virtual Machines. |
   | **Restored** | The backup file was successfully restored on SQL Server on Azure Virtual Machines. |
   | **Canceled** | The migration process was canceled. |
   | **Ignored** | The backup file was ignored because it doesn't belong to a valid database backup chain. |

After all database backups are restored on the instance of SQL Server on Azure Virtual Machines, an automatic migration cutover is initiated by Database Migration Service to ensure that the migrated database is ready to use. The migration status changes from **In progress** to **Succeeded**.

## [Online with Azure DMS](#tab/online-with-extension)

This article describes an online migration from an on-premises SQL Server to a SQL Server on Azure Virtual Machine.

## Launch the Migrate to Azure SQL wizard in Azure Data Studio

1. Open Azure Data Studio and select the server icon to connect to your on-premises SQL Server (or SQL Server on Azure Virtual Machine).
1. On the server connection, right-click and select **Manage**.
1. On the server's home page, Select **Azure SQL Migration** extension.
1. On the Azure SQL Migration dashboard, select **Migrate to Azure SQL** to launch the migration wizard.

   :::image type="content" source="media/database-migration-service/launch-migrate-to-azure-sql-wizard.png" alt-text="Screenshot of Launch Migrate to Azure SQL wizard." lightbox="media/database-migration-service/launch-migrate-to-azure-sql-wizard.png":::

1. In the first step of the migration wizard, link your existing or new Azure account to Azure Data Studio.

## Run database assessment, collect performance data and get Azure recommendation

1. Select the databases to run assessment and select **Next**.
1. Select SQL Server on Azure Virtual Machine as the target.

   :::image type="content" source="media/database-migration-service/assessment-complete-target-selection.png" alt-text="Screenshot of assessment confirmation.":::

1. Select **View/Select** to view details of the assessment results for your databases, select the databases to migrate, and select **OK**.
1. Select **Get Azure recommendation**.
1. Pick the **Collect performance data now** option, enter a path for performance logs to be collected, and select **Start**.
1. Azure Data Studio will now collect performance data until you either stop the collection, select **Next** in the wizard or close Azure Data Studio.
1. After 10 minutes you see a recommended configuration for your Azure SQL VM. You can also select the **Refresh recommendation** link after the initial 10 minutes to refresh the recommendation with the extra data collected.
1. In the **SQL Server on Azure Virtual Machine** box, select **View details** for more information about your recommendation.
1. Close the view details box, and select **Next**.

## Configure migration settings

1. Specify your **target SQL Server on Azure Virtual Machine** by selecting your subscription, location, resource group from the corresponding dropdown lists and then select **Next**.
1. Select **Online migration** as the migration mode.

   > [!NOTE]  
   > In the online migration mode, the source SQL Server database can be used for read and write activity while database backups are continuously restored on the target SQL Server on Azure Virtual Machine. Application downtime is limited to duration for the cutover at the end of migration.

1. In step 5, select the location of your database backups. Your database backups can either be located on an on-premises network share or in an Azure storage blob container.

   > [!NOTE]  
   > If your database backups are provided in an on-premises network share, DMS will require you to setup self-hosted integration runtime in the next step of the wizard. Self-hosted integration runtime is required to access your source database backups, check the validity of the backup set and upload them to Azure storage account.<br/> If your database backups are already on an Azure storage blob container, you don't need to setup self-hosted integration runtime.

   - For backups located on a network share provide the below details of your source SQL Server, source backup location, target database name and Azure storage account for the backup files to be uploaded to.

     | Field | Description |
     | --- | --- |
     | **Source Credentials - Username** | The credential (Windows / SQL authentication) to connect to the source SQL Server instance and validate the backup files. |
     | **Source Credentials - Password** | The credential (Windows / SQL authentication) to connect to the source SQL Server instance and validate the backup files. |
     | **Network share location that contains backups** | The network share location that contains the full and transaction log backup files. Any invalid files or backups files in the network share that don't belong to the valid backup set will be automatically ignored during the migration process. |
     | **Windows user account with read access to the network share location** | The Windows credential (username) that has read access to the network share to retrieve the backup files. |
     | **Password** | The Windows credential (password) that has read access to the network share to retrieve the backup files. |
     | **Target database name** | The target database name can be modified if you wish to change the database name on the target during the migration process. |

   - For backups stored in an Azure storage blob container, specify the below details of the Target database name, resource group, Azure storage account, Blob container from the corresponding dropdown lists.

     | Field | Description |
     | --- | --- |
     | **Target database name** | The target database name can be modified if you wish to change the database name on the target during the migration process. |
     | **Storage account details** | The resource group, storage account and container where backup files are located. |

1. Select **Next** to continue.

   > [!IMPORTANT]  
   > If loopback check functionality is enabled and the source SQL Server and file share are on the same computer, then source won't be able to access the files hare using FQDN. To fix this issue, disable loopback check functionality using the instructions [here](/troubleshoot/windows-server/networking/accessing-server-locally-with-fqdn-cname-alias-denied)

   - The [Azure SQL migration extension for Azure Data Studio](/azure/dms/migration-using-azure-data-studio) no longer requires specific configurations on your Azure Storage account network settings to migrate your SQL Server databases to Azure. However, depending on your database backup location and desired storage account network settings, there are a few steps needed to ensure your resources can access the Azure Storage account. See the following table for the various migration scenarios and network configurations:

     | Scenario | SMB network share | Azure Storage account container |
     | --- | --- | --- |
     | **Enabled from all networks** | No extra steps | No extra steps |
     | **Enabled from selected virtual networks and IP addresses** | [See 1a](#1a---azure-blob-storage-network-configuration) | [See 2a](#2a---azure-blob-storage-network-configuration-private-endpoint) |
     | **Enabled from selected virtual networks and IP addresses + private endpoint** | [See 1b](#1b---azure-blob-storage-network-configuration) | [See 2b](#2b---azure-blob-storage-network-configuration-private-endpoint) |

### 1a - Azure Blob storage network configuration

If you have your Self-Hosted Integration Runtime (SHIR) installed on an Azure VM, see section [1b - Azure Blob storage network configuration](#1b---azure-blob-storage-network-configuration). If you have your Self-Hosted Integration Runtime (SHIR) installed on your on-premises network, you need to add your client IP address of the hosting machine in your Azure Storage account as so:

:::image type="content" source="media/database-migration-service/storage-networking-details.png" alt-text="Screenshot that shows the storage account network details." lightbox="media/database-migration-service/storage-networking-details.png":::

To apply this specific configuration, connect to the Azure portal from the SHIR machine, open the Azure Storage account configuration, select **Networking**, and then mark the **Add your client IP address** checkbox. Select **Save** to make the change persistent. See section [2a - Azure Blob storage network configuration (Private endpoint)](#2a---azure-blob-storage-network-configuration-private-endpoint) for the remaining steps.

### 1b - Azure Blob storage network configuration

If your SHIR is hosted on an Azure VM, you need to add the virtual network of the VM to the Azure Storage account since the Virtual Machine has a nonpublic IP address that can't be added to the IP address range section.

:::image type="content" source="media/database-migration-service/storage-networking-firewall.png" alt-text="Screenshot that shows the storage account network firewall configuration." lightbox="media/database-migration-service/storage-networking-firewall.png":::

To apply this specific configuration, locate your Azure Storage account, from the **Data storage** panel select **Networking**, then mark the **Add existing virtual network** checkbox. A new panel opens up, select the subscription, virtual network, and subnet of the Azure VM hosting the Integration Runtime. This information can be found on the **Overview** page of the Azure Virtual Machine. The subnet might say **Service endpoint required** if so, select **Enable**. Once everything is ready, save the updates. Refer to section [2a - Azure Blob storage network configuration (Private endpoint)a](#2a---azure-blob-storage-network-configuration-private-endpoint) for the remaining required steps.

### 2a - Azure Blob storage network configuration (Private endpoint)

If your backups are placed directly into an Azure Storage Container, all the above steps are unnecessary since there's no Integration Runtime communicating with the Azure Storage account. However, we still need to ensure that the target SQL Server instance can communicate with the Azure Storage account to restore the backups from the container. To apply this specific configuration, follow the instructions in section [1b - Azure Blob storage network configuration](#1b---azure-blob-storage-network-configuration), specifying the target SQL instance Virtual Network when filling out the "Add existing virtual network" popup.

### 2b - Azure Blob storage network configuration (Private endpoint)

If you have a private endpoint set up on your Azure Storage account, follow the steps outlined in section [2a - Azure Blob storage network configuration (Private endpoint)](#2a---azure-blob-storage-network-configuration-private-endpoint). However, you need to select the subnet of the private endpoint, not just the target SQL Server subnet. Ensure the private endpoint is hosted in the same VNet as the target SQL Server instance. If it isn't, create another private endpoint using the process in the Azure Storage account configuration section.

## Create Azure Database Migration Service

1. Create a new Azure Database Migration Service or reuse an existing Service that you previously created.

   > [!NOTE]  
   > If you had previously created DMS using the Azure Portal, you can't reuse it in the migration wizard in Azure Data Studio. Only DMS created previously using Azure Data Studio can be reused.

1. Select the **Resource group** where you have an existing DMS or need to create a new one. The **Azure Database Migration Service** dropdown lists any existing DMS in the selected resource group.
1. To reuse an existing DMS, select it from the dropdown list and the status of the self-hosted integration runtime will be displayed at the bottom of the page.
1. To create a new DMS, select **Create new**.
1. On the **Create Azure Database Migration Service**, screen provide the name for your DMS and select **Create**.
1. After successful creation of DMS, you'll be provided with details to **Setup integration runtime**.
1. Select **Download and install integration runtime** to open the download link in a web browser. Complete the download. Install the integration runtime on a machine that meets the prerequisites of connecting to source SQL Server and the location containing the source backup.
1. After the installation is complete, the **Microsoft Integration Runtime Configuration Manager** will automatically launch to begin the registration process.
1. Copy and paste one of the authentication keys provided in the wizard screen in Azure Data Studio. If the authentication key is valid, a green check icon is displayed in the Integration Runtime Configuration Manager indicating that you can continue to **Register**.
1. After successfully completing the registration of self-hosted integration runtime, close the **Microsoft Integration Runtime Configuration Manager** and switch back to the migration wizard in Azure Data Studio.
1. Select **Test connection** in the **Create Azure Database Migration Service** screen in Azure Data Studio to validate that the newly created DMS is connected to the newly registered self-hosted integration runtime and select **Done**.

   :::image type="content" source="media/database-migration-service/test-connection-integration-runtime-complete.png" alt-text="Screenshot of Test connection integration runtime.":::

1. Review the summary and select **Done** to start the database migration.

## Monitor your migration

1. On the **Database Migration Status**, you can track the migrations in progress, migrations completed, and migrations failed (if any).

   :::image type="content" source="media/database-migration-service/monitor-migration-dashboard.png" alt-text="Screenshot of monitor migration dashboard.":::

1. Select **Database migrations in progress** to view ongoing migrations and get further details by selecting the database name.
1. The migration details page displays the backup files and the corresponding status:

   | Status | Description |
   | --- | --- |
   | **Arrived** | Backup file arrived in the source backup location and validated |
   | **Uploading** | Integration runtime is currently uploading the backup file to Azure storage |
   | **Uploaded** | Backup file is uploaded to Azure storage |
   | **Restoring** | Azure Database Migration Service is currently restoring the backup file to SQL Server on Azure Virtual Machine |
   | **Restored** | Backup file is successfully restored on SQL Server on Azure Virtual Machine |
   | **Canceled** | Migration process was canceled |
   | **Ignored** | Backup file was ignored as it doesn't belong to a valid database backup chain |

   :::image type="content" source="media/database-migration-service/online-to-vm-migration-status-detailed.png" alt-text="Screenshot of online vm backup restore details." lightbox="media/database-migration-service/online-to-vm-migration-status-detailed.png":::

## Complete migration cutover

The final step of the tutorial is to complete the migration cutover. The completion ensures the migrated database in SQL Server on Azure Virtual Machine is ready for use. Downtime is required for applications that connect to the database and the timing of the cutover needs to be carefully planned with business or application stakeholders.

To complete the cutover:

1. Stop all incoming transactions to the source database.
1. Make application configuration changes to point to the target database in SQL Server on Azure Virtual Machines.
1. Take a final log backup of the source database in the backup location specified
1. Put the source database in read-only mode. Therefore, users can read data from the database but not modify it.
1. Ensure all database backups have the status *Restored* in the monitoring details page.
1. Select *Complete cutover* in the monitoring details page.

During the cutover process, the migration status changes from *in progress* to *completing*. The migration status changes to *succeeded* when the cutover process is completed. The database migration is successful and that the migrated database is ready for use.

---

## Limitations

-If migrating a single database, the database backups must be placed in a flat-file structure inside a database folder (including container root folder), and the folders can't be nested, as it isn't supported.

-If migrating multiple databases using the same Azure Blob Storage container, you must place backup files for different databases in separate folders inside the container.

-Overwriting existing databases using DMS in your target SQL Server on Azure Virtual Machine isn't supported.

-Configuring high availability and disaster recovery on your target to match source topology isn't supported by DMS.

The following server objects aren't supported:

- SQL Server Agent jobs
- Credentials
- SSIS packages
- Server audit

-You can't use an existing self-hosted integration runtime created from Azure Data Factory for database migrations with DMS. Initially, the self-hosted integration runtime should be created using the Azure SQL migration extension in Azure Data Studio and can be reused for further database migrations.

-VM with SQL Server 2008 and below as target versions aren't supported when migrating to SQL Server on Azure Virtual Machines.

-If you're using a VM with SQL Server 2012 or SQL Server 2014, you need to store your source database backup files on an Azure Storage Blob Container instead of using the network share option. Store the backup files as page blobs since block blobs are only supported in SQL 2016 and after.

-You must make sure the SQL IaaS Agent Extension in the target Azure Virtual Machine is in **Full mode** instead of Lightweight mode.

-Migration to Azure SQL VM using DMS uses SQL IaaS agent internally. And SQL IaaS Agent Extension only supports management of Default Server Instance or Single Named Instance.

<!--The number of databases you can migrate to a SQL server Azure Virtual Machine depends on the hardware specification and workload, but there's no enforced limit.-->
-You can migrate a maximum of 100 databases to the same Azure SQL Server Virtual Machine as the target using one or more migrations simultaneously. Moreover, once a migration(s) with 100 databases finishes, wait for at least 30 minutes before starting a new migration to the same Azure SQL Server Virtual Machine as the Target. Also, every migration operation (start migration, cutover) for each database takes a few minutes sequentially. For example, to migrate 100 databases, it might take approx. 200 (2 x 100) minutes to create the migration queues and approximately 100 (1 x 100) minutes to cutover all 100 databases (excluding backup and restore timing). Therefore, the migration becomes slower as the number of databases increases. You should either schedule a longer migration window in advance based on rigorous migration testing, or partitioning large number of databases into batches when migrating them to a SQL server Azure VM.

-Apart from configuring the Networking/Firewall of your Azure Storage Account to allow your VM to access backup files. You also need to configure the Networking/Firewall of your SQL Server on Azure VM to allow outbound connection to your storage account.

-You need to keep the target SQL Server on Azure VM power **ON** while the SQL Migration is in progress. Also, when creating a new migration, failover or cancel the migration.

### Possible error messages

#### Login failed for user 'NT Service\SQLIaaSExtensionQuery

**Error**: `Login failed for user 'NT Service\SQLIaaSExtensionQuery`

**Reason**: SQL Server instance is in single-user mode. One possible reason is the target SQL Server on Azure VM is in upgrade mode.

**Solution**: Wait for the target SQL Server on Azure VM to exit the upgrade mode, and start migration again.

#### Failed to create restore job

**Error**: `Ext_RestoreSettingsError, message: Failed to create restore job.;Cannot create file 'F:\data\XXX.mdf' because it already exists.`

**Solution**: Connect to the target SQL Server on Azure VM and delete the `XXX.mdf` file. Then, start migration again.

## Related content

- [Migrate a SQL Server database to SQL Server on a virtual machine](/azure/azure-sql/virtual-machines/windows/migrate-to-vm-from-sql-server)
- [What is SQL Server on Windows Azure Virtual Machines?](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview)
- [Connect to a SQL Server virtual machine on Azure](/azure/azure-sql/virtual-machines/windows/ways-to-connect-to-sql)
- [Known issues, limitations, and troubleshooting](/azure/dms/known-issues-azure-sql-migration-azure-data-studio)
- [Migrate a database to SQL Server on Azure Virtual Machines by using the T-SQL RESTORE command](/azure/azure-sql/migration-guides/virtual-machines/sql-server-to-sql-on-azure-vm-individual-databases-guide)
