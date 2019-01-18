---
title: "Chapter Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "ADORecordsetConstruction::Chapter"
  - "ADORecordsetConstruction::put_Chapter"
  - "ADORecordsetConstruction::get_Chapter"
helpviewer_keywords: 
  - "Chapter property [ADO]"
ms.assetid: 8aa90cb0-f588-4141-9dc9-3b22918394ee
author: MightyPen
ms.author: genemi
manager: craigg
---
# Chapter Property (ADO)
Gets or sets an OLE DB **Chapter** object from/on an [ADORecordsetConstruction Interface](../../../ado/reference/ado-api/adorecordsetconstruction-interface.md) object. When you use **put_Chapter** to set the **Chapter** object, a subset of rows is turned into an ADO [Recordset Object](../../../ado/reference/ado-api/recordset-object-ado.md) object. This sets the current chapter of the **Rowset**object. This property is read/write.  
  
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
 [ADORecordsetConstruction Interface](../../../ado/reference/ado-api/adorecordsetconstruction-interface.md)
