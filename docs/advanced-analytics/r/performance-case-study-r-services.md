---
title: "Performance Case Study (R Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/10/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 0e902312-ad9c-480d-b82f-b871cd1052d9
caps.latest.revision: 8
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Performance Case Study (R Services)

To demonstrate the effect of the guidance provided in the previous sections, tests were run using the tables from the Airline data set. 

## Tests and Example Data

There are six tables, with 10M rows in each table:

| Table name | Description |
| ---------- | ----------- |
| _airline_ | Data converted from original xdf file using `rxDataStep` |
| _airlineWithIntCol_ | *DayOfWeek* converted to an integer rather than a string. Also adds a *rowNum* column. |
| _airlineWithIndex_ | The same data as the *airlineWithIntCol* table, but with a single clustered index using the *rowNum* column. |
| _airlineWithPageComp_ | The same data as the *airlineWithIndex* table, but with page compression enabled. Also adds two columns, *CRSDepHour* and *Late*, which are computed from *CRSDepTime* and *ArrDelay*. |
| _airlineWithRowComp_ | The same data as the *airlineWithIndex* table, but with row compression enabled. Also adds two columns, *CRSDepHour* and *Late* which are computed from *CRSDepTime* and *ArrDelay*. 
| _airlineColumnar_ | A columnar store with a single clustered index. This table is populated with data from a cleaned up csv file. |

