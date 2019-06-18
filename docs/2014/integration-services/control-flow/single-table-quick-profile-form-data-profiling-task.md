---
title: "Single Table Quick Profile Form (Data Profiling Task) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.dataprofilingtask.quickprofile.f1"
helpviewer_keywords: 
  - "Data Profiling Task Editor"
ms.assetid: d2fac9ce-730e-474e-961a-69406b633778
author: janinezhang
ms.author: janinez
manager: craigg
---
# Single Table Quick Profile Form (Data Profiling Task)
  Use the **Single Table Quick Profile Form** to configure the Data Profiling task quickly to profile a single table or view by using default settings.  
  
 For more information about how to use the Data Profiling Task, see [Setup of the Data Profiling Task](data-profiling-task.md). For more information about how to use the Data Profile Viewer To analyze the output of the Data Profiling Task, see [Data Profile Viewer](data-profile-viewer.md).  
  
## Options  
 **Connection**  
 Select an existing [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager that uses the .NET Data Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (SqlClient) to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database that contains the table or view to be profiled.  
  
 **Table or View**  
 Select an existing table or view in the database to which the selected connection manager connects.  
  
 **Compute**  
 Select which profiles to compute.  
  
|Value|Description|  
|-----------|-----------------|  
|**Column Null Ratio Profile**|Compute a Column Null Ratio profile by using the default settings for all applicable columns in the selected table or view.<br /><br /> This profile reports the percentage of null values in the selected column. This profile can help you identify problems in your data such as an unexpectedly high ratio of null values in a column. For more information about the settings for this profile, see [Column Null Ratio Profile Request Options &#40;Data Profiling Task&#41;](column-null-ratio-profile-request-options-data-profiling-task.md).|  
|**Column Statistics Profile**|Compute a Column Statistics profile by using the default settings for all applicable columns in the selected table or view.<br /><br /> This profile reports statistics such as minimum, maximum, average, and standard deviation for numeric columns, and minimum and maximum for `datetime` columns. This profile can help you identify problems in your data such as invalid dates. For more information about the settings for this profile, see [Column Statistics Profile Request Options &#40;Data Profiling Task&#41;](column-statistics-profile-request-options-data-profiling-task.md).|  
|**Column Value Distribution Profile**|Compute a Column Value Distribution profile by using the default settings for all applicable columns in the selected table or view.<br /><br /> This profile reports all the distinct values in the selected column and the percentage of rows in the table that each value represents. This profile can also report values that represent more than a specified percentage of rows in the table. This profile can help you identify problems in your data such as an incorrect number of distinct values in a column. For more information about this profile, see [Column Value Distribution Profile Request Options &#40;Data Profiling Task&#41;](column-value-distribution-profile-request-options-data-profiling-task.md).|  
|**Column Length Distribution Profile**|Compute a Column Length Distribution profile by using the default settings for all applicable columns in the selected table or view.<br /><br /> This profile reports all the distinct lengths of string values in the selected column and the percentage of rows in the table that each length represents. This profile can help you identify problems in your data, such as values that are not valid. For more information about the settings for this profile, see [Column Length Distribution Profile Request Options &#40;Data Profiling Task&#41;](column-length-distribution-profile-request-options-data-profiling-task.md).|  
|**Column Pattern Profile**|Compute a Column Pattern profile by using the default settings for all applicable columns in the selected table or view.<br /><br /> This profile reports a set of regular expressions that cover the values in a string column. This profile can help you identify problems in your data, such as strings that are not valid. This profile can also suggest regular expressions that can be used in the future to validate new values. For more information about the settings for this profile, see [Column Pattern Profile Request Options &#40;Data Profiling Task&#41;](column-pattern-profile-request-options-data-profiling-task.md).|  
|**Candidate Key Profile**|Compute a Candidate Key profile for column combinations that include up to the number of columns that is specified in **for up to N Column keys**.<br /><br /> This profile reports whether a column or set of columns is appropriate to serve as a key for the selected table. This profile can also help you identify problems in your data, such as duplicate values in a potential key column. For more information about the settings for this profile, see [Candidate Key Profile Request Options &#40;Data Profiling Task&#41;](candidate-key-profile-request-options-data-profiling-task.md).|  
|**for up to N Column keys**|Select the maximum number of columns to test in possible combinations as a key for the table or view. The default value is 1. The maximum value is 1000. For example, selecting 3 tests one-column, two-column, and three-column key combinations.|  
|**Functional Dependency Profile**|Compute a Functional Dependency profile for determinant column combinations that include up to the number of columns that is specified in **for up to N Columns as the determiner**.<br /><br /> This profile reports the extent to which the values in one column (the dependent column) depend on the values in another column or set of columns (the determinant column). This profile can also help you identify problems in your data, such as values that are not valid. For more information about the settings for this profile, see [Functional Dependency Profile Request Options &#40;Data Profiling Task&#41;](functional-dependency-profile-request-options-data-profiling-task.md).|  
|**for up to N Columns as the determiner**|Select the maximum number of columns to test in possible combinations as the determinant columns. The default value is 1. The maximum value is 1000. For example, selecting 2 tests combinations in which either single columns or two-column combinations are the determinant columns for another (dependent) column.|  
  
> [!NOTE]  
>  The Value Inclusion Profile type is not available from the **Single Table Quick Profile Form**.  
  
## See Also  
 [Data Profiling Task Editor &#40;General Page&#41;](../general-page-of-integration-services-designers-options.md)   
 [Data Profiling Task Editor &#40;Profile Requests Page&#41;](data-profiling-task-editor-profile-requests-page.md)  
  
  
