---
title: "get_OLEDBCommand Method"
description: "get_OLEDBCommand Method"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "get_OLEDBCommand method [ADO]"
---
# get_OLEDBCommand Method
Returns the underlying OLE DB Command, first propagating any parameter information set on the ADO Command to the OLE DB Command.  
  
## Syntax  
  
```  
  
HRESULT get_OLEDBCommand(  
      IUnknown **ppOLEDBCommand  
);  
```  
  
#### Parameters  
 *ppOLEDBCommand*  
 [out] A pointer to a pointer location where the IUnknown pointer for the underlying OLE DB Command will be written.  
  
## Applies To  
 [IADOCommandConstruction](/previous-versions/windows/desktop/aa965677(v=vs.85))