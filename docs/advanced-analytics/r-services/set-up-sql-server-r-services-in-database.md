---
title: "Set up SQL Server R Services (In-Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
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
  In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and later, you can install the components required for using R, by running the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup wizard. SQL Server setup provides these options:
  
  + Install the database engine together with SQL Server R Services (In-Database), to enable running R scripts in SQL Server 

OR:  
  + Install Microsoft R Server (Standalone) to create a development environment for R solutions. For more information, see [Create a Standalone R Server](../../advanced-analytics/r-services/create-a-standalone-r-server.md).  
 

 
## Prerequisites

+  We recommend that you do not install both R Server and R Services in a single installation. Installing R Server (Standalone) is typically done because you want to create an environment that a data scientist or developer can use to connect to SQL Server and deploy R solutions. Therefore, there is no need to install both on the same computer.
+ If you are installing any version prior to SQL Server 2016 SP1, you must enable creation of short file names using the 8dot3 notation. This is for compatibility with the open source R components. However, this restriction has been removed as of SQL Server 2016 SP1, or Cumulative Update CU3 OD. 

    + [Hotfix for SQL Server 2016 CU3](https://support.microsoft.com/help/3210110/on-demand-hotfix-update-package-for-sql-server-2016-cu3)
    + [Cumulative Update 1 for SQL Server 2016 SP1](https://support.microsoft.com/help/3208177/cumulative-update-1-for-sql-server-2016-sp1)

+ If you have installed any earlier versions of the Revolution Analytics development environment or the RevoScaleR packages, or if you installed any pre-release versions of SQL Server 2016, you should uninstall them first. Side-by-side install is not supported. For help uninstalling previous versions, see [Upgrade and Installation FAQ for SQL Server R Services](../../advanced-analytics/r-services/upgrade-and-installation-faq-sql-server-r-services.md)

+ You cannot install [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] on a failover cluster. The reason is that the security mechanism used for isolating R processes is not compatible with a Windows Server failover cluster environment. As a workaround, you can use replication to copy necessary tables to a standalone SQL Server instance with R Services, or install [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] on a standalone computer that uses Always On and is part of an availability group. . 

> [!IMPORTANT]    
> After setup is complete, some additional steps are required to enable the R Services feature. Depending on your use of R, you might also need to give users permissions to specific databases, change or configure accounts, or set up a remote data science client. .   

  
##  <a name="bkmk_installRServicesInDatabase"></a> Step 1: Install R Services (In-Database) on SQL Server 2016 or later  


1.  Run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup.  
  
    For information about how to do unattended installs, see [Unattended Installs of SQL Server R Services](../../advanced-analytics/r-services/unattended-installs-of-sql-server-r-services.md).  
  
2.  On the **Installation** tab, click **New SQL Server stand-alone installation or add features to an existing installation**.  
   
3.  On the **Feature Selection** page, select both of these options:  
  
    -   **Database Engine Services**  
  
         At least one instance of the database engine is required to use [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)]. You can use either a default or named instance.  
  
    -   **R Services (In-Database)**  
  
         This option configures the database services used by R jobs and installs the extensions that support external scripts and processes.  
        > [!NOTE]
        > Be sure to choose the **[!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)]** option  if you need to operationalize your solutions in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. 
        > 
        > Select the **Microsoft R Server (Standalone)** option to install the R components on a different computer, such as a laptop that is used for R development.
 
  
4.  On the page, **Consent to Install Microsoft R Open**, click **Accept**.  
  
     This license agreement is required to download Microsoft R Open, which include a distribution of the open source R base packages and tools, together with enhanced R packages and connectivity providers from Revolution Analytics.  
  
    > [!NOTE]  
    >  If the computer you are using does not have Internet access, you can pause setup at this point to download the installers separately as described here: [Installing R Components without Internet Access](../../advanced-analytics/r-services/installing-r-components-without-internet-access.md)  
  
     Click **Accept**, wait until the **Next** button becomes active, and then click **Next**.  
  
5.  On the **Ready to Install** page, verify that these selections are included, and click **Install**. 
  
     + Database Engine Services  
  
     + R Services (In-Database)  
  
