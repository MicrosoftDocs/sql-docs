---
title: "Run Data Migration Assistant from the command line"
description: Learn how to run Data Migration Assistant from the command line to assess SQL Server databases for migration
author: rajeshsetlem
ms.author: rajpo
ms.date: "05/06/2019"
ms.service: sql
ms.subservice: dma
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "Data Migration Assistant, Command Line"
---

# Run Data Migration Assistant from the command line

With version 2.1 and above, when you install Data Migration Assistant, it will also install dmacmd.exe in *%ProgramFiles%\\Microsoft Data Migration Assistant\\*. Use dmacmd.exe to assess your databases in an unattended mode, and output the result to JSON or CSV file. This method is especially useful when assessing several databases or huge databases. 

> [!NOTE]
> Dmacmd.exe supports running assessments only. Migrations are not supported at this time.

## Assessments using the Command Line Interface (CLI)

```
DmaCmd.exe /AssessmentName="string"
/AssessmentDatabases="connectionString1" \["connectionString2"\]
\[/AssessmentSourcePlatform="SourcePlatform"]
\[/AssessmentTargetPlatform="TargetPlatform"\]
/AssessmentEvaluateFeatureParity|/AssessmentEvaluateCompatibilityIssues
\[/AssessmentOverwriteResult\]
/AssessmentResultJson="file"|/AssessmentResultCsv="file"
```

|Argument  |Description  | Required (Y/N)
|---------|---------|---------------|
| `/help or /?`     | How to use dmacmd.exe help text        | N
|`/AssessmentName`     |   Name of the assessment project   | Y
|`/AssessmentDatabases`     | Space-delimited list of connection strings. Database name (Initial Catalog) is case-sensitive. | Y
|`/AssessmentSourcePlatform`     | Source platform for the assessment: <br>Supported values for Assessment: SqlOnPrem, RdsSqlServer (default) <br>Supported values for Target Readiness Assessment: SqlOnPrem, RdsSqlServer (default), Cassandra (preview)   | N
|`/AssessmentTargetPlatform`     | Target platform for the assessment:  <br> Supported values for Assessment: AzureSqlDatabase, ManagedSqlServer, SqlServer2012, SqlServer2014, SqlServer2016, SqlServerLinux2017 and SqlServerWindows2017 (default)  <br> Supported values for Target Readiness Assessment: ManagedSqlServer (default), CosmosDB (preview)   | N
|`/AssessmentEvaluateFeatureParity`  | Run feature parity rules. If source platform is RdsSqlServer, feature parity evaluation is not supported for target platform AzureSqlDatabase  | Y <br> (Either AssessmentEvaluateCompatibilityIssues or AssessmentEvaluateFeatureParity is required.)
|`/AssessmentEvaluateCompatibilityIssues`     | Run compatibility rules  | Y <br> (Either AssessmentEvaluateCompatibilityIssues or AssessmentEvaluateFeatureParity is required.)
|`/AssessmentOverwriteResult`     | Overwrite the result file    | N
|`/AssessmentResultJson`     | Full path to the JSON result file     | Y <br> (Either AssessmentResultJson or AssessmentResultCsv is required)
|`/AssessmentResultCsv`    | Full path to the CSV result file   | Y <br> (Either AssessmentResultJson or AssessmentResultCsv is required)
|`/AssessmentResultDma`    | Full path to the dma result file   | N
|`/Action`    | Use SkuRecommendation to get SKU recommendations. <br> Use AssessTargetReadiness to perform target readiness assessment. <br> Use AzureMigrateUpload to upload all DMA assessment files in the AzzessmentResultInputFolder to bulk upload to Azure Migrate.Action type usage /Action= AzureMigrateUpload   | N
|`/SourceConnections`    | Space delimited list of connection strings. Database name (Initial Catalog) is optional. If no database name is provided, then all databases on the source are assessed.   | Y <br> (Required if Action is 'AssessTargetReadiness')
|`/TargetReadinessConfiguration`    | Full path to the XML file describing values for the name, source connections and result file.   | Y <br> (Either TargetReadinessConfiguration or SourceConnections is required)
|`/FeatureDiscoveryReportJson`    | Path to the feature discovery JSON report. If this file is generated, then it can be used to run target readiness assessment again without connecting to source. | N
|`/ImportFeatureDiscoveryReportJson`    | Path to the feature discovery JSON report created earlier. Instead of source connections, this file will be used.   | N
|`/EnableAssessmentUploadToAzureMigrate`    | Enables uploading and publishing assessment results to Azure Migrate   | N
|`/AzureCloudEnvironment`    |Selects the Azure cloud environment to connect to, default is Azure Public Cloud. Supported values: Azure (default), AzureChina, AzureGermany, AzureUSGovernment.   | N 
|`/SubscriptionId`    |Azure subscription ID.   | Y <br> (Required if EnableAssessmentUploadToAzureMigrate argument is specified)
|`/AzureMigrateProjectName`    |The Azure Migrate Project name to upload assessment results to.   | Y <br> (Required if EnableAssessmentUploadToAzureMigrate argument is specified)
|`/ResourceGroupName`    |Azure Migrate resource group name.   | Y <br> (Required if EnableAssessmentUploadToAzureMigrate argument is specified)
|`/AssessmentResultInputFolder`    |The input folder path containing .DMA assessment files to upload to Azure Migrate.   | Y <br> (Required if Action is AzureMigrateUpload)



