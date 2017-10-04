---
title: "Deploy a SQL Server Database to a Microsoft Azure Virtual Machine | Microsoft Docs"
ms.date: "07/29/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.swb.deploymentwizard.deploymentsettings.f1"
  - "sql13.swb.deploymentwizard.sourcesettings.f1"
  - "sql13.swb.deploymentwizard.summary.f1"
  - "sql13.swb.agdashboard.agp9virtualnw.issues.f1"
  - "sql13.swb.deploymentwizard.f1"
  - "sql13.swb.deploymentwizard.progress.f1"
  - "sql13.swb.usevmdialog.f1"
  - "sql13.swb.newvmdialog.f1"
  - "sql13.swb.sqlvmdialog.f1"
  - "sql13.swb.deploymentwizard.results.f1"
  - "sql13.swb.deploymentwizard.azuresignin.f1"
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
caps.latest.revision: 30
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Deploy a SQL Server Database to a Microsoft Azure Virtual Machine
  Use the **Deploy a Database to a Windows Azure VM** wizard to deploy a database from an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in a Windows Azure Virtual Machine (VM). The wizard uses a full database backup operation, so it always copies the complete database schema and the data from a SQL Server user database. The wizard also does all of the Azure VM configuration for you, so no pre-configuration of the VM is required.  
  
 You cannot use the wizard for differential backups. The wizard will not overwrite an existing database that has the same database name. To replace an existing database on the VM, you must first drop the existing database or change the database name. If there is a naming conflict between the database name for an in-flight deploy operation and an existing database on the VM, the wizard will suggest an appended database name for the in-flight database to enable you to complete the operation.  
  
