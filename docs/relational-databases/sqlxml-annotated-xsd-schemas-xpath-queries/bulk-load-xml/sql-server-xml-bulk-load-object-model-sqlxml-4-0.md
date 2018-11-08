---
title: "SQL Server XML Bulk Load Object Model (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "bulk load [SQLXML], object model"
  - "ErrorLogFile property"
  - "SGDropTables property"
  - "SGUseID property"
  - "KeepNulls property"
  - "ConnectionCommand property"
  - "SchemaGen property"
  - "XMLFragment property"
  - "SQLXMLBulkLoad object"
  - "ForceTableLock property"
  - "CheckConstraints property"
  - "BulkLoad property"
  - "TempFilePath property"
  - "IgnoreDuplicateKeys property"
  - "Transaction property"
  - "KeepIdentity property"
  - "ConnectionString property"
  - "FireTriggers property"
  - "Execute method"
  - "XML Bulk Load [SQLXML], object model"
ms.assetid: a9efbbde-ed2b-4929-acc1-261acaaed19d
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQL Server XML Bulk Load Object Model (SQLXML 4.0)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  The Microsoft [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] XML Bulk Load object model consists of the SQLXMLBulkLoad object. This object supports the following methods and properties.  
  
## Methods  
 Execute  
 Bulk loads the data by using the schema file and data file (or stream) that are provided as parameters.  
  
## Properties  
 BulkLoad  
 Specifies whether a Bulk Load should be performed. This property is useful if you want to generate only the schemas (see the SchemaGen, SGDropTables, and SGUseID properties that follow) and not perform a bulk load. This is a Boolean property. When the property is set to TRUE, XML Bulk Load executes. When it is set to FALSE, XML Bulk Load does not execute.  
  
 The default value is TRUE.  
  
 CheckConstraints  
 Specifies whether the constraints (such as constraints due to the primary key/foreign key relationship among columns) that are specified on the column should be checked when XML Bulk Load inserts data into the columns. This is a Boolean property.  
  
 When the property is set to TRUE, XML Bulk Load checks the constraints for each value inserted (which means that a constraint violation results in an error).  
  
> [!NOTE]  
>  To leave this property as FALSE, you must have **ALTER TABLE** permissions on target tables. For more information, see [ALTER TABLE &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-table-transact-sql.md).  
  
 The default value is FALSE. When it is set to FALSE, XML Bulk Load ignores the constraints during an insert operation. In the current implementation, you must define the tables in the order of primary key and foreign key relationships in the mapping schema. That is, a table with a primary key must be defined before the corresponding table with the foreign key; otherwise, XML Bulk Load fails.  
  
 Note that if ID Propagation is being done, then this option does not apply and constraint checking will be left on. This occurs when `KeepIdentity=False` and there is a relationship defined where the parent is an identity field and the value is given to the child as it is generated.  
  
 ConnectionCommand  
 Identifies an existing connection object (for example, the ADO or ICommand command object) that XML Bulk Load should use. You can use the ConnectionCommand property instead of specifying a connection string with the ConnectionString property. The Transaction property must be set to TRUE if you use ConnectionCommand.  
  
 If you use both the ConnectionString and ConnectionCommand properties, XML Bulk Load uses the last specified property.  
  
 The default value is NULL.  
  
 ConnectionString  
 Identifies the OLE DB connection string that provides the necessary information to establish a connection to an instance of the database. If you use both the ConnectionString and ConnectionCommand properties, XML Bulk Load uses the last specified property.  
  
 The default value is NULL.  
  
 ErrorLogFile  
 Specifies the file name into which the XML Bulk Load logs errors and messages. The default is an empty string, in which case no logging takes place.  
  
 FireTriggers  
 Specifies if triggers defined on target tables should fire during the Bulk Load operation. The default is FALSE.  
  
 When set to TRUE, triggers will fire as per normal during insert operations.  
  
