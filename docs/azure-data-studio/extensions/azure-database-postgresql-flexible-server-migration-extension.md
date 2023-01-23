---
title: Azure Database for PostgreSQL flexible server extension
description: Learn how to install the Azure Data Studio Azure Database for PostgreSQL flexible server extension. This extension helps guide you choose the best Azure SQL database and Azure Database for PostgreSQL migration path.
author: apduvuri
ms.author: adityaduvuri
ms.reviewer: maghan, randolphwest
ms.date: 01/31/2023
ms.service: azure-data-studio
ms.topic: conceptual
---

# Azure PostgreSQL - flexible server migration extension (Preview)

[!INCLUDE [database-migration-services-ads](../../includes/database-migration-services-postgresql-ads.md)]

The Azure PostgreSQL migration extension in Azure Data Studio helps you assess your PostgreSQL workload for migrating to Azure Database for PostgreSQL flexible server. The extension identifies an appropriate PostgreSQL target with right-sizing recommendations and the complexity of the migration.

## Prerequisites

- [Azure Data Studio version 1.39 or above](../download-azure-data-studio.md).
- A PostgreSQL Server instance running 9.3 or higher.
  - PostgreSQL Server instance needs to be IaaS.

## Install Azure Data Studio extension

Follow the steps below to install the Azure PostgreSQL Migration extension in Azure Data Studio.

1. Open the extensions manager in Azure Data Studio. You can select the extensions icon or select **Extensions** in the **View** menu.

1. Type *PostgreSQL Migration* in the search bar.

1. Select the **Azure PostgreSQL Migration** extension and view its details.

1. Select **Install**.

  :::image type="content" source="media/azure-database-postgresql-flexible-server-migration-extension/install-azure-database-postgresql-flexible-server-migration-extension.png" alt-text="install extension":::

## Run assessment

Once the assessment extension is installed, the next step is to [connect to PostgreSQL Server instance](postgres-extension.md), collect metadata information from PostgreSQL Server instance and generate an assessment report.

1. Go to the connections icon in the menu bar.

   :::image type="content" source="media/azure-database-postgresql-flexible-server-migration-extension/new-connection.png" alt-text="connections menu bar":::

1. In the **Connection Details**, fill out the fields.
   1. In the **Connection type** field, select **PostgreSQL**.
   1. In the **Data Source** field, type in your PostgreSQL server.
   1. Provide your database username in the **User Id** field.
   1. Provide your database password in the **Password** field.

   :::image type="content" source="media/azure-database-postgresql-flexible-server-migration-extension/connection-details.png" alt-text="connection details":::

1. Select **Connect**.

1. Now, a new connection appears in the connection details menu.

1. Right-click on the PostgreSQL connection and select **Manage**.

   :::image type="content" source="media/azure-database-postgresql-flexible-server-migration-extension/manage-database-connection.png" alt-text="Screenshot showing the Manage database screen.":::

1. Under the **General** section select **Azure PostgreSQL Migration** then select **Assess Databases(s)**.

   :::image type="content" source="media/azure-database-postgresql-flexible-server-migration-extension/start-assessment.png" alt-text="Screenshot showing the Migration Assessment database screen.":::

There are three steps to complete before the assessment begins.

1. **Step 1: Databases(s) for assessment**
   1. Select what database(s) you want to assess for migration, then select **Next**.
      1. For this example, the ***paymentapp*** database is selected

       :::image type="content" source="media/azure-database-postgresql-flexible-server-migration-extension/run-assessment-step-1.png" alt-text="Screenshot of step 1.":::

1. **Step 2: Assessment Parameters**

      The default page shows **Manual collection**, however, for this example we are using the **Automation collection** page.

      1. Enter your IOPS value and select your **Scale factor**.
      1. Next select **Run Assessment**.

         :::image type="content" source="media/azure-database-postgresql-flexible-server-migration-extension/run-assessment-step-2-auto-values.png" alt-text="Screenshot of manual options for running te assessment.":::

1. **Step 3: View Assessment Results**

   Once the assessment is complete, a consolidated output is generated.

      - On the top the two cards represent the recommended SKU in Azure.
         - Target Platform – Currently, Assessment is performed and supported on Azure Database for PostgreSQL - Flexible server.
         - SKU Recommendation – Based on the performance metrics, SKU available in Azure Database for PostgreSQL - Flexible server is recommended.
         - Target Version – It is the latest version available in Azure Database for PostgreSQL -Flexible server.
      - User can click on the Instance name; it shows the summary and the migration readiness of the PostgreSQL instance. Users can go through different server parameters and the features, understand the use of the parameter, and get to know the recommendation for resolving the warnings.
      - User can click on the respective databases, understand the blockers, warnings   and go through the summary if the database is in Ready, Ready with conditions or Not Ready state for migration into Azure.
      - Users can save the Assessment report on their machine for offline viewing by clicking on the “Save Assessment” action.

      Now you can save the assessment report in a location of your choice.

   :::image type="content" source="media/azure-database-postgresql-flexible-server-migration-extension/run-assessment-step-3-view-assessment.png" alt-text="Screenshot of consolidated assessment report.":::

## Next steps

- [Azure SQL Migration extension](azure-sql-migration-extension.md)
- [PostgreSQL extension](postgres-extension.md)
- [Data Schema Conversion](dsct/database-schema-conversion-toolkit.md)