-   [Before You Begin](#before_you_begin)  
  
-   [Limitations and Restrictions](#limitations)  
  
-   [Considerations for Deploying a FILESTREAM-enabled Database](#filestream)  
  
-   [Considerations for Geographic Distribution of Assets](#geography)  
  
-   [Wizard Configuration Settings](#configuration_settings)  
  
-   [Required Permissions](#permissions)  
  
-   [Launching the Wizard](#launch_wizard)  
  
-   [Wizard Pages](#wizard_pages)  
  
> [!NOTE]  
>  For a detailed step-by-step walkthrough of this wizard, see [Migrate a SQL Server database to SQL Server in an Azure VM](https://azure.microsoft.com/en-us/documentation/articles/virtual-machines-migrate-onpremises-database/)  
  
##  <a name="before_you_begin"></a> Before You Begin  
 To complete this wizard, you must be able to provide the following information and have these configuration settings in place:  
  
-   The Microsoft account details associated with your Windows Azure subscription.  
  
-   Your Windows Azure publishing profile.  
  
    > [!CAUTION]  
    >  SQL Server currently supports publishing profile version 2.0. To download the supported version of the publishing profile, see [Download Publishing Profile 2.0](http://go.microsoft.com/fwlink/?LinkId=396421).  
  
-   The management certificate uploaded to your Microsoft Azure subscription.  Create the management certificate with the Powershell Cmdlet [New-SelfSignedCertificate](https://technet.microsoft.com/library/hh848633(v=wps.630)).  Then, upload the management certificate to your Microsoft Azure subscription.  For more information on uploading a management certificate, see [Upload an Azure Management API Management Certificate](https://azure.microsoft.com/en-us/documentation/articles/azure-api-management-certs/).  Sample syntax for creating a management certificate from [Certificates overview for Azure Cloud Services](https://azure.microsoft.com/en-us/documentation/articles/cloud-services-certs-create/): 

    ```powershell  
    $cert = New-SelfSignedCertificate -DnsName yourdomain.cloudapp.net -CertStoreLocation "cert:\LocalMachine\My"
    $password = ConvertTo-SecureString -String "your-password" -Force -AsPlainText
    Export-PfxCertificate -Cert $cert -FilePath ".\my-cert-file.pfx" -Password $password
    ```    

    > [!NOTE]  
    > The MakeCert tool can also be used to create a management certificate; however, MakeCert is now deprecated.  For additional information, see [MakeCert](https://msdn.microsoft.com/library/windows/desktop/aa386968).
   
  -   The management certificate saved into the personal certificate store on the computer where the wizard is running.  
  
-   You must have a temporary storage location that is available to the computer where the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database is hosted. The temporary storage location must also be available to the computer where the wizard is running.  
  
-   If you are deploying the database to an existing VM, the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be configured to listen on a TCP/IP port.  
  
-   Either a Windows Azure VM or Gallery image you plan to use for creation of the VM must have the [Cloud Adapter for SQL Server](http://msdn.microsoft.com/library/82ed0d0f-952d-4d49-aa36-3855a3ca9877) configured and running.  
  
-   You must configure an open endpoint for your [Cloud Adapter for SQL Server](http://msdn.microsoft.com/library/82ed0d0f-952d-4d49-aa36-3855a3ca9877) on the Windows Azure gateway with private port 11435.  
  
 In addition, if you plan to deploy your database into an existing Windows Azure VM, you must also be able to provide:  
  
-   The DNS name of the cloud service that hosts your VM.  
  
-   Administrator credentials for the VM.  
  
-   Credentials with Backup operator privileges on the database you plan to deploy, from the source instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 For more information about running SQL Server in Windows Azure virtual machines, see [Provision a SQL Server virtual machine in the Azure Portal](https://msdn.microsoft.com/library/dn133141.aspx) and [Migrate a SQL Server database to SQL Server in an Azure VM](http://msdn.microsoft.com/library/dn133142.aspx).  
  
 On computers running Windows Server operating systems, you must use the following configuration settings to run this wizard:  
  
-   Turn off Enhanced Security Configuration:  Use Server Manager > Local Server to set Internet Explorer Enhanced Security Configuration (ESC) to **OFF**.  
  
-   Enable JavaScript:  Internet Explorer > Internet Options > Security > Customer Level > Scripting > Active Scripting: **Enable**.  
  
###  <a name="limitations"></a> Limitations and Restrictions  
This deployment feature is for use only with an Azure Storage Account created through the Service Management (Classic) deployment model. For more information regarding Azure deployment models, see [Azure Resource Manager vs. classic deployment](https://azure.microsoft.com/en-us/documentation/articles/resource-manager-deployment-model/).

 The database size limitation for this operation is 1 TB.  
  
 This deployment feature is available in SQL Server Management Studio for [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)].  
  
 This deployment feature is for use only with user databases; deploying system databases is not supported.  
  
 The deployment feature does not support hosted services that are associated with an Affinity Group. For example, storage accounts associated with an Affinity Group cannot be selected for use on the **Deployment Settings** page of this wizard.  
  
 SQL Server database versions that can be deployed to a Windows Azure VM using this wizard:  
  
-   SQL Server 2008  
  
-   SQL Server 2008 R2  
  
-   SQL Server 2012  
  
-   [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]  

- [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]
  
 SQL Server database versions running in a Windows Azure VM database can be deployed to:  
  
-   SQL Server 2012  
  
-   [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]  

- [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]
  
 If there is a naming conflict between the database name for an in-flight deploy operation and an existing database on the VM, the wizard will suggest an appended database name for the in-flight database to enable you to complete the operation.  
  
###  <a name="filestream"></a> Considerations for Deploying a FILESTREAM-enabled Database to an Azure VM  
 Note the following guidelines and restrictions when deploying databases that have BLOBS stored in FILESTREAM objects:  
  
-   The deployment feature cannot deploy a FILESTREAM-enabled database into a new VM. If FILESTREAM is not enabled in the VM before you run the wizard, the database restore operation will fail and the wizard operation will not be able to complete successfully. To successfully deploy a database that uses FILESTREAM, enable FILESTREAM in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the host VM before launching the wizard. For more information, see [FILESTREAM (SQL Server)](http://msdn.microsoft.com/library/gg471497.aspx).  
  
-   If your database utilizes In-Memory OLTP, you can deploy the database to an Azure VM without any modifications to the database. For more information, see [In-Memory OLTP (In-Memory Optimization)](http://msdn.microsoft.com/library/dn133186\(SQL.120\).aspx).  
  
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
  
        -   \<PublishProfile \<!-- The last used publish profile information. -->  
  
            -   Certificate="12A34B567890123ABCD4EF567A8" \<!-- The certificate for use in the wizard. -->  
  
            -   Subscription="1a2b34c5-67d8-90ef-ab12-xxxxxxxxxxxxx" \<!-- The subscription for use in the wizard. -->  
  
            -   Name="My Subscription" \<!-- The name of the subscription. -->  
  
            -   Publisher="" />  
  
    -   \</DeploymentSettings>  
  
 **Configuration file values**  
  
###  <a name="permissions"></a> Permissions  
 The database being deployed must be in a normal state, the database must be accessible to the user account running the wizard, and the user account must have permissions to perform a backup operation.  
  
##  <a name="launch_wizard"></a> Using the Deploy Database to Microsoft Azure VM Wizard  
 **To launch the wizard, use the following steps:**  
  
1.  Use SQL Server Management Studio to connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with the database you want to deploy.  
  
2.  In **Object Explorer**, expand the instance name, then expand the **Databases** node.  
  
3.  Right-click the database you want to deploy, select **Tasks**, and then select **Deploy Database to a Microsoft Azure VM…**  
  
##  <a name="wizard_pages"></a> Wizard Pages  
 The following sections provide additional information about deployment settings and configuration details for this operation.  
  
-   [Introduction](#Introduction)  
  
-   [Source Settings](#Source_settings)  
  
-   [Windows Azure Sign-in](#Azure_sign-in)  
  
-   [Deployment Settings](#Deployment_settings)  
  
-   [Summary](#Summary)  
  
-   [Results](#Results)  
  
##  <a name="Introduction"></a> Introduction 
 This page describes the **Deploy Database to a Microsoft Azure VM** wizard.  
  
-   **Do not show this page again.**  
  Click this check box to stop the Introduction page from being displayed in the future.  
  
-   **Next**  
Proceeds to the **Source Settings** page.  
  
-   **Cancel**  
  Cancels the operation and closes the wizard.  
  
-   **Help**  
Launches the MSDN Help topic for the wizard.  
  
##  <a name="Source_settings"></a> Source Settings  
 Use this page to connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that hosts the database you want to deploy to the Microsoft Azure VM. You will also specify a temporary location for files to be saved from the local machine before they are transferred to Microsoft Azure. This can be a shared, network location.  
 
- **SQL Server**    
Click **Connect** and then specify connection details for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that hosts the database to deploy.  
  
-   **Select Database**  
Use the drop-down list to specify the database to deploy.  
  
-   **Other Settings**  
In the field, specify a shared folder that will be accessible to the Microsoft Azure VM service.  
  
##  <a name="Azure_sign-in"></a> Microsoft Azure Sign-in  
 Sign in to Microsoft Azure with your Microsoft account or your organizational account. Your Microsoft or organizational account is in the format of an email address, such as patc@contoso.com. For more information about Azure credentials, see [Microsoft Account for Organizations FAQ](http://technet.microsoft.com/jj592903) and [Troubleshooting Problems](https://technet.microsoft.com/dn197220).  
  
##  <a name="Deployment_settings"></a> Deployment Settings
 Use this page to specify the destination server and to provide details about your new database.  
  
 **Microsoft Azure Virtual Machine**  
  
-   **Cloud Service name**  
Specify the name of the service that hosts the VM. To create a new Cloud Service, specify a name for the new Cloud Service.  
  
-   **Virtual Machine name**
  Specify the name of the VM that will host the SQL Server database. To create a new Microsoft Azure VM, specify a name for the new VM.  
  
-   **Storage account**  
Select the storage account from the drop-down list. To create a new storage account, specify a name for the new account. Note that storage accounts associated with an Affinity Group will not be available in the drop-down list.  

-   **Settings**  
Use the Settings button to create a new VM to host the SQL Server database. If you are using an existing VM, the information you provide will be used to authenticate your credentials.  
  
**Target Database**
  
-   **SQL instance name**  
Connection details for the server.  
  
-   **Database name**  
Specify or confirm the name of a new database. If the database name already exists on the destination SQL Server instance, we suggest that you specify a modified database name.  
  
##  <a name="Summary"></a> Summary
 Use this page to review the specified settings for the operation. To complete the deploy operation using the specified settings, click **Finish**. To cancel the deploy operation and exit the wizard, click **Cancel**.  Clicking **Finish** will launch the **Deployment Progress** page.  You can also view progress from the log file located `"%LOCALAPPDATA%\SQL Server\Deploy to SQL in WA VM"`.
  
 There may be manual steps required to deploy database details to the SQL Server database on the Windows Azure VM. These steps will be outlined in detail for you.  
  
##  <a name="Results"></a> Results 
 This page reports the success or failure of the deploy operation, showing the results of each action. Any action that encountered an error will have an indication in the **Result** column. Click the link to view a report of the error for that action.  
  
 Click **Finish** to close the wizard.  
  
## See Also  
 [Cloud Adapter for SQL Server](http://msdn.microsoft.com/library/82ed0d0f-952d-4d49-aa36-3855a3ca9877)   
 [Database Lifecycle Management](../../relational-databases/database-lifecycle-management.md)   
 [Export a Data-tier Application](../../relational-databases/data-tier-applications/export-a-data-tier-application.md)   
 [Import a BACPAC File to Create a New User Database](../../relational-databases/data-tier-applications/import-a-bacpac-file-to-create-a-new-user-database.md)   
 [Azure SQL Database Backup and Restore](https://msdn.microsoft.com/library/azure/jj650016.aspx)   
 [SQL Server Deployment in Windows Azure Virtual Machines](http://msdn.microsoft.com/library/dn133141.aspx)   
 [Getting Ready to Migrate to SQL Server in Windows Azure Virtual Machines](http://msdn.microsoft.com/library/dn133142.aspx)  
  
  