> [!NOTE]  
>  To leave this property as FALSE, you must have **ALTER TABLE** permissions on target tables. For more information, see [ALTER TABLE &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-table-transact-sql.md).  
  
 Note that if ID Propagation is being done, then this option does not apply and triggers will be left on. This occurs when `KeepIdentity=False` and there is a relationship defined where the parent is an identity field and the value is given to the child as it is generated.  
  
 ForceTableLock  
 Specifies whether the tables into which XML Bulk Load copies data should be locked for the duration of the Bulk Load. This is a Boolean property. When the property is set to TRUE, XML Bulk Load acquires table locks for the duration of the Bulk Load. When it is set to FALSE, XML Bulk Load acquires a table lock each time it inserts a record into a table.  
  
 The default value is FALSE.  
  
 IgnoreDuplicateKeys  
 Specifies what to do if an attempt is made to insert duplicate values in a key column. If this property is set to TRUE and an attempt is made to insert a record with a duplicate value in a key column, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does not insert that record. But it does insert the subsequent record; thus, the Bulk Load operation does not fail. If this property is set to FALSE, Bulk Load fails when an attempt is made to insert a duplicate value in a key column.  
  
 When the IgnoreDuplicateKeys property is set to TRUE, a COMMIT statement is issued for every record inserted in the table. This slows down the performance. The property can be set to TRUE only when the Transaction property is set to FALSE, because the transactional behavior is implemented using files.  
  
 The default value is FALSE.  
  
 KeepIdentity  
 Specifies how to deal with the values for an Identity type column in the source file. This is a Boolean property. When the property is set to TRUE, XML Bulk Load assigns the values that are specified in the source file to the identity column. When the property is set to FALSE, the bulk-load operation ignores the identity-column values that are specified in the source. In this case, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] assigns a value to the identity column.  
  
 If the Bulk Load involves a column that is a foreign key referring to an identity column in which [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]-generated values are stored, Bulk Load appropriately propagates these identity values to the foreign key column.  
  
 The value of this property applies to all columns involved in the bulk load. The default value is TRUE.  
  
> [!NOTE]  
>  To leave this property as TRUE, you must have **ALTER TABLE** permissions on target tables. Otherwise, it must be set to a value of FALSE. For more information, see [ALTER TABLE &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-table-transact-sql.md).  
  
 KeepNulls  
 Specifies what value to use for a column that is missing a corresponding attribute or child element in the XML document. This is a Boolean property. When the property is set to TRUE, XML Bulk Load assigns a null value to the column. It does not assign the column's default value, if any, as set on the server. The value of this property applies to all columns involved in the bulk load.  
  
 The default value is FALSE.  
  
 SchemaGen  
 Specifies whether to create the required tables before performing a Bulk Load operation. This is a Boolean property. If this property is set to TRUE, the tables identified in the mapping schema are created (the database must exist). If one or more of the tables already exist in the database, the SGDropTables property determines whether these preexisting tables are to be dropped and re-created.  
  
 The default value for the SchemaGen property is FALSE. SchemaGen does not create PRIMARY KEY constraints on the newly created tables. SchemaGen does, however, create FOREIGN KEY constraints in the database if it can find matching **sql:relationship** and **sql:key-fields** annotations in the mapping schema and if the key field consists of a single column.  
  
 Note that if you set the SchemaGen property to TRUE, XML Bulk Load does the following:  
  
-   Creates the necessary tables from the element and attribute names. Therefore, it is important that you do not use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] reserved words for element and attribute names in the schema.  
  
-   Returns overflow data for any column designated using the [sql:overflow-field](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/bulk-load-xml/annotation-interpretation-sql-overflow-field.md) in [xml data type](../../../t-sql/xml/xml-transact-sql.md) format.  
  
 SGDropTables  
 Specifies whether existing tables should be dropped and re-created. You use this property when the SchemaGen property is set to TRUE. If SGDropTables is FALSE, the existing tables are retained. When this property is TRUE, the existing tables are deleted and re-created.  
  
 The default value is FALSE.  
  
 SGUseID  
 Specifies whether the attribute in the mapping schema that is identified as **id** type can be used in creating a PRIMARY KEY constraint when the table is created. Use this property when the SchemaGen property is set to TRUE. If SGUseID is TRUE, the SchemaGen utility uses an attribute for which **dt:type="id"** is specified as the primary key column and adds the appropriate PRIMARY KEY constraint when creating the table.  
  
 The default value is FALSE.  
  
 TempFilePath  
 Specifies the file path where XML Bulk Load creates the temporary files for a transacted bulk load. (This property is useful only when the Transaction property is set to TRUE.) You must ensure that the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] account that is used for XML Bulk Load has access to this path. If this property is not set, XML Bulk Load stores the temporary files in the location that is specified in the TEMP environment variable.  
  
 Transaction  
 Specifies whether the Bulk Load should be done as a transaction, in which case the rollback is guaranteed if the Bulk Load fails. This is a Boolean property. If the property is set to TRUE, the Bulk Load occurs in a transactional context. The TempFilePath property is useful only when Transaction is set to TRUE.  
  
> [!NOTE]  
>  If you are loading binary data (such as the bin.hex, bin.base64 XML data types to the binary, image [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types), the Transaction property must be set to FALSE.  
  
 The default value is FALSE.  
  
 XMLFragment  
 Specifies whether the source data is an XML fragment. An XML fragment is an XML document with no single, top-level (root) element. This is a Boolean property. This property must be set to TRUE if the source file consists of an XML fragment.  
  
 The default value is FALSE.  
  
  
