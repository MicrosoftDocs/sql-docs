---
title: "Open Method (ADO MD) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Open"
  - "Cellset::Open"
helpviewer_keywords: 
  - "Open method [ADO MD]"
ms.assetid: a87d8080-a238-45e5-bc80-9a8625b3810f
author: MightyPen
ms.author: genemi
manager: craigg
---
# Open Method (ADO MD)
Retrieves the results of a multidimensional query and returns the results to a [Cellset](../../../ado/reference/ado-md-api/cellset-object-ado-md.md).  
  
## Syntax  
  
```  
  
Cellset.Open Source, ActiveConnection  
```  
  
#### Parameters  
 *Source*  
 Optional. A **Variant** that evaluates to a valid multidimensional query, such as a Multidimensional Expression (MDX) query. The *Source* argument corresponds to the [Source](../../../ado/reference/ado-md-api/source-property-ado-md.md) property. For more information about MDX, see the [OLE DB for Online Analytical Processing (OLAP)](https://msdn.microsoft.com/8a7673c6-3ca1-4411-9f1e-adf1e47df4f3) documentation in the Microsoft Data Access Components SDK.  
  
 *ActiveConnection*  
 Optional. A **Variant** that evaluates to a string specifying either a valid ADO [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object variable name or a definition for a connection. The *ActiveConnection* argument specifies the connection in which to open the [Cellset](../../../ado/reference/ado-md-api/cellset-object-ado-md.md) object. If you pass a connection definition for this argument, ADO opens a new connection using the specified parameters. The *ActiveConnection* argument corresponds to the [ActiveConnection](../../../ado/reference/ado-md-api/activeconnection-property-ado-md.md) property.  
  
## Remarks  
 The **Open** method generates an error if either of its parameters is omitted and its corresponding property value has not been set prior to attempting to open the **Cellset**.  
  
## Applies To  
 [Cellset Object (ADO MD)](../../../ado/reference/ado-md-api/cellset-object-ado-md.md)  
  
## See Also  
 [Cellset Example (VB)](../../../ado/reference/ado-md-api/cellset-example-vb.md)   
 [ActiveConnection Property (ADO MD)](../../../ado/reference/ado-md-api/activeconnection-property-ado-md.md)   
 [Close Method (ADO MD)](../../../ado/reference/ado-md-api/close-method-ado-md.md)   
 [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)   
 [Source Property (ADO MD)](../../../ado/reference/ado-md-api/source-property-ado-md.md)   
 [State Property (ADO MD)](../../../ado/reference/ado-md-api/state-property-ado-md.md)
