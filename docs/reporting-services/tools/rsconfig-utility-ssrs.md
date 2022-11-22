---
title: "rsconfig Utility | Microsoft Docs"
description: Learn about the rsconfig.exe utility that encrypts and stores report server database connection and account values in the RSReportServer.config file.
ms.date: 03/20/2017
ms.service: reporting-services
ms.subservice: tools


ms.topic: conceptual
helpviewer_keywords: 
  - "connections [Reporting Services], configuring"
  - "rsconfig utility"
  - "report servers [Reporting Services], connections"
  - "command prompt utilities [Reporting Services]"
  - "command prompt utilities [SQL Server], rsconfig"
ms.assetid: 84e45a2f-3ca6-4c16-8259-c15ff49d72ad
author: maggiesMSFT
ms.author: maggies
---
# rsconfig Utility (SSRS)
  The **rsconfig.exe** utility encrypts and stores connection and account values in the RSReportServer.config file. Encrypted values include report server database connection information and account values used for unattended report processing.  
  
## Syntax  
  
```  
  
rsconfig {-?}  
{-cconnection}  
{-eunattendedaccount}  
{-mcomputername}  
{-iinstancename}  
{-sservername}  
{-ddatabasename}  
{-aauthmethod}  
{-uusername}  
{-ppassword}  
{-ttrace}  
```  
  
## Arguments  
  
|Term|Optional/Required|Definition|  
|----------|------------------------|----------------|  
|**-?**|Optional.|Displays the syntax of Rsconfig.exe arguments.|  
|**-c**|Required if **-e** argument is not used.|Specifies the connection string, credentials, and data source values used to connect a report server to the report server database.<br /><br /> This argument does not take a value. However, additional arguments must be specified with it to provide all of the required connection values.<br /><br /> Arguments that you can specify with **-c** include **-m**, **-s**, **-i**,**-d**,**-a**,**-u**,**-p**, and**-t**.|  
|**-e**|Required if **-c** argument is not used.|Specifies the unattended report execution account.<br /><br /> This argument does not take a value. However, you must include additional arguments on the command line to specify that values that are encrypted in the configuration file.<br /><br /> Arguments that you can specify with **-e** include **-u** and **-p**. You can also set **-t**.|  
|**-m**  *computername*|Required if you are configuring a remote report server instance.|Specifies the name of the computer that is hosting the report server. If this argument is omitted, the default is **localhost**.|  
|**-s**  *servername*|Required.|Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that hosts the report server database.|  
|**-i**  *instancename*|Required if you are using named instances.|If you used a named Reporting Services instance, this value specifies the name of the Reporting Services instance.|  
|**-d**  *databasename*|Required.|Specifies the name of the report server database.|  
|**-a**  *authmethod*|Required.|Specifies the authentication method that the report server uses to connect to the report server database. Valid values are **Windows** or **SQL** (this argument is not case-sensitive).<br /><br /> **Windows** specifies that the report server use Windows Authentication.<br /><br /> **SQL** specifies that the report server use SQL Server Authentication.|  
|**-u**  *[domain\\]username*|Required with **-e** Optional with **-c**.|Specifies a user account for the report server database connection or for the unattended account.<br /><br /> For **rsconfig -e**, this argument is required. It must be a domain user account.<br /><br /> For **rsconfig -c** and **-a SQL**, this argument must specify a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.<br /><br /> For **rsconfig -c** and **-a Windows**, this argument may specify a domain user, a built-in account, or service account credentials. If you are specifying a domain account, specify *domain* and *username* in the format *domain\username*. If you are using a built-in account, this argument is optional. If you want to use service account credentials, omit this argument.|  
|**-p**  *password*|Required if **-u** is specified.|Specifies the password to use with the *username* argument. You can set this argument to a blank value if the account does not require a password. This value is case-sensitive for domain accounts.|  
|**-t**|Optional.|Outputs error messages to the trace log. This argument does not take a value. For more information, see [Report Server Service Trace Log](../../reporting-services/report-server/report-server-service-trace-log.md).|  
  
