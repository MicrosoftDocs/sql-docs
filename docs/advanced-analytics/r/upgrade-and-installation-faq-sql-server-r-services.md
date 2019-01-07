---
title: Upgrade and installation frequently asked questions (FAQ) - SQL Server Machine Learning Services
ms.custom: sqlseattle
ms.prod: sql
ms.technology: machine-learning
  
ms.date: 05/15/2018
ms.topic: conceptual
ms.author: heidist
author: HeidiSteen
manager: cgronlun
---
# Upgrade and installation FAQ for SQL Server Machine Learning or R Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This topic provides answers to some common questions about installation of machine learning features in SQL Server. It also covers common questions about upgrades.

+ Some problems occur only with upgrades from pre-release versions. Therefore, we recommend that you identify your version and edition first before reading these notes. To get version information, run `@@VERSION` in a query from SQL Server Management Studio.
+ Upgrade to the most current release or service release as soon as possible to resolve any issues that were fixed in recent releases.

**Applies to:** SQL Server 2016 R Services, SQL Server 2017 Machine Learning Services (In-Database)

## Requirements and restrictions on older versions of SQL Server 2016 

Depending on the build of SQL Server that you are installing, some of the following limitations might apply:

- In early versions of SQL Server 2016 R Services, 8dot3 notation was required on the drive that contains the working directory. If you installed a pre-release version, upgrading to SQL Server 2016 Service Pack 1 should fix this issue. This requirement does not apply to releases after SP1.

