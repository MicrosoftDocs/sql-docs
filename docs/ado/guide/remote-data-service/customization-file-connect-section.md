---
title: "Customization File Connect Section | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "connect section in RDS [ADO]"
  - "customization file in RDS [ADO]"
ms.assetid: d50eb3cc-a822-486f-b80b-65bb50547ecd
caps.latest.revision: 15
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Customization File Connect Section
The default behavior of the handler is to deny all connections. The **connect** section specifies exceptions to that behavior. For example, if all the **connect** sections were absent or empty, then by default no connections could be made.  
  
 The **connect** section can contain:  
  
-   A default access entry that specifies the default read and write operations allowed on this connection. If there is no default access entry in the section, the section will be ignored.  
  
-   A new connection string that replaces the client connection string.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/en-us/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](http://go.microsoft.com/fwlink/?LinkId=199565).  
  
## Syntax  
 A default access entry is of the form:  
  
```  
  
Access=  
accessRight  
  
```  
  
 A replacement connection string entry is of the form:  
  
```  
  
Connect=  
connectionString  
  
```  
  
## Remarks  
  
|Part|Description|  
|----------|-----------------|  
|**Connect**|A literal string that indicates this is a connection string entry.|  
|***connectionString***|A string that replaces the whole client connection string.|  
|**Access**|A literal string that indicates this is an access entry.|  
|***accessRight***|One of the following access rights:<br /><br /> -   **NoAccess** — User cannot access the data source.<br />-   **ReadOnly** — User can read the data source.<br />-   **ReadWrite** — User can read or write to the data source.|  
  
 If you want to allow any connection (in effect, disabling the default handler behavior), set the access entry in the **connect default** section to `Access=ReadWrite`, and delete or comment out any other **connect** *identifier* section.  
  
## See Also  
 [Customization File Logs Section](../../../ado/guide/remote-data-service/customization-file-logs-section.md)   
 [Customization File SQL Section](../../../ado/guide/remote-data-service/customization-file-sql-section.md)   
 [Customization File UserList Section](../../../ado/guide/remote-data-service/customization-file-userlist-section.md)   
 [DataFactory Customization](../../../ado/guide/remote-data-service/datafactory-customization.md)   
 [Required Client Settings](../../../ado/guide/remote-data-service/required-client-settings.md)   
 [Understanding the Customization File](../../../ado/guide/remote-data-service/understanding-the-customization-file.md)   
 [Writing Your Own Customized Handler](../../../ado/guide/remote-data-service/writing-your-own-customized-handler.md)



