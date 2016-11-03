---
# required metadata

title: Configure shared disk cluster for SQL Server on Linux | SQL Server vNext CTP1
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 10-31-2016
ms.topic: article
ms.prod: sql-non-specified
ms.service: 
ms.technology: 
ms.assetid: 

# optional metadata
# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---

# Configure shared disk cluster for SQL Server on Linux

You can configure a shared-storage high-availability cluster with Linux to allow the SQL Server resource to failover from one node to another. In a typical cluster two or more servers are connected to shared storage. The servers are the cluster nodes. Failover clusters provide redundancy to improve recovery time by to allowing resources to failover between multiple nodes. Configuration steps depend on the Linux distribution and clustering solutions. The following table identifies specific steps for validated cluster solutions.

| Distribution | Topic 
| ----- | -----
| Red Hat Enterprise Linux 7.0 | [Configure Red Hat Enterprise Linux 7.0 shared disk cluster for SQL Server](sql-server-linux-shared-disk-cluster-red-hat-7-configure.md)

## Next steps

