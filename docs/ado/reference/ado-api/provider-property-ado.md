---
title: "Provider Property (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Connection15::get_Provider"
  - "Connection15::PutProvider"
  - "Connection15::put_Provider"
  - "Connection15::GetProvider"
  - "Connection15::Provider"
helpviewer_keywords: 
  - "Provider property [ADO]"
ms.assetid: 0ff70e72-0061-4ffc-90fb-e3ea23129bb2
author: MightyPen
ms.author: genemi
manager: craigg
---
# Provider Property (ADO)
Indicates the name of the provider for a [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object.  
  
## Settings and Return Values  
 Sets or returns a **String** value that indicates the provider name.  
  
## Remarks  
 Use the **Provider** property to set or return the name of the provider for a connection. This property can also be set by the contents of the [ConnectionString](../../../ado/reference/ado-api/connectionstring-property-ado.md) property or the *ConnectionString* argument of the [Open](../../../ado/reference/ado-api/open-method-ado-connection.md) method; however, specifying a provider in more than one place while calling the **Open** method can have unpredictable results. If no provider is specified, the property will default to MSDASQL ([Microsoft OLE DB Provider for ODBC](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-odbc.md)).  
  
 The **Provider** property is read/write when the connection is closed and read-only when it is open. The setting does not take effect until you either open the **Connection** object or access the [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection of the **Connection** object. If the setting is not valid, an error occurs.  
  
## Applies To  
 [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)  
  
## See Also  
 [Provider and DefaultDatabase Properties Example (VB)](../../../ado/reference/ado-api/provider-and-defaultdatabase-properties-example-vb.md)   
 [Provider and DefaultDatabase Properties Example (VB)](../../../ado/reference/ado-api/provider-and-defaultdatabase-properties-example-vb.md)   
 [Microsoft OLE DB Provider for ODBC](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-odbc.md)   
 [Appendix A: Providers](../../../ado/guide/appendixes/appendix-a-providers.md)
