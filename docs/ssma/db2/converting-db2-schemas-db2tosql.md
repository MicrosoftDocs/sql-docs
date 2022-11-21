---
description: "Converting DB2 Schemas (DB2ToSQL)"
title: "Converting DB2 Schemas (DB2ToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 7947efc3-ca86-4ec5-87ce-7603059c75a0
author: cpichuka 
ms.author: cpichuka 
f1_keywords: 
  - "ssma.db2.convert.f1"
---
# Converting DB2 Schemas (DB2ToSQL)
After you have connected to DB2, connected to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and set project and data mapping options, you can convert DB2 database objects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database objects.  
  
## The Conversion Process  
Converting database objects takes the object definitions from DB2, converts them to similar [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects, and then loads this information into the SSMA metadata. It doesn't load the information into the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can then view the objects and their properties by using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Metadata Explorer.  
  
During the conversion, SSMA prints output messages to the Output pane and error messages to the Error List pane. Use the output and error information to determine whether you have to modify your DB2 databases or your conversion process to obtain the desired conversion results.  
  
## Setting Conversion Options  
Before converting objects, review the project conversion options in the **Project Settings** dialog box. By using this dialog box, you can set how SSMA converts functions and global variables. For more information, see [Project Settings &#40;Conversion&#41; &#40;DB2ToSQL&#41;](../../ssma/db2/project-settings-conversion-db2tosql.md).  
  
## Conversion Results  
The following table shows which DB2 objects are converted, and the resulting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects:  
  
|DB2 Objects|Resulting SQL Server Objects|  
|-----------|----------------------------|  
|Data Types|**SSMA maps every type except the following listed below:**<br /><br />CLOB: Some native functions for work with this type is not supported (for example,  CLOB_EMPTY())<br /><br />BLOB: Some native functions for work with this type aren't supported  (for example,  BLOB_EMPTY())<br /><br />DBLOB: Some native functions for work with this type aren't supported  (for example,  DBLOB_EMPTY())|  
|User-Defined Types|**SSMA maps the following User-Defined:**<br /><br />Distinct Type<br /><br />Structured Type<br /><br />SQL PL data types - Note: Weak cursor type aren't supported.|  
|Special Registers|**SSMA only maps registers listed below:**<br /><br />CURRENT TIMESTAMP<br /><br />CURRENT DATE<br /><br />CURRENT TIME<br /><br />CURRENT TIMEZONE<br /><br />CURRENT USER<br /><br />SESSION_USER and USER<br /><br />SYSTEM_USER<br /><br />CURRENT CLIENT_APPLNAME<br /><br />CURRENT CLIENT_WRKSTNNAME<br /><br />CURRENT LOCK TIMEOUT<br /><br />CURRENT SCHEMA<br /><br />CURRENT SERVER<br /><br />CURRENT ISOLATION<br /><br />Other Special Registers aren't mapped to SQL server semantic.|  
|CREATE TABLE|**SSMA maps CREATE TABLE with the following exceptions:**<br /><br />Multidimensional clustering (MDC) tables<br /><br />Range-clustered tables (RCT)<br /><br />Partitioned tables<br /><br />Detached table<br /><br />DATA CAPTURE clause<br /><br />IMPLICITLY HIDDEN option<br /><br />VOLATILE option|  
|CREATE VIEW|SSMA maps CREATE VIEW with 'WITH LOCAL CHECK OPTION' but other options aren't mapped to SQL server semantics|  
|CREATE INDEX|**SSMA maps CREATE INDEX with the following exceptions:**<br /><br />XML index<br /><br />BUSINESS_TIME WITHOUT OVERLAPS option<br /><br />PARTITIONED clause<br /><br />SPECIFICATION ONLY option<br /><br />EXTEND USING option<br /><br />MINPCTUSED option<br /><br />PAGE SPLIT option|  
|Triggers|**SSMA maps the following trigger semantics:**<br /><br />AFTER / FOR EACH ROW Triggers<br /><br />AFTER /FOR EACH STATEMENT Triggers<br /><br />BEFORE / FOR EACH ROW and INSTEAD OF / FOR EACH ROW Triggers|  
|Sequences|Are mapped.|  
|SELECT Statement|**SSMA maps SELECT with the following exceptions:**<br /><br />Data-change-table-reference clause - Partially mapped, but FINAL tables don't supported<br /><br />Table-reference clause - Partially mapped, but only-table-reference, outer-table-reference, analyze_table-expression, collection-derived-table, xmltable-expression aren't mapped to SQL server semantics<br /><br />Period-specification clause - Not mapped.<br /><br />Continue-handler clause - Not mapped.<br /><br />Typed-correlation clause - Not mapped.<br /><br />Concurrent-access-resolution clause - Not mapped.|  
|VALUES Statement|Is mapped.|  
|INSERT Statement|Is mapped.|  
|UPDATE Statement|S**SMA maps UPDATE with the following exceptions:**<br /><br />Table-reference clause - Only-table-reference is not mapped to SQL server semantics<br /><br />Period clause - Is not mapped.|  
|MERGE Statement|**SSMA maps MERGE with the following exceptions:**<br /><br />Single vs Multiple Occurrences of Each Clause - Is mapped to SQL server semantics for limited occurrences of each clause<br /><br />SIGNAL Clause - doesn't map to SQL Server semantics<br /><br />Mixed UPDATE and DELETE Clauses - doesn't map to SQL Server semantics<br /><br />Period-clause - doesn't map to SQL Server semantics|  
|DELETE Statement|**SSMA maps DELETE with the following exceptions:**<br /><br />Table-reference clause - Only-table-reference is not mapped to SQL server semantics<br /><br />Period clause - doesn't map to SQL Server semantics|  
|Isolation Level and Lock Type|Is mapped.|  
|Procedures (SQL)|Are mapped.|  
|Procedures (external)|Require manual update.|  
|Procedures (Sourced)|Do not map to SQL Server semantics.|  
|Assignment statement|Is mapped.|  
|CALL Statement for a Procedure|Is mapped.|  
|CASE Statement|Is mapped.|  
|FOR Statement|Is mapped.|  
|GOTO statement|Is mapped.|  
|IF Statement|Is mapped.|  
|ITERATE Statement|Is mapped.|  
|LEAVE Statement|Is mapped.|  
|LOOP Statement|Is mapped.|  
|REPEAT Statement|Is mapped.|  
|RESIGNAL Statement|Conditions aren't supported. Messages can be optional.|  
|RETURN Statement|Is mapped.|  
|SIGNAL Statement|Conditions aren't supported. Messages can be optional.|  
|WHILE Statement|Is mapped.|  
|GET DIAGNOSTICS Statement|**SSMA maps GET DIAGNOSTICS with the following exceptions:**<br /><br />ROW_COUNT - Is mapped.<br /><br />DB2_RETURN_STATUS - Is mapped.<br /><br />MESSAGE_TEXT - Is mapped.<br /><br />DB2_SQL_NESTING_LEVEL - doesn't map to SQL Server semantics<br /><br />DB2_TOKEN_STRING - doesn't map to SQL Server semantics|  
|Cursors|**SSMA maps CURSORS with the following exceptions:**<br /><br />ALLOCATE CURSOR statement - doesn't map to SQL Server semantics<br /><br />ASSOCIATE LOCATORS statement - doesn't map to SQL Server semantics<br /><br />DECLARE CURSOR statement - Returnability clause is not mapped to SQL server semantics<br /><br />FETCH statement - Partial mapping. Variables as target are supported only. SQLDA DESCRIPTOR is not  mapped to SQL server semantics|  
|Variables|Are mapped.|  
|Exceptions, Handlers, and Conditions|**SSMA maps "exception handling" with the following exceptions:**<br /><br />EXIT Handlers - Are mapped.<br /><br />UNDO Handlers - Are mapped.<br /><br />CONTINUE Handlers - Aren't mapped.<br /><br />Conditions - It doesn't map to SQL server semantics.|  
|Dynamic SQL|Not mapped.|  
|Aliases|Are mapped.|  
|Nicknames|Partial mapping. Manual processing is required for underlying object|  
|Synonyms|Are mapped.|  
|Standard Functions in DB2|SSMA maps DB2 standard functions when an equivalent function is available in SQL Server:|  
|Authorization|Not mapped.|  
|Predicates|Are mapped.|  
|SELECT INTO statement|Not mapped.|  
|VALUES INTO statement|Not mapped.|  
|Transaction control|Not mapped.|  
  
## Converting DB2 Database Objects  
To convert DB2 database objects, you first select the objects that you want to convert, and then have SSMA perform the conversion. To view output messages during the conversion, on the **View** menu, select **Output**.  
  
**To convert DB2 objects to SQL Server syntax**  
  
1.  In DB2 Metadata Explorer, expand the DB2 server, and then expand **Schemas**.  
  
2.  Select objects to convert:  
  
    -   To convert all schemas, select the check box next to **Schemas**.  
  
    -   To convert or omit a database, select the check box next to the schema name.  
  
    -   To convert or omit a category of objects, expand a schema, and then select or clear the check box next to the category.  
  
    -   To convert or omit individual objects, expand the category folder, and then select or clear the check box next to the object.  
  
3.  To convert all selected objects, right-click **Schemas** and select **Convert Schema**.  
  
    You can also convert individual objects or categories of objects by right-clicking the object or its parent folder, and then selecting **Convert Schema**.  
  
## Viewing Conversion Problems  
Some DB2 objects might not be converted. You can determine the conversion success rates by viewing the summary conversion report.  
  
**To view a summary report**  
  
1.  In DB2 Metadata Explorer, select **Schemas**.  
  
2.  In the right pane, select the **Report** tab.  
  
    This report shows the summary assessment report for all database objects that have been assessed or converted. You can also view a summary report for individual objects:  
  
    -   To view the report for an individual schema, select the schema in DB2 Metadata Explorer.  
  
    -   To view the report for an individual object, select the object in DB2 Metadata Explorer. Objects that have conversion problems have a red error icon.  
  
For objects that failed conversion, you can view the syntax that resulted in the conversion failure.  
  
**To view individual conversion problems**  
  
1.  In DB2 Metadata Explorer, expand **Schemas**.  
  
2.  Expand the schema that shows a red error icon.  
  
3.  Under the schema, expand a folder that has a red error icon.  
  
4.  Select the object that has a red error icon.  
  
5.  In the right pane, click the **Report** tab.  
  
6.  At the top of the **Report** tab is a drop-down list. If the list shows **Statistics**, change the selection to **Source**.  
  
    SSMA will display the source code and several buttons immediately above the code.  
  
7.  Click the **Next Problem** button. This is a red error icon with an arrow that points to the right.  
  
    SSMA will highlight the first problematic source code it finds in the current object.  
  
For each item that could not be converted, you've to determine what you want to do with that object:  
  
-   You can modify the source code for procedures on the **SQL** tab.  
  
-   You can modify the object in the DB2 database to remove or revise problematic code. To load the updated code into SSMA, you will have to update the metadata. For more information, see [Connecting to DB2 Database &#40;DB2ToSQL&#41;](../../ssma/db2/connecting-to-db2-database-db2tosql.md).  
  
-   You can exclude the object from migration. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Metadata Explorer and DB2 Metadata Explorer, clear the check box next to the item before loading the objects into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and migrating data from DB2.  
  
## Next Step  
The next step in the migration process is to [Load the converted objects into SQL Server](./loading-converted-database-objects-into-sql-server-db2tosql.md).  
  
## See Also  
[Migrating DB2 Data into SQL Server &#40;DB2ToSQL&#41;](../../ssma/db2/migrating-db2-data-into-sql-server-db2tosql.md)  
