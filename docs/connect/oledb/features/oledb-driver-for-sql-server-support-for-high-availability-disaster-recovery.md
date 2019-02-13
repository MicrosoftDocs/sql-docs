---
title: "Support for High Availability, Disaster Recovery | Microsoft Docs"
description: "Support for High Availability, Disaster Recovery"
ms.custom: ""
ms.date: "06/12/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Support for High Availability, Disaster Recovery
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  This article discusses *OLE DB Driver for SQL Server* support for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]. For more information about [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], see [Availability Group Listeners, Client Connectivity, and Application Failover &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md), [Creation and Configuration of Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/creation-and-configuration-of-availability-groups-sql-server.md), [Failover Clustering and AlwaysOn Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/failover-clustering-and-always-on-availability-groups-sql-server.md), and [Active Secondaries: Readable Secondary Replicas &#40;AlwaysOn Availability Groups&#41;](../../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md).  
  
 You can specify the availability group listener of a given availability group in the connection string. If an OLE DB Driver for SQL Server application is connected to a database in an availability group that fails over, the original connection is broken, and the application must open a new connection to continue work after the failover.  
  
 If you are not connecting to an availability group listener, and if multiple IP addresses are associated with a hostname, OLE DB Driver for SQL Server will iterate sequentially through all IP addresses associated with DNS entry. This can be time consuming if the first IP address returned by DNS server is not bound to any network interface card (NIC). When connecting to an availability group listener, OLE DB Driver for SQL Server attempts to establish connections to all IP addresses in parallel and if a connection attempt succeeds, the driver will discard any pending connection attempts.  
  
> [!NOTE]  
> Increasing connection timeout and implementing connection retry logic will increase the probability that an application will connect to an availability group. Also, because a connection can fail because of an availability group failover, you should implement connection retry logic, retrying a failed connection until it reconnects.  
  
## Connecting With MultiSubnetFailover  
 Always specify **MultiSubnetFailover=Yes** when connecting to a SQL Server Always On Availability Group listener or [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Failover Cluster Instance. **MultiSubnetFailover** enables faster failover for all Always On Availability Groups and Failover Cluster Instances in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], and will significantly reduce failover time for single and multi-subnet Always On topologies. During a multi-subnet failover, the client will attempt connections in parallel. During a subnet failover, OLE DB Driver for SQL Server will retry the TCP connection.  
  
 The **MultiSubnetFailover** connection property indicates that the application is being deployed in an availability group or Failover Cluster Instance, and that OLE DB Driver for SQL Server will try to connect to the database on the primary [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance by trying to connect to all the IP addresses. When **MultiSubnetFailover=Yes** is specified for a connection, the client retries TCP connection attempts faster than the operating system's default TCP retransmit intervals. This enables faster reconnection after failover of either an Always On Availability Group or a Failover Cluster Instance, and is applicable to both single- and multi-subnet Availability Groups and Failover Cluster Instances.  
  
 For more information about connection string keywords, see [Using Connection String Keywords with OLE DB Driver for SQL Server](../../oledb/applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md).  
  
 Specifying **MultiSubnetFailover=Yes** when connecting to something other than an availability group listener or Failover Cluster Instance may result in a negative performance impact, and is not supported.  
  
 Use the following guidelines to connect to a server in an availability group or Failover Cluster Instance:  
  
-   Use the **MultiSubnetFailover** connection property when connecting to a single subnet or multi-subnet; it will improve performance for both.  
  
-   To connect to an availability group, specify the availability group listener of the availability group as the server in your connection string.  
  
-   Connecting to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance configured with more than 64 IP addresses will cause a connection failure.  
  
-   Behavior of an application that uses the **MultiSubnetFailover** connection property is not affected based on the type of authentication: [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication, Kerberos Authentication, or Windows Authentication.  
  
-   You can increase the value of **loginTimeout** to accommodate for failover time and reduce application connection retry attempts.  
  
-   Distributed transactions are not supported.  
  
If read-only routing is not in effect, connecting to a secondary replica location in an availability group will fail in the following situations:  
  
1.  If the secondary replica location is not configured to accept connections.  
  
2.  If an application uses **ApplicationIntent=ReadWrite** (discussed below) and the secondary replica location is configured for read-only access.  
  
A connection will fail if a primary replica is configured to reject read-only workloads and the connection string contains **ApplicationIntent=ReadOnly**.  
  
## Upgrading to Use Multi-Subnet Clusters from Database Mirroring  
A connection error will occur if the **MultiSubnetFailover** and **Failover_Partner** connection keywords are present in the connection string. An error will also occur if **MultiSubnetFailover** is used and the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] returns a failover partner response indicating it is part of a database mirroring pair.  
  
If you upgrade an OLE DB Driver for SQL Server application that currently uses database mirroring to a multi-subnet scenario, you should remove the **Failover_Partner** connection property and replace it with **MultiSubnetFailover** set to **Yes** and replace the server name in the connection string with an availability group listener. If a connection string uses **Failover_Partner** and **MultiSubnetFailover=Yes**, the driver will generate an error. However, if a connection string uses **Failover_Partner** and **MultiSubnetFailover=No** (or **ApplicationIntent=ReadWrite**), the application will use database mirroring.  
  
The driver will return an error if database mirroring is used on the primary database in the availability group, and if **MultiSubnetFailover=Yes** is used in the connection string that connects to a primary database instead of to an availability group listener.  


[!INCLUDE[specify-application-intent_read-only-routing](~/includes/paragraph-content/specify-application-intent-read-only-routing.md)]


## OLE DB  
The OLE DB Driver for SQL Server supports both the **ApplicationIntent** and the **MultiSubnetFailover** keywords.   
  
The two OLE DB connection string keywords were added to support [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] in OLE DB Driver for SQL Server:  
  
-   **ApplicationIntent** 
-   **MultiSubnetFailover**  
  
 For more information about connection string keywords in OLE DB Driver for SQL Server, see [Using Connection String Keywords with OLE DB Driver for SQL Server](../../oledb/applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md).  

### Application Intent 

The equivalent connection properties are:  
  
-   **SSPROP_INIT_APPLICATIONINTENT**  
  
-   **DBPROP_INIT_PROVIDERSTRING**  
  
An OLE DB Driver for SQL Server application can use one of the methods to specify application intent:  
  
 -   **IDBInitialize::Initialize**  
 **IDBInitialize::Initialize** uses the previously configured set of properties to initialize the data source and create the data source object. Specify application intent as a provider property or as part of the extended properties string.  
  
 -   **IDataInitialize::GetDataSource**  
 **IDataInitialize::GetDataSource** takes an input connection string that can contain the **Application Intent** keyword.  
  
 -   **IDBProperties::SetProperties**  
 To set the **ApplicationIntent** property value, call **IDBProperties::SetProperties** passing in the **SSPROP_INIT_APPLICATIONINTENT** property with value "**ReadWrite**" or "**ReadOnly**" or **DBPROP_INIT_PROVIDERSTRING** property with value containing "**ApplicationIntent=ReadOnly**" or "**ApplicationIntent=ReadWrite**".  
  
You can specify application intent in the Application Intent Properties field of the All tab in the **Data Link Properties** dialog box.  
  
When implicit connections are established, the implicit connection will use the application intent setting of the parent connection. Similarly, multiple sessions created from the same data source will inherit the data source's application intent setting.  
  
### MultiSubnetFailover

The equivalent connection properties are:  
  
-   **SSPROP_INIT_MULTISUBNETFAILOVER**  
  
-   **DBPROP_INIT_PROVIDERSTRING**  

An OLE DB Driver for SQL Server application can use one of the following methods to set the MultiSubnetFailover option:  

 -   **IDBInitialize::Initialize**  
 **IDBInitialize::Initialize** uses the previously configured set of properties to initialize the data source and create the data source object. Specify application intent as a provider property or as part of the extended properties string.  
  
 -   **IDataInitialize::GetDataSource**  
 **IDataInitialize::GetDataSource** takes an input connection string that can contain the **MultiSubnetFailover** keyword.  

-   **IDBProperties::SetProperties**  
To set the **MultiSubnetFailover** property value, call **IDBProperties::SetProperties** passing in the **SSPROP_INIT_MULTISUBNETFAILOVER** property with value **VARIANT_TRUE** or **VARIANT_FALSE** or **DBPROP_INIT_PROVIDERSTRING** property with value containing "**MultiSubnetFailover=Yes**" or "**MultiSubnetFailover=No**".

#### Example

```
DBPROP rgPropMultisubnet;

rgPropMultisubnet.dwPropertyID = SSPROP_INIT_MULTISUBNETFAILOVER;
rgPropMultisubnet.dwOptions = DBPROPOPTIONS_REQUIRED;
rgPropMultisubnet.dwStatus = DBPROPSTATUS_OK;
rgPropMultisubnet.colid = DB_NULLID;
V_VT(&(rgPropMultisubnet.vValue)) = VT_BOOL;
V_BOOL(&(rgPropMultisubnet.vValue)) = VARIANT_TRUE;

DBPROPSET PropSet;

PropSet.rgProperties = &rgPropMultisubnet;
PropSet.cProperties = 1;
PropSet.guidPropertySet = DBPROPSET_SQLSERVERDBINIT;
IDBProperties* pIDBProperties = NULL;
hr = pIDBInitialize->QueryInterface(IID_IDBProperties, (void **)&pIDBProperties);
pIDBProperties->SetProperties(1, &PropSet);
```

## See Also  
 [OLE DB Driver for SQL Server Features](../../oledb/features/oledb-driver-for-sql-server-features.md)    
 [Using Connection String Keywords with OLE DB Driver for SQL Server](../../oledb/applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md)  
  
  
