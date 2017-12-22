---
title: "Set up SQL Server Machine Learning Services (In-Database) | Microsoft Docs"
ms.custom: ""
ms.date: "11/15/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
keywords: 
  - "installing SQL Server R Services"
  - "installing SQL Server Machine Learning Services"
  - "Set up R Services"
  - "install SQL machine learning"
ms.assetid: 4d773c74-c779-4fc2-b1b6-ec4b4990950d
caps.latest.revision: 36
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "Active"
---
# Set up SQL Server Machine Learning Services (In-Database)

This topic describes how to install and configure the following machine learning features that support in-database analytics in SQL Server:

+ **SQL Server 2016 R Services (In-Database)**. If you have SQL Server 2016, install this feature to enable execution of R code in SQL Server. Requires the database engine.

    [Set up machine learning in SQL Server 2016](#bkmk_2016top)

+ **SQL Server 2017 Machine Learning Services (In-Database)**. If you have SQL Server 2017, install this to  run R (or Python) code in SQL Server. Requires the database engine. 

    [Set up machine learning in SQL Server 2017](#bkmk_2017top)

+ A machine learning server with **no** SQL Server

    [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup also includes the option to install a "standalone" version of the machine learning components that does not require the database engine, and does not run in SQL Server.  We generally recommend that you install this option on a different computer than the computer that hosts SQL Server.
    
    [Set up a standalone machine learning server](create-a-standalone-r-server.md).

This article describes the process of setup that uses the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup wizard. For command-line installation, or to download installers to use in offline servers, see these articles:

+ [Install R for SQL Server from the command line](unattended-installs-of-sql-server-r-services.md)
+ [Install Python for SQL Server from the command line](../python/unattended-installs-of-sql-server-python-services.md)
+ [Install a standalone machine learning server from the command line](install-microsoft-r-server-from-the-command-line.md)
+ [Install machine learning components on a server with no internet aces](installing-ml-components-without-internet-access.md)

**Applies to:** SQL Server 2016, SQL Server 2017

## <a name="bkmk_prereqs"> </a> Preinstallation checklist

+ Machine learning in-database requires SQL Server 2016 or later. 

+ Supported languages: 

    + SQL Server 2016 supports R only. 

    + R is also available as a preview feature in Azure SQL Database, with some limitations. For more information, see [Using R in Azure SQL Database](using-r-in-azure-sql-database.md)

    + To use Python requires SQL Server 2017 or later.

+ If you used any earlier versions of the Revolution Analytics development environment or the RevoScaleR packages, or if you installed any pre-release versions of SQL Server 2016, you must uninstall them. Side-by-side installation is not supported. For help removing previous versions, see [Upgrade and Installation FAQ for SQL Server Machine Learning Services](../r/upgrade-and-installation-faq-sql-server-r-services.md).

+ You cannot install SQL Server 2016 R Services or SQL Server 2017 Machine Learning Services on a domain controller. The R Services or Machine Learning Services portion of setup will fail.

+ You cannot install the machine learning features on a failover cluster. The security mechanism that's used for isolating external script processes is not compatible with a Windows Server failover cluster environment. As a workaround, you can do either of the following:
    * Use replication to copy necessary tables to a SQL Server instance with machine learning enabled.
    * Install machine learning on a standalone computer that uses AlwaysOn and is part of an availability group.

+ The machine learning framework requires additional configuration after setup is complete. The exact steps depend on your organization and security policies, server configuration, and intended users. We recommend that you review all steps and determine additional configuration that might be required in your environment.

## <a name="bkmk2016top"></a> Install SQL Server 2016 R Services (In-Database)

> [!div class="checklist"]
> * Install database engine and machine learning features
> * Required post-installation steps: enable machine learning, and restart
> * Optional post-installation steps: add firewall rules, add users, change or configure service accounts, set up a remote data science client

**Using the SQL Server 2016 setup wizard**

1. Run the SQL Server setup wizard.

2. On the **Installation** tab, select **New SQL Server stand-alone installation or add features to an existing installation**.

    
     ![Install R Services (In-Database)](media/2016-setup-installation-rsvcs.png "Start installation of database engine with R Services")
   
3. On the **Feature Selection** page, select the following options:

   - Select **Database Engine Services**. The database engine is required in each instance that uses machine learning.
   - Select **R Services (In-Database)**. Installs support for in-database use of R.
    
     ![R Services feature selection](media/2016setup-rsvcs-features.png "Select these features for R Services In-Database")

    > [!IMPORTANT]
    > Do not install R Server and R Services at the same time. You would ordinarily install R Server (Standalone) to create an environment that a data scientist or developer uses to connect to SQL Server and deploy R solutions. Therefore, there is no need to install both on the same computer.

4.  On the **Consent to Install Microsoft R Open** page, click **Accept**.
  
    This license agreement is required to download Microsoft R Open, which includes a distribution of the open source R base packages and tools, together with enhanced R packages and connectivity providers from the Microsoft R development team.
  
    If the computer that you're using does not have internet access, you can pause setup at this point to download the installers separately, as described in [Install R components without internet access](installing-ml-components-without-internet-access.md).
  
5. After you have accepted the license agreement, there is a brief pause while the installer is prepared. Click **Next** when the button becomes available.

6. On the **Ready to Install** page, verify that the following items are included, and then select **Install**.

   + Database Engine Services
   + R Services (In-Database)

7. When installation is complete, restart your computer.


## <a name="bkmk2017top"></a> Install SQL Server 2017 Machine Learning Services (In-Database)

> [!div class="checklist"]
> * Install database engine and machine learning features
> * Required post-installation steps: enable machine learning, and restart
> * Optional post-installation steps: add firewall rules, add users, change or configure service accounts, set up a remote data science client.

**Get started**

1. Run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup.
  
2. On the **Installation** tab, select **New SQL Server stand-alone installation or add features to an existing installation**.

     ![Install Machine Learning Services (In-Database)](media/2017setup-installation-page-mlsvcs.png "Start installation of database engine with Machine Learning Services")

3. On the **Feature Selection** page, select the following options:
   
    + Select **Database Engine Services**. The database engine is required in each instance that uses machine learning.

    + Select **Machine Learning Services (In-Database)**. This option installs support for in-database use of R. After you select this option, you can select the machine learning language. You can select only R, or you can add both R and Python.
   
    ![Machine Learning Services feature selection](media/2017setup-features-page-mls-rpy.png "Select these features for R Services In-Database")

    If you do not select either the R or Python language options, the setup wizard installs only the extensibility framework, which includes SQL Server Trusted Launchpad, and does not install any language-specific components.  Generally we recommend that you install at least one language to start with. However, you might use this option if you intend to immediately use the binding process to upgrade the machine learning components. For more information, see [Use SqlBindR to upgrade an instance of R Services](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md).

    We recommend that you **do not** install the Standalone and In-Database features on the same computer, and never install them at the same time. You would ordinarily install Machine Learning Server (Standalone) to create an environment that a data scientist or developer uses to connect to SQL Server when deploying solutions. Therefore, there is no need to install both on the same computer.

4.  License agreements for machine learning: Depending on which languages you're installing, you must accept the license agreements for R or Python, or both.

    + License terms for R: This license agreement covers Microsoft R Open, which includes a distribution of the open source R base packages and tools, together with enhanced R packages and connectivity providers from the Microsoft development team.
  
    + License terms for Python. The Python open source licensing agreement also covers Anaconda and related tools, plus some new Python libraries from the Microsoft development team.

    Click **Accept** to indicate your agreement. There is a brief pause while the components are prepared, then the **Next** button becomes available.

    If the computer that you're using does not have internet access, you can pause setup at this point to download the installers separately, as described here: [Install machine learning components without internet access](installing-ml-components-without-internet-access.md).

6. On the **Ready to Install** page, verify that the following items are included, and then select **Install**.

   - Database Engine Services
   - Machine Learning Services (In-Database)
   - R or Python, or both

7. When the installation is complete, make a note of the setup log location, and then restart your computer.

###  <a name="bkmk_enableFeature"></a>Required post-installation step

For security reasons, the machine learning feature is not enabled by default even if the feature was installed. A server administrator must enable the feature and restart the instance. 

This section describes how to reconfigure the instance for machine learning. Configuration sets up external service accounts, and starts the Launchpad service.

1. Open [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. If it is not already installed, you can rerun the SQL Server setup wizard to open a download link and install it.
  
2. Connect to the instance where you installed machine learning, and then run the following command:

   ```SQL
   sp_configure
   ```

    Look for the value of the **external scripts enabled** property, which should be **0**. That is because the feature is turned off by default to reduce the surface area.
     
3. To enable the external scripting feature, run the following statement:
  
    ```SQL
    EXEC sp_configure  'external scripts enabled', 1
    RECONFIGURE WITH OVERRIDE
    ```
  
4. Restart the SQL Server service for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. Restarting the SQL Server service also automatically restarts the related [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service.

    You can restart the service by using the **Services** panel in Control Panel or by using SQL Server Configuration Manager.

5. To verify that the external script execution service is enabled, in SQL Server Management Studio, open a new **Query** window, and then run the following command:
  
    ```SQL
    EXEC sp_configure  'external scripts enabled'
    ```
    The **run_value** should now be set to 1.
    
6. Open the **Services** panel, and verify that the Launchpad service for your instance is running. If you install multiple instances, each instance has its own Launchpad service.

7. It is a good idea to run a simple script to verify that external scripting runtimes can communicate with SQL Server. 

    Open a new **Query** window in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then run a script such as the following:
    
    + For R
    
    ```SQL
    EXEC sp_execute_external_script  @language =N'R',
    @script=N'
	OutputDataSet <- InputDataSet;
	',
    @input_data_1 =N'SELECT 1 AS hello'
    WITH RESULT SETS (([hello] int not null));
    GO
    ```

    + For Python
    
    ```SQL
    EXEC sp_execute_external_script  @language =N'Python',
    @script=N'
	OutputDataSet = InputDataSet;
	',
    @input_data_1 =N'SELECT 1 AS hello'
    WITH RESULT SETS (([hello] int not null));
    GO
    ```

    The script can take a little while to run, the first time the external script runtime is loaded. The results should be something like this:

    | hello |
    |----|
    | 1|


8. If you get any errors, proceed to the section describing other, optional changes that you might need to make after installation is complete, or see the troubleshooting guide:

    + [Optional post-installation steps: Configure service and permissions](#bkmk_FollowUp) 
    + [Troubleshooting machine learning in SQL Server](upgrade-and-installation-faq-sql-server-r-services.md)

## <a name="bkmk_FollowUp"></a> Optional post-installation steps

Depending on your use case for machine learning, you might need to make additional changes to the server, the firewall, the accounts used by the service, or database permissions. The changes you must make vary by case.

Common scenarios that require additional changes include:

* Changing firewall rules to allow inbound connections to SQL Server.
* Enabling additional network protocols.
* Ensuring that the server supports remote connections.
* Enabling *implied authentication*, if users access SQL Server data from a remote data science client and execute code by using the RODBC package or other ODBC provider.
* Giving users access to individual databases.
* Fixing security issues that prevent communication with the Launchpad service.
* Ensuring that users have permission to run code or install packages.

> [!NOTE]
> Not all the listed changes might be required. However, we recommend that you review all items to see whether they are applicable to your scenario.

Additional troubleshooting tips can be found here: [Upgrade and installation FAQ](../r/upgrade-and-installation-faq-sql-server-r-services.md)

### <a name="bkmk_configureAccounts"></a>Enable implied authentication for the Launchpad account group

During setup, some new Windows user accounts are created for running tasks under the security token of the [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] service. When a user sends an R script from an external client, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] activates an available worker account, maps it to the identity of the calling user, and runs the R script on behalf of the user. This new service of the database engine supports the secure execution of external scripts, called *implied authentication*.

You can view these accounts in the Windows user group **SQLRUserGroup**. By default, 20 worker accounts are created, which is usually more than enough for running R jobs.

However, if you need to run R scripts from a remote data science client and are using Windows authentication, you must grant these worker accounts permission to sign in to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance on your behalf.

1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], in Object Explorer, expand **Security**, right-click **Logins**, and select **New Login**.
2. In the **Login - New** dialog box, select **Search**.
3. Select the **Object Types** and **Groups** check boxes, and clear all other check boxes.
4. Click **Advanced**, verify that the location to search is the current computer, and then click **Find Now**.
5. Scroll through the list of group accounts on the server until you find one beginning with `SQLRUserGroup`.
    
    + The name of the group that's associated with the Launchpad service for the _default instance_ is always just **SQLRUserGroup**. Select this account only for the default instance.
    + If you are using a _named instance_, the instance name is appended to the default name, `SQLRUserGroup`. Hence, if your instance is named "MLTEST", the default user group name for this instance would be **SQLRUserGroupMLTest**.
5. Click **OK** to close the advanced search dialog box, and verify that you've selected the correct account for the instance. Each instance can use only its own Launchpad service and the group created for that service.
6. Click **OK** once more to close the **Select User or Group** dialog box.
7. In the **Login - New** dialog box, click **OK**. By default, the login is assigned to the **public** role and has permission to connect to the database engine.

### <a name="bkmk_AllowLogon"></a>Give users permission to run external scripts

> [!NOTE]
> If you use a SQL login for running R scripts in a SQL Server compute context, this step is not required.

If you installed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in your own instance, you are usually executing scripts as an administrator, or at least as a database owner, and thus have implicit permissions for various operations, all data in the database, and the ability to install new packages as needed.

However, in an enterprise scenario, most users, including users who access the database by using SQL logins, do not have such elevated permissions. Therefore, for each user who will be running R or Python scripts, you must grant the user permissions to run scripts in each database where external scripts will be used.

```SQL
USE <database_name>
GO
GRANT EXECUTE ANY EXTERNAL SCRIPT  TO [UserName]
```

> [!TIP]
> Need help with setup? Not sure you have run all the steps? Use these custom reports to check installation status and run additional steps. 
> 
> [Monitor Machine Learning Services using Custom Reports](monitor-r-services-using-custom-reports-in-management-studio.md).

### Ensure that the SQL Server computer supports remote connections

If you cannot connect from a remote computer, check to see whether the server permits remote connections. Remote connections are sometimes disabled by default.

Also check to see whether the firewall allows access to SQL Server. By default, the port used by SQL Server is often blocked by the firewall. If you are using the Windows Firewall, see [Configure Windows Firewall for database engine access](../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md).

### Give your users read, write, or DDL permissions to the database

The user account that is used to run R or Python might need to read data from other databases, create new tables to store results, and write data into tables. Therefore, for each user who will be executing R or Python scripts, ensure that the user has the appropriate permissions on the database: *db_datareader*, *db_datawriter*, or *db_ddladmin*.

For example, the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement gives the SQL login *MySQLLogin* the rights to run T-SQL queries in the *RSamples* database. To run this statement, the SQL login must already exist in the security context of the server.

```SQL
USE RSamples
GO
EXEC sp_addrolemember 'db_datareader', 'MySQLLogin'
```

For more information about the permissions included in each role, see [Database-level roles](../../relational-databases/security/authentication-access/database-level-roles.md).

### Use machine learning in an Azure VM

If you installed Machine Learning Services (or R Services) on an Azure virtual machine, you might need to change some additional defaults. For more information, see [Installing SQL Server Machine Learning on an Azure virtual machine](installing-sql-server-r-services-on-an-azure-virtual-machine.md).

### Create an ODBC data source for the instance on your data science client

If you create an R solution on a data science client computer and need to run code by using the SQL Server computer as the compute context, you can use either a SQL login or integrated Windows authentication.

* For SQL logins: Ensure that the login has appropriate permissions on the database where you will be reading data. You can do so by adding *Connect to* and *SELECT* permissions, or by adding the login to the *db_datareader* role. For logins that need to create objects, add *DDL_admin* rights. For logins that must save data to tables, add the login to the *db_datawriter* role.

* For Windows authentication: You might need to configure an ODBC data source on the data science client that specifies the instance name and other connection information. For more information, see [ODBC data source administrator](https://docs.microsoft.com/sql/odbc/admin/odbc-data-source-administrator).

## Next steps

After you have verified that the script-execution feature works in SQL Server, you can run R and Python commands from SQL Server Management Studio, Visual Studio Code, or any other client that can send T-SQL statements to the server. Before doing so, you might want to make some changes to the system configuration to support heavy use of machine learning, or add new R packages.

This section lists some common optimizations and learning activities for machine learning.

### Add more worker accounts

If you think you might use R heavily, or if you expect many users to be running scripts concurrently, you can increase the number of worker accounts that are assigned to the Launchpad service. For more information, see [Modify the user account pool for SQL Server Machine Learning Services](modify-the-user-account-pool-for-sql-server-r-services.md).

### <a name="bkmk_optimize"></a>Optimize the server for external script execution

The default settings for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup are intended to optimize the balance of the server for a variety of services that are supported by the database engine, which might include extract, transform, and load (ETL) processes, reporting, auditing, and applications that use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data. Therefore, under the default settings, you might find that resources for machine learning are sometimes restricted or throttled, particularly in memory-intensive operations.

To ensure that machine learning jobs are prioritized and resourced appropriately, we recommend that you use SQL Server Resource Governor to configure an external resource pool. You might also want to change the amount of memory that's allocated to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database engine, or increase the number of accounts that run under the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service.

- To configure a resource pool for managing external resources, see [Create an external resource pool](../../t-sql/statements/create-external-resource-pool-transact-sql.md).
  
- To change the amount of memory reserved for the database, see [Server memory configuration options](../../database-engine/configure-windows/server-memory-server-configuration-options.md).
  
- To change the number of R accounts that can be started by [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)], see [Modify the user account pool for machine learning](modify-the-user-account-pool-for-sql-server-r-services.md).

If you are using Standard Edition and do not have Resource Governor, you can use Dynamic Management Views (DMVs) and Extended Events, as well as Windows event monitoring, to help manage the server resources that are used by R. For more information, see [Monitoring and managing R Services](managing-and-monitoring-r-solutions.md).

### Install additional R packages

Take a minute to install any additional R packages you'll be using.

Packages that you want to use from SQL Server must be installed in the default library that is used by the instance. If you have a separate installation of R on the computer, or if you installed packages to user libraries, you won't be able to use those packages from T-SQL.

The process for installing and managing R packages is different in SQL Server 2016 and SQL Server 2017. For example, in SQL Server 2017, you can set up user groups to share packages on a per-database level, or configure database roles to enable users to install their own packages. For more information, see [Package management](r-package-management-for-sql-server-r-services.md).

In SQL Server 2016, a database administrator must install R packages that users need.

Administrative access is also required to install additional Python packages in the instance library.

### Upgrade the machine learning components

When you install machine learning features in SQL Server, you get the version of the R or Python components that was most up-to-date when the release or service pack was published. Each time you patch or upgrade the server, the machine learning components are upgraded as well.

However, you can upgrade the machine learning components on a faster schedule than is supported by SQL Server releases, by using a process known as _binding_. When you bind a SQL Server instance, you both upgrade the R or Python versions, and change to a different support policy that supports more frequent upgrades. 

Such upgrades might include:

* New R packages
+ New APIs for Microsoft packages such as [MicrosoftML](https://docs.microsoft.com/r-server/r-reference/microsoftml/microsoftml-package).
* [Pretrained models](https://docs.microsoft.com/r-server/install/microsoftml-install-pretrained-models) for image classification and text analysis.

For information about how to upgrade a SQL Server instance, see [Upgrade machine learning components through binding](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md).


### Tutorials

To get started with some simple examples, and learn the basics of how R works with SQL Server, see [Using R code in Transact-SQL](../tutorials/rtsql-using-r-code-in-transact-sql-quickstart.md).

To view examples of machine learning that are based on real-world scenarios, see [Machine learning tutorials](../tutorials/machine-learning-services-tutorials.md).

### Troubleshooting

Run into trouble? Trying to upgrade? For answers to common questions and known issues, see the following article:

* [Upgrade and installation FAQ - Machine Learning Services](upgrade-and-installation-faq-sql-server-r-services.md)

To check the installation status of the instance and fix common issues, try these custom reports.

* [Custom reports for SQL Server R Services](\r\monitor-r-services-using-custom-reports-in-management-studio.md)
