---
title: "IDSOShapeExtensions Interface | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "IDSOShapeExtensions interface [ADO]"
ms.assetid: ad4ba313-1161-4bc7-b8f6-4083305bc81e
author: MightyPen
ms.author: genemi
manager: craigg
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
  
|||  
|-|-|  
|[GetDataProviderDSO Method](../../../ado/reference/ado-api/getdataproviderdso-method.md)|Retrieves the underlying OLE DB Data Source object from the Shape provider.|  
  
## Requirements  
 **Version:** ADO 2.0 and later  
  
 **Library:** msado15.dll  
  
 **UUID:** 00000283-0000-0010-8000-00AA006D2EA4
