---
title: "Running Stored Procedures (OLE DB) | Microsoft Docs"
description: "Running Stored Procedures (OLE DB)"
ms.custom: ""
ms.date: "06/12/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "stored procedures [OLE DB], executing"
  - "OLE DB, stored procedures"
  - "OLE DB Driver for SQL Server, stored procedures"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Stored Procedures - Running
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  When executing statements, calling a stored procedure on the data source (instead of executing or preparing a statement in the client application directly) can provide:  
  
-   Higher performance.  
  
-   Reduced network overhead.  
  
-   Better consistency.  
  
-   Better accuracy.  
  
-   Added functionality.  
  
 The OLE DB Driver for SQL Server supports three of the mechanisms that [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] stored procedures use to return data:  
  
-   Every SELECT statement in the procedure generates a result set.  
  
-   The procedure can return data through output parameters.  
  
-   The procedure can have an integer return code.  
  
 The application must be able to handle all of these outputs from stored procedures.  
  
 Different OLE DB providers return output parameters and return values at different times during result processing. In case of the OLE DB Driver for SQL Server, the output parameters and return codes are not supplied until after the consumer has retrieved or canceled the result sets returned by the stored procedure. The return codes and the output parameters are returned in the last TDS packet from the server.  
  
 Providers use the DBPROP_OUTPUTPARAMETERAVAILABILITY property to report when it returns output parameters and return values. This property is in the DBPROPSET_DATASOURCEINFO property set.  
  
 The OLE DB Driver for SQL Server sets the DBPROP_OUTPUTPARAMETERAVAILABILITY property to DBPROPVAL_OA_ATROWRELEASE to indicate that return codes and output parameters are not returned until the result set is processed or released.  
  
## See Also  
 [Stored Procedures](../../oledb/ole-db/stored-procedures.md)  
  
  
