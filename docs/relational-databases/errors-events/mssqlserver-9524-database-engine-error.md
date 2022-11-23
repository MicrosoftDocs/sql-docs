---
description: "MSSQLSERVER_9524"
title: "MSSQLSERVER_9524 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "9524 (Database Engine error)"
ms.assetid: 12da7931-e124-4466-889c-046f1b7b7bfd
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_9524
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|9254|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|XMLERR_INVALID_COLUMNSET_XML|  
|Message Text|The XML content provided does not conform to the required XML format for sparse column sets.|  
  
## Explanation  
An attempt was made to modify a column set. The XML content of a column set must meet the format restrictions of a column set. The general format of a column set is as follows:  
  
`<column_name_1>value1</column_name_1><column_name_2>value2</column_name_2>...`  
  
For more information about column sets, see the topic "Using Column Sets" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## User Action  
Correct the format of the XML for the column set in the statement.  
  