6.  When installation is complete, restart the computer.   
  
  
##  <a name="bkmk_enableFeature"></a> Step 2: Enable R Services  
  
  
1. Open [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. If it is not already installed, you can rerun the SQL Server setup wizard to open a download link and install it.  
  
2. Connect to the instance where you installed R Services (In-Database), and run the following command:

   ```    
   sp_configure    
   ```  

    The value for the property, `external scripts enabled`, should be **0** at this point. That is because the feature is turned off by default, to reduce the surface area, and must be explicitly enabled by an administrator.
     
3.  To enable the R Services feature, run the following statement.  
  
    ```    
    Exec sp_configure  'external scripts enabled', 1  
    Reconfigure  with override    
    ```  
  
10. Restart the SQL Server service for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. Restarting the SQL Server service will also automatically restart the related [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service. 

    You can restart the service using the **Services** panel in Control Panel, or by using SQL Server Configuration Manager.  
  
## Step 3. Verify that Script Execution Works Locally

After the SQL Server service is available, take a moment to verify that all components used for R Services are running.

1. In SQL Server Management Studio, open a new Query window, and run the following command:  
  
    ```    
    Exec sp_configure  'external scripts enabled'    
    ```  
    The **run_value** should now be set to 1.
    
2. Open the **Services** panel and verify that the Launchpad service for your instance is running. If you install multiple instances, each instance has its own Launchpad service.
   
3. If Launchpad is running, you should be able to run simple R scripts like the following in  [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]:  
  
    ```  
    exec sp_execute_external_script  @language =N'R',  
    @script=N'OutputDataSet<-InputDataSet',    
    @input_data_1 =N'select 1 as hello'  
    with result sets (([hello] int not null));  
    go  
    ```  
  
    **Results**  
  
    *hello*  
    *1*   
  
    + If this command is successful, you can run R commands from SQL Server Management Studio, Visual Studio Code, or any other client that can send T-SQL statements to the server.  To get started with some simple examples, and learn the basics of how R works with SQL Server, see [Using R Code in Transact-SQL](../../advanced-analytics/r-services/using-r-code-in-transact-sql-sql-server-r-services.md).
    + If you get an error, see this article for a list of some common problems:  [Upgrade and Installation FAQ](../../advanced-analytics/r-services/upgrade-and-installation-faq-sql-server-r-services.md). 

4. Optionally, take a minute to install any additional R packages you'll be using. 

    Packages that you want to use from SQL Server must be installed in the default library that is used by the instance. If you have a separate installation of R on the compouter, or if you installed packages to user libraries, you won't be able to use those packages from T-SQL. For more information,  see [Install Additional R Packages on SQL Server](../../advanced-analytics/r-services/install-additional-r-packages-on-sql-server.md).

5. Proceed to the next sections if you need to access SQL Server data, perform ODBC calls from your R code, or execute R commands from a remote data science client.  
  
##  <a name="bkmk_configureAccounts"></a> Step 4: Enable Implied Authentication for Launchpad Accounts  
   
  
During setup, a number of new Windows user accounts are created for the purpose of running tasks under the security token of the [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] service. When a user sends an R script from an external client, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will activate an available worker account, map it to the identity of the calling user, and run the R script on behalf of the user. This is a new service of the database engine that supports secure execution of external scripts, called *implied authentication*. 

You can view these accounts in the Windows user group, **SQLRUserGroup**.  By default, 20 worker accounts are created, which is typucally more than enough for running R jobs. 

However, if you need to run R scripts from a remote data science client and are using Windows authentication, these worker accounts must be given permission to log into the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance on your behalf.  
  
1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], in Object Explorer, expand **Security**, right-click **Logins**, and select **New Login**.  
2. In the **Login - New** dialog box, click **Search**.  
3. Click **Object Types** and select **Groups**. Deselect everything else.  
4. In Enter the object name to select, type *SQLRUserGroup*  and click **Check Names**.  
5. The name of the local group associated with the instance's Launchpad service should resolve to something like *instancename\SQLRUserGroup*. Click **OK**.  
6. By default, the login is assigned to the **public** role and has permission to connect to the database engine. 
7. Click **OK**.  
  
> [!NOTE]
> If you use a SQL login for running R scripts in a SQL Server compute context, this extra step is not required.
  
  
## Step 5: Give Non-Admin Users R Script Permissions 
  
If you installed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] yourself and are running R scripts in your own instance, you are typically executing scripts as an administrator and thus have implicit permission over various operations and all data in the database, as well as the ability to install new R packages as needed.  
  
However, in an enterprise scenario, most users, including those accessing the database using SQL logins, do not have such elevated permissions. Therefore, for each user that will be running external scripts, you must grant the user permissions to run R scripts in each database where R will be used.   
  
  
```  
USE <database_name>  
GO  
GRANT EXECUTE ANY EXTERNAL SCRIPT  TO [UserName]  
```  

> [!TIP]
> Need help with setup? Not sure you have run all the steps? Use these custom reports to check the installation status of R Services. For more information, see [Monitor R Services using Custom Reports](../../advanced-analytics/r-services/monitor-r-services-using-custom-reports-in-management-studio.md).    
  
##  <a name="bkmk_Additional"></a> Additional Configuration Options 
  
This section describes additional changes that you can make in the configuration of the instance, or of your data science client, to support R script execution.
  
### Modify the number of worker accounts used by [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)]
  
