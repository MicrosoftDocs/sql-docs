---
description: "Sparse Columns Support in SQL Server Native Client"
title: "Sparse columns support"
ms.custom: ""
ms.date: "03/17/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "sparse columns, ODBC"
  - "sparse columns, SQL Server Native Client"
  - "sparse columns, OLE DB"
ms.assetid: aee5ed81-7e23-42e4-92d3-2da7844d9bc3
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Sparse Columns Support in SQL Server Native Client
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-and-odbc](../../../includes/snac-removed-oledb-and-odbc.md)]

  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client supports sparse columns. For more information about sparse columns in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], see [Use Sparse Columns](../../../relational-databases/tables/use-sparse-columns.md) and [Use Column Sets](../../../relational-databases/tables/use-column-sets.md).  
  
 For more information about sparse column support in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client, see [Sparse Columns Support &#40;ODBC&#41;](../../../relational-databases/native-client/odbc/sparse-columns-support-odbc.md) and [Sparse Columns Support &#40;OLE DB&#41;](../../../relational-databases/native-client/ole-db/sparse-columns-support-ole-db.md).  
  
## User Scenarios for Sparse Columns and SQL Server Native Client  
 The following table summarizes the common user scenarios for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client users with sparse columns:  
  
|Scenario|Behavior|  
|--------------|--------------|  
|**select \* from table** or IOpenRowset::OpenRowset.|Returns all columns that are not members of the sparse **column_set**, plus an XML column that contains the values of all non-null columns that are members of the sparse **column_set**.|  
|Reference a column by name.|The column can be referenced regardless of its sparse column status or **column_set** membership.|  
|Access **column_set** member columns through a computed XML column.|Columns that are members of the sparse **column_set** can be accessed by selecting the **column_set** by name and can have values inserted and updated by updating the XML in the **column_set** column.<br /><br /> The value must conform to the schema for **column_set** columns.|  
|Retrieve metadata for all columns in a table through SQLColumns with a column search pattern of NULL or '%' (ODBC); or through the DBSCHEMA_COLUMNS schema rowset with no column restriction (OLE DB).|Returns a row for all columns that are not members of a **column_set**. If the table has a sparse **column_set**, a row will be returned for it.<br /><br /> Note that this does not return metadata for columns that are members of a **column_set**.|  
|Retrieve metadata for all columns, regardless of sparseness or membership in a **column_set**. This might return a very large number of rows.|Set the descriptor field SQL_SOPT_SS_NAME_SCOPE to SQL_SS_NAME_SCOPE_EXTENDED and call [SQLColumns](../../../relational-databases/native-client-odbc-api/sqlcolumns.md) (ODBC).<br /><br /> Call IDBSchemaRowset::GetRowset for the DBSCHEMA_COLUMNS_EXTENDED schema rowset (OLE DB).<br /><br /> This scenario is not possible from an application that uses [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client from a release earlier than [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)]. However, such an application could query system views directly.|  
|Retrieve metadata only for columns that are members of a **column_set**. This might return a very large number of rows.|Set the descriptor field SQL_SOPT_SS_NAME_SCOPE to SQL_SS_NAME_SCOPE_SPARSE_COLUMN_SET and call SQLColumns (ODBC).<br /><br /> Call IDBSchemaRowset::GetRowset for the DBSCHEMA_SPARSE_COLUMN_SET schema rowset (OLE DB).<br /><br /> This scenario is not possible from an application that uses [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client from a release earlier than [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)]. However, such an application could query system views.|  
|Determine whether a column is sparse.|Consult the SS_IS_SPARSE column of the SQLColumns result set (ODBC).<br /><br /> Consult the SS_IS_SPARSE column of the DBSCHEMA_COLUMNS schema rowset (OLE DB).<br /><br /> This scenario is not possible from an application that uses [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client from a release earlier than [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)]. However, such an application could query system views.|  
|Determine if a column is a **column_set**.|Consult the SS_IS_COLUMN_SET column of the SQLColumns result set. Or, consult the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] specific column attribute SQL_CA_SS_IS_COLUMN_SET (ODBC).<br /><br /> Consult the SS_IS_COLUMN_SET column of the DBSCHEMA_COLUMNS schema rowset. Or, consult *dwFlags* returned by IColumnsinfo::GetColumnInfo or DBCOLUMNFLAGS in the rowset returned by IColumnsRowset::GetColumnsRowset. For **column_set** columns, DBCOLUMNFLAGS_SS_ISCOLUMNSET will be set (OLE DB).<br /><br /> This scenario is not possible from an application that uses [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client from a release earlier than [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)]. However, such an application could query system views.|  
|Import and export of sparse columns by BCP for a table with no **column_set**.|No change in behavior from previous versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client.|  
|Import and export of sparse columns by BCP for a table with a **column_set**.|The **column_set** is imported and exported in the same way as XML; that is, as **varbinary(max)** if bound as a binary type, or as **nvarchar(max)** if bound as a **char** or **wchar** type.<br /><br /> Columns that are members of the sparse **column_set** are not exported as distinct columns; they are only exported in the value of the **column_set**.|  
|**queryout** behavior for BCP.|No change in the handling of explicitly named columns from previous versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client.<br /><br /> Scenarios involving import and export between tables with different schemas may require special handling.<br /><br /> For more information about BCP, see Bulk Copy (BCP) Support for Sparse Columns, later in this topic.|  
  
## Down-Level Client Behavior  
 Down-level clients will return metadata only for columns that are not members of the sparse **column_set** for SQLColumns and DBSCHMA_COLUMNS. The additional OLE DB schema rowsets introduced in [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] Native Client will not be available, nor will the modifications to SQLColumns in ODBC via SQL_SOPT_SS_NAME_SCOPE.  
  
 Down-level clients can access columns that are members of the sparse **column_set** by name, and the **column_set** column will be accessible as an XML column to [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] clients.  
  
## Bulk Copy (BCP) Support for Sparse Columns  
 There are no changes to the BCP API in either ODBC or OLE DB for the sparse columns or **column_set** features.  
  
 If a table has a **column_set**, sparse columns are not handled as distinct columns. The values of all sparse columns are included in the value of the **column_set**, which is exported in the same way as an XML column; that is, as **varbinary(max)** if bound as a binary type, or as **nvarchar(max)** if bound as a **char** or **wchar** type). On import, the **column_set** value must conform to the schema of the **column_set**.  
  
 For **queryout** operations, there is no change to the way explicitly referenced columns are handled. **column_set** columns have the same behavior as XML columns and sparseness has no effect on the handling of named sparse columns.  
  
 However, if **queryout** is used for export and you reference sparse columns that are members of the sparse column set by name, you cannot perform a direct import into a similarly structured table. This is because BCP uses metadata consistent with a **select &#42;** operation for the import and is unable to match **column_set** member columns with this metadata. To import **column_set** member columns individually, you must define a view on the table that references the desired **column_set** columns, and you must perform the import operation using the view.  
  
## See Also  
 [SQL Server Native Client Programming](../../../relational-databases/native-client/sql-server-native-client-programming.md)  
  
  
