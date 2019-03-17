---
title: "The data connection uses Windows Authentication and user credentials could not be delegated. The following connections failed to refresh: PowerPivot Data | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: d2006df1-d244-4786-b272-49d8996cc88c
author: minewiskan
ms.author: owend
manager: craigg
---
# The data connection uses Windows Authentication and user credentials could not be delegated. The following connections failed to refresh: PowerPivot Data
  For Excel workbooks that contain PowerPivot data, Excel Services returns this error if it cannot connect to a PowerPivot server instance in SharePoint.  
  
## Details  
  
|||  
|-|-|  
|Applies to|PowerPivot for SharePoint|  
|Product Version|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|  
|Cause|Connection failed when attempting to use a PowerPivot data provider.|  
|Message Text|The data connection uses Windows Authentication and user credentials could not be delegated. The following connections failed to refresh: PowerPivot Data|  
  
## Explanation  
 There are multiple causes for this error message. The common factor behind all of them is that Excel Services cannot get a valid Windows user identity from a claims token in SharePoint. In the case of Excel workbooks that contain PowerPivot data, this error occurs when any of the following conditions exist:  
  
-   The Claims to Windows Token Service is not running. You can confirm the cause of this error by viewing the SharePoint log file. If the SharePoint logs include the message "The pipe endpoint 'net.pipe://localhost/s4u/022694f3-9fbd-422b-b4b2-312e25dae2a2' could not be found on your local machine", the Claims to Windows Token Service is not running. To start it, use Central Administration and then verify the service is running in the Services console application.  
  
-   A domain controller is not available to validate the user identity. The Claims to Windows Token Service does not use cached credentials. It validates the user identity for each connection. You can confirm the cause of this error by viewing the SharePoint log file. If the SharePoint logs include the message "Failed to get WindowsIdentity from IClaimsIdentity", the user identity could not be authenticated.  
  
-   The computers must be members of the same domain or in domains that have a two-way trust relationship.  
  
-   You must use Windows domain user accounts. The accounts must have a Universal Principal Name (UPN).  
  
-   The Excel Services service account must have Active Directory permissions to query the object.  
  
## User Action  
 Use the following instructions to check the status of the Claims to Windows Token Service.  
  
 For all other scenarios, check with your network administrator.  
  
#### Enable Claims to Windows Token Service  
  
1.  In Central Administration, in System Settings, click **Manage services on server**.  
  
2.  Select **Claims to Windows Token Service**, and then click **Start**.  
  
3.  Verify the service is also running in the Services console:  
  
    1.  In Administrative Tools, click Services.  
  
    2.  Start the Claims to Windows Token Service if it is not running.  
  
## See Also  
 [Configure PowerPivot Service Accounts](configure-power-pivot-service-accounts.md)  
  
  
