---
title: "DECLARE CURSOR (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DECLARE_CURSOR_TSQL"
  - "CURSOR_TSQL"
  - "DECLARE CURSOR"
  - "CURSOR"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DECLARE CURSOR statement"
  - "cursors [SQL Server], attributes"
  - "local cursors [SQL Server]"
  - "nesting cursors"
  - "Transact-SQL cursors, attributes"
  - "global cursors [SQL Server]"
ms.assetid: 5a3a27aa-03e8-4c98-a27e-809282379b21
caps.latest.revision: 51
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# DECLARE CURSOR (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Defines the attributes of a [!INCLUDE[tsql](../../includes/tsql-md.md)] server cursor, such as its scrolling behavior and the query used to build the result set on which the cursor operates. DECLARE CURSOR accepts both a syntax based on the ISO standard and a syntax using a set of [!INCLUDE[tsql](../../includes/tsql-md.md)] extensions.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
ISO Syntax  
DECLARE cursor_name [ INSENSITIVE ] [ SCROLL ] CURSOR   
     FOR select_statement   
     [ FOR { READ ONLY | UPDATE [ OF column_name [ ,...n ] ] } ]  
[;]  
Transact-SQL Extended Syntax  
DECLARE cursor_name CURSOR [ LOCAL | GLOBAL ]   
     [ FORWARD_ONLY | SCROLL ]   
     [ STATIC | KEYSET | DYNAMIC | FAST_FORWARD ]   
     [ READ_ONLY | SCROLL_LOCKS | OPTIMISTIC ]   
     [ TYPE_WARNING ]   
     FOR select_statement   
     [ FOR UPDATE [ OF column_name [ ,...n ] ] ]  
