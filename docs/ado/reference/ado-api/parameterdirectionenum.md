---
title: "ParameterDirectionEnum | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "ParameterDirectionEnum"
helpviewer_keywords: 
  - "ParameterDirectionEnum enumeration [ADO]"
ms.assetid: c66aa6e6-d4f0-4f0f-9640-e08ae6cfdef3
author: MightyPen
ms.author: genemi
manager: craigg
---
# ParameterDirectionEnum
Specifies whether the [Parameter](../../../ado/reference/ado-api/parameter-object.md) represents an input parameter, an output parameter, both an input and an output parameter, or the return value from a stored procedure.  
  
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
  
|||  
|-|-|  
|[CreateParameter Method (ADO)](../../../ado/reference/ado-api/createparameter-method-ado.md)|[Direction Property](../../../ado/reference/ado-api/direction-property.md)|
