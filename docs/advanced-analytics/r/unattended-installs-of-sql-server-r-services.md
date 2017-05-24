---
title: "Unattended Installs of SQL Server R Services | Microsoft Docs"
ms.custom: ""
ms.date: "04/02/2017"
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
# Unattended Installs of R Machine Learning Services (In-Database)
    
This topic describes how to use command-line arguments with SQL Server setup to install R together with an instance of the database engine, in quiet mode. In an unattended installation, you do not use the interactive features of the setup wizard, and must provide all arguments required to complete installation, including licensing agreements for SQL Server and for machine learning components. 

> [!IMPORTANT]
The setup process is different in SQL Server 2016 and SQL Server 2017.
+ SQL Server 2016 supports R integration using the feature, **SQL Server R Services**. See [this section](#bkmk_OldInstall) for setup arguments.
+ SQL Server 2017 includes support for both R and Python, using the feature **Machine Learning Services (In-Database). See [this section](#bkmk_OldInstall) for R Service setup arguments.
+ The prerequisites are the same. Be sure to install the R components beforehand if the computer does not have Internet access.
+ In both SQL Server 2016 and SQL Server 2017, additional steps are required after setup to enable the feature.

## Prerequisites

+ You must install the database engine on each instance where you will use R.  
+ If you are performing an offline install, you must download the required R components in advance, and copy them to a local folder. For download locations, see [Installing R Components without Internet Access](../../advanced-analytics/r-services/installing-ml-components-without-internet-access.md).   
+ There is a new parameter, */IACCEPTROPENLICENSETERMS*, that indicates you have accepted the license terms for using the open source R components. If you do not include this parameter in your command line, setup will fail. 
+ To complete setup without having to respond to prompts, make sure that you have identified all required arguments, including those for R and SQL Server licensing, and for any other features that you might want to install. 

> [!NOTE] 
> The Mixed security mode that supports SQL logins was required in early releases. However, you might consider enabling Mixed Mode authentication to support solution development by data scientists using a SQL login.

## <a name="bkmk_NewInstall"></a> Unattended Install of R Machine Learning Services in SQL Server 2017

The following example shows the **minimum** required features to specify in the command line when performing a silent, unattended install of Machine Services (In-Database). Requires SQL Server 2017.  


1. Open an elevated command prompt, and run the following command:  

    ```  
    Setup.exe /q /ACTION=Install /FEATURES=SQL,AdvancedAnalytics, SQL_INST_MR /INSTANCENAME=MSSQLSERVER /SECURITYMODE=SQL /SAPWD="%password%" /SQLSYSADMINACCOUNTS="<username>" /IACCEPTSQLSERVERLICENSETERMS /IACCEPTROPENLICENSETERMS  
    ```  
    Note the flags required for R in SQL Server 2017: `AdvancedAnalytics`, `SQL_INST_MR`, and `IACCEPTRLICENSETERMS`. 
2. Restart the server.
3. Perform the post-installation configuration steps as described in [this section](#bkmk_PostInstall). Another restart of the SQL Server services will be required.

  
## <a name="OldInstall"></a> Unattended Install of R Services (In-Database) in SQL Server 2016
 
 The following example shows the **minimum** required features to specify in the command line when performing a silent, unattended install of SQL Server R Services. Requires SQL Server 2016.

1. Open an elevated command prompt, and run the following command:  

    ```  
    Setup.exe /q /ACTION=Install /FEATURES=SQL,AdvancedAnalytics /INSTANCENAME=MSSQLSERVER /SECURITYMODE=SQL /SAPWD="%password%" /SQLSYSADMINACCOUNTS="<username>" /IACCEPTSQLSERVERLICENSETERMS /IACCEPTROPENLICENSETERMS  
    ```
    Note the flags required for 2016 R SERVICES: `AdvancedAnalytics` and `IACCEPTRLICENSETERMS`.  
2. Restart the server.
3. Perform the post-installation configuration steps as described in [this section](#bkmk_PostInstall). Another restart of the SQL Server services will be required.

## <a name = "bkmk_PostInstall"></a>Post-Installation Steps  

1.  After installation is complete, open a new Query window in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and run the following command to enable the feature.  
  
    ```  
    EXEC sp_configure  'external scripts enabled', 1  
    RECONFIGURE WITH OVERRIDE   
    ```  
  
    > [!NOTE]  
    >  You must explicitly enable the feature and reconfigure; otherwise, it will not be possible to invoke external scripts even if the feature has been installed.  
  
2.  Restart the SQL Server service for the reconfigured instance. Doing so will automatically restart the related [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service as well.  

3. Additional steps might be required if you have a custom security configuration, or if you will be using the SQL Server to support remote compute contexts. For more information, see [Upgrade and Installation FAQ](../../advanced-analytics/r-services/upgrade-and-installation-faq-sql-server-r-services.md). 
