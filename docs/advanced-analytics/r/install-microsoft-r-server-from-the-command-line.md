---
title: "Install Machine Learning Server (Standalone) or Microsoft R Server (Standalone) from the command line | Microsoft Docs"
ms.custom: ""
ms.date: "10/31/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: fb4446ba-e9ce-4b93-9854-5e8a58507da0
caps.latest.revision: 4
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
ms.workload: "Inactive"
---
# Install Machine Learning Server (Standalone) or Microsoft R Server (Standalone) from the command line

This article describes how to use SQL Server command-line arguments to install the following SQL Server features by using the command line:

+ [Machine Learning Server (Standalone) in SQL Server 2017](#bkmk_mls2017) 
+ [Microsoft R Server (Standalone), in SQL Server 2016](#bkmk_mrs2016)

An **unattended** installation requires that you specify the location of the setup utility, and use arguments to indicate which features to install.

For a **quiet** installation, provide the same arguments and add the **/q** switch. No prompts are provided and no interaction is required. However, setup fails if any required arguments are omitted.

## Prerequisites

You should know how to perform a command-line installation of SQL Server and be familiar with its scripting arguments.

For more information, see [Install SQL Server from the command prompt](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md).

If you install Machine Learning Server or Microsoft R Server (Standalone) on a computer that has no Internet access, you must download the required R (or Python) components in advance, and copy them to a local folder. For download locations, see [Installing machine learning components without internet access](installing-ml-components-without-internet-access.md).


## <a name="bkmk_mls2017"></a> Install Microsoft Machine Learning Server (Standalone)

**Applies to: SQL Server 2017**

Run the following command from an elevated command prompt to install only Machine Learning Server (Standalone) and its prerequisites.

+ The FEATURES argument is required, as is the SQL Server licensing terms.
+ You can install one language, or both R and Python, but a separate license is required for each.

This example shows the arguments used to install R.

```
Setup.exe /q /ACTION=Install /FEATURES=SQL_SHARED_MR, SQL_INST_MR  /IACCEPTROPENLICENSETERMS /IACCEPTSQLSERVERLICENSETERMS
```

This example shows the arguments used to install Python.

```
Setup.exe /q /ACTION=Install /FEATURES=SQL_SHARED_MR, SQL_INST_MPY  /IACCEPTPYTHONOPENLICENSETERMS /IACCEPTSQLSERVERLICENSETERMS
```

+ To view progress and prompts, remove the _/q_ argument.
+ If you use the argument **FEATURES = SQL_SHARED_MR**, only the Machine Learning Server components are installed, with neither R nor Python. This installation includes all prerequisites, which are installed silently by default. However, we recommend that you add at least one language when installing for the first time.
+ Add **SQL_INST_MR** to install support for R.
+ Add **SQL_INST_MPY** to install support for Python.
+ **IACCEPTROPENLICENSETERMS** indicates you have accepted the license terms for using the open source R components.
+ **IACCEPTPYTHONLICENSETERMS** indicates you have accepted the license terms for using the Python components.
+ **IACCEPTSQLSERVERLICENSETERMS** is required to run the setup wizard.


## <a name="bkmk_mrs2016"></a> Install Microsoft R Server (Standalone)

**Applies to: SQL Server 2016**

Run the following command from an elevated command prompt to install **only** Microsoft R Server (Standalone) and its prerequisites. 

```
Setup.exe /q /ACTION=Install /FEATURES=SQL_SHARED_MR /IACCEPTROPENLICENSETERMS /IACCEPTSQLSERVERLICENSETERMS
```

> [!TIP]
> We recommend that you do not install this on the same computer that is hosting an instance of SQL Server R Services.

## Post-installation tasks

To set up another instance of Microsoft R Server with the same parameters, you can re-use the configuration file that is created during installation. For more information, see [Install SQL Server using a configuration file](../../database-engine/install-windows/install-sql-server-using-a-configuration-file.md).

### Review installed components

After setup is complete, you can review the configuration file created by SQL Server setup, along with a summary of setup actions.

By default, all setup logs and summaries for SQL Server and related features are created in the following folders:

+ SQL Server 2017: `C:\Program Files\Microsoft SQL Server\140\Setup Bootstrap\Log`
+ SQL Server 2016:  `C:\Program Files\Microsoft SQL Server\130\Setup Bootstrap\Log`

A separate subfolder is created for each feature that you installed.

### Customize the R or Python environment

After installation, you can install additional R or Python packages. The process differs somewhat depending on whether you are using SQL Server 2016 or SQL Server 2017.

In SQl Server 2017, you can install and manage R packages by using T-SQL. For more information, see [Installing and managing R packages](../r/install-additional-r-packages-on-sql-server.md).

For Python, and in SQL Server 2016, an administrator must install any additional libraries that might be required.

> [!IMPORTANT]
> If you intend to run your R code on SQL Server, make sure that you install the same packages on the computer you'll use for developing the solution, and the SQL Server instance where you'll execute or deploy the solution.

### Upgrading R Server or SQL Server machine learning

If you do not intend to use SQL Server, you can install Microsoft R Server or Machine Learning Server by using a separate Windows installer. For download locations and instructions, see these links:

+ [Install Machine Learning Server for Windows](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-install)
+ [Install R Server 9.0.1 for Windows](https://docs.microsoft.com/machine-learning-server/install/r-server-install-windows) 

The separate windows installer for Machine Learning Server can also be used to upgrade the machine learning components associated with the instance.  For more information, see these links:

+ [Use SqlBindR to upgrade R](../r/use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md)
