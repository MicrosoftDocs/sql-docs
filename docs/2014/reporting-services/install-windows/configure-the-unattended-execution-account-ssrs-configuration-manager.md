---
title: "Configure the Unattended Execution Account (SSRS Configuration Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "no credentials option [Reporting Services]"
  - "security [Reporting Services], unattended report processing"
  - "unattended report processing [Reporting Services]"
  - "credentials [Reporting Services]"
  - "external data sources [Reporting Services]"
  - "accounts [Reporting Services]"
  - "reports [Reporting Services], processing"
ms.assetid: 4e50733e-bd8c-4bf6-8379-98b1531bb9ca
author: markingmyname
ms.author: maghan
manager: kfile
---
# Configure the Unattended Execution Account (SSRS Configuration Manager)
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] provides a special account that is used for unattended report processing and for sending connection requests across the network. The account is used in the following ways:  
  
-   Send connection requests over the network for reports that use database authentication, or connect to external report data sources that do not require or use authentication. For more information, see [Specify Credential and Connection Information for Report Data Sources](../../integration-services/connection-manager/data-sources.md) in SQL Server Books Online.  
  
-   Retrieve external image files that are used in report. If you want to use an image file and the file cannot be accessed through Anonymous access, you can configure the unattended report processing account and grant the account permission to access the file.  
  
 Unattended report processing refers to any report execution process that is triggered by an event (either a schedule-driven event or data refresh event) rather than a user request. The report server uses the unattended report processing account to log on to the computer that hosts the external data source. This account is necessary because the credentials of the Report Server service account are never used to connect to other computers.  
  
> [!IMPORTANT]  
>  Configuring the account is optional. However, if you do not configure it, you will limit your options for connecting to some data sources, and you might not be able to retrieve image files from remote computers. If you do configure the account, you must keep it up to date. Specifically, if you allow a password to expire or the account information is changed in Active Directory, you will encounter the following error the next time a report is processed: "Logon failed (rsLogonFailed) Logon failure: unknown user name or bad password." Proper maintenance of the unattended report processing account is essential, even if you never retrieve external images or send connection requests to external computers. If you configure the account but then find that you are not using it, you can delete it to avoid routine account maintenance tasks.  
  
## How to Configure the Account  
 You must use a domain user account. To serve its intended purpose, this account should be different than the one used to run the Report Server service. Be sure to use an account that has minimum permissions (read-only access with network connection permissions is sufficient) and limited access to just those computers that provide data sources and resources to the report server. For more information, see [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../sql-server/install/reporting-services-configuration-manager-native-mode.md).  
  
 To specify the account, you can use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool or the **rsconfig** utility. The easiest way to configure the unattended execution account is to run the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool and specify credentials in the Execution Account page.  
  
1.  Start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool and connect to the report server instance you want to configure. For instructions, see [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../sql-server/install/reporting-services-configuration-manager-native-mode.md).  
  
2.  On the Execution Account page, select **Specify an execution account**.  
  
3.  Type the account and password, retype the password, and then click **Apply**.  
  
### Using RSCONFIG Utility  
 Another way to set the account is to use the **rsconfig** utility. To specify the account, use the **-e** argument of **rsconfig**. Specifying the **-e** argument for **rsconfig** directs the utility to write the account information to the configuration file. You do not need to specify a path to RSreportserver.config. Follow these steps to configure the account.  
  
1.  Create or select a domain account that has access to computers and servers that provide data or services to a report server. You should use an account that has reduced permissions (for example, read-only permissions).  
  
2.  Open a command prompt: On the **Start** menu, click **Run**, type **cmd**, and then click **OK**.  
  
3.  Type the following command to configure the account on a local report server instance:  
  
     **rsconfig -e -u\<domain/username> -p\<password>**  
  
 **rsconfig -e** supports additional arguments. For more information about syntax and to view command examples, see [rsconfig Utility &#40;SSRS&#41;](../tools/rsconfig-utility-ssrs.md) in SQL Server Books Online.  
  
### How Account Information is Stored  
 When you set the account, the following settings are specified as encrypted values in the RSreportserver.config file on a local or remote report server instance:  
  
```  
<UnattendedExecutionAccount>  
     <UserName></UserName>  
     <Password></Password>  
     <Domain></Domain>  
</UnattendedExecutionAccount>  
```  
  
 Once you set the values, you cannot decrypt them to view the values in plain text. If you mistype the values or forget the values you specified, you must use the Reporting Services Configuration tool or run **rsconfig -e** to start over.  
  
## How to Use the Unattended Report Processing Account  
 To retrieve image files, the report server uses the account automatically and no specific action is required on your part. To use the account to connect to external data sources that provide data to reports, you must specify a **Credential Type** option in the data source properties page of the report data source or shared data source:  
  
-   In Report Manager or on a SharePoint site, select the **Credentials are not required** option.  
  
 The unattended report processing account is used primarily to connect to external servers, and not as a login to database servers. If you want to use the account credentials to log in to a database, you must specify credentials in the connection string. You can specify **Integrated Security=SSPI** if the database server supports Windows integrated security and the account used for unattended report processing has permission to read the database. Otherwise, you must enter the user name and password in the connection string, where it appears in clear text to any user who has permission to edit data source connection properties.  
  
 Although you are not prevented from using the unattended report processing account to retrieve data after the connection is made, doing so is not recommended. The account is supposed to be used for very specific functions. If you use it to retrieve data, you undermine the purpose for which it is intended.  
  
## How to Maintain the Unattended Report Processing Account  
 Once you define the account, you must ensure that the account and password are kept up to date. You can use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to update the configuration settings that store information about this account.  
  
1.  Start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool and connect to the report server instance you want to configure.  
  
2.  On the Execution Account page, verify that **Specify an execution account** is selected.  
  
3.  Type the new account or password, retype the password, and then click **Apply**.  
  
## How to Delete the Unattended Report Processing Account  
 If you are not using the account, you can delete it to avoid routine account maintenance tasks.  
  
1.  Start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool and connect to the report server instance you want to configure.  
  
2.  On the Execution Account page, clear **Specify an execution account**.  
  
3.  Click **Apply**.  
  
 The account information is removed from the RSReportServer.config file.  
  
## See Also  
 [Reporting Services Configuration Manager &#40;del&#41;](../../sql-server/install/reporting-services-configuration-manager-native-mode.md)  
  
  
