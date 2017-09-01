---
title: "Run from command line (SQL Server Data Migration Assistant) | Microsoft Docs"
ms.custom: 
ms.date: "09/01/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "sql-dma"
ms.tgt_pltfrm: ""
ms.topic: "article"
keywords: ""
helpviewer_keywords: 
  - "Data Migration Assistant, Command Line"
ms.assetid: ""
caps.latest.revision: ""
author: "sabotta"
ms.author: "carlasab"
manager: "craigg"
---

# Run Data Migration Assistant from the command line
With version 2.1 and above, when you install Data Migration Assistant, it will also install dmacmd.exe in *%ProgramFiles%\\Microsoft Data Migration Assistant\\. Use dmacmd.exe to assess your databases in an unattended mode, and output the result to JSON or CSV file. This is especially useful when assessing several databases or huge databases. 

> [!NOTE]
> Dmacmd.exe supports running assessments only. Migrations are not supported at this time.


## Command-line arguments

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
|`/AssessmentDatabases`     | Space delimited list of connection strings. Database name (Initial Catalog) is case sensitive. | Y
|`/AssessmentTargetPlatform`     | Target platform for the assessment, supported values: SqlServer2012, SqlServer2014, SqlServer2016 and AzureSqlDatabaseV12. Default is SqlServer2016   | N
|`/AssessmentEvaluateFeatureParity`  | Run feature parity rules  | N
|`/AssessmentEvaluateCompatibilityIssues`     | Run compatibility rules  | Y <br> (Either AssessmentEvaluateCompatibilityIssues or AssessmentEvaluateRecommendations is required.)
|`/AssessmentEvaluateRecommendations`     | Run feature recommendations        | Y <br> (Either AssessmentEvaluateCompatibilityIssues or AssessmentEvaluateRecommendationsis required)
|`/AssessmentOverwriteResult`     | Overwrite the result file    | N
|`/AssessmentResultJson`     | Full path to the JSON result file     | Y <br> (Either AssessmentResultJson or AssessmentResultCsv is required)
|`/AssessmentResultCsv`    | Full path to the CSV result file   | Y <br>(Either AssessmentResultJson or AssessmentResultCsv is required)




## Examples

**Dmacmd.exe**

  `Dmacmd.exe /? or DmaCmd.exe /help`

**Single-database assessment using Windows authentication and running compatibility rules**

```
DmaCmd.exe /AssessmentName="TestAssessment"
/AssessmentDatabases="Server=SQLServerInstanceName;Initial
Catalog=DatabaseName;***Integrated Security=true*"**
***/AssessmentEvaluateCompatibilityIssues*** /AssessmentOverwriteResult
/AssessmentResultJson="C:\\temp\\Results\\AssessmentReport.json"
```



**Single-database assessment using SQL Server authentication and running feature recommendation**

```
DmaCmd.exe /AssessmentName="TestAssessment"
/AssessmentDatabases="Server=SQLServerInstanceName;Initial
Catalog=DatabaseName;***User Id=myUsername;Password=myPassword;***"
***/AssessmentEvaluateRecommendations*** /AssessmentOverwriteResult
/AssessmentResultCsv="C:\\temp\\Results\\AssessmentReport.csv"
```


**Single-database assessment for target platform SQL Server 2012 and save results to .json and .csv file**

```
DmaCmd.exe /AssessmentName="TestAssessment"
/AssessmentDatabases="Server=SQLServerInstanceName;Initial
Catalog=DatabaseName;Integrated Security=true"
***/AssessmentTargetPlatform="SqlServer2012"***
/AssessmentEvaluateRecommendations /AssessmentOverwriteResult
***/AssessmentResultJson***="C:\\temp\\Results\\AssessmentReport.json"
***/AssessmentResultCsv***="C:\\temp\\Results\\AssessmentReport.csv"
```


**Single-database assessment for target platform SQL Azure Database save results to .json and .csv file**

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
***/AssessmentDatabases="Server=SQLServerInstanceName1;Initial
Catalog=DatabaseName1;Integrated Security=true"
"Server=SQLServerInstanceName1;Initial Catalog=DatabaseName2;Integrated
Security=true" "Server=SQLServerInstanceName2;Initial
Catalog=DatabaseName3;Integrated Security=true"***
/AssessmentTargetPlatform="SqlServer2016"
/AssessmentEvaluateCompatibilityIssues /AssessmentOverwriteResult
/AssessmentResultCsv="C:\\temp\\Results\\AssessmentReport.csv"
/AssessmentResultJson="C:\\Results\\test2016.json"
```



## See also

[Data Migration Assistant Download](https://www.microsoft.com/download/details.aspx?id=53595)