- Currently, you cannot install [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] on a failover cluster. However, SQL Server 2019 preview does provide failover support if you would like to evaluate this capablity in a test environment. For more information, see [What's New](../what-s-new-in-sql-server-machine-learning-services.md).

- On an Azure VM, some additional configuration might be necessary. For example, you might need to create a firewall exception to support remote access.

- Side-by-side installation with another version of R, or with other releases from Revolution Analytics, is not supported.

- Disable virus scanning before beginning setup. After setup is completed, we recommend suspending virus scanning on the folders used by [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]. Preferably, suspend scanning on the entire [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] tree.

 - Installing Microsoft R Server on an instance of SQL Server installed on Windows Core. In the RTM version of SQL Server 2016, there was a known issue when adding Microsoft R Server to an instance on Windows Server Core edition. This has been fixed. If you encounter this issue, you can apply the fix described in [KB3164398](https://support.microsoft.com/kb/3164398) to add the R feature to the existing instance on Windows Server Core. For more information, see [Can't install Microsoft R Server Standalone on a Windows Server Core operating system](https://support.microsoft.com/kb/3168691).


## Offline installation of machine learning components for a localized version of SQL Server 2016

Early-release versions of SQL Server 2016 failed to install locale-specific .cab files during offline setup without an internet connection. This issue was fixed in later releases, but if the installer returns a message stating it cannot install the correct language, you can edit the filename to allow setup to continue.

+ Manually edit the installer file to ensure that the correct language is installed. For example, to install the Japanese version of SQL Server, you would change the name of the file from SRS_8.0.3.0_**1033**.cab to SRS_8.0.3.0_**1041**.cab.
+ The language identifier used for the machine learning components must be the same as the SQL Server setup language, or you cannot complete setup.

## Pre-release versions: support policies, upgrade, and known issues

New installations of any pre-release version of [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] is no longer supported. If you are using a pre-release version, upgrade as soon as possible.

This section contains detailed instructions for specific upgrade scenarios.

### How to upgrade SQL Server

You can upgrade your version of SQL Server by re-running the setup wizard.

+ [Upgrade SQL Server](../../database-engine/install-windows/upgrade-sql-server.md)
+ [Upgrade SQL Server Using the Installation Wizard](../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md)

You can upgrade just the machine learning components by using a process called binding: 
+ [Use SqlBindR to upgrade machine learning components](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md)

### End of support for in-place upgrades from prerelease versions

Upgrades from pre-release versions of SQL Server 2016 are no longer supported. This includes SQL Server 2016 CTP3, CTP3.1, CTP3.2, RC0, or RC1.

The following versions were installed with pre-release versions of SQL Server 2016.

| Version | Build         |
|---------|---------------|
| CTP 3.0 | 13.0.xxx      |
| CTP 3.1 | 13.0.801.12   |
| CTP 3.2 | 13.0.900.73   |
| CTP 3.3 | 13.0.1000.281 |
| RC1     | 13.0.1200.242 |
| RC2     | 13.0.1300.275 |
| RC3     | 13.0.1400.361 |

If you have any doubt about which version you are using, run `@@VERSION` in a query from SQL Server Management Studio.

In general, the process for upgrade is as follows:

1. Back up scripts and data.
2. Uninstall the pre-release version.
3. Install a release version.

Uninstalling a pre-release version of the SQL Server machine learning components can be complex and could require running a special script. Contact technical support for assistance.

###  <a name="bkmk_Uninstall"></a> Uninstall prior to upgrading from an older version of Microsoft R Server

If you installed a pre-release version of Microsoft R Server, you must uninstall it before you can upgrade to a newer version.

1.  In **Control Panel**, click **Add/Remove Programs**, and select `Microsoft SQL Server 2016 <version number>`.

2.  In the dialog box with options to **Add**, **Repair**, or **Remove** components, select **Remove**.
  
3.  On the **Select Features** page, under **Shared Features**, select **R Server (Standalone)**. Click **Next**, and then click **Finish** to uninstall just the selected components.

## R Services and R Server (Standalone) side-by-side errors 

In earlier versions of SQL Server 2016, installing both R Server (Standalone) and R Services (In-Database) at the same time sometimes caused setup to fail with an "access denied" message. This issue was fixed in Service Pack 1 for SQL Server 2016.

If you encountered this error, and need to upgrade these features, perform a slipstream installation of SQL Server 2016 with SP1. There are two ways to resolve the issue, both of which require uninstalling and reinstalling.

1. Uninstall R Services (In-Database), and make sure the user accounts for SQLRUserGroup are removed.

2. Restart the server, and then reinstall R Server (Standalone).

3. Run SQL Server setup once more, and this time select **Add Features to Existing SQL Server**.

4. Choose the instance, and then select the **R Services (In-Database)** option to add.

If this procedure fails to resolve the problem, try the following workaround:

1. Uninstall R Services (In-Database) and R Server (Standalone) at the same time.

2. Remove the local user accounts (SQLRUserGroup).

3. Restart the server.

4. Run SQL Server setup, and add the R Services (In-Database) feature only. Do not select **R Server (Standalone)**.

Generally, we recommend that you do not install both R Services (In-Database) and R Server (Standalone) on the same computer. However, assuming the server has sufficient capacity, you might find R Server Standalone might be useful as a development tool. Another possible scenario is that you need to use the operationalization features of R Server, but also want to access SQL Server data without data movement.

## Incompatible version of R Client and R Server

If you install Microsoft R Client and use it to run R in a remote SQL Server compute context, you might get an  error like this:

*You are running version 9.0.0 of Microsoft R client on your computer, which is incompatible with the Microsoft R Server version 8.0.3. Download and install a compatible version.*

In SQL Server 2016, it was required that the version of R that was running in SQL Server R Services be exactly the same as the libraries in Microsoft R Client. That requirement has been removed in later versions. However, we recommend that you always get the latest versions of the machine learning components, and install all service packs. 

If you have an earlier version of Microsoft R Server and need to ensure compatibility with Microsoft R Client 9.0.0, install the updates that are described in this [support article](https://support.microsoft.com/kb/3210262).


## Installation fails with error "Only one Revolution Enterprise product can be installed at a time."

You might encounter this error if you have an older installation of the Revolution Analytics products, or a pre-release version of SQL Server R Services. You must uninstall any previous versions before you can install a newer version of Microsoft R Server. Side-by-side installation with other versions of the Revolution Enterprise tools is not supported.

However, side-by-side installs are supported when using R Server Standalone with [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] or SQL Server 2016.

## Registry cleanup to uninstall older components

If you have problems removing an older version, you might need to edit the registry to remove related keys.

> [!IMPORTANT]
> This issue applies only if you installed a pre-release version of Microsoft R Server or a CTP version of SQL Server 2016 R Services.
  
1. Open the Windows Registry, and locate this key: `HKLM\Software\Microsoft\Windows\CurrentVersion\Uninstall`.
2. Delete any of the following entries if present, and if the key contains only the value `sEstimatedSize2`:
  
    -   E0B2C29E-B8FC-490B-A043-2CAE75634972        (for 8.0.2)
  
    -   46695879-954E-4072-9D32-1CC84D4158F4        (for 8.0.1)
  
    -   2DF16DF8-A2DB-4EC6-808B-CB5A302DA91B        (for 8.0.0)
  
    -   5A2A1571-B8CD-4AAF-9303-8DF463DABE5A        (for 7.5.0)

## See also

 [SQL Server Machine Learning Services (In-Database)](../r/sql-server-r-services.md)

 [SQL Server Machine Learning Server (Standalone)](../r/r-server-standalone.md)
