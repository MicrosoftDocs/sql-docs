---
title: "Handler Property (RDS)"
description: "Handler Property (RDS)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Handler property [ADO]"
apitype: "COM"
---
# Handler Property (RDS)
Indicates the name of a server-side customization program (handler) that extends the functionality of the [RDSServer.DataFactory](./datafactory-object-rdsserver.md), and any parameters used by the *handler*.  
  
 **Applies To:** [DataControl Object (RDS)](./datacontrol-object-rds.md)  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
## Syntax  
  
```  
  
DataControl.Handler = String  
```  
  
#### Parameters  
 *DataControl*  
 An object variable that represents an [RDS.DataControl](./datacontrol-object-rds.md) object.  
  
 *String*  
 A **String** value that contains the name of the handler and any parameters, all separated by commas (for example, `"handlerName,parm1,parm2,...,parm`*N*`"`).  
  
## Remarks  
 This property supports [customization](../../guide/remote-data-service/datafactory-customization.md), a functionality that requires setting the [CursorLocation](../ado-api/cursorlocation-property-ado.md) property to **adUseClient**.  
  
 The name of the handler and its parameters, if any, are separated by commas (","). Unpredictable behavior will result if a semicolon (";") appears anywhere within *String*. You can write your own handler, provided it supports the **IDataFactoryHandler** interface.  
  
 The name of the default handler is **MSDFMAP.Handler**, and its default parameter is a customization file named **MSDFMAP.INI**. Use this property to invoke alternate customization files created by your server administrator.  
  
 The alternative to setting the **Handler** property is to specify a handler and parameters in the [ConnectionString](../ado-api/connectionstring-property-ado.md) property; that is, "**Handler=**_handlerName,parameter1,parameter2,...;_".  
  
## Applies To  
 [DataControl Object (RDS)](./datacontrol-object-rds.md)  
  
## See Also  
 [Handler Property Example (VB)](./handler-property-example-vb.md)   
 [DataFactory Customization](../../guide/remote-data-service/datafactory-customization.md)   
 [DataFactory Object (RDSServer)](./datafactory-object-rdsserver.md)