---
title: "Configuring RDS on Windows 2000 | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: 11/09/2018
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "RDS configuration [ADO], Windows 2000"
ms.assetid: ef37e858-c05f-4f52-a65f-3ce6037e0d03
author: MightyPen
ms.author: genemi
manager: craigg
---
# Configuring RDS on Windows 2000
If you experience difficulties getting RDS to function correctly after you upgrade to Windows 2000, follow these steps to troubleshoot the issue:  
  
1.  Make sure that the World Wide Web Publishing Service is running first by navigating to https:// server by using Internet Explorer. If you cannot access the Web server in this manner, open a command prompt and enter the following command, "NET START W3SVC".  
  
2.  On the Start menu, select Run. Type msdfmap.ini and then click OK to open the msdfmap.ini file in Notepad. Check the [CONNECT DEFAULT] section, and if the ACCESS parameter is set to NOACCESS, change it to READONLY.  
  
3.  Using the RegEdit utility, navigate to "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\DataFactory\HandlerInfo" and make sure **HandlerRequired** is set to 0 and **DefaultHandler** is "" (Null string).  
  
     **Note** If you make any changes to this section of the registry, you must stop and restart the World Wide Web Publishing Service by entering the following commands at a command prompt: "NET STOP W3SVC" and "NET START W3SVC".  
  
4.  Using the RegEdit utility, navigate in the registry to "HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\W3SVC\Parameters\ADCLaunch" and verify that there is a key named **RDSServer.Datafactory**. If not, create it.  
  
5.  Using Internet Services Manager, locate the Default Web site and view the properties of the MSADC virtual root. Inspect the Directory Security/IP Address and Domain Name Restrictions. If the "Access is Denied" is checked then select "Granted".  
  
 Be sure to try rebooting the server if the changes to do not appear to solve the problem at first.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system. Migrate applications that use RDS to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
## See Also  
 [RDS Fundamentals](../../../ado/guide/remote-data-service/rds-fundamentals.md)


