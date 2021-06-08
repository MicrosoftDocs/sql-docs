---
title: "Identify the right Azure SQL Database SKU for your on-premises database (Data Migration Assistant) | Microsoft Docs"
description: Learn how to use Data Migration Assistant to identify the right Azure SQL Database, Azure SQL Managed Instance or SQL Server on Azure VM SKU for your on-premises database
ms.custom: ""
ms.date: "05/06/2019"
ms.prod: sql
ms.prod_service: "dma"
ms.reviewer: ""
ms.technology: dma
ms.topic: conceptual
keywords: ""
helpviewer_keywords: 
  - "Data Migration Assistant, Assess"
ms.assetid: ""
author: aciortea
ms.author: aciortea
---

# Identify the right Azure SQL Database or SQL Managed Instance SKU for your on-premises database

Migrating  databases to the cloud can be complicated, especially when trying to select the best Azure SQL Database, SQL Managed Instance or SQL Server on Azure VM target and SKU for your database. Our goal with the Database Migration Assistant (DMA) is to help address these questions and make your database migration experience easier by providing these SKU recommendations in a user-friendly output. Using performance data points DMA now recommends an appropriate target Azure SQL SKU, as well as an explanation for the recommendation.


The SKU recommendations feature allows you to identify both the minimum recommended Azure SQL Database, Azure SQL Managed Instance or SQL Server on Azure VM SKU based on performance data points collected from your source SQL Server instances hosting your databases. The feature provides recommendations related to pricing tier, compute level, and max data size. This functionality is currently available only via the Command Line Interface (CLI).

The following are instructions to help you determine the SKU recommendations and provision corresponding databases in Azure using DMA.

[!INCLUDE [online-offline](../includes/azure-migrate-to-assess-sql-data-estate.md)]

## Prerequisites

