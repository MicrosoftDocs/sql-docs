---
title: "State Property (ADO MD)"
description: "State Property (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "State"
  - "Cellset::State"
helpviewer_keywords:
  - "State property [ADO MD]"
apitype: "COM"
---
# State Property (ADO MD)
Indicates the current state of the cellset.  
  
## Return Values  
 Returns a **Long** integer indicating the current condition of the [Cellset](./cellset-object-ado-md.md) object and is read-only. The following values are valid: **adStateClosed** (0) and **adStateOpen** (1).  
  
## Remarks  
 To use the [ObjectStateEnum](../ado-api/objectstateenum.md) constant names, you must have the ADO type library referenced in your project. See [Using ADO with ADO MD](../../guide/multidimensional/using-ado-with-ado-md.md) for more information.  
  
## Applies To  
 [Cellset Object (ADO MD)](./cellset-object-ado-md.md)  
  
## See Also  
 [Close Method (ADO MD)](./close-method-ado-md.md)   
 [Open Method (ADO MD)](./open-method-ado-md.md)