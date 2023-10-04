---
title: "Open Method (ADO Connection)"
description: "Open Method (ADO Connection)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Connection15::raw_Open"
  - "Connection15::Open"
  - "_Connection::Open"
helpviewer_keywords:
  - "Open method [ADO]"
apitype: "COM"
---
# Open Method (ADO Connection)
Opens a connection to a data source.  
  
## Syntax  
  
```  
  
connection.Open ConnectionString, UserID, Password, Options  
```  
  
#### Parameters  
 *ConnectionString*  
 Optional. A **String** value that contains connection information. See the [ConnectionString](./connectionstring-property-ado.md) property for details on valid settings.  
  
 *UserID*  
 Optional. A **String** value that contains a user name to use when establishing the connection.  
  
 *Password*  
 Optional. A **String** value that contains a password to use when establishing the connection.  
  
 *Options*  
 Optional. A [ConnectOptionEnum](./connectoptionenum.md) value that determines whether this method should return after (synchronously) or before (asynchronously) the connection is established.  
  
## Remarks  
 Using the **Open** method on a [Connection](./connection-object-ado.md) object establishes the physical connection to a data source. After this method successfully completes, the connection is live and you can issue commands against it and process the results.  
  
 Use the optional *ConnectionString* argument to specify either a connection string containing a series of *argument* *= value* statements separated by semicolons, or a file or directory resource identified with a URL. The **ConnectionString** property automatically inherits the value used for the *ConnectionString* argument. Therefore, you can either set the **ConnectionString** property of the **Connection** object before opening it, or use the *ConnectionString* argument to set or override the current connection parameters during the **Open** method call.  
  
 If you pass user and password information both in the *ConnectionString* argument and in the optional *UserID* and *Password* arguments, the *UserID* and *Password* arguments will override the values specified in *ConnectionString*.  
  
 When you have concluded your operations over an open **Connection**, use the [Close](./close-method-ado.md) method to free any associated system resources. Closing an object does not remove it from memory; you can change its property settings and use the **Open** method to open it again later. To completely eliminate an object from memory, set the object variable to *Nothing*.  
  
> [!NOTE]
>  **Remote Data Service Usage** When used on a client-side **Connection** object, the **Open** method doesn't actually establish a connection to the server until a [Recordset](./recordset-object-ado.md) is opened on the **Connection** object.  
  
> [!NOTE]
>  URLs using the http scheme will automatically invoke the [Microsoft OLE DB Provider for Internet Publishing](../../guide/appendixes/microsoft-ole-db-provider-for-internet-publishing.md). For more information, see [Absolute and Relative URLs](../../guide/data/absolute-and-relative-urls.md).  
  
## Applies To  
 [Connection Object (ADO)](./connection-object-ado.md)  
  
## See Also  
 [Open and Close Methods Example (VB)](./open-and-close-methods-example-vb.md)   
 [Open and Close Methods Example (VBScript)](./open-and-close-methods-example-vbscript.md)   
 [Open and Close Methods Example (VC++)](./open-and-close-methods-example-vc.md)   
 [Open Method (ADO Record)](./open-method-ado-record.md)   
 [Open Method (ADO Recordset)](./open-method-ado-recordset.md)   
 [Open Method (ADO Stream)](./open-method-ado-stream.md)   
 [OpenSchema Method](./openschema-method.md)