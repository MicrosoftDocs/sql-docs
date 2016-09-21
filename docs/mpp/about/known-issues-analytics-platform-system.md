---
title: "Known Issues (Analytics Platform System)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: ee570b1e-88b5-4c56-88b3-57da462c0a45
caps.latest.revision: 36
author: BarbKess
---
# Known Issues (Analytics Platform System)
Describes known issues in Analytics Platform System. These are temporary issues that will be fixed or will no longer be applicable in a future release.  
  
## Contents  
  
-   [Appliance Update 3](#au3)  
  
-   [Appliance Update 2](#au2)  
  
-   [Appliance Update 1](#au1)  
  
## <a name="au3"></a>Appliance Update 3  
  
### Database compatibility  
For appliances that are upgraded from AU2 to AU3:  
  
-   A database that is upgraded from AU2 to AU3 will continue to have database compatibility level 110.  
  
-   A database that is created new in AU3 will have database compatibility level 110.  
  
-   Any database that is restored to AU3 will have database compatibility level 120.  
  
For an appliance that has a clean install of AU3, all databases that are created or restored will have database compatibility level 120.  
  
## <a name="au2"></a>Appliance Update 2  
These are the known issues for Appliance Update 2.  
  
### DENY ON DATABASE is blocked  
PDW sometimes executes stored procedures to distribute user actions to the compute nodes. Therefore, the execute permission for an entire database cannot be denied. (For example `DENY EXECUTE ON DATABASE::<name> TO <user>;` will fail.) As a work around, deny the execute permission to user-schemas or specific objects (procedures).  
  
## <a name="au1"></a>Appliance Update 1  
These are the known issues for Appliance Update 1.  
  
### Verify actual data row size before loading data with Integration Services  
**dwloader** and Integration Services use different code paths when loading data into SQL Server PDW. It is possible to use Integration Services to load a row into SQL Server PDW that exceeds the DMS buffer size and will cause a failure when DMS tries to move the row.  
  
To avoid this error, use **dwloader** to load data, or verify the length of your actual data before loading it with Integration Services. For more information, see the **Table, Bytes per row, DMS buffer defined size** description and the examples in [Minimum and Maximum Values &#40;SQL Server PDW&#41;](../sqlpdw/minimum-and-maximum-values-sql-server-pdw.md).  
  
### Statement execution delayed by locks  
SQL Server prevents new lock requests from blocking older statements which are waiting for locks. SQL Server PDW has not fully implemented this process. In SQL Server PDW, continuous requests for new shared locks can sometimes block an older request for an exclusive lock. For example, an **UPDATE** statement requiring an exclusive lock, can be blocked by a series of shared locks granted for **SELECT** statements.  
  
To resolve a blocked process (identified by reviewing the [sys.dm_pdw_waits &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-waits-sql-server-pdw.md) DVM), stop submitting new requests until the exclusive lock has been satisfied.  
  
### Restrictions on ROLAP operations  
In this release of SQL Server PDW, some ROLAP operations are not supported. For more information, see [Connect With SQL Server Analysis Services &#40;SQL Server PDW&#41;](../sqlpdw/connect-with-sql-server-analysis-services-sql-server-pdw.md).  
  
## See Also  
[What's New &#40;Analytics Platform System&#41;](../about/what-s-new-analytics-platform-system.md)  
  
