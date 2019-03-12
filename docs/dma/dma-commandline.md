---
title: "Run Data Migration Assistant from the command line (SQL Server) | Microsoft Docs"
description: Learn how to run Data Migration Assistant from the command line to assess SQL Server databases for migration
ms.custom: ""
ms.date: "03/12/2019"
ms.prod: sql
ms.prod_service: "dma"
ms.reviewer: ""
ms.technology: dma
ms.topic: conceptual
keywords: ""
helpviewer_keywords: 
  - "Data Migration Assistant, Command Line"
ms.assetid: ""
author: HJToland3
ms.author: rajpo
manager: craigg
---

# Run Data Migration Assistant from the command line
With version 2.1 and above, when you install Data Migration Assistant, it will also install dmacmd.exe in *%ProgramFiles%\\Microsoft Data Migration Assistant\\*. Use dmacmd.exe to assess your databases in an unattended mode, and output the result to JSON or CSV file. This method is especially useful when assessing several databases or huge databases. 

> [!NOTE]
> Dmacmd.exe supports running assessments only. Migrations are not supported at this time.


## Assessments using the Command Line Interface (CLI)

```
DmaCmd.exe /AssessmentName="string"
/AssessmentDatabases="connectionString1" \["connectionString2"\]
\[/AssessmentTargetPlatform="TargetPlatform"\]
/AssessmentEvaluateRecommendations|/AssessmentEvaluateCompatibilityIssues
\[/AssessmentOverwriteResult\]
/AssessmentResultJson="file"|/AssessmentResultCsv="file"
```

|Argument  |Description  | Required (Y/N)
|---------|---------|---------------|
| `/help or /?`     | How to use dmacmd.exe help text        | N
|`/AssessmentName`     |   Name of the assessment project   | Y
|`/AssessmentDatabases`     | Space-delimited list of connection strings. Database name (Initial Catalog) is case-sensitive. | Y
|`/AssessmentTargetPlatform`     | Target platform for the assessment, supported values: AzureSqlDatabase, ManagedSqlServer, SqlServer2012, SqlServer2014, SqlServer2016, SqlServerLinux2017 and SqlServerWindows2017. Default is SqlServerWindows2017   | N
|`/AssessmentEvaluateFeatureParity`  | Run feature parity rules  | N
|`/AssessmentEvaluateCompatibilityIssues`     | Run compatibility rules  | Y <br> (Either AssessmentEvaluateCompatibilityIssues or AssessmentEvaluateRecommendations is required.)
|`/AssessmentEvaluateRecommendations`     | Run feature recommendations        | Y <br> (Either AssessmentEvaluateCompatibilityIssues or AssessmentEvaluateRecommendationsis required)
|`/AssessmentOverwriteResult`     | Overwrite the result file    | N
|`/AssessmentResultJson`     | Full path to the JSON result file     | Y <br> (Either AssessmentResultJson or AssessmentResultCsv is required)
|`/AssessmentResultCsv`    | Full path to the CSV result file   | Y <br>(Either AssessmentResultJson or AssessmentResultCsv is required)
|`/Action`    | Use SkuRecommendation to get SKU recommendations, use AssessTargetReadiness to perform target readiness assessment.   | N
|`/SourceConnections`    | Space delimited list of connection strings. Database name (Initial Catalog) is optional. If no database name is provided, then all databases on the source are assessed.   | Y <br>(Required if Action is 'AssessTargetReadiness')
|`/TargetReadinessConfiguration`    | Full path to the XML file describing values for the name, source connections and result file.   | Y <br>(Either TargetReadinessConfiguration or SourceConnections is required)
|`/FeatureDiscoveryReportJson`    | Path to the feature discovery JSON report. If this file is generated, then it can be used to run target readiness assessment again without connecting to source.   | N
|`/ImportFeatureDiscoveryReportJson`    | Path to the feature discovery JSON report created earlier. Instead of source connections, this file will be used.   | N

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

**Single-database assessment using SQL Server authentication and running feature recommendation**

```
DmaCmd.exe /AssessmentName="TestAssessment"
/AssessmentDatabases="Server=SQLServerInstanceName;Initial
Catalog=DatabaseName;User Id=myUsername;Password=myPassword;"
/AssessmentEvaluateRecommendations /AssessmentOverwriteResult
/AssessmentResultCsv="C:\\temp\\Results\\AssessmentReport.csv"
```

**Single-database assessment for target platform SQL Server 2012, save results to .json and .csv file**

