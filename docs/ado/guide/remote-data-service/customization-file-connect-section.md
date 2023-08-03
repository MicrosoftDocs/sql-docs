---
title: "Customization File Connect Section"
description: "Customization File Connect Section"
author: rothja
ms.author: jroth
ms.date: 11/09/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "connect section in RDS [ADO]"
  - "customization file in RDS [ADO]"
---
# Customization File Connect Section
The default behavior of the handler is to deny all connections. The **connect** section specifies exceptions to that behavior. For example, if all the **connect** sections were absent or empty, then by default no connections could be made.  
  
 The **connect** section can contain:  
  
-   A default access entry that specifies the default read and write operations allowed on this connection. If there is no default access entry in the section, the section will be ignored.  
  
-   A new connection string that replaces the client connection string.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
## Syntax  
 A default access entry is of the form:  
  
```console
  
Access=  
accessRight  
  
```  
  
 A replacement connection string entry is of the form:  
  
```console
  
Connect=  
connectionString  
  
```  
  
## Remarks  
  
|Part|Description|  
|----------|-----------------|  
|**Connect**|A literal string that indicates this is a connection string entry.|  
|**_connectionString_**|A string that replaces the whole client connection string.|  
|**Access**|A literal string that indicates this is an access entry.|  
|**_accessRight_**|One of the following access rights:<br /><br /> -   **NoAccess** - User cannot access the data source.<br />-   **ReadOnly** - User can read the data source.<br />-   **ReadWrite** - User can read or write to the data source.|  
  
 If you want to allow any connection (in effect, disabling the default handler behavior), set the access entry in the **connect default** section to `Access=ReadWrite`, and delete or comment out any other **connect** _identifier_ section.  
  
## See Also  
 [Customization File Logs Section](./customization-file-logs-section.md)   
 [Customization File SQL Section](./customization-file-sql-section.md)   
 [Customization File UserList Section](./customization-file-userlist-section.md)   
 [DataFactory Customization](./datafactory-customization.md)   
 [Required Client Settings](./required-client-settings.md)   
 [Understanding the Customization File](./understanding-the-customization-file.md)   
 [Writing Your Own Customized Handler](./writing-your-own-customized-handler.md)