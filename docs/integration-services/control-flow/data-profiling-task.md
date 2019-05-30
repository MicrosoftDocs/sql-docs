---
title: "Data Profiling Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.dataprofilingtask.f1"
helpviewer_keywords: 
  - "Data Profiling task [Integration Services], about Data Profiling task"
  - "data profiling"
  - "profiling data"
ms.assetid: 248ce233-4342-42c5-bf26-f4387ea152cf
author: janinezhang
ms.author: janinez
manager: craigg
---
# Data Profiling Task

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The Data Profiling task computes various profiles that help you become familiar with a data source and identify problems in the data that have to be fixed.  
  
 You can use the Data Profiling task inside an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package to profile data that is stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and to identify potential problems with data quality.  
  
> [!NOTE]  
>  This topic only describes the features and requirements of the Data Profiling task. For a walkthrough of how to use the Data Profiling task, see the section, [Data Profiling Task and Viewer](../../integration-services/control-flow/data-profiling-task-and-viewer.md).  
  
## Requirements and Limitations  
 The Data Profiling task works only with data that is stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This task does not work with third-party or file-based data sources.  
  
 Furthermore, to run a package that contains the Data Profiling task, you must use an account that has read/write permissions, including CREATE TABLE permissions, on the tempdb database.  
  
## Data Profiler Viewer  
 After using the task to compute data profiles and save them in a file, you can use the stand-alone Data Profile Viewer to review the profile output. The Data Profile Viewer also supports drilldown capability to help you understand data quality issues tha are identified in the profile output. For more information, see [Data Profile Viewer](../../integration-services/control-flow/data-profile-viewer.md).  
  
