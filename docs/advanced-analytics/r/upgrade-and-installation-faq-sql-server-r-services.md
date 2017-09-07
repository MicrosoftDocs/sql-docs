---
title: "Upgrade and installation FAQ (SQL Server R Services) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "06/16/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 001e66b9-6c3f-41b3-81b7-46541e15f9ea
caps.latest.revision: 59
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Upgrade and installation FAQ (SQL Server R Services)

This topic provides answers to some common questions about installation of machine learning features in SQL Server. It also covers common questions about upgrades. Some problems occur only with upgrades from pre-release versions. Therefore, we recommend that you identify your version and edition first, and upgrade to the most current release or service release as soon as possible.

**Applies to:** SQL Server 2016 R Services, SQL Server 2017 Machine Learning Services (In-Database)

## Performing setup for the first time

Follow the procedures for setting up [!INCLUDEssCurrent] and the R components, as described here: 

+ [Set up SQL Server R Services or Machine Learning Services In-Database](../../advanced-analytics/r-services/set-up-sql-server-r-services-in-database.md)
+ [Set up SQL Server 2017 with Python](../python/setup-python-machine-learning-services.md)
+ [Create a Standalone R Server](create-a-standalone-r-server.md)

After you have installed SQL Server, to use external R or Python scripts, you must complete some additional configurations. That is because the external script execution feature is not enabled by default.

> [!NOTE]
> Do not use setup instructions that were published prior to the public release of SQL Server 2016. The setup process changed completely between early releases and the official release version. 

### Requirements and restrictions

Depending on the build of R Services you are installing, some of the following
limitations might apply:

- In early versions of SQL Server 2016 R Services, 8dot3 notation was required on the drive that contains the working directory. If you installed a pre-release version, upgrading to SQL Server 2016 Service Pack 1 should remove this requirement.

- You cannot install [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] on a failover cluster. 

- On an Azure VM, some additional configuration might be necessary. For example, you might need to create a firewall exception to support remote access.

- Side-by-side installation with another version of R, or with other releases from Revolution Analytics, is not supported.

- New installation of any pre-release version of [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] is no longer supported. If you are using a pre-release version, upgrade as soon as possible.

- Disable virus scanning before beginning setup. After setup is completed, we recommend suspending virus scanning on the folders used by SQL Server (preferably the entire tree).

### Licensing agreements for unattended installs

If you use the command line to upgrade an instance of SQL Server, make sure that the command line includes the new license agreement parameter, */IACCEPTROPENLICENSEAGREEMENT*. Setup can fail if you don't use this parameter.

### Offline installation of R components for a localized version of SQL Server

When you install R Services on a computer that does not have Internet access, you must take two additional steps. Download the R component installer to a local folder before you run SQL Server setup, and edit the installer file to ensure that the correct language is installed.

The language identifier used for the R components must be the same as the SQL Server setup language, or the **Next** button is disabled and you cannot complete setup.

For more information, see [Installing R Components without Internet access](../../advanced-analytics/r-services/installing-ml-components-without-internet-access.md).

## Post-installation configuration

To use machine learning with R or Python, some additional configuration is required after running SQL Server setup. Additional steps might be required depending on the security level of the server and of your SQL Server instance and databases. Review these steps from the setup instructions to determine if any additional configuration might be needed.

[Set up Sql Server R Services In-Database](set-up-sql-server-r-services-in-database.md)

- The feature that supports running external scripts, such as R or Python, is disabled by default for database security, and must be enabled.

- Ensure that the worker accounts that are used by Launchpad to run R or Python have access to the instance. See [Enable implied authentication for Launchpad account group].

- You might need to enable remote access on the server, or create a firewall rule allowing inbound communication with SQL Server.

- Depending on the planned workload, you might need to optimize the server for machine learning tasks. 

## Upgrades or uninstallation

This section contains detailed instructions for specific upgrade scenarios.

Upgrades from a pre-release version of SQL Server 2016 R Services are no longer supported. We recommend that you uninstall the pre-release version, and then install a release version as soon as possible.

### Support for slipstream upgrades

Slipstream setup refers to the ability to apply a patch or update to a failed instance installation, to repair existing problems. The advantage of this method is that SQL Server is updated at the same time that you perform setup, avoiding a separate restart later.

If the server does not have Internet access, be sure to download the SQL Server installer. You must also separately download matching versions of the R component installers *before* beginning the update process. 

For download locations, see [Installing R components without Internet access](installing-ml-components-without-internet-access.md).

When all setup files have been copied to a local directory, start the setup utility by typing SETUP.EXE from the command line.

