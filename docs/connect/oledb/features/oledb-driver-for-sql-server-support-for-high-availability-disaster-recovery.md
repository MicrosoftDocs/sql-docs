---
title: "OLE DB Driver for SQL Server Support for High Availability, Disaster Recovery"
description: "Learn about OLE DB Driver for SQL Server support for high availability and disaster recovery when connecting to databases with those features configured."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, davidengel, sunilbs, vbeiranvand
ms.date: 08/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
ms.custom:
  - ignite-2025
ai-usage: ai-assisted
---
# OLE DB Driver for SQL Server Support for High Availability, Disaster Recovery
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  This article discusses *OLE DB Driver for SQL Server* support for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]. For more information about [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], see [Availability Group Listeners, Client Connectivity, and Application Failover &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md), [Creation and Configuration of Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/creation-and-configuration-of-availability-groups-sql-server.md), [Failover Clustering and Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/failover-clustering-and-always-on-availability-groups-sql-server.md), and [Active Secondaries: Readable Secondary Replicas &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md).  
  
 You can specify the availability group listener of a given availability group in the connection string. If an OLE DB Driver for SQL Server application is connected to a database in an availability group that fails over, the original connection is broken, and the application must open a new connection to continue work after the failover.  
  
 If you are not connecting to an availability group listener, and if multiple IP addresses are associated with a hostname, OLE DB Driver for SQL Server will iterate sequentially through all IP addresses associated with DNS entry. This can be time consuming if the first IP address returned by DNS server is not bound to any network interface card (NIC). When connecting to an availability group listener, OLE DB Driver for SQL Server attempts to establish connections to all IP addresses in parallel and if a connection attempt succeeds, the driver will discard any pending connection attempts.  
  
> [!NOTE]  
> Increasing connection timeout and implementing connection retry logic will increase the probability that an application will connect to an availability group. Also, because a connection can fail because of an availability group failover, you should implement connection retry logic, retrying a failed connection until it reconnects.  
  
## Connecting with MultiSubnetFailover

Always specify **MultiSubnetFailover=Yes** when the target is Azure SQL Database, Azure SQL Managed Instance, SQL database in Microsoft Fabric, an Always On availability group listener, or a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Failover Cluster Instance.

When the server name in your connection string resolves to more than one IP address, **MultiSubnetFailover=Yes** tells OLE DB Driver for SQL Server to open connections to all of those addresses at the same time and use the first one that answers. Without it, the driver tries the addresses one at a time. An address that doesn't answer stalls until the operating system's TCP connect timeout expires, which can exhaust the connect timeout before the driver reaches an address that answers. After a failover, the address the driver tries first can be one that no longer serves the database, so a connection that would succeed against another address fails with a timeout instead.

**MultiSubnetFailover=Yes** changes how quickly the client finds the replica that serves the database. It doesn't change how long the server takes to fail over.

**MultiSubnetFailover=Yes** is safe on single-IP targets. When DNS resolves to a single address, the driver makes a single connection attempt, so the setting costs nothing when it isn't needed.

For more information about connection string keywords, see [Using Connection String Keywords with OLE DB Driver for SQL Server](../../oledb/applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md).

Use the following guidelines to connect to a server in an availability group or Failover Cluster Instance:

- Set the **MultiSubnetFailover** connection property to **Yes**.

- To connect to an availability group, specify the availability group listener of the availability group as the server in your connection string.

- You can't use **MultiSubnetFailover** over a protocol other than TCP.

- Connecting to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance configured with more than 64 IP addresses causes a connection failure.

- You can't use **MultiSubnetFailover** with database mirroring. The driver returns an error when the server reports that the database is mirrored. Database mirroring is deprecated in all supported versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Use Always On availability groups instead.

- The type of authentication, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication, Kerberos Authentication, or Windows Authentication, doesn't affect the behavior of an application that uses the **MultiSubnetFailover** connection property.

- You can increase the value of **Connect Timeout** to accommodate failover time and reduce application connection retry attempts. The default is 15 seconds. The same setting is named **Timeout** when you set it through `IDBInitialize::Initialize`, and it maps to the `DBPROP_INIT_TIMEOUT` property. For [Azure SQL Database serverless](/azure/azure-sql/database/serverless-tier-overview) with auto-pause enabled, use a **Connect Timeout** of at least 60 seconds. An auto-paused database resumes on the first connection attempt, and that attempt can fail with error 40613 while the database resumes, so the application must retry. For more information, see [Auto-pause and auto-resume](/azure/azure-sql/database/serverless-tier-auto-pause-resume).

