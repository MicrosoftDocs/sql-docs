---
title: "DefaultDatabase Property | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Connection15::DefaultDatabase"
helpviewer_keywords: 
  - "DefaultDatabase property"
ms.assetid: 41e8a8dd-e69c-4a09-8736-93502e01961c
author: MightyPen
ms.author: genemi
manager: craigg
---
# DefaultDatabase Property
Indicates the default database for a [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object.  
  
## Settings and Return Values  
 Sets or returns a **String** value that evaluates to the name of a database available from the provider.  
  
## Remarks  
 Use the **DefaultDatabase** property to set or return the name of the default database on a specific **Connection** object.  
  
 If there is a default database, SQL strings may use an unqualified syntax to access objects in that database. To access objects in a database other than the one specified in the **DefaultDatabase** property, you must qualify object names with the desired database name. Upon connection, the provider will write default database information to the **DefaultDatabase** property. Some providers allow only one database per connection, in which case you cannot change the **DefaultDatabase** property.  
  
 Some data sources and providers may not support this feature, and may return an error or an empty string.  
  
> [!NOTE]
>  **Remote Data Service Usage** This property is not available on a client-side **Connection** object.  
  
## Applies To  
 [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)  
  
## See Also  
 [Provider and DefaultDatabase Properties Example (VB)](../../../ado/reference/ado-api/provider-and-defaultdatabase-properties-example-vb.md)   
 [Provider and DefaultDatabase Properties Example (VC++)](../../../ado/reference/ado-api/provider-and-defaultdatabase-properties-example-vc.md)   
