---
title: "OLE DB Driver for SQL Server | Microsoft Docs"
ms.date: "02/28/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.service: ""
ms.component: "oledb"
ms.reviewer: ""
ms.suite: "sql"
ms.custom: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: e4d4fe39-0090-42a7-8405-6378370d11cb
caps.latest.revision: 43
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
ms.workload: "Active"
---
# OLE DB Driver for SQL Server
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

MSOLEDBSQL, or OLE DB Driver for SQL Server, is a term that has been used interchangeably to refer to OLE DB Driver for SQL Server. 

**For more information and to download the OLE DB Driver for SQL Server, visit [SNAC lifecycle explained](https://blogs.msdn.microsoft.com/sqlreleaseservices/snac-lifecycle-explained/).**

  
 Information on the OLE DB Driver for SQL Server features: 
  
-   [OLE DB Driver for SQL Server Support for LocalDB](../oledb/features/oledb-driver-for-sql-server-support-for-localdb.md)  
  
-   [Metadata Discovery](../oledb/features/metadata-discovery.md)  
  
-   [UTF-16 Support in OLE DB Driver for SQL Server](../oledb/features/utf-16-support-in-oledb-driver-for-sql-server.md)  
  
-   [OLE DB Driver for SQL Server Support for High Availability, Disaster Recovery](../oledb/features/oledb-driver-for-sql-server-support-for-high-availability-disaster-recovery.md)  
  
-   [Accessing Diagnostic Information in the Extended Events Log](../oledb/features/accessing-diagnostic-information-in-the-extended-events-log.md)  
  

 The following topics describe OLE DB Driver for SQL Server behavior changes in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].  
  
-   When calling **ICommandWithParameters::SetParameterInfo**, the value passed to the *pwszName* parameter must be a valid identifier. For more information, see [ICommandWithParameters](../oledb/ole-db-interfaces/icommandwithparameters.md).  

  
## See also  
[Install OLE DB Driver for SQL Server](../oledb/applications/installing-oledb-driver-for-sql-server.md)  
 [OLE DB Driver for SQL Server Features](../oledb/features/oledb-driver-for-sql-server-features.md )  
  
  
