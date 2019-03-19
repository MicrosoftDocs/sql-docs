---
title: "Customization File UserList Section | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: 11/09/2018
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "UserList section in rds [ADO]"
  - "customization file in RDS [ADO]"
ms.assetid: 42e8ec20-eaac-4a95-8cb8-4bba93a75bcb
author: MightyPen
ms.author: genemi
manager: craigg
---
# Customization File UserList Section
The **userlist** section pertains to the **connect** section with the same section *identifier* parameter.  
  
 This section can contain a *user access entry*, which specifies access rights for the specified user and overrides the *default* *access entry* in the matching **connect** section.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
## Syntax  
 A user access entry is of the form:  
  
 _userName_ **=**   
 **_accessRights_**  
  
|Part|Description|  
|----------|-----------------|  
|*userName*|The *user name* of the person employing this connection. Valid user names are established with the IIS **Service Manager** dialog.|  
|**_accessRights_**|One of the following access rights:<br /><br /> -   **NoAccess** - User cannot access the data source.<br />-   **ReadOnly** - User can read the data source.<br />-   **ReadWrite** - User can read or write to the data source.|  
  
## See Also  
 [Customization File Connect Section](../../../ado/guide/remote-data-service/customization-file-connect-section.md)   
 [Customization File Logs Section](../../../ado/guide/remote-data-service/customization-file-logs-section.md)   
 [Customization File SQL Section](../../../ado/guide/remote-data-service/customization-file-sql-section.md)   
 [DataFactory Customization](../../../ado/guide/remote-data-service/datafactory-customization.md)   
 [Required Client Settings](../../../ado/guide/remote-data-service/required-client-settings.md)   
 [Understanding the Customization File](../../../ado/guide/remote-data-service/understanding-the-customization-file.md)   
 [Writing Your Own Customized Handler](../../../ado/guide/remote-data-service/writing-your-own-customized-handler.md)


