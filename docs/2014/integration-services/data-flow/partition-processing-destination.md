---
title: "Partition Processing Destination | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.partitionprocessingdest.f1"
helpviewer_keywords: 
  - "partitions [Analysis Services], processing"
  - "Partition Processing destination [Integration Services]"
  - "destinations [Integration Services], Partition Processing"
ms.assetid: 36c592ff-3f78-4a58-b496-31c1c8eee131
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Partition Processing Destination
  The Partition Processing destination loads and processes an [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] partition. For more information about partitions, see [Partitions &#40;Analysis Services - Multidimensional Data&#41;](../../analysis-services/multidimensional-models-olap-logical-cube-objects/partitions-analysis-services-multidimensional-data.md).  
  
 The Partition Processing destination includes the following features:  
  
-   Options to perform incremental, full, or update processing.  
  
-   Error configuration, to specify whether processing ignores errors or stops after a specified number of errors.  
  
-   Mapping of input columns to partition columns.  
  
 For more information about processing [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] objects, see [Processing Options and Settings &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/processing-options-and-settings-analysis-services.md).  
  
> [!NOTE]  
>  Tasks described here do not apply to Analysis Services tabular models.  You cannot map input columns to partition columns for tabular models. You can instead use the Analysis Services Execute DDL task [Analysis Services Execute DDL Task](../control-flow/analysis-services-execute-ddl-task.md) to process the partition.  
  
## Configuration of the Partition Processing Destination  
 The Partition Processing destination uses an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] connection manager to connect to the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project or the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] that contains the cubes and partitions the destination processes. For more information, see [Analysis Services Connection Manager](../connection-manager/analysis-services-connection-manager.md).  
  
 This destination has one input. It does not support an error output.  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in the Partition Processing **Destination Editor** dialog box, click one of the following topics:  
  
-   [Partition Processing Destination Editor &#40;Connection Manager Page&#41;](../partition-processing-destination-editor-connection-manager-page.md)  
  
-   [Partition Processing Destination Editor &#40;Mappings Page&#41;](../partition-processing-destination-editor-mappings-page.md)  
  
-   [Partition Processing Destination Editor &#40;Advanced Page&#41;](../partition-processing-destination-editor-advanced-page.md)  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](../common-properties.md)  
  
-   [Partition Processing Destination Custom Properties](partition-processing-destination-custom-properties.md)  
  
 For more information about how to set the properties, see [Set the Properties of a Data Flow Component](set-the-properties-of-a-data-flow-component.md).  
  
  
