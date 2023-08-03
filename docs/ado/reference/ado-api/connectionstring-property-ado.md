---
title: "ConnectionString Property (ADO)"
description: "ConnectionString Property (ADO)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
f1_keywords:
  - "Connection15::ConnectionString"
helpviewer_keywords:
  - "ConnectionString property [ADO]"
apitype: "COM"
---
# ConnectionString Property (ADO)
Indicates the information used to establish a connection to a data source.  
  
## Settings and Return Values  
 Sets or returns a **String** value.  
  
## Remarks  
 Use the **ConnectionString** property to specify a data source by passing a detailed connection string containing a series of *argument* *= value* statements separated by semicolons.  
  
 ADO supports five arguments for the **ConnectionString** property; any other arguments pass directly to the provider without any processing by ADO. The arguments ADO supports are as follows.  
  
|Argument|Description|  
|--------------|-----------------|  
|*Provider=*|Specifies the name of a provider to use for the connection.|  
|*File Name=*|Specifies the name of a provider-specific file (for example, a persisted data source object) containing preset connection information.|  
|*Remote Provider=*|Specifies the name of a provider to use when opening a client-side connection. (Remote Data Service only.)|  
|*Remote Server=*|Specifies the path name of the server to use when opening a client-side connection. (Remote Data Service only.)|  
|*URL=*|Specifies the connection string as an absolute URL identifying a resource, such as a file or directory.|  
  
 After you set the **ConnectionString** property and open the [Connection](./connection-object-ado.md) object, the provider may alter the contents of the property, for example, by mapping the ADO-defined argument names to their equivalents for the specific provider.  
  
 The **ConnectionString** property automatically inherits the value used for the *ConnectionString* argument of the [Open](./open-method-ado-connection.md) method, so you can override the current **ConnectionString** property during the **Open** method call.  
  
 Because the *File Name* argument causes ADO to load the associated provider, you cannot pass both the *Provider* and *File Name* arguments.  
  
 The **ConnectionString** property is read/write when the connection is closed and read-only when it is open.  
  
 Duplicates of an argument in the **ConnectionString** property are ignored. The last instance of any argument is used.  
  
> [!NOTE]
>  **Remote Data Service Usage** When used on a client-side **Connection** object, the **ConnectionString** property can include only the *Remote Provider* and *Remote Server* parameters.  
  
 The following table lists the default ADO provider for each Windows operating system:  
  
|Default ADO provider|Windows operating system|  
|--------------------------|------------------------------|  
|MSDASQL<br /><br /> (To improve the readability of source code, explicitly specify the provider name in the connection string.)|Windows 2000 (32-bit)<br /><br /> Windows XP (32-bit)<br /><br /> Windows 2003 Server (32-bit)<br /><br /> Windows Vista (32-bit)<br /><br /> Windows Vista Service Pack 1 or later (32-bit and 64-bit)<br /><br /> Windows versions after Windows Vista (32-bit and 64-bit)|  
|No default.<br /><br /> When an ADO application runs on the following operating systems and does not specify the provider explicitly, ADO returns the following error: "ADODB.Connection: provider is not specified and there is no designated default provider"|Windows 2000 (64-bit)<br /><br /> Windows XP (64-bit)<br /><br /> Windows 2003 Server (64-bit)<br /><br /> Windows Vista (64-bit)|  
  
## Applies To  
 [Connection Object (ADO)](./connection-object-ado.md)  
  
## See Also  
 [ConnectionString, ConnectionTimeout, and State Properties Example (VB)](./connectionstring-connectiontimeout-and-state-properties-example-vb.md)   
 [ConnectionString, ConnectionTimeout, and State Properties Example (VC++)](./connectionstring-connectiontimeout-and-state-properties-example-vc.md)   
 [Appendix A: Providers](../../guide/appendixes/appendix-a-providers.md)