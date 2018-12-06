---
title: "Install Distributed Replay from the Command Prompt | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
ms.assetid: ea1171da-f50e-4f16-bedc-5e468a46477f
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Install Distributed Replay from the Command Prompt
  Installing a new instance of Distributed Replay at the command prompt enables you to specify the features to install and how they should be configured. The command prompt installation supports installing, repairing, upgrading, and uninstalling of the Distributed Replay components. When installing through the command prompt, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports full quiet mode by using the /Q parameter.  
  
> [!NOTE]  
>  For local installations, you must run Setup as an administrator. If you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read and execute permissions on the remote share.  
  
## Installation Parameters  
 The list of top-level features include [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], and Tools. The Tools feature will install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Tools, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online, [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], and other shared components. To install the Distributed Replay components, specify the following parameters:  
  
|Component|Parameter|  
|---------------|---------------|  
|Distributed Replay controller|**DREPLAY_CTLR**|  
|Distributed Replay client|**DREPLAY_CLT**|  
|Administration Tool|**Tools**|  
  
> [!IMPORTANT]  
>  After you install Distributed Replay you must create firewall rules on the controller and client computers, and grant each client computer permissions on the target server. For more information, see [Complete the Post-Installation Steps](complete-the-post-installation-steps.md).  
  
 Use the parameters in the following table to develop command line scripts for installation.  
  
|Parameter|Description|Supported Values|  
|---------------|-----------------|----------------------|  
|/CTLRSVCACCOUNT<br /><br /> **Optional**|Service account for the Distributed Replay controller service.|Checks account and password|  
|/CTLRSVCPASSWORD<br /><br /> **Optional**|Password for the Distributed Replay controller service account.|Checks account and password|  
|/CTLRSTARTUPTYPE<br /><br /> **Optional**|Startup type for the Distributed Replay controller service.|Automatic<br /><br /> Disabled<br /><br /> Manual|  
|/CTLRUSERS<br /><br /> **Optional**|Specify which users have permissions for the Distributed Replay controller service.|Set of user account strings using " " (space) for delimiter<br /><br /> **Important**: When you configure the Distributed Replay controller service, you can specify one or more user accounts that will be used to run the Distributed Replay client services. The following is the list of supported accounts:<br /><br /> Domain user account<br /><br /> User created local user account<br /><br /> Administrator<br /><br /> Virtual account and MSA (Managed Service Account)<br /><br /> Network Services, Local Services, and System<br /><br /> <br /><br /> Group accounts (local or domain) and other built-in accounts (like Everyone) are not accepted.|  
|/CLTSVCACCOUNT<br /><br /> **Optional**|Service account for the Distributed Replay client service.|Checks account and password|  
|/CLTSVCPASSWORD<br /><br /> **Optional**|Password for the Distributed Replay client service account.|Checks account and password|  
|/CLTSTARTUPTYPE<br /><br /> **Optional**|Startup type for the Distributed Replay client service.|Automatic<br /><br /> Disabled<br /><br /> Manual|  
|/CLTCTLRNAME<br /><br /> **Optional**|The computer name that the client communicates with for the Distributed Replay Controller service.||  
|/CLTWORKINGDIR<br /><br /> **Optional**|The working directory for the Distributed Replay Client service.|Valid path|  
|/CLTRESULTDIR<br /><br /> **Optional**|The result directory for the Distributed Replay Client service.|Valid path|  
  
### Sample Syntax:  
 **To install the Distributed Replay controller component**  
  
```  
setup /q /ACTION=Install /FEATURES=DREPLAY_CTLR /IAcceptSQLServerLicenseTerms /CTLRUSERS="domain\user1" "domain\user2" /CTLRSVCACCOUNT="domain\svcuser" /CTLRSVCPASSWORD="password" /CTLRSTARTUPTYPE=Automatic  
```  
  
 **To install the Distributed Replay client component**  
  
```  
setup /q /ACTION=Install /FEATURES=DREPLAY_CLT /IAcceptSQLServerLicenseTerms /CLTSVCACCOUNT="domain\svcuser" /CLTSVCPASSWORD="password" /CLTSTARTUPTYPE=Automatic /CLTCTLRNAME=ControllerMachineName /CLTWORKINGDIR="C:\WorkingDir" /CLTRESULTDIR="C:\ResultDir  
```  
  
  
