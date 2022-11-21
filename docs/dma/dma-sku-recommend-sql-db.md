---
title: "Get Azure SQL SKU recommendations (Data Migration Assistant)"
description: Learn how to use Data Migration Assistant to identify the right Azure SQL Database, Azure SQL Managed Instance or SQL Server on Azure VM SKU for your on-premises database
author: aciortea
ms.author: aciortea
ms.date: "06/08/2021"
ms.service: sql
ms.subservice: dma
ms.topic: conceptual
helpviewer_keywords:
  - "Data Migration Assistant, Assess"
---

# Identify the right Azure SQL Database, Azure SQL Managed Instance, or SQL Server on Azure VM SKU for your on-premises database

Migrating databases to the cloud can be complicated. Especially so when trying to select the best Azure SQL Database, SQL Managed Instance, or SQL Server on Azure VM target and SKU for your database. Database Migration Assistant (DMA) helps address these questions and make your database migration experience easier by providing these SKU recommendations in a user-friendly output. Using performance data DMA can now recommend an appropriate target Azure SQL SKU, and an explanation for the recommendation.

The SKU recommendations feature allows you to both collect performance data from your source SQL Server instances hosting your databases and recommend minimum Azure SQL Database, Azure SQL Managed Instance, or SQL Server on Azure VM SKU based on the data collected. The feature provides recommendations related to pricing tier, compute level, and data size. This functionality is currently available only via the Command Line Interface (CLI).

The following are instructions to help you determine the SKU recommendations and to provision corresponding databases in Azure using DMA.

[!INCLUDE [online-offline](../includes/azure-migrate-to-assess-sql-data-estate.md)]

## Prerequisites

