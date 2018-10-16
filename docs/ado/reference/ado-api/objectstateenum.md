---
title: "ObjectStateEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "ObjectStateEnum"
helpviewer_keywords: 
  - "ObjectStateEnum enumeration [ADO]"
ms.assetid: 32746558-097b-4749-989e-519aadf7e3f4
author: MightyPen
ms.author: genemi
manager: craigg
---
# ObjectStateEnum
Specifies whether an object is open or closed, connecting to a data source, executing a command, or retrieving data.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adStateClosed**|0|Indicates that the object is closed.|  
|**adStateOpen**|1|Indicates that the object is open.|  
|**adStateConnecting**|2|Indicates that the object is connecting.|  
|**adStateExecuting**|4|Indicates that the object is executing a command.|  
|**adStateFetching**|8|Indicates that the rows of the object are being retrieved.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.ObjectState.CLOSED|  
|AdoEnums.ObjectState.OPEN|  
|AdoEnums.ObjectState.CONNECTING|  
|AdoEnums.ObjectState.EXECUTING|  
|AdoEnums.ObjectState.FETCHING|  
  
## Applies To  
  
|||  
|-|-|  
|[State Property (ADO MD)](../../../ado/reference/ado-md-api/state-property-ado-md.md)|[State Property (ADO)](../../../ado/reference/ado-api/state-property-ado.md)|
