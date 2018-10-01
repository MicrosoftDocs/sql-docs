---
title: "Open Method (ADO Connection) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Connection15::raw_Open"
  - "Connection15::Open"
  - "_Connection::Open"
helpviewer_keywords: 
  - "Open method [ADO]"
ms.assetid: 663defab-5545-4973-9036-24d5882c9737
author: MightyPen
ms.author: genemi
manager: craigg
---
# Open Method (ADO Connection)
Opens a connection to a data source.  
  
## Syntax  
  
```  
  
connection.Open ConnectionString, UserID, Password, Options  
```  
  
#### Parameters  
 *ConnectionString*  
 Optional. A **String** value that contains connection information. See the [ConnectionString](../../../ado/reference/ado-api/connectionstring-property-ado.md) property for details on valid settings.  
  
 *UserID*  
 Optional. A **String** value that contains a user name to use when establishing the connection.  
  
 *Password*  
 Optional. A **String** value that contains a password to use when establishing the connection.  
  
 *Options*  
 Optional. A [ConnectOptionEnum](../../../ado/reference/ado-api/connectoptionenum.md) value that determines whether this method should return after (synchronously) or before (asynchronously) the connection is established.  
  
## Remarks  
 Using the **Open** method on a [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object establishes the physical connection to a data source. After this method successfully completes, the connection is live and you can issue commands against it and process the results.  
  
 Use the optional *ConnectionString* argument to specify either a connection string containing a series of *argument* *= value* statements separated by semicolons, or a file or directory resource identified with a URL. The **ConnectionString** property automatically inherits the value used for the *ConnectionString* argument. Therefore, you can either set the **ConnectionString** property of the **Connection** object before opening it, or use the *ConnectionString* argument to set or override the current connection parameters during the **Open** method call.  
  
 If you pass user and password information both in the *ConnectionString* argument and in the optional *UserID* and *Password* arguments, the *UserID* and *Password* arguments will override the values specified in *ConnectionString*.  
  
 When you have concluded your operations over an open **Connection**, use the [Close](../../../ado/reference/ado-api/close-method-ado.md) method to free any associated system resources. Closing an object does not remove it from memory; you can change its property settings and use the **Open** method to open it again later. To completely eliminate an object from memory, set the object variable to *Nothing*.  
  
> [!NOTE]
>  **Remote Data Service Usage** When used on a client-side **Connection** object, the **Open** method doesn't actually establish a connection to the server until a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) is opened on the **Connection** object.  
  
> [!NOTE]
>  URLs using the http scheme will automatically invoke the [Microsoft OLE DB Provider for Internet Publishing](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-internet-publishing.md). For more information, see [Absolute and Relative URLs](../../../ado/guide/data/absolute-and-relative-urls.md).  
  
## Applies To  
 [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)  
  
## See Also  
 [Open and Close Methods Example (VB)](../../../ado/reference/ado-api/open-and-close-methods-example-vb.md)   
 [Open and Close Methods Example (VBScript)](../../../ado/reference/ado-api/open-and-close-methods-example-vbscript.md)   
 [Open and Close Methods Example (VC++)](../../../ado/reference/ado-api/open-and-close-methods-example-vc.md)   
 [Open Method (ADO Record)](../../../ado/reference/ado-api/open-method-ado-record.md)   
 [Open Method (ADO Recordset)](../../../ado/reference/ado-api/open-method-ado-recordset.md)   
 [Open Method (ADO Stream)](../../../ado/reference/ado-api/open-method-ado-stream.md)   
 [OpenSchema Method](../../../ado/reference/ado-api/openschema-method.md)
