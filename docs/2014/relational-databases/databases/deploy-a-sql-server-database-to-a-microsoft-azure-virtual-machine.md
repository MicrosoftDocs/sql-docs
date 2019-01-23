---
title: "Deploy a SQL Server Database to a Microsoft Azure Virtual Machine | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.deploymentwizard.deploymentsettings.f1"
  - "sql12.swb.deploymentwizard.progress.f1"
  - "sql11.swb.usevmdialog.f1"
  - "sql11.swb.deploymentwizard.f1"
  - "sql12.swb.deploymentwizard.azuresignin.f1"
  - "sql11.swb.deploymentwizard.summary.f1"
  - "sql11.swb.deploymentwizard.azuresignin.f1"
  - "sql12.swb.deploymentwizard.f1"
  - "sql12.swb.sqlvmdialog.f1"
  - "sql11.swb.newvmdialog.f1"
  - "sql12.swb.deploymentwizard.results.f1"
  - "sql11.swb.deploymentwizard.progress.f1"
  - "sql12.swb.newvmdialog.f1"
  - "sql12.swb.deploymentwizard.sourcesettings.f1"
  - "sql12.swb.usevmdialog.f1"
  - "sql11.swb.deploymentwizard.sourcesettings.f1"
  - "sql11.swb.deploymentwizard.results.f1"
  - "sql12.swb.deploymentwizard.summary.f1"
  - "sql11.swb.deploymentwizard.deploymentsettings.f1"
helpviewer_keywords: 
  - "Deploy a database"
  - "Deploy to Azure VM"
  - "Migrate to Azure"
  - "Windows Azure virtual machine"
  - "Migrate to Azure VM"
  - "Migrate to the cloud"
  - "SQL Server Management Studio"
  - "SSMS"
  - "Deploy database wizard"
  - "Deploy a SQL Server database to Azure"
  - "Azure VM"
ms.assetid: 5e82e66a-262e-4d4f-aa89-39cb62696d06
author: stevestein
ms.author: sstein
manager: craigg
---
# Deploy a SQL Server Database to a Microsoft Azure Virtual Machine
  Use the **Deploy a SQL Server Database to a Windows Azure VM** wizard to deploy a database from an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in a Windows Azure Virtual Machine (VM). The wizard utilizes a full database backup operation, so it always copies the complete database schema and the data from a SQL Server user database. The wizard also does all of the Azure VM configuration for you, so no pre-configuration of the VM is required.  
  
 You cannot use the wizard for differential backups because the wizard will not overwrite an existing database that has the same database name. To replace an existing database on the VM, you must first drop the existing database or change the database name. If there is a naming conflict between the database name for an in-flight deploy operation and an existing database on the VM, the wizard will suggest an appended database name for the in-flight database to enable you to complete the operation.  
  
##  <a name="before_you_begin"></a> Before You Begin  
 To complete this wizard, you must be able to provide the following information and have these configuration settings in place:  
  
-   The Microsoft account details associated with your Windows Azure subscription.  
  
