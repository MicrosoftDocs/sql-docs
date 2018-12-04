---
title: "Delete Method (ADO Parameters Collection) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_DynaCollection::Delete"
  - "_DynaCollection::raw_Delete"
helpviewer_keywords: 
  - "Delete method [ADO]"
ms.assetid: 160c575e-df63-4ade-a2d3-5fd8f72e70cc
author: MightyPen
ms.author: genemi
manager: craigg
---
# Delete Method (ADO Parameters Collection)
Deletes an object from the [Parameters](../../../ado/reference/ado-api/parameters-collection-ado.md) collection.  
  
## Syntax  
  
```  
  
Parameters.Delete Index  
```  
  
#### Parameters  
 *Index*  
 A **String** value that contains the name of the object you want to delete, or the object's ordinal position (index) in the collection.  
  
## Remarks  
 Using the **Delete** method on a collection lets you remove one of the objects in the collection. This method is available only on the **Parameters** collection of a [Command](../../../ado/reference/ado-api/command-object-ado.md) object. You must use the [Parameter](../../../ado/reference/ado-api/parameter-object.md) object's [Name](../../../ado/reference/ado-api/name-property-ado.md) property or its collection index when calling the **Delete** method-an object variable is not a valid argument.  
  
## Applies To  
 [Parameters Collection (ADO)](../../../ado/reference/ado-api/parameters-collection-ado.md)  
  
## See Also  
 [Delete Method (ADO Fields Collection)](../../../ado/reference/ado-api/delete-method-ado-fields-collection.md)   
 [Delete Method (ADO Recordset)](../../../ado/reference/ado-api/delete-method-ado-recordset.md)   
 [DeleteRecord Method (ADO)](../../../ado/reference/ado-api/deleterecord-method-ado.md)
