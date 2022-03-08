---
title: "OLE automation return codes and error information | Microsoft Docs"
description: Learn the details about OLE automation return codes and how to convert error code information. 
ms.custom: ""
ms.date: "03/08/2022"
ms.prod: sql
ms.reviewer: ""
ms.technology: stored-procedures
ms.topic: conceptual
helpviewer_keywords: 
  - "return codes [SQL Server]"
  - "OLE Automation [SQL Server], return codes"
  - "OLE Automation [SQL Server], errors"
ms.assetid: 9696fb05-e9e8-4836-b359-d4de0be0eeb2
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# OLE automation return codes and error information

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

The OLE automation system stored procedures return an **int** return code that is the HRESULT returned by the underlying OLE automation operation. An HRESULT of 0 indicates success. A nonzero HRESULT is an OLE error code of the hexadecimal form 0x800*nnnnn*, but when returned as an **int** value in a stored procedure return code, HRESULT has the form -214*nnnnnnn*.  

## Example

For example, passing an invalid object name (SQLDMO.Xyzzy) to sp_OACreate causes the procedure to return an **int** HRESULT of 2147221005, which is 0x800401f3 in hexadecimal.  
  
You can use `CONVERT(binary(4), @hresult)` to convert an **int** HRESULT to a **binary** value.

For examples of supported conversion see [H. Using CONVERT with binary and character data](../../t-sql/functions/cast-and-convert-transact-sql.md#h-using-convert-with-binary-and-character-data).

## Related content  

[sp_OAGetErrorInfo &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-oageterrorinfo-transact-sql.md)  
