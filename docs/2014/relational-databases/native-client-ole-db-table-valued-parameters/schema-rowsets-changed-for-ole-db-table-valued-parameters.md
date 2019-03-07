---
title: "Schema Rowsets Changed for OLE DB Table-Valued Parameters | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "schema rowsets [OLE DB]"
  - "table-valued parameters (OLE DB), schema rowsets changed for (OLE DB)"
ms.assetid: 581e3ead-53db-44da-8718-f3fc4b5108f1
author: MightyPen
ms.author: genemi
manager: craigg
---
# Schema Rowsets Changed for OLE DB Table-Valued Parameters
  The following are the schema rowsets that have been changed or added to support table-valued parameters.  
  
|Schema rowset|Description|  
|-------------------|-----------------|  
|DBSCHEMA_PROCEDURE_PARAMETERS|Two new columns were added at the end of the rowset named SS_TYPE_CATALOG_NAME and SS_TYPE_SCHEMANAME. These columns could be re-used for future types. The TYPE_NAME and LOCAL_TYPE_NAME columns will contain the name of the table-valued parameter TABLE type. The DATA_TYPE column will have value DBTYPE_TABLE = 143 for table-valued parameters.|  
|DBSCHEMA_TABLE_TYPES|This rowset was added to support table-valued parameters. It is identical to DBSCHEMA_TABLES, except that it returns metadata only for table types, rather than for tables, views, or synonyms. The TABLE_TYPE column will have the value 'TABLE TYPE'.|  
|DBSCHEMA_TABLE_TYPE_PRIMARY_KEYS|This rowset was added to support table-valued parameters. It is identical to DBSCHEMA_PRIMARY_KEYS, except that it returns primary keys metadata only for table types, rather than for tables.|  
|DBSCHEMA_TABLE_TYPE_COLUMNS|This rowset was added to support table-valued parameters. It is identical to DBSCHEMA_COLUMNS, except that it returns column metadata only for table types, rather than for tables, views, or synonyms.|  
  
## See Also  
 [Table-Valued Parameters &#40;OLE DB&#41;](table-valued-parameters-ole-db.md)   
 [Use Table-Valued Parameters &#40;OLE DB&#41;](../native-client-ole-db-how-to/use-table-valued-parameters-ole-db.md)  
  
  
