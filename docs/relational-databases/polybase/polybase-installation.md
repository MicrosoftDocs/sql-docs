---
title: "PolyBase installation | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "08/31/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine-polybase"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "PolyBase, installation"
ms.assetid: 3a1e64be-9bfc-4408-accd-35990e1a6b52
caps.latest.revision: 25
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
---
# PolyBase installation
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  To install a trial version of  SQL Server, go to [SQL Server evaluations](https://www.microsoft.com/evalcenter/evaluate-sql-server-2016). 
  
## Prerequisites  
  
-   64-bit SQL Server Evaluation edition  
  
-   Microsoft .NET Framework 4.5.  
  
-   Oracle Java SE RunTime Environment (JRE) version 7.51 or higher (64-bit) (Either [JRE](http://www.oracle.com/technetwork/java/javase/downloads/jre8-downloads-2133155.html) or [Server JRE](http://www.oracle.com/technetwork/java/javase/downloads/server-jre8-downloads-2133154.html) will work). Go to [Java SE downloads](http://www.oracle.com/technetwork/java/javase/downloads/index.html). The installer will fail if JRE is not present.  
  
-   Minimum memory: 4GB  
  
-   Minimum hard disk space: 2GB  
  
-   TCP/IP must to be enabled for Polybase to function correctly. TCP/IP is enabled by default on all editions of SQL Server except for the Developer and Express SQL Server editions. For Polybase to function correctly on the Developer and Express editions you must enable TCP/IP connectivity (See [Enable or Disable a Server Network Protocol](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md).)
  
 **Notes**  
  
 PolyBase can be installed on only one SQL Server instance per machine.  
  
## Single Node or PolyBase ScaleOut Group
Before you start installing PolyBase on your SQL Server Instances, it is good to plan out if you want a single node installation or a PolyBase scale out group. 
For a PolyBase Scaleout group, you will need to make sure that: 
- All of the machines are on the same domain.
- You use the same service account and password during installation.
- That your SQL Server Instances can communicate with one another over the network.

Once you have installed PolyBase as either stand alone or in a scaleout group, you cannot change. You will have to uninstall and reinstall the feature to change this setting.

## Install using the installation wizard  
  
1.  Run **SQL Server Installation Center**. Insert SQL Server installation media and double-click **Setup.exe**.  
  
2.  Click **Installation**, then click **New Standalone SQL Server installation or add features**.  
  
3.  On the feature selection page, select **PolyBase Query Service for External Data**.  
  
4.  On the Server Configuration Page, configure the **SQL Server PolyBase Engine Service** and SQL Server PolyBase Data Movement Service to run under the same account.  
  
    > **IMPORTANT!** In a PolyBase scale-out group, PolyBase engine and PolyBase data movement service on all nodes must run under the same domain account.  
    > See Scaling out PolyBase.  
  
5.  On the **PolyBase Configuration Page**, select one of the two options. See [PolyBase scale-out groups](../../relational-databases/polybase/polybase-scale-out-groups.md) for more information.  
  
    -   Use the SQL Server instance as a standalone PolyBase enabled instance.  
  
         Choose this option to use the SQL Server instance as a standalone Head node.  
  
    -   Use the SQL Server instance as part of a PolyBase scale-out group.  Selecting this option opens the firewall to allow incoming connections to the SQL Server Database Engine, SQL Server PolyBase Engine, SQL Server PolyBase Data Movement service and SQL Browser. The firewall is opened to allow incoming connections from other nodes in a PolyBase scale-out group.  
  
         Selecting this option will also enable Microsoft Distributed Transaction Coordinator (MSDTC) firewall connections and modify MSDTC registry settings.  
  
6.  On the **PolyBase Configuration Page**, specify a port range with at least six ports. SQL Server setup will allocate the first six available ports from the range.  
  
##  <a name="installing"></a> Install using a command prompt  
 Use the values in this table to create installation scripts. The two services **SQL Server PolyBase Engine** and **SQL Server PolyBase Data Movement Service** must run under the same account. In a PolyBase scale-out group, PolyBase services on all nodes must run under the same domain account.  
  
|SQL Server component|Parameter and values|Description|  
|--------------------------|--------------------------|-----------------|  
|SQL Server setup control|**Required**<br /><br /> /FEATURES=PolyBase|Selects PolyBase feature.|  
|SQL Server PolyBase Engine|**Optional**<br /><br /> /PBENGSVCACCOUNT|Specifies the account for the engine service. The default is **NT Authority\NETWORK SERVICE**.|  
|SQL Server PolyBase Engine|**Optional**<br /><br /> /PBENGSVCPASSWORD|Specifies the password for the engine service account.|  
|SQL Server PolyBase Engine|**Optional**<br /><br /> /PBENGSVCSTARTUPTYPE|Specifies the startup mode for the PolyBase engine service: Automatic (default) , Disabled,  and Manual|  
|SQL Server PolyBase Data Movement Service|**Optional**<br /><br /> /PBDMSSVCACCOUNT|Specifies the account for data movement service. The default is **NT Authority\NETWORK SERVICE**.|  
|SQL Server PolyBase Data Movement Service|**Optional**<br /><br /> /PBDMSSVCPASSWORD|Specifies the password for the data movement account.|  
|SQL Server PolyBase Data Movement Service|**Optional**<br /><br /> /PBDMSSVCSTARTUPTYPE|Specifies the startup mode for the data movement service: Automatic (default) , Disabled,  and Manual|  
|PolyBase|**Optional**<br /><br /> /PBSCALEOUT|Specifies if the SQL Server instance will be used as a part of PolyBase Scale-out computational group. <br />Supported values: **True**, **False**|  
|PolyBase|**Optional**<br /><br /> /PBPORTRANGE|Specifies a port range with at least 6 ports for PolyBase services. Example:<br /><br /> `/PBPORTRANGE=16450-16460`|  
  
 **Example**  
  
 This shows a sample setup script.  
  
```  
  
Setup.exe /Q /ACTION=INSTALL /IACCEPTSQLSERVERLICENSETERMS /FEATURES=SQLEngine,Polybase   
/INSTANCENAME=MSSQLSERVER /SQLSYSADMINACCOUNTS="\<fabric-domain>\Administrator"   
/INSTANCEDIR="C:\Program Files\Microsoft SQL Server" /PBSCALEOUT=TRUE   
/PBPORTRANGE=16450-16460 /SECURITYMODE=SQL /SAPWD="<StrongPassword>"   
/PBENGSVCACCOUNT="<DomainName>\<UserName>" /PBENGSVCPASSWORD="<StrongPassword>"   
/PBDMSSVCACCOUNT="<DomainName>\<UserName>" /PBDMSSVCPASSWORD="<StrongPassword>"  
  
```  
  
## Post installation notes  
 PolyBase installs three user databases, DWConfiguration, DWDiagnostics, and DWQueue.   These are for PolyBase use and should not be altered or deleted.  
  
### How to confirm installation  
 Run the following command. If PolyBase is installed, returns 1; otherwise, 0.  
  
```tsql  
SELECT SERVERPROPERTY ('IsPolybaseInstalled') AS IsPolybaseInstalled;  
```  
  
### Firewall rules  
 SQL Server PolyBase setup creates the following firewall rules on the machine.  
  
-   SQL Server PolyBase – Database Engine - \<SQLServerInstanceName> (TCP-In)  
  
-   SQL Server PolyBase – PolyBase Services - \<SQLServerInstanceName> (TCP-In)  
  
-   SQL Server PolyBase - SQL Browser - (UDP-In)  
  
 At installation, if you choose to use the SQL Server instance as part of a PolyBase Scale-out Group, these rules are enabled and the firewall is opened to allow incoming connections to the SQL Server Database Engine, SQL Server PolyBase Engine, SQL Server PolyBase Data Movement service and SQL Browser. However, if the Firewall service on the machine is not running during installation, SQL Server setup would fail to enable these rules. In that case, you must start the Firewall service on the machine and enable these rules post-installation.  
  
#### To enable the firewall rules  
  
-   Open the **Control Panel**.  
  
-   Click **System and Security**, and click **Windows Firewall**.  
  
-   Click **Advanced Settings**, and click **Inbound rules**.  
  
-   Right-click the disabled rule, then click **Enable rule**.  
  
### PolyBase service accounts
To change the service accounts for the PolyBase Engine and PolyBase Data Movement Services, uninstall and re-install the PolyBase feature.
   
## Next steps  
 See [PolyBase configuration](../../relational-databases/polybase/polybase-configuration.md).  
  
  
