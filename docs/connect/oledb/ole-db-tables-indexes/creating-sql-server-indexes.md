---
title: "Creating SQL Server Indexes | Microsoft Docs"
description: "Creating SQL Server indexes using OLE DB Driver for SQL Server"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "CreateIndex function"
  - "constraints [OLE DB]"
  - "OLE DB Driver for SQL Server, indexes"
  - "indexes [OLE DB]"
  - "adding indexes"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Creating SQL Server Indexes
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The OLE DB Driver for SQL Server exposes the **IIndexDefinition::CreateIndex** function, allowing consumers to define new indexes on [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] tables.  
  
 The OLE DB Driver for SQL Server creates table indexes as either indexes or constraints. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] gives constraint-creation privilege to the table owner, database owner, and members of certain administrative roles. By default, only the table owner can create an index on a table. Therefore, the success or failure of **CreateIndex** depends not only on the application user's access rights but also on the type of index created.  
  
 Consumers specify the table name as a Unicode character string in the *pwszName* member of the *uName* union in the *pTableID* parameter. The *eKind* member of *pTableID* must be DBKIND_NAME.  
  
 The *pIndexID* parameter can be NULL, and if it is, the OLE DB Driver for SQL Server creates a unique name for the index. The consumer can capture the name of the index by specifying a valid pointer to a DBID in the *ppIndexID* parameter.  
  
 The consumer can specify the index name as a Unicode character string in the *pwszName* member of the *uName* union of the *pIndexID* parameter. The *eKind* member of *pIndexID* must be DBKIND_NAME.  
  
 The consumer specifies the column or columns participating in the index by name. For each DBINDEXCOLUMNDESC structure used in **CreateIndex**, the *eKind* member of the *pColumnID* must be DBKIND_NAME. The name of the column is specified as a Unicode character string in the *pwszName* member of the *uName* union in the *pColumnID*.  
  
 The OLE DB Driver for SQL Server and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] support ascending order on values in the index. The OLE DB Driver for SQL Server returns E_INVALIDARG if the consumer specifies DBINDEX_COL_ORDER_DESC in any DBINDEXCOLUMNDESC structure.  
  
 **CreateIndex** interprets index properties as follows.  
  
|Property ID|Description|  
|-----------------|-----------------|  
|DBPROP_INDEX_AUTOUPDATE|R/W: Read/write<br /><br /> Default: None<br /><br /> Description: The OLE DB Driver for SQL Server does not support this property. Attempts to set the property in **CreateIndex** cause a DB_S_ERRORSOCCURRED return value. The *dwStatus* member of the property structure indicates DBPROPSTATUS_BADVALUE.|  
|DBPROP_INDEX_CLUSTERED|R/W: Read/write<br /><br /> Default: VARIANT_FALSE<br /><br /> Description: Controls index clustering.<br /><br /> VARIANT_TRUE: The OLE DB Driver for SQL Server attempts to create a clustered index on the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] table. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports at most one clustered index on any table.<br /><br /> VARIANT_FALSE: The OLE DB Driver for SQL Server attempts to create a nonclustered index on the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] table.|  
|DBPROP_INDEX_FILLFACTOR|R/W: Read/write<br /><br /> Default: 0<br /><br /> Description: Specifies the percentage of an index page used for storage. For more information, see [CREATE INDEX](../../../t-sql/statements/create-index-transact-sql.md).<br /><br /> The type of the variant is VT_I4. The value must be greater than or equal to 1 and less than or equal to 100.|  
|DBPROP_INDEX_INITIALIZE|R/W: Read/write<br /><br /> Default: None<br /><br /> Description: The OLE DB Driver for SQL Server does not support this property. Attempts to set the property in **CreateIndex** cause a DB_S_ERRORSOCCURRED return value. The *dwStatus* member of the property structure indicates DBPROPSTATUS_BADVALUE.|  
|DBPROP_INDEX_NULLCOLLATION|R/W: Read/write<br /><br /> Default: None<br /><br /> Description: The OLE DB Driver for SQL Server does not support this property. Attempts to set the property in **CreateIndex** cause a DB_S_ERRORSOCCURRED return value. The *dwStatus* member of the property structure indicates DBPROPSTATUS_BADVALUE.|  
|DBPROP_INDEX_NULLS|R/W: Read/write<br /><br /> Default: None<br /><br /> Description: The OLE DB Driver for SQL Server does not support this property. Attempts to set the property in **CreateIndex** cause a DB_S_ERRORSOCCURRED return value. The *dwStatus* member of the property structure indicates DBPROPSTATUS_BADVALUE.|  
|DBPROP_INDEX_PRIMARYKEY|R/W: Read/write<br /><br /> Default: VARIANT_FALSE Description: Creates the index as a referential integrity, PRIMARY KEY constraint.<br /><br /> VARIANT_TRUE: The index is created to support the PRIMARY KEY constraint of the table. The columns must be nonnullable.<br /><br /> VARIANT_FALSE: The index is not used as a PRIMARY KEY constraint for row values in the table.|  
|DBPROP_INDEX_SORTBOOKMARKS|R/W: Read/write<br /><br /> Default: None<br /><br /> Description: The OLE DB Driver for SQL Server does not support this property. Attempts to set the property in **CreateIndex** cause a DB_S_ERRORSOCCURRED return value. The *dwStatus* member of the property structure indicates DBPROPSTATUS_BADVALUE.|  
|DBPROP_INDEX_TEMPINDEX|R/W: Read/write<br /><br /> Default: None<br /><br /> Description: The OLE DB Driver for SQL Server does not support this property. Attempts to set the property in **CreateIndex** cause a DB_S_ERRORSOCCURRED return value. The *dwStatus* member of the property structure indicates DBPROPSTATUS_BADVALUE.|  
|DBPROP_INDEX_TYPE|R/W: Read/write<br /><br /> Default: None<br /><br /> Description: The OLE DB Driver for SQL Server does not support this property. Attempts to set the property in **CreateIndex** cause a DB_S_ERRORSOCCURRED return value. The *dwStatus* member of the property structure indicates DBPROPSTATUS_BADVALUE.|  
|DBPROP_INDEX_UNIQUE|R/W: Read/write<br /><br /> Default: VARIANT_FALSE<br /><br /> Description: Creates the index as a UNIQUE constraint on the participating column or columns.<br /><br /> VARIANT_TRUE: The index is used to uniquely constrain row values in the table.<br /><br /> VARIANT_FALSE: The index does not uniquely constrain row values.|  
  
 In the provider-specific property set DBPROPSET_SQLSERVERINDEX, the OLE DB Driver for SQL Server defines the following data source information property.  
  
