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
monikerRange: ">= sql-server-2016 || =sqlallproducts-allversions"
---
# Install PolyBase on Windows

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

To install a trial version of SQL Server, go to [SQL Server evaluations](https://www.microsoft.com/evalcenter/evaluate-sql-server-2016). 
   
## Prerequisites  
   
- 64-bit SQL Server Evaluation edition.  
   
- Microsoft .NET Framework 4.5.  

- Oracle Java SE Runtime Environment (JRE). Versions 7 (starting from 7.51) and 8 are supported. [JRE](https://www.oracle.com/technetwork/java/javase/downloads/jre8-downloads-2133155.html) and [Server JRE](https://www.oracle.com/technetwork/java/javase/downloads/server-jre8-downloads-2133154.html) both work. Go to [Java SE downloads](https://www.oracle.com/technetwork/java/javase/downloads/index.html). If JRE isn't present, the installer fails. JRE9 and JRE10 aren't supported.

- Minimum memory: 4 GB. 
   
- Minimum hard-disk space: 2 GB.
  
- Recommended: Minimum of 16-GB RAM.
   
- TCP/IP must be enabled for PolyBase to function correctly. TCP/IP is enabled by default on all editions of SQL Server except for the Developer and Express SQL Server editions. For PolyBase to function correctly on the Developer and Express editions, you must enable TCP/IP connectivity. See [Enable or disable a server network protocol](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md).


> [!NOTE] 
> PolyBase can be installed on only one SQL Server instance per machine.


>[!NOTE] In order to use PolyBase you must have sysadmin or CONTROL SERVER level permissions on the database.

> [!IMPORTANT]
> 
> To use the computation pushdown functionality against Hadoop, the target Hadoop cluster must have the core components of HDFS, YARN and MapReduce, with the job history server enabled. PolyBase submits the pushdown query via MapReduce and pulls status from the job history server. Without either component, the query fails.
  
## Single node or PolyBase scale-out group

Before you install PolyBase on your SQL Server instances, decide whether you want a single node installation or a [PolyBase scale-out group](../../relational-databases/polybase/polybase-scale-out-groups.md).

For a PolyBase scale-out group, make sure that:

- All the machines are on the same domain.
- You use the same service account and password during PolyBase installation.
- Your SQL Server instances can communicate with one another over the network.
- The SQL Server instances are all the same version of SQL Server.

After you install PolyBase either standalone or in a scale-out group, you can't change. To change this setting, you have to uninstall and reinstall the feature.

## Use the installation wizard
   
1. Run the SQL Server setup.exe.   
   
2. Select **Installation**, and then select **New standalone SQL Server installation or add features**.  
   
3. On the Feature Selection page, select **PolyBase Query Service for External Data**.  

   ![PolyBase services](../../relational-databases/polybase/media/install-wizard.png "PolyBase services")  
   
4. On the Server Configuration page, configure the **SQL Server PolyBase Engine Service** and **SQL Server PolyBase Data Movement Service** to run under the same domain account.  
   
   > [!IMPORTANT] 
   >
   >In a PolyBase scale-out group, the PolyBase Engine and PolyBase Data Movement service on all nodes must run under the same domain account. See [PolyBase scale-out groups](#enable).
   
5. On the PolyBase Configuration page, select one of the two options. For more information, see [PolyBase scale-out groups](../../relational-databases/polybase/polybase-scale-out-groups.md).  
   
   - Use the SQL Server instance as a standalone PolyBase-enabled instance.  
   
     Choose this option to use the SQL Server instance as a standalone head node.  
   
   - Use the SQL Server instance as part of a PolyBase scale-out group. This option opens the firewall to allow incoming connections. Connections are allowed for the SQL Server Database Engine, SQL Server PolyBase Engine, SQL Server PolyBase Data Movement service, and the SQL browser. The firewall also allows incoming connections from other nodes in a PolyBase scale-out group.  
   
     This option also enables Microsoft Distributed Transaction Coordinator (MSDTC) firewall connections and modifies MSDTC registry settings.  
   
6. On the PolyBase Configuration page, specify a port range with at least six ports. SQL Server setup  allocates the first six available ports from the range.  

   > [!IMPORTANT]
   >
   > After installation, you must [enable the PolyBase feature](#enable).


##  <a name="installing"></a> Use a command prompt

Use the values in this table to create installation scripts. The SQL Server PolyBase Engine and SQL Server PolyBase Data Movement service must run under the same account. In a PolyBase scale-out group, PolyBase services on all nodes must run under the same domain account.  
   
<!--SQL Server 2016/2017-->
::: moniker range="= sql-server-2016 || = sql-server-2017"

|SQL Server component|Parameter and values|Description|  
|--------------------------|--------------------------|-----------------|  
|SQL Server setup control|**Required**<br /><br /> /FEATURES=PolyBase|Selects PolyBase feature.|  
|SQL Server PolyBase Engine|**Optional**<br /><br /> /PBENGSVCACCOUNT|Specifies the account for the engine service. The default is **NT Authority\NETWORK SERVICE**.|  
|SQL Server PolyBase Engine|**Optional**<br /><br /> /PBENGSVCPASSWORD|Specifies the password for the engine service account.|  
|SQL Server PolyBase Engine|**Optional**<br /><br /> /PBENGSVCSTARTUPTYPE|Specifies the startup mode for the PolyBase Engine: Automatic (default), Disabled, and Manual.|  
|SQL Server PolyBase Data Movement |**Optional**<br /><br /> /PBDMSSVCACCOUNT|Specifies the account for the data movement service. The default is **NT Authority\NETWORK SERVICE**.|  
|SQL Server PolyBase Data Movement |**Optional**<br /><br /> /PBDMSSVCPASSWORD|Specifies the password for the data movement account.|  
|SQL Server PolyBase Data Movement |**Optional**<br /><br /> /PBDMSSVCSTARTUPTYPE|Specifies the startup mode for the data movement service: Automatic (default), Disabled, and Manual.|  
|PolyBase|**Optional**<br /><br /> /PBSCALEOUT|Specifies whether the SQL Server instance is used as a part of a PolyBase scale-out computational group. <br />Supported values: True, False.|  
|PolyBase|**Optional**<br /><br /> /PBPORTRANGE|Specifies a port range with at least six ports for PolyBase services. Example:<br /><br /> `/PBPORTRANGE=16450-16460`|  

::: moniker-end
<!--SQL Server 2019-->
::: moniker range=">= sql-server-ver15 || =sqlallproducts-allversions"

|SQL Server component|Parameter and values|Description|  
|--------------------------|--------------------------|-----------------|  
|SQL Server setup control|**Required**<br /><br /> /FEATURES=PolyBaseCore, PolyBaseJava, PolyBase | PolyBaseCore installs support for all PolyBase features except Hadoop connectivity. PolyBaseJava enables Hadoop connectivity. PolyBase installs both. |  
|SQL Server PolyBase Engine|**Optional**<br /><br /> /PBENGSVCACCOUNT|Specifies the account for the engine service. The default is **NT Authority\NETWORK SERVICE**.|  
|SQL Server PolyBase Engine|**Optional**<br /><br /> /PBENGSVCPASSWORD|Specifies the password for the engine service account.|  
|SQL Server PolyBase Engine|**Optional**<br /><br /> /PBENGSVCSTARTUPTYPE|Specifies the startup mode for the PolyBase Engine: Automatic (default), Disabled, and Manual.|  
|SQL Server PolyBase Data Movement |**Optional**<br /><br /> /PBDMSSVCACCOUNT|Specifies the account for data movement service. The default is **NT Authority\NETWORK SERVICE**.|  
|SQL Server PolyBase Data Movement |**Optional**<br /><br /> /PBDMSSVCPASSWORD|Specifies the password for the data movement account.|  
|SQL Server PolyBase Data Movement |**Optional**<br /><br /> /PBDMSSVCSTARTUPTYPE|Specifies the startup mode for the data movement service: Automatic (default), Disabled, and Manual.|  
|PolyBase|**Optional**<br /><br /> /PBSCALEOUT|Specifies whether the SQL Server instance is used as a part of a PolyBase scale-out computational group. <br />Supported values: True, False.|  
|PolyBase|**Optional**<br /><br /> /PBPORTRANGE|Specifies a port range with at least six ports for PolyBase services. Example:<br /><br /> `/PBPORTRANGE=16450-16460`|  

::: moniker-end

After installation, you must [enable the PolyBase feature](#enable).



**Example**

This example shows a sample setup script.  

```cmd
   
Setup.exe /Q /ACTION=INSTALL /IACCEPTSQLSERVERLICENSETERMS /FEATURES=SQLEngine,PolyBase   
/INSTANCENAME=MSSQLSERVER /SQLSYSADMINACCOUNTS="\<fabric-domain>\Administrator"   
/INSTANCEDIR="C:\Program Files\Microsoft SQL Server" /PBSCALEOUT=TRUE   
/PBPORTRANGE=16450-16460 /SECURITYMODE=SQL /SAPWD="<StrongPassword>"   
/PBENGSVCACCOUNT="<DomainName>\<UserName>" /PBENGSVCPASSWORD="<StrongPassword>"   
/PBDMSSVCACCOUNT="<DomainName>\<UserName>" /PBDMSSVCPASSWORD="<StrongPassword>"  
   
```  

## <a id="enable"></a> Enable PolyBase

After installation, PolyBase must be enabled to access its features. To connect to SQL Server 2019 CTP 2.0, you must enable PolyBase after installation. Use the following Transact-SQL command.


```sql
exec sp_configure @configname = 'polybase enabled', @configvalue = 1;
RECONFIGURE [ WITH OVERRIDE ]  ;
```
The instance then must be restarted.


## Post-installation notes  

PolyBase installs three user databases, DWConfiguration, DWDiagnostics, and DWQueue. These databases are for PolyBase use. Don't alter or delete them.  
   
### <a id="confirminstall"></a> How to confirm installation  

Run the following command. If PolyBase is installed, the return is 1. Otherwise, it's 0.  

```sql  
SELECT SERVERPROPERTY ('IsPolyBaseInstalled') AS IsPolyBaseInstalled;  
```  

### Firewall rules  

SQL Server PolyBase setup creates the following firewall rules on the machine:  
   
- SQL Server PolyBase - Database Engine - \<SQLServerInstanceName> (TCP-In)  
   
- SQL Server PolyBase - PolyBase Services - \<SQLServerInstanceName> (TCP-In)  

- SQL Server PolyBase - SQL Browser - (UDP-In)  
   
At installation, if you use the SQL Server instance as part of a PolyBase scale-out group, these rules are enabled. The firewall opens to allow incoming connections. They're allowed for the SQL Server Database Engine, SQL Server PolyBase Engine, SQL Server PolyBase Data Movement service, and the SQL browser. If the firewall service on the machine isn't running during installation, SQL Server setup fails to enable these rules. In that case, start the firewall service on the machine and enable these rules post-installation.  
   
#### To enable the firewall rules  

1. Open **Control Panel**.  

2. Select **System and Security**, and select **Windows Firewall**.  
   
3. Select **Advanced Settings**, and select **Inbound rules**.  
   
4. Right-click the disabled rule, and then select **Enable rule**.  
   
### PolyBase service accounts

To change the service accounts for the PolyBase Engine and PolyBase Data Movement service, uninstall and reinstall the PolyBase feature.

## Next steps  

See [PolyBase configuration](../../relational-databases/polybase/polybase-configuration.md).
