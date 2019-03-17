---
title: "Row Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "ADORecordConstruction::PutRow"
  - "ADORecordConstruction::GetRow"
  - "ADORecordConstruction::get_Row"
  - "ADORecordConstruction::Row"
  - "ADORecordConstruction::put_Row"
helpviewer_keywords: 
  - "Row property [ADO]"
ms.assetid: 21019d89-2dd1-4a26-ac6f-384b81d66949
author: MightyPen
ms.author: genemi
manager: craigg
---
# Row Property (ADO)
Gets or sets an OLE DB **Row** object from or on an [ADORecordConstruction Interface](../../../ado/reference/ado-api/adorecordconstruction-interface.md) object. When you use **put_Row** to set a **Row** object, a row is turned into an ADO **Record** object.  
  
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
 [ADORecordConstruction Interface](../../../ado/reference/ado-api/adorecordconstruction-interface.md)