```
DmaCmd.exe /AssessmentName="TestAssessment"
/AssessmentDatabases="Server=SQLServerInstanceName;Initial
Catalog=DatabaseName;Integrated Security=true"
/AssessmentTargetPlatform="SqlServer2012"
/AssessmentEvaluateRecommendations /AssessmentOverwriteResult
/AssessmentResultJson="C:\\temp\\Results\\AssessmentReport.json"
/AssessmentResultCsv="C:\\temp\\Results\\AssessmentReport.csv"
```

**Single-database assessment for target platform SQL Azure Database, save results to .json and .csv file**

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
/SourceConnections="Server=SQLServerInstanceName;Initial Catalog=DatabaseName;User Id=myUsername;Password=myPassword;" /AssessmentEvaluateRecommendations 
/AssessmentOverwriteResult 
/AssessmentResultJson="C:\temp\Results\AssessmentReport.json" 

```

**Multiple-database Target Readiness assessment**

```
DmaCmd.exe /Action=AssessTargetReadiness 
/AssessmentName="TestAssessment" 
/SourceConnections="Server=SQLServerInstanceName1;Initial Catalog=DatabaseName1;Integrated Security=true" "Server=SQLServerInstanceName1;Initial Catalog=DatabaseName2;Integrated Security=true" "Server=SQLServerInstanceName2;Initial Catalog=DatabaseName3;Integrated Security=true" 
/AssessmentOverwriteResult  
/AssessmentResultJson="C:\Results\test2016.json"

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
<TargetReadinessConfiguration xmlns="http://microsoft.com/schemas/SqlServer/Advisor/TargetReadinessConfiguration">
  <AssessmentName>name</AssessmentName>
  <ImportFeatureDiscoveryReportJson>path\to\featurediscoveryfile.json</ImportFeatureDiscoveryReportJson>
  <AssessmentResultJson>path\to\resultfile.json</AssessmentResultJson>
  <OverwriteResult>true</OverwriteResult><!-- or false -->
