---
title: "DataFactory Object (RDSServer) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "DataFactory object [ADO]"
ms.assetid: e75240c2-b749-471e-b6ea-98cae232efbe
author: MightyPen
ms.author: genemi
manager: craigg
---
# DataFactory Object (RDSServer)
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
 This default server-side business object implements methods that provide read/write data access to specified data sources for client-side applications.  
  
 The **RDSServer.DataFactory** object is designed as a server-side Automation object that receives client requests. In an Internet implementation, it resides on a Web server and is instantiated by the ADISAPI component. The **RDSServer.DataFactory** object provides read and write access to specified data sources, but does not contain any validation or business rules logic.  
  
 If you use a method that is available in both the **RDSServer.DataFactory** and [RDS.DataControl](../../../ado/reference/rds-api/datacontrol-object-rds.md) objects, Remote Data Service uses the **RDS.DataControl** version by default. The default assumes a basic programming scenario, where the **RDSServer.DataFactory** serves as a generic server-side business object.  
  
 If you want your Web application to handle task-specific server-side processing, you can replace the **RDSServer.DataFactory** with a custom business object.  
  
 You can create server-side business objects that call the **RDSServer.DataFactory** methods, such as [Query](../../../ado/reference/rds-api/query-method-rds.md) and [CreateRecordset](../../../ado/reference/rds-api/createrecordset-method-rds.md). This is helpful if you want to add functionality to your business objects, but take advantage of existing Remote Data Service technologies.  
  
 The **DataFactory** object is not safe for scripts that run on the client side.  
  
 The class ID for the **RDSServer.DataFactory** object is 9381D8F5-0288-11D0-9501-00AA00B911A5.  
  
 This section contains the following topic.  
  
-   [DataFactory Object (RDSServer) Properties, Methods, and Events](../../../ado/reference/rds-api/datafactory-object-rdsserver-properties-methods-and-events.md)  
  
## See Also  
 [DataFactory Object, Query Method, and CreateObject Method Example (VBScript)](../../../ado/reference/rds-api/datafactory-object-query-method-and-createobject-method-example-vbscript.md)


