---
title: "Using Transparent Network IP Resolution"
description: "Using Transparent Network IP Resolution"
author: chris-rossi
ms.author: v-chross
ms.reviewer: v-davidengel
ms.date: "01/02/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
---
# Using Transparent Network IP Resolution
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

## Purpose
Transparent Network IP Resolution (TNIR) is a revision of the existing MultiSubnetFailover feature. TNIR affects the connection sequence of the driver in the case where the first resolved IP of the hostname does not respond and there are multiple IPs associated with the hostname. TNIR interacts with MultiSubnetFailover to provide the following three connection sequences:<br />
* 0: One IP is attempted, followed by all IPs in parallel
* 1: All IPs are attempted in parallel
* 2: All IPs are attempted one after another

|TransparentNetworkIPResolution|MultiSubnetFailover|Behavior|
|--------|--------|--------|
|True|True|1|
|True|False|0|
|False|True|1|
|False|False|2|

## Setting Transparent Network IP Resolution
TransparentNetworkIPResolution is enabled by default. MultiSubnetFailover is disabled by default. The following pages provide more information about setting these properties: 
- [Using Connection String Keywords with OLE DB Driver for SQL Server](..\applications\using-connection-string-keywords-with-oledb-driver-for-sql-server.md)
- [Initialization and Authorization Properties](..\ole-db-data-source-objects\initialization-and-authorization-properties.md)

## See Also 
[OLE DB Driver for SQL Server Support for High Availability, Disaster Recovery](./oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md)
