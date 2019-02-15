---
title: "Unable to refresh data for a data connection in the workbook | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: ppvt-sharepoint
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Unable to refresh data for a data connection in the workbook
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  For Excel workbooks that contain [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data, Excel Services returns this error if it submits a connection request to a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] server and the request fails.  
  
## Details  
  
|||  
|-|-|  
|Applies to:|[!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint installation|  
|Product Version|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|  
|Cause|See below.|  
|Message Text|Unable to refresh data for a data connection in the workbook. Try again or contact your system administrator. The following connections failed to refresh: [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Data|  
  
## Explanation and Resolution  
 Excel Services cannot connect to or load the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data. Conditions that cause this error to occur include the following:  
  
 **Scenario 1: Service is not started**  
  
 The SQL Server Analysis Services ( [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]) instance is not started. An expired password will cause the service to stop running. For more information about changing the password, see [Configure Power Pivot Service Accounts](../../analysis-services/power-pivot-sharepoint/configure-power-pivot-service-accounts.md) and [Start or Stop a Power Pivot for SharePoint Server](../../analysis-services/power-pivot-sharepoint/start-or-stop-a-power-pivot-for-sharepoint-server.md).  
  
 **Scenario 2a: Opening an earlier version workbook n the server**  
  
 The workbook you are attempting to open might have been created in the SQL Server 2008 R2 version of [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for Excel. Most likely, the Analysis Services data provider that is specified in the data connection string is not present on the computer that is handling the request.  
  
 If this is the case, you will find this message in the ULS log: "Refresh failed for ' [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]t Data' in the workbook '\<URL to workbook>'", followed by "Unable to get a connection".  
  
 To determine the version of the workbook, open it in Excel and check which data provider is specified in the connection string. A SQL Server 2008 R2 workbook uses MSOLAP.4 as its data provider.  
  
 To work around this issue, you can upgrade the workbook. Alternatively, you can install client libraries from the SQL Server 2008 R2 version of Analysis Services on the physical computers running [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint or Excel Services, [Install the Analysis Services OLE DB Provider on SharePoint Servers](http://msdn.microsoft.com/2c62daf9-1f2d-4508-a497-af62360ee859).  
  
 **Scenario 2b: Excel Services is running on an application server that has the wrong version of the client libraries**  
  
 By default, SharePoint Server 2010 installs the SQL Server 2008 version of the Analysis Services OLE DB provider on application servers that run Excel Services. In a farm that supports [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data access, all physical servers running applications that request [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data, such as Excel Services and [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint, must use a later version of the data provider.  
  
 Servers that run [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint get the updated OLE DB data provider automatically. Other servers, such as those running a standalone instance Excel Services without [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint on the same computer, must be patched to use the newer client libraries. For more information, see [Install the Analysis Services OLE DB Provider on SharePoint Servers](http://msdn.microsoft.com/2c62daf9-1f2d-4508-a497-af62360ee859).  
  
 **Scenario 3: Domain controller is unavailable**  
  
 The cause might be that a domain controller is not available to validate the user identity. A domain controller is required by the Claims to Windows Token Service to authenticate the SharePoint user on each connection. The Claims to Windows Token Service does not use cached credentials. It validates the user identity for each connection.  
  
 You can confirm the cause of this error by viewing the SharePoint log file. If the SharePoint logs include the message "Failed to get WindowsIdentity from IClaimsIdentity", the user identity could not be authenticated.  
  
 To work around this problem, join the computer to the same domain as the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] server, or install a domain controller on your local computer. The second solution, installing the domain controller, will require you to create local domain accounts for all services and users. You will need to configure service accounts and SharePoint permissions for the accounts you define.  
  
 Installing a domain controller on your computer is useful if your objective is to use [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint in an offline state. For detailed instructions on how to use [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] offline, see the blog entry for "Taking your [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] server off the network" on [http://www.powerpivotgeek.com](http://go.microsoft.com/fwlink/?LinkId=184241).  
  
 **Scenario 4: Unstable server**  
  
 One or more services might be in an inconsistent state. In some cases, running IISRESET will resolve the problem.  
  
  
