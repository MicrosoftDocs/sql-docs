---
title: "ConvertToString Method (RDS) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "ConvertToString method [ADO]"
ms.assetid: b3f36bc8-6f69-49b0-83cd-2ccd3afebfbe
author: MightyPen
ms.author: genemi
manager: craigg
---
# ConvertToString Method (RDS)
Converts a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) to a MIME string that represents the recordset data.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
## Syntax  
  
```  
  
DataFactory.ConvertToString(Recordset)  
```  
  
#### Parameters  
 *DataFactory*  
 An object variable that represents an [RDSServer.DataFactory](../../../ado/reference/rds-api/datafactory-object-rdsserver.md) object.  
  
 *Recordset*  
 An object variable that represents a **Recordset** object.  
  
## Remarks  
 With .asp files, use **ConvertToString** to embed the **Recordset** in an HTML page generated on the server to transport it to a client computer.  
  
 **ConvertToString** first loads the **Recordset** into the Cursor Service tables, and then generates a stream in MIME format.  
  
 On the client, Remote Data Service can convert the MIME string back into a fully functioning **Recordset**. It works well for handling fewer than 400 rows of data with no more than 1024 bytes width per row. You shouldn't use it for streaming BLOB data and large result sets over HTTP. No wire compression is performed on the string, so very large data sets will take considerable time to transport over HTTP when compared to the wire-optimized tablegram format defined and deployed by Remote Data Service as its native transport protocol format.  
  
> [!NOTE]
>  If you are using Active Server Pages to embed the resulting MIME string in a client HTML page, be aware that versions of VBScript earlier than version 2.0 limit the string's size to 32K. If this limit is exceeded, an error is returned. Keep the query scope relatively small when using MIME embedding via .asp files. To fix this, download the latest version of VBScript from the Microsoft Windows Script Technologies Web site.  
  
## Applies To  
 [DataFactory Object (RDSServer)](../../../ado/reference/rds-api/datafactory-object-rdsserver.md)  
  
## See Also  
 [ConvertToString Method Example (VB)](../../../ado/reference/ado-api/converttostring-method-example-vb.md)   
 [ConvertToString Method Example (VBScript)](../../../ado/reference/rds-api/converttostring-method-example-vbscript.md)