[;]  
```  
  
## Arguments  
 *cursor_name*  
 Is the name of the [!INCLUDE[tsql](../../includes/tsql-md.md)] server cursor defined. *cursor_name* must conform to the rules for identifiers.  
  
 INSENSITIVE  
 Defines a cursor that makes a temporary copy of the data to be used by the cursor. All requests to the cursor are answered from this temporary table in **tempdb**; therefore, modifications made to base tables are not reflected in the data returned by fetches made to this cursor, and this cursor does not allow modifications. When ISO syntax is used, if INSENSITIVE is omitted, committed deletes and updates made to the underlying tables (by any user) are reflected in subsequent fetches.  
  
 SCROLL  
 Specifies that all fetch options (FIRST, LAST, PRIOR, NEXT, RELATIVE, ABSOLUTE) are available. If SCROLL is not specified in an ISO DECLARE CURSOR, NEXT is the only fetch option supported. SCROLL cannot be specified if FAST_FORWARD is also specified.  
  
 *select_statement*  
 Is a standard SELECT statement that defines the result set of the cursor. The keywords FOR BROWSE, and INTO are not allowed within *select_statement* of a cursor declaration.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] implicitly converts the cursor to another type if clauses in *select_statement* conflict with the functionality of the requested cursor type.  
  
 READ ONLY  
 Prevents updates made through this cursor. The cursor cannot be referenced in a WHERE CURRENT OF clause in an UPDATE or DELETE statement. This option overrides the default capability of a cursor to be updated.  
  
 UPDATE [OF *column_name* [**,**...*n*]]  
 Defines updatable columns within the cursor. If OF *column_name* [**,**..*.n*] is specified, only the columns listed allow modifications. If UPDATE is specified without a column list, all columns can be updated.  
  
 *cursor_name*  
 Is the name of the [!INCLUDE[tsql](../../includes/tsql-md.md)] server cursor defined. *cursor_name* must conform to the rules for identifiers.  
  
 LOCAL  
 Specifies that the scope of the cursor is local to the batch, stored procedure, or trigger in which the cursor was created. The cursor name is only valid within this scope. The cursor can be referenced by local cursor variables in the batch, stored procedure, or trigger, or a stored procedure OUTPUT parameter. An OUTPUT parameter is used to pass the local cursor back to the calling batch, stored procedure, or trigger, which can assign the parameter to a cursor variable to reference the cursor after the stored procedure terminates. The cursor is implicitly deallocated when the batch, stored procedure, or trigger terminates, unless the cursor was passed back in an OUTPUT parameter. If it is passed back in an OUTPUT parameter, the cursor is deallocated when the last variable referencing it is deallocated or goes out of scope.  
  
 GLOBAL  
 Specifies that the scope of the cursor is global to the connection. The cursor name can be referenced in any stored procedure or batch executed by the connection. The cursor is only implicitly deallocated at disconnect.  
  
> [!NOTE]  
>  If neither GLOBAL or LOCAL is specified, the default is controlled by the setting of the **default to local cursor** database option.  
  
 FORWARD_ONLY  
 Specifies that the cursor can only be scrolled from the first to the last row. FETCH NEXT is the only supported fetch option. If FORWARD_ONLY is specified without the STATIC, KEYSET, or DYNAMIC keywords, the cursor operates as a DYNAMIC cursor. When neither FORWARD_ONLY nor SCROLL is specified, FORWARD_ONLY is the default, unless the keywords STATIC, KEYSET, or DYNAMIC are specified. STATIC, KEYSET, and DYNAMIC cursors default to SCROLL. Unlike database APIs such as ODBC and ADO, FORWARD_ONLY is supported with STATIC, KEYSET, and DYNAMIC [!INCLUDE[tsql](../../includes/tsql-md.md)] cursors.  
  
 STATIC  
 Defines a cursor that makes a temporary copy of the data to be used by the cursor. All requests to the cursor are answered from this temporary table in **tempdb**; therefore, modifications made to base tables are not reflected in the data returned by fetches made to this cursor, and this cursor does not allow modifications.  
  
 KEYSET  
 Specifies that the membership and order of rows in the cursor are fixed when the cursor is opened. The set of keys that uniquely identify the rows is built into a table in **tempdb** known as the **keyset**.  
  
> [!NOTE]  
>  If the query references at least one table without a unique index, the keyset cursor is converted to a static cursor.  
  
 Changes to nonkey values in the base tables, either made by the cursor owner or committed by other users, are visible as the owner scrolls around the cursor. Inserts made by other users are not visible (inserts cannot be made through a [!INCLUDE[tsql](../../includes/tsql-md.md)] server cursor). If a row is deleted, an attempt to fetch the row returns an @@FETCH_STATUS of -2. Updates of key values from outside the cursor resemble a delete of the old row followed by an insert of the new row. The row with the new values is not visible, and attempts to fetch the row with the old values return an @@FETCH_STATUS of -2. The new values are visible if the update is done through the cursor by specifying the WHERE CURRENT OF clause.  
  
 DYNAMIC  
 Defines a cursor that reflects all data changes made to the rows in its result set as you scroll around the cursor. The data values, order, and membership of the rows can change on each fetch. The ABSOLUTE fetch option is not supported with dynamic cursors.  
  
 FAST_FORWARD  
 Specifies a FORWARD_ONLY, READ_ONLY cursor with performance optimizations enabled. FAST_FORWARD cannot be specified if SCROLL or FOR_UPDATE is also specified.  
  
> [!NOTE]  
>  Both FAST_FORWARD and FORWARD_ONLY can be used in the same DECLARE CURSOR statement.  
  
 READ_ONLY  
 Prevents updates made through this cursor. The cursor cannot be referenced in a WHERE CURRENT OF clause in an UPDATE or DELETE statement. This option overrides the default capability of a cursor to be updated.  
  
 SCROLL_LOCKS  
 Specifies that positioned updates or deletes made through the cursor are guaranteed to succeed. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] locks the rows as they are read into the cursor to ensure their availability for later modifications. SCROLL_LOCKS cannot be specified if FAST_FORWARD or STATIC is also specified.  
  
 OPTIMISTIC  
 Specifies that positioned updates or deletes made through the cursor do not succeed if the row has been updated since it was read into the cursor. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not lock rows as they are read into the cursor. It instead uses comparisons of **timestamp** column values, or a checksum value if the table has no **timestamp** column, to determine whether the row was modified after it was read into the cursor. If the row was modified, the attempted positioned update or delete fails. OPTIMISTIC cannot be specified if FAST_FORWARD is also specified.  
  
 TYPE_WARNING  
 Specifies that a warning message is sent to the client when the cursor is implicitly converted from the requested type to another.  
  
 *select_statement*  
 Is a standard SELECT statement that defines the result set of the cursor. The keywords COMPUTE, COMPUTE BY, FOR BROWSE, and INTO are not allowed within *select_statement* of a cursor declaration.  
  
> [!NOTE]  
>  You can use a query hint within a cursor declaration; however, if you also use the FOR UPDATE OF clause, specify OPTION (*query_hint*) after FOR UPDATE OF.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] implicitly converts the cursor to another type if clauses in *select_statement* conflict with the functionality of the requested cursor type. For more information, see Implicit Cursor Conversions.  
  
 FOR UPDATE [OF *column_name* [**,**...*n*]]  
 Defines updatable columns within the cursor. If OF *column_name* [**,**...*n*] is supplied, only the columns listed allow modifications. If UPDATE is specified without a column list, all columns can be updated, unless the READ_ONLY concurrency option was specified.  
  
## Remarks  
 DECLARE CURSOR defines the attributes of a [!INCLUDE[tsql](../../includes/tsql-md.md)] server cursor, such as its scrolling behavior and the query used to build the result set on which the cursor operates. The OPEN statement populates the result set, and FETCH returns a row from the result set. The CLOSE statement releases the current result set associated with the cursor. The DEALLOCATE statement releases the resources used by the cursor.  
  
 The first form of the DECLARE CURSOR statement uses the ISO syntax for declaring cursor behaviors. The second form of DECLARE CURSOR uses [!INCLUDE[tsql](../../includes/tsql-md.md)] extensions that allow you to define cursors using the same cursor types used in the database API cursor functions of ODBC or ADO.  
  
 You cannot mix the two forms. If you specify the SCROLL or INSENSITIVE keywords before the CURSOR keyword, you cannot use any keywords between the CURSOR and FOR *select_statement* keywords. If you specify any keywords between the CURSOR and FOR *select_statement* keywords, you cannot specify SCROLL or INSENSITIVE before the CURSOR keyword.  
  
 If a DECLARE CURSOR using [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax does not specify READ_ONLY, OPTIMISTIC, or SCROLL_LOCKS, the default is as follows:  
  
-   If the SELECT statement does not support updates (insufficient permissions, accessing remote tables that do not support updates, and so on), the cursor is READ_ONLY.  
  
-   STATIC and FAST_FORWARD cursors default to READ_ONLY.  
  
-   DYNAMIC and KEYSET cursors default to OPTIMISTIC.  
  
 Cursor names can be referenced only by other [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. They cannot be referenced by database API functions. For example, after declaring a cursor, the cursor name cannot be referenced from OLE DB, ODBC or ADO functions or methods. The cursor rows cannot be fetched using the fetch functions or methods of the APIs; the rows can be fetched only by [!INCLUDE[tsql](../../includes/tsql-md.md)] FETCH statements.  
  
 After a cursor has been declared, these system stored procedures can be used to determine the characteristics of the cursor.  
  
|System stored procedures|Description|  
|------------------------------|-----------------|  
|**sp_cursor_list**|Returns a list of cursors currently visible on the connection and their attributes.|  
|**sp_describe_cursor**|Describes the attributes of a cursor, such as whether it is a forward-only or scrolling cursor.|  
|**sp_describe_cursor_columns**|Describes the attributes of the columns in the cursor result set.|  
|**sp_describe_cursor_tables**|Describes the base tables accessed by the cursor.|  
  
 Variables may be used as part of the *select_statement* that declares a cursor. Cursor variable values do not change after a cursor is declared.  
  
## Permissions  
 DECLARE CURSOR permissions default to any user that has SELECT permissions on the views, tables, and columns used in the cursor.
 
## Limitations and Restrictions

You cannot use cursors or triggers on a table with a clustered columnstore index. This restriction does not apply to nonclustered columnstore indexes; you can use cursors and triggers on a table with a nonclustered columnstore index. 
  
## Examples  
  
### A. Using simple cursor and syntax  
 The result set generated at the opening of this cursor includes all rows and all columns in the table. This cursor can be updated, and all updates and deletes are represented in fetches made against this cursor. `FETCH``NEXT` is the only fetch available because the `SCROLL` option has not been specified.  
  
```  
DECLARE vend_cursor CURSOR  
    FOR SELECT * FROM Purchasing.Vendor  
