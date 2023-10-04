---
title: "URL Property (RDS)"
description: "URL Property (RDS)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "URL property [ADO]"
apitype: "COM"
---
# URL Property (RDS)
Indicates a string that contains a relative or absolute URL.  
  
 You can set the **URL** property at design time in the [DataControl](./datacontrol-object-rds.md) object's OBJECT tag, or at run time in scripting code.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
## Syntax  
  
```  
  
Design time: <PARAM NAME="URL" VALUE="Server">  
Run time: DataControl.URL="Server"  
```  
  
#### Parameters  
 *Server*  
 A **String** value that contains a valid URL.  
  
 *DataControl*  
 An object variable that represents a **DataControl** object.  
  
## Remarks  
 Typically, the URL identifies an Active Server Page (.asp) file that can produce and return a [Recordset](../ado-api/recordset-object-ado.md). Therefore, the user can obtain a **Recordset** without having to invoke the server-side [DataFactory](./datafactory-object-rdsserver.md) object, or program a custom business object.  
  
 If the **URL** property has been set, [SubmitChanges](./submitchanges-method-rds.md) will submit changes to the location specified by the URL.  
  
## Applies To  
 [DataControl Object (RDS)](./datacontrol-object-rds.md)  
  
## See Also  
 [URL Property Example (VBScript)](./url-property-example-vbscript.md)