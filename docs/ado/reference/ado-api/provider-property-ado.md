---
title: "Provider Property (ADO)"
description: "Provider Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Connection15::get_Provider"
  - "Connection15::PutProvider"
  - "Connection15::put_Provider"
  - "Connection15::GetProvider"
  - "Connection15::Provider"
helpviewer_keywords:
  - "Provider property [ADO]"
apitype: "COM"
---
# Provider Property (ADO)
Indicates the name of the provider for a [Connection](./connection-object-ado.md) object.  
  
## Settings and Return Values  
 Sets or returns a **String** value that indicates the provider name.  
  
## Remarks  
 Use the **Provider** property to set or return the name of the provider for a connection. This property can also be set by the contents of the [ConnectionString](./connectionstring-property-ado.md) property or the *ConnectionString* argument of the [Open](./open-method-ado-connection.md) method; however, specifying a provider in more than one place while calling the **Open** method can have unpredictable results. If no provider is specified, the property will default to MSDASQL ([Microsoft OLE DB Provider for ODBC](../../guide/appendixes/microsoft-ole-db-provider-for-odbc.md)).  
  
 The **Provider** property is read/write when the connection is closed and read-only when it is open. The setting does not take effect until you either open the **Connection** object or access the [Properties](./properties-collection-ado.md) collection of the **Connection** object. If the setting is not valid, an error occurs.  
  
## Applies To  
 [Connection Object (ADO)](./connection-object-ado.md)  
  
## See Also  
 [Provider and DefaultDatabase Properties Example (VB)](./provider-and-defaultdatabase-properties-example-vb.md)   
 [Provider and DefaultDatabase Properties Example (VB)](./provider-and-defaultdatabase-properties-example-vb.md)   
 [Microsoft OLE DB Provider for ODBC](../../guide/appendixes/microsoft-ole-db-provider-for-odbc.md)   
 [Appendix A: Providers](../../guide/appendixes/appendix-a-providers.md)