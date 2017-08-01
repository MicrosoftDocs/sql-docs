---
title: "Unattended installation of Python Machine Learning Services (In-Database) | Microsoft Docs"
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
caps.latest.revision: 1
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Unattended installation of Python Machine Learning Services (In-Database)

This topic describes how to use the command-line arguments in SQL Server 2017 setup to install the SQL Server database engine with Machine Learning Services and Python, using quiet mode.

> [!NOTE]
> Don't forget to include the command-line arguments for the licensing agreements, one for Python and one for SQL Server.

## Prerequisites

Before beginning the installation process, note these requirements:

+ The Python service cannot be installed independently of the SQL Server 2017 database engine. You must include the **SQL** feature.
+ If you are performing an offline install, you must download the required Python components in advance, and copy them to a local folder. For download locations, see [Installing Machine Learning Components without Internet Access](../../advanced-analytics/r-services/installing-ml-components-without-internet-access.md).
+ There is a new parameter, */IACCEPTPYTHONLICENSETERMS*, that indicates you have accepted the license terms for using the Python components provided by Continuum Analytics. If you do not include this parameter in your command line, setup will fail.
+ To complete setup without having to respond to prompts, make sure that you have identified all required arguments, including those for Python and SQL Server licensing, and for any other features that you might want to install.
+  Mixed Mode authentication was required in some pre-release versions of SQL Server 2016. It is no longer required, though it might be useful in scenarios where data scientists are developing and testing solutions remotely using SQL logins.

## Perform an unattended install of Python Machine Learning Services (In-Database)

The following example shows the **minimum** required features to specify in the command line when performing a silent, unattended install of Python with the database engine on the default instance.

> [!NOTE]
> This feature requires SQL Server 2017. Python is not supported in earlier versions of SQL Server.

1. Open an elevated command prompt, and run the following command:

    ```  
    Setup.exe /q /ACTION=Install /FEATURES=SQL,ADVANCEDANALYTICS, SQL_INST_MPY /INSTANCENAME=MSSQLSERVER /SECURITYMODE=SQL /SAPWD="%password%" /SQLSYSADMINACCOUNTS="<username>" /IACCEPTSQLSERVERLICENSETERMS /IACCEPTPYTHONLICENSETERMS
    ```

    > [!NOTE]
    > 
    > There are new setup flags for Python: `SQL_INST_MPY` and `IACCEPTPYTHONLICENSETERMS`

2. Restart the server as directed.
3. Perform the post-installation configuration steps as described in [this section](#bkmk_PostInstall). Another restart of the SQL Server services will be required.

## <a name = "bkmk_PostInstall"></a>Post-installation steps

1.  After installation is complete, open a new **Query** window in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and run the following command to enable the feature.

    ```SQL
    EXEC sp_configure  'external scripts enabled', 1
    RECONFIGURE WITH OVERRIDE
    ```  
  
    > [!NOTE]  
    >  You must explicitly enable the feature and reconfigure; otherwise, it will not be possible to invoke external scripts even if the feature has been installed.
  
3.  Restart the SQL Server service for the reconfigured instance. Doing so will automatically restart the related [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service as well.

3. Additional steps might be required if you have a custom security configuration, or if you will be using the SQL Server to support remote compute contexts. For more information, see [Troubleshooting machine learning setup](../machine-learning-troubleshooting-faq.md).