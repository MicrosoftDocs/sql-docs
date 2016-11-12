---
# required metadata

title: Configure shared disk cluster for SQL Server on Linux | SQL Server vNext CTP1
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 10/27/2016
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 31c8c92e-12fe-4728-9b95-4bc028250d85 

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

# Shared disk cluster for SQL Server on Linux

You can configure and a shared-storage high-availability cluster with Linux to allow the SQL Server instance to failover from one node to another. In a typical cluster two or more servers are connected to shared storage. The servers are the cluster nodes. Failover clusters provide instance level protection to improve recovery time by to allowing SQL Server to failover between two or more nodes. Configuration steps depend on the Linux distribution and clustering solutions. The following table identifies specific steps for validated cluster solutions.

| Distribution | Topic 
| ----- | -----
| Red Hat Enterprise Linux 7.2 | [Configure](sql-server-linux-shared-disk-cluster-red-hat-7-configure.md)<br/>[Operate](sql-server-linux-shared-disk-cluster-red-hat-7-operate.md)
| | 
## Next steps

