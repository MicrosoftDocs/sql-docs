---
title: "Setup of the Data Profiling Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Data Profiling task [Integration Services], configuring"
ms.assetid: fe050ca4-fe45-43d7-afa9-99478041f9a8
author: janinezhang
ms.author: janinez
manager: craigg
---
# Setup of the Data Profiling Task
  Before you can review a profile of the source data, the first step is to set up and run the Data Profiling task. You create this task inside an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package. To configure the Data Profiling task, you use the Data Profiling Task Editor. This editor enables you to select where to output the profiles, and which profiles to compute. After you set up the task, you run the package to compute the data profiles.  
  
## Requirements and Limitations  
 The Data Profiling task works only with data that is stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. It does not work with third-party or file-based data sources.  
  
 Furthermore, to run a package that contains the Data Profiling task, you must use an account that has read/write permissions, including CREATE TABLE permissions, on the tempdb database.  
  
## Data Profiling Task in a Package  
 The Data Profiling task only configures the profiles and creates the output file that contains the computed profiles. To review this output file, you must use the Data Profile Viewer, a stand-alone viewer program. Because you must view the output separately, you might use the Data Profiling task in a package that contains no other tasks.  
  
 However, you do not have to use the Data Profiling task as the only task in a package. If you want to perform data profiling in the workflow or data flow of a more complex package, you have the following options:  
  
-   To implement conditional logic that is based on the task's output file, in the control flow of the package, put a Script task after the Data Profiling task. You can then use this Script task to query the output file.  
  
-   To profile data in the data flow after the data has been loaded and transformed, you have to save the changed data temporarily to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table. Then, you can profile the saved data.  
  
 For more information, see [Incorporate a Data Profiling Task in Package Workflow](incorporate-a-data-profiling-task-in-package-workflow.md).  
  
