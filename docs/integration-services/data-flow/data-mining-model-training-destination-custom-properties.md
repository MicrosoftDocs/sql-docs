---
title: "Data Mining Model Training Destination Custom Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: f0a70216-fdac-44ae-af29-35f65626217c
caps.latest.revision: 6
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Data Mining Model Training Destination Custom Properties
  The Data Mining Model Training destination has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the Data Mining Model Training destination. All properties are read/write.  
  
|Property|Data Type|Description|  
|--------------|---------------|-----------------|  
|ASConnectionId|String|The unique identifier of the connection manager.|  
|ASConnectionString|String|The connection string to an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] or to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project.|  
|ObjectRef|String|An XML tag that identifies the data mining structure that the transformation uses.|  
  
 The input and the input columns of the Data Mining Model Training destination have no custom properties.  
  
 For more information, see [Data Mining Model Training Destination](../../integration-services/data-flow/data-mining-model-training-destination.md).  
  
## See Also  
 [Common Properties](http://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
  