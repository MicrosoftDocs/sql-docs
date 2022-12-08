---
title: ISSCommandWithParameters (OLE DB driver)
description: Learn how the ISSCommandWithParameters interface supports SQL Server XML and user-defined types in OLE DB Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "ISSCommandWithParameters interface"
apiname: "ISSCommandWithParameters (OLE DB)"
apitype: "COM"
---
# ISSCommandWithParameters (OLE DB)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  **ISSCommandWithParameters** interface exposes support for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] XML and user-defined types (UDT). It's an optional interface that inherits from the core OLE DB interface **ICommandWithParameters**. In addition to the three methods inherited from **ICommandWithParameters**; **GetParameterInfo**, **MapParameterNames**, and **SetParameterInfo**; **ISSCommandWithParameters** provides two new methods that are used to handle server-specific data types.  
  
> [!NOTE]  
>  The **ISSCommandWithParameters** interface can be used when Service Components are used, but the Service Components won't use this interface.  
  
|Method|Description|  
|------------|-----------------|  
|[ISSCommandWithParameters::GetParameterProperties &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/isscommandwithparameters-getparameterproperties-ole-db.md)|Returns one **SSPARAMPROPS** property set structure in the array for each UDT or XML parameter passed to the command, but none is returned for other types of parameters.|  
|[ISSCommandWithParameters::SetParameterProperties &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/isscommandwithparameters-setparameterproperties-ole-db.md)|Sets the parameter properties on a per parameter basis by ordinal, or sets bulk parameter properties by specifying an array of **SSPARAMPROPS** structures.|  
  
## See Also  
 [Interfaces &#40;OLE DB&#41;](../../oledb/ole-db-interfaces/oledb-driver-for-sql-server-ole-db-interfaces.md)    
 [Using XML Data Types](../../oledb/features/using-xml-data-types.md)   
 [Using User-Defined Types](../../oledb/features/using-user-defined-types.md)  
  
  
