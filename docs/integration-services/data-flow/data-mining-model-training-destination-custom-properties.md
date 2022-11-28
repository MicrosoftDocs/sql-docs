---
description: "Data Mining Model Training Destination Custom Properties"
title: "Data Mining Model Training Destination Custom Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
ms.assetid: f0a70216-fdac-44ae-af29-35f65626217c
author: chugugrace
ms.author: chugu
---
# Data Mining Model Training Destination Custom Properties

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

> [!IMPORTANT]
> Data mining was deprecated in [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Analysis Services and now discontinued in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Analysis Services. Documentation is not updated for deprecated and discontinued features. To learn more, see [Analysis Services backward compatibility](/analysis-services/analysis-services-backward-compatibility).

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
 [Common Properties](./set-the-properties-of-a-data-flow-component.md)  
  
