---
title: "Close Method (ADO MD) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Close"
  - "Cellset::Close"
helpviewer_keywords: 
  - "Close method [ADO MD]"
ms.assetid: a3aa594d-f9d4-4654-8625-ec20153ff5d9
author: MightyPen
ms.author: genemi
manager: craigg
---
# Close Method (ADO MD)
Closes an open cellset.  
  
## Syntax  
  
```  
  
Cellset.Close  
```  
  
## Remarks  
 Using the **Close** method to close a [Cellset](../../../ado/reference/ado-md-api/cellset-object-ado-md.md) object will release the associated data, including data in any related [Cell](../../../ado/reference/ado-md-api/cell-object-ado-md.md), [Axis](../../../ado/reference/ado-md-api/axis-object-ado-md.md), [Position](../../../ado/reference/ado-md-api/position-object-ado-md.md), or [Member](../../../ado/reference/ado-md-api/member-object-ado-md.md) objects. Closing a **Cellset** does not remove it from memory; you can change its property settings and open it again later. To completely eliminate an object from memory, set the object variable to **Nothing**.  
  
 You can later call the [Open](../../../ado/reference/ado-md-api/open-method-ado-md.md) method to reopen the **Cellset** using the same or another source string. While the **Cellset** object is closed, retrieving any properties or calling any methods that reference the underlying data or metadata generates an error.  
  
## Applies To  
 [Cellset Object (ADO MD)](../../../ado/reference/ado-md-api/cellset-object-ado-md.md)  
  
## See Also  
 [Axis Object (ADO MD)](../../../ado/reference/ado-md-api/axis-object-ado-md.md)   
 [Cell Object (ADO MD)](../../../ado/reference/ado-md-api/cell-object-ado-md.md)   
 [Member Object (ADO MD)](../../../ado/reference/ado-md-api/member-object-ado-md.md)   
 [Open Method (ADO MD)](../../../ado/reference/ado-md-api/open-method-ado-md.md)   
 [Position Object (ADO MD)](../../../ado/reference/ado-md-api/position-object-ado-md.md)   
 [State Property (ADO MD)](../../../ado/reference/ado-md-api/state-property-ado-md.md)
