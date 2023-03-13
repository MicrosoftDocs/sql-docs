---
title: "IDSOShapeExtensions Interface"
description: "IDSOShapeExtensions Interface"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "IDSOShapeExtensions interface [ADO]"
---
# IDSOShapeExtensions Interface
Gets the underlying OLE DB Data Source object for the SHAPE provider.  
  
## Syntax  
  
```  
  
interface IDSOShapeExtensions: public IUnknown {  
public:  
      HRESULT  GetDataProviderDSO(  
            IUnknown **ppDataProviderDSOIUnknown  
      );  
};  
```  
  
## Methods  
  
|Method|Description|  
|-|-|  
|[GetDataProviderDSO Method](./getdataproviderdso-method.md)|Retrieves the underlying OLE DB Data Source object from the Shape provider.|  
  
## Requirements  
 **Version:** ADO 2.0 and later  
  
 **Library:** msado15.dll  
  
 **UUID:** 00000283-0000-0010-8000-00AA006D2EA4