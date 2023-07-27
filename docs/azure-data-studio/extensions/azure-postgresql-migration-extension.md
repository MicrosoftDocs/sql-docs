---
title: Azure PostgreSQL migration extension
description: Learn how to install the Azure Data Studio Azure Database for PostgreSQL - Flexible Server extension. This extension helps you choose the best Azure Database for PostgreSQL migration path.
author: apduvuri
ms.author: adityaduvuri
ms.reviewer: maghan, randolphwest
ms.date: 02/28/2023
ms.service: azure-data-studio
ms.topic: conceptual
---

# Azure PostgreSQL migration (assessment) extension Preview

[!INCLUDE [database-migration-services-ads](../../includes/database-migration-services-postgresql-ads.md)]

The Azure PostgreSQL migration extension in Azure Data Studio helps you assess your PostgreSQL workload for migrating to Azure Database for PostgreSQL - Flexible Server. The extension identifies an appropriate PostgreSQL target with rightsizing recommendations for migrations.

## Prerequisites

- [Azure Data Studio (1.40 and higher)](../download-azure-data-studio.md).
- PostgreSQL Server instance running 9.3 or higher.
- PostgreSQL users should have CONNECT and SELECT privileges on the databases of the instance.

## Install Azure Data Studio extension

Follow these steps to install the **Azure PostgreSQL migration** extension in Azure Data Studio.

1. Open the extensions manager in Azure Data Studio. Select the extension icon or **Extensions** in the **View** menu.

1. Type *Postgresql migration* in the search bar.

1. Select the **Azure PostgreSQL migration** extension and view its details.

1. Select **Install**.
    1. Once installed the **PostgreSQL** extension is also installed.
    1. Once installed **.NET 6** is also installed.

:::image type="content" source="media/azure-postgresql-migration-extension/search-extension.png" alt-text="Screenshot to show a search of the extension." lightbox="media/azure-postgresql-migration-extension/search-extension.png":::

## Connect to a PostgreSQL instance

Once the assessment extension is installed, the next step is to [connect to your PostgreSQL Server instance](postgres-extension.md), collect metadata information from the PostgreSQL Server instance, and generate an assessment report.

1. Go to the connections icon in the menu bar.

   :::image type="content" source="media/azure-postgresql-migration-extension/new-connection.png" alt-text="Screenshot of the new connection menu bar." lightbox="media/azure-postgresql-migration-extension/new-connection.png":::

1. Fill out the fields in the **Connection Details**.
   1. In the **Connection type** field, select **PostgreSQL**.
   1. In the **Server name** field, type in your *PostgreSQL server name*.
   1. In the **Authentication type**, select **Password**.
   1. In the **User name** field, provide your database username
   1. in the **Password** field, provide your database password.
   1. Then select **Advanced...**
      1. Under the Server section, provide the port number.
      1. Then select **OK**.

   :::image type="content" source="media/azure-postgresql-migration-extension/connection-details.png" alt-text="Screenshot of the connection details." lightbox="media/azure-postgresql-migration-extension/connection-details.png":::

1. Select **Connect**.

1. Now, a new connection appears in the connection details menu.

1. Right-click on the PostgreSQL connection and select **Manage**.

   :::image type="content" source="media/azure-postgresql-migration-extension/manage-database-connection.png" alt-text="Screenshot showing the Manage database screen." lightbox="media/azure-postgresql-migration-extension/manage-database-connection.png":::

## Run the assessment

Once you've connected to your PostgreSQL instance in Azure Data Studio, you can begin to run the assessment.

Under the General section, select **Azure PostgreSQL Migration**, then select **Run new assessment**.

   :::image type="content" source="media/azure-postgresql-migration-extension/start-assessment.png" alt-text="Screenshot showing the Migration Assessment database screen." lightbox="media/azure-postgresql-migration-extension/start-assessment.png":::

There are three steps to complete the assessment.

- **Database(s) for assessment**
- **Assessment Parameters**
- **View Assessment Results**

### 1. Database(s) for assessment

Select the database(s) you want to assess for migration for Azure Database for PostgreSQL - Flexible Server and then select **Next**.

:::image type="content" source="media/azure-postgresql-migration-extension/view-databases.png" alt-text="Screenshot of view databases." lightbox="media/azure-postgresql-migration-extension/view-databases.png":::

### 2. Assessment Parameters

In the Assessments parameters, users can choose the target version of the Azure Database for PostgreSQL - Flexible Server for assessing the source PostgreSQL instance with the selected target version of the Azure Database for PostgreSQL - Flexible Server. The target version user can select will be always greater than or equal to the source PostgreSQL version. For example, if the source PostgreSQL instance version is 13 then the target version shown would be 13, 14 and 15.

The SKU recommendation feature allows you to collect performance data from your source PostgreSQL instances hosting your databases and
recommends the rightsized Azure Database for PostgreSQL - Flexible Server SKU based on the data collected. The feature provides compute level and data size recommendations.

Choose how you want to provide SKU recommendations for the target audience. This step requires performance data of a PostgreSQL server instance.

