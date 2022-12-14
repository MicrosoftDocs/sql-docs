---
title: Rowsets and SQL Server cursors (OLE DB driver)
description: Learn about how SQL Server returns result sets to consumers and default result sets differ from server cursors in OLE DB Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "OLE DB rowsets, cursors"
  - "OLE DB Driver for SQL Server, rowsets"
  - "rowsets [OLE DB], cursors"
  - "properties [OLE DB]"
  - "cursors [OLE DB]"
---
# Rowsets and SQL Server Cursors
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] returns result sets to consumers using two methods:  
  
-   Default result sets that:  
  
    -   Minimize overhead.  
  
    -   Provide maximal performance in fetching data.  
  
    -   Support only the default forward-only, read-only cursor functionality.  
  
    -   Return rows to the consumer one row at a time.  
  
    -   Support only one active statement at a time on a connection.  
  
         After a statement has been executed, no other statements can be executed on the connection until all the results have been retrieved by the consumer or the statement has been canceled.  
  
    -   Support all [!INCLUDE[tsql](../../../includes/tsql-md.md)] statements.  
  
-   Server cursors that:  
  
    -   Support all cursor functionality.  
  
    -   Can return blocks of rows to the consumer.  
  
    -   Support multiple active statements on a single connection.  
  
    -   Balance cursor functionality against performance.  
  
         The support for cursor functionality can decrease performance relative to a default result set. This can be offset if the consumer can use cursor functionality to retrieve a smaller set of rows.  
  
    -   Do not support any [!INCLUDE[tsql](../../../includes/tsql-md.md)] statement that returns more than a single result set.  
  
 Consumers can request different cursor behaviors in a rowset by setting certain rowset properties. If the consumer does not set any one of these rowset properties or sets them all to their default values, the OLE DB Driver for SQL Server implements the rowset using a default result set. If any one of these properties is set to a value other than the default, the OLE DB Driver for SQL Server implements the rowset using a server cursor.  
  
 The following rowset properties direct the OLE DB Driver for SQL Server to use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] cursors. Some properties can be safely combined with others. For example, a rowset that exhibits the DBPROP_IRowsetScroll and DBPROP_IRowsetChange properties will be a bookmark rowset exhibiting immediate update behavior. Other properties are mutually exclusive. For example, a rowset exhibiting DBPROP_OTHERINSERT cannot contain bookmarks.  
  
