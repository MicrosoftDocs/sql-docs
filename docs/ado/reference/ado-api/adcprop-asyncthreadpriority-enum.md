---
title: "ADCPROP_ASYNCTHREADPRIORITY_ENUM | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "ADCPROP_ASYNCTHREADPRIORITY_ENUM"
helpviewer_keywords: 
  - "ADCPROP_ASYNCTHREADPRIORITY_ENUM [ADO]"
ms.assetid: f0965617-17d8-41e0-98d0-f824274735a6
author: MightyPen
ms.author: genemi
manager: craigg
---
# ADCPROP_ASYNCTHREADPRIORITY_ENUM
For an RDS [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object, specifies the execution priority of the asynchronous thread that retrieves data.  
  
 Use these constants with the **Recordset** "**Background Thread Priority**" dynamic property, which is referenced in the ADO-to-OLE DB Dynamic Property index and documented in the [Microsoft Cursor Service for OLE DB](../../../ado/guide/appendixes/microsoft-cursor-service-for-ole-db-ado-service-component.md) documentation.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adPriorityAboveNormal**|4|Sets priority between normal and highest.|  
|**adPriorityBelowNormal**|2|Sets priority between lowest and normal.|  
|**adPriorityHighest**|5|Sets priority to the highest possible.|  
|**AdPriorityLowest**|1|Sets priority to the lowest possible.|  
|**adPriorityNormal**|3|Sets priority to normal.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.AdcPropAsyncThreadPriority.ABOVENORMAL|  
|AdoEnums.AdcPropAsyncThreadPriority.BELOWNORMAL|  
|AdoEnums.AdcPropAsyncThreadPriority.HIGHEST|  
|AdoEnums.AdcPropAsyncThreadPriority.LOWEST|  
|AdoEnums.AdcPropAsyncThreadPriority.NORMAL|
