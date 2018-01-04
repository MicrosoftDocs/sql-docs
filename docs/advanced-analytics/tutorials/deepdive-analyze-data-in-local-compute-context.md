---
title: "Analyze data in local compute context (SQL and R deep dive) | Microsoft Docs"
ms.date: "12/18/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: 
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "tutorial"
applies_to: 
  - "SQL Server 2016"
  - "SQL Server 2017"
dev_langs: 
  - "R"
ms.assetid: 787bb526-4a13-40fa-9343-75d3bf5ba6a2
caps.latest.revision: 13
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "Inactive"
---
# Analyze data in local compute context (SQL and R deep dive)

This article is part of the Data Science Deep Dive tutorial, on how to use [RevoScaleR](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) with SQL Server.

In this section, you learn how to switch back to a local compute context, and move data between contexts to optimize performance.

Although i might be faster to run complex R code using the server context, sometimes it is more convenient to get your data out of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and analyze it on a local workstation.

## Create a local summary

1. Change the compute context to do all your work locally.
  
    ```R
    rxSetComputeContext ("local")
    ```
  
2. When extracting data from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can often get better performance by increasing the number of rows extracted for each read.  To do this, increase the value for the *rowsPerRead* parameter on the data source. Previously, the value of *rowsPerRead* was set to 5000.
  
    ```R
    sqlServerDS1 <- RxSqlServerData(
       connectionString = sqlConnString,
       table = sqlFraudTable,
       colInfo = ccColInfo,
       rowsPerRead = 10000)
    ```

3. Call **rxSummary** on the new data source.
  
    ```R
    rxSummary(formula = ~gender + balance + numTrans + numIntlTrans + creditLine, data = sqlServerDS1)
    ```
  
    The actual results should be the same as when you run **rxSummary** in the context of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer.  However, the operation might be faster or slower. Much depends on the connection to your database, because the data is being transferred to your local computer for analysis.

## Next step

[Move data between SQL Server and XDF File](../../advanced-analytics/tutorials/deepdive-move-data-between-sql-server-and-xdf-file.md)

## Previous step

[Perform chunking analysis using rxDataStep](../../advanced-analytics/tutorials/deepdive-perform-chunking-analysis-using-rxdatastep.md)
