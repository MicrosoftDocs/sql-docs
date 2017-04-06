---
title: "Power Pivot Minimum-Privilege Example - SharePoint 2013 | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "setup-install"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: c1e09e6c-52d3-48ab-8c70-818d5d775087
caps.latest.revision: 12
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Power Pivot Minimum-Privilege Example - SharePoint 2013
  This topic describes an example [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] for SharePoint 2013 configuration with minimum privileges. The configuration utilizes a different account for each of the three components and each account has the minimum level of privileges.  
  
||  
|-|  
|**[!INCLUDE[applies](../../../includes/applies-md.md)]**  SharePoint 2013|  
  
## Summary of Accounts  
 [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] for SharePoint 2013 supports the use of the Network Service account for the Analysis Services service account. The Network Service account is not a supported scenario with SharePoint 2010. For more information on Service accounts, see [Configure Windows Service Accounts and Permissions](http://msdn.microsoft.com/library/ms143504.aspx) (http://msdn.microsoft.com/library/ms143504.aspx).  
  
 The following table summarizes the three accounts used in this example of a minimum privileged configuration.  
  
|Scope|Name|  
|-----------|----------|  
|SharePoint Administrator account|**SPAdmin**|  
|SharePoint Farm account|**SPFarm**|  
|Analysis Services service account|**SPsvc**|  
  
### The SharePoint Administrator account (SpAdmin)  
 **SPAdmin** is a domain account you use to install and configure the farm. It is the account used to run the SharePoint Configuration Wizard and the [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] Configuration Tool for SharePoint 2013.The **SPAdmin** account is the only account that requires local Administrator rights. Before running the [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] Configuration tool, grant the **SPAdmin** account privileges to the SQL Server database instance where SharePoint creates content and configuration databases. To configure the SPAdmin account in a minimum privilege scenario, it should be a member of the roles **securityadmin** and **dbcreator**.  
  
### The Farm account (SPFarm)  
 **SPFarm** is a domain account that the SharePoint Timer service and the web application for Central Administration use to access the SharePoint content database. This account does not need to be a local administrator. The SharePoint configuration wizard grants the proper minimal privilege in the back-end SQL Server database.The minimum SQL Server privilege configuration is membership in the roles **securityadmin** and **dbcreator**.  
  
### The Service Account for Power Pivot Service (SPsvc)  
 If a new SharePoint farm is not configured before you run the [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] Configuration tool, then by default the [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] Configuration tool will create the following:  
  
-   [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] Service application.  
  
-   Excel Services application.  
  
-   Secure Store application.  
  
 The [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] configuration tool configures all three of the service applications in the default application pool. That application pool is typically configured to run as the SPFarm account, which has access to many resources that a service account does not require.To make the environment a minimum-privileged environment, configure a new domain account to be use by the appropriate application pool and web application.  
  
 **To create a new domain account SPsvc to be used as a SharePoint Service account:**  
  
1.  In SharePoint Central Administration, select **Security**.  
  
2.  Select **Configure Service Accounts**  
  
3.  Select **Register new managed account**.  
  
 The **SPSvc** account has no local administrator privileges and SPsvc will not have any privileges in the SharePoint database. The only privileges SPsvc requires is administrative rights to the [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] Instance of the Analysis Services.  
  
 **To configure the appropriate application pool to use the SPsvc account :**  
  
1.  In SharePoint Central Administration, select **Security**.  
  
2.  Select **Configure Service Accounts**.  
  
3.  Select the service application pool used by the [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] Service application. Then select the SPSvc account.  
  
 **To Grant access to the web application with PowerShell:**  
  
1.  Run the SharePoint 2013 Management Shell with administrator privileges.  
  
2.  Run the following PowerShell code:  
  
    ```  
    $webApp = Get-SPWebApplication "http://<servername>"  
    $webApp.GrantAccessToProcessIdentity("DOMAIN\<ServiceAccountName>")  
  
    ```  
  
  