## Permissions  
 You must be a local administrator on the computer that hosts the report server you are configuring.  
  
## File Location  
 Rsconfig.exe is located in **\Program Files\Microsoft SQL Server\110\Tools\Binn**. You can run the utility from any folder on your file system.  
  
## Remarks  
 Rsconfig.exe is used for two purposes:  
  
-   To modify the connection information that a report server uses to connect to a report server database.  
  
-   To configure a special account that the report server uses to log on to a remote database server when other credentials are not available.  
  
You can run the **rsconfig** utility on a local or remote instance of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. You cannot use the **rsconfig** utility to decrypt and view values that are already set.  
  
Before you can run this utility, Windows Management Instrumentation (WMI) must be installed on the computer that you are configuring.  
  
## Examples  
 The following examples illustrate ways of using **rsconfig**.  
  
#### Specifying a Domain User Account  
 This example shows how to configure a report server to use a domain user account when connecting to a local report server database.  
  
```  
rsconfig -c -s <SQLSERVERNAME> -d reportserver -a Windows -u <MYDOMAIN\MYACCOUNT> -p <PASSWORD>  
```  
  
#### Specifying a SQL Server Database User Account  
 This example shows how to configure a report server to use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login to connect to a remote report server database.  
  
```  
rsconfig -c -m <REMOTECOMPUTERNAME> -s <SQLSERVERNAME> -d reportserver -a SQL -u SA -p <SAPASSWORD>  
```  
  
#### Specifying a Built-in Account  
 This example shows how to configure a report server to use a built-in account when connecting to a local report server database. Notice that **-u** is not used. Examples of supported built-in account values include NT AUTHORITY\SYSTEM for Local System and NT AUTHORITY\NETWORKSERVICE for Network Service ( [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[winserver2003](../../includes/winserver2003-md.md)] only).  
  
```  
rsconfig -c -s <SQLSERVERNAME> -d reportserver -a Windows "NT AUTHORITY\SYSTEM"  
```  
  
#### Specifying a Service Account  
 This example shows how to configure a report server to use the Report Server Windows service account and Web service account when connecting to a local report server database. Notice that **-u** is not used and that no account information is specified. When you eliminate account values from the command, the **rsconfig** utility uses integrated security and the service account that each service runs under.  
  
```  
rsconfig -c -s <SQLSERVERNAME> -d reportserver -a Windows  
```  
  
#### Specifying the Unattended Account on a Local Server  
 This example shows how to configure the account used for unattended report execution for reports that do not pass credentials to the external data source. The account must be a Windows domain account. You cannot specify a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login for the user name and password. The account is configured on a local report server instance. Error messages are captured in the trace logs in the ReportingServices\LogFiles folder.  
  
```  
rsconfig -e -u <DOMAIN\ACCOUNT> -p <PASSWORD> -t  
```  
  
#### Specifying the Unattended Account on a Remote Server  
 This example shows how to configure the account on a remote report server instance that is the same version as Rsconfig.exe (for example, the report server and Rsconfig.exe are the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2008 R2 version). Error message information is captured in the trace logs on the remote server.  
  
```  
rsconfig -e -m <REMOTECOMPUTERNAME> -s <SQLSERVERNAME> -u <DOMAIN\ACCOUNT> -p <PASSWORD> -t  
```  
  
## See Also  
 [Configure a Report Server Database Connection  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md)   
 [Configure the Unattended Execution Account &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md)   
 [Reporting Services Report Server &#40;Native Mode&#41;](../../reporting-services/report-server/reporting-services-report-server-native-mode.md)   
 [Store Encrypted Report Server Data &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-store-encrypted-report-server-data.md)   
 [Reporting Services Configuration Files](../../reporting-services/report-server/reporting-services-configuration-files.md)   
 [Report Server Command Prompt Utilities &#40;SSRS&#41;](../../reporting-services/tools/report-server-command-prompt-utilities-ssrs.md)   
 [RsReportServer.config Configuration File](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)  
  
  