- Use the */UPDATESOURCE* argument to specify the location of a local file containing the SQL Server update, such as a cumulative update or service pack release.

- Use the */MRCACHEDIRECTORY* argument to specify the folder containing the R component CAB files.

For more information, see this blog by the support team: [Deploying R Services on computers without Internet access](https://blogs.msdn.microsoft.com/sqlcat/2016/10/20/do-it-right-deploying-sql-server-r-services-on-computers-without-internet-access/)

### Upgrade R components offline

If you install or upgrade servers that are not connected to the Internet, you must download an updated version of the R components manually before beginning the refresh. For more information, see [Installing R components without Internet access](../../advanced-analytics/r-services/installing-ml-components-without-internet-access.md).

### Schedule for update of R components

As hotfixes or improvements to SQL Server 2016 are released, R components are also upgraded or refreshed, if your instance already includes the R Services feature.

If you are using SQL Server 2017, upgrades to R components are automatically installed.

As of December 2016, you can upgrade R components on a faster cadence than the SQL Server release cycle. Do this by *binding* an instance of R Services to the Modern Software Lifecycle policy. Currently support is provided only for upgrade of 2016 instances. When a new version of R Server is released, you will be able to upgrade to 2017 instances as well.

For more information, see [Use SqlBindR to upgrade an instance of SQL Server R Services](../../advanced-analytics/r-services/use-sqlbindr-exe-to-upgrade-an-instance-of-r-services.md).

### Upgrade from a pre-release version of SQL Server 2016

In general, in-place upgrades are not supported for any pre-release versions.

To install R Services successfully, you must uninstall any previous versions of [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] and its related R components. This includes SQL Server 2016 CTP3, CTP3.1, CTP3.2, RC0, or RC1.

Uninstalling a pre-release version can be complex and require running a special script. Contact technical support for assistance.

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

If you have any doubt about which version you are using, run `@@VERSION` from SQL Server Management Studio.

### Problems with setup of R Server (Standalone)

This section describes issues specific to installations of Microsoft R Server (Standalone) that use SQL Server 2016 setup. For more general issues related to R Server upgrades, see [Microsoft R Server](https://msdn.microsoft.com/microsoft-r/) on MSDN.

#### Failure to install localized versions

When you install R Server offline, pre-release versions do not allow you to use localized languages.

Generally, when the server does not have Internet access, before running setup you must download all the installation packages for R Server. Then you specify the location of the files during the installation.

However, if the language identifier associated with the installer package is not the same as the SQL Server setup language, a problem occurs. When you reach the page for the R component installation, the **Next** button is disabled and you cannot proceed with the installation. As a workaround, you can rename the package to use a matching identifier.

For example, the name of the installation packages might be `SRO_3.2.2.0_1031.cab`.
To install the 104 language on SQL Server, rename the file to `SRO_3.2.2.0_1041.cab`.

#### Installing R Services and R Server Standalone on the same computer

Generally, you do not install both R Services (In-Database) and R Server (Standalone) on the same computer. However, if the server has sufficient capacity, R Server Standalone might be useful as a development tool. You might also need to use the operationalization features of R Server, and access SQL Server data from R Server without data movement.

Note that if you install both R Server and R Services on the same computer, two separate sets of the same R libraries are installed. One is for use by the SQL Server instance, and one is for development use or use by R Server.

In earlier versions of SQL Server 2016, installing both R Server (Standalone) and R Services (In-Database) at the same time could cause setup to fail with an “access denied” message. This issue was fixed in Service Pack 1 for SQL Server 2016.

If you encountered this error, and need to upgrade these features, perform a slipstream installation of SQL Server 2016 with SP1. There are two ways to resolve the issue, both of which require uninstalling and reinstalling.

1. Uninstall R Services (In-Database), and make sure the user accounts for SQLRUserGroup are removed.

2. Restart the server, and then reinstall R Server (Standalone).

3. Run SQL Server setup once more, and this time select **Add Features to Existing SQL Server**.

4. Choose the instance, and then select the **R Services (In-Database)** option to add.

In some cases, this procedure fails to resolve the problem. Try the following workaround:

1. Uninstall R Services (In-Database) and R Server (Standalone) at the same time.

2. Remove the local user accounts (SQLRUserGroup).

3. Restart the server.

4. Run SQL Server setup, and add the R Services (In-Database) feature only. Do not select **R Server (Standalone)**.

## See also

 [Getting started with SQL Server R Services](../r/getting-started-with-sql-server-r-services.md)

 [Getting started with Microsoft R Server Standalone](../r/getting-started-with-microsoft-r-server-standalone.md)
