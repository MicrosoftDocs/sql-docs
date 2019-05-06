---
title: "ODBC Flow Components | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: cf751f1e-2348-4a77-904c-bd92c0d7d0ae
author: janinezhang
ms.author: janinez
manager: craigg
---
# ODBC Flow Components
  This topic describes the concepts necessary for creating an ODBC data flow using [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)]  
  
 The Connector for Open Database Connectivity (ODBC) by Attunity for [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] helps SSIS developers easily create packages that load and unload data from ODBC-supported databases.  
  
 The ODBC Connector is designed to achieve optimal performance when loading data into or unloading data from an ODBC-supported database in the context of [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)].  
  
## Benefits  
 The ODBC source and ODBC destination for [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] provides a competitive edge for SSIS in projects dealing with loading data into or unloading data from ODBC-supported databases.  
  
 Both the ODBC source and ODBC destination enable high performance data integration with ODBC-enabled databases. Both components can be configured to work with row-wise parameter array bindings for high-functioning ODBC providers that support this mode of binding and single-row parameter bindings for low-functioning ODBC providers.  
  
## Getting Started with the ODBC Source and Destination  
 Before you can set up packages that use [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)], you must make sure that the following are available.  
  
-   [ODBC Source](odbc-source.md)  
  
-   [ODBC Destination](odbc-destination.md)  
  
 The ODBC source and ODBC destination provide an easy way to unload and load data and transfer data from an ODBC-supported source database to an ODBC-supported destination database.  
  
 To use the source or destination to load or unload data, open a new [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] Project in the [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)]. Then drag the source or destination onto the design surface of the [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)].  
  
-   The ODBC source component reads data from the source ODBC-supported database.  
  
 You can connect the ODBC source to any destination or transform component supported by SSIS.  
  
 **See also:**  
  
 ODBC Source  
  
 ODBC Source Editor (Connection Manager Page)  
  
 ODBC Source Editor (Error Output Page)  
  
-   The ODBC destination loads data into an ODBC-supported database. You connect the destination to any source or transform component supported by SSIS.  
  
 **See also:**  
  
 ODBC Destination  
  
 ODBC Destination Editor (Connection Manager Page)  
  
 ODBC Destination Editor (Error Output Page)  
  
## Operating Scenarios  
 This section describes some of the main uses for the ODBC source and destination components.  
  
### Bulk Copy Data from SQL Server tables to any ODBC-Supported database table  
 You can use the components to bulk copy data from one or more [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables to a single ODBC-supported database table.  
  
 The following example shows how to create an SSIS Data Flow Task that extracts data from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table and loads it into a DB2 table.  
  
-   Create an [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] Project in the [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)].  
  
-   Create an OLE DB connection manager that connects to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database that contains the data you want to copy.  
  
-   Create an ODBC connection manager that uses a locally installed DB2 ODBC driver with a DSN pointing to a local or remote DB2 database. This database is where the data from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database is loaded.  
  
-   Drag an OLE DB source to the design surface, then configure the source to get the data from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database and table with the data you are going to extract. Use the OLE DB connection manager you created previously.  
  
-   Drag an ODBC destination to the design surface, connect the source output to the ODBC destination, then configure the destination to load the data into the DB2 table with the data you extract from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. Use the ODBC connection manager you created previously.  
  
### Bulk Copy Data from ODBC-supported database tables to any SQL Server table  
 You can use the components to bulk copy data from one or more ODBC-supported database tables to a single [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database table.  
  
 The following example shows how to create an SSIS Data Flow Task that extracts data from a Sybase database table and loads it into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database table.  
  
-   Create an [!INCLUDE[ssISCurrent](../../includes/ssiscurrent-md.md)] Project in the [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)]  
  
-   Create an ODBC connection manager that uses a locally installed Sybase ODBC driver with a DSN pointing to a local or remote Sybase database. This database is where the data is extracted.  
  
-   Create an OLE DB connection manager that connects to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database where you want to load the data.  
  
-   Drag an ODBC source to the design surface, then configure the source to get the data from the Sybase table with the data you are going to copy. Use the ODBC connection manager you created previously.  
  
-   Drag an OLE DB destination to the design surface, connect the source output to the OLE DB destination, then configure the destination to load the data into the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table with the data you extract from the Sybase database. Use the OLE DB connection manager you created previously.  
  
## Supported Data Types  
 The ODBC Bulk SSIS components support all built-in ODBC data types, including support for large objects (CLOBs and BLOBs).  
  
There is no data type support for extensible C types as described in the ODBC 3.8 specifications.The following table describes which SSIS data types are used for each ODBC SQL type. An SSIS developer can override the default mapping and specify a different SSIS data type for input/output columns without impacting the performance for the required data conversions.  
  
