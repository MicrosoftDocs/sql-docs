---
title: "Supports Method | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset15::raw_Supports"
  - "Recordset15::Supports"
helpviewer_keywords: 
  - "Supports method [ADO]"
ms.assetid: 298fc41c-0b55-42fc-b373-c5133b4da6a5
author: MightyPen
ms.author: genemi
manager: craigg
---
# Supports Method
Determines whether a specified [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object supports a particular type of functionality.  
  
## Syntax  
  
```  
  
boolean = recordset.Supports(CursorOptions )  
```  
  
## Return Value  
 Returns a **Boolean** value that indicates whether all of the features identified by the *CursorOptions* argument are supported by the provider.  
  
#### Parameters  
 *CursorOptions*  
 A **Long** expression that consists of one or more [CursorOptionEnum](../../../ado/reference/ado-api/cursoroptionenum.md) values.  
  
## Remarks  
 Use the **Supports** method to determine what types of functionality a **Recordset** object supports. If the **Recordset** object supports the features whose corresponding constants are in *CursorOptions*, the **Supports** method returns **True**. Otherwise, it returns **False**.  
  
> [!NOTE]
>  Although the **Supports** method may return **True** for a given functionality, it does not guarantee that the provider can make the feature available under all circumstances. The **Supports** method simply returns whether the provider can support the specified functionality, assuming certain conditions are met. For example, the **Supports** method may indicate that a **Recordset** object supports updates even though the cursor is based on a multiple table join, some columns of which are not updatable.  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [Supports Method Example (VB)](../../../ado/reference/ado-api/supports-method-example-vb.md)   
 [Supports Method Example (VC++)](../../../ado/reference/ado-api/supports-method-example-vc.md)   
 [CursorType Property (ADO)](../../../ado/reference/ado-api/cursortype-property-ado.md)
