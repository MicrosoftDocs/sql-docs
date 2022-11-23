---
title: "System Requirements for the Address Book Application"
description: "System Requirements for the Address Book Application"
author: rothja
ms.author: jroth
ms.date: 11/09/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "address book application scenario [ADO]"
  - "RDS scenarios [ADO]"
---
# System Requirements for the Address Book Application
To set up the Address Book sample application, you need to meet the following software and database requirements:  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
## Software Requirements  
 The server computer software requirements for running this Web application include:  
  
-   Microsoft Windows NT® Server 4.0, with Service Pack 3 or later, or Microsoft Windows® 2000 Server.  
  
-   Microsoft Internet Information Services (IIS) version 3.0 or later, with Active Server Pages.  
  
 The client computer software requirements for running this Web application include:  
  
-   Microsoft Internet Explorer 4.0 or later.  
  
-   Microsoft Windows NT 4.0 Workstation or Server, Windows 2000, or Microsoft Windows 98.  
  
## Database Requirements  
 To use this sample, you must have:  
  
-   An operational Microsoft® SQL Server version 6.5 or later database server.  
  
-   Privileges to create the database and populate it with the sample data.  
  
-   Verification of the populated data through Enterprise Manager or the ISQL utilities (called Query Analyzer in SQL Server 7.0).  
  
 If you do not have privileges, your database administrator may need to set up the system and give you access permission to the database, or set up the database for you.  
  
## See Also  
 [Running the Address Book SQL Script](./running-the-address-book-sql-script.md)   
 [DataControl Object (RDS)](../../reference/rds-api/datacontrol-object-rds.md)   
 [Running the Address Book Sample Application](./running-the-address-book-sample-application.md)