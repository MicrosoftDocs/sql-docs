---
title: "Lesson 2: Create and Run R Scripts (Data Science Deep Dive) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "10/26/2016"
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
ms.assetid: 51e8e66f-a0a5-4e96-aa71-f5c870e6d0d4
caps.latest.revision: 18
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Lesson 2: Create and Run R Scripts (Data Science Deep Dive)
Now that you’ve set up your data sources and established one or several compute contexts, you’re ready to run some high-powered R scripts using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  In this lesson, you'll use the server compute context to do some common machine learning tasks:  
  
-   Visualize data and generate some summary statistics    
-   Create a linear regression model    
-   Create a logistic regression model    
-   Score new data and create a histogram of the scores  
  
## Change Compute Context to the Server  
Before you run any R code, you need to specify the *current* or *active* compute context.  
  
1.  To activate a compute context that you've already defined using R, use the *rxSetComputeContext* function as shown here:  
  
    ```R  
    rxSetComputeContext(sqlCompute)   
    ```  
  
    As soon as you run this statement, all subsequent computations will take place on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer specified in the *sqlCompute* parameter.  
  
  
2.  If you decide that you'd rather run the R code on your workstation, you can switch the compute context back to the local computer by using the  **local** keyword.  
  
    ```R  
    rxSetComputeContext ("local")    
    ```  
  
    For a list of other keywords supported by this function, type `help("rxSetComputeContext")` from an R command line.  
  
> [!NOTE]  
> After you've specified a compute context, it remains active until you change it. However, any R scripts that *cannot* be run in a remote server context will be run locally.  
  
## Compute Summary Statistics  
To see how the compute context works, try generating some summary statistics using the *sqlFraudDS* data source.  Remember, the data source object just defines the data you'll use; it doesn't change the compute context.

+ To perform the summary locally, use *rxSetComputeContext* and specify the "local" keyword.
+ To create the same calculations on the SQL Server computer, switch to the SQL compute context you defined earlier.  

  
1.  Call the *rxSummary* function and pass required arguments, such as the formula and the data source, and assign the results to the variable *sumOut*.  
  
    ```R  
    sumOut \<- rxSummary(formula = ~gender + balance + numTrans + numIntlTrans + creditLine, data = sqlFraudDS)  
  
    ```  
  
    The R language provides many summary functions, but *rxSummary* supports execution on various remote compute contexts, including  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] .  For more information about similar functions, see [Data Summaries](https://msdn.microsoft.com/microsoft-r/scaler-user-guide-data-summaries) in the [ScaleR reference](https://msdn.microsoft.com/microsoft-r/scaler/scaler).
  
2.  When processing is done, you can print the contents of the *sumOut* variable to the console.  
  
    ```R  
    sumOut  
    ```  
  
    > [!NOTE]  
    > Don't try to print the results before they have returned from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer, or you might get an error.  
  
  
**Results**  
  
*Summary Statistics Results for: ~gender + balance + numTrans +*   
 *numIntlTrans + creditLine*    
 *Data: sqlFraudDS (RxSqlServerData Data Source)*    
 *Number of valid observations: 10000*    
 *Name  Mean    StdDev  Min Max ValidObs    MissingObs*    
 *balance       4075.0318 3926.558714            0   25626 100000*    
 *numTrans        29.1061   26.619923 0     100 10000    0           100000*    
 *numIntlTrans     4.0868    8.726757 0      60 10000    0           100000*    
 *creditLine       9.1856    9.870364 1      75 10000    0          100000 Category Counts for gender*    
 *Number of categories: 2*    
 *Number of valid observations: 10000*   
 *Number of missing observations: 0*    
 *gender Counts*    
 *Male   6154*    
  *Female 3846*  
  
## Add Maximum and Minimum Values  
Based on the computed summary statistics, you've discovered some useful information about the data that you want to put into the data source for use in further computations. For example, the minimum and maximum values can be used to compute histograms, so you decide to add the high and low values to the *RxSqlServerData* data source.  
  
Fortunately [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] includes optimized functions that can very efficiently convert integer data to categorical factor data.  
  
1.  Start by setting up some temporary variables.  
  
    ```R  
    sumDF <- sumOut$sDataFrame   
    var <- sumDF$Name    
    ```  
  
2.  Use the variable *ccColInfo* that you created earlier to define the columns in the data source.  
  
    You'll also add to some new computed columns (*numTrans*, *numIntlTrans*, and *creditLine*) to the column collection.  
  
    ```R 
    ccColInfo <- list(
        gender = list(type = "factor",  
          levels = c("1", "2"), 
          newLevels = c("Male", "Female")), 
        cardholder = list(type = "factor",  
          levels = c("1", "2"), 
          newLevels = c("Principal", "Secondary")), 
        state = list(type = "factor", 
          levels = as.character(1:51), 
          newLevels = stateAbb), 
        balance  = list(type = "numeric"),
        numTrans = list(type = "factor", 
          levels = as.character(sumDF[var == "numTrans", "Min"]:sumDF[var == "numTrans", "Max"])),
        numIntlTrans = list(type = "factor",  
            levels = as.character(sumDF[var == "numIntlTrans", "Min"]:sumDF[var =="numIntlTrans", "Max"])),
        creditLine = list(type = "numeric")
            )
  
    ```  
  
3.  Having updated the column collection, you can apply the following statement to create an updated version of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source that you defined earlier.  
  
    ```R  
    sqlFraudDS \<- RxSqlServerData(  
        connectionString = sqlConnString,   
        table = sqlFraudTable,   
        colInfo = ccColInfo,        
        rowsPerRead = sqlRowsPerRead)   
    ```  
  
    The *sqlFraudDS* data source now includes the new columns added in *ccColInfo*.  
  
These modifications affect only the data source object in R; no new data has been  written to the database table yet. However, you can use the data captured in the *sumOut* variable to create visualizations and summaries. In the next step you'll learn how to do this while switching compute contexts. 

> [!TIP]
> If you forget which compute context you're using, use `rxGetComputeContext()`.  If the return value is `RxLocalSeq Compute Context`, you're using the local compute context.
  
## Next Step  
[Visualize SQL Server Data using R &#40;Data Science Deep Dive&#41;](../../advanced-analytics/r-services/lesson-2-1-visualize-sql-server-data-using-r.md)  
  
## Previous Step  
[Define and Use Compute Contexts &#40;Data Science Deep Dive&#41;](../../advanced-analytics/r-services/lesson-1-3-define-and-use-compute-contexts.md)  
  
  
  

