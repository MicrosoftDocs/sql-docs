---
title: Install R Server or Machine Learning Server (Standalone) using SQL Server Setup - SQL Server Machine Learning
description: Setup a non-instance-aware standalone machine learning server for R and Python development using RevoScaleR, revoscalepy, MicrosoftML and other packages.
ms.prod: sql
ms.technology: machine-learning

ms.date: 08/28/2018 
ms.topic: conceptual
ms.author: dphansen
ms.author: davidph
manager: cgronlun
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
---
# Install Machine Learning Server (Standalone) or R Server (Standalone) using SQL Server Setup
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

SQL Server Setup includes a **shared feature** option for installing a non-instance-aware, standalone machine learning server that runs outside of SQL Server. In SQL Server 2016, this feature is called **R Server (Standalone)**. In SQL Server 2017, it's called **Machine Learning Server (Standalone)** and includes R and Python. 

A standalone server as installed by SQL Server Setup is functionally equivalent to the non-SQL-branded versions of [Microsoft Machine Learning Server](https://docs.microsoft.com/machine-learning-server/what-is-machine-learning-server), supporting the same use cases and scenarios, including:

+ Remote execution, switching between local and remote sessions in the same console
+ Operationalization with web nodes and compute nodes
+ Web service deployment: the ability to package R and Python script into web services
+ Complete collection of R and Python function libraries

As an independent server decoupled from SQL Server, the R and Python environment is configured, secured, and accessed using the underlying operating system and tools provided in the standalone server, not SQL Server.

As an adjunct to SQL Server, a standalone server is useful if you need to develop high-performance machine learning solutions that can use remote compute contexts to the full range of supported data platforms. You can shift execution from the local server to a remote Machine Learning Server on a Spark cluster or on another SQL Server instance.

<a name="bkmk_prereqs"> </a>

## Pre-install checklist

If you installed a previous version, such as SQL Server 2016 R Server (Standalone) or Microsoft R Server, uninstall the existing installation before continuing.

As a general rule, we recommend that you treat standalone server and database engine instance-aware installations as mutually exclusive to avoid resource contention, but if you have sufficient resources, there is no prohibition against installing them both on the same physical computer.

You can only have one standalone server on the computer: either SQL Server 2017 Machine Learning Server or SQL Server 2016 R Server (Standalone). Be sure to uninstall one version before adding a new one.

::: moniker range="=sql-server-2016"
<a name="bkmk_ga_instalpatch"></a> 

 ###  Install patch requirement 

For SQL Server 2016 only: Microsoft has identified a problem with the specific version of Microsoft VC++ 2013 Runtime binaries that are installed as a prerequisite by SQL Server. If this update to the VC runtime binaries is not installed, SQL Server may experience stability issues in certain scenarios. Before you install SQL Server follow the instructions at [SQL Server Release Notes](../../sql-server/sql-server-2016-release-notes.md#bkmk_ga_instalpatch) to see if your computer requires a patch for the VC runtime binaries.  
::: moniker-end

## Get the installation media

[!INCLUDE[GetInstallationMedia](../../includes/getssmedia.md)]

::: moniker range=">=sql-server-2017||=sqlallproducts-allversions"
## Run Setup

For local installations, you must run Setup as an administrator. If you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read and execute permissions on the remote share.

1. Start the installation wizard.

2. Click the **Installation** tab, and select **New Machine Learning Server (Standalone) installation**.
    
     ![Install Machine Learning Server Standalone](media/2017setup-installation-page-mlsvr.png "Start installation of Machine Learning Server Standalone")

3. After the rules check is complete, accept SQL Server licensing terms, and select a new installation.

4. On the **Feature Selection** page, the following options should already be selected:

    - Microsoft Machine Learning Server (Standalone)

    - R and Python are both selected by default. You can deselect either language, but we recommend that you install at least one of the supported languages.

     ![Choose R or Python features](media/2017setup-features-page-mlsvr-rpy.png "Start installation of Machine Learning Server Standalone")
    
    All other options should be ignored. 
    
    > [!NOTE]
    > Avoid installing the **Shared Features** if the computer already has Machine Learning Services installed for SQL Server in-database analytics. This creates duplicate libraries.
    > 
    > Also, whereas R or Python scripts running in SQL Server are managed by SQL Server so as not to conflict with memory used by other database engine services, the standalone machine learning server has no such constraints, and can interfere with other database operations. Finally, remote access via RDP session, which is often used for operationalization, is typically blocked by database administrators.
    > 
    > For these reasons, we generally recommend that you install Machine Learning Server (Standalone) on a separate computer from SQL Server Machine Learning Services.

5.  Accept the license terms for downloading and installing base language distributions. When the **Accept** button becomes unavailable, you can click **Next**. 

     ![Python license agreement](media/2017setup-python-license.png "Python license agreement")

6.  On the **Ready to Install** page, verify your selections, and click **Install**.

When installation is finished, see [Custom reports for SQL Server R Services](../r/monitor-r-services-using-custom-reports-in-management-studio.md)
For help with any errors or warnings, see [Upgrade and installation FAQ - Machine Learning Services](../r/upgrade-and-installation-faq-sql-server-r-services.md).
::: moniker-end

::: moniker range="=sql-server-2016"
## Run Setup

For local installations, you must run Setup as an administrator. If you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read and execute permissions on the remote share.

1. Start the installation wizard.

2. On the **Installation** tab, click **New R Server (Standalone) installation**.
    
     ![Start setup of R Server Standalone](media/2016-setup-installation-rsvr.png "Start setup of R Server Standalone")

3. After the rules check is complete, accept SQL Server licensing terms, and select a new installation.

4.  On the **Feature Selection** page, the following option should be already selected:
    
    **R Server (Standalone)**  
    
    ![Feature selections for R Server Standalone](media/2016setup-rserver-features.png "Feature selections for R Server Standalone")
    
    All other options can be ignored. 
    
    > [!NOTE]
    > Avoid installing the **Shared Features** if you are running setup on a computer where R Services has already been installed for SQL Server in-database analytics. This creates duplicate libraries.
    > 
    > Whereas R scripts running in SQL Server are managed by SQL Server so as not to conflict with memory used by other database engine services, the standalone R Server has no such constraints, and can interfere with other database operations.
    > 
    > We generally recommend that you install R Server (Standalone) on a separate computer from SQL Server R Services (In-Database).

5.  Accept the license terms for downloading and installing base language distributions. When the **Accept** button becomes unavailable, you can click **Next**. 

6.  On the **Ready to Install** page, verify your selections, and click **Install**.

When installation is finished, see [Custom reports for SQL Server R Services](../r/monitor-r-services-using-custom-reports-in-management-studio.md)
For help with any errors or warnings, see [Upgrade and installation FAQ - Machine Learning Services](../r/upgrade-and-installation-faq-sql-server-r-services.md).
::: moniker-end

## Set environment variables

For R feature integration only, you should set the **MKL_CBWR** environment variable to [ensure consistent output](https://software.intel.com/articles/introduction-to-the-conditional-numerical-reproducibility-cnr) from Intel Math Kernel Library (MKL) calculations.

1. In Control Panel, click **System and Security** > **System** > **Advanced System Settings** > **Environment Variables**.

2. Create a new User or System variable. 

  + Set variable name to `MKL_CBWR`
  + Set the variable value to `AUTO`

3. Restart the server.

<a name="install-path"></a>

### Default installation folders

For R and Python development, it's common to have multiple versions on the same computer. As installed by SQL Server setup, the base distribution is installed in a folder associated with the SQL Server version that you used for setup.

The following table lists the paths for R and Python distributions created by Microsoft installers. For completeness, the table includes paths generated by SQL Server setup as well as the standalone installer for Microsoft Machine Learning Server.

|Version| Installation method | Default folder|
|----|----|----|
|SQL Server 2017 Machine Learning Server (Standalone) |  SQL Server 2017 setup wizard |`C:\Program Files\Microsoft SQL Server\140\R_SERVER` <br/>`C:\Program Files\Microsoft SQL Server\140\PYTHON_SERVER`|
|Microsoft Machine Learning Server (Standalone) |  Windows standalone installer |`C:\Program Files\Microsoft\ML Server\R_SERVER`<br/>`C:\Program Files\Microsoft\ML Server\PYTHON_SERVER`|
|SQL Server 2017 Machine Learning Services (In-Database) |SQL Server 2017 setup wizard, with R language option|`C:\Program Files\Microsoft SQL Server\MSSQL14.<instance_name>\R_SERVICES`  <br/>`C:\Program Files\Microsoft SQL Server\MSSQL14.<instance_name>\PYTHON_SERVICES` |
|SQL Server 2016 R Server (Standalone) |  SQL Server 2016 setup wizard |`C:\Program Files\Microsoft SQL Server\130\R_SERVER`|
|SQL Server 2016 R Services (In-Database) |SQL Server 2016 setup wizard|`C:\Program Files\Microsoft SQL Server\MSSQL13.<instance_name>\R_SERVICES`|

<a name="apply-cu"></a>

## Apply updates

We recommend that you apply the latest cumulative update to both the database engine and machine learning components. Cumulative updates are installed through the Setup program. 

On internet-connected devices, you can download a self-extracting executable. Applying an update for the database engine automatically pulls in cumulative updates for existing R and Python features. 

On disconnected servers, extra steps are required. You must obtain the cumulative update for the database engine as well as the CAB files for machine learning features. All files must be transferred to the isolated server and applied manually.

1. Start with a baseline instance. You can only apply cumulative updates to existing installations:

  + Machine Learning Server (Standalone) from SQL Server 2017 initial release
  + R Server (Standalone) from SQL Server 2016 initial release, SQL Server 2016 SP 1, or SQL Server 2016 SP 2

2. Close any open R or Python sessions and stop any processes still running on the system.

3. If you enabled operationalization to run as web nodes and compute nodes for web service deployments, back up the **AppSettings.json** file as a precaution. Applying SQL Server 2017 CU13 or later revises this file, so you might want a backup copy to preserve the original version.

4. On an internet connected device, click the cumulative update link for your version of SQL Server.

  + [SQL Server 2017 updates](https://sqlserverupdates.com/sql-server-2017-updates/)
  + [SQL Server 2016 updates](https://sqlserverupdates.com/sql-server-2016-updates/)

5. Download the latest cumulative update. It is an executable file.

6. On an internet-connected device, double-click the .exe to run Setup and step through the wizard to accept licensing terms, review affected features, and monitor progress until completion.

7. On a server with no internet connectivity:

   + Get corresponding CAB files for R and Python. For download links, see [CAB downloads for cumulative updates on SQL Server in-database analytics instances](sql-ml-cab-downloads.md).

   + Transfer all files, the main executable and CAB files, to a folder on the offline computer.

   + Double-click the .exe to run Setup. When installing a cumulate update on a server with no internet connectivity, you are prompted to select the location of the .cab files for R and Python.

8. Post-install, on a server for which you have enabled operationalization with web nodes and compute nodes, edit **AppSettings.json**, adding an "MMLResourcePath" entry, directly under "MMLNativePath":

    ```json
    "ScorerParameters": {
        "MMLNativePath": "C:\Program Files\Microsoft SQL Server\140\R_SERVER\library\MicrosoftML\mxLibs\x64\",
        "MMLResourcePath": "C:\Program Files\Microsoft SQL Server\140\R_SERVER\library\MicrosoftML\mxLibs\x64\"
    }
    ```

9. [Run the admin CLI utility](https://docs.microsoft.com/machine-learning-server/operationalize/configure-admin-cli-launch) to restart the web and compute nodes. For steps and syntax, see [Monitor, start, and stop web and compute nodes](https://docs.microsoft.com/machine-learning-server/operationalize/configure-admin-cli-stop-start).

## Development tools

A development IDE is not installed as part of setup. For more information about configuring a development environment, see [Set up R tools](../r/set-up-a-data-science-client.md) and [Set up Python tools](../python/setup-python-client-tools-sql.md).

## Next steps

R developers can get started with some simple examples, and learn the basics of how R works with SQL Server. For your next step, see the following links:

+ [Tutorial: Run R in T-SQL](../tutorials/rtsql-using-r-code-in-transact-sql-quickstart.md)
+ [Tutorial: In-database analytics for R developers](../tutorials/sqldev-in-database-r-for-sql-developers.md)

::: moniker range=">=sql-server-2017||=sqlallproducts-allversions"
Python developers can learn how to use Python with SQL Server by following these tutorials:

+ [Tutorial: Run Python in T-SQL](../tutorials/run-python-using-t-sql.md)
+ [Tutorial: In-database analytics for Python developers](../tutorials/sqldev-in-database-python-for-sql-developers.md)
::: moniker-end

To view examples of machine learning that are based on real-world scenarios, see [Machine learning tutorials](../tutorials/machine-learning-services-tutorials.md).
