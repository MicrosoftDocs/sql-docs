---
title: "Using SQLConfigDatasource with the ODBC Driver for Oracle | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLConfigDataSource function [ODBC], ODBC driver for Oracle"
ms.assetid: e535d1ef-aff9-4ae7-a3ed-ef4ca2584289
author: MightyPen
ms.author: genemi
manager: craigg
---
# Using SQLConfigDatasource with the ODBC Driver for Oracle
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.  
  
 The following table lists valid **SQLConfigDatasource** settings for the Microsoft ODBC Driver for Oracle, version 1.0 (Msorcl10.dll) and the Microsoft ODBC Driver for Oracle, version 2.0 (Msorcl32.dll).  
  
> [!NOTE]  
>  The Msorcl10.dll driver (version 1.0) supports all settings except **Server**. The Msorcl32.dll driver (version 2.0 and higher) supports all settings.  
  
 Some settings are ignored by the driver but are accepted by **SQLConfigDatasource**. Including these settings in the ODBC connection string is the only way they will be accepted at run time. An ignored setting will not be stored in the registry when **SQLConfigDatasource** creates the data source.  
  
 In the following table, *A/N* means any valid alphanumeric string up to the maximum allowable length. *Max Len* (maximum length) is the maximum allowable string length accepted by the setting, including the string-terminator character.  
  
|Setting|Max Len|Default value|Valid values|Description|  
|-------------|-------------|-------------------|------------------|-----------------|  
|BufferSize|7|65535|1000|Minimum fetch buffer size up to 65535 bytes|  
|CatalogCap|2|1|0 or 1|If 1, nonquoted identifiers will be converted to uppercase in the catalog functions.|  
|ConnectString|128|""|A/N|Connection string. Required method of specifying the server name with the Msorcl10.dll driver.|  
|Description|256|""|A/N|Description.|  
|DSN|33|""|A/N|Data source name.|  
|GuessTheColDef|4|0|A/N|Returns a non-zero value for columns without Oracle-defined scale.|  
|NumberFloat|2|""|0 or 1|If 0, FLOAT columns are treated as SQL_FLOAT. If 1, FLOAT columns are treated as SQL_DOUBLE.|  
|PWD|30|""|A/N|Password.|  
|RDOSupport|2|""|0 or 1|Allows RDO to call Oracle procedures.|  
|Remarks|2|0|0 or 1|Include REMARKS in catalog functions.|  
|RowLimit|4|""|0 to 99|Maximum number of rows returned by a SELECT statement. A zero-length string indicates that no limit is applied.|  
|Server|128|""|A/N|Oracle server name.|  
|SynonymColumns|2|1|0 or 1|Include SYNONYMs in SQLColumns.|  
|SystemTable|2|""|0 or 1|If 0, system tables will not be displayed. If 1, system tables will be displayed.|  
|TranslationDLL|33|""|A/N|Translation .dll name.|  
|TranslationName|33|""|A/N|Translation name.|  
|TranslationOption|33|""|A/N|Translation option.|  
|TxnCap|2|""|A/N|Transaction capable. If 0, the driver reports that it does not support transactions. If 1, the driver reports that it is capable of performing transactions.|  
|UID|30|""|A/N|User name.|
