---
title: "ObjectStateEnum"
description: "ObjectStateEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "ObjectStateEnum"
helpviewer_keywords:
  - "ObjectStateEnum enumeration [ADO]"
apitype: "COM"
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

:::row:::
    :::column:::
        [State Property (ADO)](./state-property-ado.md)  
    :::column-end:::
    :::column:::
        [State Property (ADO MD)](../ado-md-api/state-property-ado-md.md)  
    :::column-end:::
:::row-end:::