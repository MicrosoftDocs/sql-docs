---
title: "ParentRow Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "ADORecordConstruction::put_ParentRow"
  - "ADORecordConstruction::ParentRow"
  - "ADORecordConstruction::putParentRow"
helpviewer_keywords: 
  - "ParentRow property [ADO]"
ms.assetid: 5ea8029b-eda4-490b-ae84-2ad036fb582f
author: MightyPen
ms.author: genemi
manager: craigg
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
 [ADORecordConstruction Interface](../../../ado/reference/ado-api/adorecordconstruction-interface.md)
