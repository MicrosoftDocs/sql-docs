---
title: "Command Parameters | Microsoft Docs"
description: "Command parameters"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "parameters [OLE DB Driver for SQL Server]"
  - "OLE DB Driver for SQL Server, parameters"
  - "OLE DB Driver for SQL Server, commands"
  - "parameters [OLE DB Driver for SQL Server], OLE DB"
  - "commands [OLE DB]"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Command Parameters
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  Parameters are marked in command text with the question mark character. For example, the following SQL statement is marked for a single input parameter:  
  
```  
{call SalesByCategory('Produce', ?)}  
```  
  
 To improve performance by reducing network traffic, the OLE DB Driver for SQL Server does not automatically derive parameter information unless **ICommandWithParameters::GetParameterInfo** or **ICommandPrepare::Prepare** is called before executing a command. This means that the OLE DB Driver for SQL Server does not automatically:  
  
-   Verify the correctness of the data type specified with **ICommandWithParameters::SetParameterInfo**.  
  
-   Map from the DBTYPE specified in the accessor binding information to the correct [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type for the parameter.  
  
 Applications will receive possible errors or loss of precision with either of these methods if they specify data types that are not compatible with the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type of the parameter.  
  
 To ensure this does not happen, the application should:  
  
-   Ensure that *pwszDataSourceType* matches the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type for the parameter if hard-coding **ICommandWithParameters::SetParameterInfo**.  
  
-   Ensure that the DBTYPE value being bound to the parameter is of the same type as the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type for the parameter if hard-coding an accessor.  
  
-   Code the application to call **ICommandWithParameters::GetParameterInfo** so that the provider can obtain the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types of the parameters dynamically. Note that this causes an extra network round trip to the server.  
  
> [!NOTE]  
>  The provider does not support calling **ICommandWithParameters::GetParameterInfo** for any [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] UPDATE or DELETE statement containing a FROM clause; for any SQL statement depending on a subquery containing parameters; for SQL statements containing parameter markers in both expressions of a comparison, like, or quantified predicate; or queries where one of the parameters is a parameter to a function. When processing a batch of SQL statements, the provider also does not support calling **ICommandWithParameters::GetParameterInfo** for parameter markers in statements after the first statement in the batch. Comments (/* \*/) are not allowed in the [!INCLUDE[tsql](../../../includes/tsql-md.md)] command.  
  
 The OLE DB Driver for SQL Server supports input parameters in SQL statement commands. On procedure-call commands, the OLE DB Driver for SQL Server supports input, output, and input/output parameters. Output parameter values are returned to the application either on execution (only if there are no rowsets returned) or when all returned rowsets are exhausted by the application. To ensure that returned values are valid, use **IMultipleResults** to force rowset consumption.  
  
 The names of stored procedure parameters need not be specified in a DBPARAMBINDINFO structure. Use NULL for the value of the *pwszName* member to indicate that the OLE DB Driver for SQL Server should ignore the parameter name and use only the ordinal specified in the *rgParamOrdinals* member of **ICommandWithParameters::SetParameterInfo**. If the command text contains both named and unnamed parameters, all of the unnamed parameters must be specified before any named parameters.  
  
 If the name of a stored procedure parameter is specified, the OLE DB Driver for SQL Server checks the name to ensure that it is valid. The OLE DB Driver for SQL Server returns an error when it receives an erroneous parameter name from the consumer.  
  
> [!NOTE]  
>  To expose support for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] XML and user-defined types (UDT), the OLE DB Driver for SQL Server implements a new [ISSCommandWithParameters](../../oledb/ole-db-interfaces/isscommandwithparameters-ole-db.md) interface.  
  
## See Also  
 [Commands](../../oledb/ole-db-commands/commands.md)  
  
  
