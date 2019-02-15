---
title: "Run DQSInstaller.exe to Complete Data Quality Server Installation | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
ms.assetid: 7a8c96e0-1328-4f35-97fc-b6d9cb808bae
author: leolimsft
ms.author: lle
manager: craigg
---
# Run DQSInstaller.exe to Complete Data Quality Server Installation
  To complete the [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] installation, you must run the DQSInstaller.exe file after installing [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. This topic describes how to run the DQSInstaller.exe from the **Start** screen, **Start** menu, Windows Explorer, or Command Prompt; you can choose any of the ways to run the DQSInstaller.exe file.  
  
##  <a name="Prerequisites"></a> Prerequisites  
  
-   You must have selected **Data Quality Services** under **Database Engine Services** on the Feature Selection page in the SQL Server setup while installing SQL Server, and must have completed the SQL Server installation. For more information, see [Install Data Quality Services](install-data-quality-services.md).  
  
-   Your Windows user account must be a member of the sysadmin fixed server role in the SQL Server database engine instance.  
  
-   You must be logged on as a member of the Administrators group on the computer where you are running DQSInstaller.exe.  
  
##  <a name="WindowsExplorer"></a> Run DQSInstaller.exe from Start Screen, Start Menu or Windows Explorer  
  
1.  On the computer where you chose to install [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)], run the DQSInstaller.exe file using any of the following as applicable:  
  
    -   **Start screen**: On the **Start** screen, click **Data Quality Server Installer.**  
  
    -   **Start menu**: On the taskbar, click **Start**, point to **All Programs**, click **Microsoft SQL Server 2014**. Under **Microsoft SQL Server 2014**, click **Data Quality Services**, and then click **Data Quality Server Installer.**  
  
    -   **Windows Explorer**: Locate the DQSInstaller.exe file. If you installed the default instance of SQL Server, the DQSInstaller.exe file will be available at C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Binn. Double-click the DQSInstaller.exe file.  
  
