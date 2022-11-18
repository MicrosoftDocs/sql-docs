---
title: "Stream Property"
description: "Stream Property"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "ADOStreamConstruction::GetStream"
  - "ADOStreamConstruction::PutStream"
  - "ADOStreamConstruction::put_Stream"
  - "ADOStreamConstruction::Stream"
  - "ADOStreamConstruction::get_Stream"
helpviewer_keywords:
  - "Stream property"
apitype: "COM"
---
# Stream Property
Gets or sets an OLE DB **Stream** object from/on an **ADOStreamConstruction** object.  
  
 Read/write.  
  
## Syntax  
  
```  
HRESULT get_Stream([out, retval] IUnknown** ppStream);  
HRESULT put_Stream([in] IUnknown* pStream);  
```  
  
## Parameters  
 *ppStream*  
 Pointer to an OLE DB **Stream** object.  
  
 *pStream*  
 An OLE DB **Stream** object.  
  
## Return Values  
 This property method returns the standard HRESULT values. This includes S_OK and E_FAIL.  
  
## Applies To  
 [ADOStreamConstruction Interface](./adostreamconstruction-interface.md)