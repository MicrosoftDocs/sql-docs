---
title: "Candidate Key Profile Request Options (Data Profiling Task) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Data Profiling Task Editor"
ms.assetid: 8632dbc4-4394-4dc7-b19c-f9adeb21ba52
author: janinezhang
ms.author: janinez
manager: craigg
---
# Candidate Key Profile Request Options (Data Profiling Task)
  Use the **Request Properties** pane of the **Profile Requests** page to set the options for the **Candidate Key Profile Request** that is selected in the requests pane. A Candidate Key profile reports whether a column or set of columns is a key, or an approximate key, for the selected table. This profile can also help you identify problems in your data such as duplicate values in a potential key column.  
  
> [!NOTE]  
>  The options described in this topic appear on the **Profile Requests page** of the **Data Profiling Task Editor**. For more information about this page of the editor, see [Data Profiling Task Editor &#40;Profile Requests Page&#41;](data-profiling-task-editor-profile-requests-page.md).  
  
 For more information about how to use the Data Profiling Task, see [Setup of the Data Profiling Task](data-profiling-task.md). For more information about how to use the Data Profile Viewer to analyze the output of the Data Profiling Task, see [Data Profile Viewer](data-profile-viewer.md).  
  
## Understanding the Selection of Columns for the KeyColumns Property  
 Each **Candidate Key Profile Request** computes the key strength of a single key candidate that consists of a single column or of multiple columns:  
  
-   When you select only one column in **KeyColumns**, the task computes the key strength of that one column.  
  
-   When you select multiple columns in **KeyColumns**, the task computes the key strength of the composite key that consists of all the selected columns.  
  
-   When you select the wildcard character, **(\*)**, in **KeyColumns**, the task computers the key strength of each column in the table or view.  
  
 For example, consider a sample table that contains columns A, B, and C. You make the following selections for **KeyColumns**:  
  
-   You select (\*) and column C in **KeyColumns**. The task computes the key strength of column C, and then of composite key candidates (A, C) and (B, C).  
  
-   You select (\*) and (\*) in **KeyColumns**. The task computes the key strength of individual columns A, B, and C, and then of composite key candidates (A, B), (A, C) and (B, C).  
  
> [!NOTE]  
>  If you select (*), this option might result in a large number of computations and decrease the performance of the task. However, if the task finds a subset that satisfies the threshold for a key, the task does not analyze additional combinations. For example, in the sample table described above, if the task determines that column C is a key, the task does not continue to analyze the composite key candidates.  
  
## Request Properties Options  
 For a **Candidate Key Profile Request**, the **Request Properties** pane displays the following groups of options:  
  
-   **Data**, which includes the **TableOrView** and **KeyColumns** options  
  
-   **General**  
  
-   **Options**  
  
### Data Options  
 **ConnectionManager**  
 Select the existing [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager that uses the .NET Data Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (SqlClient) to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database that contains the table or view to be profiled.  
  
 **TableOrView**  
 Select the existing table or view to be profiled.  
  
 For more information, see the section, "TableorView Options," in this topic.  
  
 **KeyColumns**  
 Select the existing column or columns to be profiled. Select **(\*)** to profile all columns.  
  
 For more information, see the sections, "Understanding the Selection of Columns for the KeyColumns Property" and "KeyColumns Options," in this topic.  
  
#### TableOrView Options  
 **Schema**  
 Specify the schema to which the selected table belongs. This option is read-only.  
  
 **Table**  
 Displays the name of the selected table. This option is read-only.  
  
#### KeyColumns Options  
 The following options are presented for each column selected for profiling in **KeyColumns**, or for the **(\*)** option.  
  
 For more information, see the section, "Understanding the Selection of Columns for the KeyColumns Property," earlier in this topic.  
  
 **IsWildcard**  
 Specifies whether the **(\*)** wildcard has been selected. This option is set to **True** if you have selected **(\*)** to profile all columns. It is **False** if you have selected an individual column to be profiled. This option is read-only.  
  
 **ColumnName**  
 Displays the name of the selected column. This option is blank if you have selected **(\*)** to profile all columns. This option is read-only.  
  
 **StringCompareOptions**  
 Select options for comparing string values. This property has the options listed in the following table. The default value of this option is **Default**.  
  
> [!NOTE]  
>  When you use a **(\*)** wildcard for **ColumnName**, **CompareOptions** is read-only and is set to the **Default** setting.  
  
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
 **ThresholdSetting**  
 This property has the options listed in the following table. The default value of this property is **Specified**.  
  
|Value|Description|  
|-----------|-----------------|  
|**None**|No threshold is specified. The key strength is reported regardless of its value.|  
|**Specified**|A threshold is specified in **KeyStrengthThreshold**. The key strength is reported only if it is greater than the threshold.|  
|**Exact**|No threshold is specified. The key strength is reported only if the selected columns are an exact key.|  
  
 **KeyStrengthThreshold**  
 Specify the threshold (by using a value between 0 and 1) above which the key strength should be reported. The default value of this property is 0.95. This option is enabled only when **Specified** is selected as the **KeyStrengthThresholdSetting**.  
  
 **MaxNumberOfViolations**  
 Specify the maximum number of candidate key violations to report in the output. The default value of this property is 100. This option is disabled when **Exact** is selected as the **KeyStrengthThresholdSetting**.  
  
## See Also  
 [Data Profiling Task Editor &#40;General Page&#41;](../general-page-of-integration-services-designers-options.md)   
 [Single Table Quick Profile Form &#40;Data Profiling Task&#41;](single-table-quick-profile-form-data-profiling-task.md)  
  
  
