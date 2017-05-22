---
title: "Transform Data Using R | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "05/18/2017"
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
ms.assetid: 0327e788-94cc-4a47-933b-7c5c027b9208
caps.latest.revision: 19
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Transform Data Using R

The **RevoScaleR** package provides multiple functions for transforming data at various stages of your analysis:

- **rxDataStep** can be used to create and transform subsets of data.

- **rxImport** supports transforming data as data is being imported to or from an XDF file or an in-memory data frame.

- Although not specifically for data movement, the functions **rxSummary**, **rxCube**, **rxLinMod**, and **rxLogit** all support data transformations.

In this section, you'll learn how to use these functions. Let's start with rxDataStep.

## Use rxDataStep to Transform Variables

The rxDataStep function processes data one chunk at a time, reading from one data source and writing to another. You can specify the columns to transform, the transformations to load, and so forth.

To make this example interesting, you’ll use a function from another R package to transform your data.  The **boot** package is one of the "recommended" packages, meaning that **boot** is included with every distribution of R, but is not loaded automatically on start-up. Therefore, the package should already be available on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that you've been using with [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)].

From the **boot** package, you'll use the  function `inv.logit`, which computes the inverse of a logit. That is, the `inv.logit` function converts a logit back to a probability on the [0,1] scale.

> [!TIP] 
> Another way to  get predictions in this scale would be to set the *type* parameter to **response** in the original call to rxPredict.

1. Start by creating a data source to hold the data destined for the table, *ccScoreOutput*.
  
    ```R
    sqlOutScoreDS <- RxSqlServerData( table =  "ccScoreOutput",  connectionString = sqlConnString, rowsPerRead = sqlRowsPerRead )
    ```
  
2. Add another data source to hold the data for the table ccScoreOutput2.
  
    ```R
    sqlOutScoreDS2 <- RxSqlServerData( table =  "ccScoreOutput2",  connectionString = sqlConnString, rowsPerRead = sqlRowsPerRead )
    ```
  
    In the new table, you will get all the variables from the previous *ccScoreOutput* table, plus the newly created variable.
  
3. Set the compute context to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.
  
    ```R
    rxSetComputeContext(sqlCompute)
    ```
  
4. Use the function rxSqlServerTableExists to check whether the output table *ccScoreOutput2* already exists; and if so, use the function rxSqlServerDropTable to delete the table.
  
    ```R
    if (rxSqlServerTableExists("ccScoreOutput2"))     rxSqlServerDropTable("ccScoreOutput2")
    ```
  
5. Call the rxDataStep function, and specify the desired transforms in a list.
  
    ```R
    rxDataStep(inData = sqlOutScoreDS,
        outFile = sqlOutScoreDS2,
        transforms = list(ccFraudProb = inv.logit(ccFraudLogitScore)),
        transformPackages = "boot",
        overwrite = TRUE)
    ```

    When you define the transformations that are applied to each column, you can also specify any additional R packages that are needed to perform the transformations.  For more information about the types of transformations that you can perform, see  [Transforming and Subsetting Data](https://msdn.microsoft.com/microsoft-r/scaler-user-guide-data-transform)
  
6. Call rxGetVarInfo to view a summary of the variables in the new data set.
  
    ```R
    rxGetVarInfo(sqlOutScoreDS2)
    ```

    **Results**
    
    *Var 1: ccFraudLogitScore, Type: numeric*
    
    *Var 2: state, Type: character*
    
    *Var 3: gender, Type: character*
    
    *Var 4: cardholder, Type: character*
    
    *Var 5: balance, Type: integer*
    
    *Var 6: numTrans, Type: integer*
    
    *Var 7: numIntlTrans, Type: integer*
    
    *Var 8: creditLine, Type: integer*
    
    *Var 9: ccFraudProb, Type: numeric*

The original logit scores are preserved, but a new column, *ccFraudProb*, has been added, in which the logit scores are represented as values between 0 and 1.

Notice that the factor variables have been written to the table *ccScoreOutput2* as character data.  To use them as factors in subsequent analyses, use the parameter *colInfo* to specify the levels.

## Next Step

[Load Data into Memory using rxImport](../../advanced-analytics/tutorials/deepdive-load-data-into-memory-using-rximport.md)

## Previous Step

[Create and Run R Scripts](../../advanced-analytics/tutorials/deepdive-create-and-run-r-scripts.md)
