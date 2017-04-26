---
title: "SQL Server Management Studio -  Release Notes | Microsoft Docs"
ms.custom: ""
ms.date: "01/30/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssms"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 0b95337b-80bf-4624-8f5d-cdaf6181d3b8
caps.latest.revision: 51
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# SQL Server Management Studio -  Release Notes
Welcome to our generally-available release of SQL Server Management Studio!  This release of SQL Server Management Studio (SSMS) is a stand-alone install outside of the SQL Server release. Our goal is to update this frequently with new functionality, fixes, and support for the newest features in SQL Server and Azure SQL Database.  
  
To install the latest SQL Server Management Studio, see [Download SQL Server Management Studio &#40;SSMS&#41;](../ssms/download-sql-server-management-studio-ssms.md).  
  
The following are issues and limitations with this release of SQL Server Management Studio:  

1. **Restore Database Wizard generates an Incorrect Path Pattern for destination database file location**
    This is a known issue when SSMS is connected to a Linux server. Even though the path looks incorrect/odd, it is handled correctly on the server side, i.e. there is no functional issue.

2. **File Browser Issues**
    - When working with a Windows-based SQL Server 2017 CTP 2.0 instance, the file browser UI in SSMS may fail to open if the server has an empty floppy drive or a fixed disk protected by Bitlocker installed. 
    - The file browser UI no longer supports versions of SQL Server 2017 prior to CTP 2.0.
    


3. **Only a single Azure Active Directory account can log in for an SSMS instance using Active Directory Universal Authentication.**  
    This restriction is limited to Active Directory Universal Authentication - you can log in to different servers using Active Directory Password Authentication, Active Directory Integrated Authentication or SQL Server Authentication.
    
    As a workaround, you can use another instance of SSMS to login as another Azure Active Directory account. 
    
4. **Data-Tier Application Framework (DacFx) commands and the Schema Designer in SSMS do not support Active Directory Universal Authentication.**  
    Commands that use DacFx (e.g. import and export), the schema designer in SSMS do not currently support Active Directory Universal Authentication.
    
    As a workaround, you can use the other forms of authentication provided in SSMS - Active Directory Password Authentication, Active Directory Integrated Authentication or SQL Server Authentication.

5. **SSMS can only connect to SQL Server 2016 Integrated Services (SSIS 2016) instances.**  
    There is a known compatibility limitation with SQL Server Integration Services that prevents connecting to previous versions.
    
    As a workaround for this problem, you can connect to your SQL Server Integration Service instance by using the [SSMS release aligned with your SSIS instance.](../ssms/previous-sql-server-management-studio-releases.md) 
  
5. **SSMS won't save maintenance plans for SQL Server 2008 R2 and earlier SQL Server versions.**  
    This is a known limitation that we hope to address in the future. In the meantime, you can use the [SSMS 2014 release](../ssms/previous-sql-server-management-studio-releases.md) to save the maintenance plans.  
    
5. **Non-English SSMS installations may require the installation of an additional security package.**  
Non-English localized releases of SSMS [require the KB 2862966 security update package](https://support.microsoft.com/en-us/kb/2862966) if installing on: Windows 8, Windows 7, Windows Server 2012, and Windows Server 2008 R2.
  
## Feedback  
  
![needhelp_person_icon](../ssms/media/needhelp_person_icon.png) [SQL Client Tools Forum](https://social.msdn.microsoft.com/Forums/en-US/home?forum=sqltools) |  [Log an issue or suggestion at Microsoft Connect](https://connect.microsoft.com/SQLServer/Feedback).  
  
## See Also  
[SQL Server Management Studio Tutorial](../ssms/use-sql-server-management-studio.md)  
[Download SQL Server Management Studio &#40;SSMS&#41;](../ssms/download-sql-server-management-studio-ssms.md)  
[Previous SQL Server Management Studio Releases](../ssms/previous-sql-server-management-studio-releases.md)  

  
