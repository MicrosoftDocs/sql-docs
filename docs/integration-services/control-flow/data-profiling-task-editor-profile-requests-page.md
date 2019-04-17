---
title: "Data Profiling Task Editor (Profile Requests Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.dataprofilingtask.profilerequests.f1"
helpviewer_keywords: 
  - "Data Profiling Task Editor"
ms.assetid: c72acb3d-380e-436e-8041-ed364eddfabd
author: janinezhang
ms.author: janinez
manager: craigg
---
# Data Profiling Task Editor (Profile Requests Page)
  Use the **Profile Requests Page** of the **Data Profiling Task Editor** to select and configure the profiles that you want to compute. In a single Data Profiling task, you can compute multiple profiles for multiple columns or combinations of columns in multiple tables or views.  
  
 For more information about how to use the Data Profiling Task, see [Setup of the Data Profiling Task](../../integration-services/control-flow/setup-of-the-data-profiling-task.md). For more information about how to use the Data Profile Viewer to analyze the output of the Data Profiling Task, see [Data Profile Viewer](../../integration-services/control-flow/data-profile-viewer.md).  
  
 **To open the Profile Requests page of the Data Profiling Task Editor**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that has the Data Profiling task.  
  
2.  On the **Control Flow** tab, double-click the Data Profiling task.  
  
3.  In the **Data Profiling Task Editor**, click **Profile Requests**.  
  
## Using the Requests Pane  
 The requests pane is the pane that appears at the top of the page. This pane lists all the profiles that have been configured for the current Data Profiling task. If no profiles have been configured, the requests pane is empty. To add a new profile, click in an empty area under the **Profile Type** column and select a profile type from the list. To configure a profile, select the profile in the requests pane, and then set the profile's properties in the **Request Properties** pane.  
  
### Requests Pane Options  
 The request pane has the following options:  
  
 **View**  
 Select whether to view all the profiles that have been configured for the task or just one of the profiles.  
  
 The columns in the requests pane changed based on the **View** that you select. For more information about each of those columns, see the next section, "Requests Pane Columns."  
  
### Requests Pane Columns  
 The columns that the requests pane displays depend on the **View** that you have selected:  
  
-   If you select to view **All Requests**, the requests pane has two columns: **Profile Type** and **Request ID**.  
  
-   If you select to view one of the five column profiles, the request pane has four columns: **Profile Type**, **Table or View**, **Column**, and **Request ID**.  
  
-   If you select to view a Candidate Key profile, the request pane has four columns: **Profile Type**, **Table or View**, **KeyColumns**, and **Request ID**.  
  
-   If you select to view a Functional Dependency profile, the request pane has five columns: **Profile Type**, **Table or View**, **Determinant Columns**, **Dependent Column**, and **Request ID**.  
  
-   If you select to view a Value Inclusion Profile, the request pane has six columns: **Profile Type**, **Subset Side Table or View**, **Superset Side Table or View**, **Subset Side Columns**, **Superset Side Columns**, and **Request ID**.  
  
 The following sections describe each of those columns.  
  
#### Columns Common to All Views  
 **Profile Type**  
 Select a data profile from the following options:  
  
|Value|Description|  
|-----------|-----------------|  
|**Candidate Key Profile Request**|Compute a Candidate Key Profile.<br /><br /> This profile This profile reports whether a column or set of columns is a key, or an approximate key, for the selected table. This profile can also help you identify problems in your data, such as duplicate values in a potential key column.|  
|**Column Length Distribution Profile Request**|Compute a Column Length Distribution Profile.<br /><br /> The Column Length Distribution Profile reports all the distinct lengths of string values in the selected column and the percentage of rows in the table that each length represents. This profile can help you identify problems in your data, such as values that are not valid. For example, you profile a column of two-character United States state codes and discover values longer than two characters.|  
|**Column Null Ratio Profile Request**|Compute a Column Null Ratio Profile.<br /><br /> The Column Null Ratio Profile reports the percentage of null values in the selected column. This profile can help you identify problems in your data such as an unexpectedly high ratio of null values in a column. For example, you profile a Zip Code/Postal Code column and discover an unacceptably high percentage of missing codes.|  
|**Column Pattern Profile Request**|Compute a Column Pattern Profile.<br /><br /> The Column Pattern Profile reports a set of regular expressions that cover the specified percentage of values in a string column. This profile can help you identify problems in your data, such as strings that are not valid strings. This profile can also suggest regular expressions that can be used in the future to validate new values. For example, a pattern profile of a Zip Code/Postal Code column might produce the regular expressions: \d{5}-\d{4}, \d{5}, and \d{9}. If you see other regular expressions, your data likely contains values that are not valid or in an incorrect format.|  
|**Column Statistics Profile Request**|Select this option to compute a Column Statistics Profile using the default settings for all applicable columns in the selected table or view.<br /><br /> The Column Statistics Profile reports statistics such as minimum, maximum, average, and standard deviation for numeric columns, and minimum and maximum for **datetime** columns. This profile can help you identify problems in your data, such as dates that are not valid. For example, you profile a column of historical dates and discover a maximum date that is in the future.|  
|**Column Value Distribution Profile Request**|Compute a Column Value Distribution Profile.<br /><br /> The Column Value Distribution Profile reports all the distinct values in the selected column and the percentage of rows in the table that each value represents. This profile can also report values that represent more than a specified percentage in the table. This profile can help you identify problems in your data such as an incorrect number of distinct values in a column. For example, you profile a column that contains states in the United States and discover more than 50 distinct values.|  
|**Functional Dependency Profile Request**|Compute a Functional Dependency Profile.<br /><br /> The Functional Dependency Profile reports the extent to which the values in one column (the dependent column) depend on the values in another column or set of columns (the determinant column). This profile can also help you identify problems in your data, such as values that are not valid. For example, you profile the dependency between a column of United States Zip Codes and a column of states in the United States. The same Zip Code should always have the same state, but the profile discovers violations of this dependency.|  
|**Value Inclusion Profile Request**|Compute a Value Inclusion Profile.<br /><br /> The Value Inclusion Profile computes the overlap in the values between two columns or sets of columns. This profile can also determine whether a column or set of columns is appropriate to serve as a foreign key between the selected tables. This profile can also help you identify problems in your data such as values that are not valid. For example, you profile the ProductID column of a Sales table and discover that the column contains values that are not found in the ProductID column of the Products table.|  
  
 **RequestID**  
 Displays the identifier for the request. Typically, you do not have to change the autogenerated value.  
  
