---
title: "rsconfig utility"
description: Learn about the rsconfig.exe utility that encrypts and stores report server database connection and account values in the RSReportServer.config file.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "connections [Reporting Services], configuring"
  - "rsconfig utility"
  - "report servers [Reporting Services], connections"
  - "command prompt utilities [Reporting Services]"
  - "command prompt utilities [SQL Server], rsconfig"
---
# rsconfig utility (SSRS)
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
|**-c**|Required if **-e** argument isn't used.|Specifies the connection string, credentials, and data source values used to connect a report server to the report server database.<br /><br /> This argument doesn't take a value. However, other arguments must be specified with it to provide all of the required connection values.<br /><br /> Arguments that you can specify with **-c** include **-m**, **-s**, **-i**,**-d**,**-a**,**-u**,**-p**, and**-t**.|  
|**-e**|Required if **-c** argument isn't used.|Specifies the unattended report execution account.<br /><br /> This argument doesn't take a value. However, you must include other arguments on the command line to specify that values that are encrypted in the configuration file.<br /><br /> Arguments that you can specify with **-e** include **-u** and **-p**. You can also set **-t**.|  
|**-m**  *computername*|Required if you're configuring a remote report server instance.|Specifies the name of the computer that is hosting the report server. If this argument is omitted, the default is **localhost**.|  
|**-s**  *servername*|Required.|Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that hosts the report server database.|  
|**-i**  *instancename*|Required if you use named instances.|If you used a named Reporting Services instance, this value specifies the name of the Reporting Services instance.|  
|**-d**  *databasename*|Required.|Specifies the name of the report server database.|  
|**-a**  *authmethod*|Required.|Specifies the authentication method that the report server uses to connect to the report server database. Valid values are **Windows** or **SQL** (this argument isn't case-sensitive).<br /><br /> **Windows** specifies that the report server use Windows Authentication.<br /><br /> **SQL** specifies that the report server use SQL Server Authentication.|  
|**-u**  *[domain\\]username*|Required with **-e** Optional with **-c**.|Specifies a user account for the report server database connection or for the unattended account.<br /><br /> For **rsconfig -e**, this argument is required. It must be a domain user account.<br /><br /> For **rsconfig -c** and **-a SQL**, this argument must specify a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sign in.<br /><br /> For **rsconfig -c** and **-a Windows**, this argument might specify a domain user, a built-in account, or service account credentials. If you're specifying a domain account, specify *domain* and *username* in the format *domain\username*. If you use a built-in account, this argument is optional. If you want to use service account credentials, omit this argument.|  
|**-p**  *password*|Required if **-u** is specified.|Specifies the password to use with the *username* argument. You can set this argument to a blank value if the account doesn't require a password. This value is case-sensitive for domain accounts.|  
|**-t**|Optional.|Outputs error messages to the trace log. This argument doesn't take a value. For more information, see [Report Server Service Trace Log](../../reporting-services/report-server/report-server-service-trace-log.md).|  
  
## Permissions  
 You must be a local administrator on the computer that hosts the report server you're configuring.  
  
## File location  
 Rsconfig.exe is located in **\Program Files\Microsoft SQL Server\110\Tools\Binn**. You can run the utility from any folder on your file system.  
  
## Remarks  
 Rsconfig.exe is used for two purposes:  
  
-   To modify the connection information that a report server uses to connect to a report server database.  
  
-   To configure a special account that the report server uses to, sign in to a remote database server when other credentials aren't available.  
  
You can run the **rsconfig** utility on a local or remote instance of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. You can't use the **rsconfig** utility to decrypt and view values that are already set.  
  
Before you can run this utility, Windows Management Instrumentation (WMI) must be installed on the computer that you're configuring.  
  
## Examples  
 The following examples illustrate ways to use **rsconfig**.  
  
#### Specify a domain user account  
 This example shows how to configure a report server to use a domain user account when connecting to a local report server database.  
  
```  
rsconfig -c -s <SQLSERVERNAME> -d reportserver -a Windows -u <MYDOMAIN\MYACCOUNT> -p <PASSWORD>  
```  
  
#### Specify a SQL Server database user account  
 This example shows how to configure a report server to use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sign in to connect to a remote report server database.  
  
```  
rsconfig -c -m <REMOTECOMPUTERNAME> -s <SQLSERVERNAME> -d reportserver -a SQL -u SA -p <SAPASSWORD>  
```  
  
#### Specify a built-in account  
 This example shows how to configure a report server to use a built-in account when connecting to a local report server database. Notice that **-u** isn't used. Examples of supported built-in account values include NT AUTHORITY\SYSTEM for Local System and NT AUTHORITY\NETWORKSERVICE for Network Service ( [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[winserver2003](../../includes/winserver2003-md.md)] only).  
  
```  
rsconfig -c -s <SQLSERVERNAME> -d reportserver -a Windows "NT AUTHORITY\SYSTEM"  
```  
  
#### Specify a service account  
 This example demonstrates how to configure a report server to use the Report Server Windows service account. It also illustrates configuring the Web service account when connecting to a local report server database. Notice that **-u** isn't used and that no account information is specified. When you eliminate account values from the command, the **rsconfig** utility uses integrated security and the service account that each service runs under.  
  
```  
rsconfig -c -s <SQLSERVERNAME> -d reportserver -a Windows  
```  
  
#### Specify the unattended account on a local server  
 This example shows how to configure the account used for unattended report execution for reports that don't pass credentials to the external data source. The account must be a Windows domain account. You can't specify a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sign in for the user name and password. The account is configured on a local report server instance. Error messages are captured in the trace logs in the ReportingServices\LogFiles folder.  
  
```  
rsconfig -e -u <DOMAIN\ACCOUNT> -p <PASSWORD> -t  
```  
  
#### Specify the unattended account on a remote server  
 This example shows how to configure the account on a remote report server instance that is the same version as Rsconfig.exe. For example, the report server and Rsconfig.exe are the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2008 R2 version. Error message information is captured in the trace logs on the remote server.  
  
```  
rsconfig -e -m <REMOTECOMPUTERNAME> -s <SQLSERVERNAME> -u <DOMAIN\ACCOUNT> -p <PASSWORD> -t  
```  
  
## Related content

- [Configure a report server database connection  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md)
- [Configure the unattended execution account &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md)
- [Reporting Services report server &#40;native mode&#41;](../../reporting-services/report-server/reporting-services-report-server-native-mode.md)
- [Store encrypted report server data &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-store-encrypted-report-server-data.md)
- [Reporting Services configuration files](../../reporting-services/report-server/reporting-services-configuration-files.md)
- [Report Server command prompt utilities &#40;SSRS&#41;](../../reporting-services/tools/report-server-command-prompt-utilities-ssrs.md)
- [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)