|ODBC SQL Type|SSIS Data Type|Comments|  
|-----------------|------------------|------------|  
|SQL_BIT|DT_BOOL||  
|SQL_TINYINT|DT_I1<br /><br />DT_UI1|SQL data types are mapped to SSIS unsigned types (DT_UI1, DT_UI2, DT_UI4, DT_UI8) when the ODBC driver sets the UNSIGNED_ATTRIBUTE to SQL_TRUE for that SQL data type.|  
|SQL_SMALLINT|DT_I2<br /><br />DT_UI2|SQL data types are mapped to SSIS unsigned types (DT_UI1, DT_UI2, DT_UI4, DT_UI8) when the ODBC driver sets the UNSIGNED_ATTRIBUTE to SQL_TRUE for that SQL data type.|  
|SQL_INTEGER|DT_I4<br /><br />DTUI4|SQL data types are mapped to SSIS unsigned types (DT_UI1, DT_UI2, DT_UI4, DT_UI8) when the ODBC driver sets the UNSIGNED_ATTRIBUTE to SQL_TRUE for that SQL data type.|  
|SQL_BIGINT|DT_I8<br /><br />DT_UI8|SQL data types are mapped to SSIS unsigned types (DT_UI1, DT_UI2, DT_UI4, DT_UI8) when the ODBC driver sets the UNSIGNED_ATTRIBUTE to SQL_TRUE for that SQL data type.|  
|SQL_DOUBLE|DT_R8|  
|SQL_FLOAT|DT_R8|  
|SQL_REAL|DT_R4|  
|SQL_NUMERIC (p,s)|DT_NUMERIC (p,s)<br /><br />DT_R8<br /><br />DT_CY|The numeric data type is mapped to DT_NUMERIC when P is greater than or equal to 38 and S is greater than or equal to 0 and S is less than or equal to P. The numeric data type is mapped to DT_R8 when at least one of the following is true:<br /><br />Precision is greater than 38<br /><br />Scale is less than zero<br /><br />Scale is greater than 38<br /><br />Scale is greater than Precision<br /><br /><br /><br />Note that the numeric data type is mapped to DT_CY when it is declared as a money data type.|  
|SQL_DECIMAL (p,s)|DT_NUMERIC (p,s)<br /><br />DT_R8<br /><br />DT_CY|The decimal data type is mapped to DT_NUMERIC when P is greater than or equal to 38 and S is greater than or equal to 0 and S is less than or equal to P. The decimal data type is mapped to DT_R8 when at least one of the following is true:<br /><br />Precision is greater than 38<br /><br />Scale is less than zero<br /><br />Scale is greater than 38<br /><br />Scale is greater than Precision<br /><br />Note that the decimal data type is mapped to DT_CY when it is declared as a money data type.|  
|SQL_DATE<br /><br />SQL_TYPE_DATE|DT_DBDATE|  
|SQL_TIME<br /><br />SQL_TYPE_TIME|DT_DBTIME|  
|SQL_TIMESTAMP<br /><br />SQL_TYPE_TIMESTAMP|DT_DBTIMESTAMP<br /><br />DT_DBTIMESTAMP2|SQL_TIMESTAMP data types are mapped to DT_DBTIMESTAMP2 if  scale is greater than 3. In all other cases, they are mapped to DT_DBTIMESTAMP.|  
|SQL_CHAR<br /><br />SQLVARCHAR|DT_STR<br /><br />DT_WSTR<br /><br />DT_TEXT<br /><br />DT_NTEXT|DT_STR is used if the column length is less than or equal to 8000 and the **ExposeStringsAsUnicode** property is false.<br /><br />DT_WSTR is used if the column length is less than or equal to 8000 and the **ExposeStringsAsUnicode** property is true.<br /><br />DT_TEXT is used if the column length is greater than 8000 and the **ExposeStringsAsUnicode** property is false.<br /><br />DT_NTEXT is used if the column length is greater than 8000 and the **ExposeStringsAsUnicode** property is true.|  
|SQL_LONGVARCHAR|DT_TEXT<br /><br />DT_NTEXT|DT_NTEXT is used if the **ExposeStringsAsUnicode** property is true.|  
|SQL_WCHAR<br /><br />SQL_WVARCHAR|DT_WSTR<br /><br />DT_NTEXT|DT_WSTR is used if the column length is less than or equal to 4000.<br /><br />DT_NTEXT is used if the column length is greater than 4000.|  
|SQL_WLONGVARCHAR|DT_NTEXT|  
|SQL_BINARY|DT_BYTE<br /><br />DT_IMAGE|DT_BYTES is used if the column length is less than or equal to 8000.<br /><br />DT_IMAGE if the column length is greater than 8000.|  
|SQL_LONGVARBINARY|DT_IMAGE|  
|SQL_GUID|DT_GUID|  
|SQL_INTERVAL_YEAR<br /><br />SQL_INTERVAL_MONTH<br /><br />SQL_INTERVAL_DAY<br /><br />SQL_INTERVAL_HOUR<br /><br />SQL_INTERVAL_MINUTE<br /><br />SQL_INTERVAL_SECOND<br /><br />SQL_INTERVAL_YEAR_TO_MONTH<br /><br />SQL_INTERVAL_DAY_TO_HOUR<br /><br />SQL_INTERVAL_DAY_TO_MINUTE<br /><br />SQL_INTERVAL_DAY_TO_SECOND<br /><br />SQL_INTERVAL_HOUR_TO_MINUTE<br /><br />SQL_INTERVAL_HOUR_TO_SECOND<br /><br />SQL_INTERVAL_MINUTE_TO_SECOND|DT_WSTR|  
|Provider Specific Data Types|DT_BYTES<br /><br />DT_IMAGE|DT_BYTES is used if the column length is less than or equal to 8000.<br /><br />DT_IMAGE is used if the column length is zero or greater than 8000.|  
  
## In This Section  
  
-   [ODBC Source](odbc-source.md)  
  
-   [ODBC Destination](odbc-destination.md)  
  
 