|Property ID|Description|  
|-----------------|-----------------|  
|SSPROP_INDEX_XML|Type: VT_BOOL (R/W)<br /><br /> Default: VARIANT_FALSE<br /><br /> Description: When this property is specified with a value of VARIANT_TRUE with IIndexDefinition::CreateIndex, it results in a primary xml index being created corresponding to the column being indexed. If this property is VARIANT_TRUE, cIndexColumnDescs should be 1, otherwise it is an error.|  
  
 This example creates a primary key index:  
  
```  
// This CREATE TABLE statement shows the referential integrity and   
// PRIMARY KEY constraint on the OrderDetails table that will be created   
// by the following example code.  
//  
// CREATE TABLE OrderDetails  
// (  
//    OrderID      int      NOT NULL  
//    ProductID   int      NOT NULL  
//        CONSTRAINT PK_OrderDetails  
//        PRIMARY KEY CLUSTERED (OrderID, ProductID),  
//    UnitPrice   money      NOT NULL,  
//    Quantity   int      NOT NULL,  
//    Discount   decimal(2,2)   NOT NULL  
//        DEFAULT 0  
// )  
//  
HRESULT CreatePrimaryKey  
    (  
    IIndexDefinition* pIIndexDefinition  
    )  
    {  
    HRESULT             hr = S_OK;  
  
    DBID                dbidTable;  
    DBID                dbidIndex;  
    const ULONG         nCols = 2;  
    ULONG               nCol;  
    const ULONG         nProps = 2;  
    ULONG               nProp;  
  
    DBINDEXCOLUMNDESC   dbidxcoldesc[nCols];  
    DBPROP              dbpropIndex[nProps];  
    DBPROPSET           dbpropset;  
  
    DBID*               pdbidIndexOut = NULL;  
  
    // Set up identifiers for the table and index.  
    dbidTable.eKind = DBKIND_NAME;  
    dbidTable.uName.pwszName = L"OrderDetails";  
  
    dbidIndex.eKind = DBKIND_NAME;  
    dbidIndex.uName.pwszName = L"PK_OrderDetails";  
  
    // Set up column identifiers.  
    for (nCol = 0; nCol < nCols; nCol++)  
        {  
        dbidxcoldesc[nCol].pColumnID = new DBID;  
        dbidxcoldesc[nCol].pColumnID->eKind = DBKIND_NAME;  
  
        dbidxcoldesc[nCol].eIndexColOrder = DBINDEX_COL_ORDER_ASC;  
        }  
    dbidxcoldesc[0].pColumnID->uName.pwszName = L"OrderID";  
    dbidxcoldesc[1].pColumnID->uName.pwszName = L"ProductID";  
  
    // Set properties for the index. The index is clustered,  
    // PRIMARY KEY.  
    for (nProp = 0; nProp < nProps; nProp++)  
        {  
        dbpropIndex[nProp].dwOptions = DBPROPOPTIONS_REQUIRED;  
        dbpropIndex[nProp].colid = DB_NULLID;  
  
        VariantInit(&(dbpropIndex[nProp].vValue));  
  
        dbpropIndex[nProp].vValue.vt = VT_BOOL;  
        }  
    dbpropIndex[0].dwPropertyID = DBPROP_INDEX_CLUSTERED;  
    dbpropIndex[0].vValue.boolVal = VARIANT_TRUE;  
  
    dbpropIndex[1].dwPropertyID = DBPROP_INDEX_PRIMARYKEY;  
    dbpropIndex[1].vValue.boolVal = VARIANT_TRUE;  
  
    dbpropset.rgProperties = dbpropIndex;  
    dbpropset.cProperties = nProps;  
    dbpropset.guidPropertySet = DBPROPSET_INDEX;  
  
    hr = pIIndexDefinition->CreateIndex(&dbidTable, &dbidIndex, nCols,  
        dbidxcoldesc, 1, &dbpropset, &pdbidIndexOut);  
  
    // Clean up dynamically allocated DBIDs.  
    for (nCol = 0; nCol < nCols; nCol++)  
        {  
        delete dbidxcoldesc[nCol].pColumnID;  
        }  
  
    return (hr);  
    }  
```  
  
## See Also  
 [Tables and Indexes](../../oledb/ole-db-tables-indexes/tables-and-indexes.md)  
  
  