OPEN vend_cursor  
FETCH NEXT FROM vend_cursor;  
```  
  
### B. Using nested cursors to produce report output  
 The following example shows how cursors can be nested to produce complex reports. The inner cursor is declared for each vendor.  
  
```  
SET NOCOUNT ON;  
  
DECLARE @vendor_id int, @vendor_name nvarchar(50),  
    @message varchar(80), @product nvarchar(50);  
  
PRINT '-------- Vendor Products Report --------';  
  
DECLARE vendor_cursor CURSOR FOR   
SELECT VendorID, Name  
FROM Purchasing.Vendor  
WHERE PreferredVendorStatus = 1  
ORDER BY VendorID;  
  
OPEN vendor_cursor  
  
FETCH NEXT FROM vendor_cursor   
INTO @vendor_id, @vendor_name  
  
WHILE @@FETCH_STATUS = 0  
BEGIN  
    PRINT ' '  
    SELECT @message = '----- Products From Vendor: ' +   
        @vendor_name  
  
    PRINT @message  
  
    -- Declare an inner cursor based     
    -- on vendor_id from the outer cursor.  
  
    DECLARE product_cursor CURSOR FOR   
    SELECT v.Name  
    FROM Purchasing.ProductVendor pv, Production.Product v  
    WHERE pv.ProductID = v.ProductID AND  
    pv.VendorID = @vendor_id  -- Variable value from the outer cursor  
  
    OPEN product_cursor  
    FETCH NEXT FROM product_cursor INTO @product  
  
    IF @@FETCH_STATUS <> 0   
        PRINT '         <<None>>'       
  
    WHILE @@FETCH_STATUS = 0  
    BEGIN  
  
        SELECT @message = '         ' + @product  
        PRINT @message  
        FETCH NEXT FROM product_cursor INTO @product  
        END  
  
    CLOSE product_cursor  
    DEALLOCATE product_cursor  
        -- Get the next vendor.  
    FETCH NEXT FROM vendor_cursor   
    INTO @vendor_id, @vendor_name  
END   
CLOSE vendor_cursor;  
DEALLOCATE vendor_cursor;  
```  
  
## See Also  
 [@@FETCH_STATUS &#40;Transact-SQL&#41;](../../t-sql/functions/fetch-status-transact-sql.md)   
 [CLOSE &#40;Transact-SQL&#41;](../../t-sql/language-elements/close-transact-sql.md)   
 [Cursors &#40;Transact-SQL&#41;](../../t-sql/language-elements/cursors-transact-sql.md)   
 [DEALLOCATE &#40;Transact-SQL&#41;](../../t-sql/language-elements/deallocate-transact-sql.md)   
 [FETCH &#40;Transact-SQL&#41;](../../t-sql/language-elements/fetch-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)  
  
  