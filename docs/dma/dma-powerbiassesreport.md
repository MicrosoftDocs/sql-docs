---
title: "Report on your Consolidated Assessments using Power BI (SQL Server Data Migration Assistant) | Microsoft Docs"
ms.custom: 
ms.date: "08/24/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "sql-dma"
ms.tgt_pltfrm: ""
ms.topic: "article"
keywords: ""
helpviewer_keywords: 
  - "Data Migration Assistant, Assess"
ms.assetid: ""
caps.latest.revision: ""
author: "sabotta"
ms.author: "carlasab"
manager: "craigg"

---

# Report on your Consolidated Assessments using Power BI (Data Migration Assistant)

This article describes how to create a Power BI report for consolidated migration assessments.

For information on consolidating migration assessments using Data Migration Assistant, see [Consolidate Assessment Reports](../dma/dma-consolidatereports.md)

You can download at https://powerbi.microsoft.com/.

## Example Power BI Reports

To download examples of Power BI reports for consolidated migration assessments, click [here](https://msdnshared.blob.core.windows.net/media/2017/03/DMA-Reports-V3.11.zip).

The following reports are included. 

- [Dashboard](#dashboard--details)

  Includes snapshot stats and a drill down report

- [On Premise Upgrade Readiness](#on-premises-upgrade-readiness--details)

  The data source is the UpgradeSuccessRanking view in the DMAReporting database.  This report shows the percentage upgrade success for your assessed databases

- [On Premise Feature Parity](#on-premise-feature-parity--details)

  Shows the feature recommendations for the target SQL Server version

- [Azure SQL DB Upgrade Readiness](#azure-sql-db-upgrade-readiness--details)

  The data source is the UpgradeSuccessRanking view in the DMAReporting database.  This report shows the percentage upgrade success for databases assessed for Azure SQL DB migrations

- [Azure SQL DB Unsupported Features](#azure-sql-db-unsupported-features--details)

  Shows features that in your existing databases that are not supported in Azure SQL DB (V12).

You can modify these reports to work with your environment by changing the data source in Power BI. 

1. Select the down arrow next to **Edit Queries** and select
**Data source settings**.

   ![Edit Queries menu, Data source settings](../dma/media/DataSourceSettings.png)

1. Select **Change Source…** and enter the server and database values.

   ![Change source, server and database](../dma/media/ChangeSource.png)

1. Click **Ok**, and click then **Close**.

1. Refresh your reports.

   ![Refresh Power BI report](../dma/media/RefreshReport.png)

### Dashboard Report -- Details

![Dashboard Report](../dma/media/DashboardReport.png)

The dashboard shows details about all of your assessments.  You can use the slicers on the left hand side to filter by instance or database.  You can use the bar chart to drill down into specific categories to see where the issues lie.

To drill down, enable the drill down feature by clicking the circle with the down arrow in the top right of corner of the bar chart.

![Category DrilDown](../dma/media/CategoryDrillDown.png)

The drilldown sequence is set as shown in the following image (under **Axis**).  To change the sequence, simply drag and drop columns to the desired order.

![Visualizations, bar chart axis](../dma/media/VisualizationsAxis.png)

This view becomes even more powerful when first filtering by a specific database then drilling down into the specific category issues.  In the following example,the HR database is selected for instance **SQL01** to view all objects that are preventing migrations (breaking changes).

![Breaking changes for HR database](../dma/media/BreakingChanges.png)

### On Premises Upgrade Readiness Report -- Details

![On Premises Upgrade Readiness Report](../dma/media/OnPremisesUpgradeReadinessReport.png)

This report shows a snapshot of how ready your databases are to migrate to a higher version of SQL Server.  The data in this report comes from the dbo.UpgradeSuccessFactor\_OnPrem view in the DMAReporting database.

Filtering by instance and database name and using the score cards at the top, you can see which version the database could be migrated too.  For example, if you filter by the AdventureWorks 2012 database, you can see that the database is ready to move to all the SQL Server versions listed in the report.  This is determined by ensuring there are no breaking changes for that database and compatibility level.

![Upgrade success factor for AdventureWorks database](../dma/media/UpgradeSuccessFactor.png)

### On Premise Feature Parity Report -- Details

![On Premises Feature Parity Report](../dma/media/OnPremisesFeatureParityReport.png)

This report can be used to highlight what new features can be used for the database in the target SQL Server version.

By selecting a feature in the funnel chart, the data at the bottom highlights which objects would be affected by the feature.  In the following example, the **Stretch database for storage savings** feature is selected, and a table is listed that could benefit from this feature.

![Feature recommendation for Stretch Database](../dma/media/FeatureRecommend_StretchDatabase.png)

### Azure SQL DB Upgrade Readiness Report -- Details

![Azure SQL DB Upgrade Readiness Report](../dma/media/AzureSQLDBUpgradeReadinessReport.png)

This report shows the database readiness to migrate to Azure SQL Database V12.  The data from this report comes from the dbo.UpgradeSuccessRanking view in the DMAReporting database.

### Azure Features Parity Report -- Details

![Azure Features Parity Report](../dma/media/AzureFeaturesParityReport.png)

This report can be used to highlight the **instance level features** that are not supported by Azure SQL Database V12.

By selecting a feature in the funnel chart, the data at the bottom lists the instance(s) and database features that are not supported.  In the following example, this feature is selected -- **Always on Availability group configuration is not supported in Azure SQL Database**.  

![Always on Availability Group feature](../dma/media/Feature_AlwaysOnAvailability.png)

 
### Azure SQL DB Unsupported Features Report -- Details

![Azure SQL DB Unsupported Features Report](../dma/media/AzureSQLDBUnsupportedFeaturesReport.png)

This report highlights which features are not supported for a given **database** when the target is Azure SQL Database (V12).

By filtering by the database name and feature value in the funnel chart, you can see details on the unsupported feature. Details include which object is affected and recommendations for addressing the problem.

For example, filtering by the DTC database and **Read-only databases cannot be upgraded**, you can see a list of objects that are affected.

![Read-only databases cannot be upgraded issue](../dma/media/ReadOnlyDatabases.png)

## See Also

[Overview of Data Migration Assistant](../dma/dma-overview.md)

[Data Migration Assistant Download](https://www.microsoft.com/download/details.aspx?id=53595)

[Power BI Download](https://powerbi.microsoft.com/)
