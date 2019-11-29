---
title: "Using Transparent Network IP Resolution | Microsoft Docs"
ms.custom: ""
ms.date: "11/28/2019"
ms.prod: sql
ms.prod_service:  connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
author: agaz45
ms.author: v-angazi
---
# Using Transparent Network IP Resolution
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

Transparent Network IP Resolution (TNIR) is a revision of the existing MultiSubnetFailover feature. TNIR affects the connection sequence of the driver in the case where the first resolved IP of the hostname does not respond and there are multiple IPs associated with the hostname. TNIR interacts with MultiSubnetFailover to provide the following three connection sequences:<br />
* 0: One IP is attempted, followed by all IPs in parallel
* 1: All IPs are attemped in parallel
* 2: All IPs are attempted one after another

|TransparentNetworkIPResolution|MultiSubnetFailover|Behavior|
|--------|--------|--------|
|True|True|1|
|True|False|0|
|False|True|1|
|False|False|2| 
TransparentNetworkIPResolution is **True** by default.<br />MultiSubnetFailover is **False** by default.

### Connection String
|Keyword|Default|Values|Description|
|---               |---         |---            |---           |
|MultiSubnetFailover|`False`|`True`, `False`|Enables or disables Multi Subnet Failover|
|TransparentNetworkIPResolution|`True`|`True`, `False`|Enables or disables TNIR|

### TransparentNetworkIPResolution
The equivalent connection properties are:  
-   **SSPROP_INIT_TNIR**  

-   **DBPROP_INIT_PROVIDERSTRING**  

An OLE DB Driver for SQL Server application can use one of the following methods to set the TransparentNetworkIPResolution option:  

 -   **IDBInitialize::Initialize**  
 **IDBInitialize::Initialize** uses the previously configured set of properties to initialize the data source and create the data source object. Specify application intent as a provider property or as part of the extended properties string.  
  
 -   **IDataInitialize::GetDataSource**  
 **IDataInitialize::GetDataSource** takes an input connection string that can contain the **TransparentNetworkIPResolution** keyword.  

-   **IDBProperties::SetProperties**  
To set the **TransparentNetworkIPResolution** property value, call **IDBProperties::SetProperties** passing in the **SSPROP_INIT_TNIR** property with value **VARIANT_TRUE** or **VARIANT_FALSE** or **DBPROP_INIT_PROVIDERSTRING** property with value containing "**TransparentNetworkIPResolution=Yes**" or "**TransparentNetworkIPResolution=No**".
#### Example
```
DBPROP rgPropTNIR;

rgPropTNIR.dwPropertyID = SSPROP_INIT_TNIR;
rgPropTNIR.dwOptions = DBPROPOPTIONS_REQUIRED;
rgPropTNIR.dwStatus = DBPROPSTATUS_OK;
rgPropTNIR.colid = DB_NULLID;
V_VT(&(rgPropTNIR.vValue)) = VT_BOOL;
V_BOOL(&(rgPropTNIR.vValue)) = VARIANT_TRUE;

DBPROPSET PropSet;

PropSet.rgProperties = &rgPropTNIR;
PropSet.cProperties = 1;
PropSet.guidPropertySet = DBPROPSET_SQLSERVERDBINIT;
IDBProperties* pIDBProperties = NULL;
hr = pIDBInitialize->QueryInterface(IID_IDBProperties, (void **)&pIDBProperties);
pIDBProperties->SetProperties(1, &PropSet);
```
For more information about MultiSubnetFailover, see [OLE DB Driver for SQL Server Support for High Availability, Disaster Recovery](./oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery)