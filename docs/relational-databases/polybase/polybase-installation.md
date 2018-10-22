---
title: "Install PolyBase on Windows | Microsoft Docs"
ms.custom: ""
ms.date: 09/24/2018
ms.prod: sql
ms.reviewer: ""
ms.technology: polybase
ms.topic: conceptual
helpviewer_keywords: 
   - "PolyBase, installation"
author: rothja
ms.author: jroth
manager: craigg
---
# Install PolyBase on Windows

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

To install a trial version of  SQL Server, go to [SQL Server evaluations](https://www.microsoft.com/evalcenter/evaluate-sql-server-2016). 
   
## Prerequisites  
   
- 64-bit SQL Server Evaluation edition  
   
- Microsoft .NET Framework 4.5.  

- Oracle Java SE Runtime Environment (JRE). Versions 7 (starting from 7.51) and 8 are supported (Either [JRE](http://www.oracle.com/technetwork/java/javase/downloads/jre8-downloads-2133155.html) or [Server JRE](http://www.oracle.com/technetwork/java/javase/downloads/server-jre8-downloads-2133154.html) will work). Go to [Java SE downloads](http://www.oracle.com/technetwork/java/javase/downloads/index.html). The installer will fail if JRE is not present. JRE9 and JRE10 are not supported.

- Minimum memory: 4GB  
   
- Minimum hard disk space: 2GB  
- **Recommended:** Minimum of 16GB RAM
   
- TCP/IP must be enabled for Polybase to function correctly. TCP/IP is enabled by default on all editions of SQL Server except for the Developer and Express SQL Server editions. For Polybase to function correctly on the Developer and Express editions you must enable TCP/IP connectivity (See [Enable or Disable a Server Network Protocol](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md).)

- MSVC++ 2012 

**Note**  

PolyBase can be installed on only one SQL Server instance per machine.

> **Important**
>
> If you are going to use the computation pushdown functionality against Hadoop, you will need to ensure that the target Hadoop cluster has the core components of HDFS, Yarn/MapReduce with Jobhistory server enabled. PolyBase submits the pushdown query via MapReduce and pulls status from the JobHistory Server. Without either component the query will fail.
  
## Single Node or PolyBase ScaleOut Group

Before you start installing PolyBase on your SQL Server instances, it is good to plan out if you want a single node installation or a [PolyBase scale-out group](../../relational-databases/polybase/polybase-scale-out-groups.md).

For a PolyBase scale-out group, you will need to make sure that:

- All of the machines are on the same domain.
- You use the same service account and password during PolyBase installation.
- Your SQL Server Instances can communicate with one another over the network.
- The SQL Server Instances are all the same version of SQL Server.

Once you have installed PolyBase as either stand alone or in a scale-out group, you cannot change. You will have to uninstall and reinstall the feature to change this setting.

## Install using the installation wizard  
   
1. Run the SQL Server setup.exe.   
   
2. Click **Installation**, then click **New Standalone SQL Server installation or add features**.  
   
3. On the feature selection page, select **PolyBase Query Service for External Data**.  

 ![PolyBase services](../../relational-databases/polybase/media/install-wizard.png "PolyBase services")  
   
4. On the Server Configuration Page, configure the **SQL Server PolyBase Engine Service** and SQL Server PolyBase Data Movement Service to run under the same domain account.  
   
 > **IMPORTANT!** 
>
>In a PolyBase scale-out group, PolyBase engine and PolyBase data movement service on all nodes must run under the same domain account. See [PolyBase scale-out groups](#Enable)
   
5. On the **PolyBase Configuration Page**, select one of the two options. See [PolyBase scale-out groups](../../relational-databases/polybase/polybase-scale-out-groups.md) for more information.  
   
   - Use the SQL Server instance as a standalone PolyBase enabled instance.  
   
     Choose this option to use the SQL Server instance as a standalone Head Node.  
   
   - Use the SQL Server instance as part of a PolyBase scale-out group.  Selecting this option opens the firewall to allow incoming connections to the SQL Server Database Engine, SQL Server PolyBase Engine, SQL Server PolyBase Data Movement Service and SQL Browser. The firewall is opened to allow incoming connections from other nodes in a PolyBase scale-out group.  
   
     Selecting this option will also enable Microsoft Distributed Transaction Coordinator (MSDTC) firewall connections and modify MSDTC registry settings.  
   
6. On the **PolyBase Configuration Page**, specify a port range with at least six ports. SQL Server setup will allocate the first six available ports from the range.  

  > **IMPORTANT!**
  >
  > After installation, you must [enable the PolyBase feature](#enable).


##  <a name="installing"></a> Install using a command prompt  

Use the values in this table to create installation scripts. The two services **SQL Server PolyBase Engine** and **SQL Server PolyBase Data Movement Service** must run under the same account. In a PolyBase scale-out group, PolyBase services on all nodes must run under the same domain account.  
   
<!--SQL Server 2016/2017-->
::: moniker range="= sql-server-2016 || = sql-server-2017"

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

::: moniker-end
<!--SQL Server 2019-->
::: moniker range=">= sql-server-ver15 || =sqlallproducts-allversions"

|SQL Server component|Parameter and values|Description|  
|--------------------------|--------------------------|-----------------|  
|SQL Server setup control|**Required**<br /><br /> /FEATURES=PolyBaseCore, PolyBaseJava, PolyBase | **PolyBaseCore** installs support for all PolyBase features except Hadoop connectivity. **PolyBaseJava** enables Hadoop connectivity. **PolyBase** installs both. |  
|SQL Server PolyBase Engine|**Optional**<br /><br /> /PBENGSVCACCOUNT|Specifies the account for the engine service. The default is **NT Authority\NETWORK SERVICE**.|  
|SQL Server PolyBase Engine|**Optional**<br /><br /> /PBENGSVCPASSWORD|Specifies the password for the engine service account.|  
|SQL Server PolyBase Engine|**Optional**<br /><br /> /PBENGSVCSTARTUPTYPE|Specifies the startup mode for the PolyBase engine service: Automatic (default) , Disabled,  and Manual|  
|SQL Server PolyBase Data Movement Service|**Optional**<br /><br /> /PBDMSSVCACCOUNT|Specifies the account for data movement service. The default is **NT Authority\NETWORK SERVICE**.|  
|SQL Server PolyBase Data Movement Service|**Optional**<br /><br /> /PBDMSSVCPASSWORD|Specifies the password for the data movement account.|  
|SQL Server PolyBase Data Movement Service|**Optional**<br /><br /> /PBDMSSVCSTARTUPTYPE|Specifies the startup mode for the data movement service: Automatic (default) , Disabled,  and Manual|  
|PolyBase|**Optional**<br /><br /> /PBSCALEOUT|Specifies if the SQL Server instance will be used as a part of PolyBase Scale-out computational group. <br />Supported values: **True**, **False**|  
|PolyBase|**Optional**<br /><br /> /PBPORTRANGE|Specifies a port range with at least 6 ports for PolyBase services. Example:<br /><br /> `/PBPORTRANGE=16450-16460`|  

::: moniker-end

After installation, you must [enable the PolyBase feature](#enable).



**Example**

This shows a sample setup script.  

```cmd
   
Setup.exe /Q /ACTION=INSTALL /IACCEPTSQLSERVERLICENSETERMS /FEATURES=SQLEngine,Polybase   
/INSTANCENAME=MSSQLSERVER /SQLSYSADMINACCOUNTS="\<fabric-domain>\Administrator"   
/INSTANCEDIR="C:\Program Files\Microsoft SQL Server" /PBSCALEOUT=TRUE   
/PBPORTRANGE=16450-16460 /SECURITYMODE=SQL /SAPWD="<StrongPassword>"   
/PBENGSVCACCOUNT="<DomainName>\<UserName>" /PBENGSVCPASSWORD="<StrongPassword>"   
/PBDMSSVCACCOUNT="<DomainName>\<UserName>" /PBDMSSVCPASSWORD="<StrongPassword>"  
   
```  

## <a id="enable"></a> Enable PolyBase

Once you are done with the installation, Polybase must be enabled to access it's features. connect to SQL Server 2019 CTP 2.0, you must enable PolyBase after installation using the following Transact-SQL command:


```sql
exec sp_configure @configname = 'polybase enabled', @configvalue = 1;
RECONFIGURE [ WITH OVERRIDE ]  ;
```
The instance then needs to be **restarted** 


## Post installation notes  

PolyBase installs three user databases, DWConfiguration, DWDiagnostics, and DWQueue.   These are for PolyBase use and should not be altered or deleted.  
   
### <a id="confirminstall"></a> How to confirm installation  

Run the following command. If PolyBase is installed, returns 1; otherwise, 0.  

```sql  
SELECT SERVERPROPERTY ('IsPolybaseInstalled') AS IsPolybaseInstalled;  
```  

### Firewall rules  

SQL Server PolyBase setup creates the following firewall rules on the machine.  
   
- SQL Server PolyBase – Database Engine - \<SQLServerInstanceName> (TCP-In)  
   
- SQL Server PolyBase – PolyBase Services - \<SQLServerInstanceName> (TCP-In)  

- SQL Server PolyBase - SQL Browser - (UDP-In)  
   
At installation, if you choose to use the SQL Server instance as part of a PolyBase scale-out group, these rules are enabled and the firewall is opened to allow incoming connections to the SQL Server Database Engine, SQL Server PolyBase Engine, SQL Server PolyBase Data Movement service and SQL Browser. However, if the Firewall service on the machine is not running during installation, SQL Server setup will fail to enable these rules. In that case, you must start the Firewall service on the machine and enable these rules post-installation.  
   
#### To enable the firewall rules  

- Open the **Control Panel**.  

- Click **System and Security**, and click **Windows Firewall**.  
   
- Click **Advanced Settings**, and click **Inbound rules**.  
   
- Right-click the disabled rule, then click **Enable rule**.  
   
### PolyBase service accounts

To change the service accounts for the PolyBase Engine and PolyBase Data Movement Services, uninstall and re-install the PolyBase feature.

## Next steps  

See [PolyBase configuration](../../relational-databases/polybase/polybase-configuration.md).