## Examples of assessments using the CLI

**Dmacmd.exe**

  `Dmacmd.exe /? or DmaCmd.exe /help`

**Single-database assessment using Windows authentication and running compatibility rules**

```
DmaCmd.exe /AssessmentName="TestAssessment"
/AssessmentDatabases="Server=SQLServerInstanceName;Initial
Catalog=DatabaseName;Integrated Security=true"
/AssessmentEvaluateCompatibilityIssues /AssessmentOverwriteResult
/AssessmentResultJson="C:\\temp\\Results\\AssessmentReport.json"
```

**Single-database assessment using SQL Server authentication and running feature parity**

```
DmaCmd.exe /AssessmentName="TestAssessment"
/AssessmentDatabases="Server=SQLServerInstanceName;Initial
Catalog=DatabaseName;User Id=myUsername;Password=myPassword;"
/AssessmentEvaluateFeatureParity /AssessmentOverwriteResult
/AssessmentResultCsv="C:\\temp\\Results\\AssessmentReport.csv"
```

**Single-database assessment for target platform SQL Server 2012, save results to .json and .csv file**

```
DmaCmd.exe /AssessmentName="TestAssessment"
/AssessmentDatabases="Server=SQLServerInstanceName;Initial
Catalog=DatabaseName;Integrated Security=true"
/AssessmentTargetPlatform="SqlServer2012"
/AssessmentEvaluateFeatureParity /AssessmentOverwriteResult
/AssessmentResultJson="C:\\temp\\Results\\AssessmentReport.json"
/AssessmentResultCsv="C:\\temp\\Results\\AssessmentReport.csv"
```

**Single-database assessment for target platform Azure SQL Database, save results to .json and .csv file**

```
DmaCmd.exe /AssessmentName="TestAssessment" 
/AssessmentDatabases="Server=SQLServerInstanceName;Initial
Catalog=DatabaseName;Integrated Security=true"
/AssessmentTargetPlatform="AzureSqlDatabaseV12"
/AssessmentEvaluateCompatibilityIssues /AssessmentEvaluateFeatureParity
/AssessmentOverwriteResult 
/AssessmentResultCsv="C:\\temp\\AssessmentReport.csv" 
/AssessmentResultJson="C:\\temp\\AssessmentReport.json"
```

**Multiple-database assessment**

```
DmaCmd.exe /AssessmentName="TestAssessment"
/AssessmentDatabases="Server=SQLServerInstanceName1;Initial
Catalog=DatabaseName1;Integrated Security=true"
"Server=SQLServerInstanceName1;Initial Catalog=DatabaseName2;Integrated
Security=true" "Server=SQLServerInstanceName2;Initial
Catalog=DatabaseName3;Integrated Security=true"
/AssessmentTargetPlatform="SqlServer2016"
/AssessmentEvaluateCompatibilityIssues /AssessmentOverwriteResult
/AssessmentResultCsv="C:\\temp\\Results\\AssessmentReport.csv"
/AssessmentResultJson="C:\\Results\\test2016.json"
```

