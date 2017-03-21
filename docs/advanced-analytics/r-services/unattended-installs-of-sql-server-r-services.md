---
title: "Unattended Installs of SQL Server R Services | Microsoft Docs"
ms.custom: ""
ms.date: "2017-02-10"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 77e92b2d-5777-4c31-bf02-f931ed54a247
caps.latest.revision: 18
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Unattended Installs of SQL Server R Services
    
This topic describes how to use SQL Server command-line argument to install SQL Server with R Services enabled in quiet mode: that is, an unattended installation that does not use the interactive features of the setup wizard. 

## Prerequisites

Before beginning the installation process, note these requirements:

+ You must install the database engine on each instance where you will use R Services (In-Database) in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
+ If you are performing an offline install, you must download the required R components in advance, and copy them to a local folder. For download locations, see [Installing R Components without Internet Access](../../advanced-analytics/r-services/installing-r-components-without-internet-access.md).   
+ There is a new parameter, */IACCEPTROPENLICENSETERMS*, that indicates you have accepted the license terms for using the open source R components. If you do not include this parameter in your command line, setup will fail. 
+ To complete setup without having to respond to prompts, make sure that you have identified all required arguments, including those for R and SQL Server licensing, and for any other features that you might want to install. 
  
## Perform an Unattended Install of R Services (In-Database)  
 The following example shows the **minimum** required features to specify in the command line when performing a silent, unattended install of R Services in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
###  <a name="bkmk_Unattended"></a>  
  
1. Run the following command from an elevated command prompt:  

    ```  
    Setup.exe /q /ACTION=Install /FEATURES=SQL,AdvancedAnalytics /INSTANCENAME=MSSQLSERVER /SECURITYMODE=SQL /SAPWD="%password%" /SQLSYSADMINACCOUNTS="<username>" /IACCEPTSQLSERVERLICENSETERMS /IACCEPTROPENLICENSETERMS  
    ```  
    > [!NOTE] 
    > The SQL security mode was required only in early releases. However, Mixed Mode authentication has often been used in data science scenarios where developers are connecting to the SQL Server using SQL logins.

## Post-Installation Steps  

2.  After installation is complete, in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], run the following command to enable the feature.  
  
    ```  
    EXEC sp_configure  'external scripts enabled', 1  
    RECONFIGURE WITH OVERRIDE   
    ```  
  
    > [!NOTE]  
    >  You must explicitly enable the feature and reconfigure; otherwise, it will not be possible to invoke R scripts even if the feature has been installed.  
  
3.  Restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. This will automatically restart the related [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service as well.  

3. Additional steps might be required if you have a custom security configuration, or if you will be using the SQL Server to support remote compute contexts. For more information, see [Upgrade and Installation FAQ](../../advanced-analytics/r-services/upgrade-and-installation-faq-sql-server-r-services.md). 
  
## See Also  
 [Troubleshooting R Services Setup](http://msdn.microsoft.com/library/ce6b902b-a4fa-4b0a-ac0d-be47a59c2a78)  
  
  