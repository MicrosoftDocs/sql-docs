---
title: "Dimension Processing Destination | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.dimensionprocessingdest.f1"
  - "sql13.dts.designer.dimprocessingtransformation.connection.f1"
  - "sql13.dts.designer.dimprocessingtransformation.mappings.f1"
  - "sql13.dts.designer.dimprocessingtransformation.advanced.f1"
helpviewer_keywords: 
  - "Dimension Processing destination"
  - "loading dimensions"
  - "destinations [Integration Services], Dimension Processing"
  - "dimensions [Analysis Services], processing"
ms.assetid: 4c49bb95-7259-42f4-a785-bb6aaf5f8566
author: janinezhang
ms.author: janinez
manager: craigg
---
# Dimension Processing Destination

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The Dimension Processing destination loads and processes an [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] dimension. For more information about dimensions, see [Dimensions &#40;Analysis Services - Multidimensional Data&#41;](../../analysis-services/multidimensional-models-olap-logical-dimension-objects/dimensions-analysis-services-multidimensional-data.md).  
  
 The Dimension Processing destination includes the following features:  
  
-   Options to perform incremental, full, or update processing.  
  
-   Error configuration, to specify whether dimension processing ignores errors or stops after a specified number of errors.  
  
-   Mapping of input columns to columns in dimension tables.  
  
 For more information about processing [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects, see [Processing Options and Settings &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/processing-options-and-settings-analysis-services.md).  
  
## Configuration of the Dimension Processing Destination  
 The Dimension Processing destination uses an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] connection manager to connect to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project or the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] that contains the dimensions the destination processes. For more information, see [Analysis Services Connection Manager](../../integration-services/connection-manager/analysis-services-connection-manager.md).  
  
 This destination has one input. It does not support an error output.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topic:  
  
-   [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
 For more information about how to set the properties, see [Set the Properties of a Data Flow Component](../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
## Dimension Processing Destination Editor (Connection Manager Page)
  Use the **Connection Manager** page of the **Dimension Processing Destination Editor** dialog box to specify a connection to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project or to an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
### Options  
 **Connection manager**  
 Select an existing connection manager from the list, or click **New** to create a new connection manager.  
  
 **New**  
 Create a new connection by using the **Add Analysis Services Connection Manager** dialog box.  
  
 **List of available dimensions**  
 Select the dimension to process.  
  
 **Processing method**  
 Select the processing method to apply to the dimension selected in the list. The default value of this option is **Full**.  
  
|Value|Description|  
|-----------|-----------------|  
|**Add (incremental)**|Perform an incremental processing of the dimension.|  
|**Full**|Perform full processing of the dimension.|  
|**Update**|Perform an update processing of the dimension.|  
  
## Dimension Processing Destination Editor (Mappings Page)
  Use the **Mappings** page of the **Dimension Processing Destination Editor** dialog box to map input columns to dimension columns.  
  
### Options  
 **Available Input Columns**  
 View the list of available input columns. Use a drag-and-drop operation to map available input columns in the table to destination columns.  
  
 **Available Destination Columns**  
 View the list of available destination columns. Use a drag-and-drop operation to map available destination columns in the table to input columns.  
  
 **Input Column**  
 View input columns selected from the table above. You can change the mappings by using the list of **Available Input Columns**.  
  
 **Destination Column**  
 View each available destination column, and whether it is mapped or not.  
  
## Dimension Processing Destination Editor (Advanced Page)
  Use the **Advanced** page of the **Dimension Processing Destination Editor** dialog box to configure error handling.  
  
### Options  
 **Use default error configuration.**  
 Specify whether to use the default [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] error handling. By default, this value is **True**.  
  
 **Key error action**  
 Specify how to handle records that have unacceptable key values.  
  
|Value|Description|  
|-----------|-----------------|  
|**ConvertToUnknown**|Convert the unacceptable key value to the **UnknownMember** value.|  
|**DiscardRecord**|Discard the record.|  
  
 **Ignore errors**  
 Specify that errors should be ignored.  
  
 **Stop on error**  
 Specify that processing should stop when an error occurs.  
  
 **Number of errors**  
 Specify the error threshold at which processing should stop, if you have selected **Stop on errors**.  
  
 **On error action**  
 Specify the action to take when the error threshold is reached, if you have selected **Stop on errors**.  
  
|Value|Description|  
|-----------|-----------------|  
|**StopProcessing**|Stop processing.|  
|**StopLogging**|Stop logging errors.|  
  
 **Key not found**  
 Specify the action to take for a key not found error. By default, this value is **ReportAndContinue**.  
  
|Value|Description|  
|-----------|-----------------|  
|**IgnoreError**|Ignore the error and continue processing.|  
|**ReportAndContinue**|Report the error and continue processing.|  
|**ReportAndStop**|Report the error and stop processing.|  
  
 **Duplicate key**  
 Specify the action to take for a duplicate key error. By default, this value is **IgnoreError**.  
  
|Value|Description|  
|-----------|-----------------|  
|**IgnoreError**|Ignore the error and continue processing.|  
|**ReportAndContinue**|Report the error and continue processing.|  
|**ReportAndStop**|Report the error and stop processing.|  
  
 **Null key converted to unknown**  
 Specify the action to take when a null key has been converted to the **UnknownMember** value. By default, this value is **IgnoreError**.  
  
|Value|Description|  
|-----------|-----------------|  
|**IgnoreError**|Ignore the error and continue processing.|  
|**ReportAndContinue**|Report the error and continue processing.|  
|**ReportAndStop**|Report the error and stop processing.|  
  
 **Null key not allowed**  
 Specify the action to take when null keys are not allowed and a null key is encountered. By default, this value is **ReportAndContinue**.  
  
|Value|Description|  
|-----------|-----------------|  
|**IgnoreError**|Ignore the error and continue processing.|  
|**ReportAndContinue**|Report the error and continue processing.|  
|**ReportAndStop**|Report the error and stop processing.|  
  
 **Error log path**  
 Type the path of the error log, or click the **browse(...)** button to select a destination.  
  
 **Browse (...)**  
 Select a path for the error log.  
  
## See Also  
 [Data Flow](../../integration-services/data-flow/data-flow.md)   
 [Integration Services Transformations](../../integration-services/data-flow/transformations/integration-services-transformations.md)  
  
  
