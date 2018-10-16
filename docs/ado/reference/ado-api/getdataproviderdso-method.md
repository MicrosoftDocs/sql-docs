---
title: "GetDataProviderDSO Method | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "GetDataProviderDSO Method [ADO]"
ms.assetid: 5a4c6bd5-0c79-4f81-a977-0561392d8d50
author: MightyPen
ms.author: genemi
manager: craigg
---
# GetDataProviderDSO Method
Retrieves the underlying OLE DB Data Source object from the Shape provider.  
  
## Syntax  
  
```  
  
HRESULT GetDataProviderDSO(  
      IUnknown **ppDataProviderDSOIUnknown  
);  
```  
  
#### Parameters  
 *ppDataProviderDSOIUnknown*  
 [out]  A pointer to a pointer that returns the IUnknown of the underlying OLE DB Data Source object.  
  
## Remarks  
 This method does not addref the interface pointer. If the caller plans to hold the pointer, the caller must do the required addref and release.  
  
## Applies to  
 [IDSOShapeExtensions Interface](../../../ado/reference/ado-api/idsoshapeextensions-interface.md)
