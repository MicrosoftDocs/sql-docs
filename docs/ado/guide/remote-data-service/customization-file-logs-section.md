---
title: "Customization File Logs Section"
description: "Customization File Logs Section"
author: rothja
ms.author: jroth
ms.date: 11/09/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "logs section in RDS [ADO]"
  - "customization file in RDS [ADO]"
---
# Customization File Logs Section
The **logs** section contains a log file entry, which specifies the name of a file that records errors during the operation of the **DataFactory**.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
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
 [Customization File Connect Section](./customization-file-connect-section.md)   
 [Customization File SQL Section](./customization-file-sql-section.md)   
 [Customization File UserList Section](./customization-file-userlist-section.md)   
 [DataFactory Customization](./datafactory-customization.md)   
 [Required Client Settings](./required-client-settings.md)   
 [Understanding the Customization File](./understanding-the-customization-file.md)   
 [Writing Your Own Customized Handler](./writing-your-own-customized-handler.md)