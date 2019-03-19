---
title: Upgrade R and Python components - SQL Server Machine Learning Services
description: Upgrade R and Python in SQL Server 2016 Services or SQL Server 2017 Machine Learning Services using sqlbindr.exe to bind to Machine Learning Server.
ms.prod: sql
ms.technology: machine-learning

ms.date: 09/30/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Upgrade machine learning (R and Python) components in SQL Server instances
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

R and Python integration in SQL Server includes open-source and Microsoft-proprietary packages. Under standard SQL Server servicing, packages are updated according to the SQL Server release cycle, with bug fixes to existing packages at the current version, but no major version upgrades. 

However, many data scientists are accustomed to working with newer packages as they become available. For both SQL Server 2017 Machine Learning Services (In-Database) and SQL Server 2016 R Services (In-Database), you can get [newer versions of R and Python](#version-map) by *binding* to **Microsoft Machine Learning Server**. 

## What is binding?

Binding is an installation process that swaps out the contents of your R_SERVICES and PYTHON_SERVICES folders with newer executables, libraries, and tools from [Microsoft Machine Learning Server](https://docs.microsoft.com/machine-learning-server/index).

Along with updated components comes a switch in servicing models. Instead of the [SQL Server product lifecycle](https://support.microsoft.com/lifecycle/search?alpha=SQL%20Server%202017), with [SQL Server cumulative updates](https://support.microsoft.com/help/4047329/sql-server-2017-build-versions), your service updates now conform to the [support Timeline for Microsoft R Server & Machine Learning Server](https://docs.microsoft.com/machine-learning-server/resources-servicing-support) on the [Modern Lifecycle](https://support.microsoft.com/help/30881/modern-lifecycle-policy).

Except for component versions and service updates, binding does not change the fundamentals of your installation: R and Python integration is still part of a database engine instance, licensing is unchanged (no additional costs associated with binding), and SQL Server support policies still hold for the database engine. The rest of this article explains the binding mechanism and how it works for each version of SQL Server.

> [!NOTE]
> Binding applies to (In-Database) instances only that are bound to SQL Server instances. Binding is not relevant for a (Standalone) installation.

**SQL Server 2017 binding considerations**

For SQL Server 2017 Machine Learning Services, you would consider binding only when Microsoft Machine Learning Server begins to offer additional packages or newer versions over what you already have.

**SQL Server 2016 binding considerations**

For SQL Server 2016 R Services customers, binding provides updated R packages, new packages not part of the original installation ([MicrosoftML](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/microsoftml-package)), and [pretrained models](https://docs.microsoft.com/machine-learning-server/install/microsoftml-install-pretrained-models), all of which can further be refreshed at each new major and minor release of [Microsoft Machine Learning Server](https://docs.microsoft.com/machine-learning-server/index). Binding does not give you Python support, which is a SQL Server 2017 feature. 

<a name="version-map"></a>

## Version map

The following table is a version map, showing package versions across release vehicles so that you can ascertain potential upgrade paths when you bind to Microsoft Machine Learning Server (previously known as R Server, before the addition of Python support starting in MLS 9.2.1). 

Notice that binding does not guarantee the very latest version of R or Anaconda. When you bind to Microsoft Machine Learning Server (MLS), you get the R or Python version installed through Setup, which may not be the latest version available on the web.

[**SQL Server 2016 R Services**](../install/sql-r-services-windows-install.md)

Component |Initial Release | [R Server 9.0.1](https://docs.microsoft.com/machine-learning-server/install/r-server-install-windows) | [R Server 9.1](https://docs.microsoft.com/machine-learning-server/install/r-server-install-windows) | [MLS 9.2.1](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-install) | [MLS 9.3](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-install) |
----------|----------------|----------------|--------------|---------|-------|
Microsoft R Open (MRO) over R | R 3.2.2     | R 3.3.2   |R 3.3.3   | R 3.4.1  | R 3.4.3 |
[RevoScaleR](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) | 8.0.3  | 9.0.1 |  9.1 |  9.2.1 |  9.3 |
[MicrosoftML](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/microsoftml-package)| n.a. | 9.0.1 |  9.1 |  9.2.1 |  9.3 |
[pretrained models](https://docs.microsoft.com/machine-learning-server/install/microsoftml-install-pretrained-models)| n.a. | 9.0.1 |  9.1 |  9.2.1 |  9.3 |
[sqlrutils](https://docs.microsoft.com/machine-learning-server/r-reference/sqlrutils/sqlrutils)| n.a. | 1.0 |  1.0 |  1.0 |  1.0 |
[olapR](https://docs.microsoft.com/machine-learning-server/r-reference/olapr/olapr) | n.a. | 1.0 |  1.0 |  1.0 |  1.0 |


[**SQL Server 2017 Machine Learning Services**](../install/sql-machine-learning-services-windows-install.md)

Component |Initial Release | MLS 9.3 | | | |
----------|----------------|---------|-|-|-|-|
Microsoft R Open (MRO) over R | R 3.3.3 | R 3.4.3 | | | |
[RevoScaleR](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) |   9.2 |  9.3 | | | |
[MicrosoftML](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/microsoftml-package) | 9.2  | 9.3| | | |
[sqlrutils](https://docs.microsoft.com/machine-learning-server/r-reference/sqlrutils/sqlrutils)| 1.0 |  1.0 | | | |
[olapR](https://docs.microsoft.com/machine-learning-server/r-reference/olapr/olapr) | 1.0 |  1.0 | | | |
Anaconda 4.2 over Python 3.5  | 4.2/3.5.2 | 4.2/3.5.2 | | | |
[revoscalepy](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/revoscalepy-package) | 9.2  | 9.3| | | |
 [microsoftml](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/microsoftml-package) | 9.2  | 9.3| | | |
[pretrained models](https://docs.microsoft.com/machine-learning-server/install/microsoftml-install-pretrained-models) | 9.2 | 9.3| | | |

## How component upgrade works 

R and Python libraries and executables are upgraded when you bind an existing installation of R and Python to Machine Learning Server. Binding is executed by the [Microsoft Machine Learning Server installer](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-install) when you run Setup on an existing SQL Server database engine instance, either 2016 or 2017, having R or Python integration. Setup detects the existing features and prompts you to rebind to Machine Learning Server. 

During binding, the contents of C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES and \PYTHON_SERVICES is overwritten with the newer executables and libraries of C:\Program Files\Microsoft\ML Server\R_SERVER and \PYTHON_SERVER.

At the same time, the servicing model is also flipped from SQL Server Update mechanisms, to the more frequent major and minor release cycle of Microsoft Machine Learning Server. Switching support policies is an attractive option for data science teams who require newer generation R and Python modules for their solutions. 

+ Without binding, R and Python packages are patched for bug fixes when you install a SQL Server service pack or cumulative update (CU). 
+ With binding, newer package versions can be applied to your instance, independently of the CU release schedule, under the [Modern Lifecycle Policy](https://support.microsoft.com/help/30881/modern-lifecycle-policy) and releases of Microsoft Machine Learning Server. The Modern Lifecycle support policy offers more frequent updates over a shorter, one-year lifespan. Post-binding, you would continue to use the MLS installer for future updates of R and Python as they become available on Microsoft download sites.

Binding applies to R and Python features only. Namely, open-source packages for R and Python features (Microsoft R Open, Anaconda), and the proprietary packages RevoScaleR, revoscalepy, and so forth. Binding does not change the support model for the database engine instance and doesn't change the version of SQL Server.

Binding is reversible. You can revert to SQL Server servicing by [unbinding the instance](#bkmk_Unbind) and reparing your SQL Server database engine instance.

Summed up, steps for binding are as follows:

+ Start with an existing, configured installation of SQL Server 2016 R Services (or SQL Server 2017 Machine Learning Services).
+ Determine which version of Microsoft Machine Learning Server has the upgraded components you want to use.
+ Download and run Setup for that version. Setup detects the existing instance, adds a binding option, and returns a list of compatible instances.
+ Choose the instance you want to bind and then finish setup to execute the binding.

In terms of user experience, the technology and how you work with it is unchanged. The only difference is the presence of newer-versioned packages and possibly additional packages not originally available through SQL Server (such as MicrosoftML for SQL Server 2016 R Services customers).

## <a name="bkmk_BindWizard"></a>Bind to MLS using Setup

Microsoft Machine Learning Setup detects the existing features and SQL Server version and invokes a utility called SqlBindR.exe to change the binding. Internally, SqlBindR is chained to Setup and used indirectly. Later, you can call SqlBindR directly from the command line to exercise specific options.

1. In SSMS, run `SELECT @@version` to verify the server meets minimum build requirements. 

   For SQL Server 2016 R Services, the minimum is [Service Pack 1](https://www.microsoft.com/download/details.aspx?id=54276) and [CU3](https://support.microsoft.com/help/4019916/cumulative-update-3-for-sql-server-2016-sp1).

1. Check the version of R base and RevoScaleR packages to confirm the existing versions are lower than what you plan to replace them with. For SQL Server 2016 R Services, R Base package is 3.2.2 and RevoScaleR is 8.0.3.

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

1. Close down SSMS and any other tools having an open connection to SQL Server. Binding overwrites program files. If SQL Server has open sessions, binding will fail with bind error code 6.

1. Download Microsoft Machine Learning Server onto the computer that has the instance you want to upgrade. We recommend the [latest version](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-install#download-machine-learning-server-installer).

1. Unzip the folder and start ServerSetup.exe, located under MLSWIN93.

   ![Microsoft Machine Learning Server setup wizard](media/mls-921-installer-start.PNG)

1. On **Configure the installation**, confirm the components to upgrade, and review the list of compatible instances. 

   This step is very important.

   On the left, choose every feature that you want to keep or upgrade. You cannot upgrade some features and not others. An empty checkbox removes that feature, assuming it is currently installed. In the screenshot, given an instance of SQL Server 2016 R Services (MSSQL13), R and the R version of the pre-trained models are selected. This configuration is valid because SQL Server 2016 supports R but not Python.

   If you are upgrading components on SQL Server 2016 R Services, do not select the Python feature. You cannot add Python to SQL Server 2016 R Services.

   On the right, select the checkbox next to the instance name. If no instances are listed, you have an incompatible combination. If you do not select an instance, a new standalone installation of Machine Learning Server is created, and the SQL Server libraries are unchanged. If you can't select an instance, it might not be at [SP1 CU3](https://support.microsoft.com/help/4019916/cumulative-update-3-for-sql-server-2016-sp1). 

    ![Configure installation step](media/mls-931-installer-mssql13.png)

1. On the **License agreement** page, select **I accept these terms** to accept the licensing terms for Machine Learning Server. 

1. On successive pages, provide consent to additional licensing conditions for any open-source components you selected, such as Microsoft R Open or the Python Anaconda distribution.

1. On the **Almost there** page, make a note of the installation folder. The default folder is \Program Files\Microsoft\ML Server.

    If you want to change the installation folder, click **Advanced** to return to the first page of the wizard. However, you must repeat all previous selections.

During the installation process, any R or Python libraries used by SQL Server are replaced and Launchpad is updated to use the newer components. As a result, if the instance previously used libraries in the default R_SERVICES folder, after upgrade these libraries are removed and the properties for the Launchpad service are changed to use the libraries in the new location.

Binding affects the contents of these folders: C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\R_SERVICES\library is replaced with the contents of C:\Program Files\Microsoft\ML Server\R_SERVER. The second folder and its contents are created by Microsoft Machine Learning Server Setup. 

If upgrade fails, check [SqlBindR error codes](#sqlbindr-error-codes) for more information.

## Confirm binding

Recheck the version of R and RevoScaleR to confirm you have newer versions. Use the R console distributed with the R packages in your database engine instance to get package information:

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

For SQL Server 2016 R Services bound to Machine Learning Server 9.3, R Base package should be 3.4.3, RevoScaleR should be  9.3, and you should also have MicrosoftML 9.3. 

If you added the pre-trained models, the models are embedded in the MicrosoftML library and you can call them through MicrosoftML functions. For more information, see [R samples for MicrosoftML](https://docs.microsoft.com/machine-learning-server/r/sample-microsoftml).

## Offline binding (no internet access)

For systems with no internet connectivity, you can download the installer and .cab files to an internet-connected machine, and then transfer files to the isolated server. 

The installer (ServerSetup.exe) includes the Microsoft packages (RevoScaleR, MicrosoftML, olapR, sqlRUtils). The .cab files provide other core components. For example, the "SRO" cab provides R Open, Microsoft's distribution of open-source R.

The following instructions explain how to place the files for an offline installation.

1. Download the MLS Installer. It downloads as a single zipped file. We recommend the [latest version](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-install#download-machine-learning-server-installer), but you can also install [earlier versions](https://docs.microsoft.com/machine-learning-server/install/r-server-install-windows-offline#download-required-components).

1. Download .cab files. The following links are for the 9.3 release. If you require earlier versions, additional links can be found in [R Server 9.1](https://docs.microsoft.com/machine-learning-server/install/r-server-install-windows-offline#download-required-components). Recall that Python/Anaconda can only be added to a SQL Server 2017 Machine Learning Services instance. Pre-trained models exist for both R and Python; the .cab provides models in the languages you are using.

    | Feature | Download |
    |---------|----------|
    | R       | [SRO_3.4.3.0_1033.cab](https://go.microsoft.com/fwlink/?LinkId=867186&clcid=1033) |
    | Python  | [SPO_9.3.0.0_1033.cab](https://go.microsoft.com/fwlink/?LinkId=859054) | 
    | Pre-trained models | [MLM_9.3.0.0_1033.cab](https://go.microsoft.com/fwlink/?LinkId=859053) |

1. Transfer .zip and .cab files to the target server.

1. On the server, type `%temp%` in the Run command to get the physical location of the temp directory. The physical path varies by machine, but it is usually `C:\Users\<your-user-name>\AppData\Local\Temp`.

1. Place the .cab files in the %temp% folder.

1. Unzip the Installer.

1. Run ServerSetup.exe and follow the on-screen prompts to complete the installation.

## <a name="bkmk_BindCmd"></a>Command line operations

After you run Microsoft Machine Learning Server, a command-line utility called SqlBindR.exe becomes available that you can use for further binding operations. For example, should you decide to reverse a binding, you could either rerun Setup or use the command-line utility. Additionally, you can use this tool to check for instance compatibility and availability.

> [!TIP]
> Can't find SqlBindR? You probably have not run Setup. 
> SqlBindR is available only after running Machine Learning Server Setup.

1. Open a command prompt as administrator and navigate to the folder containing sqlbindr.exe. The default location is C:\Program Files\Microsoft\MLServer\Setup

2. Type the following command to view a list of available instances:
   `SqlBindR.exe /list`
  
   Make a note of the full instance name as listed. For example, the instance name might be MSSQL14.MSSQLSERVER for a default instance, or something like SERVERNAME.MYNAMEDINSTANCE.

3. Run the **SqlBindR.exe** command with the */bind* argument, and specify the name of the instance to upgrade, using the instance name that was returned in the previous step.

   For example, to upgrade the default instance, type:
    `SqlBindR.exe /bind MSSQL14.MSSQLSERVER`

4. When the upgrade has completed, restart the Launchpad service associated with any instance that has been modified.

## <a name="bkmk_Unbind"></a>Revert or unbind an instance

You can restore a bound instance to an initial installation of the R and Python components, established by SQL Server Setup. There are three parts to reverting back to the SQL Server servicing.

+ [Step 1: Unbind from Microsoft Machine Learning Server](#step-1-unbind)
+ [Step 2: Restore the instance to original status](#step-2-restore)
+ [Step 3: Reinstall any packages you added to the installation](#step-3-reinstall-packages)

<a name="step-1-unbind"></a> 

### Step 1: Unbind

You have two options for rolling back the binding: re-rerun setup or use SqlBindR command line utility.

#### <a name="bkmk_wizunbind"></a> Unbind using Setup

1. Locate the installer for Machine Learning Server. If you have removed the installer, you might need to download it again, or copy it from another computer.
2. Be sure to run the installer on the computer that has the instance you want to unbind.
2. The installer identifies local instances that are candidates for unbinding.
3. Deselect the check box next to the instance that you want to revert to the original configuration.
4. Accept the licensing agreement. You must indicate your acceptance of licensing terms even when installing.
5. Click **Finish**. The process takes a while.

#### <a name="bkmk_cmdunbind"></a> Unbind using the command line

1. Open a command prompt and navigate to the folder that contains **sqlbindr.exe**, as described in the previous section.

2. Run the **SqlBindR.exe** command with the */unbind* argument, and specify the instance.

   For example, the following command reverts the default instance:
   
    `SqlBindR.exe /unbind MSSQL14.MSSQLSERVER`

<a name="step-2-restore"></a> 

###  Step 2: Repair the SQL Server instance

Run SQL Server Setup to repair the database engine instance having the R and Python features. Existing updates are preserved, but if you missed any SQL Server servicing updates to R and Python packages, this step applies those patches.

Alternatively, this is more work, but you could also fully uninstall and reinstall the database engine instance, and then apply all service updates.

<a name="step-3-reinstall-packages"></a> 

### Step 3: Add any third-party packages

You might have added other open-source or third-party packages to your package library. Since reversing the binding switches the location of the default package library, you must reinstall the packages to the library that R and Python are now using. For more information, see [Default packages](installing-and-managing-r-packages.md), [Install new R packages](install-additional-r-packages-on-sql-server.md), and [Install new Python packages](../python/install-additional-python-packages-on-sql-server.md).

## SqlBindR.exe command syntax

### Usage

`sqlbindr [/list] [/bind <SQL_instance_ID>] [/unbind <SQL_instance_ID>]`

### Parameters

|Name|Description|
|------|------|
|*list*| Displays a list of all SQL database instance IDs on the current computer|
|*bind*| Upgrades the specified SQL database instance to the latest version of R Server and ensures the instance automatically gets future upgrades of R Server|
|*unbind*|Uninstalls the latest version of R Server from the specified SQL database instance and prevents future R Server upgrades from affecting the instance|

<a name="sqlbindr-error-codes"><a/>

## Binding errors

MLS Installer and SqlBindR both return the following error codes and messages.

|Error code  | Message           | Details               |
|------------|-------------------|-----------------------|
|Bind error 0 | Ok (success) | Binding passed with no errors. |
|Bind error 1 | Invalid arguments | Syntax error. |
|Bind error 2 | Invalid action | Syntax error. |
|Bind error 3 | Invalid instance | An instance exists, but is not valid for binding. |
|Bind error 4 | Not bindable | |
|Bind error 5 | Already bound | You ran the *bind* command, but the specified instance is already bound. |
|Bind error 6 | Bind failed | An error occurred while unbinding the instance. This error can occur if you run the MLS installer without selecting any features. Binding requires that you select both an MSSQL instance and R and Python, assuming the instance is SQL Server 2017. This error also occurs if SqlBindR could not write to the Program Files folder. Open sessions or handles to SQL Server will cause this error to occur. If you get this error, reboot the computer and redo the binding steps before starting any new sessions.|
|Bind error 7 | Not bound | The database engine instance has R Services or SQL Server Machine Learning Services. The instance is not bound to Microsoft Machine Learning Server. |
|Bind error 8 | Unbind failed | An error occurred while unbinding the instance. |
|Bind error 9 | No instances found | No database engine instances were found on this computer. |

## Known issues

This section lists known issues specific to use of the SqlBindR.exe utility, or to upgrades of Machine Learning Server that might affect SQL Server instances.

### Restoring packages that were previously installed

If you upgraded to Microsoft R Server 9.0.1, the version of SqlBindR.exe for that version failed to restore the original packages or R components completely, requiring that the user run SQL Server repair on the instance, apply all service releases, and then restart the instance.

Later version of SqlBindR automatically restore the original R features, eliminating the need for reinstallation of R components or re-patch the server. However, you must install any R package updates that might have been added after the initial installation.

If you have used the package management roles to install and share package, this task is much easier: you can use R commands to synchronize installed packages to the file system using records in the database, and vice versa. For more information, see [R package management for SQL Server](install-additional-r-packages-on-sql-server.md).

### Problems with multiple upgrades from SQL Server

If you have previously upgraded an instance of SQL Server 2016 R Services to 9.0.1, when you run the new installer for Microsoft R Server 9.1.0, it displays a list of all valid instances, and then by default selects previously bound instances. If you continue, the previously bound instances are unbound. As a result, the earlier 9.0.1 installation is removed, including any related packages, but the new version of Microsoft R Server (9.1.0) is not installed.

As a workaround, you can modify the existing R Server installation as follows:
1. In Control Panel, open **Add or Remove Programs**.
2. Locate Microsoft R Server, and click **Change/Modify**.
3. When the installer starts, select the instances you want to bind to 9.1.0.

Microsoft Machine Learning Server 9.2.1 and 9.3 do not have this issue.

### Binding or unbinding leaves multiple temporary folders

Sometimes the binding and unbinding operations fail to clean up temporary folders.
If you find folders with a name like this, you can remove it after installation is complete: R_SERVICES_<guid>

> [!NOTE]
> Be sure to wait until installation is complete. It can take a long time to remove R libraries associated with one version and then add the new R libraries. When the operation completes, temporary folders are removed.

## See also

+ [Install Machine Learning Server for Windows (Internet connected)](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-install)
+ [Install Machine Learning Server for Windows (offline)](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-offline)
+ [Known issues in Machine Learning Server](https://docs.microsoft.com/machine-learning-server/resources-known-issues)
+ [Feature announcements from previous release of R Server](https://docs.microsoft.com/r-server/whats-new-in-r-server)
+ [Deprecated, discontinued or changed features](https://docs.microsoft.com/machine-learning-server/resources-deprecated-features)
