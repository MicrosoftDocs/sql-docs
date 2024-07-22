---
title: "Assess an app's data access layer with Data Migration Assistant"
description: Learn how to use the Data Migration Assistant to assess the data access layer for an application. The data access layer gives access to persisted data.
author: ajithkr-ms
ms.author: ajithkr
ms.reviewer: randolphwest
ms.date: 06/28/2024
ms.service: sql
ms.subservice: dma
ms.topic: how-to
ms.custom:
  - kr2b-contr-experiment
  - sql-migration-content
helpviewer_keywords:
  - "Data Migration Assistant, Assess"
---

# Assess an application's data access layer with Data Migration Assistant

[!INCLUDE [deprecation-notice](includes/deprecation-notice.md)]

Applications typically connect and persist data to a database. The data access layer of the application provides simplified access to this data. Data Migration Assistant (DMA) enables you to assess your databases and related objects. The latest version of DMA (v5.0) introduces support for analyzing database connectivity and embedded SQL queries in the application code.

Consider this C# code segment:

:::image type="content" source="media/dma-assess-app-data-layer/dma-sample-c-sharp-code-segment.png" alt-text="Screenshot shows a sample C# code segment." lightbox="media/dma-assess-app-data-layer/dma-sample-c-sharp-code-segment.png":::

In this case, you can see that the application is using a SQL query to get the name of an employee.

:::image type="content" source="media/dma-assess-app-data-layer/dma-sample-c-sharp-code-detail.png" alt-text="Screenshot shows a line of the sample C# code segment." lightbox="media/dma-assess-app-data-layer/dma-sample-c-sharp-code-detail.png":::

As an application owner, you need to be able to identify the various databases that the application can connect to and the queries embedded in the application's data access layer. In addition, you need to identify any changes required to modernize the application to Azure Data services.

## Assess an application with Data Access Migration Toolkit

To enable this assessment, use the Data Access Migration Toolkit (DAMT), a Visual Studio Code extension. The latest version of this extension (v 0.2) adds support for .NET applications and T-SQL dialect.

1. Download and install [VS Code](https://code.visualstudio.com/download).
1. Enable the [Data Access Migration Toolkit extension](https://marketplace.visualstudio.com/items?itemName=ms-databasemigration.data-access-migration-toolkit) from the Extensions Marketplace.

   :::image type="content" source="media/dma-assess-app-data-layer/dma-damt-extension-page.png" alt-text="Screenshot shows the Data Access Migration Toolkit extension page in Visual Studio Code." lightbox="media/dma-assess-app-data-layer/dma-damt-extension-page.png":::

1. Open the application project in Visual Studio Code.

   :::image type="content" source="media/dma-assess-app-data-layer/dma-app-project-in-vscode.png" alt-text="Screenshot shows Visual Studio Code with the application project open." lightbox="media/dma-assess-app-data-layer/dma-app-project-in-vscode.png":::

1. Press **Ctrl**+**Shift**+**P** to start the extension console and then run the **Data Access: Analyze Workspace** command.

   :::image type="content" source="media/dma-assess-app-data-layer/dma-vscode-extension-console.png" alt-text="Screenshot shows the extension console in Visual Studio Code." lightbox="media/dma-assess-app-data-layer/dma-vscode-extension-console.png":::

1. Select the SQL Server dialect.

   :::image type="content" source="media/dma-assess-app-data-layer/dma-sql-server-dialect.png" alt-text="Screenshot shows SQL Server dialect selection." lightbox="media/dma-assess-app-data-layer/dma-sql-server-dialect.png":::

   At the end of the analysis, the command produces a report of SQL connectivity commands and queries.

   :::image type="content" source="media/dma-assess-app-data-layer/dma-data-access-report.png" alt-text="Screenshot shows the results, a Data Access Report." lightbox="media/dma-assess-app-data-layer/dma-data-access-report.png":::

1. Review the report for data connectivity components and for SQL queries embedded in the application code. These elements appear highlighted.

   :::image type="content" source="media/dma-assess-app-data-layer/dma-sql-queries-in-app-code.png" alt-text="Screenshot shows SQL queries in application code." lightbox="media/dma-assess-app-data-layer/dma-sql-queries-in-app-code.png":::

   These queries can be analyzed through DMA for compatibility and feature parity issues based on the target SQL platform.

1. To assess the application's data layer, export the report in JSON format.

   :::image type="content" source="media/dma-assess-app-data-layer/dma-json-file-export.png" alt-text="Screenshot shows Visual Studio Code exporting the report as a json file." lightbox="media/dma-assess-app-data-layer/dma-json-file-export.png":::

   In this case, the generated file has these contents:

   :::image type="content" source="media/dma-assess-app-data-layer/dma-json-file-contents.png" alt-text="Screenshot shows the contents of the json file." lightbox="media/dma-assess-app-data-layer/dma-json-file-contents.png":::

   Data Migration Assistant enables assessing the queries identified in the application within the context of modernizing the database to Azure Data platform.

1. Start Data Migration Assistant, and then create an assessment project.

   :::image type="content" source="media/dma-assess-app-data-layer/dma-new-assessment-project.png" alt-text="Screenshot shows Data Migration Assistant, ready to create a new assessment project." lightbox="media/dma-assess-app-data-layer/dma-new-assessment-project.png":::

1. Select the source SQL Server instance.

   :::image type="content" source="media/dma-assess-app-data-layer/dma-select-sql-source.png" alt-text="Screenshot show Data Migration Assistant with SQL Server source instance selected." lightbox="media/dma-assess-app-data-layer/dma-select-sql-source.png":::

1. Select the database to which the application is connecting.

   :::image type="content" source="media/dma-assess-app-data-layer/dma-select-app-database.png" alt-text="Screenshot shows Data Access Migration with an application database selected." lightbox="media/dma-assess-app-data-layer/dma-select-app-database.png":::

   To facilitate data access assessment, DMA introduces the ability to include JSON files with application queries. Next, include the JSON file created earlier with the application queries.

1. Select the database and browse to the JSON file exported from Data Access Migration Toolkit to include the queries from the application for the assessment.

   :::image type="content" source="media/dma-assess-app-data-layer/dma-open-damt-json-file.png" alt-text="Screenshot shows Data Migration Assistant with the Browse button highlighted and a D M A T json file to be opened." lightbox="media/dma-assess-app-data-layer/dma-open-damt-json-file.png":::

1. Select **Start Assessment**.

   :::image type="content" source="media/dma-assess-app-data-layer/dma-start-assessment.png" alt-text="Screenshot shows Data Migration Assistant with Start Assessment highlighted." lightbox="media/dma-assess-app-data-layer/dma-start-assessment.png":::

1. Review the assessment report. The generated report includes any compatibility or feature parity issues detected in the application queries as shown below.

   :::image type="content" source="media/dma-assess-app-data-layer/dma-assessment-report.png" alt-text="Screenshot shows the Data Migration Assistant assessment report." lightbox="media/dma-assess-app-data-layer/dma-assessment-report.png":::

Now, in addition to having the database perspective of the migration, users also have a view from the application perspective.

## Related content

- [Overview of Data Migration Assistant](dma-overview.md)
- [Data Migration Assistant: Configuration settings](dma-configurationsettings.md)
- [Data Migration Assistant: Best Practices](dma-bestpractices.md)
- [Data Access Migration Toolkit](https://marketplace.visualstudio.com/items?itemName=ms-databasemigration.data-access-migration-toolkit)
