---
title: "ICommand (OLE DB driver)"
description: "Learn about behavior of the ICommand::Execute method that is specific to OLE DB Driver for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "ICommand [OLE DB Driver for SQL Server]"
---
# ICommand (OLE DB)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  This article discusses OLE DB behavior that is specific to OLE DB Driver for SQL Server.  
  
## ICommand::Execute  
 Inserting data that is greater than the size of a column typically results in an error. But there are situations where S_OK is returned and `dwStatus` is set to DBSTATUS_S_TRUNCATED. Here are a few scenarios where it usually occurs:

- When inserting data with parameters  
- Where the column isn't large enough to hold the data  
- When `ICommandWithParameters::SetParameterInfo` hasn't been called  
  
## See Also  
 [Interfaces &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/oledb-driver-for-sql-server-ole-db-interfaces.md)
  
  
