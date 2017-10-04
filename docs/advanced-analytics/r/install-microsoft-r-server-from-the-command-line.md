---
title: "Install Microsoft R Server from the Command Line | Microsoft Docs"
ms.custom: ""
ms.date: "04/12/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: fb4446ba-e9ce-4b93-9854-5e8a58507da0
caps.latest.revision: 4
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Install Microsoft R Server from the Command Line
    
This topic describes how to use SQL Server command-line arguments to install Microsoft R Server in SQL Server 2016, or install Machine Learning Server (Standalone) in SQL Server 2017. 

> [!NOTE]
You can also install Microsoft R Server by using a separate Windows installer. For more information, see [Install R Server 9.0.1 for Windows](https://msdn.microsoft.com/microsoft-r/rserver-install-windows). 

## Prerequisites

This method of installation requires that you know how to perform a command-line installation of SQL Server and are familiar with its scripting arguments.

- **Unattended** installation requires that you specify the location of the setup utility, and use arguments to indicate which features to install. 
- For a **quiet** installation, provide the same arguments and add the **/q** switch. No prompts will be provided and no interaction is required. However, setup will fail if any required arguments are omitted.

For more information, see [Install SQL Server 2016 from the Command Prompt](../../database-engine/install-windows/install-sql-server-2016-from-the-command-prompt.md).

## SQL Server 2017: Microsoft Machine Learning Server (Standalone)

Run the following command from an elevated command prompt to install only Microsoft Machine Learning Server (Standalone) and its prerequisites.  The example shows the arguments used to install R.

```
Setup.exe /q /ACTION=Install /FEATURES=SQL_SHARED_MR, SQL_INST_MR  /IACCEPTROPENLICENSETERMS /IACCEPTSQLSERVERLICENSETERMS 
```

To view progress and prompts, remove the _/q_ argument.

- **FEATURES = SQL_SHARED_MR** gets only the Machine Learning Server components. This includes any prerequisites, which are installed silently by default.
- **SQL_INST_MR** is required to installl support for the R language.
- **SQL_INST_MPY** is required to install support for Python.
- **IACCEPTROPENLICENSETERMS** indicates you have accepted the license terms for using the open source R components.
- **IACCEPTPYTHONLICENSETERMS** indicates you have accepted the license terms for using the Python components.
- **IACCEPTSQLSERVERLICENSETERMS** is required to run the setup wizard.

**Notes**

1. The FEATURES argument is required, as is the SQL Server licensing terms.
2. Specify at least one language, together with the licensing agreement flag.
3. You can install one language, or both R and Python, but a separate license is required for each.

## SQL Server 2016: Microsoft R Server (Standalone)

Run the following command from an elevated command prompt to install only Microsoft R Server (Standalone) and its prerequisites.  The example shows the arguments used with SQL Server 2016 setup.

```
Setup.exe /q /ACTION=Install /FEATURES=SQL_SHARED_MR /IACCEPTROPENLICENSETERMS /IACCEPTSQLSERVERLICENSETERMS
```

## Offline Installation

If you install Machine Learning Server or Microsoft R Server (Standalone) on a computer that has no Internet access, you must download the required R components in advance, and copy them to a local folder. For download locations, see [Installing R Components without Internet Access](../r/installing-ml-components-without-internet-access.md).

## What Is Installed

After setup is complete, you can review the configuration file created by SQL Server setup, along with a summary of setup actions.

By default, all setup logs and summaries for SQL Server and related features are created in the following folders:

- SQL Server 2016:  `C:\Program Files\Microsoft SQL Server\130\Setup Bootstrap\Log`
- SQL Server 2017: `C:\Program Files\Microsoft SQL Server\140\Setup Bootstrap\Log`

A separate subfolder is created  for each feature installed.

To set up another instance of Microsoft R Server with the same parameters, you can re-use the configuration file that is created during installation. For more information, see [Install SQL Server Using a Configuration File](../../database-engine/install-windows/install-sql-server-2016-using-a-configuration-file.md)


## Customize Your R Environment

After installation, you can install additional R packages. For more information, see [Installing and Managing R Packages](../r/install-additional-r-packages-on-sql-server.md).

> [!IMPORTANT]
> If you intend to run your R code on SQL Server, make sure that you install the same packages on the computer you'll use for developing the solution, and the SQL Server instance where you'll execute or deploy the solution.

After you have installed Machine Learning Services for R (In-Database), you can use the separate Windows installer to upgrade the version of R that is associated with a specified SQL Server instance. For more information, see [Use SqlBindR to Upgrade R](../r/use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md).


