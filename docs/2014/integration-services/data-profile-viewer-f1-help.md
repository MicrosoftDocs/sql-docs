---
title: "Data Profile Viewer F1 Help | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.dataprofileviewer.f1"
helpviewer_keywords: 
  - "Data Profile Viewer [Integration Services]"
  - "Data Profiling task [Integration Services], viewer"
ms.assetid: 3469c60a-8f4f-46ba-999a-cb9070197fea
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Data Profile Viewer F1 Help
  Use the Data Profile Viewer to view the output of the Data Profiling task.  
  
 For more information about how to use the Data Profile Viewer, see [Data Profile Viewer](control-flow/data-profile-viewer.md). For more information about how to use the Data Profiling task, which creates the profile output that you analyze in the Data Profile Viewer, see [Setup of the Data Profiling Task](control-flow/data-profiling-task.md).  
  
## Static Options  
 **Open**  
 Click to browse for the saved file that contains the output of the Data Profiling task.  
  
 **Profiles** pane  
 Expand the tree in the **Profiles** pane to see the profiles that are included in the output. Select a profile to view the results for that profile.  
  
 **Message** pane  
 Displays status messages.  
  
 **Drilldown** pane  
 Displays the rows of data that match a value in the output, if the data source that is used by the Data Profiling task is available.  
  
 For example, if you are viewing the output of a Column Value Distribution profile for a US State column, the **Detailed Value Distribution** pane might contain a row for "WA". Double-click the row in the **Detailed Value Distribution** pane to see the rows of data where the value of the state column is "WA" in the drilldown pane.  
  
## Dynamic Options  
  
### Profile Type = Column Length Distribution Profile  
  
#### Column Length Distribution Profile - \<column> pane  
 **Minimum Length**  
 Displays the minimum length of values in this column.  
  
 **Maximum Length**  
 Displays the maximum length of values in this column.  
  
 **Ignore Leading Spaces**  
 Displays whether this profile was computed with an `IgnoreLeadingSpaces` value of True or False. This property was set on the **Profile Requests** page of the Data Profiling Task Editor.  
  
 **Ignore Trailing Spaces**  
 Displays whether this profile was computed with an `IgnoreTrailingSpaces` value of True or False. This property was set on the **Profile Requests** page of the Data Profiling Task Editor.  
  
 **Row Count**  
 Displays the number of rows in the table or view.  
  
#### Detailed Length Distribution pane  
 **Length**  
 Displays the column lengths found in the profiled column.  
  
 **Count**  
 Displays the number of rows in which the value of the profiled column has the length shown in the **Length** column.  
  
 **Percentage**  
 Displays the percentage of rows in which the value of the profiled column has the length shown in the **Length** column.  
  
### Profile Type = Column Null Ratio Profile  
  
#### Column Null Ratio Profile - \<column> pane  
 **Null Count**  
 Displays the number of rows in which the profiled column has a null value.  
  
 **Null Percentage**  
 Displays the percentage of rows in which the profiled column has a null value.  
  
 **Row Count**  
 Displays the number of rows in the table or view.  
  
### Profile Type = Column Pattern Profile  
  
#### Column Pattern Profile - \<column> pane  
 **Row Count**  
 Displays the number of rows in the table or view.  
  
#### Pattern Distribution pane  
 **Pattern**  
 Displays the patterns computed for the profiled column.  
  
 **Percentage**  
 Displays the percentage of rows whose values match the pattern displayed in the **Pattern** column.  
  
### Profile Type = Column Statistics Profile  
  
#### Column Statistics Profile - \<column> pane  
 **Minimum**  
 Displays the minimum value found in the profiled column.  
  
 **Maximum**  
 Displays the maximum value found in the profiled column.  
  
 **Mean**  
 Displays the mean of the values found in the profiled column.  
  
 **Standard Deviation**  
 Displays the standard deviation of the values found in the profiled column.  
  
### Profile Type = Column Value Distribution Profile  
  
#### Column Value Distribution Profile - \<column> pane  
 **Number of Distinct Values**  
 Displays the count of distinct values found in the profiled column.  
  
 **Row Count**  
 Displays the number of rows in the table or view.  
  
#### Detailed Value Distribution pane  
 **Value**  
 Displays the distinct values found in the profiled column.  
  
 **Count**  
 Displays the number of rows in which the profiled column has the value shown in the **Value** column.  
  
 **Percentage**  
 Displays the percentage of rows in which the profiled column has the value shown in the **Value** column.  
  
### Profile Type = Candidate Key Profile  
  
#### Candidate Key Profile - \<table> pane  
 **Key Columns**  
 Displays the columns that were selected for profiling as a candidate key.  
  
 **Key Strength**  
 Displays the strength (as a percentage) of the candidate key column or combination of columns. A key strength of less than 100% indicates that duplicate values exist.  
  
#### Key Violations pane  
 **\<column1>, \<column2>, etc.**  
 Displays the duplicate values that were found in the profiled column.  
  
 **Count**  
 Displays the number of rows in which the specified column has the value shown in the first column.  
  
### Profile Type = Functional Dependency Profile  
  
#### Functional Dependency Profile pane  
 **Determinant Columns**  
 Displays the column or columns selected as the determinant column. In the example where the same United States Zip Code should always have the same state, the Zip Code is the determinant column.  
  
 **Dependent Columns**  
 Displays the column or columns selected as the dependent column. In the example where the same United States Zip Code should always have the same state, the state is the dependent column.  
  
 **Functional Dependency Strength**  
 Displays the strength (as a percentage) of the functional dependency between columns. A key strength of less than 100% indicates that there are cases where the determinant value does not determine the dependent value. In the example where the same United States Zip Code should always have the same state, this probably indicates some state values are not valid.  
  
#### Functional Dependency Violations pane  
  
> [!NOTE]  
>  A high percentage of erroneous values in the data could lead to unexpected results from a Functional Dependency profile. For example, 90% of the rows have a State value of "WI" for a Postal Code value of "98052." The profile reports rows that contain the correct state value of "WA" as violations.  
  
 **\<determinant column name>**  
 Displays the value of the determinant column or combination of columns in this instance of a functional dependency violation.  
  
 **\<dependent column name>**  
 Displays the value of the dependent column in this instance of a functional dependency violation.  
  
 **Support Count**  
 Displays the number of rows in which the determinant column value determines the dependent column.  
  
 **Violation Count**  
 Displays the number of rows in which the determinant column value does not determine the dependent column. (These are the rows where the dependent value is the value shown in the **\<dependent column name>** column.)  
  
 **Support Percentage**  
 Displays the percentage of rows in which the determinant column determines the dependent column.  
  
### Profile Type = Value Inclusion Profile  
  
#### Value Inclusion Profile pane  
 **Subset Side Columns**  
 Displays the column or combination of columns that were profiled to determine whether they are in the superset columns.  
  
 **Superset Side Columns**  
 Displays the column or combination of columns that were profiled to determine whether they include the values in the subset columns.  
  
 **Inclusion Strength**  
 Displays the strength (as a percentage) of the overlap between columns. A key strength of less than 100% indicates that there are cases where the subset value is not found among the superset values.  
  
#### Inclusion Violations pane  
 **\<column1>, \<column2>, etc.**  
 Displays the values in the subset column or columns that were not found in the superset column or columns.  
  
 **Count**  
 Displays the number of rows in which the specified column has the value shown in the first column.  
  
## See Also  
 [Data Profile Viewer](control-flow/data-profile-viewer.md)   
 [Data Profiling Task and Viewer](control-flow/data-profiling-task-and-viewer.md)  
  
  
