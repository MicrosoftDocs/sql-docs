---
title: "Assess an app's data access layer with Data Migration Assistant"
description: Learn how to use the Data Migration Assistant to assess the data access layer for an application. The data access layer gives access to persisted data.
author: rajeshsetlem
ms.author: rajpo
ms.date: 05/18/2022
ms.service: sql
ms.subservice: dma
ms.topic: how-to
ms.custom:
  - "seo-lt-2019"
  - kr2b-contr-experiment
helpviewer_keywords:
  - "Data Migration Assistant, Assess"
---

# Assess an application's data access layer with Data Migration Assistant

Applications typically connect and persist data to a database. The data access layer of the application provides simplified access to this data. Data Migration Assistant (DMA) enables you to assess your databases and related objects. The latest version of DMA (v5.0) introduces support for analyzing database connectivity and embedded SQL queries in the application code.

Consider this C# code segment:

![Screenshot shows a sample C# code segment.](../dma/media/dma-assess-app-data-layer/dma-sample-c-sharp-code-segment.png)

In this case, you can see that the application is using a SQL query to get the name of an employee.

![Screenshot shows a line of the sample C# code segment.](../dma/media/dma-assess-app-data-layer/dma-sample-c-sharp-code-detail.png)

As an application owner, you need to be able to identify the various databases that the application can connect to and the queries embedded in the application's data access layer. In addition, you need to identify any changes required to modernize the application to Azure Data services.

## Assess an application with Data Access Migration Toolkit

To enable this assessment, use the Data Access Migration Toolkit (DAMT), a Visual Studio Code extension. The latest version of this extension (v 0.2) adds support for .NET applications and T-SQL dialect.

1. Download and install [VS Code](https://code.visualstudio.com/download).
2. Enable the [Data Access Migration Toolkit extension](https://marketplace.visualstudio.com/items?itemName=ms-databasemigration.data-access-migration-toolkit) from the Extensions Marketplace.

   ![Screenshot shows the Data Access Migration Toolkit extension page in Visual Studio Code.](../dma/media/dma-assess-app-data-layer/dma-damt-extension-page.png)

3. Open the application project in Visual Studio Code.

   ![Screenshot shows Visual Studio Code with the application project open.](../dma/media/dma-assess-app-data-layer/dma-app-project-in-vscode.png)

4. Press **Ctrl**+**Shift**+**P** to start the extension console and then run the **Data Access: Analyze Workspace** command.

   ![Screenshot shows the extension console in Visual Studio Code.](../dma/media/dma-assess-app-data-layer/dma-vscode-extension-console.png)

5. Select the SQL Server dialect.

   ![Screenshot shows SQL Server dialect selection.](../dma/media/dma-assess-app-data-layer/dma-sql-server-dialect.png)

   At the end of the analysis, the command produces a report of SQL connectivity commands and queries.

   ![Screenshot shows the results, a Data Access Report.](../dma/media/dma-assess-app-data-layer/dma-data-access-report.png)

6. Review the report for data connectivity components and for SQL queries embedded in the application code. These elements appear highlighted.

   ![Screenshot shows SQL queries in application code.](../dma/media/dma-assess-app-data-layer/dma-sql-queries-in-app-code.png)

   These queries can be analyzed through DMA for compatibility and feature parity issues based on the target SQL platform.

7. To assess the application's data layer, export the report in JSON format.

   ![Screenshot shows Visual Studio Code exporting the report as a json file.](../dma/media/dma-assess-app-data-layer/dma-json-file-export.png)

   In this case, the generated file has these contents:

   ![Screenshot shows the contents of the json file.](../dma/media/dma-assess-app-data-layer/dma-json-file-contents.png)

   Data Migration Assistant enables assessing the queries identified in the application within the context of modernizing the database to Azure Data platform.

8. Start Data Migration Assistant, and then create an assessment project.

   ![Screenshot shows Data Migration Assistant, ready to create a new assessment project.](../dma/media/dma-assess-app-data-layer/dma-new-assessment-project.png)

9. Select the source SQL Server instance.

   ![Screenshot show Data Migration Assistant with SQL Server source instance selected.](../dma/media/dma-assess-app-data-layer/dma-select-sql-source.png)

10. Select the database to which the application is connecting.

    ![Screenshot shows Data Access Migration with an application database selected.](../dma/media/dma-assess-app-data-layer/dma-select-app-database.png)

    To facilitate data access assessment, DMA introduces the ability to include JSON files with application queries. Next, include the JSON file created earlier with the application queries.

11. Select the database and browse to the JSON file exported from Data Access Migration Toolkit to include the queries from the application for the assessment.

    ![Screenshot shows Data Migration Assistant with the Browse button highlighted and a D M A T json file to be opened.](../dma/media/dma-assess-app-data-layer/dma-open-damt-json-file.png)

12. Select **Start Assessment**.

    ![Screenshot shows Data Migration Assistant with Start Assessment highlighted.](../dma/media/dma-assess-app-data-layer/dma-start-assessment.png)

13. Review the assessment report. The generated report includes any compatibility or feature parity issues detected in the application queries as shown below.

    ![Screenshot shows the Data Migration Assistant assessment report.](../dma/media/dma-assess-app-data-layer/dma-assessment-report.png)

Now, in addition to having the database perspective of the migration, users also have a view from the application perspective.

## See also

* [Overview of Data Migration Assistant](../dma/dma-overview.md)
* [Data Migration Assistant: Configuration settings](../dma/dma-configurationsettings.md)
* [Data Migration Assistant: Best Practices](../dma/dma-bestpractices.md)
* [Data Access Migration Toolkit](https://marketplace.visualstudio.com/items?itemName=ms-databasemigration.data-access-migration-toolkit)
