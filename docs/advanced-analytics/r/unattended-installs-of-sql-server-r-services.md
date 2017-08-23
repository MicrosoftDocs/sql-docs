---
title: "Unattended installation of Machine Learning Services | Microsoft Docs"
ms.custom: ""
ms.date: "07/31/2017"
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
# Unattended installation of Machine Learning Services (In-Database)

This topic describes how to use command-line arguments with SQL Server setup to install machine learning in an instance of the database engine, in quiet mode. In an unattended installation, you do not use the interactive features of the setup wizard, and must provide all arguments required to complete installation, including licensing agreements for SQL Server and for machine learning components.

**Applies to:** SQL Server 2016 R Services, SQL Server 2017 Machine Learning Services (In-Database)

> [!IMPORTANT]
> 
> In both SQL Server 2016 and SQL Server 2017, additional steps are required after setup is finished to enable the feature. These include a reconfiguration and restart of the instance. Be sure to review all steps.

## Prerequisites

+ You must install the database engine on each instance where you will use machine learning.

+ If the computer does not have Internet access, be sure to install the installers for the machine learning components beforehand. For download locations, see [Installing machine learning components without Internet access](../../advanced-analytics/r/installing-ml-components-without-internet-access.md).

+ There are new licensing parameters related to the open source components for R and Python. You must accept the license terms with a separate argument for each language that you install. If you do not include this parameter in your command line, setup will fail.

+ To complete setup without having to respond to prompts, make sure that you have identified all required arguments, including those for licensing, and for any other features that you might want to install.

> [!NOTE] 
> The Mixed security mode that supports SQL logins was required in early releases. However, you might consider enabling Mixed Mode authentication to support solution development by data scientists using a SQL login.

## <a name="bkmk_NewInstall"></a> Unattended installation of Machine Learning Services with R

The following example shows the **minimum** required features to specify in the command line when performing a silent, unattended install of SQL Server 2017 Machine Services (In-Database).

1. Open an elevated command prompt, and run the following command:

    ```  
    Setup.exe /q /ACTION=Install /FEATURES=SQLENGINE,ADVANCEDANALYTICS, SQL_INST_MR /INSTANCENAME=MSSQLSERVER /SECURITYMODE=SQL /SAPWD="%password%" /SQLSYSADMINACCOUNTS="<username>" /IACCEPTSQLSERVERLICENSETERMS /IACCEPTROPENLICENSETERMS
    ```
    
    Note the flags required for R in SQL Server 2017: `ADVANCEDANALYTICS`, `SQL_INST_MR`, and `IACCEPTROPENLICENSETERMS`.
2. Restart the server.
3. Perform the post-installation configuration steps as described in [this section](#bkmk_PostInstall). Another restart of the SQL Server services will be required.

## <a name="OldInstall"></a> Unattended install of R Services (In-Database)
 
 The following example shows the **minimum** required features to specify in the command line when performing a silent, unattended install of SQL Server 2016 R Services.

1. Open an elevated command prompt, and run the following command:

    ```  
    Setup.exe /q /ACTION=Install /FEATURES=SQL,ADVANCEDANALYTICS /INSTANCENAME=MSSQLSERVER /SECURITYMODE=SQL /SAPWD="%password%" /SQLSYSADMINACCOUNTS="<username>" /IACCEPTSQLSERVERLICENSETERMS /IACCEPTROPENLICENSETERMS
    ```
    Note the flags required for 2016 R SERVICES: `ADVANCEDANALYTICS` and `IACCEPTROPENLICENSETERMS`.
2. Restart the server.
3. Perform the post-installation configuration steps as described in [this section](#bkmk_PostInstall). Another restart of the SQL Server services will be required.

## <a name = "bkmk_PostInstall"></a>Additional steps after setup

1.  After installation is complete, open a new **Query** window in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and run the following command to enable the feature.
  
    ```SQL
    EXEC sp_configure  'external scripts enabled', 1
    RECONFIGURE WITH OVERRIDE
    ```
  
    > [!NOTE]
    >  You must explicitly enable the feature and reconfigure; otherwise, it will not be possible to invoke external scripts even if the feature has been installed.
  
2.  Restart the SQL Server service for the reconfigured instance. Doing so will automatically restart the related [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service as well.

3. Additional steps might be required if you have a custom security configuration, or if you will be using the SQL Server to support remote compute contexts. For more information, see [Upgrade and Installation FAQ](../../advanced-analytics/r/upgrade-and-installation-faq-sql-server-r-services.md).
