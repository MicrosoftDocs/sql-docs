---
title: "Checklist of Preparation for Data Mining | Microsoft Docs"
ms.custom: ""
ms.date: "12/29/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 0e056c95-ba06-413e-8dc1-4d411a447c3b
author: minewiskan
ms.author: owend
manager: craigg
---
# Checklist of Preparation for Data Mining
  Although the Data Mining Add-ins make it fairly easy and fun to create and experiment with models, when you need to get repeatable, actionable results, you must allow adequate time for formulating basic business requirements, and for obtaining and preparing data. This section provides a checklist to help you plan your investigation, and describes common problems.  
  
## Checklist of Data Preparation  
 **I have identified a clearly defined output.**  
 Have a plan for how you will use the results. Different types of models have different outputs. A time series model generates values for a series in the future, which are easily understood and acted on. Other models generate complex sets that must be analyzed by subject matter experts to yield the most value.  
  
-   What output do you want?  
  
-   Can you define the output as a single column or value, or other actionable result?  
  
-   What are your criteria for knowing that the model was useful?  
  
-   How will you use and interpret those results?  
  
-   Can you map new input data to the expected results?  
  
 **I know the meaning, data types, and distribution of the input data.**  
 Take some time to explore and understand your source data. It is important that people who review the model understand what kind of input data was used and know how to interpret the data types and the variability, as well as the balance and the quality.  
  
-   How much data do you have? Is there sufficient data for modeling?  
  
     It need not be a huge amount - smaller and balanced can be better.  
  
-   Is the data from multiple sources, or a single source?  
  
-   Is the data already processed and clean? Is more input data available?  
  
-   Do you know how it was manipulated before you received it - how data might have been truncated, summarized, or converted?  
  
-   Does the input data have some example results that can be used for training?  
  
 **I understand the level of data integrity we have and the level we need.**  
 Bad data can affect the quality of the model, or prevent the model from being built at all. You should have a good understanding of both the distribution and meaning of the data, and how it came to this state. You'll need to understand if it is possible or appropriate to simplify the data by labeling, truncating numeric data types, or by summarizing.  
  
-   Data labels: are they clear and correct?  
  
-   Data types: are they appropriate, and have they been changed?  
  
-   Have you sorted, cleaned up, or discarded wrong data?  
  
     Have you verified there are no duplicates?  
  
-   How will you handle missing values? Do missing values have a meaning?  
  
-   Have you verified the sources to see if any errors might have been introduced in the import process?  
  
     Where is the input stored? How long does it stay available?  
  
     Is there a data dictionary? Can you create one?  
  
-   If you combined data sets, did you check for multiple columns representing the same data?  
  
 **I know where source data is stored, where it came from, and how it is processed. The process can be easily repeated if needed.**  
 One-off data sets are fine for experiments, but if you ever want to move the model into production, you'll want to think in advance about how the cleaning process can be applied to operational data. Also, if you have operational data, you need to know how it might have been altered before you got it-you'll need to know how it was rounded, or summarized, certainly.  
  
-   Do you want to be able to repeat the experiment?  
  
-   What tools will you use to prepare data in a format that supports data analysis? Can it be automated or do you need someone to review and clean up in Excel?  
  
-   If you are sourcing data from another system, will you be able to capture and track filters that were applied?  
  
-   Can your data processing framework also apply machine learning algorithms, perform tests, and visualize results?  
  
 **We have agreed on the desired granularity of predictions and our data has been modified to output those units.**  
 Decide on the granularity of the results you want before preparing data, For example, do you want sales predictions by the day, or for each quarter? You might consider setting up different data structures for the same data, to handle different levels of summary.  
  
-   What is the current unit of measure or unit of time?  
  
     What unit do you want to use in the results?  
  
-   Is it possible to define a basic unit (e.g. day/ hour / min / instruction call) for all input data?  
  
     Do you want to rollup to higher units?  
  
-   Are categories labeled consistently? Is it easy to add or remove categories?  
  
 **Our experimental design is repeatable and reproducible.**  
 Consider strategies for analyzing and validating your results and plan to capture a data snapshot, to make sure you can trace back effects to data. If a random seed is used, the results can differ subtly. This can make it difficult to compare and validate models.  
  
-   If you make a lot of custom changes to the data, what happens the next time you want to build the model?  
  
-   Has a manual procedure or approved process already been defined that you should use to process input and get the desired outputs?  
  
-   Have you decided to use a seed for the model?  
  
 **We have the domain knowledge to validate the results, or have access to subject matter experts who can advise.**  
 Take time to validate the variables, the model, and the results. Get the help of experts to assess interactions and results. However, don't let assumptions overrule evidence. Be open to new and unexpected findings.  
  
-   Is domain knowledge available to help in filtering data and reducing input noise?  
  
-   Can domain experts help understand interpret the results and suggest improvements?  
  
## See Also  
 [Choosing Data for Data Mining](choosing-data-for-data-mining.md)  
  
  
