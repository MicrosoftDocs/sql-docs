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

# Azure Database for PostgreSQL - Flexible Server extension (Preview)

The Database Migration Assessment for Oracle extension in Azure Data Studio helps you assess an Oracle workload for migrating to Azure SQL and Azure Database for PostgreSQL. The extension identifies an appropriate Azure SQL or PostgreSQL target with right-sizing recommendations and the complexity of the migration.

We're aiming to ease the burden of PostgreSQL database migrations to Azure Database for PostgreSQL flexible server by providing a simplified end-to-end assessment experience in Azure Data Studio (ADS).
The first step is an assessment capability that helps to identify whether the PostgreSQL Server instance is ready to be moved to Azure. Assessment is a fundamental step that allows users to understand the value of cloud migrations and make strategic decisions for moving into Azure Cloud services (Azure Database for PostgreSQL – Flexible server).
Salient features of the PostgreSQL Assessment in ADS:
• Identify cloud migration readiness and suggest actions that need to be taken from users to make it ready to migrate into Azure.
• Provides the optimal SKU recommendation for your workload in Azure.
• Free of cost.
• A connection to the source instance server and a few essential inputs are needed to run this Assessment.
• Save the assessment report for offline analysis.

## Prerequisites

- [Azure Data Studio version 1.39 or above](../download-azure-data-studio.md).
- A PostgreSQL Server instance running 9.3 or higher.
  - PostgreSQL Server instance should be on IaaS.
  - User needs to have at least minimum privileges.

### For automatic collection for SKU recommendation –

For automatic collection for SKU recommendation, only the following apply:

- User needs to have to execute privilege on pg_read_file() function
GRANT EXECUTE ON FUNCTION pg_read_file(text) TO <<username>>;

- User should be granted the role pg_read_server_files
GRANT pg_read_server_files TO <<username>>;

- Applicable only for Linux

## Install Azure Data Studio extension

Follow the steps below to install the Database Migration Assessment for Oracle extension in Azure Data Studio.

1. Open the extensions manager in Azure Data Studio. You can select the extensions icon or select **Extensions** in the View menu.

1. Type *PostgreSQL Migration* in the search bar.

1. Select the **Azure PostgreSQL Migration** extension and view its details.

1. Select **Install**.

  :::image type="content" source="media/database-migration-assessment-for-oracle-extension/install-database-migration-assessment-for-oracle-extension.png" alt-text="install extension":::

• Go to File and Select on Install extension from VSIX package.

• Select  postgresql-migration-1.2.x.vsix from the local machine. Build No will change in every release - You'll receive a prompt with the below message.

• Select Yes, and wait for the extension to be installed.
• Along with the “Azure PostgreSQL Migration” extension, PostgreSQL and.NET6 Runtime extension gets installed as part of dependencies.

## Run assessment

Once the assessment extension is installed, the next step is to connect to PostgreSQL Server instance, collect metadata information from PostgreSQL Server instance and generate an assessment report.

1. Go to the connections icon in the menu bar.

   :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-connections-menu-bar.png" alt-text="connections menu bar":::

1. In the **Connection Details**, fill out the fields.
   1. In the **Connection type** field, select **Oracle**.
   1. In the **Data Source** field, type in your Oracle server name and instance.
      1. You can provide the TNS name as well. (Make sure that the `ORACLE_HOME` environment variable is set and the `TNSNAMES.ORA` file is located in the `<ORACLE_HOME>/network/admin folder`.)
   1. Provide the database username in the **User Id** field.
   1. Provide the database password in the **Password** field.

   :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-connections-details.png" alt-text="connection details":::

1. Select **Connect**.

1. Now, a new connection appears in the connection details menu.

1. Right-click on the Oracle connection and select **Manage**.

   :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-manage-database-connection.png" alt-text="Screenshot showing the Manage database screen.":::

1. Select **Migration Assessment**.

   :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-select-oracle-assessment.png" alt-text="Screenshot showing the Migration Assessment database screen.":::

1. Select **Run new Assessment**.

   :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-run-new-oracle-assessment.png" alt-text="Screenshot showing the Run new Assessment screen.":::

