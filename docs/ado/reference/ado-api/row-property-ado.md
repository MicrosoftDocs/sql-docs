---
title: "Row Property (ADO)"
description: "Row Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "ADORecordConstruction::PutRow"
  - "ADORecordConstruction::GetRow"
  - "ADORecordConstruction::get_Row"
  - "ADORecordConstruction::Row"
  - "ADORecordConstruction::put_Row"
helpviewer_keywords:
  - "Row property [ADO]"
apitype: "COM"
---
# Row Property (ADO)
Gets or sets an OLE DB **Row** object from or on an [ADORecordConstruction Interface](./adorecordconstruction-interface.md) object. When you use **put_Row** to set a **Row** object, a row is turned into an ADO **Record** object.  
  
## Read/write.Syntax  
  
```  
HRESULT get_Row([out, retval] IUnknown** ppRow);  
HRESULT put_Row([in] IUnknown* pRow);  
```  
  
## Parameters  
 *ppRow*  
 Pointer to an OLE DB **Row** object.  
  
 *PRow*  
 An OLE DB **Row** object.  
  
## Return Values  
 This property method returns the standard HRESULT values, including S_OK and E_FAIL.  
  
## Applies To  
 [ADORecordConstruction Interface](./adorecordconstruction-interface.md)