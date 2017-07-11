---
title: "Dimension Processing Destination | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.dimensionprocessingdest.f1"
helpviewer_keywords: 
  - "Dimension Processing destination"
  - "loading dimensions"
  - "destinations [Integration Services], Dimension Processing"
  - "dimensions [Analysis Services], processing"
ms.assetid: 4c49bb95-7259-42f4-a785-bb6aaf5f8566
caps.latest.revision: 40
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Dimension Processing Destination
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
  
 For more information about the properties that you can set in the **Dimension Processing Destination Editor** dialog box, click one of the following topics:  
  
-   [Dimension Processing Destination Editor &#40;Connection Manager Page&#41;](../../integration-services/data-flow/dimension-processing-destination-editor-connection-manager-page.md)  
  
-   [Dimension Processing Destination Editor &#40;Mappings Page&#41;](../../integration-services/data-flow/dimension-processing-destination-editor-mappings-page.md)  
  
-   [Dimension Processing Destination Editor &#40;Advanced Page&#41;](../../integration-services/data-flow/dimension-processing-destination-editor-advanced-page.md)  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topic:  
  
-   [Common Properties](http://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
 For more information about how to set the properties, see [Set the Properties of a Data Flow Component](../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
## See Also  
 [Data Flow](../../integration-services/data-flow/data-flow.md)   
 [Integration Services Transformations](../../integration-services/data-flow/transformations/integration-services-transformations.md)  
  
  