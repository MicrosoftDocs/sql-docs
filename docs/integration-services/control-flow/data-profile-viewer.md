---
title: "Data Profile Viewer | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.dataprofileviewer.f1"
helpviewer_keywords: 
  - "Data Profile Viewer [Integration Services]"
  - "Data Profiling task [Integration Services], output viewer"
ms.assetid: b9043428-ce26-45bb-910c-588d07579565
author: janinezhang
ms.author: janinez
manager: craigg
---
# Data Profile Viewer

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  Viewing and analyzing the data profiles is the next step in the data profiling process. You can view these profiles after you have run the Data Profiling task inside an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package and computed the data profiles. For more information about how to set up and run the Data Profiling tasks, see [Setup of the Data Profiling Task](../../integration-services/control-flow/setup-of-the-data-profiling-task.md).  
  
> [!IMPORTANT]  
>  The output file might contain sensitive data about your database and the data that database contains. For suggestions on how to make this file more secure, see [Access to Files Used by Packages](../../integration-services/security/security-overview-integration-services.md#files).  
  
## Data Profiles  
 To view the data profiles, you configure the Data Profiling task to send its output to a file, and then you use the stand-alone Data Profile Viewer. To open the Data Profile Viewer, do one of the following.  
  
-   Right-click the **Data Profiling** task in the [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, and then click **Edit**. Click **Open Profile Viewer** on the **General** page of the **Data Profiling Task Editor**.  
  
-   In the folder, *\<drive>*:\Program Files (x86) | Program Files\Microsoft SQL Server\110\DTS\Binn, run DataProfileViewer.exe.  
  
 The viewer uses multiple panes to display the profiles that you requested and the computed results, with optional details and drilldown capability:  
  
 **Profiles** pane  
 The **Profiles** pane displays the profiles that were requested in the Data Profile task. To view the computed results for the profile, select the profile in the **Profiles** pane and the results will appear in the other panes of the viewer.  
  
 **Results** pane  
 The **Results** pane uses a single row to summarize the computed results of the profile. For example, if you request a **Column Length Distribution Profile**, this row includes the minimum and maximum length, and the row count. For most profiles, you can select this row in the **Results** pane to see additional detail in the optional **Details** pane.  
  
 **Details** pane  
 For most profile types, the **Details** pane displays additional information about the profile results selected in the **Results** pane. For example, if you request a **Column Length Distribution Profile**, the **Details** pane displays each column length that was found. The pane also displays the number and percentage of rows in which the column value has that column length.  
  
 For the three profile types that are computed against more than one column (Candidate Key, Functional Dependency, and Value Inclusion), the **Details** pane displays violations of the expected relationship. For example, if you request a Candidate Key Profile, the Details pane displays duplicate values that violate the uniqueness of the candidate key.  
  
 If the data source that is used to compute the profile is available, you can double-click a row in the **Details** pane to see the matching rows of data in the **Drilldown** pane.  
  
 **Drilldown** pane  
 You can double-click a row in the **Details** pane to see the matching rows of data in the **Drilldown** pane when the following conditions are true:  
  
-   The data source that is used to compute the profile is available.  
  
-   You have permission to view the data.  
  
 To connect to the source database for a drilldown request, the Data Profile Viewer uses Windows Authentication and the credentials of the current user. The Data Profile Viewer does not use the connection information that is stored in the package that ran the Data Profiling task.  
  
> [!IMPORTANT]  
>  The drilldown capability that is available in the Data Profile Viewer sends live queries to the original data source. These queries may have a negative impact on the performance of the server.  
>   
>  If you drill down from an output file that was not created recently, the drilldown queries might return a different set of rows than those on which the original output was calculated.  
  
 For more information about the user interface of the Data Profile Viewer, see [Data Profile Viewer F1 Help](../../integration-services/control-flow/data-profile-viewer-f1-help.md).  
  
## Data Profile Viewer F1 Help
  Use the Data Profile Viewer to view the output of the Data Profiling task.  
  
 For more information about how to use the Data Profile Viewer, see [Data Profile Viewer](../../integration-services/control-flow/data-profile-viewer.md). For more information about how to use the Data Profiling task, which creates the profile output that you analyze in the Data Profile Viewer, see [Setup of the Data Profiling Task](../../integration-services/control-flow/setup-of-the-data-profiling-task.md).  
  
### Static Options  
 **Open**  
 Click to browse for the saved file that contains the output of the Data Profiling task.  
  
 **Profiles** pane  
 Expand the tree in the **Profiles** pane to see the profiles that are included in the output. Select a profile to view the results for that profile.  
  
 **Message** pane  
 Displays status messages.  
  
 **Drilldown** pane  
 Displays the rows of data that match a value in the output, if the data source that is used by the Data Profiling task is available.  
  
 For example, if you are viewing the output of a Column Value Distribution profile for a US State column, the **Detailed Value Distribution** pane might contain a row for "WA". Double-click the row in the **Detailed Value Distribution** pane to see the rows of data where the value of the state column is "WA" in the drilldown pane.  
  
### Dynamic Options  
  
#### Profile Type = Column Length Distribution Profile  
  
##### Column Length Distribution Profile - \<column> pane  
 **Minimum Length**  
 Displays the minimum length of values in this column.  
  
 **Maximum Length**  
 Displays the maximum length of values in this column.  
  
 **Ignore Leading Spaces**  
 Displays whether this profile was computed with an **IgnoreLeadingSpaces** value of True or False. This property was set on the **Profile Requests** page of the Data Profiling Task Editor.  
  
 **Ignore Trailing Spaces**  
 Displays whether this profile was computed with an **IgnoreTrailingSpaces** value of True or False. This property was set on the **Profile Requests** page of the Data Profiling Task Editor.  
  
 **Row Count**  
 Displays the number of rows in the table or view.  
  
##### Detailed Length Distribution pane  
 **Length**  
 Displays the column lengths found in the profiled column.  
  
 **Count**  
 Displays the number of rows in which the value of the profiled column has the length shown in the **Length** column.  
  
 **Percentage**  
 Displays the percentage of rows in which the value of the profiled column has the length shown in the **Length** column.  
  
#### Profile Type = Column Null Ratio Profile  
  
##### Column Null Ratio Profile - \<column> pane  
 **Null Count**  
 Displays the number of rows in which the profiled column has a null value.  
  
 **Null Percentage**  
 Displays the percentage of rows in which the profiled column has a null value.  
  
 **Row Count**  
 Displays the number of rows in the table or view.  
  
#### Profile Type = Column Pattern Profile  
  
##### Column Pattern Profile - \<column> pane  
 **Row Count**  
 Displays the number of rows in the table or view.  
  
##### Pattern Distribution pane  
 **Pattern**  
 Displays the patterns computed for the profiled column.  
  
 **Percentage**  
 Displays the percentage of rows whose values match the pattern displayed in the **Pattern** column.  
  
#### Profile Type = Column Statistics Profile  
  
##### Column Statistics Profile - \<column> pane  
 **Minimum**  
 Displays the minimum value found in the profiled column.  
  
 **Maximum**  
 Displays the maximum value found in the profiled column.  
  
 **Mean**  
 Displays the mean of the values found in the profiled column.  
  
 **Standard Deviation**  
 Displays the standard deviation of the values found in the profiled column.  
  
#### Profile Type = Column Value Distribution Profile  
  
##### Column Value Distribution Profile - \<column> pane  
 **Number of Distinct Values**  
 Displays the count of distinct values found in the profiled column.  
  
 **Row Count**  
 Displays the number of rows in the table or view.  
  
##### Detailed Value Distribution pane  
 **Value**  
 Displays the distinct values found in the profiled column.  
  
 **Count**  
 Displays the number of rows in which the profiled column has the value shown in the **Value** column.  
  
 **Percentage**  
 Displays the percentage of rows in which the profiled column has the value shown in the **Value** column.  
  
#### Profile Type = Candidate Key Profile  
  
##### Candidate Key Profile - \<table> pane  
 **Key Columns**  
 Displays the columns that were selected for profiling as a candidate key.  
  
 **Key Strength**  
 Displays the strength (as a percentage) of the candidate key column or combination of columns. A key strength of less than 100% indicates that duplicate values exist.  
  
##### Key Violations pane  
 **\<column1>, \<column2>, etc.**  
 Displays the duplicate values that were found in the profiled column.  
  
 **Count**  
 Displays the number of rows in which the specified column has the value shown in the first column.  
  
#### Profile Type = Functional Dependency Profile  
  
##### Functional Dependency Profile pane  
 **Determinant Columns**  
 Displays the column or columns selected as the determinant column. In the example where the same United States Zip Code should always have the same state, the Zip Code is the determinant column.  
  
 **Dependent Columns**  
 Displays the column or columns selected as the dependent column. In the example where the same United States Zip Code should always have the same state, the state is the dependent column.  
  
 **Functional Dependency Strength**  
 Displays the strength (as a percentage) of the functional dependency between columns. A key strength of less than 100% indicates that there are cases where the determinant value does not determine the dependent value. In the example where the same United States Zip Code should always have the same state, this probably indicates some state values are not valid.  
  
##### Functional Dependency Violations pane  
  
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
  
#### Profile Type = Value Inclusion Profile  
  
##### Value Inclusion Profile pane  
 **Subset Side Columns**  
 Displays the column or combination of columns that were profiled to determine whether they are in the superset columns.  
  
 **Superset Side Columns**  
 Displays the column or combination of columns that were profiled to determine whether they include the values in the subset columns.  
  
 **Inclusion Strength**  
 Displays the strength (as a percentage) of the overlap between columns. A key strength of less than 100% indicates that there are cases where the subset value is not found among the superset values.  
  
##### Inclusion Violations pane  
 **\<column1>, \<column2>, etc.**  
 Displays the values in the subset column or columns that were not found in the superset column or columns.  
  
 **Count**  
 Displays the number of rows in which the specified column has the value shown in the first column.  
  