1. Provide your assessment details.

   1. In the **Assessment name** field, enter a title, for example, `demo1`.
   1. Enter your **Assessment setting**.
      1. Enter the destination migration database in the **Target Platform** field. Currently, it supports Azure SQL and PostgreSQL as targets.
      1. In the **Performance data collection** section, either select **Run performance data collection on Oracle database (connected)** or **Add AWR report**.

         The recommendation is to use the connected option if you're running this tool during a peak or realistic load. Otherwise, provide the AWR reports generated in the past for performance and sizing recommendations.

      1. Enter the multiplier value in the **Scale factor** field.

         1. If the recommended SKU needs to consider other peak loads, the scale factor multiplier should be greater than 1. For example, Burst Load, Seasonal usage, and future capacity planning. When a partial Oracle schema workload is migrated, the multiplier should be less than 1.

      1. In the **Percentile utilization** field, enter the percentile value for sizing the Azure target.

         1. The percentile value of the performance sample set to be considered for sizing the Azure target.

   :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-assessment-details.png" alt-text="Screenshot showing the assessment details screen.":::

1. When the target is PostgreSQL, there are two types of assessment:

   - **Workload Assessment.** In this assessment, the Oracle assessment module performs a lightweight discovery of the schema objects and categorizes the schema complexity for migration to various categories with a high-level estimate for conversion hours.

   - **Code Assessment.** The code assessment performs an in-depth assessment of the Oracle schema objects and then suggests an overall readiness of the code objects with "ready", "need review", and "not ready" object types at a granular level and its associated conversion hours with higher accuracy.

     > [!NOTE]  
     > The extension uses the open-source Ora2Pg schema converter for code assessments.

     If the Oracle client and/or Ora2Pg isn't installed on the same machine as Azure Data Studio, either manually install them, or use the [Ora2Pg installation script](https://github.com/microsoft/OrcasNinjaTeam/tree/master/ora2pg-install) from GitHub.

     If the Oracle client and Ora2Pg are installed, ensure that the Oracle client home path, and Ora2Pg installation path are updated in the extension settings.

1. Now, you see the new assessment in the "Last five assessments" section.

   :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-demo.png" alt-text="Screenshot showing the option to name the assessment.":::

1. Select the assessment link to view the assessment details page. You can view the latest assessment status.

   :::image type="content" source="media/database-migration-assessment-for-oracle-extension/dmafo-latest-assessment-status.png" alt-text="Screenshot showing the latest assessment status.":::

1. Select **Refresh**, or wait until the assessment completes. By default, the assessment status page refreshes every 15 seconds. The status shows you one of four statuses: *success*, *failed*, *in-progress*, or *canceled*.

View Assessment
Once the Assessment is complete, a consolidated output will be generated.

• On the top, the two cards represent the recommended SKU in Azure
o Target Platform – Currently, Assessment is performed and supported on Azure Database for PostgreSQL - Flexible server.
o SKU Recommendation – Based on the performance metrics, SKU available in Azure Database for PostgreSQL - Flexible server is recommended.
o Target Version – The latest version is available in Azure Database for PostgreSQL -Flexible server.
• User can Select on the Instance name; it shows the PostgreSQL instance's summary and migration readiness. Users can go through different server parameters and features, understand the use of the parameter, and get to know the recommendation for resolving the warnings.
• User can Select on the respective databases, understand the blockers and warnings and go through the summary if the database is in Ready, Ready with conditions, or Not Ready state for migration into Azure.
• Users can save the Assessment report on their machine for offline viewing by Selecting on the “Save Assessment” action.

• Sample report screens

Known Issues
• If the PostgreSQL user doesn't have the execute privilege on the PG_READ_FILE function, then the ADS extension “Azure PostgreSQL Migration” doesn't throw any error while using the automatic collection, and the SKU is recommended based on the IOPS and Storage. In the report, the source properties for vCores and Memory will be shown as “N/A.”
• Collations used in source and not present in Azure Database for PostgreSQL – Flexible server will be shown as Blocker at the database level and Warning at the Server parameter level.
• Feedback/New support request button in the landing page not supported.

Feedback/Issues
Your feedback is valuable to us. For questions and feedback, reach out to epgsupport@microsoft.com with the subject "Database Migration for PostgreSQL Feedback”

## Next steps

- [Azure SQL Migration extension](azure-sql-migration-extension.md)
- [Oracle extension](extension-for-oracle.md)
- [Data Schema Conversion](dsct/database-schema-conversion-toolkit.md)
- [Add extensions](../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/bulk-load-xml/xml-bulk-load-examples-sqlxml-4-0.md)