- Download and install the latest version of [DMA](https://aka.ms/get-dma). If you have already an earlier version of the tool, open it, and you'll be prompted to upgrade DMA.
- Install the minimum version [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/current/runtime) on the tools machine where the SKU recommendations console application is running.
- Ensure the account used to connect to your SQL Server on-premises source has sysadmin permission.

> [!NOTE]
> It is recommended that the tool is utilized from a separate tools (client) machine with connectivity to the target SQL instance(s), rather than from the machine hosting SQL Server itself, in order to minimize any potential overhead. When collecting performance data for SKU recommendations, it is recommended that the tool is ran with default option values over the span of several hours, covering both off-peak and on-peak workloads and excluding maintenance tasks such as index rebuild or backup database. Maintenance tasks can impact the CPU, Memory and IO consumption and subsequently drive higher recommended SKU tiers.

## Collect performance data 

The collected data includes limited information about the hardware configuration of your server, as well SQL-specific performance counters from system Dynamic Management Views (DMVs)  such as CPU, memory, and storage usage, as well as IO throughput and IO latency. The collected data is stored locally on your machine. The collected data can then be aggregated and analysed, and by examining the performance characteristics of your source instance, SKU recommendations can be determined for Azure SQL offerings (including SQL Database, SQL Managed Instance, and SQL on Azure VM) that best suit your workload while also being cost-effective.

In the DMA installation folder, locate the SQLAssessmentConsole folder and the SqlAssessment.exe application

  ![SKUConsoleApplication.exe shown in DMA folder](../dma/media/dma-sku-recommend-console-location.jpg)

In order to start the data collection process, specify the `PerfDataCollection` action in the console application, with the following arguments:

  - **sqlConnectionStrings**: [Required] Quote-enclosed formal connection string(s) for the target SQL instance(s).
   - **perfQueryIntervalInSec**: [Optional] Interval at which to query performance data, in seconds. (Default: 30)
  - **staticQueryIntervalInSec**: [Optional Interval at which to query and persist static configuration data, in seconds. (Default: 60)
  - **numberOfIterations**: [Optional] Number of iterations of performance data collection to perform before persisting to file. For example, with default values, performance data will be persisted every 30 seconds * 20 iterations = 10 minutes. (Default: 20)
  - **outputFolder**: [Optional] Folder which performance data, reports, and logs will be written to/read from. (Default: current directory)


Here's a sample invocation:

```
.\SqlAssessment.exe PerfDataCollection 
--sqlConnectionStrings "Data Source=Server1;Initial Catalog=master;Integrated Security=True;" "Data Source=Server2;Initial Catalog=master;Integrated Security=True;" 
--outputFolder C:\Output
```

   
Alternatively, the data collection process can be invoked by providing the appropriate arguments in a JSON configuration file, and passing the configuration file to the tool by running the executable without an action, as follows:

  ```
 .\SqlAssessment.exe --configFile C:\path\to\config.json
  ```
    
Below is a sample config file equivalent to the performance data collection action described above:
    
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
Sample config files for all of the actions can be found in the Example folder under DMA installation path: AssessSampleConfigFile.json, PerfDataCollectionSampleConfigFile.json, and GetSkuRecommendationSampleConfigFile.json.
    

After the command executes, the performance data and configuration data points are saved as a set of three *_Counters.csv files per target instance, each containing the server and instance name. You can use this file as input for the next part of the process, which will provide SKU recommendations for Azure SQL Database, Azure SQL Managed Instance or SQL Server on Azure VM.


## Use the console application to get SKU recommendations

The data points collected by the previous step will be used as the input for the SKU recommendation process.

For the single database option, DMA will provide recommendations for the Azure SQL Database single database pricing tier, the compute level, and the maximum data size for each database on your SQL instance. If you have multiple databases on your SQL instance, you can also specify the databases for which you want recommendations.

For Azure SQL Managed Instance and SQL Server on Azure VM, the recommendations support a lift-and-shift scenario. As a result, SKU recommendations console app will provide you with recommendations for the Azure SQL Managed Instance or SQL Server on Azure VM pricing tier, the compute level, and the maximum data size for the set of databases on your SQL instance. Again, if you have multiple databases on your SQLIns, you can also specify the databases for which you want recommendations. 

In order to start the SKU recommendation process, specify the `GetSkuRecommendation` action in the console application, with the following arguments:
 
- **perfQueryIntervalInSec**: [Optional] Interval at which performance data was queried, in seconds. Note: This must match the value that was originally used during the performance data collection. (Default: 30)
- **targetPlatform**: [Optional] Target platform for SKU recommendation: either AzureSqlDatabase, AzureSqlManagedInstance, AzureSqlVirtualMachine, or Any. If Any is selected, then SKU recommendations for all three target platforms are evaluated, and the best fit is returned. (Default: Any)
- **targetSqlInstance**: [Optional] Name of the SQL instance that SKU recommendation targets.  (Default: outputFolder is scanned for files created by the PerfDataCollection action, and recommendations are provided for every instance found)
- **targetPercentile**: [Optional] Percentile of data points to be used during aggregation of the performance data. (Default: 95)
- **scalingFactor**: [Optional] Scaling ('comfort') factor used during SKU recommendation. For example, if it is determined that there is a 4 vCore CPU requirement with a scaling factor of 150%, then the true CPU requirement will be 6 vCores. (Default: 100)
- **startTime**: [Optional] UTC start time of performance data points to consider during aggregation, in "YYYY-MM-DD HH:MM" format. (Default: all data points collected will be considered)
- **endTime**: [Optional] UTC end time of performance data points to consider during aggregation, in "YYYY-MM-DD HH:MM" format. (Default: all data points collected will be considered)
- **overwrite**: [Optional] Whether or not to overwrite any existing SKU recommendation reports. (Default: true)
- **displayResult**: [Optional] Whether or not to print the SKU recommendation results to the console. (Default: true)
- **outputFolder**: [Optional] Folder which performance data, reports, and logs will be written to/read from. (Default: current directory)

To run the SKU recommendations using a .JSON configuration file, run the executable without an action but provide a value for ConfigFile, as follows:

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

Below are some sample invocations:

**Sample 1: Getting SKU recommendations  for Azure SQL Database.**

```
.\SqlAssessment.exe GetSkuRecommendation 
--outputFolder C:\Output 
--targetPlatform AzureSqlDatabase
```

**Sample 2: Getting SKU recommendations  for Azure SQL Managed Instance.**

```
.\SqlAssessment.exe GetSkuRecommendation 
--outputFolder C:\Output 
--targetPlatform AzureSqlManagedInstance
```

**Sample 3: Getting SKU recommendations  for Azure SQL Virtual Machine.**

```
.\SqlAssessment.exe GetSkuRecommendation 
--outputFolder C:\Output 
--targetPlatform AzureSqlVirtualMachine
```

The following is an example output of an Azure SQL Database recommendation:

![PowerShell single-db file shown in DMA folder](../dma/media/dma-sku-recommend-single-db-recommendations.png)

The following is an example output of an Azure SQL Managed Instance recommendation:

![PowerShell managed instance file shown in DMA folder](../dma/media/dma-sku-recommend-mi-recommendations.png)

The following is an example output of a SQL Server on Azure VM recommendation:

-**insertphoto**

The output of the SKU recommendations covers the following sections:
- **Instance Name**: Name of the on-premises SQL Server instance(s)
- **Database Name**: Name of the on-premises SQL Server database(s)
- **SKU Recommendation**: The minimum cost-efficient SKU offering among all the performance eligible SKUs that could accommodate your workloads.
- **Recommendation Reason**: For each tier that is recommended, we provide the reasons and collected data values driving the recommendations.

The final recommended tier and configuration values for that tier reflect the minimum SKU required for your queries to run in Azure with a success rate similar to your on-premises databases. For example, if the recommended minimum SKU is the S4 standard tier, then choosing S3 or below may cause queries to time out or fail to execute.


## Next step

- For a complete listing of commands for running DMA from the CLI, see the article [Run Data Migration Assistant from the command line](./dma-commandline.md).