There are two options to collect performance data to receive the target recommendation for the databases you want to migrate.

- Automatically collect performance data
- Enter performance data parameters

#### Automatically collect performance data

> [!NOTE]  
> Before you select your databases, you need to execute privileges for automatic collection for SKU recommendation.
>  
> The user needs to execute privilege on the `pg_read_file() function`.
>  
> ```sql
> GRANT EXECUTE ON FUNCTION pg_read_file(text) TO <<username>>;
> ```
>  
> The user should be granted the `role pg_read_server_files`.
>  
> ```sql
> GRANT pg_read_server_files TO <<username>>;
> ```

If your environment supports **Automatically collect performance data**, then this is the [default option](#known-issues-and-limitations).

Select automatic performance data collection to receive the target recommendations for the databases you want to migrate.

Fill out the fields in the SKU recommendation parameters as follows.

- **Time duration** - enter the time you want to run the data collection.

   > [!NOTE]  
   > It is recommended that you collect the assessment data during peak workload times. Data collection duration should run for 24 hours because it provides time to collect data with higher confidence. The assessment wizard needs to be open while the data collection is in progress.

- **Scale factor** - Enter values **0.2-2**, to expand during peak performance times.

   > [!NOTE]  
   > The scale factor during the assessment is a buffer applied on top of current utilization data for PostgreSQL (vCores, memory, and storage). The scale factor accounts for seasonal usage, short performance history, and increases in future use.

- **Percentile utilization** - The percentile value of the performance sample set to be considered for sizing the Azure target.

Once you've provided your values, select **Assess**.

:::image type="content" source="media/azure-postgresql-migration-extension/automatic-collect-data.png" alt-text="Screenshot of automatically collecting data." lightbox="media/azure-postgresql-migration-extension/automatic-collect-data.png":::

#### Enter performance data parameters

Based on your environment, you might have to [provide the data manually](#known-issues-and-limitations) to perform the assessment. As such, you can use the **Enter Performance Data Parameter** option to enter values needed to provide an assessment manually.

Fill out the fields in the performance parameters as follows.

- **vCores** – Number of logical cores available in the server.
- **Memory (GB)** – Total memory available in the server.
- **Storage (GB)** – Total storage used by the PostgreSQL Server instance.
- **IOPS** – Input/Output operations per second by the PostgreSQL Server instance.

Fill out the fields in the recommendation parameters as follows.

- **Scale factor** - Enter values **0.2-2**, to expand during peak performance times.

   > [!NOTE]  
   > The scale factor during the assessment is a buffer applied on top of current utilization data for PostgreSQL (vCores, Memory, and storage). The scale factor accounts for seasonal usage, short performance history, and increases in future use.

Once you've filled in your parameters, select **Assess**.

:::image type="content" source="media/azure-postgresql-migration-extension/enter-data-parameters.png" alt-text="Screenshot of entering performance data." lightbox="media/azure-postgresql-migration-extension/enter-data-parameters.png":::

For more information about SKU recommendations, view [SKU recommendations](#sku-recommendations).

### 3. View Assessment Results

Once the assessment is complete, a consolidated output is generated.

- The cards at the top represent the recommended SKU in Azure.
  - **Target Platform** – Currently, the assessment is performed and supported on Azure Database for PostgreSQL - Flexible Server.
  - **Recommended Configuration** – Based on the performance metrics, SKU available in Azure Database for PostgreSQL - Flexible Server is recommended. For more information about SKU recommendations, see [SKU recommendations](#sku-recommendations).
  - **Data collection status** – Shows the number of cycles completed and the data collection status.
    - There are four statuses: **In Progress**, **Completed**, **Stopped**, **Not Applicable**  (only for **Enter performance data parameters** data collection).

      > [!NOTE]  
      > In the **Data collection status** card, a user can start or stop the collection at any time.

:::image type="content" source="media/azure-postgresql-migration-extension/configuration-cards-02.png" alt-text="Screenshot of configuration cards." lightbox="media/azure-postgresql-migration-extension/configuration-cards.png":::

Users can select the Instance name that shows the PostgreSQL instance's summary and migration readiness. Users can go through different server parameters and features, understand the use of the parameter, and get to know the recommendation for resolving the warnings.

Users can select the value next to the [Migration Readiness state](#migration-readiness-state) to determine which database is under what status.

Users can select the respective databases, understand the blockers and warnings and go through the [Migration Readiness](#migration-readiness-state) summary if the database is in the Not Ready, Ready with conditions, or Ready state for migration into Azure.

Users can save the assessment report on their machine for offline viewing by selecting the **Save Assessment** action.

:::image type="content" source="media/azure-postgresql-migration-extension/save-assessment-02.png" alt-text="Screenshot to show how to save the assessment." lightbox="media/azure-postgresql-migration-extension/save-assessment.png":::

#### Migration Readiness state

- **Not Ready** - The PostgreSQL instance (DBs) can't be migrated to Azure. For example, if an on-premises server's disk stores more than 64 TB, Azure can't host the server. Follow the remediation guidance to fix the problem before migration.
- **Ready with Conditions** - The PostgreSQL instance (DBs) can be migrated to Azure by following the recommendations provided in ADS. For example, Azure PostgreSQL - Flexible Server doesn't support the latest collation version running on-premises instances. You must be careful before you migrate these instances to Azure. To fix any readiness problems, follow the remediation guidance. In this example, the user needs to rebuild the index.
- **Ready** - The PostgreSQL instance (DBs) can be migrated to Azure without any changes.

## SKU recommendations

The SKU recommendation feature allows you to collect performance data from your source PostgreSQL instances hosting your databases and recommends the rightsized Azure Database for PostgreSQL - Flexible Server based on the data collected. The feature provides compute level and data size recommendations.

The SKU recommendation evaluates various performance metrics, such as vCores, Memory, IOPS, and storage. Based on the usage and configuration data, the recommender provides the suitable target and the appropriate service tier.

### Review confidence rating

The recommended Configuration provides a confidence rating based on the data collection and a 24-hour run time. Rating is from one star (lowest) to five stars (highest).

The confidence rating helps you estimate the reliability of rightsized recommendation in the assessment.

Confidence ratings are as follows for a 24-hour data collection run time.

| Data point availability | Approximate run time | Confidence rating |
| --- | --- | --- |
| 1%-20% | 10 minutes - 5 hours | 1 star |
| 21%-40% | > 5 hours - 10 hours | 2 stars |
| 41%-60% | > 10 hours - 14 hours | 3 stars |
| 61%-80% | > 14 hours - 19 hours | 4 stars |
| 81%-100% | > 19 hours - 24 hours | 5 stars |

### Recommended Configuration

Once the data collection process is complete, you can select the **View Details** option in the **Recommended Details** card at the top to view the Recommended details screen. You can see the **Recommendation reason** and **Source properties**.

The recommendation reasons list the CPU, memory, storage, and IOPs requirements, comparing it with the Azure Database for PostgreSQL - Flexible Server target.

The source properties list the used and actual performance parameters. The *Used* information explains the usage by the source PostgreSQL instance. The *Actual* information explains the total utilization that can be used by the source PostgreSQL instance.

:::image type="content" source="media/azure-postgresql-migration-extension/recommendation-details.png" alt-text="Screenshot of View details screen." lightbox="media/azure-postgresql-migration-extension/recommendation-details.png":::

## Change assessment path

If you want to save your assessment and performance data in a different path, you can edit the assessment path under the extension settings.

1. Go to the extension marketplace and search for *Azure PostgreSQL migration*.
1. Select the **Manage** icon and select extensions settings.
1. Provide the new assessment path under **PostgreSQL Assessment: Assessment Path**.

:::image type="content" source="media/azure-postgresql-migration-extension/postgresql-migration-extension-settings.png" alt-text="Screenshot of extension settings." lightbox="media/azure-postgresql-migration-extension/postgresql-migration-extension-settings.png":::

## Troubleshoot

To troubleshoot any Azure PostgreSQL migration extension issue, you should find out the details about the error and warnings from the logs generated.

### Logs

The extension stores errors, warnings, and other diagnostic logs in the default log directory:

- Windows - `C:\Users\<username>\.postgresmigration\logs\`
- Linux - `~/.postgresmigration/logs`
- macOS - `/Users/<username>/.postgresmigration/logs`

> [!NOTE]  
> By default, the extension stores the last seven log files.

For more information about troubleshooting issues, visit [Troubleshoot Azure PostgreSQL migration extension errors](azure-postgresql-migration-extension-troubleshoot.md).

## Known issues and limitations

- Automatic collection for SKU recommendation isn't supported for any PostgreSQL PaaS services.
- Automatic collections are only applicable for Linux.
- Automatic collections are only applicable for PostgreSQL versions 11 and higher.

## Get help from Microsoft support

You can raise a support request to get Microsoft support assistance if you encounter issues or errors with your database migrations using the PostgreSQL migration extension.

Select the **New support request** button in the upper section of the extension. It automatically takes you to the Azure portal, where you can fill in the details and submit a support request.

You can submit ideas/suggestions for improvement, and other feedback, including bugs, in the [Azure Community forum—Azure Database Migration Service](https://feedback.azure.com/d365community/forum/2dd7eb75-ef24-ec11-b6e6-000d3a4f0da0).

> [!NOTE]  
> You can also use the **Feedback** button or [email](mailto:epgsupport@microsoft.com) the product group if you have any suggestions or feedback to improve the extension.

## Next steps

- [Troubleshoot Azure PostgreSQL migration issues](azure-postgresql-migration-extension-troubleshoot.md)
- [PostgreSQL extension](postgres-extension.md)
- [Azure Data Studio extensions](add-extensions.md)
- [Azure Data Studio release notes](../release-notes-azure-data-studio.md)
- [Database migrations using Azure Data Studio](/azure/dms/migration-using-azure-data-studio)
- [Download Azure Data Studio](../download-azure-data-studio.md)
- [Azure SQL Migration extension](azure-sql-migration-extension.md)
