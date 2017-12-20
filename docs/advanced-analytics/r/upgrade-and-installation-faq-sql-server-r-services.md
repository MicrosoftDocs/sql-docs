---
title: "Upgrade and installation FAQ for SQL Server Machine Learning | Microsoft Docs"
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
ms.assetid: 001e66b9-6c3f-41b3-81b7-46541e15f9ea
caps.latest.revision: 59
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "On Demand"
---
# Upgrade and installation FAQ for SQL Server Machine Learning

This topic provides answers to some common questions about installation of machine learning features in SQL Server. It also covers common questions about upgrades.

+ Some problems occur only with upgrades from pre-release versions. Therefore, we recommend that you identify your version and edition first before reading these notes.
+ Upgrade to the most current release or service release as soon as possible, to resolve any issues that were fixed in recent releases.

**Applies to:** SQL Server 2016 R Services, SQL Server 2017 Machine Learning Services (In-Database)

## Performing setup for the first time

Follow the procedures for setting up [!INCLUDE[sscurrent_md](../../includes/sscurrent_md.md)] and the R components, as described here: 

+ [Set up SQL Server R Services or Machine Learning Services In-Database](../r/set-up-sql-server-r-services-in-database.md)
+ [Set up SQL Server 2017 with Python](../python/setup-python-machine-learning-services.md)
+ [Create a standalone R Server](../r/create-a-standalone-r-server.md)

> [!IMPORTANT]
> 
> After you have installed SQL Server and the machine learning features, before you can use R or Python scripts, you must complete some additional configuration steps. That is because the external script execution feature is not enabled by default.

### Requirements and restrictions

Depending on the build of SQL Server that you are installing, some of the following limitations might apply:

- In early versions of SQL Server 2016 R Services, 8dot3 notation was required on the drive that contains the working directory. If you installed a pre-release version, upgrading to SQL Server 2016 Service Pack 1 should fix this issue. This requirement does not apply to releases after SP1.

- Currently, you cannot install [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] on a failover cluster. 

- On an Azure VM, some additional configuration might be necessary. For example, you might need to create a firewall exception to support remote access.

- Side-by-side installation with another version of R, or with other releases from Revolution Analytics, is not supported.

- New installation of any pre-release version of [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] is no longer supported. If you are using a pre-release version, upgrade as soon as possible.

- Disable virus scanning before beginning setup. After setup is completed, we recommend suspending virus scanning on the folders used by [!INCLUDE[ssnoversion](../../includes/ssnoversion.md)]. Preferably, suspend scanning on the entire [!INCLUDE[ssnoversion](../../includes/ssnoversion.md)] tree.

### Licensing agreements for unattended installs

If you use the command line to upgrade an instance of SQL Server, make sure that the command line includes both the [!INCLUDE[ssnoversion](../../includes/ssnoversion.md)] licensing agreement parameter, and the new license agreement parameters for R and Python.

### Offline installation of machine learning components for a localized version of SQL Server

When you install [!INCLUDE[ssnoversion](../../includes/ssnoversion.md)] machine learning components on a computer that does not have internet access, you must take some additional steps:

+ Download the R or Python component installers to a local folder before you run SQL Server setup.
+ In some cases, you might need to edit the installer file to ensure that the correct language is installed.
+ The language identifier used for the machine learning components must be the same as the SQL Server setup language, or you cannot complete setup.

For more information, see [Installing machine learning components without internet access](../r/installing-ml-components-without-internet-access.md).

## Post-installation configuration

To use machine learning with R or Python, some additional configuration is required after running SQL Server setup. The precise steps that are required depend on the security level of the server, and how you have configured your SQL Server instance and databases.

Review all options in the list of post-installation instructions to see which additional steps might be required in your environment.

+ [Set up SQL Server machine learning in database](set-up-sql-server-r-services-in-database.md) 

## Upgrades or uninstallation

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

### Support for slipstream upgrades

Slipstream setup refers to the ability to apply a patch or update to a failed instance installation, to repair existing problems. The advantage of this method is that SQL Server is updated at the same time that you perform setup, avoiding a separate restart later.

If the server does not have internet access, be sure to download the SQL Server installer. You must also separately download matching versions of the R component installers *before* beginning the update process. 

For download locations, see [Installing machine learning components without internet access](installing-ml-components-without-internet-access.md).

When all setup files have been copied to a local directory, start the setup utility by typing SETUP.EXE from the command line.

- Use the */UPDATESOURCE* argument to specify the location of a local file that contains the SQL Server update, such as a cumulative update or service pack release.

- Use the */MRCACHEDIRECTORY* argument to specify the folder that contains the R component CAB files.

For more information, see this blog by the support team: [Deploying R Services on computers without internet access](https://blogs.msdn.microsoft.com/sqlcat/2016/10/20/do-it-right-deploying-sql-server-r-services-on-computers-without-internet-access/).

### Get machine learning components for offline installs

If you install or upgrade servers that are not connected to the internet, you must download an updated version of the machine learning components manually before beginning the refresh. 

+ [Installing machine learning components without internet access](../../advanced-analytics/r/installing-ml-components-without-internet-access.md).

### Support policy and schedule for update of machine learning components

As hotfixes or improvements to SQL Server are released, machine learning components are  automatically upgraded or refreshed, if your instance already includes the feature.

As of December 2016, you can upgrade machine learning components on a faster cadence than the SQL Server release cycle. You do this by *binding* an instance of SQL Server to the Modern Software Lifecycle policy. Whenever a new version of the machine learning tools is released by the machine learning development team, you can download the latest version and apply it to a SQL Server instance that is used for machine learning.

For more information, see:

+ [Support timeline for Microsoft R Server and Machine Learning Server](https://docs.microsoft.com/machine-learning-server/resources-servicing-support)
+ [Use SqlBindR to upgrade an instance of SQL Server](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md).

## R Server (Standalone)

This section describes issues specific to installations of Microsoft R Server (Standalone) that use SQL Server 2016 setup. 

For issues related to upgrades from  R Server to Machine Learning Server, see [Install Machine Learning Server for Windows](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-install).

### Problems when R Services and R Server Standalone are installed on the same computer

In earlier versions of SQL Server 2016, installing both R Server (Standalone) and R Services (In-Database) at the same time sometimes caused setup to fail with an “access denied” message. This issue was fixed in Service Pack 1 for SQL Server 2016.

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

## See also

 [Getting started with SQL Server R Services](../r/getting-started-with-sql-server-r-services.md)

 [Getting started with Microsoft R Server Standalone](../r/getting-started-with-microsoft-r-server-standalone.md)