|Property ID|Value|Rowset behavior|  
|-----------------|-----------|---------------------|  
|DBPROP_SERVERCURSOR|VARIANT_TRUE|Cannot update [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data through the rowset. The rowset is sequential, supporting forward scrolling and fetching only. Relative row positioning is supported. Command text can contain an ORDER BY clause.|  
|DBPROP_CANSCROLLBACKWARDS or DBPROP_CANFETCHBACKWARDS|VARIANT_TRUE|Cannot update [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data through the rowset. The rowset supports scrolling and fetching in either direction. Relative row positioning is supported. Command text can contain an ORDER BY clause.|  
|DBPROP_BOOKMARKS or DBPROP_LITERALBOOKMARKS|VARIANT_TRUE|Cannot update [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data through the rowset. The rowset is sequential, supporting forward scrolling and fetching only. Relative row positioning is supported. Command text can contain an ORDER BY clause.|  
|DBPROP_OWNUPDATEDELETE or DBPROP_OWNINSERT or DBPROP_OTHERUPDATEDELETE|VARIANT_TRUE|Cannot update [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data through the rowset. The rowset supports scrolling and fetching in either direction. Relative row positioning is supported. Command text can contain an ORDER BY clause.|  
|DBPROP_OTHERINSERT|VARIANT_TRUE|Cannot update [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data through the rowset. The rowset supports scrolling and fetching in either direction. Relative row positioning is supported. Command text can include an ORDER BY clause if an index exists on the referenced columns.<br /><br /> DBPROP_OTHERINSERT cannot be VARIANT_TRUE if the rowset contains bookmarks. Trying to create a rowset with this visibility property and bookmarks causes an error.|  
|DBPROP_IRowsetLocate or DBPROP_IRowsetScroll|VARIANT_TRUE|Cannot update [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data through the rowset. The rowset supports scrolling and fetching in either direction. Bookmarks and absolute positioning through the **IRowsetLocate** interface are supported in the rowset. Command text can contain an ORDER BY clause.<br /><br /> DBPROP_IRowsetLocate and DBPROP_IRowsetScroll require bookmarks in the rowset. Trying to create a rowset with bookmarks and DBPROP_OTHERINSERT set to VARIANT_TRUE causes an error.|  
|DBPROP_IRowsetChange or DBPROP_IRowsetUpdate|VARIANT_TRUE|Can update [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data through the rowset. The rowset is sequential, supporting forward scrolling and fetching only. Relative row positioning is supported. All the commands that support updatable cursors can support these interfaces.|  
|DBPROP_IRowsetLocate or DBPROP_IRowsetScroll and  DBPROP_IRowsetChange or DBPROP_IRowsetUpdate|VARIANT_TRUE|Can update [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data through the rowset. The rowset supports scrolling and fetching in either direction. Bookmarks and absolute positioning through **IRowsetLocate** are supported in the rowset. Command text can contain an ORDER BY clause.|  
|DBPROP_IMMOBILEROWS|VARIANT_FALSE|Cannot update [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data through the rowset. The rowset supports forward scrolling only. Relative row positioning is supported. Command text can include an ORDER BY clause if an index exists on the referenced columns.<br /><br /> DBPROP_IMMOBILEROWS is only available in rowsets that can show [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] rows inserted by commands on other sessions or by other users. Trying to open a rowset with the property set to VARIANT_FALSE on any rowset for which DBPROP_OTHERINSERT cannot be VARIANT_TRUE causes an error.|  
|DBPROP_REMOVEDELETED|VARIANT_TRUE|Cannot update [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data through the rowset. The rowset supports forward scrolling only. Relative row positioning is supported. Command text can contain an ORDER BY clause unless constrained by another property.|  
  
 An OLE DB Driver for SQL Server rowset supported by a server cursor can be easily created on a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] base table or view by using the **IOpenRowset::OpenRowset** method. Specify the table or view by name, passing the required rowset property sets in the *rgPropertySets* parameter.  
  
 Command text that creates a rowset is restricted when the consumer requires that the rowset be supported by a server cursor. Specifically, the command text is restricted to either a single SELECT statement that returns a single rowset result or a stored procedure that implements a single SELECT statement returning a single rowset result.  
  
 These two tables show the mappings of various OLE DB properties and the cursor models. They also show which rowset properties should be set to use a certain type of cursor model.  
  
 Each cell in the table contains a value of the rowset property for the specific cursor model. The data type of all the rowset properties listed earlier in this topic is VT_BOOL and the default value is VARIANT_FALSE. The following symbols are used in the table.  
  
 F = default value (VARIANT_FALSE)  
  
 T = VARIANT_TRUE  
  
 \- = VARIANT_TRUE or VARIANT_FALSE  
  
 To use a certain type of cursor model, locate the column corresponding to the cursor model and find all the rowset properties with value 'T' in the column. Set these rowset properties to VARIANT_TRUE to use the specific cursor model. The rowset properties with '-' as a value can be set to either VARIANT_TRUE or VARIANT_FALSE.  
  
|Rowset properties/cursor models|Default<br /><br /> result<br /><br /> set<br /><br /> (RO)|Fast<br /><br /> forward-<br /><br /> only<br /><br /> (RO)|Static<br /><br /> (RO)|Keyset<br /><br /> driven<br /><br /> (RO)|  
|--------------------------------------|-------------------------------------------|--------------------------------------------|-----------------------|----------------------------------|  
|DBPROP_SERVERCURSOR|F|T|T|T|  
|DBPROP_DEFERRED|F|F|-|-|  
|DBPROP_IrowsetChange|F|F|F|F|  
|DBPROP_IrowsetLocate|F|F|-|-|  
|DBPROP_IrowsetScroll|F|F|-|-|  
|DBPROP_IrowsetUpdate|F|F|F|F|  
|DBPROP_BOOKMARKS|F|F|-|-|  
|DBPROP_CANFETCHBACKWARDS|F|F|-|-|  
|DBPROP_CANSRCOLLBACKWARDS|F|F|-|-|  
|DBPROP_CANHOLDROWS|F|F|-|-|  
|DBPROP_LITERALBOOKMARKS|F|F|-|-|  
|DBPROP_OTHERINSERT|F|T|F|F|  
|DBPROP_OTHERUPDATEDELETE|F|T|F|T|  
|DBPROP_OWNINSERT|F|T|F|T|  
|DBPROP_OWNUPDATEDELETE|F|T|F|T|  
|DBPROP_QUICKSTART|F|F|-|-|  
|DBPROP_REMOVEDELETED|F|F|F|-|  
|DBPROP_IrowsetResynch|F|F|F|-|  
|DBPROP_CHANGEINSERTEDROWS|F|F|F|F|  
|DBPROP_SERVERDATAONINSERT|F|F|F|-|  
|DBPROP_UNIQUEROWS|-|F|F|F|  
|DBPROP_IMMOBILEROWS|-|-|-|T|  
  
|Rowset properties/Cursor models|Dynamic (RO)|Keyset (R/W)|Dynamic (R/W)|  
|--------------------------------------|--------------------|---------------------|----------------------|  
|DBPROP_SERVERCURSOR|T|T|T|  
|DBPROP_DEFERRED|-|-|-|  
|DBPROP_IrowsetChange|F|-|-|  
|DBPROP_IrowsetLocate|F|-|F|  
|DBPROP_IrowsetScroll|F|-|F|  
|DBPROP_IrowsetUpdate|F|-|-|  
|DBPROP_BOOKMARKS|F|-|F|  
|DBPROP_CANFETCHBACKWARDS|-|-|-|  
|DBPROP_CANSRCOLLBACKWARDS|-|-|-|  
|DBPROP_CANHOLDROWS|F|-|F|  
|DBPROP_LITERALBOOKMARKS|F|-|F|  
|DBPROP_OTHERINSERT|T|F|T|  
|DBPROP_OTHERUPDATEDELETE|T|T|T|  
|DBPROP_OWNINSERT|T|T|T|  
|DBPROP_OWNUPDATEDELETE|T|T|T|  
|DBPROP_QUICKSTART|-|-|-|  
|DBPROP_REMOVEDELETED|T|-|T|  
|DBPROP_IrowsetResynch|-|-|-|  
|DBPROP_CHANGEINSERTEDROWS|F|-|F|  
|DBPROP_SERVERDATAONINSERT|F|-|F|  
|DBPROP_UNIQUEROWS|F|F|F|  
|DBPROP_IMMOBILEROWS|F|T|F|  
  
 For a particular set of rowset properties, the cursor model that is selected is determined as follows.  
  
 From the specified collection of rowset properties, obtain a subset of properties listed in the previous tables. Divide these properties into two subgroups depending on the flag value-required (T, F) or optional (-)-of each rowset property. For each cursor model, start in the first table and move from left to right. Compare the values of the properties in the two subgroups with the values of the corresponding properties in that column. The cursor model that has no mismatch with the required properties and the least number of mismatches with the optional properties is selected. If there is more than one cursor model, the leftmost is chosen.  
  
## SQL Server Cursor Block Size  
 When a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] cursor supports an OLE DB Driver for SQL Server rowset, the number of elements in the row handle array parameter of the **IRowset::GetNextRows** or the **IRowsetLocate::GetRowsAt** methods defines the cursor block size. The rows indicated by the handles in the array are the members of the cursor block.  
  
 For rowsets supporting bookmarks, the row handles retrieved by using the **IRowsetLocate::GetRowsByBookmark** method define the members of the cursor block.  
  
 Regardless of the method used to populate the rowset and form the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] cursor block, the cursor block is active until the next row-fetching method is executed on the rowset.  
  
## See Also  
 [Rowsets](../../oledb/ole-db-rowsets/rowsets.md)  
  
  
