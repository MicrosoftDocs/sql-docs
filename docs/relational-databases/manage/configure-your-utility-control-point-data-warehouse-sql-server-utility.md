---
title: "Configure Your Utility Control Point Data Warehouse (SQL Server Utility) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
ms.assetid: c2c6f050-8cdb-4b8e-ad38-4aae0a949847
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Configure Your Utility Control Point Data Warehouse (SQL Server Utility)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Data collected by managed instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are stored in the utility management data warehouse (UMDW); the UMDW file name is sysutility_mdw.  
  
 You can configure the UMDW data retention period. For more information, see [Utility Administration &#40;SQL Server Utility&#41;](https://msdn.microsoft.com/library/3e5a00c3-8905-40f0-9ddc-d924df9c2f0d).  
  
 The following configuration settings are not configurable in this release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   UMDW name: Sysutility_mdw.  
  
-   Collection set upload frequency: Every 15 minutes.  
  
 The UMDW directory is configurable: \<System drive>:\Program Files\Microsoft SQL Server\MSSQL10_50.<UCP_Name>\MSSQL\Data\\, where \<System drive> is normally the C:\ drive. The log file, Sysutility_mdw_\<GUID>_LOG, is located in the same directory.  
  
> [!NOTE]  
>  The UMDW (sysutility_mdw) file location can be changed using detach/attach or ALTER DATABASE. We recommend the use of ALTER DATABASE. For more information see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md).  
  
## See Also  
 [SQL Server Utility Features and Tasks](../../relational-databases/manage/sql-server-utility-features-and-tasks.md)  
  
  
