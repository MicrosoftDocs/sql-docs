---
title: ISSCommandWithParameters (Native Client OLE DB provider)
description: "ISSCommandWithParameters (Native Client OLE DB provider)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "ISSCommandWithParameters interface"
apiname: "ISSCommandWithParameters (OLE DB)"
apitype: "COM"
---
# ISSCommandWithParameters (Native Client OLE DB provider)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT]
> [!INCLUDE[snac-removed-oledb-only](../../includes/snac-removed-oledb-only.md)]

  **ISSCommandWithParameters** exposes support for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] XML and user-defined types (UDT). This is an optional interface that inherits from the core OLE DB interface **ICommandWithParameters**. In addition to the three methods inherited from **ICommandWithParameters**; **GetParameterInfo**, **MapParameterNames**, and **SetParameterInfo**; **ISSCommandWithParameters** provides two new methods that are used to handle server specific data types.  
  
> [!NOTE]  
>  The **ISSCommandWithParameters** interface can be used when Service Components are used, but the Service Components themselves will not use this interface.  
  
|Method|Description|  
|------------|-----------------|  
|[ISSCommandWithParameters::GetParameterProperties &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-interfaces/isscommandwithparameters-getparameterproperties-ole-db.md)|Returns one **SSPARAMPROPS** property set structure in the array for each UDT or XML parameter passed to the command, but none is returned for other types of parameters.|  
|[ISSCommandWithParameters::SetParameterProperties &#40;OLE DB&#41;](../../relational-databases/native-client-ole-db-interfaces/isscommandwithparameters-setparameterproperties-ole-db.md)|Sets the parameter properties on a per parameter basis by ordinal, or sets bulk parameter properties by specifying an array of **SSPARAMPROPS** structures.|  
  
## See Also  
 [Interfaces &#40;OLE DB&#41;](./sql-server-native-client-ole-db-interfaces.md)   
 [Using XML Data Types](../../relational-databases/native-client/features/using-xml-data-types.md)   
 [Using User-Defined Types](../../relational-databases/native-client/features/using-user-defined-types.md)  
  
