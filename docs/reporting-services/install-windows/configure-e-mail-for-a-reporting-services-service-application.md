---
title: "Configure E-mail for a Reporting Services Service Application | Microsoft Docs"
ms.date: 05/10/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint"

ms.topic: conceptual
ms.assetid: 38fc34a6-aae7-4dde-9ad2-f1eee0c42a9f
author: maggiesMSFT
ms.author: maggies
monikerRange: ">=sql-server-2016 <=sql-server-2016||=sqlallproducts-allversions"
---
# Configure E-mail for a Reporting Services Service Application

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016](../../includes/ssrs-appliesto-2016.md)] [!INCLUDE[ssrs-appliesto-not-pbirsi](../../includes/ssrs-appliesto-not-pbirs.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)]

[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data alerting sends alerts in e-mail messages. To send e-mail you may need to configure your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application and you may need to modify the e-mail delivery extension for the service application. The e-mail settings are also required if you plan to use the e-mail delivery extension for the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] subscription feature.  

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.
  
### To configure e-mail for the shared service  
  
1.  In SharePoint Central Administration, click the **Application Management**.  
  
2.  In the **Service Applications** group, click **Manage service applications**.  
  
3.  In the **Name** list, click the name of your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application.  
  
4.  Click **E-mail Settings** on the **Manage Reporting Services Application** page.  
  
5.  Select **Use SMTP server**.  
  
6.  In the **Outbound SMTP server** box, type the name of an SMTP server.  
  
7.  In the **From address** box, type an e-mail address.  
  
     This address is the sender of all alert e-mail messages.  
  
     The account of the user specified in **From address** must be a managed account that you specified when you configured the application pool for the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application. If you have permission, you can view a list of existing managed accounts on the Service Accounts page in SharePoint Central Administration.  
  
8.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
### NTLM Authentication  
  
1.  If your email environment requires NTLM authentication and does not allow anonymous access, you need to modify the e-mail delivery extension configuration for your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service applications. For example, if you see the following message in the for the **Last Results** on the **Manage Subscriptions** page:subscriptions.  
  
    -   Failure sending mail: The SMTP server requires a secure connection or the client was not authenticated. The server response was: 5.7.1 Client was not authenticatedMail will not be resent.  
  
     Change the **SMTPAuthenticate** to use a value of "2". This value cannot be changed from the user interface. The following PowerShell script example, updates the full configuration for the report server e-mail delivery extension for the service application named "SSRS_TESTAPPLICATION". Note some of the nodes listed in the script can also be set from the user interface, for example the "From" address.  
  
    ```  
    $app=get-sprsserviceapplication |where {$_.name -like "SSRS_TESTAPPLICATION *"}  
    $emailCfg = Get-SPRSExtension -identity $app -ExtensionType "Delivery" -name "Report Server Email" | select -ExpandProperty ConfigurationXml   
    $emailXml = [xml]$emailCfg   
    $emailXml.SelectSingleNode("//SMTPServer").InnerText = "your email server name"  
    $emailXml.SelectSingleNode("//SendUsing").InnerText = "2"  
    $emailXml.SelectSingleNode("//SMTPAuthenticate").InnerText = "2"  
    $emailXml.SelectSingleNode("//From").InnerText = "your FROM email address"  
    Set-SPRSExtension -identity $app -ExtensionType "Delivery" -name "Report Server Email" -ExtensionConfiguration $emailXml.OuterXml  
    ```  
  
2.  If you need to verify the name of your service application, run the **Get-SPRSServiceApplication cmdlet**.  
  
    ```  
    get-sprsserviceapplication  
    ```  
  
3.  The following example will return the current values of the e-mail extension for the service application named "SSRS_TESTAPPLICATION".  
  
    ```  
    $app=get-sprsserviceapplication |where {$_.name -like "SSRSTEST_APPLICATION*"}  
    Get-SPRSExtension -identity $app -ExtensionType "Delivery" -name "Report Server Email" | select -ExpandProperty ConfigurationXml  
    ```  
  
4.  The following example will create a new file named "emailconfig.txt" with the current values of the e-mail extension for the service application named "SSRS_TESTAPPLICATION"  
  
    ```  
    $app=get-sprsserviceapplication |where {$_.name -like "SSRS_TESTAPPLICATION*"}  
    Get-SPRSExtension -identity $app -ExtensionType "Delivery" -name "Report Server Email" | select -ExpandProperty ConfigurationXml | out-file c:\emailconfig.txt  
    ```  
  
  
More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
