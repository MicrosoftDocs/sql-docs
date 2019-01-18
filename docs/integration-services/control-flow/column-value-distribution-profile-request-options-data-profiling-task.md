---
title: "Column Value Distribution Profile Request Options (Data Profiling Task) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Data Profiling Task Editor"
ms.assetid: c1e5f5de-04f5-4d00-a9f0-55817186bdf9
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Column Value Distribution Profile Request Options (Data Profiling Task)
  Use the **Request Properties** pane of the **Profile Requests** page to set the options for the **Column Value Distribution Profile Request** selected in the requests pane. A Column Value Distribution profile reports all the distinct values in the selected column and the percentage of rows in the table that each value represents. The profile can also report values that represent more than a specified percentage of rows in the table. This profile can help you identify problems in your data such as an incorrect number of distinct values in a column. For example, you profile a United States state column and discover more than 50 distinct values.  
  
> [!NOTE]  
>  The options described in this topic appear on the **Profile Requests page** of the **Data Profiling Task Editor**. For more information about this page of the editor, see [Data Profiling Task Editor &#40;Profile Requests Page&#41;](../../integration-services/control-flow/data-profiling-task-editor-profile-requests-page.md).  
  
 For more information about how to use the Data Profiling Task, see [Setup of the Data Profiling Task](../../integration-services/control-flow/setup-of-the-data-profiling-task.md). For more information about how to use the Data Profile Viewer to analyze the output of the Data Profiling Task, see [Data Profile Viewer](../../integration-services/control-flow/data-profile-viewer.md).  
  
## Request Properties Options  
 For a **Column Value Distribution Profile Request**, the **Request Properties** pane displays the following groups of options:  
  
-   **Data**, which includes the **TableOrView** and **Column** options  
  
-   **General**  
  
-   **Options**  
  
### Data Options  
 **ConnectionManager**  
 Select the existing [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager that uses the .NET Data Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (SqlClient) to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database that contains the table or view to be profiled.  
  
 **TableOrView**  
 Select the existing table or view that contains the column to be profiled.  
  
 For more information, see the section, "TableorView Options," in this topic.  
  
 **Column**  
 Select the existing column to be profiled. Select **(\*)** to profile all columns.  
  
 For more information, see the section, "Column Options," in this topic.  
  
#### TableOrView Options  
 **Schema**  
 Specifies the schema to which the selected table belongs. This option is read-only.  
  
 **Table**  
 Displays the name of the selected table. This option is read-only.  
  
#### Column Options  
 **IsWildCard**  
 Specifies whether the **(\*)** wildcard has been selected. This option is set to **True** if you have selected **(\*)** to profile all columns. It is **False** if you have selected an individual column to be profiled. This option is read-only.  
  
 **ColumnName**  
 Displays the name of the selected column. This option is blank if you have selected **(\*)** to profile all columns. This option is read-only.  
  
 **StringCompareOptions**  
 Select options for comparing string values. This property has the options listed in the following table. The default value of this option is **Default**.  
  
> [!NOTE]  
>  If the **(\*)** wildcard is used for **ColumnName**, **CompareOptions** is read-only and is set to the **Default** setting.  
  
|Value|Description|  
|-----------|-----------------|  
|**Default**|Sorts and compares data based on the column's collation in the source table.|  
|**BinarySort**|Sorts and compares data based on the bit patterns defined for each character. Binary sort order is case sensitive and accent sensitive. Binary is also the fastest sorting order.|  
|**DictionarySort**|Sorts and compares data based on the sorting and comparison rules as defined in dictionaries for the associated language or alphabet.|  
  
 If you select **DictionarySort**, you can also select any combination of the options listed in the following table. By default, none of these additional options are selected.  
  
|Value|Description|  
|-----------|-----------------|  
|**IgnoreCase**|Specifies whether the comparison distinguishes between uppercase and lowercase letters. If this option is set, the string comparison ignores case. For example, "ABC" becomes the same as "abc".|  
|**IgnoreNonSpace**|Specifies whether the comparison distinguishes between spacing characters and diacritics. If this option is set, the comparison ignores diacritics. For example, "å" is equal to "a".|  
|**IgnoreKanaType**|Specifies whether the comparison distinguishes between the two types of Japanese kana characters: hiragana and katakana. If this option is set, the string comparison ignores kana type.|  
|**IgnoreWidth**|Specifies whether the comparison distinguishes between a single-byte character and the same character when it is represented as a double-byte character. If this option is set, the string comparison treats single-byte and double-byte representations of the same character as identical.|  
  
### General Options  
 **RequestID**  
 Type a descriptive name to identify this profile request. Typically, you do not have to change the autogenerated value.  
  
### Options  
 **ValueDistributionOption**  
 Specify whether to compute the distribution for all column values. The default value of this option is **FrequentValues**.  
  
|Value|Description|  
|-----------|-----------------|  
|**AllValues**|The distribution is computed for all column values.|  
|**FrequentValues**|The distribution is computed only for values whose frequency exceeds the minimum value specified in **FrequentValueThreshold**. Values that do not meet the **FrequentValueThreshold** are excluded from the output report.|  
  
 **FrequentValueThreshold**  
 Specify the threshold (by using a value between 0 and 1) above which the column value should be reported. This option is disabled when you select **AllValues** as the **ValueDistributionOption**. The default value of this option is 0.001.  
  
## See Also  
 [Data Profiling Task Editor &#40;General Page&#41;](../../integration-services/control-flow/data-profiling-task-editor-general-page.md)   
 [Single Table Quick Profile Form &#40;Data Profiling Task&#41;](../../integration-services/control-flow/single-table-quick-profile-form-data-profiling-task.md)  
  
  
