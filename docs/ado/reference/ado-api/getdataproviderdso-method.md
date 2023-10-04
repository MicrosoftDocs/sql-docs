---
title: "GetDataProviderDSO Method"
description: "GetDataProviderDSO Method"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "GetDataProviderDSO Method [ADO]"
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
 [IDSOShapeExtensions Interface](./idsoshapeextensions-interface.md)