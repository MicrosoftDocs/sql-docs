---
title: Azure PostgreSQL migration extension
description: Learn how to install the Azure Data Studio Azure Database for PostgreSQL flexible server extension. This extension helps guide you choose the best Azure SQL database and Azure Database for PostgreSQL migration path.
author: apduvuri
ms.author: adityaduvuri
ms.reviewer: maghan, randolphwest
ms.date: 01/31/2023
ms.service: azure-data-studio
ms.topic: conceptual
---

# Azure PostgreSQL migration (assessment) extension Preview

[!INCLUDE [database-migration-services-ads](../../includes/database-migration-services-postgresql-ads.md)]

The Azure PostgreSQL migration extension in Azure Data Studio helps you assess your PostgreSQL workload for migrating to Azure Database for PostgreSQL flexible server. The extension identifies an appropriate PostgreSQL target with right-sizing recommendations and the complexity of the migration.

## Prerequisites

- [Azure Data Studio version](../download-azure-data-studio.md).
- PostgreSQL Server instance running 9.3 or higher.

## Install Azure Data Studio extension

Follow these steps to install the **Azure PostgreSQL migration** extension in Azure Data Studio.

1. Open the extensions manager in Azure Data Studio. Select the extension icon or **Extensions** in the **View** menu.

1. Type *PostgreSQL Migration* in the search bar.

1. Select the **Azure PostgreSQL Migration** extension and view its details.

1. Select **Install**.
    1. Once installed the **PostgreSQL** extension is also installed.
    1. Once installed **.NET 6** is also installed.

:::image type="content" source="media/azure-postgresql-migration-extension/search-extension.png" alt-text="Scheenshot to show a search of the extension.":::

## Connect to a PostgreSQL instance

Once the assessment extension is installed, the next step is to [connect to your PostgreSQL Server instance](postgres-extension.md), collect metadata information from the PostgreSQL Server instance, and generate an assessment report.

1. Go to the connections icon in the menu bar.

   :::image type="content" source="media/azure-postgresql-migration-extension/new-connection.png" alt-text="connections menu bar":::

1. Fill out the fields in the **Connection Details**.
   1. In the **Connection type** field, select **PostgreSQL**.
   1. In the **Server name** field, type in your *PostgreSQL server name*.
   1. In the **Authentication type**, select **Password**.
   1. In the **User Id** field, provide your database username
   1. in the **Password** field, provide your database password.
   1. Then select **Advanced...**
      1. Under the Server section, provide the port number.
      1. Then select **OK**.

   :::image type="content" source="media/azure-postgresql-migration-extension/connection-details.png" alt-text="connection details":::

1. Select **Connect**.

1. Now, a new connection appears in the connection details menu.

1. Right-click on the PostgreSQL connection and select **Manage**.

   :::image type="content" source="media/azure-postgresql-migration-extension/manage-database-connection.png" alt-text="Screenshot showing the Manage database screen.":::

## Run the assessment

Once you've connected to your PostgreSQL instance in Azure Data Studio, you can begin to run the assessment.

Under the General section, select **Azure PostgreSQL Migration**, then select **Assess Databases(s)**.

   :::image type="content" source="media/azure-postgresql-migration-extension/start-assessment.png" alt-text="Screenshot showing the Migration Assessment database screen.":::

There are three steps to complete the assessment.

- **Databases(s) for assessment**
- **Assessment Parameters**
- **View Assessment Results**

> [!NOTE]
> Before you select your databases, you need to execute privileges for automatic collection for SKU recommendation.
>
> The user needs to execute privilege on the pg_read_file() function.
>
> ```sql
> GRANT EXECUTE ON FUNCTION pg_read_file(text) TO <<username>>;
> ```
>
> The user should be granted the role pg_read_server_files.
>
> ```sql
> GRANT pg_read_server_files TO <<username>>;
> ```

### 1. Databases(s) for assessment

Select the database(s) you want to assess for migration for Azure Database for PostgreSQL - Flexible Server.

:::image type="content" source="media/azure-postgresql-migration-extension/view-databases.png" alt-text="Screenshot of view databases.":::

### 2. Assessment Parameters

The SKU recommendation feature allows you to collect performance data from your source PostgreSQL instances hosting your databases and
recommends the right-sized Azure database for PostgreSQL – Flexible Server SKU based on the data collected. The feature provides pricing tier,
compute level, and data size recommendations.

