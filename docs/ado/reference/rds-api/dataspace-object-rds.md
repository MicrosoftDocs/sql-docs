---
title: "DataSpace Object (RDS)"
description: "DataSpace Object (RDS)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "DataSpace object [RDS]"
apitype: "COM"
---
# DataSpace Object (RDS)
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
 Creates client-side proxies to custom business objects located on the middle tier.  
  
 Remote Data Service needs business object proxies so that client-side components can communicate with business objects located on the middle tier. Proxies facilitate the packaging, unpackaging, and transport (marshaling) of the application's [Recordset](../ado-api/recordset-object-ado.md) data across process or machine boundaries.  
  
 Remote Data Service uses the **RDS.DataSpace** object's [CreateObject](./createobject-method-rds.md) method to create business object proxies. The business object proxy is dynamically created whenever an instance of its middle-tier business object counterpart is created. Remote Data Service supports the following protocols: HTTP, HTTPS (HTTP Secure Sockets), DCOM, and in-process (client components and the business object reside on the same computer).  
  
> [!NOTE]
>  RDS behaves in a "stateless" manner when the **RDS.DataSpace** object uses the HTTP or HTTPS protocols. That is, any internal information about a client request is discarded after the server returns a response.  
  
> [!NOTE]
>  Although the business object appears to exist for the lifetime of the business object proxy, the business object actually exists only until a response is sent to a request. When a request is issued (that is, a method is invoked on the business object), the proxy opens a new connection to the server and the server creates a new instance of the business object. After the business object responds to the request, the server destroys the business object and closes the connection.  
  
> [!NOTE]
>  This behavior means you cannot pass data from one request to another using a business object property or variable. You must employ some other mechanism, such as a file or a method argument, to persist state data.  
  
 The class ID for the **RDS.DataSpace** object is BD96C556-65A3-11D0-983A-00C04FC29E36.  
  
 The **DataSpace** object is safe for scripting.  
  
 This section contains the following topic.  
  
-   [DataSpace Object (RDS) Properties, Methods, and Events](./dataspace-object-rds-properties-methods-and-events.md)  
  
## See Also  
 [DataSpace Object and CreateObject Method Example (VBScript)](./dataspace-object-and-createobject-method-example-vbscript.md)