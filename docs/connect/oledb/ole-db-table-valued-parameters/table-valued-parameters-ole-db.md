---
title: Table-valued parameters (OLE DB driver)
description: These articles describe support for table-valued parameters in OLE DB Driver for SQL Server, including parameter rowset creation and parameter type discovery.
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "OLE DB, table-valued parameters"
  - "table-valued parameters (OLE DB)"
---
# Table-Valued Parameters (OLE DB)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  This section describes support for table-valued parameters in OLE DB Driver for SQL Server. For additional overview information, see [Table-Valued Parameters &#40;OLE DB Driver for SQL Server&#41;](../../oledb/features/table-valued-parameters-oledb-driver-for-sql-server.md). For a sample, see [Use Table-Valued Parameters &#40;OLE DB&#41;](../../oledb/ole-db-how-to/use-table-valued-parameters-ole-db.md).  
  
## Remarks  
 Currently, you can send multirow data to the server as parameters to a procedure with parameter sets (the DBPARAMS parameter in **ICommand::Execute**). With parameter sets, every element of the set has to be sent in a separate remote procedure call (RPC) request to the server. Table-valued parameters provide similar functionality, but there is better integration with the server. It reduces the number of RPC requests and enables set-based operations on the server.  
  
 Table-value parameters are supported in OLE DB Driver for SQL Server as OLE DB **Rowset** objects. Any **Rowset** object could be provided by the consumer (that is, the client application using OLE DB Driver for SQL Server) as a placeholder for table-valued parameter parameters. Table-valued parameters are treated like other [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] parameter types. The OLE DB Driver for SQL Server provides creation, discovery, specification, binding, and schema interfaces.  
  
## In This Section  
  
-   [Table-Valued Parameter Rowset Creation](../../oledb/ole-db-table-valued-parameters/table-valued-parameter-rowset-creation.md)  
  
-   [Table-Valued Parameter Type Discovery](../../oledb/ole-db-table-valued-parameters/table-valued-parameter-type-discovery.md)  
  
-   [Executing Commands Containing Table-Valued Parameters](../../oledb/ole-db-table-valued-parameters/executing-commands-containing-table-valued-parameters.md)  
  
-   [Inserting Data into Table-Valued Parameters](../../oledb/ole-db-table-valued-parameters/inserting-data-into-table-valued-parameters.md)  
  
-   [Schema Rowsets Changed for OLE DB Table-Valued Parameters](../../oledb/ole-db-table-valued-parameters/schema-rowsets-changed-for-ole-db-table-valued-parameters.md)  
  
-   [OLE DB Table-Valued Parameter Type Support](../../oledb/ole-db-table-valued-parameters/ole-db-table-valued-parameter-type-support.md)  
  
-   [OLE DB Table-Valued Parameter Type Support &#40;Methods&#41;](../../oledb/ole-db-table-valued-parameters/ole-db-table-valued-parameter-type-support-methods.md)  
  
-   [OLE DB Table-Valued Parameter Type Support &#40;Properties&#41;](../../oledb/ole-db-table-valued-parameters/ole-db-table-valued-parameter-type-support-properties.md)  
  
## See Also  
 [OLE DB Driver for SQL Server Programming](../../oledb/ole-db/oledb-driver-for-sql-server-programming.md)   
 [Use Table-Valued Parameters &#40;OLE DB&#41;](../../oledb/ole-db-how-to/use-table-valued-parameters-ole-db.md)  
  
  