-   Your Windows Azure publishing profile.  
  
    > [!CAUTION]  
    >  SQL Server currently supports publishing profile version 2.0. To download the supported version of the publishing profile, see [Download Publishing Profile 2.0](https://go.microsoft.com/fwlink/?LinkId=396421).  
  
-   The management certificate uploaded to your Windows Azure subscription.  
  
-   The management certificate saved into the personal certificate store on the computer where the wizard is running.  
  
-   You must have a temporary storage location that is available to the computer where the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database is hosted. The temporary storage location must also be available to the computer where the wizard is running.  
  
-   If you are deploying the database to an existing VM, the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be configured to listen on a TCP/IP port.  
  
-   Either a Windows Azure VM or Gallery image you plan to use for creation of the VM must have the SQL Server Cloud Adapter configured and running.  
  
-   You must configure an open endpoint for your SQL Server Cloud Adapter on the Windows Azure gateway with private port 11435.  
  
 In addition, if you plan to deploy your database into an existing Windows Azure VM, you must also be able to provide:  
  
-   The DNS name of the cloud service that hosts your VM.  
  
-   Administrator credentials for the VM.  
  
-   Credentials with Backup operator privileges on the database you plan to deploy, from the source instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 For more information about running SQL Server in Windows Azure virtual machines, see [Getting Ready to Migrate to SQL Server in Windows Azure Virtual Machines](https://msdn.microsoft.com/library/dn133142.aspx).  
  
 On computers running Windows Server operating systems, you must use the following configuration settings to run this wizard:  
  
-   Turn off Enhanced Security Configuration:  Use Server Manager > Local Server to set Internet Explorer Enhanced Security Configuration (ESC) to **OFF**.  
  
-   Enable JavaScript:  Internet Explorer > Internet Options > Security > Customer Level > Scripting > Active Scripting: **Enable**.  
  
###  <a name="limitations"></a> Limitations and Restrictions  
 The database size limitation for this operation is 1 TB.  
  
 This deployment feature is available in SQL Server Management Studio for [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)].  
  
 This deployment feature is for use only with user databases; deploying system databases is not supported.  
  
 The deployment feature does not support hosted services that are associated with an Affinity Group. For example, storage accounts associated with an Affinity Group cannot be selected for use on the **Deployment Settings** page of this wizard.  
  
 The SQL Server version in the VM must be the same or later than the source SQL Server version. SQL Server database versions that can be deployed to a Windows Azure VM using this wizard:  
  
-   SQL Server 2008  
  
-   SQL Server 2008 R2  
  
-   SQL Server 2012  
  
-   [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]  
  
 SQL Server database versions running in a Windows Azure VM database can be deployed to:  
  
-   SQL Server 2012  
  
-   [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]  
  
 If there is a naming conflict between the database name for an in-flight deploy operation and an existing database on the VM, the wizard will suggest an appended database name for the in-flight database to enable you to complete the operation.  
  
###  <a name="filestream"></a> Considerations for Deploying a FILESTREAM-enabled Database to an Azure VM  
 Note the following guidelines and restrictions when deploying databases that have BLOBS stored in FILESTREAM objects:  
  
-   The deployment feature cannot deploy a FILESTREAM-enabled database into a new VM. If FILESTREAM is not enabled in the VM before you run the wizard, the database restore operation will fail and the wizard operation will not be able to complete successfully. To successfully deploy a database that uses FILESTREAM, enable FILESTREAM in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the host VM before launching the wizard. For more information, see [FILESTREAM (SQL Server)](https://msdn.microsoft.com/library/gg471497.aspx).  
  
-   If your database utilizes In-Memory OLTP, you can deploy the database to an Azure VM without any modifications to the database. For more information, see [In-Memory OLTP (In-Memory Optimization)](https://msdn.microsoft.com/library/dn133186\(SQL.120\).aspx).  
  
###  <a name="geography"></a> Considerations for Geographic Distribution of Assets  
 Note that the following assets must be located in the same geographic region:  
  
-   Cloud Service  
  
-   VM Location  
  
-   Data Disk Storage Service  
  
 If the assets listed above are not co-located, the wizard will not be able to complete successfully.  
  
###  <a name="configuration_settings"></a> Wizard Configuration Settings  
 Use the following configuration details to modify settings for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database deployment to an Azure VM.  
  
-   **Default path for the configuration file** - %LOCALAPPDATA%\SQL Server\Deploy to SQL in WA VM\DeploymentSettings.xml  
  
-   **Configuration file structure**  
  
    -   \<DeploymentSettings>  
  
        -   <OtherSettings  
  
            -   TraceLevel="Debug" \<!-- Logging level -->  
  
            -   BackupPath="\\\\[server name]\\[volume]\\" \<!-- The last used path for backup. Used as default in the wizard. -->  
  
            -   CleanupDisabled = False /> \<!-- Wizard will not delete intermediate files and Windows Azure objects (VM, CS, SA). -->  
  
        -   <PublishProfile \<!-- The last used publish profile information. -->  
  
            -   Certificate="12A34B567890123ABCD4EF567A8" \<!-- The certificate for use in the wizard. -->  
  
            -   Subscription="1a2b34c5-67d8-90ef-ab12-xxxxxxxxxxxxx" \<!-- The subscription for use in the wizard. -->  
  
            -   Name="My Subscription" \<!-- The name of the subscription. -->  
  
            -   Publisher="" />  
  
    -   \</DeploymentSettings>  
  
 **Configuration file values**  
  
###  <a name="permissions"></a> Permissions  
 The database being deployed must be in a normal state, the database must be accessible to the user account running the wizard, and the user account must have permissions to perform a backup operation.  
  
##  <a name="launch_wizard"></a> Using the Deploy Database to Windows Azure VM Wizard  
 **To launch the wizard, use the following steps:**  
  
1.  Use SQL Server Management Studio to connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with the database you want to deploy.  
  
2.  In **Object Explorer**, expand the instance name, then expand the **Databases** node.  
  
3.  Right-click the database you want to deploy, select **Tasks**, and then select **Deploy Database to Windows Azure VM...**  
  

  
##  <a name="Introduction"></a> Introduction Page  
 This page describes the **Deploy a SQL Server Database to a Windows Azure VM** wizard.  
  
 **Options**  
  
-   **Do not show this page again.** - Click this check box to stop the Introduction page from being displayed in the future.  
  
-   **Next** - Proceeds to the **Source Settings** page.  
  
-   **Cancel** - Cancels the operation and closes the wizard.  
  
-   **Help** - Launches the MSDN Help topic for the wizard.  
  
##  <a name="Source_settings"></a> Source Settings  
 Use this page to connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that hosts the database you want to deploy to the Windows Azure VM. You will also specify a temporary location for files to be saved from the local machine before they are transferred to Windows Azure. This can be a shared, network location.  
  
 **Options**  
  
-   Click **Connect...** and then specify connection details for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that hosts the database to deploy.  
  
-   Use the **Select Database** drop-down list to specify the database to deploy.  
  
-   In the **Other Settings** field, specify a shared folder that will be accessible to the Windows Azure VM service.  
  
##  <a name="Azure_sign-in"></a> Windows Azure Sign-in  
 Use this page to connect to Windows Azure and provide management certificate or publishing profile details.  
  
 **Options**  
  
-   **Management Certificate** - Use this option to specify a certificate from the local certificate store that matches the management certificate from Windows Azure.  
  
-   **Publishing Profile** - Use this option if you already have a publishing profile downloaded to your computer.  
  
-   **Sign In** - Use this option to sign in to Windows Azure using a Microsoft account - for example, a Live ID or Hotmail account - to generate and download a new management certificate. Note that the number of certificates per subscription is limited.  
  
-   **Subscription** - Select, type, or paste your Windows Azure subscription ID that matches the management certificate from the local certificate store or a publishing profile.  
  
##  <a name="Deployment_settings"></a> Deployment Settings Page  
 Use this page to specify the destination server and to provide details about your new database.  
  
 **Options**  
  
-   **Azure Virtual Machine** - Specify details for the VM that will host the SQL Server database:  
  
-   **Cloud Service name** - Specify the name of the service that hosts the VM. To create a new Cloud Service, specify a name for the new Cloud Service.  
  
-   **Virtual Machine name** - Specify the name of the VM that will host the SQL Server database. To create a new Windows Azure VM, specify a name for the new VM.  
  
-   **Settings** - Use the Settings button to create a new VM to host the SQL Server database. If you are using an existing VM, the information you provide will be used to authenticate your credentials.  
  
-   **Storage account** - Select the storage account from the drop-down list. To create a new storage account, specify a name for the new account. Note that storage accounts associated with an Affinity Group will not be available in the drop-down list.  
  
-   **Target Database** - Specify details for the target database.  
  
-   **Server Connection** - Connection details for the server.  
  
-   **Database** - Specify or confirm the name of a new database. If the database name already exists on the destination SQL Server instance, we suggest that you specify a modified database name.  
  
##  <a name="Summary"></a> Summary Page  
 Use this page to review the specified settings for the operation. To complete the deploy operation using the specified settings, click **Finish**. To cancel the deploy operation and exit the wizard, click **Cancel**.  
  
 There may be manual steps required to deploy database details to the SQL Server database on the Windows Azure VM. These steps will be outlined in detail for you.  
  
##  <a name="Results"></a> Results Page  
 This page reports the success or failure of the deploy operation, showing the results of each action. Any action that encountered an error will have an indication in the **Result** column. Click the link to view a report of the error for that action.  
  
 Click **Finish** to close the wizard.  
  
## See Also  
 [Cloud Adapter for SQL Server](../../database-engine/cloud-adapter-for-sql-server.md)   
 [Database Lifecycle Management](../database-lifecycle-management.md)   
 [Export a Data-tier Application](../data-tier-applications/export-a-data-tier-application.md)   
 [Import a BACPAC File to Create a New User Database](../data-tier-applications/import-a-bacpac-file-to-create-a-new-user-database.md)   
 [Azure SQL Database Backup and Restore](https://msdn.microsoft.com/library/azure/jj650016.aspx)   
 [SQL Server Deployment in Windows Azure Virtual Machines](https://msdn.microsoft.com/library/dn133141.aspx)   
 [Getting Ready to Migrate to SQL Server in Windows Azure Virtual Machines](https://msdn.microsoft.com/library/dn133142.aspx)  
  
  