> [!IMPORTANT]  
>  The output file might contain sensitive data about your database and the data that the database contains. For suggestions about how to make this file more secure, see [Access to Files Used by Packages](../../integration-services/security/security-overview-integration-services.md#files).  
>   
>  The drilldown capability, that is available in the Data Profile Viewer, sends live queries to the original data source.  
  
## Available Profiles  
 The Data Profiling Task can compute eight different data profiles. Five of these profiles analyze individual columns, and the remaining three analyze multiple columns or relationships between columns and tables.  
  
 The following five profiles analyze individual columns.  
  
|Profiles that analyze individual columns|Description|  
|----------------------------------------------|-----------------|  
|Column Length Distribution Profile|Reports all the distinct lengths of string values in the selected column and the percentage of rows in the table that each length represents.<br /><br /> This profile helps you identify problems in your data, such as values that are not valid. For example, you profile a column of United States state codes that should be two characters and discover values longer than two characters.|  
|Column Null Ratio Profile|Reports the percentage of null values in the selected column.<br /><br /> This profile helps you identify problems in your data, such as an unexpectedly high ratio of null values in a column. For example, you profile a Zip Code/Postal Code column and discover an unacceptably high percentage of missing codes.|  
|Column Pattern Profile|Reports a set of regular expressions that cover the specified percentage of values in a string column.<br /><br /> This profile helps you identify problems in your data, such as string that are not valid. This profile can also suggest regular expressions that can be used in the future to validate new values. For example, a pattern profile of a United States Zip Code column might produce the regular expressions: \d{5}-\d{4}, \d{5}, and \d{9}. If you see other regular expressions, your data likely contains values that are not valid or in an incorrect format.|  
|Column Statistics Profile|Reports statistics, such as minimum, maximum, average, and standard deviation for numeric columns, and minimum and maximum for **datetime** columns.<br /><br /> This profile helps you identify problems in your data, such as dates that are not valid. For example, you profile a column of historical dates and discover a maximum date that is in the future.|  
|Column Value Distribution Profile|Reports all the distinct values in the selected column and the percentage of rows in the table that each value represents. Can also report values that represent more than a specified percentage of rows in the table.<br /><br /> This profile helps you identify problems in your data, such as an incorrect number of distinct values in a column. For example, you profile a column that is supposed to contain states in the United States and discover more than 50 distinct values.|  
  
 The following three profiles analyze multiple columns or relationships between columns and tables.  
  
|Profiles that analyze multiple columns|Description|  
|--------------------------------------------|-----------------|  
|Candidate Key Profile|Reports whether a column or set of columns is a key, or an approximate key, for the selected table.<br /><br /> This profile also helps you identify problems in your data, such as duplicate values in a potential key column.|  
|Functional Dependency Profile|Reports the extent to which the values in one column (the dependent column) depend on the values in another column or set of columns (the determinant column).<br /><br /> This profile also helps you identify problems in your data, such as values that are not valid. For example, you profile the dependency between a column that contains United States Zip Codes and a column that contains states in the United States. The same Zip Code should always have the same state, but the profile discovers violations of this dependency.|  
|Value Inclusion Profile|Computes the overlap in the values between two columns or sets of columns. This profile can determine whether a column or set of columns is appropriate to serve as a foreign key between the selected tables.<br /><br /> This profile also helps you identify problems in your data, such as values that are not valid. For example, you profile the ProductID column of a Sales table and discover that the column contains values that are not found in the ProductID column of the Products table.|  
  
## Prerequisites for a Valid Profile  
 A profile is not valid unless you select tables and columns that are not empty, and the columns contain data types that are valid for the profile.  
  
### Valid Data Types  
 Some of the available profiles are meaningful only for certain data types. For example, computing a Column Pattern profile for a column that contains numeric or **datetime** values is not meaningful. Therefore, such a profile is not valid.  
  
|Profile|Valid Data Types*|  
|-------------|------------------------|  
|ColumnStatisticsProfile|Columns of numeric type or **datetime** type (no **mean** and **stddev** for **datetime** column)|  
|ColumnNullRatioProfile|All columns**|  
|ColumnValueDistributionProfile|Columns of **integer** type, **char** type, and **datetime** type|  
|ColumnLengthDistributionProfile|Columns of **char** type|  
|ColumnPatternProfile|Columns of **char** type|  
|CandidateKeyProfile|Columns of **integer** type, **char** type, and **datetime** type|  
|FunctionalDependencyProfile|Columns of **integer** type, **char** type, and **datetime** type|  
|InclusionProfile|Columns of **integer** type, **char** type, and **datetime** type|  
  
 \* In the previous table of valid data types, the **integer**, **char**, **datetime**, and **numeric** types include the following specific data types:  
  
 Integer types include **bit**, **tinyint**, **smallint**, **int**, and **bigint**.  
  
 Character types include **char**, **nchar**, **varchar**, and **nvarchar,** but do not include **varchar(max)** and **nvarchar(max)**.  
  
 Date and time types include **datetime**, **smalldatetime**, and **timestamp**.  
  
 Numeric types include **integer** types (except **bit**), **money**, **smallmoney**, **decimal**, **float**, **real**, and **numeric**.  
  
 \*\* **image**, **text**, **XML**, **udt**, and **variant** types are not supported for profiles other than the Column Null Ratio Profile.  
  
### Valid Tables and Columns  
 If the table or column is empty, the Data Profiling takes the following actions:  
  
-   When the selected table or view is empty, the Data Profiling task does not compute any profiles.  
  
-   When all the values in the selected column are null, the Data Profiling task computes only the Column Null Ratio profile. The task does not compute the Column Length Distribution profile, Column Pattern profile, Column Statistics profile, or Column Value Distribution profile.  
  
## Features of the Data Profiling Task  
 The Data Profiling task has these convenient configuration options:  
  
-   **Wildcard columns** When you are configuring a profile request, the task accepts the **(\*)** wildcard in place of a column name. This simplifies the configuration and makes it easier to discover the characteristics of unfamiliar data. When the task runs, the task profiles every column that has an appropriate data type.  
  
-   **Quick Profile** You can select Quick Profile to configure the task quickly. A Quick Profile profiles a table or view by using all the default profiles and default settings.  
  
## Custom Logging Messages Available on the Data Profililng Task  
 The following table lists the custom log entries for the Data Profiling task. For more information, see [Integration Services &#40;SSIS&#41; Logging](../../integration-services/performance/integration-services-ssis-logging.md).  
  
|Log entry|Description|  
|---------------|-----------------|  
|**DataProfilingTaskTrace**|Provides descriptive information about the status of the task. Messages include the following information:<br /><br /> Start Processing Requests<br /><br /> Query Start<br /><br /> Query End<br /><br /> Finish Computing Request|  
  
## Output and Its Schema  
 The Data Profiling task outputs the selected profiles into XML that is structured according to the DataProfile.xsd schema. You can specify whether this XML output is saved in a file or in a package variable. You can view this schema online at [https://schemas.microsoft.com/sqlserver/2008/DataDebugger/](https://schemas.microsoft.com/sqlserver/2008/DataDebugger/). From the webpage, you can save a local copy of the schema. You can then view the local copy of the schema in Microsoft [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] or another schema editor, in an XML editor, or in a text editor such as Notepad.  
  
 This schema for data quality information could be useful for:  
  
-   Exchanging data quality information within and across organizations.  
  
-   Building custom tools that work with data quality information.  
  
 The target namespace is identified in the schema as [https://schemas.microsoft.com/sqlserver/2008/DataDebugger/](https://schemas.microsoft.com/sqlserver/2008/DataDebugger/).  
  
## Output in the Conditional Workflow of a Package  
 The data profiling components do not include built-in functionality to implement conditional logic in the workflow of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package based on the output of the Data Profiling task. However, you can easily add this logic, with a minimal amount of programming, in a Script task. This code would perform an XPath query against the XML output, and then save the result in a package variable. Precedence constraints that connect the Script task to subsequent tasks can use an expression to determine the workflow. For example, the Script task detects that the percentage of null values in a column exceeds a certain threshold. When this condition is true, you might want to interrupt the package and resolve the problem before continuing.  
  
## Configuration of the Data Profiling Task  
 You configure the Data Profiling task by using the **Data Profiling Task Editor**. The editor has two pages:  
  
 [General Page](../../integration-services/control-flow/data-profiling-task-editor-general-page.md)  
 On the **General** page, you specify the output file or variable. You can also select **Quick Profile** to configure the task quickly to compute profiles by using the default settings. For more information, see [Single Table Quick Profile Form &#40;Data Profiling Task&#41;](../../integration-services/control-flow/single-table-quick-profile-form-data-profiling-task.md).  
  
 [Profile Requests Page](../../integration-services/control-flow/data-profiling-task-editor-profile-requests-page.md)  
 On the **Profile Requests** page, you specify the data source, and select and configure the data profiles that you want to compute. For more information about the various profiles that you can configure, see the following topics:  
  
-   [Candidate Key Profile Request Options &#40;Data Profiling Task&#41;](../../integration-services/control-flow/candidate-key-profile-request-options-data-profiling-task.md)  
  
-   [Column Length Distribution Profile Request Options &#40;Data Profiling Task&#41;](../../integration-services/control-flow/column-length-distribution-profile-request-options-data-profiling-task.md)  
  
-   [Column Null Ratio Profile Request Options &#40;Data Profiling Task&#41;](../../integration-services/control-flow/column-null-ratio-profile-request-options-data-profiling-task.md)  
  
-   [Column Pattern Profile Request Options &#40;Data Profiling Task&#41;](../../integration-services/control-flow/column-pattern-profile-request-options-data-profiling-task.md)  
  
-   [Column Statistics Profile Request Options &#40;Data Profiling Task&#41;](../../integration-services/control-flow/column-statistics-profile-request-options-data-profiling-task.md)  
  
-   [Column Value Distribution Profile Request Options &#40;Data Profiling Task&#41;](../../integration-services/control-flow/column-value-distribution-profile-request-options-data-profiling-task.md)  
  
-   [Functional Dependency Profile Request Options &#40;Data Profiling Task&#41;](../../integration-services/control-flow/functional-dependency-profile-request-options-data-profiling-task.md)  
  
-   [Value Inclusion Profile Request Options &#40;Data Profiling Task&#41;](../../integration-services/control-flow/value-inclusion-profile-request-options-data-profiling-task.md)  
  
  