**Single-database Target Readiness assessment using Windows authentication**

```
DmaCmd.exe /Action=AssessTargetReadiness 
/AssessmentName="TestAssessment" 
/SourceConnections="Server=SQLServerInstanceName;Initial Catalog=DatabaseName;Integrated Security=true" 
/AssessmentOverwriteResult 
/AssessmentResultJson="C:\temp\Results\AssessmentReport.json"
```

**Single-database Target Readiness assessment using SQL Server authentication**

```
DmaCmd.exe /Action=AssessTargetReadiness 
/AssessmentName="TestAssessment" 
/SourceConnections="Server=SQLServerInstanceName;Initial Catalog=DatabaseName;User Id=myUsername;Password=myPassword;" /AssessmentEvaluateFeatureParity 
/AssessmentOverwriteResult 
/AssessmentResultJson="C:\temp\Results\AssessmentReport.json" 

```

**Single-database assessment for target platform Azure SQL Database, save results to .json and .csv file**

```
DmaCmd.exe /AssessmentName="TestAssessment" 
/AssessmentDatabases="Server=SQLServerInstanceName;Initial
Catalog=DatabaseName;Integrated Security=true"
/AssessmentSourcePlatform="SqlOnPrem"
/AssessmentTargetPlatform="AzureSqlDatabase"
/AssessmentEvaluateCompatibilityIssues /AssessmentEvaluateFeatureParity
/AssessmentOverwriteResult 
/AssessmentResultCsv="C:\\temp\\AssessmentReport.csv" 
/AssessmentResultJson="C:\\temp\\AssessmentReport.json"

```

**Multiple-database Target Readiness assessment**

```
DmaCmd.exe /Action=AssessTargetReadiness
/AssessmentName="TestAssessment"
/AssessmentSourcePlatform=SourcePlatform
/AssessmentTargetPlatform=TargetPlatform
/SourceConnections="Server=SQLServerInstanceName1;Initial Catalog=DatabaseName1;Integrated Security=true" "Server=SQLServerInstanceName1;Initial Catalog=DatabaseName2;Integrated Security=true" "Server=SQLServerInstanceName2;Initial Catalog=DatabaseName3;Integrated Security=true"
/AssessmentOverwriteResult  
/AssessmentResultJson="C:\Results\test2016.json"

(/AssessmentSourcePlatform and /AssessmentTargetPlatform are optional.)
```

**Target Readiness assessment for all databases on a server using Windows authentication**

```
DmaCmd.exe /Action=AssessTargetReadiness
/AssessmentName="TestAssessment"
/SourceConnections="Server=SQLServerInstanceName;Integrated Security=true"
/AssessmentOverwriteResult
/AssessmentResultJson="C:\temp\Results\AssessmentReport.json"

```

**Target Readiness assessment by importing feature discovery report created earlier**

```
DmaCmd.exe /Action=AssessTargetReadiness
/AssessmentName="TestAssessment"
/ImportFeatureDiscoveryReportJson="c:\temp\feature_report.json" 
/AssessmentOverwriteResult
/AssessmentResultJson="C:\temp\Results\AssessmentReport.json"

```

**Target Readiness assessment by providing configuration file**

```
DmaCmd.exe /Action=AssessTargetReadiness 
/TargetReadinessConfiguration=.\Config.xml
```

Configuration file contents when using source connections:

```
<?xml version="1.0" encoding="utf-8" ?>
<TargetReadinessConfiguration xmlns="http://microsoft.com/schemas/SqlServer/Advisor/TargetReadinessConfiguration">
  <AssessmentName>name</AssessmentName>
  <SourcePlatform>Source Platform</SourcePlatform> <!-- Optional. The default is SqlOnPrem -->
  <TargetPlatform>TargetPlatform</TargetPlatform> <!-- Optional. The default is ManagedSqlServer -->
  <SourceConnections>
    <SourceConnection>connection string 1</SourceConnection>
    <SourceConnection>connection string 2</SourceConnection>
    <!-- ... -->
    <SourceConnection>connection string n</SourceConnection>
  </SourceConnections>
  <AssessmentResultJson>path\to\file.json</AssessmentResultJson>
  <FeatureDiscoveryReportJson>path\to\featurediscoveryreport.json</FeatureDiscoveryReportJson>
  <OverwriteResult>true</OverwriteResult> <!-- or false -->
</TargetReadinessConfiguration>
```

