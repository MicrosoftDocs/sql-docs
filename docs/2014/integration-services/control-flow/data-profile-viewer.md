---
title: "Data Profile Viewer | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Data Profile Viewer [Integration Services]"
  - "Data Profiling task [Integration Services], output viewer"
ms.assetid: b9043428-ce26-45bb-910c-588d07579565
author: janinezhang
ms.author: janinez
manager: craigg
---
# Data Profile Viewer
  Viewing and analyzing the data profiles is the next step in the data profiling process. You can view these profiles after you have run the Data Profiling task inside an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package and computed the data profiles. For more information about how to set up and run the Data Profiling tasks, see [Setup of the Data Profiling Task](data-profiling-task.md).  
  
> [!IMPORTANT]  
>  The output file might contain sensitive data about your database and the data that database contains. For suggestions on how to make this file more secure, see [Access to Files Used by Packages](../access-to-files-used-by-packages.md).  
  
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
  
 For more information about the user interface of the Data Profile Viewer, see [Data Profile Viewer F1 Help](../data-profile-viewer-f1-help.md).  
  
  
