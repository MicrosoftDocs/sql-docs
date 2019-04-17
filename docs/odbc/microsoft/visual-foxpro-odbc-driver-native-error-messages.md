---
title: "Visual FoxPro ODBC Driver Native Error Messages | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "error messages [ODBC], Visual FoxPro ODBC driver"
  - "Visual FoxPro ODBC driver [ODBC], error messages"
  - "FoxPro ODBC driver [ODBC], error messages"
ms.assetid: 7b2622e8-ccee-4853-9171-4fb10de0461d
author: MightyPen
ms.author: genemi
manager: craigg
---
# Visual FoxPro ODBC Driver Native Error Messages
The following tables list error messages native to the Visual FoxPro ODBC Driver.  
  
## 001  
  
|||  
|-|-|  
|1|Feature is not available.|  
|2|Input/output operation failure.|  
|3|Free handle is not found.|  
|5|Use of unallocated handle.|  
|99|Procedure canceled.|  
  
## 100  
  
|||  
|-|-|  
|100|Too many files open.|  
|101|Cannot open file.|  
|102|Cannot create file.|  
|105|Error writing to file.|  
|107|Invalid key length.|  
|109|Record is out of range.|  
|110|Record is not in index.|  
|111|Invalid file descriptor.|  
|113|File is not open.|  
|114|Not enough disk space for *value*.|  
|115|Invalid operation for the cursor.|  
|118|Index file does not match table.|  
|119|No table is open.|  
|120|File does not exist.|  
|121|File already exists.|  
|122|Table has no index order set.|  
|123|Not a table.|  
|125|Index expression exceeds maximum length.|  
|127|You must use a logical expression with a FOR or WHILE clause.|  
|128|Not a numeric expression.|  
|129|Variable is not found.|  
|132|File is in use.|  
|133|Index does not match the table. Delete the index file and  re-create the index.|  
|135|End of file encountered.|  
|136|Beginning of file encountered.|  
|137|Alias is not found.|  
|139|You must use a logical expression with FILTER.|  
|142|Cyclic relation.|  
|143|No fields were found to copy.|  
|144|The LOCATE command must be issued before the CONTINUE command.|  
|145|Must be a character or numeric key field.|  
|146|Cannot write to a read-only file.|  
|147|Target table is already engaged in a relation.|  
|148|Expression has been re-entered while the filter is executing.|  
|149|Not enough memory for buffer.|  
|150|Not enough memory for file map.|  
|155|Invalid buffdirty call.|  
|156|Duplicate field names.|  
|158|No fields found to process.|  
|159|Numeric overflow. Data was lost.|  
|162|Procedure '*value*' is not found.|  
|165|*value* is not related to the current work area.|  
|170|Variable '*value*' is not found.|  
|171|Cannot open file *value*.|  
|173|File '*value*' does not exist.|  
|174|'*value*' is not a memory variable.|  
|175|'*value*' is not a file variable.|  
|176|'*value*' is not an array.|  
|177|Alias '*value*' is not found.|  
|180|File was not placed in memory using the LOAD command.|  
|182|There is not enough memory to complete this operation.|  
  
## 200  
  
|||  
|-|-|  
|200|Syntax error.|  
|201|Too many names used.|  
|202|Program is too large.|  
|203|Too many memory variables.|  
|205|Nesting error.|  
|206|Recursive macro definition.|  
|209|Line is too long.|  
|210|Allowed DO nesting level exceeded.|  
|211|An IF &#124; ELSE &#124; ENDIF statement is missing.|  
|212|Structure nesting is too deep.|  
|213|There is a missing keyword in the FOR...ENDFOR or DO CASE...ENDCASE command structure.|  
|219|Command contains unrecognized phrase/keyword.|  
|221|Command is missing required clause.|  
|222|Unrecognized command verb.|  
|224|Invalid subscript reference.|  
|227|Missing expression.|  
|228|Table number is invalid.|  
|229|Too few arguments.|  
|230|Too many arguments.|  
|233|Statement is not allowed in interactive mode.|  
|234|Subscript is outside defined range.|  
|236|Suspend program before using RESUME.|  
|238|No PARAMETER statement is found.|  
|239|Must specify additional parameters.|  
|240|Not a character expression.|  
|250|Too many PROCEDURE commands are in effect.|  
|252|Compiled code for this line is too long.|  
|257|Key string is too long.|  
|291|Expression used with ASIN() is out of range.|  
|292|Cannot use 0 or negative as the argument for LOG10().|  
|293|Expression used with ACOS() is out of range.|  
|294|FOXUSER.DBF file is invalid.|  
|295|Invalid path or file name.|  
|296|Error reading the resource.|  
|297|Command is allowed only in interactive mode.|  
  
