---
title: "Lesson 4: Analyze Data in Local Compute Context (Data Science Deep Dive) | Microsoft Docs"
ms.custom: ""
ms.date: "10/03/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016"
dev_langs: 
  - "R"
ms.assetid: 787bb526-4a13-40fa-9343-75d3bf5ba6a2
caps.latest.revision: 13
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Lesson 4: Analyze Data in Local Compute Context (Data Science Deep Dive)
Although typically it is much faster to run complex R code using the server context, it is sometimes more convenient to get your data out of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and analyze it on your private workstation.  
  
In this section, you'll learn how to switch back to a local compute context, and move data between contexts to optimize performance.  
  
## Create a Local Summary  
  
1.  Change the compute context to do all your work locally.  
  
    ```R  
    rxSetComputeContext ("local")    
    ```  
  
2.  When extracting data from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can often get better performance by increasing the number of rows extracted for each read.  To do this, increase the value for the *rowsPerRead* parameter on the data source.  
  
    ```R  
    sqlServerDS1 <- RxSqlServerData(  
       connectionString = sqlConnString,        
       table = sqlFraudTable,   
       colInfo = ccColInfo,   
       rowsPerRead = 10000)  
    ```  
  
    Previously, the value of *rowsPerRead* was set to 5000.  
  
3.  Now, call *rxSummary* on the new data source.  
  
    ```R  
    rxSummary(formula = ~gender + balance + numTrans + numIntlTrans + creditLine, data = sqlServerDS1)    
    ```  
  
    The actual results should be the same as when you run *rxSummary* in the context of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer.  However, the operation might be faster or slower. Much depends on the connection to your database, because the data is being transferred to your local computer for analysis.  
  

## Next  Step  
[Move Data between SQL Server and XDF File &#40;Data Science Deep Dive&#41;](../../advanced-analytics/r-services/lesson-4-1-move-data-between-sql-server-and-xdf-file.md)  
  
## Previous Step  
[Perform Chunking Analysis using rxDataStep &#40;Data Science Deep Dive&#41;](../../advanced-analytics/r-services/lesson-3-3-perform-chunking-analysis-using-rxdatastep.md)  
  
  
  

