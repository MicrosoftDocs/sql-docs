---
title: "Chapter Property (ADO)"
description: "Chapter Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "ADORecordsetConstruction::Chapter"
  - "ADORecordsetConstruction::put_Chapter"
  - "ADORecordsetConstruction::get_Chapter"
helpviewer_keywords:
  - "Chapter property [ADO]"
apitype: "COM"
---
# Chapter Property (ADO)
Gets or sets an OLE DB **Chapter** object from/on an [ADORecordsetConstruction Interface](./adorecordsetconstruction-interface.md) object. When you use **put_Chapter** to set the **Chapter** object, a subset of rows is turned into an ADO [Recordset Object](./recordset-object-ado.md) object. This sets the current chapter of the **Rowset**object. This property is read/write.  
  
## Syntax  
  
```  
HRESULT get_Chapter([out, retval] long* plChapter);  
HRESULT put_Chapter([in] long lChapter);  
```  
  
## Parameters  
 *plChapter*  
 Pointer to the handle of a chapter.  
  
 *LChapter*  
 Handle of a chapter.  
  
## Return Values  
 This property method returns the standard HRESULT values, including S_OK and E_FAIL.  
  
## Applies To  
 [ADORecordsetConstruction Interface](./adorecordsetconstruction-interface.md)