## Setup of the Task Output  
 After the Data Profiling task is in a package, you must set up the output for the profiles that the task will compute. To set up the output for the profiles, you use the **General** page of the Data Profiling Task Editor. In addition to specifying the destination for the output, the **General** page also offers you the ability to perform a quick profile of the data. When you select **Quick Profile**, the Data Profiling task profiles a table or view by using some or all the default profiles with their default settings.  
  
 For more information, see [Data Profiling Task Editor &#40;General Page&#41;](../general-page-of-integration-services-designers-options.md) and [Single Table Quick Profile Form &#40;Data Profiling Task&#41;](data-profiling-task.md).  
  
> [!IMPORTANT]  
>  The output file might contain sensitive data about your database and the data that database contains. For suggestions about how to make this file more secure, see [Access to Files Used by Packages](../access-to-files-used-by-packages.md).  
  
## Selection and Configuration of the Profiles to be Computed  
 After you have set up the output file, you have to select which data profiles to compute. The Data Profiling Task can compute eight different data profiles. Five of these profiles analyze individual columns, and the remaining three analyze multiple columns or relationships between columns and tables. In a single Data Profiling task, you can compute multiple profiles for multiple columns or combinations of columns in multiple tables or views.  
  
 The following table describes the reports that each of these profiles computes and the data types for which the profile is valid.  
  
|To compute|Which help identify|Use this profile|  
|----------------|-------------------------|----------------------|  
|All the distinct lengths of string values in the selected column and the percentage of rows in the table that each length represents.|**String values that are not valid**-For example, you profile of a column that is supposed to use two characters for state codes in the United States, but discover values that are longer than two characters.|**Column Length Distribution-**Valid for a column with one of these data types:<br /><br /> Character data types: `char`, `nchar`, `varchar`, and `nvarchar`|  
|A set of regular expressions that cover the specified percentage of values in a string column.<br /><br /> Also, to find regular expressions that can be used in the future to validate new values|**String values that are not valid or not in the correct format-**For example, a pattern profile of a Zip Code/Postal Code column might produce the regular expressions: \d{5}-\d{4}, \d{5}, and \d{9}. If the output contains other regular expressions, the data contains values that are either not valid or in an incorrect format.|**Column Pattern Profile-**Valid for a column with one of these data types:<br /><br /> Character data types: `char`, `nchar`, `varchar`, and `nvarchar`|  
|The percentage of null values in the selected column.|**An unexpectedly high ratio of null values in a column-**For example, you profile a column that is supposed to contain United States Zip Codes, but discover an unacceptably high percentage of missing zip codes.|**Column Null Ratio-**Valid for a column with these data types:<br /><br /> Any data type. This includes `image`, `text`, `xml`, user-defined types, and variant types.|  
|Statistics such as minimum, maximum, average, and standard deviation for numeric columns, and minimum and maximum for `datetime` columns.|**Numeric values and dates that are not valid**-For example, you profile a column of historical dates, but discover a maximum date that is in the future.|**Column Statistics Profile-**Valid for a column with one of these data types:<br /><br /> Numeric data types: integer types (except `bit`), `money`, `smallmoney`, `decimal`, `float`, `real`, and `numeric`<br /><br /> Date and time data types: `datetime`, `smalldatetime`, `timestamp`, `date`, `time`, `datetime2`, and `datetimeoffset`<br />Note: For a column that has a date and time data type, the profile computes minimum and maximum only.|  
|All the distinct values in the selected column and the percentage of rows in the table that each value represents. Or, the values that represent more than a specified percentage in the table.|**An incorrect number of distinct values in a column**-For example, you profile a column that contains states in the United States, but discover more than 50 distinct values.|**Column Value Distribution-**Valid for a column with one of these data types:<br /><br /> Numeric data types: integer types (except `bit`), `money`, `smallmoney`, `decimal`, `float`, `real`, and `numeric`<br /><br /> Character data types: `char`, `nchar`, `varchar`, and `nvarchar`<br /><br /> Date and time data types: `datetime`, `smalldatetime`, `timestamp`, `date`, `time`, `datetime2`, and `datetimeoffset`|  
|Whether a column or set of columns is a key, or an approximate key, for the selected table.|**Duplicate values in a potential key column-**For example, you profile the Name and Address columns in a Customers table, and discover duplicate values where the name and address combinations should be unique.|**Candidate Key-**A multiple column profile that reports whether a column or set of columns is appropriate to serve as a key for the selected table. Valid for columns with one of these data types:<br /><br /> Integer data types: `bit`, `tinyint`, `smallint`, `int`, and `bigint`<br /><br /> Character data types: `char`, `nchar`, `varchar`, and `nvarchar`<br /><br /> Date and time data types: `datetime`, `smalldatetime`, `timestamp`, `date`, `time`, `datetime2`, and `datetimeoffset`|  
|The extent to which the values in one column (the dependent column) depend on the values in another column or set of columns (the determinant column).|**Values that are not valid in dependent columns-**For example, you profile the dependency between a column that contains United States Zip Codes and a column that contains states in the United States. The same Zip Code should always have the same state. However, the profile discovers violations of the dependency.|**Functional Dependency-**Valid for columns with one of these data types:<br /><br /> Integer data types: `bit`, `tinyint`, `smallint`, `int`, and `bigint`<br /><br /> Character data types: `char`, `nchar`, `varchar`, and `nvarchar`<br /><br /> Date and time data types: `datetime`, `smalldatetime`, `timestamp`, `date`, `time`, `datetime2`, and `datetimeoffset`|  
|Whether a column or set of columns is appropriate to serve as a foreign key between the selected tables.<br /><br /> That is, this profile reports the overlap in the values between two columns or sets of columns.|**Values that are not valid-**For example, you profile the ProductID column of a Sales table. The profile discovers that the column contains values that are not found in the ProductID column of the Products table.|**Value Inclusion-**Valid for columns with one of these data types:<br /><br /> Integer data types: `bit`, `tinyint`, `smallint`, `int`, and `bigint`<br /><br /> Character data type: `char`, `nchar`, `varchar`, and `nvarchar`<br /><br /> Date and time data types: `datetime`, `smalldatetime`, `timestamp`, `date`, `time`, `datetime2`, and `datetimeoffset`|  
  
 To select which profiles to compute, you use the **Profile Requests** page of the Data Profiling Task Editor. For more information, see [Data Profiling Task Editor &#40;Profile Requests Page&#41;](data-profiling-task-editor-profile-requests-page.md).  
  
 On the **Profile Request** page, you also specify the data source and configure the data profiles. When you configure the task, think about the following information:  
  
-   To simplify configuration and make it easier to discover characteristics of unfamiliar data, you can use the wildcard, **(\*)**, in place of an individual column name. If you use this wildcard, the task will profile every column that has an appropriate data type, which in turn can slow down processing.  
  
-   When the selected table or view is empty, the Data Profiling task does not compute any profiles.  
  
-   When all the values in the selected column are null, the Data Profiling task computes only the Column Null Ratio Profile. It does not compute the Column Length Distribution Profile, Column Pattern Profile, Column Statistics Profile, or Column Value Distribution Profile for the empty column.  
  
 Each of the available data profiles has its own configuration options. For more information about those options, see the following topics:  
  
-   [Candidate Key Profile Request Options &#40;Data Profiling Task&#41;](candidate-key-profile-request-options-data-profiling-task.md)  
  
-   [Column Length Distribution Profile Request Options &#40;Data Profiling Task&#41;](column-length-distribution-profile-request-options-data-profiling-task.md)  
  
-   [Column Null Ratio Profile Request Options &#40;Data Profiling Task&#41;](column-null-ratio-profile-request-options-data-profiling-task.md)  
  
-   [Column Pattern Profile Request Options &#40;Data Profiling Task&#41;](column-pattern-profile-request-options-data-profiling-task.md)  
  
-   [Column Statistics Profile Request Options &#40;Data Profiling Task&#41;](column-statistics-profile-request-options-data-profiling-task.md)  
  
-   [Column Value Distribution Profile Request Options &#40;Data Profiling Task&#41;](column-value-distribution-profile-request-options-data-profiling-task.md)  
  
-   [Functional Dependency Profile Request Options &#40;Data Profiling Task&#41;](functional-dependency-profile-request-options-data-profiling-task.md)  
  
-   [Value Inclusion Profile Request Options &#40;Data Profiling Task&#41;](value-inclusion-profile-request-options-data-profiling-task.md)  
  
## Execution of the Package that Contains the Data Profiling Task  
 After you have set up the Data Profiling task, you can run the task. The task then computes the data profiles and outputs this information in XML format to a file or a package variable. The structure of this XML follows the DataProfile.xsd schema. You can open the schema in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] or another schema editor, in an XML editor, or in a text editor such as Notepad. This schema for data quality information could be useful for the following purposes:  
  
-   To exchange data quality information within and across organizations.  
  
-   To build custom tools that work with data quality information.  
  
 The target namespace is identified in the schema as [https://schemas.microsoft.com/sqlserver/2008/DataDebugger/](https://schemas.microsoft.com/sqlserver/2008/DataDebugger/).  
  
## Next Step  
 [Data Profile Viewer](data-profile-viewer.md).  
  
  
