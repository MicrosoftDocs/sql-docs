---
title: "ParentRow Property (ADO)"
description: "ParentRow Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "ADORecordConstruction::put_ParentRow"
  - "ADORecordConstruction::ParentRow"
  - "ADORecordConstruction::putParentRow"
helpviewer_keywords:
  - "ParentRow property [ADO]"
apitype: "COM"
---
# ParentRow Property (ADO)
Sets the container of an OLE DB **Row** object on an **ADORecordConstruction** object, so that the parent of the row is turned into an ADO **Record** object.  
  
 Write-only.  
  
## Syntax  
  
```  
HRESULT put_ParentRow([in] IUnknown* pParent);  
```  
  
## Parameters  
 *pParent*  
 A container of a row.  
  
## Return Values  
 This property method returns the standard HRESULT values, including S_OK and E_FAIL.  
  
## Applies To  
 [ADORecordConstruction Interface](./adorecordconstruction-interface.md)