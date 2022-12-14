---
description: "Functional Dependency Profile Request Options (Data Profiling Task)"
title: "Functional Dependency Profile Request Options (Data Profiling Task) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Data Profiling Task Editor"
ms.assetid: 6eb853aa-8016-490c-be4f-06ab8d7f5021
author: chugugrace
ms.author: chugu
---
# Functional Dependency Profile Request Options (Data Profiling Task)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  Use the **Request Properties** pane of the **Profile Requests** page to set the options for the **Functional Dependency Profile Request** selected in the requests pane. A Functional Dependency profile reports the extent to which the values in one column (the dependent column) depend on the values in another column or set of columns (the determinant column). This profile can also help you identify problems in your data such as invalid values. For example, you profile the dependency between a Zip Code/Postal Code column and a United States state column. In this profile, the same Zip Code should always have the same state, but the profile discovers violations of the dependency.  
  
> [!NOTE]  
>  The options described in this topic appear on the **Profile Requests page** of the **Data Profiling Task Editor**. For more information about this page of the editor, see [Data Profiling Task Editor &#40;Profile Requests Page&#41;](../../integration-services/control-flow/data-profiling-task-editor-profile-requests-page.md).  
  
 For more information about how to use the Data Profiling Task, see [Setup of the Data Profiling Task](../../integration-services/control-flow/setup-of-the-data-profiling-task.md). For more information about how to use the Data Profile Viewer to analyze the output of the Data Profiling Task, see [Data Profile Viewer](../../integration-services/control-flow/data-profile-viewer.md).  
  
## Understanding the Selection of Determinant and Dependent Columns  
 A **Functional Dependency Profile Request** computes the degree to which the determinant side column or set of columns (specified in the **DeterminantColumns** property) determines the value of the dependent side column (specified in the **DependentColumn** property). For example, a United States state column should be functionally dependent on a United States Zip Code column. That is, if the Zip Code (determinant column) is 98052, the state (dependent column) should always be Washington.  
  
 For the determinant side, you can specify a column or a set of columns in the **DeterminantColumns** property. For example, consider a sample table that contains columns A, B, and C. You make the following selections for the **DeterminantColumns** property:  
  
-   When you select the **(\*)** wildcard, the Data Profiling task tests each column as the determinant side of the dependency.  
  
-   When you select the **(\*)** wildcard and another column or columns, the Data Profiling task tests each combination of columns as the determinant side of the dependency. For example, consider a sample table that contains columns A, B, and C. If you specify **(\*)** and column C as the value of the **DeterminantColumns** property, the Data Profiling task tests the combinations (A, C) and (B, C) as the determinant side of the dependency.  
  
 For the dependent side, you can specify a single column or the **(\*)** wildcard in the **DependentColumn** property. When you select **(\*)**, the Data Profiling task tests the determinant side column or set of columns against each column.  
  
> [!NOTE]  
>  If you select **(\*)**, this option might result in a large number of computations and decrease the performance of the task. However, if the task finds a subset that satisfies the threshold for a functional dependency, the task does not analyze additional combinations. For example, in the sample table described above, if the task determines that column C is a determinant column, the task does not continue to analyze the composite candidates.  
  
## Request Properties Options  
 For a **Functional Dependency Profile Request**, the **Request Properties** pane displays the following groups of options:  
  
-   **Data**, which includes the **DeterminantColumns** and **DependentColumn** options  
  
-   **General**  
  
-   **Options**  
  
### Data Options  
 **ConnectionManager**  
 Select the existing [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager that uses the .NET Data Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (SqlClient) to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database that contains the table or view to be profiled.  
  
 **TableOrView**  
 Select the existing table or view to be profiled.  
  
 **DeterminantColumns**  
 Select the determinant column or set of columns. That is, select the column or set of columns whose values determine the value of the dependent column.  
  
 For more information, see the sections, "Understanding the Selection of Determinant and Dependent Columns" and "DeterminantColumns and DependentColumn Options," in this topic.  
  
 **DependentColumn**  
 Select the dependent column. That is, select the column whose value is determined by the value of the determinant side column or set of columns.  
  
 For more information, see the sections, "Understanding the Selection of Determinant and Dependent Columns" and "DeterminantColumns and DependentColumn Options," in this topic.  
  
#### DeterminantColumns and DependentColumn Options  
 The following options are presented for each column selected for profiling in **DeterminantColumns** and in **DependentColumn**.  
  
 For more information, see the section, "Understanding the Selection of Determinant and Dependent Columns," earlier in this topic.  
  
 **IsWildCard**  
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
 **ThresholdSetting**  
 Specify the threshold setting. The default value of this property is **Specified**.  
  
|Value|Description|  
|-----------|-----------------|  
|**None**|Does not specify a threshold. The functional dependency strength is reported regardless of its value.|  
|**Specified**|Use the threshold that is specified in **FDStrengthThreshold**. The functional dependency strength is reported only if it is greater than the threshold.|  
|**Exact**|Does not specify a threshold. The functional dependency strength is reported only if the functional dependency between the selected columns is exact.|  
  
 **FDStrengthThreshold**  
 Specify the threshold (by using a value between 0 and 1) above which the functional dependency strength should be reported. The default value of this property is 0.95. This option is enabled only when **Specified** is selected as the **ThresholdSetting**.  
  
 **MaxNumberOfViolations**  
 Specify the maximum number of functional dependency violations to report in the output. The default value of this property is 100. This option is disabled when **Exact** is selected as the **ThresholdSetting**.  
  
## See Also  
 [Data Profiling Task Editor &#40;General Page&#41;](../../integration-services/control-flow/data-profiling-task-editor-general-page.md)   
 [Single Table Quick Profile Form &#40;Data Profiling Task&#41;](../../integration-services/control-flow/single-table-quick-profile-form-data-profiling-task.md)  
  
  
