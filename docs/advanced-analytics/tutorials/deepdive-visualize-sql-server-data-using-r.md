---
title: " Visualize SQL Server data using R (SQL and R deep dive) | Microsoft Docs"
ms.date: "12/14/2017"
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
ms.assetid: 10def0b3-9b09-4df9-b8aa-69516f7d7659
caps.latest.revision: 14
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "Inactive"
---
#  Visualize SQL Server data using R (SQL and R deep dive)

This article is part of the Data Science Deep Dive tutorial, on how to use [RevoScaleR](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) with SQL Server.

The enhanced packages in [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] include multiple functions that have been optimized for scalability and parallel processing. Typically these functions are prefixed with **rx** or **Rx**.

For this walkthrough, you use the **rxHistogram** function to view the distribution of values in the _creditLine_ column by gender.

## Visualize data using rxHistogram

1. Use the following R code to call the [rxHistogram](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxhistogram) function and pass a formula and data source. You can run this locally at first, to see the expected results, and how long it takes.
  
    ```R
    rxHistogram(~creditLine|gender, data = sqlFraudDS,  histType = "Percent")
    ```
 
    Internally, **rxHistogram** calls the [rxCube](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxcube) function, which is included in the **RevoScaleR** package. **rxCube** outputs a single list (or data frame) containing one column for each variable specified in the formula, plus a counts column.
    
2. Now, set the compute context to the remote SQL Server computer and run **rxHistogram** again.
  
    ```R
    rxSetComputeContext(sqlCompute)
    ```
 
3. The results are exactly the same, since you're using the same data source; however, the computations are performed on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer.  The results are then returned to your local workstation for plotting.
   
![histogram results](media/rsql-sue-histogramresults.jpg "histogram results")

4. You can also call the **rxCube** function and pass the results to an R plotting function.  For example, the following example uses **rxCube** to compute the mean of *fraudRisk* for every combination of *numTrans* and *numIntlTrans*:
  
    ```R
    cube1 <- rxCube(fraudRisk~F(numTrans):F(numIntlTrans),  data = sqlFraudDS)
    ```
  
    To specify the groups used to compute group means, use the `F()` notation. In this example, `F(numTrans):F(numIntlTrans)` indicates that the integers in the variables `_numTrans` and `numIntlTrans` should be treated as categorical variables, with a level for each integer value.
  
    Because the low and high levels were already added to the data source `sqlFraudDS` (using the `colInfo` parameter), the levels are automatically used in the histogram.
  
5. The default return value of **rxCube** is an *rxCube object*, which represents a cross-tabulation. However, you can use the [rxResultsDF](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxresultsdf) function to convert the results into a data frame that can easily be used in one of R’s standard plotting functions.
  
    ```R
    cubePlot <- rxResultsDF(cube1)
    ```
  
    The **rxCube** function includes an optional argument, *returnDataFrame* = **TRUE**, that you could use to convert the results to a data frame directly. For example:
    
    `print(rxCube(fraudRisk~F(numTrans):F(numIntlTrans), data = sqlFraudDS, returnDataFrame = TRUE))`
       
    However, the output of **rxResultsDF** is much cleaner and preserves the names of the source columns.
  
6. Finally, run the following code to create a heat map using the `levelplot` function from the **lattice** package, which is included with all R distributions.
  
    ```R
    levelplot(fraudRisk~numTrans*numIntlTrans, data = cubePlot)
    ```
  
    **Results**
  
    ![scatterplot results](media/rsql-sue-scatterplotresults.jpg "scatterplot results")
  
From even this quick analysis, you can see that the risk of fraud increases with both the number of transactions and the number of international transactions.

For more information about the **rxCube** function and crosstabs in general, see [Data summaries using RevoScaleR](https://docs.microsoft.com/machine-learning-server/r/how-to-revoscaler-data-summaries).

## Next step

[Create R models using SQL Server data](../../advanced-analytics/tutorials/deepdive-create-models.md)

## Previous step

[Create and run R scripts](../../advanced-analytics/tutorials/deepdive-create-and-run-r-scripts.md)