Configuration file contents when importing feature discovery report:

```
<TargetReadinessConfiguration xmlns="https://microsoft.com/schemas/SqlServer/Advisor/TargetReadinessConfiguration">
  <AssessmentName>name</AssessmentName>
  <ImportFeatureDiscoveryReportJson>path\to\featurediscoveryfile.json</ImportFeatureDiscoveryReportJson>
  <AssessmentResultJson>path\to\resultfile.json</AssessmentResultJson>
  <OverwriteResult>true</OverwriteResult><!-- or false -->
</TargetReadinessConfiguration>
```
**Assess and upload to Azure Migrate in Azure Public Cloud (default)**
```
DmaCmd.exe
/Action="Assess" 
/AssessmentSourcePlatform=SqlOnPrem 
/AssessmentTargetPlatform=ManagedSqlServer
/AssessmentEvaluateCompatibilityIssues 
/AssessmentEvaluateFeatureParity 
/AssessmentOverwriteResult 
/AssessmentName="assess-myDatabase"
/AssessmentDatabases="Server=myServer;Initial Catalog=myDatabase;Integrated Security=true" 
/AssessmentResultDma="C:\assessments\results\assess-1.dma"
/SubscriptionId="Subscription Id" 
/AzureMigrateProjectName="Azure Migrate project ame" 
/ResourceGroupName="Resource Group name" 
/AzureAuthenticationInteractiveAuthentication
/AzureAuthenticationTenantId="Azure Tenant Id"
/EnableAssessmentUploadToAzureMigrate

```
**Batch upload DMA assessment files to Azure Migrate in Azure Public Cloud (default)**
```
DmaCmd.exe 
/Action="AzureMigrateUpload" 
/AssessmentResultInputFolder="C:\assessments\results" 
/SubscriptionId="Subscription Id" 
/AzureMigrateProjectName="Azure Migrate project name" 
/ResourceGroupName="Resource Group name" 
/AzureAuthenticationInteractiveAuthentication
/AzureAuthenticationTenantId="Azure Tenant Id"
/EnableAssessmentUploadToAzureMigrate

```
## Azure SQL Database / Azure SQL Managed Instance / SQL Server on Azure VM SKU recommendations using the CLI

With version 5.4 and above, when you install Data Migration Assistant, it will also install SqlAssessment.exe in %ProgramFiles%\Microsoft Data Migration Assistant\SQLAssessmentConsole\. Use SqlAssessment.exe to collect performance data for your SQL instance over an extended period of time, and output the result to JSON or CSV file.

These commands support recommendations for both Azure SQL Database single database, Azure SQL Managed Instance and SQL Server on Azure VM deployment options.

```
.\SqlAssessment.exe GetSkuRecommendation 
--outputFolder C:\Output 
--targetPlatform AzureSqlManagedInstance
```

