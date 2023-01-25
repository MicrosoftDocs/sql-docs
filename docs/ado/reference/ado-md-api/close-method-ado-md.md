---
title: "Close Method (ADO MD)"
description: "Close Method (ADO MD)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Close method [ADO MD]"
apitype: "COM"
---

# Close Method (ADO MD)

Closes an open cellset.  
  
## Syntax  
  
```  
  
Cellset.Close  
```  
  
## Remarks  
 Using the **Close** method to close a [Cellset](./cellset-object-ado-md.md) object will release the associated data, including data in any related [Cell](./cell-object-ado-md.md), [Axis](./axis-object-ado-md.md), [Position](./position-object-ado-md.md), or [Member](./member-object-ado-md.md) objects. Closing a **Cellset** does not remove it from memory; you can change its property settings and open it again later. To completely eliminate an object from memory, set the object variable to **Nothing**.  
  
 You can later call the [Open](./open-method-ado-md.md) method to reopen the **Cellset** using the same or another source string. While the **Cellset** object is closed, retrieving any properties or calling any methods that reference the underlying data or metadata generates an error.  
  
## Applies To  
 [Cellset Object (ADO MD)](./cellset-object-ado-md.md)  
  
## See Also  
 [Axis Object (ADO MD)](./axis-object-ado-md.md)   
 [Cell Object (ADO MD)](./cell-object-ado-md.md)   
 [Member Object (ADO MD)](./member-object-ado-md.md)   
 [Open Method (ADO MD)](./open-method-ado-md.md)   
 [Position Object (ADO MD)](./position-object-ado-md.md)   
 [State Property (ADO MD)](./state-property-ado-md.md)