- Distributed transactions aren't supported.

If read-only routing isn't in effect, connecting to a secondary replica location in an availability group fails in the following situations:  

1. If the secondary replica location isn't configured to accept connections.
1. If an application uses **ApplicationIntent=ReadWrite** and the secondary replica location is configured for read-only access.

A connection fails if a primary replica is configured to reject read-only workloads and the connection string contains **ApplicationIntent=ReadOnly**.

### Upgrading from database mirroring

A connection error occurs if the connection string contains both the **MultiSubnetFailover** and **Failover_Partner** keywords. An error also occurs if you use **MultiSubnetFailover** and the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] returns a failover partner response indicating it's part of a database mirroring pair.  

If you upgrade an OLE DB Driver for SQL Server application that currently uses database mirroring to a multi-subnet scenario, remove the **Failover_Partner** connection property and replace it with **MultiSubnetFailover** set to **Yes**. Replace the server name in the connection string with an availability group listener. If a connection string uses **Failover_Partner** and **MultiSubnetFailover=Yes**, the driver generates an error. However, if a connection string uses **Failover_Partner** and **MultiSubnetFailover=No** (or **ApplicationIntent=ReadWrite**), the application uses database mirroring.  

The driver returns an error if you use database mirroring on the primary replica in the availability group, and if you use **MultiSubnetFailover=Yes** in the connection string that connects to a primary replica instead of to an availability group listener.  

### Set MultiSubnetFailover programmatically

The equivalent connection properties are:

- **SSPROP_INIT_MULTISUBNETFAILOVER**
- **DBPROP_INIT_PROVIDERSTRING**

An OLE DB Driver for SQL Server application can use one of the following methods to set the **MultiSubnetFailover** option:

- **IDBInitialize::Initialize**  
  Uses the previously configured set of properties to initialize the data source and create the data source object. Specify **MultiSubnetFailover** as a provider property or as part of the extended properties string.
- **IDataInitialize::GetDataSource**  
  Takes an input connection string that can contain the **MultiSubnetFailover** keyword.
- **IDBProperties::SetProperties**  
  To set the **MultiSubnetFailover** property value, call **IDBProperties::SetProperties** passing in the **SSPROP_INIT_MULTISUBNETFAILOVER** property with value **VARIANT_TRUE** or **VARIANT_FALSE**, or the **DBPROP_INIT_PROVIDERSTRING** property with value containing **MultiSubnetFailover=Yes** or **MultiSubnetFailover=No**.

#### Example

```cpp
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

[!INCLUDE[specify-application-intent_read-only-routing](~/includes/paragraph-content/specify-application-intent-read-only-routing.md)]

## ApplicationIntent

The OLE DB Driver for SQL Server supports the **ApplicationIntent** connection string keyword. For more information about connection string keywords, see [Using Connection String Keywords with OLE DB Driver for SQL Server](../../oledb/applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md).

### Set ApplicationIntent programmatically

The equivalent connection properties are:

- **SSPROP_INIT_APPLICATIONINTENT**
- **DBPROP_INIT_PROVIDERSTRING**

An OLE DB Driver for SQL Server application can use one of the following methods to specify application intent:

- **IDBInitialize::Initialize**  
  Uses the previously configured set of properties to initialize the data source and create the data source object. Specify application intent as a provider property or as part of the extended properties string.
- **IDataInitialize::GetDataSource**  
  Takes an input connection string that can contain the **Application Intent** keyword.
- **IDBProperties::SetProperties**  
  To set the **ApplicationIntent** property value, call **IDBProperties::SetProperties** passing in the **SSPROP_INIT_APPLICATIONINTENT** property with value **ReadWrite** or **ReadOnly**, or the **DBPROP_INIT_PROVIDERSTRING** property with value containing **ApplicationIntent=ReadOnly** or **ApplicationIntent=ReadWrite**.

You can specify application intent in the Application Intent Properties field of the **All** tab in the **Data Link Properties** dialog box.

When you establish implicit connections, the implicit connection uses the application intent setting of the parent connection. Similarly, multiple sessions created from the same data source inherit the data source's application intent setting.

## Related content

- [OLE DB Driver for SQL Server Features](oledb-driver-for-sql-server-features.md)
- [Using connection string keywords with OLE DB Driver for SQL Server](../applications/using-connection-string-keywords-with-oledb-driver-for-sql-server.md)