#### Columns Common to All Individual Profiles  
 **Connection Manager**  
 Displays the [!INCLUDE[vstecado](../../includes/vstecado-md.md)] connection manager that connects to the source database.  
  
 **Request ID**  
 Displays an identifier for the request. Typically, you do not have to change the autogenerated value.  
  
#### Columns Common to the Five Individual Column Profiles  
 **Table or View**  
 Displays the table or view that contains the selected column.  
  
 **Column**  
 Displays the column selected for profiling.  
  
#### Columns Specific to the Candidate Key Profile  
 **Table or View**  
 Displays the table or view that contains the selected columns.  
  
 **Key Columns**  
 Displays the columns selected for profiling.  
  
#### Columns Specific to the Functional Dependency Profile  
 **Table or View**  
 Displays the table or view that contains the selected columns.  
  
 **Determinant Columns**  
 Displays the columns selected for profiling as the determinant column or columns. In the example where the United States Zip Code determines the state in the United States, the determinant column is the Zip Code column.  
  
 **Dependent column**  
 Displays the columns selected for profiling as the dependent column. In the example where the United States Zip Code determines the state in the United States, the dependent column is the state column.  
  
#### Columns Specific to the Value Inclusion Profile  
 **Subset Side Table or View**  
 Displays the table or view that contains the column or columns selected as the subset side columns.  
  
 **Superset Side Table or View**  
 Displays the table or view that contains the column or columns selected as the superset side columns.  
  
 **Subset Side Columns**  
 Displays the column or columns selected for profiling as the subset side columns. In the example where you want to verify that the values in a US state column are found in a reference table of two-character US state codes, the subset column is the state column in the source table.  
  
 **Superset Side Columns**  
 Displays the column or columns selected for profiling as the superset side columns. In the example where you want to verify that the values in a US state column are found in a reference table of two-character US state codes, the superset column is the column of state codes in the reference table.  
  
## Using the Request Properties Pane  
 The **Request Properties** pane appears underneath the requests pane. This pane displays the options for the profile that you have selected in the requests pane.  
  
> [!NOTE]  
>  After you select a **Profile Type**, you must select the **Request ID** field to see the properties for the profile request in the **Request Properties** pane.  
  
 These options vary based on the selected profile. For information about the options for individual profile types, see the following topics:  
  
-   [Candidate Key Profile Request Options &#40;Data Profiling Task&#41;](../../integration-services/control-flow/candidate-key-profile-request-options-data-profiling-task.md)  
  
-   [Column Null Ratio Profile Request Options &#40;Data Profiling Task&#41;](../../integration-services/control-flow/column-null-ratio-profile-request-options-data-profiling-task.md)  
  
-   [Column Statistics Profile Request Options &#40;Data Profiling Task&#41;](../../integration-services/control-flow/column-statistics-profile-request-options-data-profiling-task.md)  
  
-   [Column Value Distribution Profile Request Options &#40;Data Profiling Task&#41;](../../integration-services/control-flow/column-value-distribution-profile-request-options-data-profiling-task.md)  
  
-   [Column Length Distribution Profile Request Options &#40;Data Profiling Task&#41;](../../integration-services/control-flow/column-length-distribution-profile-request-options-data-profiling-task.md)  
  
-   [Column Pattern Profile Request Options &#40;Data Profiling Task&#41;](../../integration-services/control-flow/column-pattern-profile-request-options-data-profiling-task.md)  
  
-   [Functional Dependency Profile Request Options &#40;Data Profiling Task&#41;](../../integration-services/control-flow/functional-dependency-profile-request-options-data-profiling-task.md)  
  
-   [Value Inclusion Profile Request Options &#40;Data Profiling Task&#41;](../../integration-services/control-flow/value-inclusion-profile-request-options-data-profiling-task.md)  
  
## See Also  
 [Data Profiling Task Editor &#40;General Page&#41;](../../integration-services/control-flow/data-profiling-task-editor-general-page.md)   
 [Single Table Quick Profile Form &#40;Data Profiling Task&#41;](../../integration-services/control-flow/single-table-quick-profile-form-data-profiling-task.md)  
  
  
