---
title: Azure Cosmos DB Migration for MongoDB extension
description: Learn how to install and use the Azure Data Studio Azure Cosmos DB Migration for MongoDB extension.
author: sandnair
ms.author: sandnair
ms.reviewer: 
ms.date: 04/14/2023
ms.service: azure-data-studio
ms.topic: conceptual
---

# Azure Cosmos DB Migration for MongoDB extension (Preview)

The Azure Cosmos DB Migration for MongoDB extension in Azure Data Studio helps you assess a MongoDB workload for migrating to Azure Cosmos DB for MongoDB. You can use this extension to run an end-to-end assessment on your workload and find out the actions that you may need to take to seamlessly migrate your workloads on Azure Cosmos DB.

> [!NOTE]  
> The extension currently supports only running migration readiness assessment, not actual migration.


## Prerequisites

- [Download and Install latest Azure Data Studio](../../azure-data-studio/download-azure-data-studio.md).
- A MongoDB database running version 3.2 or higher.


## Install Azure Cosmos DB Migration for MongoDB extension

Following are the steps to install the Azure Cosmos DB Migration for MongoDB extension in Azure Data Studio.

1. Open the extensions manager in Azure Data Studio. You can select the extensions icon or select **Extensions** in the View menu.

1. Type *MongoDB* in the search bar.

1. Select the **Azure Cosmos DB Migration for MongoDB** extension and view its details.

    :::image type="content" source="media/database-migration-assessment-for-mongodb-extension/install-database-migration-assessment-for-mngodb-extension.png" alt-text="Screenshot showing Azure Cosmos DB Migration for MongoDB extension install button.":::

1. Select **Install**.



## Run assessment

Once the assessment extension installs, the next step is to connect to MongoDB endpoint, collect metadata information from your MongoDB endpoint and generate an assessment report.

1. Go to the connections icon in the menu bar, select **New Connection**.

   :::image type="content" source="media/database-migration-assessment-for-mongodb-extension/new-connection-mongodb-Menu.png" alt-text="Screenshot showing connections menu bar.":::

1. In the **Connection Details**, fill out the fields.
   1. In the **Connection type** field, select **Mongo account**.
   1. Fill in the connection details based on the *Connection String*  or *Parameters* option selection.
   1. In the Server Group,** select **Default**
   1. Provide an optional **Name** for this connection.

       :::image type="content" source="media/database-migration-assessment-for-mongodb-extension/new-connection-mongodb-details.png" alt-text="Screenshot showing the connection details":::

    1. Select **Connect**.

1. Now, a new connection appears in the connection details menu.

1. Right-click on the MongoDB connection and select **Manage**.

   :::image type="content" source="media/database-migration-assessment-for-mongodb-extension/connection-mongodb-manage.png" alt-text="Screenshot showing the Manage database screen.":::

1. Select **Migration Assessment**.

   :::image type="content" source="media/database-migration-assessment-for-mongodb-extension/database-migration-assessment-mongodb-dashboard.png" alt-text="Screenshot showing the Migration Assessment database screen.":::

1. Select **Dashboard** and click **Run new assessment**.

   :::image type="content" source="media/database-migration-assessment-for-mongodb-extension/mongodb-run-new-assessment.png" alt-text="Screenshot showing the Run new Assessment screen.":::

1. Provide your assessment details.

   1. In the **Assessment name** field, enter a title, for example, `demo1`.
   1. Select the target MongoDB version from the **Target Platform** dropdown.
   1. Provide the path to MongoDB profiler Logs. This is an optional field, however specifying the logs path results in more granular findings at the collection level. When the log folder isn't specified, the tool uses details from the `serverStatus` command to perform the assessment. `serverStatus` command returns feature usage only since the last restart, so you would need to ensure that sufficient time has passed since the last server restart to get an assessment that captures your workload.
   1. Click **Run validation** to validate the assessment inputs.
   1. On successful validation, select **Start assessment** to run the assessment.

1. Now, you see the new assessment in the "Assessments" table.

   :::image type="content" source="media/database-migration-assessment-for-mongodb-extension/mongodb-view-assessment-new-table.png" alt-text="Screenshot showing the new assessment in the Assessments table.":::

1. Select **Refresh**, or wait until the assessment completes. By default, the assessment status page refreshes every 15 seconds. The status shows you one of four statuses: *success*, *failed*, *in-progress*, or *canceled*.

## View the assessment

1. You can see the new assessment in the "Last five assessments" section.

   :::image type="content" source="media/database-migration-assessment-for-mongodb-extension/mongodb-view-last-assessment.png" alt-text="Screenshot showing the option to name the assessment.":::

1. Select the assessment link to view the assessment details page. A consolidated downloadable report is generated at the end of the assessment.


The initial part of the report has the key details of the assessment run including a summary of the source MongoDB environment against which the assessment was run. Details include source MongoDB version, license type and instance type. It also contains a list of the databases and collections assessed, with their respective assessment summaries and migration readiness.

The findings are categorized into Critical, Warning and Informational, to help you prioritize them according to their importance.

The details include:

1. `Collection Options` : Findings related to the unsupported collection settings. Examples include time series and collations.

1. `Features`: Findings related to unsupported database commands, query syntax or operators, including aggregation pipeline queries. In the additional details column, you would be able to see how often the particular feature was being used on the source endpoint. This would help you prioritize findings about more frequently used features.

1. `Limits and Quotas`: Findings related to Azure Cosmos DB for MongoDB specific quotas and limits.
   
1. `Indexes`: Findings related to the unsupported MongoDB index types or properties.

1. `Shard Keys`: Findings related to unsupported shard key configurations.

## Configure extension settings

You can configure some extension settings after installing the extension. This step is optional. If no settings are explicitly configured, the extension uses default settings.

1. Go to extensions and select **Azure Cosmos DB Migration for MongoDB**, select the **manage settings icon**, and then select extension settings.

   :::image type="content" source="media/database-migration-assessment-for-mongodb-extension/database-migration-assessment-for-mongodb-extension-settings.png" alt-text="Screenshot showing extension settings selection.":::

1. Under extension settings for this extension, provide the `Assessment path` to change the location where the assessment metadata is stored. If left blank,  default location will be used.

## Logs

The extension stores errors, warnings, and other diagnostic logs in the default log directory:

- Windows - `C:\Users\<username>\.dmamongo\logs\`
- Linux - `~/.dmamongo/logs`
- macOS - `/Users/<username>/.dmamongo/logs`

> [!NOTE] 
> A separate log file is created for each day. By default, the extension stores the last seven log files.

For more information about Frequently Asked Questions, visit [Migration for MongoDB extension FAQ](database-migration-for-mongodb-extension-faq.md).


## Contact Microsoft for help

If you need further assistance from Microsoft, contact Microsoft Support. For faster turnaround, you may attach the logs from [default log directory](#logs).

## Next steps
[Pre-migration steps for data migrations from MongoDB to Azure Cosmos DB for MongoDB](/azure/cosmos-db/mongodb/pre-migration-steps)