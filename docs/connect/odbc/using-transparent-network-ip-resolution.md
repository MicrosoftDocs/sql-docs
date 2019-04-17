---
title: "Using Transparent Network IP Resolution | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: d255208f-d486-4ad3-8080-61c6e0261825
author: MightyPen
ms.author: genemi
manager: craigg
---
# Using Transparent Network IP Resolution
[!INCLUDE[Driver_ODBC_Download](../../includes/driver_odbc_download.md)]

TransparentNetworkIPResolution is a revision of the existing MultiSubnetFailover feature, available in Microsoft ODBC Driver 13.1 for SQL Server, that affects the connection sequence of the driver in the case where the first resolved IP of the hostname does not respond and there are multiple IPs associated with the hostname. It interacts with MultiSubnetFailover to provide the following three connection sequences:

* 0: One IP is attempted, followed by all IPs in parallel
* 1: All IPs are attempted in parallel
* 2: All IPs are attempted one after another

|TransparentNetworkIPResolution|MultiSubnetFailover|Behaviour|
|:-:|:-:|:-:|
|(default)|(default)|0|
|(default)|Enabled|1|
|(default)|Disabled|0|
|Enabled|(default)|0|
|Enabled|Enabled|1|
|Enabled|Disabled|0|
|Disabled|(default)|2|
|Disabled|Enabled|1|
|Disabled|Disabled|2|

The `TransparentNetworkIPResolution` connection string and DSN keyword controls this setting at the connection-string level. The default is enabled.

Keyword|Values|Default
-|-|-
`TransparentNetworkIPResolution`|`Yes`, `No`|`Yes`

The `SQL_COPT_SS_TNIR` pre-connection attribute allows an application to control this setting programmatically:

Connection Attribute|	Size/Type|	Default| Value|	Description
-|-|-|-|-
`SQL_COPT_SS_TNIR` (1249)| `SQL_IS_INTEGER` or `SQL_IS_UINTEGER`| `SQL_IS_ON`(1), `SQL_IS_OFF`(0)|`SQL_IS_ON`|Enables or disables TNIR.

For more information about MultiSubnetFailover, see [ODBC Driver on Linux and macOS - High Availability and Disaster Recovery](../../connect/odbc/linux-mac/odbc-driver-on-linux-support-for-high-availability-disaster-recovery.md)
--------------------------------------------------
## See Also  
* [Microsoft ODBC Driver for SQL Server on Windows](../../connect/odbc/windows/microsoft-odbc-driver-for-sql-server-on-windows.md)
* [SQL Server Multi-Subnet Clustering (SQL Server)](https://msdn.microsoft.com/library/ff878716.aspx#RelatedContent)