## 300  
  
|||  
|-|-|  
|301|Operator/operand type mismatch.|  
|302|Data type mismatch.|  
|305|Expression evaluated to an illegal value.|  
|307|Cannot divide by 0.|  
|308|Insufficient stack space.|  
|337|Cannot nest the PRINTJOB command.|  
  
## 400  
  
|||  
|-|-|  
|406|Printer is not ready.|  
|407|Invalid argument used with the SET function.|  
|410|Unable to create temporary work files.|  
|423|Error creating the OLE object.|  
|424|Error copying the OLE object to the Clipboard.|  
|462|*value* internal consistency error.|  
|465|SQL pass-through internal consistency error.|  
|466|Connection handle is invalid.|  
|467|Property is invalid for local cursors.|  
|468|Property is invalid for table cursors.|  
|469|Property value is out of bounds.|  
|470|Incorrect property name.|  
|471|Incorrect column format.|  
|473|Environment-level property is invalid.|  
|474|Invalid call issued while executing a SQLEXEC() sequence.|  
|479|Invalid update column name \\*value*\\.|  
|489|General fields cannot be used in the WHERE condition of an update statement. Change the WhereType property of the view.|  
|491|No update tables are specified. Use the Tables property of the cursor.|  
|492|No key columns are specified for the update table \\*value*\\. Use the KeyFieldList property of the cursor.|  
|493|SQL parameter is missing.|  
|494|View definition has been changed.|  
|495|Warning: The key defined by the KeyField property for table *value* is not unique.|  
|498|SQL SELECT statement is invalid.|  
|499|SQL parameter *value* is invalid.|  
  
## 500  
  
|||  
|-|-|  
|502|Cannot write to the record because it is in use.|  
|503|File cannot be locked.|  
|508|Error initializing OLE.|  
|520|No database is open or set as the current database.|  
|522|Connectivity internal consistency error.|  
|523|Execution was canceled by the user.|  
|525|Function is not supported on remote tables.|  
|526|Connectivity error: *value.*|  
|527|Cannot load ODBC library, ODBC32.DLL.|  
|528|ODBC entry point missing, *value*.|  
|530|Fetching canceled; remote table is closed.|  
|532|Type conversion is not supported.|  
|533|This property is read-only.|  
|536|Function is not supported on native tables.|  
|538|A stored procedure is executing.|  
|540|Session number is invalid.|  
|541|Connection *value* is busy.|  
|542|Base table fields have been changed and no longer match view fields. View field properties cannot be set.|  
|543|Type conversion required by the DataType property for field '*value*' is invalid.|  
|544|DataType property for field '*value*' is invalid.|  
|545|Table buffer for alias \\*value*\ contains uncommitted changes.|  
|546|Cannot close table during execution of table-bound expression.|  
|547|Cannot insert an empty row from a view into its base table(s).|  
|548|Table *value* has one or more non-structural indexes open. Please close them and retry the Begin Transaction.|  
|549|Data session #*value* cannot be released with open transaction(s).|  
|550|.DBC internal consistency error.|  
|557|The database must be opened exclusively.|  
|559|Property is not found.|  
|560|Property value is invalid.|  
|561|Database is invalid. Please validate.|  
|562|Cannot find object *value* in the database.|  
|563|Cannot find view *value* in the current database.|  
|566|Cannot issue the PACK command on a database while its tables are in use.|  
|567|Primary key property is invalid; please validate database.|  
|570|Database is read-only.|  
|571|The name *value* is already used for another|  
|575|Object name is invalid.|  
|577|Table *value* is referenced in a relation.|  
|578|Invalid database table name.|  
|579|Command cannot be issued on a table with cursors in table buffering mode.|  
|580|Feature is not supported for non-.DBC tables.|  
|581|Field *value* does not accept null *value*.|  
|583|Record validation rule is violated.|  
|585|Update conflict. Use TABLEUPDATE() with the lForce parameter to commit the update or TABLEREVERT() to roll back the update.|  
|586|Function requires row or table buffering mode.|  
|587|Illegal nested OLDVAL() or CURVAL().|  
|589|Table or row buffering requires that SET MULTILOCKS is set to ON.|  
|590|BEGIN TRANSACTION command failed. Nesting level is too deep.|  
|591|END TRANSACTION command cannot be issued without a corresponding BEGIN TRANSACTION command.|  
|592|ROLLBACK command cannot be issued without a corresponding BEGIN TRANSACTION command.|  
|593|Command cannot be issued within a transaction.|  
|594|Illegal to attempt a file lock in a transaction after taking prior record locks.|  
|596|Table buffering is not enabled.|  
|597|Views require either DB_BUFOPTROW or DB_BUFOPTTABLE.|  
|598|Rule and trigger code must balance transaction usage.|  
|599|Data session #*value* was forced to ROLLBACK all transactions to avoid deadlock.|  
  
