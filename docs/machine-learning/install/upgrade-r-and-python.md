---
title: Upgrade Python and R runtimes (binding)
description: Upgrade Python and R runtimes in SQL Server Machine Learning Services or SQL Server R Services using sqlbindr.exe to bind to Machine Learning Server.
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 09/30/2020
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=sql-server-2016||=sql-server-2017"
---
# Upgrade Python and R runtime with binding in SQL Server Machine Learning Services
[!INCLUDE [SQL Server 2016 and 2017](../../includes/applies-to-version/sqlserver2016-2017-only.md)]

[!INCLUDE [retirement banner](~/includes/machine-learning-server-retirement.md)]

This article describes how to use am installation process called **binding** to upgrade the R or Python runtimes in [SQL Server 2016 R Services](../r/sql-server-r-services.md) or [SQL Server 2017 Machine Learning Services](../sql-server-machine-learning-services.md). You can get [newer versions of Python and R](#version-map) by *binding* to [Microsoft Machine Learning Server](/machine-learning-server).

> [!IMPORTANT]
> This article describes an old method for upgrading the R and Python runtimes, called *binding*. If you have installed **Cumulative Update (CU) 14 or later for SQL Server 2016 Services Pack (SP) 2** or **Cumulative Update (CU) 22 or later for SQL Server 2017**, see how to [change the default R or Python language runtime to a later version](change-default-language-runtime-version.md) instead.

## What is binding?

Binding is an installation process that replaces the contents of your **R_SERVICES** and **PYTHON_SERVICES** folders with newer executables, libraries, and tools from [Microsoft Machine Learning Server](/machine-learning-server).

The uploaded components included with the servicing model has changed. The service updates match the [support Timeline for Microsoft R Server & Machine Learning Server](/machine-learning-server/resources-servicing-support) on the [Modern Lifecycle](https://support.microsoft.com/help/30881/modern-lifecycle-policy).

Except for component versions and service updates, binding doesn't change the basics of your installation:

- Python and R integration is still part of a database engine instance.
- Licensing is unchanged (no additional costs associated with binding).
- SQL Server support policies still hold for the database engine.

The rest of this article explains the binding mechanism and how it works for each version of SQL Server.

> [!NOTE]
> Binding applies to in-database instances only that are bound to SQL Server instances. In this case binding is not necessary for a Standalone installation.

::: moniker range="=sql-server-2016"
**SQL Server 2016 binding considerations**

For SQL Server 2016 R Services customers, binding provides:

- Updated R packages.
- New packages not part of the original installation ([MicrosoftML](../r/ref-r-microsoftml.md))
- Pre-trained machine learning [models](/machine-learning-server/install/microsoftml-install-pretrained-models) for sentiment analysis and image detection.

All of the binding can further be refreshed at each new major and minor release of [Microsoft Machine Learning Server](/machine-learning-server/index).
::: moniker-end

## Version map

The following tables are version maps. Each map shows package versions across releases. You can review upgrade paths when you bind to Microsoft Machine Learning Server (previously known as R Server, before the addition of Python support starting in Machine Learning Server 9.2.1).

The binding doesn't guarantee the latest version of R or Anaconda. When you bind to Microsoft Machine Learning Server, you get the R or Python version installed through Setup, which may not be the latest version available on the web.

::: moniker range="=sql-server-2016"
[**SQL Server 2016 R Services**](../install/sql-r-services-windows-install.md)

Component |Initial Release | [R Server 9.0.1](/machine-learning-server/install/r-server-install-windows) | [R Server 9.1](/machine-learning-server/install/r-server-install-windows) | [Machine Learning Server 9.2.1](/machine-learning-server/install/machine-learning-server-windows-install) | [Machine Learning Server 9.3](/machine-learning-server/install/machine-learning-server-windows-install) |  [Machine Learning Server 9.4.7](/machine-learning-server/install/machine-learning-server-windows-install)
----------|----------------|----------------|--------------|---------|-------|-------|
Microsoft R Open (MRO) over R | R 3.2.2     | R 3.3.2   |R 3.3.3   | R 3.4.1  | R 3.4.3 | R 3.5.2
[RevoScaleR](/machine-learning-server/r-reference/revoscaler/revoscaler) | 8.0.3  | 9.0.1 |  9.1 |  9.2.1 |  9.3 |  9.4.7 |
[MicrosoftML](../r/ref-r-microsoftml.md)| n.a. | 9.0.1 |  9.1 |  9.2.1 |  9.3 | 9.4.7 |
[pretrained models](/machine-learning-server/install/microsoftml-install-pretrained-models)| n.a. | 9.0.1 |  9.1 |  9.2.1 |  9.3 | 9.4.7 |
[sqlrutils](../r/ref-r-sqlrutils.md)| n.a. | 1.0 |  1.0 |  1.0 |  1.0 | 1.0 |
[olapR](../r/ref-r-olapr.md) | n.a. | 1.0 |  1.0 |  1.0 |  1.0 | 1.0 |
::: moniker-end

::: moniker range="=sql-server-2017"
[**SQL Server 2017 Machine Learning Services**](../install/sql-machine-learning-services-windows-install.md)

Component |Initial Release | Machine Learning Server 9.3 | Machine Learning Server 9.4.7 |
----------|----------------|---------|---------|
Microsoft R Open (MRO) over R | R 3.3.3 | R 3.4.3 | R 3.5.2 |
[RevoScaleR](/machine-learning-server/r-reference/revoscaler/revoscaler) |   9.2 |  9.3 | 9.4.7 |
[MicrosoftML](../r/ref-r-microsoftml.md) | 9.2  | 9.3| 9.4.7 |
[sqlrutils](../r/ref-r-sqlrutils.md)| 1.0 |  1.0 | 1.0 |
[olapR](../r/ref-r-olapr.md) | 1.0 |  1.0 | 1.0 |
Anaconda 4.2 over Python 3.5  | 4.2/3.5.2 | 4.2/3.5.2 |
[revoscalepy](/machine-learning-server/python-reference/revoscalepy/revoscalepy-package) | 9.2  | 9.3| 9.4.7 |
[microsoftml](../python/ref-py-microsoftml.md) | 9.2  | 9.3| 9.4.7 |
[pretrained models](/machine-learning-server/install/microsoftml-install-pretrained-models) | 9.2 | 9.3| 9.4.7 |
::: moniker-end

## How component upgrade works

Executable files, Python, and R libraries are upgraded when you bind an existing installation of Python and R to Machine Learning Server.

Binding is executed by the [Microsoft Machine Learning Server installer](/machine-learning-server/install/machine-learning-server-windows-install) when you run Setup on an existing SQL Server database engine instance having Python or R integration. 

Setup detects the existing features and prompts you to rebind to Machine Learning Server.

During binding, the contents of `C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES` and `\PYTHON_SERVICES` is overwritten with the newer executable files and libraries of `C:\Program Files\Microsoft\ML Server\R_SERVER` and `\PYTHON_SERVER`.

Binding applies to Python and R features only. Open-source packages for Python and R consists of:

- Anaconda
- Microsoft R Open
- Proprietary packages RevoScaleR
- Revoscalepy

The binding doesn't change the support model for the database engine instance or the version of SQL Server.

Binding is reversible. You can revert to SQL Server servicing by [unbinding the instance](#bkmk_Unbind) and reparing your SQL Server database engine instance.

<a name="bkmk_BindWizard"></a>

## Bind to Machine Learning Server using Setup

Follow the steps to bind SQL Server to Microsoft Machine Learning Server using setup. 

1. In SSMS, run `SELECT @@version` to verify the server meets minimum build requirements.

   For SQL Server 2016 R Services, the minimum is [Service Pack 1](https://www.microsoft.com/download/details.aspx?id=54276) and [CU3](https://support.microsoft.com/help/4019916/cumulative-update-3-for-sql-server-2016-sp1).

1. Check the version of R base and RevoScaleR packages to confirm the existing versions are lower than what you plan to replace them with. 

    ```sql
    EXECUTE sp_execute_external_script
    @language=N'R'
    ,@script = N'str(OutputDataSet);
    packagematrix <- installed.packages();
    Name <- packagematrix[,1];
    Version <- packagematrix[,3];
    OutputDataSet <- data.frame(Name, Version);'
    , @input_data_1 = N''
    WITH RESULT SETS ((PackageName nvarchar(250), PackageVersion nvarchar(max) ))
    ```

1. Close SSMS and any other tools having an open connection to SQL Server. Binding overwrites program files. If SQL Server has open sessions, binding will fail with bind error code 6.

1. Download Microsoft Machine Learning Server onto the computer that has the instance you want to upgrade. We recommend the [latest version](/machine-learning-server/install/machine-learning-server-windows-install#download-machine-learning-server-installer).

1. Unzip the folder and start ServerSetup.exe, located under MLSWIN93.

1. On **Configure the installation**, confirm the components to upgrade, and review the list of compatible instances.

1. On the **License agreement** page, select **I accept these terms** to accept the licensing terms for Machine Learning Server. 

1. On successive pages, provide consent to additional licensing conditions for any open-source components you selected, such as Microsoft R Open or the Python Anaconda distribution.

1. On the **Almost there** page, make a note of the installation folder. The default folder is \Program Files\Microsoft\ML Server.

    If you want to change the installation folder, select **Advanced** to return to the first page of the wizard. However, you must repeat all previous selections.

If upgrade fails, check [SqlBindR error codes](#sqlbindr-error-codes) for more information.

## Offline binding (no internet access)

For systems with no internet connectivity, you can download the installer and .cab files to an internet-connected machine, and then transfer files to the isolated server.

The installer (ServerSetup.exe) includes the Microsoft packages (RevoScaleR, MicrosoftML, olapR, sqlRUtils). The .cab files provide other core components. For example, the "SRO" cab provides R Open, Microsoft's distribution of open-source R.

The following instructions explain how to place the files for an offline installation.

1. Download the MLSWIN93 Installer. It downloads as a single zipped file. We recommend the [latest version](/machine-learning-server/install/machine-learning-server-windows-install#download-machine-learning-server-installer), but you can also install [earlier versions](/machine-learning-server/install/r-server-install-windows-offline#download-required-components).

1. Download .cab files. The following links are for the 9.3 release. If you require earlier versions, additional links can be found in [R Server 9.1](/machine-learning-server/install/r-server-install-windows-offline#download-required-components). Recall that Python/Anaconda can only be added to a SQL Server Machine Learning Services instance. Pre-trained models exist for both Python and R; the .cab provides models in the languages you're using.

    | Feature | Download |
    |---------|----------|
    | R       | [SRO_3.4.3.0_1033.cab](https://go.microsoft.com/fwlink/?LinkId=867186&clcid=1033) |
    | Python  | [SPO_9.3.0.0_1033.cab](https://go.microsoft.com/fwlink/?LinkId=859054) | 
    | Pre-trained models | [MLM_9.3.0.0_1033.cab](https://go.microsoft.com/fwlink/?LinkId=859053) |

1. Transfer .zip and .cab files to the target server.

1. On the server, type `%temp%` in the Run command to get the physical location of the temp directory. The physical path varies by machine, but it's usually `C:\Users\<your-user-name>\AppData\Local\Temp`.

1. Place the .cab files in the %temp% folder.

1. Unzip the Installer.

1. Run ServerSetup.exe and follow the on-screen prompts to complete the installation.

## <a name="bkmk_BindCmd"></a>Command-line operations


> [!TIP]
> Can't find SqlBindR? You probably have not run Setup.
> SqlBindR is available only after running Machine Learning Server Setup.

1. Open a command prompt as administrator and navigate to the folder containing sqlbindr.exe. The default location is C:\Program Files\Microsoft\MLServer\Setup

2. Type the following command to view a list of available instances:
   `SqlBindR.exe /list`
  
   Make a note of the full instance name as listed. For example, the instance name might be MSSQL14.MSSQLSERVER for a default instance, or something like SERVERNAME.MYNAMEDINSTANCE.

3. Run **SqlBindR.exe** command with the */bind* argument. Specify the name of the instance to upgrade, using the instance name that was returned in the previous step.

   For example, to upgrade the default instance, type:
    `SqlBindR.exe /bind MSSQL14.MSSQLSERVER`

4. When the upgrade has completed, restart the Launchpad service associated with any instance that has been modified.

## <a name="bkmk_Unbind"></a>Revert or unbind an instance

You can restore a bound instance to an initial installation of the Python and R components, established by SQL Server Setup. There are three parts to reverting back to the SQL Server servicing.

+ [Step 1: Unbind from Microsoft Machine Learning Server](#step-1-unbind)
+ [Step 2: Restore the instance to original status](#step-2-restore)
+ [Step 3: Reinstall any packages you added to the installation](#step-3-reinstall-packages)

<a name="step-1-unbind"></a> 

### Step 1: Unbind

You have two options for rolling back the binding: re-rerun setup or use SqlBindR command-line utility.

#### <a name="bkmk_wizunbind"></a> Unbind using Setup

1. Locate the installer for Machine Learning Server. If you have removed the installer, you may need to download it again, or copy it from another computer.
2. Be sure to run the installer on the computer that has the instance you want to unbind.
2. The installer identifies local instances that are candidates for unbinding.
3. Deselect the check box next to the instance that you want to revert to the original configuration.
4. Accept all licensing agreements.
5. Select **Finish**. The process takes a while.

#### <a name="bkmk_cmdunbind"></a> Unbind using the command line

1. Open a command prompt and navigate to the folder that contains **sqlbindr.exe**, as described in the previous section.

2. Run the **SqlBindR.exe** command with the */unbind* argument, and specify the instance.

   For example, the following command reverts the default instance:
   
    `SqlBindR.exe /unbind MSSQL14.MSSQLSERVER`

<a name="step-2-restore"></a> 

###  Step 2: Repair the SQL Server instance

Run SQL Server Setup to repair the database engine instance having the Python and R features. Pre-existing updates are preserved. The next step applies if an update was missed for the servicing updates to Python and R packages.

Alternate solution: Fully uninstall and reinstall the database engine instance, and then apply all service updates.

<a name="step-3-reinstall-packages"></a> 

### Step 3: Add any third-party packages

You might have added other open-source or third-party packages to your package library. Since reversing the binding switches the location of the default package library, you must reinstall the packages to the library that Python and R are now using. For more information, see [R package information](../package-management/r-package-information.md) and [installation](../package-management/install-additional-r-packages-on-sql-server.md), and [Python package information](../package-management/python-package-information.md) and [installation](../package-management/install-additional-python-packages-on-sql-server.md).

## SqlBindR.exe command syntax

### Usage

`sqlbindr [/list] [/bind <SQL_instance_ID>] [/unbind <SQL_instance_ID>]`

### Parameters

|Name|Description|
|------|------|
|*list*| Displays a list of all SQL Server instance IDs on the current computer|
|*bind*| Upgrades the specified SQL Server instance to the latest version of R Server and ensures the instance automatically gets future upgrades of R Server|
|*unbind*|Uninstalls the latest version of R Server from the specified SQL Server instance and prevents future R Server upgrades from affecting the instance|

<a name="sqlbindr-error-codes"><a/>

## Binding errors

Machine Learning Server Installer and SqlBindR both return the following error codes and messages.

|Error code  | Message           | Details               |
|------------|-------------------|-----------------------|
|Bind error 0 | Ok (success) | Binding passed with no errors. |
|Bind error 1 | Invalid arguments | Syntax error. |
|Bind error 2 | Invalid action | Syntax error. |
|Bind error 3 | Invalid instance | An instance exists, but isn't valid for binding. |
|Bind error 4 | Not bindable | |
|Bind error 5 | Already bound | You ran the *bind* command, but the specified instance is already bound. |
|Bind error 6 | Bind failed | An error occurred while unbinding the instance. This error can occur if you run the Machine Learning Server installer without selecting any features. Binding requires that you select both an MSSQL instance and Python and R, assuming the instance is SQL Server 2017. This error also occurs if SqlBindR couldn't write to the Program Files folder. Open sessions or handles to SQL Server will cause this error to occur. If you get this error, reboot the computer and redo the binding steps before starting any new sessions.|
|Bind error 7 | Not bound | The database engine instance has R Services or SQL Server Machine Learning Services. The instance isn't bound to Microsoft Machine Learning Server. |
|Bind error 8 | Unbind failed | An error occurred while unbinding the instance. |
|Bind error 9 | No instances found | No database engine instances were found on this computer. |

## Known issues

This section lists known issues specific to use of the SqlBindR.exe utility, or to upgrades of Machine Learning Server that might affect SQL Server instances.

### Restoring packages that were previously installed

SqlBindR.exe fails to restore original packages or R components with upgrade to Microsoft R Server 9.0.1. Use SQL Server repair on instance and apply all service releases. Restart instance.

Later version of SqlBindR automatically restores the original R features, eliminating the need for reinstallation of R components or repatch the server. However, you must install any R package updates that might have been added after the initial installation.

Use R commands to synchronize installed packages to the file system using records in the database. For more information, see [R package management for SQL Server](../package-management/install-additional-r-packages-on-sql-server.md).

### Problems with overwritten sqlbinr.ini file in SQL Server

Scenario:
This issue occurs when binding Machine Learning Server 9.4.7 to SQL Server 2017.  When Python is updated and bound or when you update to a new CU, it doesn't understand that Python is bound, and overwrites files. There isn't a known issue with R.

As a workaround, create a `sqlbindr.ini` file in the PYTHON_SERVICES directory that isn't empty. The contents doesn't impact how the file functions.

Create a `sqlbindr.ini` file, containing **9.4.7.82**, save to this location:  

`C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES`

### Problems with multiple upgrades from SQL Server

Scenario:
Previously upgraded instance of SQL Server 2016 R Services to 9.0.1. Executed the new installer for Microsoft R Server 9.1.0. The installer displays a list of all valid instances.
By default installer selects previously bound instances. If you continue, the previously bound instances are unbound. The result is the earlier 9.0.1 installation is removed and any related packages, but the new version of Microsoft R Server (9.1.0) isn't installed.

As a workaround, you can modify the existing R Server installation as follows:
1. In Control Panel, open **Add or Remove Programs**.
2. Locate Microsoft R Server, and select **Change/Modify**.
3. When the installer starts, select the instances you want to bind to 9.1.0.

Microsoft Machine Learning Server 9.2.1 and 9.3 don't have this issue.

### Binding or unbinding leaves multiple temporary folders

Remove temporary folders after installation is complete.

> [!NOTE]
> Be sure to wait until installation is complete. It can take a long time to remove R libraries associated with one version and then add the new R libraries. When the operation completes, temporary folders are removed.

## See also

+ [Change the default R or Python language runtime version](./change-default-language-runtime-version.md)
+ [Install Machine Learning Server for Windows (Internet connected)](/machine-learning-server/install/machine-learning-server-windows-install)
+ [Install Machine Learning Server for Windows (offline)](/machine-learning-server/install/machine-learning-server-windows-offline)
+ [Known issues in Machine Learning Server](/machine-learning-server/resources-known-issues)
+ [Feature announcements from previous release of R Server](/r-server/whats-new-in-r-server)
+ [Deprecated, no longer supported, or changed features](/machine-learning-server/resources-deprecated-features)