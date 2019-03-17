---
title: "Data Profiling Task and Viewer | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Data Profiling task [Integration Services], about data profiling"
  - "data profiling"
  - "profiling data"
ms.assetid: 756840e3-aa09-45cd-9951-1a17af4b5925
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Data Profiling Task and Viewer
  The Data Profiling task provides data profiling functionality inside the process of extracting, transforming, and loading data. By using the Data Profiling task, you can achieve the following benefits:  
  
-   Analyze the source data more effectively  
  
-   Understand the source data better  
  
-   Prevent data quality problems before they are introduced into the data warehouse.  
  
> [!IMPORTANT]  
>  The Data Profiling task works only with data that is stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. It does not work with third-party or file-based data sources.  
  
## Data Profiling Overview  
 Data quality is important to every business. As enterprises build analytical and business intelligence systems on top of their transactional systems, the reliability of key performance indicators and of data mining predictions depends completely on the validity of the data on which they are based. But although the importance of valid data for business decision-making is increasing, the challenge of making sure of this data's validity is also increasing. Data is streaming into the enterprise constantly from diverse systems and sources, and a large numbers of users.  
  
 Metrics for data quality can be difficult to define because they are specific to the domain or the application. One common approach to defining data quality is data profiling.  
  
 A data profile is a collection of aggregate statistics about data that might include the following:  
  
-   The number of rows in the Customer table.  
  
-   The number of distinct values in the State column.  
  
-   The number of null or missing values in the Zip column.  
  
-   The distribution of values in the City column.  
  
-   The strength of the functional dependency of the State column on the Zip column-that is, the state should always be the same for a given zip value.  
  
 The statistics that a data profile provides gives you the information that you need in order to effectively minimize the quality issues that might occur from using the source data.  
  
## Integration Services and Data Profiling  
 In [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], the data profiling process consist of the following steps:  
  
 **Step 1: Setting up the Data Profiling Task**  
 The Data Profiling task is a task that you use to configure the profiles that you want to compute. You then run the package that contains the Data Profiling task to compute the profiles. The task saves the profile output in XML format to a file or a package variable.  
  
 **For more information:** [Setup of the Data Profiling Task](data-profiling-task.md)  
  
 **Step 2: Reviewing the Profiles that the Data Profiling Task Computes**  
 To view the data profiles that the Data Profiling task computes, you send the output to a file, and then you use the Data Profile Viewer. This viewer is a stand-alone utility that displays the profile output in both summary and detail format with optional drilldown capability.  
  
 **For more information:** [Data Profile Viewer](data-profile-viewer.md)  
  
### Addition of Conditional Logic to the Data Profiling Workflow  
 The Data Profiling task does not have built-in features that allow you to use conditional logic to connect this task to downstream tasks based on the profile output. However, you can easily add this logic, with a small amount of programming, in a Script task. For example, the Script task could perform an XPath query against the output file of the Data Profiling task. The query could determine whether the percentage of null values in a particular column exceeds a certain threshold. If the percentage exceeds the threshold, you could interrupt the package and resolve the problem in the source data before continuing. For more information, see [Incorporate a Data Profiling Task in Package Workflow](incorporate-a-data-profiling-task-in-package-workflow.md).  
  
## Related Content  
 [Data Profiler Schema](https://go.microsoft.com/fwlink/?LinkId=251524)  
  
  
