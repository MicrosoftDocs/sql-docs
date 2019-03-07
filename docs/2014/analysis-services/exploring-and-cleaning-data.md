---
title: "Exploring and Cleaning Data | Microsoft Docs"
ms.custom: ""
ms.date: "12/29/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 7c888c95-8986-461e-9f11-2395044b9d97
author: minewiskan
ms.author: owend
manager: craigg
---
# Exploring and Cleaning Data
  Data preparation is much more than data cleansing. Remember that how data is prepared also affects how results are interpreted in the end. Data preparation involves these tasks:  
  
-   Exploring and checking the distribution of data.  
  
-   Cleaning bad records, and choosing columns for data mining.  
  
-   Handling nulls appropriately.  
  
-   Binning values, or aggregating values by different chunks of time.  
  
-   Adding labels to improve the usability of the results.  
  
-   Converting data types or categorizing values where necessary for analysis.  
  
 If you are new to data modeling, we recommend that you read the related topic, [Checklist of Preparation for Data Mining](checklist-of-preparation-for-data-mining.md).  
  
## Data Preparation Tools  
 The Data Mining Add-ins for Office includes the following tools for data cleansing and preparation:  
  
### Explore Data  
 Use the **Explore Data** wizard for these data preparation tasks:  
  
-   Preview your data and identify errors that must be fixed prior to analysis.  
  
-   Gather statistical information that is useful for understanding the balance of data and the required clean-up tasks.  
  
-   Identify columns that are useful for analysis, and plan the data modeling phase.  
  
 [Explore Data &#40;SQL Server Data Mining Add-ins&#41;](explore-data-sql-server-data-mining-add-ins.md).  
  
### Detect and Handle Outliers  
 The **Outliers** wizard graphs the distribution of values in your data and helps you remove extreme values. Use the **Outliers** tool for the following data preparation tasks:  
  
-   Determine whether individual values are reliable, based on patterns found in the data.  
  
-   Review unusual values and take action by deleting or replacing them.  
  
-   Scope a model to a specific range of values. For example, if you know that you have outliers at a particular store, you can eliminate that value and get a model that better predicts other stores.  
  
 [Outliers &#40;SQL Server Data Mining Add-ins&#41;](outliers-sql-server-data-mining-add-ins.md).  
  
### Relabel and Bin Data  
 The **Relabel** wizard groups data by values so that you can change the labels on the data. Use the Relabel tool for these data preparation tasks:  
  
-   Change numeric codes used in survey results to a text description of what the numeric code means.  
  
     For example, you might replace data entries such as Gender = 1 with Gender = Female.  
  
-   Bin data, by creating groups to represents number ranges.  
  
     For example, you might want to replace an Income column of numbers with labels such as **Income - Moderate** and **Income - High**.  
  
-   Collapse discrete values into categories.  
  
     For example, if you have too many individual products to detect a pattern among purchases, you could try assigning products into broader categories.  
  
 [Relabel &#40;SQL Server Data Mining Add-ins&#41;](relabel-sql-server-data-mining-add-ins.md)  
  
### Cleanse Data  
 Data cleansing encompasses a wide range of activities, most of which are supported by the add-ins  
  
-   Identify nulls and determine whether they should be changed to a real value or handled as `Missing` values.  
  
-   Detect missing values, and then remove them, or impute an appropriate value, such as a mean, null, or other value.  
  
 [Explore Data &#40;SQL Server Data Mining Add-ins&#41;](explore-data-sql-server-data-mining-add-ins.md)  
  
 [Relabel &#40;SQL Server Data Mining Add-ins&#41;](relabel-sql-server-data-mining-add-ins.md)  
  
 [Fill From Example](fill-from-example-table-analysis-tools-for-excel.md)  
  
### Sample Data  
 The Sample Data wizard provides two methods for creating balanced data sets for training and testing models.  
  
-   **Random sampling.** Use this option to extract a representative set of data from a larger data set, for use in either training or testing. The Data Mining Add-ins use *stratified sampling* to ensure that a balanced set of values is obtained for each variable sampled.  
  
-   **Oversampling.** Use this option when you have less data than you would like for a target outcome, and need to weight that data more heavily. For example, fraud might be relatively rare, but you can oversample cases involving fraud to get adequate data for modeling.  
  
 [Sample Data &#40;SQL Server Data Mining Add-ins&#41;](sample-data-sql-server-data-mining-add-ins.md).  
  
## See Also  
 [Creating a Data Mining Model](creating-a-data-mining-model.md)   
 [Validating Models and Using Models for Prediction &#40;Data Mining Add-ins for Excel&#41;](validating-models-and-using-models-for-prediction-data-mining-add-ins-for-excel.md)   
 [Deploying and Scaling Mining Models &#40;Data Mining Add-ins for Excel&#41;](deploying-and-scaling-mining-models-data-mining-add-ins-for-excel.md)  
  
  
