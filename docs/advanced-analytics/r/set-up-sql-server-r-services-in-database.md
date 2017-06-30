---
title: "Set up SQL Server R Services (In-Database) | Microsoft Docs"
ms.custom: ""
ms.date: "06/29/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
keywords: 
  - "installing SQL Server R Services"
ms.assetid: 4d773c74-c779-4fc2-b1b6-ec4b4990950d
caps.latest.revision: 35
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Set up SQL Server R Services (In-Database)

This topic describes how to set up machine learning services for the R language, in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] or later, by running the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup wizard.

SQL Server setup provides these options for installing R:

+ Install R in the SQL Server database

  This option lets you run R scripts inside SQL Server, or lets the server act as a remote compute context for R scripts run from an external connection.

+ Install a standalone server

  This option creates a development environment for machine learning solutions that runs separately from SQL Server. We recommend that you installing R Server on a different computer than the one hosting SQL Server. For more information on this option, see [Create a Standalone R Server](../r/create-a-standalone-r-server.md).

**Overview of installation process**

1. [Install the external scripting feature](#bkmk_installExt)
2. [Enable the feature and restart the computer](#bkmk_enableFeature)
3. [Verify that external script execution works](#bkmk_TestScript)
4. [Additional configuration of accounts, packages, or databases](#bkmk_FollowUp)

## <a name="bkmk_prereqs"> </a> Prerequisites

+  Avoid installing both R Server and R Services at the same time. Installing R Server (Standalone) is typically done because you want to create an environment that a data scientist or developer can use to connect to SQL Server and deploy R solutions. Therefore, there is no need to install both on the same computer.

+ If you used any earlier versions of the Revolution Analytics development environment or the RevoScaleR packages, or if you installed any pre-release versions of SQL Server 2016, you must uninstall them. Side-by-side install is not supported. For help removing previous versions, see [Upgrade and Installation FAQ for SQL Server R Services](../r/upgrade-and-installation-faq-sql-server-r-services.md)

+ You cannot install machine learning services on a failover cluster. The reason is that the security mechanism used for isolating external script processes is not compatible with a Windows Server failover cluster environment. As a workaround, you can use replication to copy necessary tables to a standalone SQL Server instance with R Services, or install [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] on a standalone computer that uses Always On and is part of an availability group.

> [!IMPORTANT]
> After setup is complete, some additional steps are required to enable machine learning feature. You might also need to give users permissions to specific databases, change or configure accounts, or set up a remote data science client.

##  <a name="bkmk_installExt"></a> Step 1: Install the extensibility features and choose a machine learning language

To use machine learning, you must install SQL Server 2016 or later. At least one instance of the database engine is required to use [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)]. You can use either a default or named instance.

1.  Run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup.
  
    For information about how to do unattended installs, see [Unattended Installs of SQL Server R Services](../r/unattended-installs-of-sql-server-r-services.md).
  
2.  On the **Installation** tab, click **New SQL Server stand-alone installation or add features to an existing installation**.
   
3.  On the **Feature Selection** page, select the following options, to install the database services used by R jobs and installs the extensions that support external scripts and processes. 
  
    **SQL Server 2016**
    
    - Select **Database Engine Services**
    - Select **R Services (In-Database)**

    **SQL Server 2017**
    
    - Select **Database Engine Services**
    - Select **Machine Learning Services (In-Database)**
    - Select at least one machine learning language to enable. You can select just R, or add both R and Python.

    > [!NOTE]
    > If you do not select either the R or Python language options, the setup wizard will install only the extensibility framework, which includes SQL Server Trusted Launchpad, but will not install any language-specific components. This option is for binding the SQL Server instance to R or Python as a part of the modern lifecycle support policy. For more information, see [Use SqlBindR to Upgrade an Instance of R Services](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md)

4.  On the page, **Consent to Install Microsoft R Open**, click **Accept**.
  
     This license agreement is required to download Microsoft R Open, which include a distribution of the open source R base packages and tools, together with enhanced R packages and connectivity providers from Revolution Analytics.
  
    > [!NOTE]
    >  If the computer you are using does not have Internet access, you can pause setup at this point to download the installers separately as described here: [Installing R Components without Internet Access](installing-ml-components-without-internet-access.md)
  
     Click **Accept**, wait until the **Next** button becomes active, and then click **Next**.
  
5.  On the **Ready to Install** page, verify that these selections are included, and click **Install**.
  
     **SQL Server 2016**
     - Database Engine Services
     - R Services (In-Database)

     **SQL Server 2017**
     - Database Engine Services
     - Machine Learning Services (In-Database)
     - R or Python, or both
  
6.  When installation is complete, restart the computer as instructed.

##  <a name="bkmk_enableFeature"></a> Step 2: Enable external script services

1. Open [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. If it is not already installed, you can rerun the SQL Server setup wizard to open a download link and install it.
  
2. Connect to the instance where you installed machine learning, and run the following command:

   ```SQL
   sp_configure
   ```

    The value for the property, `external scripts enabled`, should be **0** at this point. That is because the feature is turned off by default, to reduce the surface area, and must be explicitly enabled by an administrator.
     
3.  To enable the external scripting feature, run the following statement.
  
    ```SQL
    EXEC sp_configure  'external scripts enabled', 1
    RECONFIGURE WITH OVERRIDE
    ```
  
10. Restart the SQL Server service for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. Restarting the SQL Server service will also automatically restart the related [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service.

    You can restart the service using the **Services** panel in Control Panel, or by using SQL Server Configuration Manager.

## <a name="bkmk_TestScript"></a> Step 3. Verify that script execution works locally

Verify that the external script execution service is enabled.

1. In SQL Server Management Studio, open a new Query window, and run the following command:
  
    ```SQL
    EXEC sp_configure  'external scripts enabled'
    ```
    The **run_value** should now be set to 1.
    
2. Open the **Services** panel and verify that the Launchpad service for your instance is running. If you install multiple instances, each instance has its own Launchpad service.
   
3. Open a new Query window in  [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and run a simple R script.
  
    ```SQL
    EXEC sp_execute_external_script  @language =N'R',
    @script=N'OutputDataSet<-InputDataSet',
    @input_data_1 =N'SELECT 1 as hello'
    WITH RESULT SETS (([hello] int not null));
    GO
    ```
  
    **Results**
  
    *hello*
    *1*
  
   If the command executes without an error, go on to the next steps.  If you get an error, see this article for a list of some common problems:  [Upgrade and Installation FAQ](../r/upgrade-and-installation-faq-sql-server-r-services.md).

## Next steps

After you have verified that the script execution feature works in SQL Server, you can run R commands from SQL Server Management Studio, Visual Studio Code, or any other client that can send T-SQL statements to the server.

However, if you intend to run commands from a remote R development client, you might need to make additional changes. For more information, see [Additional Configuration](#bkmk_FollowUp).

### Tutorials

To get started with some simple examples, and learn the basics of how R works with SQL Server, see [Using R Code in Transact-SQL](../tutorials/rtsql-using-r-code-in-transact-sql-quickstart.md).

To view advanced machine learning walkthroughs based on real-world scenarios, see [R Tutorials](../tutorials/machine-learning-services-tutorials.md)

### Install R packages

Take a minute to install any additional R packages you'll be using.

Packages that you want to use from SQL Server must be installed in the default library that is used by the instance. If you have a separate installation of R on the computer, or if you installed packages to user libraries, you won't be able to use those packages from T-SQL. For more information,  see [Install Additional R Packages on SQL Server](../../advanced-analytics/r-services/install-additional-r-packages-on-sql-server.md).

You can also set up user groups to share packages on a per-database level, or configure database roles to enable users to install their own packages. For more information, see [Package Management](r-package-management-for-sql-server-r-services.md).

## <a name="bkmk_FollowUp"></a> Additional Configuration

Depending on your use case for R in SQL Server, you might need to make additional changes to the server, the firewall, the accounts used by the service, or to database permissions.

For example, you might need to access SQL Server data from a remote R development terminal, perform ODBC calls from your R code, or convert R code to stored procedures. If you have additional security restrictions, you will need to ensure that the Launchpad services accounts can access the database. If you will be running R scripts from a remote computer, you will need to give the remote users database access and permission to run external scripts.

> [!NOTE]
> Not all the listed changes might be required. However, we recommend that you review all items to see if they are applicable to your scenario.

###  <a name="bkmk_configureAccounts"></a> Enable implied authentication for Launchpad account group

During setup, a number of new Windows user accounts are created for the purpose of running tasks under the security token of the [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] service. When a user sends an R script from an external client, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will activate an available worker account, map it to the identity of the calling user, and run the R script on behalf of the user. This is a new service of the database engine that supports secure execution of external scripts, called *implied authentication*.

You can view these accounts in the Windows user group, **SQLRUserGroup**.  By default, 20 worker accounts are created, which is typically more than enough for running R jobs.

However, if you need to run R scripts from a remote data science client and are using Windows authentication, these worker accounts must be given permission to log in to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance on your behalf.

1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], in Object Explorer, expand **Security**, right-click **Logins**, and select **New Login**.
2. In the **Login - New** dialog box, click **Search**.
3. Click **Object Types** and select **Groups**. Deselect everything else.
4. In Enter the object name to select, type *SQLRUserGroup*  and click **Check Names**.
5. The name of the local group associated with the instance's Launchpad service should resolve to something like *instancename\SQLRUserGroup*. Click **OK**.
6. By default, the login is assigned to the **public** role and has permission to connect to the database engine.
7. Click **OK**.

> [!NOTE]
> If you use a SQL login for running R scripts in a SQL Server compute context, this extra step is not required.

### <a name="bkmk_AllowLogon"></a>Give users permission to run external scripts

If you installed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and are running R scripts in your own instance, you are typically executing scripts as an administrator, or at least database owner, and thus have implicit permission over various operations, all data in the database, and the ability to install new R packages as needed.

However, in an enterprise scenario, most users, including those accessing the database using SQL logins, do not have such elevated permissions. Therefore, for each user who will be running R or Python scripts, you must grant the user permissions to run scripts in each database where external scripts will be used.

```SQL
USE <database_name>
GO
GRANT EXECUTE ANY EXTERNAL SCRIPT  TO [UserName]
```

> [!TIP]
> Need help with setup? Not sure you have run all the steps? Use these custom reports to check the installation status of R Services. For more information, see [Monitor R Services using Custom Reports](monitor-r-services-using-custom-reports-in-management-studio.md).

### Ensure that the SQL Server supports remote connections

If you cannot connect from a remote computer, check whether the server permits remote connections. Sometimes remote connections are disabled by default.

Also check whether the firewall allows access to SQL Server. By default, the port used by SQL Server is often blocked by the firewall. If you are using the Windows firewall, see [Configure Windows Firewall for Database Engine Access](../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md)

### Add more worker accounts

If you think you will use R heavily, or if you expect many users to be running scripts concurrently, you can increase the number of worker accounts that are assigned to the Launchpad service. For more information, see [Modify the User Account Pool for SQL Server R Services](modify-the-user-account-pool-for-sql-server-r-services.md).

### Give your users read, write, or DDL permissions to database

While running R scripts, the user account or SQL login might need to read data from other databases, create new tables to store results, and write data into tables.

For each user account or SQL login that will be executing R scripts, be sure that the account or login has `db_datareader`, `db_datawriter`, or `db_ddladmin` permissions on the specific database.

For example, the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement gives the SQL login *MySQLLogin* the rights to run T-SQL queries in the *RSamples* database. To run this statement, the SQL login must already exist in the security context of the server.

```SQL
USE RSamples
GO
EXEC sp_addrolemember 'db_datareader', 'MySQLLogin'
```

For more information about the permissions included in each role, see [Database-Level Roles](../../relational-databases/security/authentication-access/database-level-roles.md).

### Create an ODBC data source for the instance on your data science client

If you create an R solution on a data science client computer and need to run code using the SQL Server computer as the compute context, you can use either a SQL login, or integrated Windows authentication.

+ For SQL logins: Ensure that the login has appropriate permissions on the database where you will be reading data. You can do this by adding *Connect to* and *SELECT* permissions, or by adding the login to the `db_datareader` role. If you need to create objects, you will need `DDL_admin` rights.  To save data to tables, add the login to the `db_datawriter` role.

+ For Windows authentication: You must configure an ODBC data source on the data science client that specifies the instance name and other connection information. For more information, see [Using the ODBC Data Source Administrator](http://windows.microsoft.com/windows/using-odbc-data-source-administrator).

### <a name="bkmk_optimize"></a>Optimize the server for R

The default settings for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup are intended to optimize the balance of the server for a variety of services supported by the database engine, which might include ETL processes, reporting, auditing, and applications that use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data. Therefore, under the default settings you  might find that resources for R operations are sometimes restricted or throttled, particularly in memory-intensive operations.

To ensure that R tasks are prioritized and resourced appropriately, we recommend that you use Resource Governor to configure an external resource pool specific for R operation. You might also wish to change the amount of memory allocated to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database engine, or increase the number of accounts running under the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service.

-   Configure a resource pool for managing external resources
  
     [CREATE EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-resource-pool-transact-sql.md)
  
-   Change the amount of memory reserved for the database
  
     [Server Memory Server Configuration Options](../../database-engine/configure-windows/server-memory-server-configuration-options.md)
  
-   Change the number of R accounts that can be started by [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)]
  
     [Modify the User Account Pool for SQL Server R Services](modify-the-user-account-pool-for-sql-server-r-services.md)

If you are using Standard Edition and do not have Resource Governor, you can use DMVs and Extended Events, as well as Windows event monitoring, to help you manage server resources used by R. For more information, see [Monitoring and Managing R Services](managing-and-monitoring-r-solutions.md).

### Resources

 Run into trouble? Trying to upgrade? See these articles for common questions and known issues:

 + [Upgrade and Installation FAQ - SQL Server R Services](upgrade-and-installation-faq-sql-server-r-services.md)

 Try these custom reports to check the installation status of the instance and fix common issues.
 
 + [Custom Reports for SQL Server R Services](monitor-r-services-using-custom-reports-in-management-studio.md)
