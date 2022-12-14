---
description: "Value Inclusion Profile Request Options (Data Profiling Task)"
title: "Value Inclusion Profile Request Options (Data Profiling Task) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Data Profiling Task Editor"
ms.assetid: ca94da82-a4c9-4e87-9cba-c2d85bd31f01
author: chugugrace
ms.author: chugu
---
# Value Inclusion Profile Request Options (Data Profiling Task)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  Use the **Request Properties** pane of the **Profile Requests** page to set the options for the **Value Inclusion Profile Request** selected in the requests pane. A Value Inclusion profile computes the overlap in the values between two columns or sets of columns. Thus, it can also determine whether a column or set of columns is appropriate to serve as a foreign key between the selected tables. This profile can also help you identify problems in your data such as invalid values. For example, you use a value inclusion profile to profile the ProductID column of a Sales table. The profile discovers that the column contains values that are not found in the ProductID column of the Products table.  
  
> [!NOTE]  
>  The options described in this topic appear on the **Profile Requests page** of the **Data Profiling Task Editor**. For more information about this page of the editor, see [Data Profiling Task Editor &#40;Profile Requests Page&#41;](../../integration-services/control-flow/data-profiling-task-editor-profile-requests-page.md).  
  
 For more information about how to use the Data Profiling Task, see [Setup of the Data Profiling Task](../../integration-services/control-flow/setup-of-the-data-profiling-task.md). For more information about how to use the Data Profile Viewer to analyze the output of the Data Profiling Task, see [Data Profile Viewer](../../integration-services/control-flow/data-profile-viewer.md).  
  
## Understanding the Selection of Columns for the InclusionColumns Property  
 A **Value Inclusion Profile Request** computes whether all the values in a subset are present in the superset. The superset is often a lookup or reference table. For example, the state column in a table of addresses is the subset table. Every two-character state code in this column should also be found in the table of United States Postal Service state codes, which is the superset table.  
  
 When you use the (*) wildcard as the value of the subset column or the superset column, the Data Profiling task compares each column on that side against the column specified on the other side.  
  
> [!NOTE]  
>  If you select (*), this option might result in a large number of computations and decrease the performance of the task.  
  
## Understanding the Threshold Settings  
 You can use two different threshold settings to refine the output of a Value Inclusion Profile Request.  
  
 When you specify a value other than **None** for **InclusionThresholdSetting**, the profile reports the inclusion strength of the subset in the superset only under one of the following conditions:  
  
-   When the inclusion strength exceeds the threshold specified in **InclusionStrengthThreshold**.  
  
-   When the inclusion strength has a value of 1.0 and the **InclusionStrengthThreshold** is set to **Exact**.  
  
 You can refine the output more by filtering out combinations where the superset column is not an appropriate key for the superset table because of non-unique values. When you specify a value other than **None** for **SupersetColumnsKeyThresholdSetting**, the profile reports the inclusion strength of the subset in the superset only under one of the following conditions:  
  
-   When the suitability of the superset columns as a key in the superset table exceeds the threshold specified in **SupersetColumnsKeyThreshold**  
  
-   When the inclusion strength has a value or 1.0 and the **SupersetColumnsKeyThreshold** is set to **Exact**.  
  
## Request Properties Options  
 For a **Value Inclusion Profile Request**, the **Request Properties** pane displays the following groups of options:  
  
-   **Data**, which includes the **SubsetTableOrView**, **SupersetTableOrView**, and **InclusionColumns** options  
  
-   **General**  
  
-   **Options**  
  
### Data Options  
 **ConnectionManager**  
 Select the existing [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager that uses the .NET Data Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (SqlClient) to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database that contains the table or view to be profiled.  
  
 **SubsetTableOrView**  
 Select the existing table or view to be profiled.  
  
 For more information, see the section, "SubsetTableOrView and SupersetTableOrView Options," in this topic.  
  
 **SupersetTableOrView**  
 Select the existing table or view to be profiled.  
  
 For more information, see the section, "SubsetTableOrView and SupersetTableOrView Options," in this topic.  
  
 **InclusionColumns**  
 Select the columns or sets of columns from the subset and superset tables.  
  
 For more information, see the sections, "Understanding the Selection of Columns for the InclusionColumns Property" and "InclusionColumns Options," in this topic.  
  
#### SubsetTableOrView and SupersetTableOrView Options  
 **Schema**  
 Specifies the schema to which the selected table belongs. This option is read-only.  
  
 **TableOrView**  
 Displays the name of the selected table. This option is read-only.  
  
#### InclusionColumns Options  
 The following options are presented for each set of columns selected for profiling in **InclusionColumns**.  
  
 For more information, see the section, "Understanding the Selection of Columns for the InclusionColumns Property," earlier in this topic.  
  
 **IsWildcard**  
 Specifies whether the **(\*)** wildcard has been selected. This option is set to **True** if you have selected **(\*)** to profile all columns. It is **False** if you have selected an individual column to be profiled. This option is read-only.  
  
 **ColumnName**  
 Displays the name of the selected column. This option is blank if you have selected **(\*)** to profile all columns. This option is read-only.  
  
 **StringCompareOptions**  
 Select options for comparing string values. This property has the options listed in the following table. The default value of this option is **Default**.  
  
> [!NOTE]  
>  When you use the **(\*)** wildcard for **ColumnName**, **CompareOptions** is read-only and is set to the **Default** setting.  
  
|Value|Description|  
|-----------|-----------------|  
|**Default**|Sorts and compares data based on the column's collation in the source table.|  
|**BinarySort**|Sorts and compares data based on the bit patterns defined for each character. Binary sort order is case sensitive and accent sensitive. Binary is also the fastest sorting order.|  
|**DictionarySort**|Sorts and compares data based on the sorting and comparison rules as defined in dictionaries for the associated language or alphabet.|  
  
 If you select **DictionarySort**, you can also select any combination of the options listed in the following table. By default, none of these additional options are selected.  
  
|Value|Description|  
|-----------|-----------------|  
|**IgnoreCase**|Specifies whether the comparison distinguishes between uppercase and lowercase letters. If this option is set, the string comparison ignores case. For example, "ABC" becomes the same as "abc".|  
|**IgnoreNonSpace**|Specifies whether the comparison distinguishes between spacing characters and diacritics. If this option is set, the comparison ignores diacritics. For example, "Ã¥" is equal to "a".|  
|**IgnoreKanaType**|Specifies whether the comparison distinguishes between the two types of Japanese kana characters: hiragana and katakana. If this option is set, the string comparison ignores kana type.|  
|**IgnoreWidth**|Specifies whether the comparison distinguishes between a single-byte character and the same character when it is represented as a double-byte character. If this option is set, the string comparison treats single-byte and double-byte representations of the same character as identical.|  
  
### General Options  
 **RequestID**  
 Type a descriptive name to identify this profile request. Typically, you do not have to change the autogenerated value.  
  
### Options  
 **InclusionThresholdSetting**  
 Select the threshold setting to refine the output of the profile. The default value of this property is **Specified**. For more information, see the section, "Understanding the Threshold Settings," earlier in this topic.  
  
|Value|Description|  
|-----------|-----------------|  
|**None**|Does not specify a threshold. The key strength is reported regardless of its value.|  
|**Specified**|Use the threshold that is specified in **InclusionStrengthThreshold**. The inclusion strength is reported only if it is greater than the threshold.|  
|**Exact**|Does not specify a threshold. The inclusion strength is reported only if the subset values are completedly included in the upserset values.|  
  
 **InclusionStrengthThreshold**  
 Specify the threshold (by using a value between 0 and 1) above which the inclusion strength should be reported. The default value of this property is 0.95. This option is enabled only when **Specified** is selected as the **InclusionThresholdSetting**.  
  
 For more information, see the section, "Understanding the Threshold Settings," earlier in this topic.  
  
 **SupersetColumnsKeyThresholdSetting**  
 Specify the superset threshold. The default value of this property is **Specified**. For more information, see the section, "Understanding the Threshold Settings," earlier in this topic.  
  
|Value|Description|  
|-----------|-----------------|  
|**None**|Does not specify a threshold. The inclusion strength is reported regardless of the key strength of the superset column.|  
|**Specified**|Use the threshold that is specified in **SupersetColumnsKeyThreshold**. The inclusion strength is reported only if the key strength of the superset column is greater than the threshold.|  
|**Exact**|Does not specify a threshold. The inclusion strength is reported only if the supserset columns are an exact key in the superset table.|  
  
 **SupersetColumnsKeyThreshold**  
 Specify the threshold (by using a value between 0 and 1) above which the inclusion strength should be reported. The default value of this property is 0.95. This option is enabled only when **Specified** is selected as the **SupersetColumnsKeyThresholdSetting**.  
  
 For more information, see the section, "Understanding the Threshold Settings," earlier in this topic.  
  
 **MaxNumberOfViolations**  
 Specify the maximum number of inclusion violations to report in the output. The default value of this property is 100. This option is disabled when **Exact** is selected as the **InclusionThresholdSetting**.  
  
## See Also  
 [Data Profiling Task Editor &#40;General Page&#41;](../../integration-services/control-flow/data-profiling-task-editor-general-page.md)   
 [Single Table Quick Profile Form &#40;Data Profiling Task&#41;](../../integration-services/control-flow/single-table-quick-profile-form-data-profiling-task.md)  
  
  