Choose how you want to provide SKU recommendations for the target audience. This step requires performance data of a PostgreSQL server instance.

You can automatically collect data to receive the target recommendation for the databases you want to migrate or provide the parameters manually depending on your environment's setup.

#### Automatically collect data

Select automatic performance data collection to receive the target recommendations for the databases you want to migrate.

Fill out the fields in the SKU recommendation parameters as follows.

- Time duration - enter the amount of time you want to run the assessment.

   > [!NOTE]
   > It is recommended that you perform the assessment data collection during peak workload time and data collection duration should be 24 hours. The assessment wizard needs to be open while the data collection is in progress.

- Scale factor - Enter values **1-5**, to expand during peak performance times.
- Percentage Utilization - The percentile value of the performance sample set to be considered for sizing the Azure target.

Once you've filled in your parameters, select **Assess**.

:::image type="content" source="media/azure-postgresql-migration-extension/automatic-collect-perf-data.png" alt-text="Screenshot of automatically collecting data":::

#### Enter Performance Data parameters

Based on your environment, you might need help with collecting the data required to perform the assessment automatically. As such, you can use the **Enter Performance Data Parameter** option to enter values needed to provide an assessment manually.

Fill out the fields in the SKU performance parameters as follows.

- **vCores** – Number of logical cores available in the server.
- **Memory (in GB)** – Total memory available in the server.
- **Storage (in GB)** – Total storage used by the PostgreSQL Server instance.
- **IOPS** – Input/Output operations per second by the PostgreSQL Server instance.

Fill out the fields in the SKU recommendation parameters as follows.

- **Scale factor** - The scale factor during the assessment is a buffer applied on top of current utilization data for PostgreSQL (vCores, Memory, and storage). The scale factor accounts for seasonal usage, short performance history, and increases in future use.
- **Percentage Utilization** - The percentile value of the performance sample set to be considered for sizing the Azure target.

Once you've filled in your parameters, select **Assess**.

:::image type="content" source="media/azure-postgresql-migration-extension/enter-perf-data-parameters.png" alt-text="Screenshot of entering performance data":::

For more information about SKU recommendations, view [SKU recommendations](#sku-recommendations).

### 3. View Assessment Results

Once the assessment is complete, a consolidated output is generated.

- The cards at the top represent the recommended SKU in Azure.
  - **Target Platform** – Currently, the assessment is performed and supported on Azure Database for PostgreSQL - Flexible server.
  - **SKU Recommendation** – Based on the performance metrics, SKU available in Azure Database for PostgreSQL - Flexible server is recommended.
  - **Data Collection status** – Shows the number of cycles completed and the status of the assessment.

Users can select the Instance name; it shows the PostgreSQL instance's summary and migration readiness. Users can go through different server parameters and features, understand the use of the parameter, and get to know the recommendation for resolving the warnings.

Users can select the respective databases, understand the blockers and warnings and go through the summary if the database is in Ready, Ready with conditions, or Not Ready state for migration into Azure.

Users can save the assessment report on their machine for offline viewing by selecting the “Save Assessment” action.

   :::image type="content" source="media/azure-postgresql-migration-extension/run-assessment-step-3-view-assessment.png" alt-text="Screenshot of consolidated assessment report.":::

## SKU recommendations

The SKU recommendation feature allows you to collect performance data from your source PostgreSQL instances hosting your databases and recommends the right-sized Azure database for PostgreSQL – Flexible server SKU based on the data collected. The feature provides pricing tier, compute level, and data size recommendations.

The SKU recommendation provides the following:

- The suitable Azure target.
- The service tier.
- The metric thresholds are used to provide the recommended SKU.

The SKU recommendation evaluates various performance metrics, such as CPU, Memory, IOPS, latency, and storage. Based on the usage and configuration data, the recommender provides the suitable target and the appropriate service tier.

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

- [PostgreSQL extension](postgres-extension.md)
- [Azure Data Studio extensions](add-extensions.md)
- [Azure Data Studio release notes](../release-notes-azure-data-studio.md)
- [Database migrations using Azure Data Studio](/azure/dms/migration-using-azure-data-studio)
- [Download Azure Data Studio](../download-azure-data-studio.md)
- [Azure SQL Migration extension](azure-sql-migration-extension.md)