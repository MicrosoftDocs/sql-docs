---
title: "Customization File Logs Section | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: 11/09/2018
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "logs section in RDS [ADO]"
  - "customization file in RDS [ADO]"
ms.assetid: a368e264-865c-41ee-be00-d9097255c2ea
author: MightyPen
ms.author: genemi
manager: craigg
---
# Customization File Logs Section
The **logs** section contains a log file entry, which specifies the name of a file that records errors during the operation of the **DataFactory**.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
## Syntax  
 A log file entry is of the form:  
  
```console
  
err=  
FileName  
  
```  
  
## Remarks  
  
|Part|Description|  
|----------|-----------------|  
|**err**|A literal string that indicates this is a log file entry.|  
|*FileName*|A complete path and file name. The typical file name is **c:\msdfmap.log**.|  
  
 The log file will contain the user name, HRESULT, date, and time of each error.  
  
## See Also  
 [Customization File Connect Section](../../../ado/guide/remote-data-service/customization-file-connect-section.md)   
 [Customization File SQL Section](../../../ado/guide/remote-data-service/customization-file-sql-section.md)   
 [Customization File UserList Section](../../../ado/guide/remote-data-service/customization-file-userlist-section.md)   
 [DataFactory Customization](../../../ado/guide/remote-data-service/datafactory-customization.md)   
 [Required Client Settings](../../../ado/guide/remote-data-service/required-client-settings.md)   
 [Understanding the Customization File](../../../ado/guide/remote-data-service/understanding-the-customization-file.md)   
 [Writing Your Own Customized Handler](../../../ado/guide/remote-data-service/writing-your-own-customized-handler.md)