|Argument  |Description  | Required (Y/N)
|---------|---------|---------------|
|`PerfDataCollection` | Starts collection of performance data. | Y
|`GetSkuRecommendation` | Performs aggregation and analysis of the collected performance data and determines SKU recommendations. | Y
|`GetMetadata` | Performs a metadata collection of the target SQL instance(s), including the number and properties of server instances, databases and database files, user-defined objects, etc. A full report is exported to `MetadataReport.json`. | Y
|`--outputFolder` | Folder which performance data, reports, and logs will be written to/read from. | N <br> (Default: current directory)
|`--sqlConnectionStrings` | Quote-enclosed formal connection string(s) for the target SQL instance(s). | Y
|`--overwrite` | Whether or not to overwrite any existing assessment or SKU recommendations reports. | N <br> ( Default: `true`)
|`--perfQueryIntervalInSec` | Interval at which to query performance data, in seconds. | N <br> (Specific for `PerfDataCollection` action. Default `30`)
|`--staticQueryIntervalInSec` | Interval at which to query and persist static configuration data, in seconds. | N <br> (Specific for `PerfDataCollection` action. Default `30`)
|`--numberOfIterations` | Number of iterations of performance data collection to perform before persisting to file. | N <br> (Specific for `PerfDataCollection` action. Default `20`)
|`--perfQueryIntervalInSec` | Interval at which performance data was queried, in seconds. | N <br> (Specific for `GetSkuRecommendation`action. This must match the value that was originally used during the performance data collection. Default: `30`) 
|`--targetPlatform` | Target platform for SKU recommendation: either `AzureSqlDatabase`, `AzureSqlManagedInstance`, `AzureSqlVirtualMachine`, or `Any`. | N <br> (Specific for `GetSkuRecommendation`action. Default: `Any`)
|`--targetSqlInstance` | Name of the SQL instance that SKU recommendation will be targeting. | N <br> (Specific for `GetSkuRecommendation`action)
|`--targetPercentile` | Percentile of data points to be used during aggregation of the performance data. | N <br> (Specific for `GetSkuRecommendation`action. Only used for baseline (non-elastic) strategy.  Default: `95`)
|`--scalingFactor` | Scaling (comfort) factor used during SKU recommendation. | N <br> (Specific for `GetSkuRecommendation`action. Default: `100`) 
|`--startTime` | UTC start time of performance data points to consider during aggregation, in `"YYYY-MM-DD HH:MM"` format. | N <br> (Specific for `GetSkuRecommendation`action. Only used for baseline (non-elastic) strategy) 
|`--endTime` | UTC end time of performance data points to consider during aggregation, in `"YYYY-MM-DD HH:MM"` format | N <br> (Specific for `GetSkuRecommendation`action. Only used for baseline (non-elastic) strategy) 
|`--elasticStrategy` | Whether or not to use the elastic strategy for SKU recommendations based on statistical resource usage profiling. Elastic strategy is currently available for Azure SQL Databases and SQL Managed Instance, not yet available for SQL Server on Azure VM target. | N <br> (Specific for `GetSkuRecommendation`action. Default: `false`)
|`--databaseAllowList` | Space separated list of names of databases to be included for SKU recommendations | N <br> (Specific for `GetSkuRecommendation`action. Default: `null`)
|`--databaseDenyList` | Space separated list of names of databases to be excluded for SKU recommendations. Only set one of the following or neither: `databaseAllowList`, `databaseDenyList` | N <br> (Specific for `GetSkuRecommendation`action. Default: `null`)
|`--displayResult` | Whether or not to print the SKU recommendation results to the console. Only set one of the following or neither: `databaseAllowList`, `databaseDenyList` | N <br> (Specific for `GetSkuRecommendation`action. Default: `true`)

## Examples of SKU assessments using the CLI

**SqlAssessment.exe**

`SqlAssessment.exe --help`

**Start the data collection process for on-premises SQL Server instances** 

```
.\SqlAssessment.exe PerfDataCollection 
--sqlConnectionStrings "Data Source=Server1;Initial Catalog=master;Integrated Security=True;" "Data Source=Server2;Initial Catalog=master;Integrated Security=True;" 
--outputFolder C:\Output
```

**Azure SQL Database / Azure SQL Managed Instance / SQL Server on Azure VM SKU recommendations**

```
.\SqlAssessment.exe GetSkuRecommendation 
--outputFolder C:\Output 
--targetPlatform Any
```

**Azure SQL Managed Instance SKU recommendations with specific aggregation percentage for data points and custom scaling factor**

```  
.\SqlAssessment.exe GetSkuRecommendation 
--outputFolder C:\Output 
--targetPlatform AzureSqlManagedInstance
--targetPercentile 90
--scalingFactor 80
```

**SQL Server on Azure VM SKU recommendations with custom aggregation timeline**

```
.\SqlAssessment.exe GetSkuRecommendation 
--outputFolder C:\Output 
--targetPlatform AzureSqlVirtualMachine
--startTime "2021-06-05 00:00"
--endTime "2021-06-07 00:00"
```

## See also
- [Data Migration Assistant](https://aka.ms/get-dma) download.
- The article [Identify the right Azure SQL Database SKU for your on-premises database](./dma-sku-recommend-sql-db.md).