## 600  
  
|||  
|-|-|  
|601|Alias name is already in use.|  
|602|Operation is invalid for a Memo, General, or Picture field.|  
|612|No such menu or menu item is defined.|  
|618|Menu has not been defined with DEFINE MENU.|  
|624|Menu title has not been defined with DEFINE PAD.|  
|625|Menu has not been defined with DEFINE POPUP.|  
|631|Array dimensions are invalid.|  
|637|File must be opened exclusively to convert the Memo file.|  
|638|Field must be a Memo field.|  
|649|No previous PRINTJOB command to correspond to this command.|  
|651|CANCEL or SUSPEND is not allowed.|  
|659|The table has memo fields that cannot be converted while open read-only.|  
|683|Index tag is not found.|  
  
## 700  
  
|||  
|-|-|  
|700|Record is in use by another user.|  
|701|File must be opened exclusively.|  
|702|File is in use by another user.|  
|703|Record is not locked.|  
|705|File access is denied.|  
|706|Cannot sort .IDX files in descending order.|  
|707|Structural .CDX file is not found.|  
|708|File is open in another work area.|  
|712|Field name is a duplicate or invalid.|  
|714|Window '*value*' has not been defined.|  
|718|File is read-only.|  
|722|Preprocessor expression is invalid.|  
|734|Property *value* is not found.|  
|737|*value* is a method, event, or object.|  
|738|Property *value* is not a method or event.|  
|740|*value* is a read-only property.|  
|748|This file is incompatible with the current version of Visual FoxPro.|  
|750|File was created in a later version of Visual FoxPro than the current version.|  
|763|Property *value* already exists.|  
|773|Database object type is invalid.|  
|784|This object is derived from a base class and does not have a parent class.|  
  
## 800  
  
|||  
|-|-|  
|802|SQL: Cannot locate table.|  
|872|Too many columns.|  
|879|No primary key.|  
|884|Uniqueness of index *value* is violated.|  
|885|Only structural tags can be defined as candidate.|  
|886|Index does not accept NULL.|  
|887|Illegal recursion in rule evaluation.|  
|888|Tag name is too long.|  
  
## 900  
  
|||  
|-|-|  
|901|Function argument value, type, or count is invalid.|  
|902|Expression evaluator failed.|  
|903|String is too long to fit.|  
|904|** or ^ domain error.|  
|905|LOG(): Zero or negative used as argument.|  
|906|SQRT() argument cannot be negative.|  
|912|Operation is invalid for a General field.|  
|914|Code page number is invalid.|  
|915|Collating sequence '*value*' is not found.|  
|918|File name is too long.|  
|922|Volume does not exist.|  
|923|Object *value* is not found.|  
|924|*value* is not an object.|  
|925|Unknown member *value*.|  
|928|Statement is only valid within a class definition.|  
|929|*value* can only be used within a method.|  
|930|Cannot redefine *value*.|  
|931|Statement is not in a procedure.|  
|934|Statement is only valid within a method.|  
|935|The current object does not inherit from class *value*.|  
|937|Procedure file '*value*' is not found.|  
|938|Object is not contained in a *value*.|  
|939|WITH/ENDWITH mismatch.|  
|940|Expression is not valid outside of WITH/ENDWITH.|  
|941|Error code is invalid.|  
|942|Objects cannot be assigned to arrays.|  
|943|Member *value* does not evaluate to an object.|  
|945|The current object has been released.|  
|947|Expression is too complex.|  
|951|Cannot clear the object in use.|  
|955|WIN.INI/registry is corrupted.|  
|957|Error accessing printer spooler.|  
|959|Invalid coordinates.|  
|960|Illegal redefinition of variable *value*.|  
|971|Cannot compile until the current COMPILE command has completed.|  
|972|Array *value* is in use.|  
|974|Arrays cannot be assigned to array elements.|  
|976|Cannot resolve backlink.|  
|988|Currency value is out of range.|  
|990|Cancel.|  
|999|Function is not implemented.|
