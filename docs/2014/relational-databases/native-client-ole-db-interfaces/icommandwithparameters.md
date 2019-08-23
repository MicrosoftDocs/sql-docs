---
title: "ICommandWithParameters | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
ms.assetid: 66644c70-def7-46d8-8c47-b883292a0288
author: MightyPen
ms.author: genemi
manager: craigg
---
# ICommandWithParameters
  Improvements in the database engine beginning with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] allow ICommandWithParameters::GetParameterInfo to obtain more accurate descriptions of the expected results. These more accurate results may differ from the values returned by CommandWithParameters::GetParameterInfo in previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Metadata Discovery](../native-client/features/metadata-discovery.md).  
  
 Also beginning in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], when calling ICommandWithParameters::SetParameterInfo, the value passed to the *pwszName* parameter must be a valid identifier. For more information, see [Database Identifiers](../databases/database-identifiers.md).  
  
## See Also  
 [Interfaces &#40;OLE DB&#41;](../../database-engine/dev-guide/interfaces-ole-db.md)  
  
  