</TargetReadinessConfiguration>
```

## Azure SQL Database SKU recommendations using the CLI

```
.\DmaCmd.exe /Action=SkuRecommendation
/SkuRecommendationInputDataFilePath="C:\TestOut\out.csv"
/SkuRecommendationTsvOutputResultsFilePath="C:\TestOut\prices.tsv"
/SkuRecommendationJsonOutputResultsFilePath="C:\TestOut\prices.json"
/SkuRecommendationOutputResultsFilePath="C:\TestOut\prices.html"
/SkuRecommendationPreventPriceRefresh=true 
```

|Argument  |Description  | Required (Y/N)
|---------|---------|---------------|
|`/Action=SkuRecommendation` | Execute SKU assessment using DMA command line | Y
|`/SkuRecommendationInputDataFilePath`	| Full path to the performance counter file collected from the computer hosting your databases |	Y
|`/SkuRecommendationTsvOutputResultsFilePath`	| Full path to the TSV result file |	Y <br>(Either TSV or JSON or HTML file path is required)
|`/SkuRecommendationJsonOutputResultsFilePath`	| Full path to the JSON result file |	Y <br>(Either TSV or JSON or HTML file path is required)
|`/SkuRecommendationHtmlResultsFilePath` |	Full path to the HTML result file |	Y <br>(Either TSV or JSON or HTML file path is required)
|`/SkuRecommendationPreventPriceRefresh` |	Prevents the price refresh from occurring. Use if running in offline mode. |	Y <br>(Either this argument is selected for static prices or all the arguments below need to be selected for getting latest prices)
|`/SkuRecommendationCurrencyCode` |	The currency in which to display prices (e.g. "USD") | Y <br>(If you want to get the latest prices)
|`/SkuRecommendationOfferName` |	The offer name (e.g. "MS-AZR-0003P"). For more information, see the [Microsoft Azure Offer Details](https://azure.microsoft.com/support/legal/offer-details/) page. |	Y <br>(If you want to get the latest prices)
|`/SkuRecommendationRegionName` |	The region name (e.g. "WestUS") |	Y <br>(If you want to get the latest prices)
|`/SkuRecommendationSubscriptionId` | The subscription ID. |	Y <br>(If you want to get the latest prices)
|`/AzureAuthenticationTenantId` | The authentication tenant. |	Y <br>(If you want to get the latest prices)
|`/AzureAuthenticationClientId` | The client ID of the AAD app used for authentication. | Y <br>(If you want to get the latest prices)
|`/AzureAuthenticationInteractiveAuthentication`	| Set to true to pop up the window. |	Y <br>(If you want to get the latest prices) <br>(Pick one of the 3 authentication options - option 1)
|`/AzureAuthenticationCertificateStoreLocation`	| Set to the certificate store location (e.g. "CurrentUser"). |	Y <br>(If you want to get the latest prices) <br>(Pick one of the 3 authentication options - option 2)
|`/AzureAuthenticationCertificateThumbprint`	| Set to the cert thumbprint. |	Y <br>(If you want to get the latest prices) <br>(Pick one of the 3 authentication options - option 2)
|`/AzureAuthenticationToken` |	Set to the certificate token. |	Y <br>(If you want to get the latest prices) <br>(Pick one of the 3 authentication options - option 3)

## Examples of SKU assessments using the CLI

**Dmacmd.exe**

  `Dmacmd.exe /? or DmaCmd.exe /help`

**Azure SQL DB SKU recommendation with price refresh (get latest prices) - Interactive authentication** 
```
.\DmaCmd.exe /Action=SkuRecommendation
/SkuRecommendationInputDataFilePath="C:\TestOut\out.csv"
/SkuRecommendationTsvOutputResultsFilePath="C:\TestOut\prices.tsv"
/SkuRecommendationJsonOutputResultsFilePath="C:\TestOut\prices.json"
/SkuRecommendationOutputResultsFilePath="C:\TestOut\prices.html"
/SkuRecommendationCurrencyCode=USD
/SkuRecommendationOfferName=MS-AZR-0044p
/SkuRecommendationRegionName=UKWest
/SkuRecommendationSubscriptionId=<Your Subscription Id>
/AzureAuthenticationClientId=<Your AzureAuthenticationClientId>
/AzureAuthenticationTenantId=<Your AzureAuthenticationTenantId>
/AzureAuthenticationInteractiveAuthentication=true 
```

**Azure SQL DB SKU recommendation with price refresh (get latest prices) - Certificate authentication**
```
.\DmaCmd.exe /Action=SkuRecommendation
/SkuRecommendationInputDataFilePath="C:\TestOut\out.csv"
/SkuRecommendationTsvOutputResultsFilePath="C:\TestOut\prices.tsv"
/SkuRecommendationJsonOutputResultsFilePath="C:\TestOut\prices.json"
/SkuRecommendationOutputResultsFilePath="C:\TestOut\prices.html"
/SkuRecommendationCurrencyCode=USD
/SkuRecommendationOfferName=MS-AZR-0044p
/SkuRecommendationRegionName=UKWest
/SkuRecommendationSubscriptionId=<Your Subscription Id>
/AzureAuthenticationClientId=<Your AzureAuthenticationClientId>
/AzureAuthenticationTenantId=<Your AzureAuthenticationTenantId>
/AzureAuthenticationCertificateStoreLocation=<Your Certificate Store Location>
/AzureAuthenticationCertificateThumbprint=<Your Certificate Thumbprint>  
```

**Azure SQL DB SKU recommendation with price refresh (get latest prices) - Token authentication**  
```
.\DmaCmd.exe /Action=SkuRecommendation
/SkuRecommendationInputDataFilePath="C:\TestOut\out.csv"
/SkuRecommendationTsvOutputResultsFilePath="C:\TestOut\prices.tsv"
/SkuRecommendationJsonOutputResultsFilePath="C:\TestOut\prices.json"
/SkuRecommendationOutputResultsFilePath="C:\TestOut\prices.html"
/SkuRecommendationCurrencyCode=USD
/SkuRecommendationOfferName=MS-AZR-0044p
/SkuRecommendationRegionName=UKWest
/SkuRecommendationSubscriptionId=<Your Subscription Id>
/AzureAuthenticationClientId=<Your AzureAuthenticationClientId>
/AzureAuthenticationTenantId=<Your AzureAuthenticationTenantId>
/AzureAuthenticationToken=<Your Authentication Token> 
```

**Azure SQL DB SKU recommendation without price refresh (use static prices)** 
```
.\DmaCmd.exe /Action=SkuRecommendation
/SkuRecommendationInputDataFilePath="C:\TestOut\out.csv"
/SkuRecommendationTsvOutputResultsFilePath="C:\TestOut\prices.tsv"
/SkuRecommendationJsonOutputResultsFilePath="C:\TestOut\prices.json"
/SkuRecommendationOutputResultsFilePath="C:\TestOut\prices.html"
/SkuRecommendationPreventPriceRefresh=true  
```

## See also
- [Data Migration Assistant](https://aka.ms/get-dma) download.
- The article [Identify the right Azure SQL Database SKU for your on-premises database](https://aka.ms/dma-sku-recommend-sqldb).
