---
title: Connect to appliance Nodes - Analytics Platform System | Microsoft Docs
description: This article explains the various ways to connect to each node in the Analytics Platform System appliance.
author: mzaman1 
manager: craigg
ms.prod: sql
ms.technology: data-warehouse
ms.topic: conceptual
ms.date: 04/17/2018
ms.author: murshedz
ms.reviewer: martinle
---

# Connect to appliance nodes in Analytics Platform System
This article explains the various ways to connect to each node in the Analytics Platform System appliance.  
  
## Connecting with Hadoop  
Before using Hadoop with SQL Server PDW, ask your appliance administrator to install the Java Runtime Environment onto SQL Server PDW. For instructions, see [Configure PolyBase Connectivity to External Data &#40;Analytics Platform System&#41;](configure-polybase-connectivity-to-external-data.md) in the Appliance Operations Guide.  
  
## <a name="ConnectingToIndividualNodes"></a>Connecting to Appliance Nodes  
Each of the appliance nodes is accessed directly only under specific usage scenarios and by specific user types. The following table lists each appliance node and the scenarios under which users will connect directly to that node.  
  
<!-- MISSING LINKS For information on the purpose of each node, see [Understanding SQL Server PDW &#40;SQL Server PDW&#41;](../sqlpdw/understanding-sql-server-pdw-sql-server-pdw.md).  -->  
  
|||  
|-|-|  
|**Node**|**Access Scenarios**|  
|Control node|Use a web browser to access the Admin Console, which runs on the Control node. For more information, see [Monitor the Appliance by Using the Admin Console &#40;Analytics Platform System&#41;](monitor-the-appliance-by-using-the-admin-console.md).<br /><br />All client applications and tools connect to the Control node, regardless of whether the connection is using Ethernet or InfiniBand.<br /><br />To configure an Ethernet connection to the Control node, use the Control node Cluster IP address and port **17001**. For example, "192.168.0.1,17001".<br /><br />To configure an InfiniBand connection to the Control node, use <strong>*appliance_domain*-SQLCTL01</strong> and port **17001**. By using <strong>*appliance_domain*-SQLCTL01</strong>, the appliance DNS server will connect your server to the active InfiniBand network. To configure your non-appliance server to use this, see [Configure InfiniBand Network Adapters](configure-infiniband-network-adapters.md).<br /><br />The appliance administrator connects to the Control node to perform management operations. For example, the appliance administrator performs the following operations from the Control node:<br /><br />Configure Analytics Platform System with the **dwconfig.exe** configuration tool.|  
|Compute node|Compute node connections are directed by the Control node. The IP addresses of Compute nodes are never entered into application commands as parameters.<br /><br />For loading, backup, Remote Table Copy, and Hadoop, SQL Server PDW does send or receive data directly in parallel between the Compute nodes and the non-appliance nodes or servers. These applications connect with SQL Server PDW by connecting to the Control node, and then the Control node directs SQL Server PDW to establish communication between the Compute nodes and the non-appliance server.<br /><br />For example, these data transfer operations happen in parallel with direct connections to the Compute nodes:<br /><br />Loading from the loading server to SQL Server PDW.<br /><br />Backing up a database from SQL Server PDW to the backup server.<br /><br />Restoring a database from the backup server to SQL Server PDW.<br /><br />Querying Hadoop data from SQL Server PDW.<br /><br />Exporting data from SQL Server PDW to an external Hadoop table.<br /><br />Copying a SQL Server PDW table to a remote SMP SQL Server database.|  
  
## Connecting to the Ethernet and InfiniBand Networks  
Remote servers can connect through either the appliance InfiniBand network or through the Ethernet network. For performance reasons, loading servers, backup servers, and servers receiving data via **CREATE REMOTE TABLE AS SELECT** statements, usually transfer data through the appliance InfiniBand network.  
  
You can configure non-appliance servers to belong to your own customer workgroup or domain, and then use your own customer network to connect to those servers and transfer data to them. Also, non-appliance servers connected to the appliance InfiniBand have the option to transfer data to each other over the appliance InfiniBand network; be careful with this because it can slow the performance of SQL Server PDW.  
  
For example, this statement adds network credentials for performing backups through InfiniBand to a backup server that has an InfiniBand IP address 192.168.0.1. The appliance domain is *mypdw*, and the statement is performed from the backup server. Before running this statement, you need to fill in your own parameters.  
  
```  
sqlcmd -S "mypdw-sqlctl01,17001" -U sa -P password -I -Q "exec sp_pdw_add_network_credentials '192.168.0.1', 'mypdw\Administrator', 'password'"  
```  
  
For example, a loading command will start with the following:  
  
```  
dwloader -S mypdw-SQLCTL01  
```  
  
<!-- MISSING LINKS ## See Also  
[Configure an External Windows System To Receive Remote Table Copies Using InfiniBand &#40;SQL Server PDW&#41;](../sqlpdw/configure-an-external-windows-system-to-receive-remote-table-copies-using-infiniband-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  -->  
  