If you think you will use R heavily, or if you expect many users to be running scripts concurrently, you can increase the number of worker accounts that are assigned to the Launchpad service. For more information, see [Modify the User Account Pool for SQL Server R Services](../../advanced-analytics/r-services/modify-the-user-account-pool-for-sql-server-r-services.md).  
  
  
### Give your users read, write, or DDL permissions as needed in additional databases  
  
While running R scripts, the user account or SQL login might need to read data from other databases, create new tables to store results, and write data into tables. 
     
For each user account or SQL login that will be executing R scripts, be sure that the account or login has **db_datareader**, **db_datawriter**, or **db_ddladmin** permissions on the specific database.   
       
For example, the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement gives the SQL login *MySQLLogin* the rights to run T-SQL queries in the *RSamples* database. To run this statement, the SQL login must already exist in the security context of the server.  
  
```  
USE RSamples  
GO  
EXEC sp_addrolemember 'db_datareader', 'MySQLLogin'  
```  
  
For more information about the permissions included in each role, see [Database-Level Roles](../../relational-databases/security/authentication-access/database-level-roles.md).  
  
  
### Create an ODBC data source for the instance on your data science client  
  
If you create an R solution on a data science client computer and need to run code using the SQL Server computer as the compute context, you can use either a SQL login, or integrated Windows authentication.  
  
+ For SQL logins: Ensure that the login has appropriate permissions on the database where you will be reading data. You can do this by adding *Connect to* and *SELECT* permissions, or by adding the login to the **db_datareader** role. If you need to create objects, you will need **DDL_admin** rights.  To save data to tables, add the login to the **db_datawriter** role.  
  
+ For Windows authentication: You must configure an ODBC data source on the data science client that specifies the instance name and other connection information. For more information, see [Using the ODBC Data Source Administrator](http://windows.microsoft.com/windows/using-odbc-data-source-administrator).  
  
### Optimize the Server for R  

The default settings for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup are intended to optimize the balance of the server for a variety of services supported by the database engine, which might include ETL processes, reporting, auditing, and applications that use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data. Therefore, under the default settings you  might find that resources for R operations are sometimes restricted or throttled, particularly in memory-intensive operations.  
  
 To ensure that R tasks are prioritized and resourced appropriately, we recommend that you use Resource Governor to configure an external resource pool specific for R operation. You might also wish to change the amount of memory allocated to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database engine, or increase the number of accounts running under the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service.  
  
-   Configure a resource pool for managing external resources  
  
     [CREATE EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-resource-pool-transact-sql.md)  
  
-   Change the amount of memory reserved for the database engine  
  
     [Server Memory Server Configuration Options](../../database-engine/configure-windows/server-memory-server-configuration-options.md)  
  
-   Change the number of R accounts that can be started by [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)]  
  
     [Modify the User Account Pool for SQL Server R Services](../../advanced-analytics/r-services/modify-the-user-account-pool-for-sql-server-r-services.md)  

If you are using Standard Edition and do not have Resource Governor, you can use DMVs and Extended Events, as well as Windows event monitoring, to help you manage server resources used by R. For more information, see [Monitoring and Managing R Services](../../advanced-analytics/r-services/managing-and-monitoring-r-solutions.md).
  
### Get the R Source Code (Optional)  

[!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] includes a distribution of the open source R base packages.  
  
Optionally, click one of these links to immediately begin downloading the modified GPL/LGPL source code. The source code is made available in compliance with the GNU General Public License, but is not required to install or use [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)].  
  
-   Compatible with RC2: Download archive [rre-gpl-src.8.0.2.tar.gz](http://go.microsoft.com/fwlink/?LinkId=786770) 
  
-   Compatible with RC3: Download archive [rre-gpl-src.8.0.3.tar.gz](http://go.microsoft.com/fwlink/?LinkId=786771) 

-   Compatible with RTM: Download archive [rre-gpl-src.8.0.3.tar.gz](http://go.microsoft.com/fwlink/?LinkID=786771)
  
## Troubleshooting  

 Run into trouble? Have a pre-release version of [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)]? See this list of common issues.  
  
 + [Upgrade and Installation FAQ &#40;SQL Server R Services&#41;](../../advanced-analytics/r-services/upgrade-and-installation-faq-sql-server-r-services.md)  
 
 Try these custom reports, to check the installation status of the instance and fix common issues.
 
 + [Custom Reports for SQL Server R Services](../../advanced-analytics/r-services/monitor-r-services-using-custom-reports-in-management-studio.md)
  
## See Also  
 [Set Up  a Data Science Client](../../advanced-analytics/r-services/set-up-a-data-science-client.md)   
 [Create a Standalone R Server](../../advanced-analytics/r-services/create-a-standalone-r-server.md)  
  
  
