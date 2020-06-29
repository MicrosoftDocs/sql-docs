---
title: "Assess an application’s data access layer with Data Migration Assistant"
description: Learn how to use Data Migration Assistant to assess an application’s data access layer.
ms.date: "01/23/2020"
ms.prod: sql
ms.prod_service: "dma"
ms.reviewer: ""
ms.technology: dma
ms.topic: conceptual
keywords: ""
helpviewer_keywords: 
  - "Data Migration Assistant, Assess"
ms.assetid: ""
author: rajeshsetlem
ms.author: rajpo
ms.custom: "seo-lt-2019"
---

# Assess an app's data access layer with Data Migration Assistant

Applications typically connect and persist data to a database. The data access layer of the application provides simplified access to this data. Data Migration Assistant (DMA) has enabled users to assess their databases and related objects. The latest version of DMA (v5.0) introduces support for analyzing database connectivity and embedded SQL queries in the application code.

Consider the C# code segment below:

![Sample C# code segment](../dma/media/dma-assess-app-data-layer/dma-sample-c-sharp-code-segment.png)

In this case, you can see that the application is using a SQL query to get the name of an employee.

![Sample C# code segment detail](../dma/media/dma-assess-app-data-layer/dma-sample-c-sharp-code-detail.png)

As an application owner, I need to be able to identify the various databases that the application can connect to and the queries embedded in the application’s data access layer. In addition, I need to identify any changes required to modernize the application to Azure Data services.

## Assess an app with Data Access Migration Toolkit

To enable this assessment, we recently introduced the Data Access Migration Toolkit (DAMT), a Visual Studio Code extension. The latest version of this extension (v 0.2) adds support for .Net applications and T-SQL dialect.

1. Download and install VS Code from [here](https://code.visualstudio.com/download).
2. Enable the [Data Access Migration Toolkit extension](https://marketplace.visualstudio.com/items?itemName=ms-databasemigration.data-access-migration-toolkit) from the Extensions Marketplace.

   ![Data Access Migration Toolkit extension page](../dma/media/dma-assess-app-data-layer/dma-damt-extension-page.png)

3. Open the application project in Visual Studio Code.

   ![Application project in Visual Studio Code](../dma/media/dma-assess-app-data-layer/dma-app-project-in-vscode.png)

4. Start the extension console (Ctrl-Shft-P), and then run the **Data Access: Analyze Workspace** command.

   ![Extension console in Visual Studio Code](../dma/media/dma-assess-app-data-layer/dma-vscode-extension-console.png)

5. Select the SQL Server dialect.

   ![SQL Server dialect selection](../dma/media/dma-assess-app-data-layer/dma-sql-server-dialect.png)

   At the end of the analysis, the command produces a report of SQL connectivity commands and queries.

   ![Data Access Report](../dma/media/dma-assess-app-data-layer/dma-data-access-report.png)

6. Review the report for data connectivity components and for SQL queries embedded in the application code, which appear highlighted.

   ![SQL queries in application code](../dma/media/dma-assess-app-data-layer/dma-sql-queries-in-app-code.png)

   These queries can be analyzed through DMA for compatibility and feature parity issues based on the target SQL platform.

7. To assess the application’s data layer, export the report as json file.

   ![.json file export](../dma/media/dma-assess-app-data-layer/dma-json-file-export.png)

   In this case, the generated file is:

   ![View contents of json file](../dma/media/dma-assess-app-data-layer/dma-json-file-contents.png)

   Data Migration Assistant enables assessing the queries identified in the application within the context of modernizing the database to Azure Data platform.

8. Start Data Migration Assistant, and then create an assessment project.

   ![Data Migration Assistant new assessment project](../dma/media/dma-assess-app-data-layer/dma-new-assessment-project.png)

9. Select the source SQL Server instance.

   ![Data Migration Assistant select SQL Server source](../dma/media/dma-assess-app-data-layer/dma-select-sql-source.png)

10. Select the database to which the application is connecting.

    ![Data Access Migration select application database](../dma/media/dma-assess-app-data-layer/dma-select-app-database.png)

    To facilitate data access assessment, DMA introduces the ability to include json files with application queries. Next, we’ll include the json file we crafted earlier with the application queries.

11. Select the database and browse to the json file exported from Data Access Migration Toolkit to include the queries from the application for the assessment.

    ![Data Migration Assistant open DMAT json file](../dma/media/dma-assess-app-data-layer/dma-open-damt-json-file.png)

12. Start the assessment.

    ![Data Migration Assistant start assessment](../dma/media/dma-assess-app-data-layer/dma-start-assessment.png)

13. Review the assessment report. The generated report will include any compatibility or feature parity issues detected in the application queries as shown below.

    ![Data Migration Assistant assessment report](../dma/media/dma-assess-app-data-layer/dma-assessment-report.png)

Now, in addition to having the database perspective of the migration, users also have a view from the application perspective.

## See also

* [Overview of Data Migration Assistant](../dma/dma-overview.md)
* [Data Migration Assistant: Configuration settings](../dma/dma-configurationsettings.md)
* [Data Migration Assistant: Best Practices](../dma/dma-bestpractices.md)
* [Data Access Migration Toolkit](https://marketplace.visualstudio.com/items?itemName=ms-databasemigration.data-access-migration-toolkit)