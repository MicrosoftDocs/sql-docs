---
title: "Handler Property (RDS) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "Handler property [ADO]"
ms.assetid: fdc34362-6d47-4727-b171-8d033159408e
author: MightyPen
ms.author: genemi
manager: craigg
---
# Handler Property (RDS)
Indicates the name of a server-side customization program (handler) that extends the functionality of the [RDSServer.DataFactory](../../../ado/reference/rds-api/datafactory-object-rdsserver.md), and any parameters used by the *handler*.  
  
 **Applies To:** [DataControl Object (RDS)](../../../ado/reference/rds-api/datacontrol-object-rds.md)  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
## Syntax  
  
```  
  
DataControl.Handler = String  
```  
  
#### Parameters  
 *DataControl*  
 An object variable that represents an [RDS.DataControl](../../../ado/reference/rds-api/datacontrol-object-rds.md) object.  
  
 *String*  
 A **String** value that contains the name of the handler and any parameters, all separated by commas (for example, `"handlerName,parm1,parm2,...,parm`*N*`"`).  
  
## Remarks  
 This property supports [customization](../../../ado/guide/remote-data-service/datafactory-customization.md), a functionality that requires setting the [CursorLocation](../../../ado/reference/ado-api/cursorlocation-property-ado.md) property to **adUseClient**.  
  
 The name of the handler and its parameters, if any, are separated by commas (","). Unpredictable behavior will result if a semicolon (";") appears anywhere within *String*. You can write your own handler, provided it supports the **IDataFactoryHandler** interface.  
  
 The name of the default handler is **MSDFMAP.Handler**, and its default parameter is a customization file named **MSDFMAP.INI**. Use this property to invoke alternate customization files created by your server administrator.  
  
 The alternative to setting the **Handler** property is to specify a handler and parameters in the [ConnectionString](../../../ado/reference/ado-api/connectionstring-property-ado.md) property; that is, "**Handler=**_handlerName,parameter1,parameter2,...;_".  
  
## Applies To  
 [DataControl Object (RDS)](../../../ado/reference/rds-api/datacontrol-object-rds.md)  
  
## See Also  
 [Handler Property Example (VB)](../../../ado/reference/rds-api/handler-property-example-vb.md)   
 [DataFactory Customization](../../../ado/guide/remote-data-service/datafactory-customization.md)   
 [DataFactory Object (RDSServer)](../../../ado/reference/rds-api/datafactory-object-rdsserver.md)


