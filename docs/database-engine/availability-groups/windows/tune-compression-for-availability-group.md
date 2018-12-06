---
title: "Tune compression for availability group | Microsoft Docs"
ms.custom: ""
ms.date: "06/22/2016"
ms.prod: sql
ms.technology: high-availability
ms.topic: conceptual
ms.assetid: 7632769c-b246-4766-886f-7c60ec540be8
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Tune compression for availability group
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
By default, SQL Server compresses data streams where appropriate for availability groups. Compression reduces network traffic, increases CPU load, and may induce latency. You must be a member of the sysadmin fixed server role to enable compression. The following table shows when SQL Server uses compression for availability group log streams:

| Scenario | Compression Setting
| ---- | ----
| Synchronous-commit replica | Not compressed
| Asynchronous- commit replicas | Compressed
| During automatic seeding | Not compressed

## Trace flags for availability group compression 

For most scenarios Microsoft does not recommend changing these settings. You can use global trace flags to test changing these settings. SQL Server applies global trace flags to the entire instance. All of the availability groups in the instance will be affected by these settings.  

The following table shows trace flags that will change the default compression behavior for SQL Server. 

Trace flag | Description
------------- | -------------
1462          | Disables log stream compression for Availability Groups with asynchronous replicas. This feature is enabled by default on asynchronous replicas to optimize network bandwidth.
9567          | Enables compression of the data stream for Availability Groups during automatic seeding. During automatic seeding, compression can significantly reduce the transfer time and will increase the load on the processor.
9592          | Enables log stream compression for Availability Groups with synchronous replicas. This feature is disabled by default on synchronous replicas because compression adds latency. Log stream compression is enabled by default for asynchronous replicas.


## Resources


[Database Engine Startup Options](../../../database-engine/configure-windows/database-engine-service-startup-options.md)

[Automatic Seeding](https://msdn.microsoft.com/library/mt735149(SQL.130).aspx)

[Always On Prerequisites](prereqs-restrictions-recommendations-always-on-availability.md) 
