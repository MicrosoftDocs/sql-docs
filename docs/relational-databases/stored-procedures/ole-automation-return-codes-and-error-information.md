---
title: "OLE automation return codes and error information"
description: Learn the details about OLE automation return codes and how to convert error code information. 
ms.custom: ""
ms.date: "03/10/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: stored-procedures
ms.topic: conceptual
helpviewer_keywords: 
  - "return codes [SQL Server]"
  - "OLE Automation [SQL Server], return codes"
  - "OLE Automation [SQL Server], errors"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-2016"
---
# OLE automation return codes and error information

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The OLE automation system stored procedures return an **int** return code that is the HRESULT returned by the underlying OLE automation operation. An HRESULT of 0 indicates success. A nonzero HRESULT is an OLE error code of the hexadecimal form 0x800*nnnnn*, but when returned as an **int** value in a stored procedure return code, HRESULT has the form -214*nnnnnnn*.  

## Example

For example, passing an invalid object name (SQLDMO.Xyzzy) to sp_OACreate causes the procedure to return an **int** HRESULT of 2147221005, which is 0x800401f3 in hexadecimal.  
  
You can use `CONVERT(binary(4), @hresult)` to convert an **int** HRESULT to a **binary** value.

For examples of supported conversion, see [H. Using CONVERT with binary and character data](../../t-sql/functions/cast-and-convert-transact-sql.md#h-using-convert-with-binary-and-character-data).

## Next steps

- [sp_OAGetErrorInfo &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-oageterrorinfo-transact-sql.md)  
