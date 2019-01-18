---
title: "RowPosition Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "ADORecordConstruction::put_RowPosition"
  - "ADORecordConstruction::PutRowPosition"
  - "ADORecordConstruction::GetRowPosition"
  - "ADORecordConstruction::RowPosition"
  - "ADORecordConstruction::get_RowPosition"
helpviewer_keywords: 
  - "RowPosition property [ADO]"
ms.assetid: 9d068fed-39bf-4842-afc3-686a2af2145d
author: MightyPen
ms.author: genemi
manager: craigg
---
# RowPosition Property (ADO)
Gets or sets an OLE DB **RowPosition** object from/on an **ADORecordsetConstruction** object. When you use **put_RowPosition** to set the **RowPosition** object, the resulting **Recordset** object uses the **RowPosition** object to determine the current row.  
  
 Read/write.  
  
## Syntax  
  
```  
HRESULT get_RowPosition([out, retval] IUnknown** ppRowPos);  
HRESULT put_RowPosition([in] IUnknown* pRowPos);  
```  
  
## Parameters  
 *ppRowPos*  
 Pointer to an OLE DB **RowPosition** object.  
  
 *PRowPos*  
 An OLE DB **RowPosition** object.  
  
## Return Values  
 This property method returns the standard HRESULT values, including S_OK and E_FAIL.  
  
## Remarks  
 When this property is set, if the **Rowset** object on the **RowPosition** object is different from the **Rowset** object on the **Recordset** object, the former overrides the latter. The same behavior applies to the current **Chapter** of the **RowPosition** as well.  
  
## Applies To  
 [ADORecordsetConstruction Interface](../../../ado/reference/ado-api/adorecordsetconstruction-interface.md)
