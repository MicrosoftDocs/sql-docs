---
title: "ParameterDirectionEnum"
description: "ParameterDirectionEnum"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "ParameterDirectionEnum"
helpviewer_keywords:
  - "ParameterDirectionEnum enumeration [ADO]"
apitype: "COM"
---
# ParameterDirectionEnum
Specifies whether the [Parameter](./parameter-object.md) represents an input parameter, an output parameter, both an input and an output parameter, or the return value from a stored procedure.  
  
|Constant|Value|Description|  
|--------------|-----------|-----------------|  
|**adParamInput**|1|Default. Indicates that the parameter represents an input parameter.|  
|**adParamInputOutput**|3|Indicates that the parameter represents both an input and output parameter.|  
|**adParamOutput**|2|Indicates that the parameter represents an output parameter.|  
|**adParamReturnValue**|4|Indicates that the parameter represents a return value.|  
|**adParamUnknown**|0|Indicates that the parameter direction is unknown.|  
  
## ADO/WFC Equivalent  
 Package: **com.ms.wfc.data**  
  
|Constant|  
|--------------|  
|AdoEnums.ParameterDirection.INPUT|  
|AdoEnums.ParameterDirection.INPUTOUTPUT|  
|AdoEnums.ParameterDirection.OUTPUT|  
|AdoEnums.ParameterDirection.RETURNVALUE|  
|AdoEnums.ParameterDirection.UNKNOWN|  
  
## Applies To  

:::row:::
    :::column:::
        [CreateParameter Method (ADO)](./createparameter-method-ado.md)  
    :::column-end:::
    :::column:::
        [Direction Property](./direction-property.md)  
    :::column-end:::
:::row-end:::