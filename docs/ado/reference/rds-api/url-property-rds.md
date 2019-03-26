---
title: "URL Property (RDS) | Microsoft Docs"
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: 
ms.prod: sql  
ms.prod_service: connectivity
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "URL property [ADO]"
ms.assetid: 8c56b233-1be8-442c-8d0e-a4c96465bc99
author: MightyPen
ms.author: genemi
manager: craigg
---
# URL Property (RDS)
Indicates a string that contains a relative or absolute URL.  
  
 You can set the **URL** property at design time in the [DataControl](../../../ado/reference/rds-api/datacontrol-object-rds.md) object's OBJECT tag, or at run time in scripting code.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
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
 Typically, the URL identifies an Active Server Page (.asp) file that can produce and return a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md). Therefore, the user can obtain a **Recordset** without having to invoke the server-side [DataFactory](../../../ado/reference/rds-api/datafactory-object-rdsserver.md) object, or program a custom business object.  
  
 If the **URL** property has been set, [SubmitChanges](../../../ado/reference/rds-api/submitchanges-method-rds.md) will submit changes to the location specified by the URL.  
  
## Applies To  
 [DataControl Object (RDS)](../../../ado/reference/rds-api/datacontrol-object-rds.md)  
  
## See Also  
 [URL Property Example (VBScript)](../../../ado/reference/rds-api/url-property-example-vbscript.md)


