---
title: "Feature Switch (Analytics Platform System)"
description: "Displays information about the two feature switches that are introduced in Analytics Platform System AU7."
author: "mzaman1" 
manager: "craigg"	  
ms.prod: "sql"
ms.technology: "data-warehouse"
ms.topic: "conceptual"
ms.date: "06/27/2018"
ms.author: "murshedz"
ms.reviewer: "martinle"
monikerRange: ">= aps-pdw-2016-au7 || = sqlallproducts-allversions"
---
#Appliance Feature Switch
The **Feature Switch** page displays information about the two feature switches that are introduced in Analytics Platform System 2016-AU7. Use this page to update or enable/disable features and settings in Analytics Platform System. Changing feature switch values requires a service restart.

![DWConig Appliance Feature Switch](media/feature-switch/SQL_Server_PDW_DWConfig_feature_switch.png "DWConig Appliance Feature Switch") 

##AutoStatsEnabled Switch
Controls the auto statistics feature. This feature switch is set to true by default after upgrading to AU7. Any database created after the upgrade will inherit auto creation and asynchronous update of statistics. For existing databases, database administrators can enable auto statistics with [alter database] (/sql/t-sql/statements/alter-database-parallel-data-warehouse). For more information on statistics, see [Statistics](../relational-databases/statistics/statistics.md).

##DmsProcessStopMessageTimeoutInSeconds Switch
Controls the time Data Movement Service (DMS) waits to synchronize on a busy system when a query involving data movement is cancelled. Updating to AU7 sets this value to 900 seconds (15 minutes) by default. The valid range is 0 – 3600 seconds.
