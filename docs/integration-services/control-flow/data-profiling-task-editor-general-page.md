---
title: "Data Profiling Task Editor (General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.dataprofilingtask.general.f1"
helpviewer_keywords: 
  - "Data Profiling Task Editor"
ms.assetid: eec15906-d757-4079-b2f6-aca4e52b3b4c
author: janinezhang
ms.author: janinez
manager: craigg
---
# Data Profiling Task Editor (General Page)

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  Use the **General** page of the **Data Profiling Task Editor** to configure the following options:  
  
-   Specify the destination for the profile output.  
  
-   Use the default settings to simplify the task of profiling a single table or view.  
  
 For more information about how to use the Data Profiling task, see [Setup of the Data Profiling Task](../../integration-services/control-flow/setup-of-the-data-profiling-task.md). For more information about how to use the Data Profile Viewer to analyze the output of the Data Profiling task, see [Data Profile Viewer](../../integration-services/control-flow/data-profile-viewer.md).  
  
 **To open the General page of the Data Profiling Task Editor**  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], open the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package that has the Data Profiling task.  
  
2.  On the **Control Flow** tab, double-click the Data Profiling task.  
  
3.  In the **Data Profiling Task Editor**, click **General**.  
  
## Data Profiling Options  
 **Timeout**  
 Specify the number of seconds after which the Data Profiling task should timeout and stop running. The default value is 0, which indicates no timeout.  
  
## Destination Options  
  
> [!IMPORTANT]  
>  The output file might contain sensitive data about your database and the data that database contains. For suggestions about how to make this file more secure, see [Access to Files Used by Packages](../../integration-services/security/security-overview-integration-services.md#files).  
  
 **DestinationType**  
 Specify whether to save the data profile output to a file or a variable:  
  
|Value|Description|  
|-----------|-----------------|  
|**FileConnection**|Save the profile output to a file in the location that is specified in a File connection manager.<br /><br /> Note: You specify which File connection manager to use in the **Destination** option.|  
|**Variable**|Save the profile output to a package variable.<br /><br /> Note: You specify which package variable to use in the **Destination** option.|  
  
 **Destination**  
 Specify which File connection manager or package variable contains the data profile output:  
  
-   If the **DestinationType** option is set to **FileConnection**, the **Destination** option displays the available File connection managers. Select one of these connection managers, or select \<New File connection> to create a new File connection manager.  
  
-   If the **DestinationType** option is set to **Variable**, the **Destination** option displays the available package variables in the **Destination** list. Select one of these variables, or select \<New Variable> to create a new variable.  
  
 **OverwriteDestination**  
 Specify whether to overwrite the output file if it already exists. The default value is **False**. The value of this property is used only when the DestinationType option is set to FileConnection. When the DestinationType option is set to Variable, the task always overwrites the previous value of the variable.  
  
> [!IMPORTANT]  
>  If you try to run the Data Profiling task more than once without changing the output file name or changing the value of the **OverwriteDestination** property to **True**, the task fails with the message that the output file already exists.  
  
## Other Options  
 **Quick Profile**  
 Display the **Single Table Quick Profile Form**. This form simplifies the task of profiling a single table or view by using default settings. For more information, see [Single Table Quick Profile Form &#40;Data Profiling Task&#41;](../../integration-services/control-flow/single-table-quick-profile-form-data-profiling-task.md).  
  
 **Open Profile Viewer**  
 Opens the Data Profile Viewer. The stand-alone Data Profile Viewer displays data profile output from the Data Profiling task. You can view the data profile output after you have run the Data Profiling task inside the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package and computed the data profiles.  
  
> [!NOTE]  
>  You can also open the Data Profile Viewer by running the DataProfileViewer.exe in the folder, *\<drive>*:\Program Files (x86) | Program Files\Microsoft SQL Server\110\DTS\Binn.  
  
## See Also  
 [Single Table Quick Profile Form &#40;Data Profiling Task&#41;](../../integration-services/control-flow/single-table-quick-profile-form-data-profiling-task.md)   
 [Data Profiling Task Editor &#40;Profile Requests Page&#41;](../../integration-services/control-flow/data-profiling-task-editor-profile-requests-page.md)  
  
  