- Download and install the latest version of [DMA](https://aka.ms/get-dma). If you have already an earlier version of the tool, open it, and you'll be prompted to upgrade DMA.
- Install the minimum version [.NET Core 3.1](https://aka.ms/dotnet-core-applaunch?framework=Microsoft.NETCore.App&framework_version=3.1.0) on the tools machine where the SKU recommendations console application is running.
- Ensure the account used to connect to your SQL Server on-premises source has sysadmin permission.

> [!NOTE]
> It is recommended that the tool is utilized from a separate tools (client) machine with connectivity to the target SQL instance(s), rather than from the machine hosting SQL Server itself, in order to minimize any potential overhead. When collecting performance data for SKU recommendations, it is recommended that the tool is ran with default option values over the span of several hours, covering both off-peak and on-peak workloads and excluding maintenance tasks such as index rebuild or backup database. Maintenance tasks can impact the CPU, Memory and IO consumption and subsequently drive higher recommended SKU tiers.

## Collect performance data

The collected data includes limited information about the hardware configuration of your server and aggregated SQL-specific performance data from system Dynamic Management Views (DMVs) such as CPU, memory, storage usage, IO throughput, and IO latency. The collected data is stored locally on your machine for further aggregation and analysis. The performance characteristics of your source instance is analyzed to enable SKU recommendations for Azure SQL offerings (including SQL Database, SQL Managed Instance, and SQL on Azure VM) that best suit your workload while also being cost-effective.

In the DMA installation path, locate the SQLAssessmentConsole folder and the SqlAssessment.exe application

  ![Screenshot of SKUConsoleApplication.exe file shown in DMA installation folder location.](../dma/media/dma-sku-recommend-console-location.png)

In order to start the data collection process, specify the `PerfDataCollection` action in the console application, with the following arguments:

- **sqlConnectionStrings**: (_Required_) Quote-enclosed formal connection string(s) for the target SQL instance(s).
- **perfQueryIntervalInSec** (_Optional_):  Interval at which to query performance data, in seconds. (Default: 30)
- **staticQueryIntervalInSec** (_Optional_): Interval at which to query and persist static configuration data, in seconds. (Default: 60)
- **numberOfIterations** (_Optional_):  Number of iterations of performance data collection to perform before persisting to file. For example, with default values, performance data will be persisted every 30 seconds * 20 iterations = 10 minutes. (Default: 20)
- **outputFolder** (_Optional_):  Folder which performance data, reports, and logs will be written to/read from. (Default: %LocalAppData%/Microsoft/SqlAssessmentConsole)

The following is a sample invocation:

```
.\SqlAssessment.exe PerfDataCollection 
--sqlConnectionStrings "Data Source=Server1;Initial Catalog=master;Integrated Security=True;" "Data Source=Server2;Initial Catalog=master;Integrated Security=True;" 
--outputFolder C:\Output
```

Alternatively, the data collection process can be invoked by providing the appropriate arguments in a JSON configuration file, and passing the configuration file to the tool by running the executable without an action, as follows:

  ```

 .\SqlAssessment.exe --configFile C:\path\to\config.json
  ```

Below is a sample ConfigFile equivalent to the performance data collection action described above:

  ```

  {
    "action": "PerfDataCollection",
    "sqlConnectionStrings": [
    "Data Source=Server1;Initial Catalog=master;Integrated Security=True;",
    "Data Source=Server2;Initial Catalog=master;Integrated Security=True;"
    ],
    "outputFolder": "C:\\Output"
  }
  ```

Sample config files for all of the actions can be found in the `Example` folder under DMA installation path: GetMetadataSampleConfigFile.json, PerfDataCollectionSampleConfigFile.json, and GetSkuRecommendationSampleConfigFile.json.

After the command executes, the performance and configuration data points are saved as a set of three *_Counters.csv files per target instance, each containing the server, and instance name. You can use this file as input for the next part of the process, which will provide SKU recommendations for Azure SQL Database, Azure SQL Managed Instance, or SQL Server on Azure VM.

## Use the console application to get SKU recommendations

The data points collected by the previous step will be used as the input for the SKU recommendation process.

For the single database option, DMA will provide recommendations for the Azure SQL Database single database tier, the compute level, and the recommended storage configuration for each database on your SQL instance.

For Azure SQL Managed Instance and SQL Server on Azure VM, the recommendations support a lift-and-shift scenario. As a result, SKU recommendations console app can provide you with recommendations for the Azure SQL Managed Instance, or SQL Server on Azure VM tier, the compute level, and the recommended storage configuration for the set of databases on your SQL instance. You can also specify only a subset of databases to be included or excluded from the SKU recommendations.

`GetSkuRecommendation` uses by default a baseline strategy, which maps collected performance data values representative for the workload (based on the percentile value specified) to the right Azure SQL SKU.
We also offer an elastic strategy (statistical approach), which generates a unique price-to-performance curve based on the collected performance data by analyzing the workload patterns against a model based on customers who already migrated to Azure SQL.

In order to start the SKU recommendation process, specify the `GetSkuRecommendation` action in the console application, with the following arguments:

- **perfQueryIntervalInSec** (_Optional_):  Interval at which performance data was queried, in seconds. Note: The value provided must match the value originally used during the performance data collection. (Default: 30)
- **targetPlatform** (_Optional_): Target platform for SKU recommendation: either AzureSqlDatabase, AzureSqlManagedInstance, AzureSqlVirtualMachine, or Any. Choosing Any allows SKU recommendations for all three target platforms to be evaluated, and the best fit is returned. (Default: Any)
- **targetSqlInstance** (_Optional_): Name of the SQL instance that SKU recommendation targets.  (Default: outputFolder is scanned for files created by the PerfDataCollection action, and recommendations are provided for every instance found)
- **targetPercentile** (_Optional_): Percentile of data points to be used during aggregation of the performance data. Only used for baseline (non-elastic) strategy). Only used for baseline (non-elastic) strategy. (Default: 95)
- **scalingFactor** (_Optional_): Scaling ('comfort') factor used during SKU recommendation. For example, if it is determined that there is a 4 vCore CPU requirement with a scaling factor of 150%, then the true CPU requirement will be 6 vCores. (Default: 100)
- **startTime** (_Optional_): UTC start time of performance data points to consider during aggregation, in "YYYY-MM-DD HH:MM" format. Only used for baseline (non-elastic) strategy. (Default: all data points collected will be considered)
- **endTime** (_Optional_): UTC end time of performance data points to consider during aggregation, in "YYYY-MM-DD HH:MM" format. Only used for baseline (non-elastic) strategy. (Default: all data points collected will be considered)
- **elasticStrategy** (_Optional_): Whether or not to use the elastic strategy for SKU recommendations based on resource usage profiling and cost-performance analysis. Elastic strategy is currently available for Azure SQL Databases and SQL Managed Instance, not yet available for SQL Server on Azure VM target. (Default: false)
- **databaseAllowList** (_Optional_): Space separated list of names of databases to be allowed for SKU recommendation consideration while excluding all others. Only set one of the following or neither: databaseAllowList, databaseDenyList. (Default: null)
- **databaseDenyList** (_Optional_): Space separated list of names of databases to be excluded for SKU recommendation. Only set one of the following or neither: databaseAllowList, databaseDenyList. (Default: null)
- **overwrite** (_Optional_): Whether or not to overwrite any existing SKU recommendation reports. (Default: true)
- **displayResult** (_Optional_): Whether or not to print the SKU recommendation results to the console. (Default: true)
- **outputFolder** (_Optional_): Folder in which performance data, reports, and logs will be written to/read from. (Default: %LocalAppData%\Microsoft\SqlAssessment)
- **suppressPreviewFeatures** (_Optional_):  If set to true any Azure feature that is in a preview period will not be included in the recommendation. (Default: false)

Advanced settings for the SKU recommendations can be found in the `Console.Settings.json` file in the root directory. Currently, it includes the following customizable parameters:

**`CommandTimeoutGroupSetting`**: The time in seconds to wait for SQL query commands to execute before timing out.

- `PerfCollectionCommandTimeout`: Command timeout for potentially long-running queries related to performance data collection (Default: 300)
- `DefaultCollectionCommandTimeout`: Command timeout for all other queries (Default: 120)

**`ThrottlingGroupSetting`**: Number of parallel tasks to create based on number of cores on the machine

- `ServerInstancesParallelCount`: Number of server instances to assess in parallel (Default: 2)
- `DatabasesParallelCount`: Number of databases to assess in parallel (Default: 4)
- `UserDefinedObjectsParallelCountPerDb`: Number of user-defined objects (stored procedures, views, triggers, etc.) to assess in parallel per database (Default: 4)

**`AllowTelemetry`**: Whether or not to allow the collection and transmission of anonymous feature usage and diagnostic data to Microsoft. (Default: true)

Alternatively, the SKU recommendation process can be invoked by providing the appropriate arguments in a JSON configuration file, and passing the configuration file to the tool by running the executable without an action, as follows:

```
 .\SqlAssessment.exe --configFile C:\path\to\config.json
```

Below is a sample ConfigFile equivalent to the SKU recommendations action described above:

```
{
    "action": "GetSkuRecommendation",
    "outputFolder": "C:\\Output",
    "targetPlatform": "AzureSqlDatabase",
    "targetSqlInstance": "Server1",
    "targetPercentile": 95,
    "scalingFactor": 100,
    "startTime": "2020-01-01 00:00",
    "endTime": "2022-01-01 00:00",
    "perfQueryIntervalInSec": 30,
    "overwrite": "true"
}
 ```

In order to get SKU recommendations for a specific Azure SQL platform instead of selecting one automatically, provide a value for the `--targetPlatform` option, as follows:

**Sample 1: Getting SKU recommendations  for Azure SQL Database.**

```
.\SqlAssessment.exe GetSkuRecommendation 
--outputFolder C:\Output 
--targetPlatform AzureSqlDatabase
```

**Sample 2: Getting SKU recommendations using elastic strategy for Azure SQL Managed Instance.**

```
.\SqlAssessment.exe GetSkuRecommendation 
--outputFolder C:\Output 
--targetPlatform AzureSqlManagedInstance
--elasticStrategy true
```

**Sample 3: Getting SKU recommendations for Azure SQL Virtual Machine.**

```
.\SqlAssessment.exe GetSkuRecommendation 
--outputFolder C:\Output 
--targetPlatform AzureSqlVirtualMachine
```

**Sample 4: Getting SKU recommendations for Azure SQL Virtual Machine and surpressing preview features.**

```
.\SqlAssessment.exe GetSkuRecommendation 
--outputFolder C:\Output 
--targetPlatform AzureSqlVirtualMachine
--suppressPreviewFeatures True
```

The following is an example output of an Azure SQL Database recommendation:

:::image type="content" source="media/sku-recommendations-azure-sql-db.png" alt-text="Screenshot of Azure SQL Database SKU tier and sizing recommendations shown in SQLAssessment console.":::

The following is an example output of an Azure SQL Managed Instance recommendation:

:::image type="content" source="media/sku-recommendations-azure-sql-managed-instance.png" alt-text="Screenshot of Azure SQL Managed Instance SKU tier and size recommendations shown in console.":::

The following is an example output of a SQL Server on Azure VM recommendation:

:::image type="content" source="media/sku-recommendations-azure-sql-virtual-machine.png" alt-text="Screenshot of SQL Server on Azure VM SKU tier and size recommendations output shown in console.":::

The output of the SKU recommendation is saved both as a detailed report in JSON format and a summarized easy to read HTML file. The output covers the following sections:

- **Instance Name**: Name of the on-premises SQL Server instance(s)
- **Database Name**: Name of the on-premises SQL Server database(s)
- **SKU Recommendation**: The minimum cost-efficient SKU offering among all the performance eligible SKUs that could accommodate your workloads.
- **Recommendation Reason**: For each tier that is recommended, we provide the reasons and collected data values driving the recommendations.

The final recommended tier and configuration values for that tier reflect the minimum SKU required for your queries to run in Azure with a success rate similar to your on-premises databases.

## Next step

- For a complete list of commands for running DMA from the CLI, see the article [Run Data Migration Assistant from the command line](./dma-commandline.md).
