---
title: "Rowset Property (ADO)"
description: "Rowset Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "ADORecordsetConstruction::PutRowset"
  - "ADORecordsetConstruction::GetRowset"
  - "ADORecordsetConstruction::Rowset"
  - "ADORecordsetConstruction::put_Rowset"
  - "ADORecordsetConstruction::get_Rowset"
helpviewer_keywords:
  - "Rowset property [ADO]"
apitype: "COM"
---
# Rowset Property (ADO)
Gets or sets an OLE DB **Rowset** object from/on an **ADORecordsetConstruction** object. When you use put_Rowset, the rowset is turned into an ADO **Recordset** object.  
  
 Read/write.  
  
## Syntax  
  
```  
HRESULT get_Rowset([out, retval] IUnknown** ppRowset);  
HRESULT put_Rowset([in] IUnknown* pRowset);  
```  
  
## Parameters  
 *ppRowset*  
 Pointer to an OLE DB **Rowset** object.  
  
 *PRowset*  
 An OLE DB **Rowset** object.  
  
## Return Values  
 This property method returns the standard HRESULT values, including S_OK and E_FAIL.  
  
## Applies To  
 [ADORecordsetConstruction Interface](./adorecordsetconstruction-interface.md)