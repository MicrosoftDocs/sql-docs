---
title: "Load Data into Memory using rxImport (Data Science Deep Dive) | Microsoft Docs"
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
ms.assetid: 47a42e9a-05a0-4a50-871d-de73253cf070
caps.latest.revision: 14
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Lesson 3-1 - Load Data into Memory using rxImport
The *rxImport* function can be used to move data from a data source into a data frame in R session memory, or into an XDF file on disk. If you don't specify a file as destination, data is put into memory as a data frame.  
  
In this step, you'll learn how to get data from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and then use the *rxImport* function to put the data of interest into a local file. That way, you can analyze it in the local compute context repeatedly, without having to re-query the database.  
  
## Extract a Subset of Data from SQL Server to Local Memory  
You've decided that  you want to examine only the high risk individuals in more detail. The source table in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is big, so you will get the information about just the high-risk customers, and load it into a data frame in the memory of the local workstation.  
  
1.  Reset the compute context to your local workstation.  
  
    ```R  
    rxSetComputeContext("local")   
    ```  
  
2.  Create a new SQL Server data source object, providing a valid SQL statement in the *sqlQuery* parameter. This example gets a subset of the observations with the highest risk scores. That way, only the data you really need is put in local memory.  
  
    ```R    
    sqlServerProbDS \<- RxSqlServerData(       
        sqlQuery = paste("SELECT * FROM ccScoreOutput2",  
        "WHERE (ccFraudProb > .99)"),
        connectionString = sqlConnString)   
    ```  
  
3.  You use the function *rxImport* to actually load the data into a data frame in the local R session.  
  
    ```R  
    highRisk <- rxImport(sqlServerProbDS)   
    ```  
     If the operation was successful, you should see a status message:
   Rows Read: 35, Total Rows Processed: 35, Total Chunk Time: 0.036 seconds 
   
4.  Now that you have the high-risk observations in a data frame in memory, you can use various R functions to manipulate the data frame. For example, you can order customers by their risk score, and print the customers who pose the highest risk.  
  
    ```R  
    orderedHighRisk <- highRisk[order(-highRisk$ccFraudProb),]   
    row.names(orderedHighRisk) <- NULL    
    head(orderedHighRisk)  
  
    ```  
  
 **Results**  
  
 *ccFraudLogitScore   state gender cardholder balance numTrans numIntlTrans creditLine ccFraudProb1*  
*9.786345    SD   Male  Principal   23456       25            5 75   0.99994382*  
*9.433040    FL Female  Principal   20629       24           28 75   0.99992003*  
*8.556785    NY Female  Principal   19064       82           53 43   0.99980784*  
*8.188668    AZ Female  Principal   19948       29            0 75   0.99972235*  
*7.551699    NY Female  Principal   11051       95            0 75   0.99947516*  
*7.335080    NV   Male  Principal   21566        4            6  75   0.9993482*  
  
## More about rxImport  
You can use *rxImport* not just to move data, but to transform data in the process of reading it. For example, you can specify the number of characters for fixed-width columns, provide a description of the variables, set levels for factor columns, and even create new levels to use after importing.  
  
The *rxImport* function assigns variable names to the columns during the import process, but you can indicate new variable names by using the *colInfo* parameter, and you can change data types using the *colClasses* parameter.  
  
By specifying additional operations in the *transforms* parameter, you can do elementary processing on each chunk of data that is read.  
  
## Next Step  
[Create New SQL Server Table using rxDataStep &#40;Data Science Deep Dive&#41;](../../advanced-analytics/r-services/lesson-3-2-create-new-sql-server-table-using-rxdatastep.md)  
  
## Previous Step  
[Lesson 3: Transform Data Using R &#40;Data Science Deep Dive&#41;](../../advanced-analytics/r-services/lesson-3-transform-data-using-r-data-science-deep-dive.md)  
  
## See Also  
[SQL Server R Services Tutorials](../../advanced-analytics/r-services/sql-server-r-services-tutorials.md)  
  
  
  