2.  A command prompt window appears that shows the status of the installation. You will notice the following three things:  
  
    1.  The installer creates an installation log file, DQS_install.log, which contains information about the actions performed on running the DQSInstaller.exe file. If you installed the default instance of SQL Server, the DQS_install.log file will be available at C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Log.  
  
    2.  The installer uses the default server collation, SQL_Latin1_General_CP1_CI_AS, for installing [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)].  
  
        > [!IMPORTANT]  
        >  You can provide a different server collation value for installing [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] only if you are running DQSInstaller.exe from command prompt. For more information, see [Run DQSInstaller.exe from Command Prompt](#CommandPrompt) later in this topic.  
  
    3.  The installer checks for any pending restarts on your computer owing to any updates that have been recently installed on your computer. If any pending restarts are found, a message appears to notify you about the same, and you can choose to continue or abort the installation by pressing Y or N respectively. We recommend that if there are any pending restarts, you must abort the installation, restart your computer, and then run the DQSInstaller.exe again.  
  
3.  You are prompted to type a password for the database master key. The database master key is required to encrypt the reference data service provider keys that will be stored in the DQS_MAIN database when you set up reference data providers in [!INCLUDE[ssDQSnoversion](../../includes/ssdqsnoversion-md.md)] (DQS) later.  
  
    > [!IMPORTANT]  
    >  The password must be at least 8 characters long, and must contain characters from three of the following four categories: English upper case letter (A, B, C,... Z), English lowercase letter (a, b, c,... z), Numeral (0, 1, 2,... 9), and nonalphanumeric or special character (~!@#$%^&*()_-+=|\\{}[]:;"'<>,.?/). For example: P@ssword. The installer will prompt you to enter another password if the current password does not match the requirement.  
  
4.  Provide a password, confirm the password, and then press ENTER to continue with the installation.  
  
    > [!IMPORTANT]  
    >  You must retain the password specified for the database master key because you will require it while restoring the DQS databases from a backup in future, if you choose to do so. For more information about restoring DQS databases, see [Backing Up and Restoring DQS Databases](../backing-up-and-restoring-dqs-databases.md).  
  
5.  If a Master Data Services database is present in the same SQL Server instance as [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)], the installer creates a user mapped to the Master Data Services login, and grants it the dqs_administrator role on the DQS_MAIN database. For information about installing Master Data Services and creating a Master Data Services database, see [Install Master Data Services](../../master-data-services/install-windows/install-master-data-services.md).  
  
6.  A completion message is displayed after the installation has completed successfully. Press any key to close the command prompt window.  
  
##  <a name="CommandPrompt"></a> Run DQSInstaller.exe from Command Prompt  
 You can run DQSInstaller.exe from command prompt using the following command-line parameters:  
  
|DQSInstaller.exe Parameter|Description|Sample Syntax|  
|--------------------------------|-----------------|-------------------|  
|-collation|The server collation to be used for installing [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)].<br /><br /> DQS supports only case-insensitive collation. If you specify a case-sensitive collation, the installer attempts to use the case-insensitive version of the specified collation. If there is no case-insensitive version, or if the collation is unsupported by SQL, the [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] installation fails.<br /><br /> If a server collation is not specified, the default collation, SQL_Latin1_General_CP1_CI_AS, is used.|`dqsinstaller.exe -collation <collation_name>`|  
|-upgradedlls|Skips recreating the DQS databases (DQS_MAIN, DQS_PROJECTS, and DQS_STAGING_DATA), and only updates the SQL Common Language Runtime (SQLCLR) assemblies used by DQS in the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] database.<br /><br /> For more information, see [Upgrade SQLCLR Assemblies After .NET Framework Update](upgrade-sqlclr-assemblies-after-net-framework-update.md)|`dqsinstaller.exe -upgradedlls`|  
|-exportkbs|Export all the knowledge bases to a DQS backup file (.dqsb). You must also specify the full path and file name where you want to export all the knowledge bases.<br /><br /> For more information, see [Export and Import DQS Knowledge Bases Using DQSInstaller.exe](export-and-import-dqs-knowledge-bases-using-dqsinstaller-exe.md).|`dqsinstaller.exe -exportkbs <path><filename>`<br /><br /> For example, `dqsinstaller.exe -exportkbs c:\DQSBackup.dqsb`|  
|-importkbs|Import all the knowledge bases from a DQS backup file (.dqsb) after completing the [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] installation. You must also specify the full path and file name from where you want to import all the knowledge bases.<br /><br /> For more information, see [Export and Import DQS Knowledge Bases Using DQSInstaller.exe](export-and-import-dqs-knowledge-bases-using-dqsinstaller-exe.md).|`dqsinstaller.exe -importkbs <path><filename>`<br /><br /> For example, `dqsinstaller.exe -importkbs c:\DQSBackup.dqsb`|  
|-upgrade|Upgrades DQS databases schema. You must use this parameter after you have installed a SQL Server update on a previously configured DQS instance. For more information, see [Upgrade DQS Databases Schema After Installing SQL Server Update](upgrade-dqs-databases-schema-after-installing-sql-server-update.md).|`dqsinstaller.exe -upgrade`|  
|-uninstall|Uninstalls [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] from the current SQL Server instance.<br /><br /> You can also export all the knowledge bases in the existing Data Quality Server installation to a DQS backup file (.dqsb), and then uninstall Data Quality Server. For more information, see [Export and Import DQS Knowledge Bases Using DQSInstaller.exe](export-and-import-dqs-knowledge-bases-using-dqsinstaller-exe.md).<br /><br /> **\*\* Important \*\*** If you uninstall [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] from a SQL server instance using the `-uninstall` command line parameter, all the DQS objects are deleted as part of the uninstall process. You do not have to delete them manually after uninstalling [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] as mentioned in [Remove Data Quality Server Objects](../../sql-server/install/remove-data-quality-server-objects.md).|**To just uninstall Data Quality Server:** <br /> `dqsinstaller.exe -uninstall`<br /><br /> **To export all the knowledge bases to a file, and then uninstall Data Quality Server:** <br /> `dqsinstaller.exe -uninstall <path><filename>` <br />For example, `dqsinstaller.exe -uninstall c:\DQSBackup.dqsb`|  
  
 **To run DQSInstaller.exe from Command Prompt:**  
  
1.  Start Command Prompt.  
  
2.  At the command prompt, change your directory to the location where DQSInstaller.exe is available. If you installed the default instance of SQL Server, the DQSInstaller.exe file will be available at C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Binn:  
  
    ```  
    cd C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Binn  
    ```  
  
3.  At the command prompt, run DQSInstaller.exe with or without command-line parameters:  
  
    -   **Without Command-Line Parameter**: Type `dqsinstaller.exe`, and then press ENTER.  
  
    -   **With Command-Line Parameter**: Type the required command as mentioned in the table above, and then press ENTER.  
  
4.  The required actions are performed based on the specified command. If you just chose to install [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] without any command-line parameters, rest of the steps are same as described in steps 2-6 in the previous section, [Run DQSInstaller.exe from Start Screen, Start Menu or Windows Explorer](run-dqsinstaller-exe-to-complete-data-quality-server-installation.md#WindowsExplorer).  
  
## Next Steps  
  
-   Grant appropriate DQS roles to users based on their work profile. See [Grant DQS Roles to Users](grant-dqs-roles-to-users.md).  
  
-   If [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] will be accessed remotely from [!INCLUDE[ssDQSClient](../../includes/ssdqsclient-md.md)], enable the TCP/IP protocol using SQL Server Configuration Manager on this computer.  
  
-   Make sure that you can access your source data for the DQS operations, and can export the processed data to a table in a database. See [Access Data for the DQS Operations](access-data-for-the-dqs-operations.md).  
  
## See Also  
 [Install Data Quality Services](install-data-quality-services.md)   
 [Upgrade SQLCLR Assemblies After .NET Framework Update](upgrade-sqlclr-assemblies-after-net-framework-update.md)   
 [Export and Import DQS Knowledge Bases Using DQSInstaller.exe](export-and-import-dqs-knowledge-bases-using-dqsinstaller-exe.md)  
  
  