The scripts used to perform the tests described in this section, as well as links to the example data used for the tests, are available at [https://github.com/Microsoft/SQL-Server-R-Services-Samples/tree/master/PerfTuning](https://github.com/Microsoft/SQL-Server-R-Services-Samples/tree/master/PerfTuning).

Each test was run six times, and the time of the first run (the "cold run") was dropped. To allow for the occasional outlier, the maximum time for the remaining five runs was also dropped. The average of the four remaining runs was taken to compute the average elapsed runtime of each test. R garbage collection was induced before each test. The value of *rowsPerRead* for each test was set to 500000.

## Data Size When Using Compression and a Columnar Store Table

The following are the results of using compression and a columnar table to reduce the size of the data:

| Table name | Rows | Reserved | Data | index_size | Unused | % Saving (reserved) |
| ---------- | ---- | -------- | ---- | ---------- | ------ | ------------------- |
| _airlineWithIndex_ | 10000000 | 2978816 KB | 2972160 KB | 6128 KB | 528 KB | 0 |
| _airlineWithPageComp_ | 10000000 | 625784 KB | 623744 KB | 1352 KB | 688 KB | 79% |
| _airlineWithRowComp_ | 10000000 | 1262520 KB | 1258880 KB | 2552 KB | 1088 KB | 58% |
| _airlineColumnar_ | 9999999 | 201992 KB | 201624 KB | n/a | 368 KB | 93% |

## Using Integer vs. String in Formula

In this experiment, `rxLinMod` was used with the two tables, one with string factors, and one with integer factors. 
+ For the _airline_ table

    *DayOfWeek* is a string
    
    The `colInfo` parameter was used to specify the factor levels (`Monday`, `Tuesday`, …) 

+ For the *airlineWithIndex* table

    *DayOfWeek* is an integer 

    `colInfo` was not specified
    
In both cases, the same formula was used: `ArrDelay ~ CRSDepTime + DayOfWeek`. 

The following results clearly show the benefit of using integers rather than strings for factors:

| Table name | Test name | Average time |
| ---------- | --------- | ------------ |
| _airline_ | _FactorCol_ | 10.72 |
| _airlineWithIntCol_ | _IntCol_ | 3.4475 |

## Using Compression

In this experiment, `rxLinMod` was used with multiple data tables:  *airlineWithIndex*, *airlineWithPageComp*, and *airlineWithRowComp*. The same formula and query was used for all tables. 

| Table name | Test name | numTasks | Average time |
| ---------- | --------- | -------- | ------------ |
| _airlineWithIndex_ | NoCompression | 1 | 5.6775 |
| &nbsp; | &nbsp; | 4 | 5.1775 |
| _airlineWithPageComp_ | PageCompression | 1 | 6.7875 |
| &nbsp; | &nbsp; | 4 | 5.3225 |
| _airlineWithRowComp_ | RowCompression | 1 | 6.1325 |
| &nbsp; | &nbsp; | 4 | 5.2375 |

Note that compression alone (*numTasks* set to 1) does not seem to help in this example, as the increase in CPU to handle compression compensates for the decrease in IO time. 

However, when the test is run in parallel by setting *numTasks* to 4, the average time decreases. For larger data sets, the effect of compression may be more noticeable. Compression depends on the data set and values, so experimentation may be needed to determine the effect compression has on your data set.

## Avoiding Transformation Function

In this experiment, `rxLinMod` is used with the table _airlineWithIndex_ in two runs, one using a transformation function, and one without the transformation function.  

| Test name | Average time |
| --------- | ------------ |
| WithTransformation | 5.1675 |
| WithoutTransformation | 4.7 |

Note that there is improvement in time when not using a transformation function; in other words, when using columns that are pre-computed and persisted in the table. The savings would be much greater if there were many more transformations and the data set were larger (> 100M).

## Using Columnar Store

In this experiment, `rxLinMod` was used with two tables, _airlineWithIndex_ and _airlineColumnar_, and no transformation was used. These results indicate that the columnar store can perform better than row store. There will be a significant difference in performance on larger data set (> 100 M).  

| Table name | Test name |Average time |
| ---------- | --------- | ------------ |
| _airlineWithIndex_ | RowStore | 4.67 |
| _airlineColumnar_ | ColStore | 4.555 |

## Effect of the Cube Parameter

In this experiment, `rxLinMod` is used with the _airline_ table, in which the column _DayOfWeek_ is stored as a string. The formula used is `ArrDelay ~ Origin:DayOfWeek + Month + DayofMonth + CRSDepTime`. The results clearly show that the use of the `cube` parameter helps with performance. 

| Test name | Cube parameter | numTasks | Average time | One row predict (ArrDelay_Pred) |
| --------- | -------------- | -------- | ------------ | ------------------------------- |
| CubeArgEffect | `cube = F` | 1 | 91.0725 | 9.959204 |
| &nbsp; | &nbsp; | 4 | 44.09 | 9.959204 |
| &nbsp; | `cube = T` | 1 | 21.1125 | 9.959204 |
| &nbsp; | &nbsp; | 4 | 8.08 | 9.959204 |

## Effect of maxDepth for rxDTree

In this experiment, `rxDTree` is used with the _airlineColumnar_ table. Several different values for *maxDepth* were used to demonstrate how it affects the run time complexity. 

| Test name | maxDepth | Average time |
| --------- | -------- | ------------ |
| TreeDepthEffect | 1 | 10.1975 |
| &nbsp; | 2 | 13.2575 |
| &nbsp; | 4 | 19.27 |
| &nbsp; | 8 | 45.5775 |
| &nbsp; | 16 | 339.54 |

As the depth increases, the total number of nodes increases exponentially and the elapsed time will increase significantly. For this test *numTasks* was set to 4.

## Effect of Windows Power Plan Options

In this experiment, `rxLinMod` was used with the _airlineWithIntCol_ table. The Windows Power Plan was set to either **Balanced** or **High Performance**. For all tests, *numTasks* was set to 1. The test was run 6 times, and was performed twice under both power options to demonstrate the variability of results when using the Balanced power option. The results show that the numbers are more consistent and faster when using the high performance power plan. 

__High Performance__ power option:

| Test name | Run # | Elapsed time | Average time |
| --------- | ----- | ------------ | ------------ |
| IntCol | 1 | 3.57 seconds | &nbsp; |
| &nbsp; | 2 | 3.45 seconds | &nbsp; |
| &nbsp; | 3 | 3.45 seconds | &nbsp; |
| &nbsp; | 4 | 3.55 seconds | &nbsp; |
| &nbsp; | 5 | 3.55 seconds | &nbsp; |
| &nbsp; | 6 | 3.45 seconds | &nbsp; |
| &nbsp; | &nbsp; | &nbsp; | 3.475 |
| &nbsp; | 1 | 3.45 seconds | &nbsp; |
| &nbsp; | 2 | 3.53 seconds | &nbsp; |
| &nbsp; | 3 | 3.63 seconds | &nbsp; |
| &nbsp; | 4 | 3.49 seconds | &nbsp; |
| &nbsp; | 5 | 3.54 seconds | &nbsp; |
| &nbsp; | 6 | 3.47 seconds | &nbsp; |
| &nbsp; | &nbsp; | &nbsp; | 3.5075 |

__Balanced__ power option:

| Test name | Run # | Elapsed time | Average time |
| --------- | ----- | ------------ | ------------ |
| IntCol | 1 | 3.89 seconds | &nbsp; |
| &nbsp; | 2 | 4.15 seconds | &nbsp; |
| &nbsp; | 3 | 3.77 seconds | &nbsp; |
| &nbsp; | 4 | 5 seconds | &nbsp; |
| &nbsp; | 5 | 3.92 seconds | &nbsp; |
| &nbsp; | 6 | 3.8 seconds | &nbsp; |
| &nbsp; | &nbsp; | &nbsp; | 3.91 |
| &nbsp; | 1 | 3.82 seconds | &nbsp; |
| &nbsp; | 2 | 3.84 seconds | &nbsp; |
| &nbsp; | 3 | 3.86 seconds | &nbsp; |
| &nbsp; | 4 | 4.07 seconds | &nbsp; |
| &nbsp; | 5 | 4.86 seconds | &nbsp; |
| &nbsp; | 6 | 3.75 seconds | &nbsp; |
| &nbsp; | &nbsp; | &nbsp; | 3.88 |

## Prediction Using a Stored Model

In this experiment, a model is created and stored to a database. Then the stored model is loaded from the database and predictions created using a one row data frame in memory (local compute context). The time taken to train, save, and load the model and predict is shown below. . 

| Table name | Test name | Average time (to train model) | Time to save/load model |
| ---------- | --------- | ----- | ----- |
| airline | SaveModel | 21.59 | 2.08 | 
| &nbsp; | LoadModelAndPredict | &nbsp; |  2.09 (includes time to predict) |

The test results show the time to save the model and the time taken to load the model and predict. This is clearly a faster way to do prediction. 

## Performance Troubleshooting

The tests used in this section produce output files for each run by using the *reportProgress* parameter, which is passed to the tests with value `3`. The console output is directed to a file in the output directory. The output file contains information regarding the time spent in IO, transition time, and compute time. These times are useful for troubleshooting and diagnosis. 

The test scripts then process these times for the various runs to come up with the average time over runs.  For example, the following shows the sample times for a run. The main timings of interest are **Total read time** (IO time) and **Transition time** (overhead in setting up processes for computation).

    Running IntCol Test. Using airlineWithIntCol table.  
        run  1  took  3.66  seconds  
        run  2  took  3.44  seconds  
        run  3  took  3.44  seconds  
        run  4  took  3.44  seconds  
        run  5  took  3.45  seconds  
        run  6  took  3.75  seconds  

    Average Time:  3.4425  
                    metric   time    pct 
    1           Total time 3.4425 100.00 
    2 Overall compute time 2.8512  82.82 
    3      Total read time 2.5378  73.72 
    4      Transition time 0.5913  17.18 
    5    Total non IO time 0.3134   9.10 
 
 > [!TIP]
 > These test scripts and techniques can also be useful in troubleshooting issues when using rx analytic functions on your [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)].
 
## Scripts and Resources

+ Github: [Sample data and scripts](https://github.com/Microsoft/SQL-Server-R-Services-Samples/tree/master/PerfTuning) for this case study
+ Blog: [Reference Implementation of Credit Risk Prediction using R](https://blogs.msdn.microsoft.com/rserver/2017/02/22/credit-risk-prediction/)

     A performance case study that includes downloadable source code.
       
+ [Monitoring R Services using Custom Reports](../../advanced-analytics/r-services/monitor-r-services-using-custom-reports-in-management-studio.md)
 
     Custom reports that can be viewed in SQL Server Management Studio.

 ## See Also

 
 [SQL Server R Services Performance Tuning Guide](../../advanced-analytics/r-services/sql-server-r-services-performance-tuning.md)
 
 [SQL Server Configuration for R Services](../../advanced-analytics/r-services/sql-server-configuration-r-services.md)
 
 [R and Data Optimization](../../advanced-analytics/r-services/r-and-data-optimization-r-services.md)
