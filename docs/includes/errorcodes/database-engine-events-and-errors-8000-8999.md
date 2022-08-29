---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/19/2022
ms.topic: include
---
| Error| Severity | Event Logged | Description|
| :------ | :------| :------| :----------------------------- |
|    8001    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Meta-information is invalid for the Sql Variant parameter.    |
|    8002    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d ("%.*ls"): Data type 0x%02X (XML) has an invalid database or schema specified.    |
|    8003    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Too many parameters were provided in this RPC request. The maximum is %d.    |
|    8004    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. The RPC name is invalid.    |
|    8005    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d: The parameter name is invalid.    |
|    8006    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d: The parameter status flags are invalid.    |
|    8007    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d ("%.*ls"): The chunking format is incorrect for a large object parameter of type 0x%02X.    |
|    8008    |    16    |    No    |    The number of params passed to sp_execute is not equal to the number of params used when the handle was prepared (%d).    |
|    8009    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d ("%.*ls"): Data type 0x%02X is unknown.    |
|    8010    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d ("%.*ls"): The RPC is marked with the metadata unchanged flag, but data type 0x%02X is different from the one sent last time.    |
|    8011    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d ("%.*ls"): Data type 0x%02X (sql_variant) has an invalid length for type-specific metadata.    |
|    8012    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d ("%.*ls"): Data type 0x%02X (sql_variant) has an invalid precision or scale for type-specific metadata.    |
|    8013    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d ("%.*ls"): Data type 0x%02X (sql_variant) has an invalid instance length.    |
|    8014    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d ("%.*ls"): Data type 0x%02X (sql_variant) has an invalid type for type-specific metadata.    |
|    8015    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d ("%.*ls"): Data type 0x%02X is an untyped NULL but is marked as an output parameter.    |
|    8016    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d ("%.*ls"): Data type 0x%02X has an invalid data length or metadata length.    |
|    8017    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d ("%.*ls"): Data type 0x%02X has an invalid precision or scale.    |
|    8018    |    16    |    No    |    Invalid parameter %d ('%.*ls'): Data type 0x%02X is a deprecated large object, or LOB, but is marked as output parameter. Deprecated types are not supported as output parameters. Use current large object types instead.    |
|    8019    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d ("%.*ls"): Data type "0x%02X" (CLR type) has an invalid user type specified.    |
|    8020    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d ("%.*ls"): Data type "0x%02X" (CLR type) has an invalid length for serialization metadata.    |
|    8021    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d ("%.*ls"): Data type "0x%02X" (CLR type) has an invalid database specified.    |
|    8022    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d ("%.*ls"): The supplied value is NULL and data type %.*ls cannot be NULL. Check the source data for invalid values.    |
|    8023    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d ("%.*ls"): The supplied value is not a valid instance of data type %.*ls. Check the source data for invalid values. An example of an invalid value is data of numeric type with scale greater than precision.    |
|    8024    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d ("%.*ls"): Data type 0x%02X (sql_variant) has an invalid collation for type-specific metadata.    |
|    8025    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d ("%.*ls"): The RPC is marked with the metadata unchanged flag, but data type 0x%02X has a maximum length different from the one sent last time.    |
|    8026    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d ("%.*ls"): The RPC is marked with the metadata unchanged flag, but data type 0x%02X has an actual length different from the one sent last time.    |
|    8027    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d ("%.*ls"): Data type "0x%02X" (CLR type) has an invalid schema specified.    |
|    8028    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Parameter %d ("%.*ls"): The supplied length is not valid for data type %.*ls. Check the source data for invalid lengths. An example of an invalid length is data of nchar type with an odd length in bytes.    |
|    8029    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (user-defined table type) unexpected token encountered processing a table-valued parameter.    |
|    8030    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (XML) has an invalid database or schema specified.    |
|    8031    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: The chunking format is incorrect for a large object parameter of data type 0x%02X.    |
|    8032    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X is unknown.    |
|    8033    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (sql_variant) has an invalid length for type-specific metadata.    |
|    8034    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (sql_variant) has an invalid precision or scale for type-specific metadata.    |
|    8035    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (sql_variant) has an invalid instance length.    |
|    8036    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (sql_variant) has an invalid type for type-specific metadata.    |
|    8037    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X has an invalid data length or metadata length.    |
|    8038    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X has an invalid precision or scale.    |
|    8039    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (CLR type) has an invalid user type specified.    |
|    8040    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (CLR type) has an invalid length for serialization metadata.    |
|    8041    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (CLR type) has an invalid database specified.    |
|    8042    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: The supplied value is NULL and data type %.*ls cannot be NULL. Check the source data for invalid values.    |
|    8043    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: The supplied value is not a valid instance of data type %.*ls. Check the source data for invalid values. An example of an invalid value is data of numeric type with scale greater than precision.    |
|    8044    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (sql_variant) has an invalid collation for type-specific metadata.    |
|    8045    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (CLR type) has an invalid schema specified.    |
|    8046    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: The supplied length is not valid for data type %.*ls. Check the source data for invalid lengths. An example of an invalid length is data of nchar type with an odd length in bytes.    |
|    8047    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (user-defined table type) has a non-zero length database name specified. Database name is not allowed with a table-valued parameter, only schema name and type name are valid.    |
|    8048    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (user-defined table type) has an invalid schema specified.    |
|    8049    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (user-defined table type) has an invalid type name specified.    |
|    8050    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (user-defined table type) has an invalid column count specified.    |
|    8051    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (user-defined table type) has an invalid column name specified.    |
|    8052    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (user-defined table type) timestamp column is required to be default.    |
|    8053    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (user-defined table type) has an invalid column flag specified.    |
|    8054    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (user-defined table type) has invalid ordering and uniqueness metadata specified.    |
|    8055    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (user-defined table type) has invalid column ordering metadata specified.    |
|    8056    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (user-defined table type) has too many optional metadata tokens specified.    |
|    8057    |    16    |    No    |    Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (user-defined table type). The specified column is computed or default and has ordering or uniqueness set. Ordering and uniqueness can only be set on columns that have client supplied data.    |
|    8058    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d, to a parameterized string has no table type defined.    |
|    8059    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter "%.*ls", to a parameterized string has no table type defined.    |
|    8060    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call (RPC) protocol stream is incorrect. Table-valued parameter %d ("%.*ls"), row %I64d, column %d: Data type 0x%02X (user-defined table type) is null and not set to default. A null table-valued parameter is required to be sent as a default parameter.    |
|    8061    |    16    |    No    |    The data for table-valued parameter "%.*ls" doesn't conform to the table type of the parameter.    |
|    8062    |    16    |    No    |    The data for the table-valued parameter %d doesn't conform to the table type of the parameter.    |
|    8063    |    16    |    No    |    The incoming tabular data stream (TDS) remote procedure call stream is sending an unlimited length CLR type. Parameter %d ("%.*ls") is defined as type %.*ls. This type is not supported by down-level clients. Send the serialized data of the large CLR type as varbinary(max), or upgrade the client driver to one that supports unlimited CLR types.    |
|    8064    |    16    |    No    |    Parameter %d ([%.*ls].[%.*ls].[%.*ls]): The CLR type does not exist or you do not have permissions to access it.    |
|    8101    |    16    |    No    |    An explicit value for the identity column in table '%.*ls' can only be specified when a column list is used and IDENTITY_INSERT is ON.    |
|    8102    |    16    |    No    |    Cannot update identity column '%.*ls'.    |
|    8105    |    16    |    No    |    '%.*ls' is not a user table. Cannot perform SET operation.    |
|    8106    |    16    |    No    |    Table '%.*ls' does not have the identity property. Cannot perform SET operation.    |
|    8107    |    16    |    No    |    IDENTITY_INSERT is already ON for table '%.*ls.%.*ls.%.*ls'. Cannot perform SET operation for table '%.*ls'.    |
|    8108    |    16    |    No    |    Cannot add identity column, using the SELECT INTO statement, to table '%.*ls', which already has column '%.*ls' that inherits the identity property.    |
|    8109    |    16    |    No    |    Attempting to add multiple identity columns to table '%.*ls' using the SELECT INTO statement.    |
|    8110    |    16    |    No    |    Cannot add multiple PRIMARY KEY constraints to table '%.*ls'.    |
|    8111    |    16    |    No    |    Cannot define PRIMARY KEY constraint on nullable column in table '%.*ls'.    |
|    8112    |    16    |    No    |    Cannot add more than one clustered index for constraints on table '%.*ls'.    |
|    8113    |    16    |    No    |    Incorrect use of the XML data type method '%.*ls'. A mutator method is expected in this context.    |
|    8114    |    16    |    No    |    Error converting data type %ls to %ls.    |
|    8115    |    16    |    No    |    Arithmetic overflow error converting %ls to data type %ls.    |
|    8116    |    16    |    No    |    Argument data type %ls is invalid for argument %d of %ls function.    |
|    8117    |    16    |    No    |    Operand data type %ls is invalid for %ls operator.    |
|    8118    |    16    |    No    |    Column '%.*ls.%.*ls' is invalid in the select list because it is not contained in an aggregate function and there is no GROUP BY clause.    |
|    8119    |    16    |    No    |    Column '%.*ls.%.*ls' is invalid in the HAVING clause because it is not contained in an aggregate function and there is no GROUP BY clause.    |
|    8120    |    16    |    No    |    Column '%.*ls.%.*ls' is invalid in the select list because it is not contained in either an aggregate function or the GROUP BY clause.    |
|    8121    |    16    |    No    |    Column '%.*ls.%.*ls' is invalid in the HAVING clause because it is not contained in either an aggregate function or the GROUP BY clause.    |
|    8123    |    16    |    No    |    A correlated expression is invalid because it is not in a GROUP BY clause.    |
|    8124    |    16    |    No    |    Multiple columns are specified in an aggregated expression containing an outer reference. If an expression being aggregated contains an outer reference, then that outer reference must be the only column referenced in the expression.    |
|    8125    |    16    |    No    |    An aggregated expression containing an outer reference must be contained in either the select list, or a HAVING clause subquery in the query whose FROM clause contains the table with the column being aggregated.    |
|    8126    |    16    |    No    |    Column "%.*ls.%.*ls" is invalid in the ORDER BY clause because it is not contained in an aggregate function and there is no GROUP BY clause.    |
|    8127    |    16    |    No    |    Column "%.*ls.%.*ls" is invalid in the ORDER BY clause because it is not contained in either an aggregate function or the GROUP BY clause.    |
|    8128    |    10    |    Yes    |    Using '%s' version '%s' to execute extended stored procedure '%s'. This is an informational message only; no user action is required.    |
|    8129    |    16    |    No    |    The new disk size must be greater than %d. Consider using DBCC SHRINKDB.    |
|    8131    |    10    |    Yes    |    Extended stored procedure DLL '%s' does not export __GetXpVersion(). Refer to the topic "Backward Compatibility Details (Level 1) - Open Data Services" in the documentation for more information.    |
|    8132    |    10    |    Yes    |    Extended stored procedure DLL '%s' reports its version is %d.%d. The expected version is %d.%d.    |
|    8133    |    16    |    No    |    None of the result expressions in a CASE specification can be NULL.    |
|    8134    |    16    |    No    |    Divide by zero error encountered.    |
|    8135    |    16    |    No    |    Table level constraint does not specify column list, table '%.*ls'.    |
|    8136    |    16    |    No    |    Duplicate columns specified in %ls constraint key list, table '%.*ls'.    |
|    8137    |    16    |    No    |    Incorrect use of the XML data type method '%.*ls'. A non-mutator method is expected in this context.    |
|    8138    |    16    |    No    |    More than 16 columns specified in foreign key column list, table '%.*ls'.    |
|    8139    |    16    |    No    |    Number of referencing columns in foreign key differs from number of referenced columns, table '%.*ls'.    |
|    8140    |    16    |    No    |    More than one key specified in column level %ls constraint, table '%.*ls'.    |
|    8141    |    16    |    No    |    Column %ls constraint for column '%.*ls' references another column, table '%.*ls'.    |
|    8143    |    16    |    No    |    Parameter '%.*ls' was supplied multiple times.    |
|    8144    |    16    |    No    |    Procedure or function %.*ls has too many arguments specified.    |
|    8145    |    16    |    No    |    %.*ls is not a parameter for procedure %.*ls.    |
|    8146    |    16    |    No    |    Procedure %.*ls has no parameters and arguments were supplied.    |
|    8147    |    16    |    No    |    Could not create IDENTITY attribute on nullable column '%.*ls', table '%.*ls'.    |
|    8148    |    16    |    No    |    More than one column %ls constraint specified for column '%.*ls', table '%.*ls'.    |
|    8149    |    16    |    No    |    OLE Automation objects are not supported in fiber mode.    |
|    8150    |    16    |    No    |    Multiple NULL constraints were specified for column '%.*ls', table '%.*ls'.    |
|    8151    |    16    |    No    |    Both a PRIMARY KEY and UNIQUE constraint have been defined for column '%.*ls', table '%.*ls'. Only one is allowed.    |
|    8152    |    16    |    No    |    String or binary data would be truncated.    |
|    8153    |    10    |    No    |    Warning: Null value is eliminated by an aggregate or other SET operation.    |
|    8154    |    15    |    No    |    The table '%.*ls' is ambiguous.    |
|    8155    |    15    |    No    |    No column name was specified for column %d of '%.*ls'.    |
|    8156    |    15    |    No    |    The column '%.*ls' was specified multiple times for '%.*ls'.    |
|    8158    |    15    |    No    |    '%.*ls' has more columns than were specified in the column list.    |
|    8159    |    15    |    No    |    '%.*ls' has fewer columns than were specified in the column list.    |
|    8160    |    15    |    No    |    A GROUPING or GROUPING_ID function can only be specified when there is a GROUP BY clause.    |
|    8161    |    15    |    No    |    Argument %d of the %.*ls function does not match any of the expressions in the GROUP BY clause.    |
|    8162    |    16    |    No    |    The formal parameter "%.*ls" was not declared as an OUTPUT parameter, but the actual parameter passed in requested output.    |
|    8164    |    16    |    No    |    An INSERT EXEC statement cannot be nested.    |
|    8165    |    16    |    No    |    Invalid subcommand value %d. Legal range from %d to %d.    |
|    8166    |    16    |    No    |    Constraint name '%.*ls' not permitted. Constraint names cannot begin with a number sign (#).    |
|    8167    |    16    |    No    |    The type of column "%.*ls" conflicts with the type of other columns specified in the UNPIVOT list.    |
|    8168    |    16    |    No    |    Cannot create, drop, enable, or disable more than one constraint, column, or trigger named '%.*ls' in this context. Duplicate names are not allowed.    |
|    8169    |    16    |    No    |    Conversion failed when converting from a character string to uniqueidentifier.    |
|    8170    |    16    |    No    |    Insufficient result space to convert uniqueidentifier value to char.    |
|    8171    |    16    |    No    |    Hint '%ls' on object '%.*ls' is invalid.    |
|    8172    |    16    |    No    |    The argument %d of the XML data type method "%.*ls" must be a string literal.    |
|    8173    |    15    |    No    |    Incorrect syntax was used to invoke the XML data type method '%.*ls'.    |
|    8174    |    16    |    No    |    Schema lock with handle %d not found.    |
|    8175    |    10    |    No    |    Could not find table %.*ls. Will try to resolve this table name later.    |
|    8176    |    16    |    No    |    Resync procedure expects value of key '%.*ls', which was not supplied.    |
|    8177    |    16    |    No    |    Cannot use a column in the %hs clause unless it is contained in either an aggregate function or the GROUP BY clause.    |
|    8178    |    16    |    No    |    The parameterized query '%.*ls' expects the parameter '%.*ls', which was not supplied.    |
|    8179    |    16    |    No    |    Could not find prepared statement with handle %d.    |
|    8180    |    16    |    No    |    Statement(s) could not be prepared.    |
|    8181    |    16    |    No    |    Text for '%.*ls' is missing from the system catalog. The object must be dropped and re-created before it can be used.    |
|    8183    |    16    |    No    |    Only UNIQUE or PRIMARY KEY constraints can be created on computed columns, while CHECK, FOREIGN KEY, and NOT NULL constraints require that computed columns be persisted.    |
|    8184    |    16    |    No    |    Error in binarychecksum. There are no comparable columns in the binarychecksum input.    |
|    8185    |    16    |    No    |    Error expanding "*": An incomparable column has been found in an underlying table or view.    |
|    8186    |    16    |    No    |    Function '%.*ls' can be used only on user and system tables.    |
|    8187    |    16    |    Yes    |    The prepared handle %d is currently being used by another command (error state: %d).    |
|    8188    |    16    |    No    |    There is already a SQL type for assembly type "%.*ls" on assembly "%.*ls". Only one SQL type can be mapped to a given assembly type. CREATE TYPE fails.    |
|    8189    |    16    |    No    |    You do not have permission to run '%ls'.    |
|    8190    |    16    |    No    |    Cannot compile replication filter procedure without defining table being filtered.    |
|    8191    |    16    |    No    |    Replication filter procedures can only contain SELECT, GOTO, IF, WHILE, RETURN, and DECLARE statements.    |
|    8192    |    16    |    No    |    Replication filter procedures cannot have parameters.    |
|    8193    |    16    |    No    |    Cannot execute a procedure marked FOR REPLICATION.    |
|    8195    |    16    |    No    |    Cannot create "%.*ls" on "%.*ls". Insert, Update, and Delete triggers can only be created on user tables and views.    |
|    8196    |    16    |    No    |    Duplicate column specified as ROWGUIDCOL.    |
|    8197    |    16    |    No    |    The object '%.*ls' does not exist or is invalid for this operation.    |
|    8199    |    16    |    No    |    In EXECUTE \<procname\>, procname can only be a literal or variable of type char, varchar, nchar, or nvarchar.    |
|    8301    |    10    |    No    |    Use of level0type with value 'USER' in procedure sp_addextendedproperty, sp_updateextendedproperty and sp_dropextendedproperty and in table-valued function fn_listextendedproperty has been deprecated and will be removed in a future version of SQL Server. Users are now schema scoped and hence use level0type with value 'SCHEMA' and level1type with value 'USER' for extended properties on USER.    |
|    8302    |    10    |    No    |    CREATE RULE and DROP RULE will be removed in a future version of SQL Server. Avoid using CREATE RULE and DROP RULE in new development work, and plan to modify applications that currently use them. Use check constraints instead, which are created using the CHECK keyword of CREATE TABLE or ALTER TABLE.    |
|    8303    |    10    |    No    |    CREATE DEFAULT and DROP DEFAULT will be removed in a future version of SQL Server. Avoid using CREATE DEFAULT and DROP DEFAULT in new development work, and plan to modify applications that currently use them. Instead, use default definitions created using the DEFAULT keyword of ALTER TABLE or CREATE TABLE.    |
|    8304    |    10    |    No    |    INDEXKEY_PROPERTY will be removed in a future version of SQL Server. Avoid using this feature in new development work, and plan to modify applications that currently use the feature. Use sys.index_columns instead.    |
|    8305    |    10    |    No    |    The TEXT IN ROW feature will be removed in a future version of SQL Server. Avoid using sp_tableoption for TEXT IN ROW option in new development work, and plan to modify applications that currently use the text in row option. The preferred method of storing large data is through use of the varchar(max), nvarchar(max) and varbinary(max) data types.    |
|    8306    |    10    |    No    |    Use of level0type with value 'TYPE' in procedure sp_addextendedproperty, sp_updateextendedproperty and sp_dropextendedproperty and in table-valued function fn_listextendedproperty has been deprecated and will be removed in a future version of SQL Server. Types are now schema scoped and hence use level0type with value 'SCHEMA' and level1type with value 'TYPE' for extended properties on TYPE.    |
|    8307    |    10    |    No    |    FILE_ID will be removed in a future version of SQL Server. Avoid using this feature in new development work, and plan to modify applications that currently use the feature. Use FILE_IDEX instead.    |
|    8308    |    10    |    No    |    USER_ID will be removed from a future version of SQL Server. Avoid using this feature in new development work, and plan to modify applications that currently use the feature. Use DATABASE_PRINCIPAL_ID instead.    |
|    8310    |    16    |    No    |    Cannot create (or open) named file mapping object '%ls'. SQL Server performance counters are disabled.    |
|    8311    |    16    |    No    |    Unable to map view of file mapping object '%ls' into SQL Server process address space. SQL Server performance counters are disabled.    |
|    8312    |    16    |    No    |    Cannot create (or open) named mutex '%ls'. SQL Server performance counters are disabled.    |
|    8313    |    16    |    No    |    Error in mapping SQL Server performance object/counter indexes to object/counter names. SQL Server performance counters are disabled.    |
|    8314    |    16    |    No    |    SQL Server performance object '%ls' not found in registry. SQL Server performance counters are disabled.    |
|    8315    |    16    |    No    |    SQL Server performance counter '%ls' not found in registry. SQL Server performance counters are disabled.    |
|    8316    |    16    |    No    |    Cannot open registry key 'HKLM\%ls'. SQL Server performance counters are disabled.    |
|    8317    |    16    |    No    |    Cannot query value '%ls' associated with registry key 'HKLM\%ls'. SQL Server performance counters are disabled.    |
|    8318    |    16    |    No    |    There was a virtual memory allocation failure during performance counters initialization. SQL Server performance counters are disabled.    |
|    8319    |    16    |    No    |    Windows kernel object '%ls' already exists. It's not owned by the SQL Server service account. SQL Server performance counters are disabled.    |
|    8320    |    10    |    No    |    \@\@REMSERVER will be removed in a future version of SQL Server. Avoid using this feature in new development work, and plan to modify applications that currently use the feature. Use linked servers and linked server stored procedures instead.    |
|    8350    |    10    |    No    |    Use of NOLOCK or READUNCOMMITTED hints in the FROM clause of an UPDATE or DELETE statement on the target table of the statement ('%.*ls') is deprecated. These hints have no effect in this location. Microsoft recommends that you remove these hints from this statement. Support for these hints in this location will be removed in a future version of SQL Server.    |
|    8351    |    16    |    Yes    |    A trace control request could not be processed because invalid parameters were specified when events were registered. Confirm that the parameters are within valid ranges.    |
|    8352    |    16    |    Yes    |    Cannot find the requested trace template: id = %ls.    |
|    8353    |    16    |    Yes    |    Event Tracing for Windows failed to start. %ls. To enable Event Tracing for Windows, restart SQL Server.    |
|    8354    |    16    |    Yes    |    Event Tracing for Windows failed to send an event. Send failures with the same error code may not be reported in the future. Error ID: %d, Event class ID: %d, Cause: %ls.    |
|    8355    |    16    |    Yes    |    Server-level event notifications can not be delivered. Either Service Broker is disabled in msdb, or msdsb failed to start. Event notifications in other databases could be affected as well. Bring msdb online, or enable Service Broker.    |
|    8356    |    16    |    Yes    |    Event Tracing for Windows (ETW) failed to send event. The server has run out of memory. The same send failure may not be reported in the future.    |
|    8357    |    16    |    Yes    |    Event Tracing for Windows (ETW) failed to send event. This may be due to low resource conditions. The same send failure may not be reported in the future.    |
|    8358    |    16    |    Yes    |    Event Tracing for Windows (ETW) failed to send event. Event message size exceeds limit. The same send failure may not be reported in the future.    |
|    8359    |    16    |    Yes    |    SQL Trace failed to send event notification. The server has run out of memory. The same send failure may not be reported in the future.    |
|    8360    |    16    |    Yes    |    SQL Trace failed to send event notification. This may be due to low resource conditions. The same send failure may not be reported in the future.    |
|    8379    |    10    |    No    |    Old style RAISERROR (Format: RAISERROR integer string) will be removed in the next version of SQL Server. Avoid using this in new development work, and plan to modify applications that currently use it to use new style RAISERROR.    |
|    8380    |    10    |    No    |    SQLOLEDB is no longer a supported provider. Please use SQL Native Client (SQLNCLI) to connect to SQL Server using linked server '%.*ls'.    |
|    8381    |    10    |    No    |    SQLOLEDB is no longer a supported provider. Please use SQL Native Client (SQLNCLI) for ad hoc connection to SQL Server.    |
|    8382    |    10    |    No    |    Specifying table hints without using a WITH keyword is a deprecated feature and will be removed in a future version.    |
|    8383    |    10    |    No    |    Specifying HOLDLOCK as a table hint without parentheses is a deprecated feature and will be removed in the next version of SQL Server.    |
|    8384    |    10    |    No    |    Use of space as a separator for table hints is a deprecated feature and will be removed in a future version. Use a comma to separate individual table hints.    |
|    8385    |    10    |    No    |    The select list of an aggregate indexed view must contain count_big(*) in 90 compatibility mode and higher.    |
|    8386    |    10    |    No    |    Use of hint "%.*ls" on the target table of INSERT is deprecated because it may be removed in a future version of SQL Server. Modify the INSERT statement to remove the use of this hint.    |
|    8387    |    10    |    No    |    The indirect application of table hints to an invocation of a multi-statement table-valued function (TVF) through a view will be removed in a future version of SQL Server. Remove hints on references to view "%.*ls" because it references a multi-statement TVF.    |
|    8388    |    10    |    No    |    The ability to return results from triggers will be removed in a future version of SQL Server. Avoid using this feature in new development work, and plan to modify applications that currently use it.    |
|    8389    |    10    |    No    |    The ALL permission will be removed in a future version of SQL Server. Avoid using this permission in new development work and plan to modify applications that currently use it.    |
|    8390    |    10    |    No    |    The '::' function calling syntax will be removed in a future version of SQL Server. Replace it with "sys.".    |
|    8391    |    10    |    No    |    The usage of 2-part names in DROP INDEX is deprecated. New-style syntax DROP INDEX <1p-name> ON {<3p-table-name> \| <3p-view-name> }    |
|    8393    |    10    |    No    |    The ability to not specify a column name when the datatype is timestamp will be removed in a future version of SQL Server. Avoid using this feature in new development work, and plan to modify applications that currently use it.    |
|    8394    |    10    |    No    |    Usage of deprecated index option syntax. The deprecated relational index option syntax structure will be removed in a future version of SQL Server. Avoid using this syntax structure in new development work, and plan to modify applications that currently use the feature.    |
|    8396    |    10    |    No    |    %ls will be removed in a future version of SQL Server. Avoid using this feature in new development work, and plan to modify applications that currently use it. Use %ls instead.    |
|    8397    |    10    |    No    |    The TEXT, NTEXT, and IMAGE data types will be removed in a future version of SQL Server. Avoid using them in new development work, and plan to modify applications that currently use them. Use the varchar(max), nvarchar(max), and varbinary(max) data types instead.    |
|    8398    |    10    |    No    |    The use of more than two-part column names will be removed in a future version of SQL Server. Avoid using this feature in new development work, and plan to modify applications that currently use it.    |
|    8399    |    10    |    No    |    %ls will be removed in a future version of SQL Server. Avoid using this feature in new development work, and plan to modify applications that currently use it.    |
|    8401    |    16    |    No    |    This message could not be delivered because the target user with ID %i in database ID %i does not have permission to receive from the queue '%.*ls'.    |
|    8402    |    16    |    No    |    The data type of the '%S_MSG' in the '%S_MSG' statement must be %s. The %s data type is not allowed.    |
|    8403    |    16    |    No    |    The message type '%.*ls' is specified more than once. Remove the duplicate message type.    |
|    8404    |    16    |    No    |    The service contract '%.*ls' is specified more than once. Remove the duplicate service contract.    |
|    8405    |    16    |    No    |    An error occurred in the service broker queue rollback handler, while trying to disable a queue. Database ID: %d, Queue ID: %d, Error: %i, State: %i.    |
|    8406    |    16    |    No    |    The dialog lifetime can not be NULL. Specify a valid dialog lifetime value from %d to %d.    |
|    8407    |    16    |    No    |    Received a message that contains invalid header fields. This may indicate a network problem or that another application is connected to the Service Broker endpoint.    |
|    8408    |    16    |    No    |    Target service '%.*ls' does not support contract '%.*ls'.    |
|    8409    |    16    |    No    |    This message could not be delivered because the targeted service does not support the service contract. Targeted service: '%.*ls', service contract: '%.*ls'.    |
|    8410    |    16    |    No    |    The conversation timer cannot be set beyond the conversation's lifetime.    |
|    8411    |    16    |    No    |    The dialog lifetime value of %d is outside the allowable range of %d to %d. Specify a valid dialog lifetime value.    |
|    8412    |    16    |    No    |    The syntax of the service name '%.*ls' is invalid.    |
|    8413    |    16    |    No    |    The syntax of the broker instance '%.*ls' is invalid. The specified broker instance is too long, the maximum size is 256 bytes.    |
|    8414    |    16    |    No    |    The conversation group ID '%.*ls' is invalid in this context. Specify a different conversion group ID.    |
|    8415    |    16    |    No    |    The activated task was aborted because the invoked stored procedure '%ls' did not issue COMMIT or ROLLBACK on one or more transactions that it begun.    |
|    8417    |    16    |    No    |    The service contract name is NULL. Specify a service contract name.    |
|    8418    |    16    |    No    |    The conversation handle is missing. Specify a conversation handle.    |
|    8419    |    16    |    No    |    Both the error code and the description must be provided for END CONVERSATION WITH ERROR. Neither value can be NULL.    |
|    8420    |    16    |    No    |    The conversation group is missing. Specify a conversation group.    |
|    8421    |    16    |    No    |    The service name is missing. Specify a service name.    |
|    8422    |    16    |    No    |    The error description is missing. Specify a description of the error.    |
|    8423    |    16    |    No    |    The service "%.*ls" is not found.    |
|    8424    |    16    |    No    |    The error code and error description are missing. Specify both the error code and description of the error.    |
|    8425    |    16    |    No    |    The service contract '%.*ls' is not found.    |
|    8426    |    16    |    No    |    The conversation handle "%.*ls" is not found.    |
|    8427    |    16    |    No    |    The conversation endpoint is not in a valid state for END CONVERSATION. The current endpoint state is '%ls'.    |
|    8428    |    16    |    No    |    The message type "%.*ls" is not found.    |
|    8429    |    16    |    No    |    The conversation endpoint is not in a valid state for SEND. The current endpoint state is '%ls'.    |
|    8430    |    16    |    No    |    The message body failed the configured validation.    |
|    8431    |    16    |    No    |    The message type '%.*ls' is not part of the service contract.    |
|    8432    |    16    |    No    |    The message cannot be sent because the message type '%.*ls' is marked SENT BY TARGET in the contract, however this service is an Initiator.    |
|    8433    |    16    |    No    |    The message body may not be NULL. A zero-length UNICODE or binary string is allowed.    |
|    8434    |    16    |    No    |    The message cannot be sent because the message type '%.*ls' is marked SENT BY INTITIATOR in the contract, however this service is a Target.    |
|    8436    |    16    |    No    |    The conversation group "%.*ls" is not found.    |
|    8437    |    16    |    No    |    The message received was sent by a Target service, but the message type '%.*ls' is marked SENT BY INITIATOR in the contract.    |
|    8438    |    16    |    No    |    The conversation endpoint is not in a valid state for MOVE CONVERSATION. The current endpoint state is '%ls'.    |
|    8439    |    16    |    No    |    The destination conversation group '%.*ls' is invalid.    |
|    8440    |    23    |    Yes    |    The conversation group exists, but no queue exists. Possible database corruption. Run DBCC CHECKDB.    |
|    8442    |    16    |    No    |    There is no Service Broker active in the database. Change to a database context that contains a Service Broker.    |
|    [8443](../../relational-databases/errors-events/mssqlserver-8443-database-engine-error.md)    |    23    |    Yes    |    The conversation with ID '%.*ls' and initiator: %d references a missing conversation group '%.*ls'. Run DBCC CHECKDB to analyze and repair the database.    |
|    8444    |    23    |    Yes    |    The service queue structure is inconsistent. Possible database corruption. Run DBCC CHECKDB.    |
|    8445    |    16    |    No    |    The conversation handle '%ls' is referencing an invalid conversation ID '%ls', initiator: %d.    |
|    8447    |    16    |    No    |    A RECEIVE statement that assigns a value to a variable must not be combined with data retrieval operations.    |
|    8450    |    16    |    No    |    Assignments in the RECEIVE projection are not allowed in conjunction with the INTO clause.    |
|    8457    |    16    |    No    |    The message received was sent by the initiator of the conversation, but the message type '%.*ls' is marked SENT BY TARGET in the contract.    |
|    8458    |    16    |    No    |    The conversation endpoint is not in a valid state for BEGIN CONVERSATION TIMER. The current endpoint state is '%ls'.    |
|    8459    |    16    |    No    |    The message size, including header information, exceeds the maximum allowed of %d.    |
|    8460    |    16    |    No    |    The conversation endpoint with ID '%ls' and is_initiator: %d is referencing the invalid conversation handle '%ls'.    |
|    8461    |    23    |    Yes    |    An internal service broker error detected. Possible database corruption. Run DBCC CHECKDB.    |
|    8462    |    16    |    No    |    The remote conversation endpoint is either in a state where no more messages can be exchanged, or it has been dropped.    |
|    8463    |    16    |    No    |    Failed to read the message body while marshaling a message. This message is a symptom of another problem. Check the SQL Server error log and the Windows event log for additional messages and address the underlying problem. If the problem persists, the database may be damaged. To recover the database, restore the database from a clean backup. If no clean backup is available, consider running DBCC CHECKDB. Note that DBCC CHECKDB may remove data to repair the database.    |
|    8468    |    16    |    No    |    Underlying service has been altered.    |
|    8469    |    16    |    No    |    Remote service has been altered.    |
|    8470    |    16    |    No    |    Remote service has been dropped.    |
|    8471    |    16    |    No    |    An SNI call failed during a Service Broker/Database Mirroring transport operation. SNI error '%ls'.    |
|    8472    |    16    |    No    |    The remote service has sent a message that contains invalid header fields.    |
|    8475    |    16    |    No    |    The conversation endpoint with ID '%ls' and is_initiator: %d has been dropped.    |
|    8477    |    16    |    No    |    An internal Service Broker error occurred (error = 0x%08x). This error indicates a serious problem with SQL Server. Check the SQL Server error log and the Windows event logs for information pointing to possible hardware problems. The database may have been damaged. To recover the database, restore the database from a clean backup. If no clean backup is available, consider running DBCC CHECKDB. Note that DBCC CHECKDB may remove data to repair the database.    |
|    8479    |    16    |    No    |    Used by test in failpoint simulation.    |
|    8487    |    16    |    No    |    The remote service contract has been dropped.    |
|    8489    |    16    |    No    |    The dialog has exceeded the specified LIFETIME.    |
|    8490    |    16    |    No    |    Cannot find the remote service '%.*ls' because it does not exist.    |
|    8492    |    16    |    No    |    The service contract '%.*ls' must have at least one message SENT BY INITIATOR or ANY.    |
|    8493    |    16    |    No    |    The alter of service '%.*ls' must change the queue or at least one contract.    |
|    8494    |    16    |    No    |    You do not have permission to access the service '%.*ls'.    |
|    8495    |    16    |    No    |    The conversation has already been acknowledged by another instance of this service.    |
|    8498    |    16    |    No    |    The remote service has sent a message of type '%.*ls' that is not part of the local contract.    |
|    8499    |    16    |    No    |    The remote service has sent a message body of type '%.*ls' that does not match the message body encoding format. This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    8501    |    16    |    No    |    MSDTC on server '%.*ls' is unavailable.    |
|    8502    |    20    |    Yes    |    Unknown token '0x%x' received from Microsoft Distributed Transaction Coordinator (MS DTC) .    |
|    8504    |    20    |    Yes    |    The import buffer for this transaction is not valid.    |
|    8506    |    20    |    Yes    |    Cannot change transaction state from %hs to %hs. The change requested is not valid.    |
|    8508    |    10    |    No    |    QueryInterface failed for "%ls": %ls.    |
|    8509    |    20    |    Yes    |    Import of Microsoft Distributed Transaction Coordinator (MS DTC) transaction failed: %ls.    |
|    8510    |    20    |    Yes    |    Enlist operation failed: %ls. SQL Server could not register with Microsoft Distributed Transaction Coordinator (MS DTC) as a resource manager for this transaction. The transaction may have been stopped by the client or the resource manager.    |
|    8511    |    16    |    Yes    |    Unknown isolation level 0x%x requested from Microsoft Distributed Transaction Coordinator (MS DTC).    |
|    8512    |    20    |    Yes    |    Microsoft Distributed Transaction Coordinator (MS DTC) commit transaction acknowledgement failed: %hs.    |
|    8513    |    20    |    Yes    |    Microsoft Distributed Transaction Coordinator (MS DTC) end transaction acknowledgement failed: %hs.    |
|    8514    |    20    |    Yes    |    Microsoft Distributed Transaction Coordinator (MS DTC) PREPARE acknowledgement failed: %hs.    |
|    8515    |    20    |    Yes    |    Microsoft Distributed Transaction Coordinator (MS DTC) global state is not valid.    |
|    8517    |    20    |    Yes    |    Failed to get Microsoft Distributed Transaction Coordinator (MS DTC) PREPARE information: %ls.    |
|    8518    |    16    |    Yes    |    Microsoft Distributed Transaction Coordinator (MS DTC) BEGIN TRANSACTION failed: %ls.    |
|    8519    |    16    |    No    |    Current Microsoft Distributed Transaction Coordinator (MS DTC) transaction must be committed by remote client.    |
|    8520    |    16    |    Yes    |    Internal Microsoft Distributed Transaction Coordinator (MS DTC) transaction failed to commit: %ls.    |
|    8521    |    20    |    Yes    |    This awakening state is not valid: slept in %hs; awoke in %hs.    |
|    8522    |    20    |    Yes    |    Microsoft Distributed Transaction Coordinator (MS DTC) has stopped this transaction.    |
|    8523    |    15    |    No    |    PREPARE TRAN statement not allowed on MSDTC transaction.    |
|    8524    |    16    |    No    |    The current transaction could not be exported to the remote provider. It has been rolled back.    |
|    [8525](../../relational-databases/errors-events/mssqlserver-8525-database-engine-error.md)    |    16    |    No    |    Distributed transaction completed. Either enlist this session in a new transaction or the NULL transaction.    |
|    8526    |    16    |    No    |    Cannot go remote while the session is enlisted in a distributed transaction that has an active savepoint.    |
|    8527    |    16    |    Yes    |    An attempt to create a distributed transaction export token failed with this error: %ls. Contact your Microsoft Distributed Transaction Coordinator (MS DTC) system administrator.    |
|    8528    |    16    |    No    |    The commit of the Kernel Transaction Manager (KTM) transaction failed: %d.    |
|    8529    |    16    |    No    |    Unable to extract the Kernel Transaction Manager (KTM) transaction handle from the Microsoft Distributed Transaction Coordinator (MS DTC) transaction: 0x%x.    |
|    8530    |    16    |    Yes    |    The Windows kernel transaction manager creation failed: 0x%x.    |
|    8531    |    16    |    Yes    |    The Windows kernel transaction manager failed to create the enlistment: 0x%08x.    |
|    8532    |    20    |    No    |    Error while reading resource manager notification from Kernel Transaction Manager (KTM): %d.    |
|    8533    |    20    |    No    |    Error while waiting for communication from Kernel Transaction Manager (KTM): %d.    |
|    8534    |    21    |    No    |    The KTM RM for this database, %ls, failed to start: %d.    |
|    8535    |    16    |    Yes    |    A savepoint operation in the Windows transactional file system failed: 0x%x.    |
|    8536    |    16    |    Yes    |    Only single DB updates are allowed with FILESTREAM operations.    |
|    8537    |    16    |    No    |    This transaction was aborted by Kernel Transaction Manager (KTM).    |
|    8538    |    16    |    No    |    The current isolation level is not supported by the FILESTREAM 0x%x.    |
|    8539    |    10    |    Yes    |    The distributed transaction with UOW %ls was forced to commit. MS DTC became temporarily unavailable and forced heuristic resolution of the transaction. This is an informational message only. No user action is required.    |
|    8540    |    10    |    Yes    |    The distributed transaction with UOW %ls was forced to rollback.    |
|    8541    |    10    |    Yes    |    System process ID %d tried to terminate the distributed transaction with Unit of Work ID %ls. This message occurs when the client executes a KILL statement on the distributed transaction.    |
|    8542    |    10    |    Yes    |    Spid %d tried to commit the distributed transaction with UOW %ls.    |
|    8543    |    10    |    Yes    |    Unable to commit a prepared transaction from the Microsoft Distributed Transaction Coordinator (MS DTC). Shutting down server to initiate resource manager (RM) recovery. When the RM recovers, it will query the transaction manager about the outcome of the in-doubt transaction, and commit or roll back accordingly.    |
|    8544    |    10    |    Yes    |    Unknown status of commit of a two-phase commit transaction. Shutting down server. Restart server to complete recovery.    |
|    8545    |    10    |    Yes    |    Unknown status '%d' from Reenlist call in rm_resolve.    |
|    8546    |    10    |    Yes    |    Unable to load Microsoft Distributed Transaction Coordinator (MS DTC) library. This error indicates that MS DTC is not installed. Install MS DTC to proceed.    |
|    8547    |    10    |    Yes    |    Resource Manager Creation Failed: %ls    |
|    8548    |    10    |    Yes    |    DTC not initialized because it's unavailable    |
|    8549    |    10    |    Yes    |    GetWhereaboutsSize call failed: %ls    |
|    8550    |    10    |    Yes    |    MS DTC initialization failed because the transaction manager address is invalid. The protocol element used to carry address information may be too large. A network protocol analyzer may provide additional information about the cause. Contact your application support provider or Microsoft Product Support Services.    |
|    8551    |    16    |    No    |    CoCreateGuid failed: %ls.    |
|    8552    |    20    |    No    |    RegOpenKeyEx of \"%ls\" failed: %ls.    |
|    8553    |    20    |    No    |    RegQueryValueEx of \"%hs\" failed: %ls.    |
|    8554    |    20    |    No    |    IIDFromString failed for %hs, (%ls).    |
|    8555    |    10    |    Yes    |    RegCloseKey failed: %ls    |
|    8556    |    10    |    Yes    |    Microsoft Distributed Transaction Coordinator (MS DTC) initialization failed due to insufficient memory. It may be necessary to change some server configuration options to make more memory available.    |
|    8557    |    10    |    No    |    The Microsoft Distributed Transaction Coordinator (MS DTC) service could not be contacted. If you would like distributed transaction functionality, please start this service.    |
|    8558    |    20    |    Yes    |    RegDeleteValue of \"%hs\" failed: %ls.    |
|    8560    |    10    |    Yes    |    Attempting to recover in-doubt distributed transactions involving Microsoft Distributed Transaction Coordinator (MS DTC). This is an informational message only. No user action is required.    |
|    8561    |    10    |    Yes    |    Recovery of any in-doubt distributed transactions involving Microsoft Distributed Transaction Coordinator (MS DTC) has completed. This is an informational message only. No user action is required.    |
|    8562    |    10    |    Yes    |    The connection has been lost with Microsoft Distributed Transaction Coordinator (MS DTC). Recovery of any in-doubt distributed transactions involving Microsoft Distributed Transaction Coordinator (MS DTC) will begin once the connection is re-established. This is an informational message only. No user action is required.    |
|    8563    |    10    |    Yes    |    An error occurred while trying to determine the state of the RPCSS service. A call to "%ls" returned: %ls. This is an informational message only. No user action is required.    |
|    8565    |    16    |    Yes    |    SQL Server failed to prepare DTC transaction. Failure code: %d.    |
|    [8601](../../relational-databases/errors-events/mssqlserver-8601-database-engine-error.md)    |    17    |    No    |    Internal Query Processor Error: The query processor could not obtain access to a required interface.    |
|    8602    |    16    |    No    |    Indexes used in hints must be explicitly included by the index tuning wizard.    |
|    8603    |    16    |    No    |    Invalid syntax for internal DBCC REPAIR statement.    |
|    8604    |    16    |    No    |    ALTER TABLE SWITCH statement failed. Table '%.*ls' has a column level check constraint '%.*ls' on column '%.*ls' that is not loadable for semantic validation.    |
|    8605    |    10    |    No    |    Index creation operation will use %ld KB of memory specified in the advanced sp_configure option "min memory per query (KB)" instead of %lu KB specified in "index create memory (KB)" option because the former has to be smaller than the latter.    |
|    8606    |    17    |    No    |    This index operation requires %I64d KB of memory per DOP. The total requirement of %I64d KB for DOP of %lu is greater than the sp_configure value of %lu KB set for the advanced server configuration option "index create memory (KB)". Increase this setting or reduce DOP and rerun the query.    |
|    8607    |    16    |    No    |    The table '%.*ls' cannot be modified because one or more nonclustered indexes reside in a filegroup which is not online.    |
|    8608    |    16    |    No    |    The query could not be completed due to an online index build operation and must be recompiled.    |
|    8616    |    10    |    No    |    The index hints for table '%.*ls' were ignored because the table was considered a fact table in the star join.    |
|    8618    |    16    |    No    |    The query processor could not produce a query plan because a worktable is required, and its minimum row size exceeds the maximum allowable of %d bytes. A typical reason why a worktable is required is a GROUP BY or ORDER BY clause in the query. If the query has a GROUP BY or ORDER BY clause, consider reducing the number and/or size of the fields in the clause. Consider using prefix (LEFT()) or hash (CHECKSUM()) of fields for grouping or prefix for ordering. Note however that this will change the behavior of the query.    |
|    8619    |    16    |    No    |    The query processor could not produce a query plan because a worktable is required, and its minimum row size exceeds the maximum allowable of %d bytes. A typical reason why a worktable is required is a GROUP BY or ORDER BY clause in the query. Resubmit your query without the ROBUST PLAN hint.    |
|    [8621](../../relational-databases/errors-events/mssqlserver-8621-database-engine-error.md)    |    16    |    No    |    The query processor ran out of stack space during query optimization. Please simplify the query.    |
|    8622    |    16    |    No    |    Query processor could not produce a query plan because of the hints defined in this query. Resubmit the query without specifying any hints and without using SET FORCEPLAN.    |
|    [8623](../../relational-databases/errors-events/mssqlserver-8623-database-engine-error.md)    |    16    |    Yes    |    The query processor ran out of internal resources and could not produce a query plan. This is a rare event and only expected for extremely complex queries or queries that reference a very large number of tables or partitions. Please simplify the query. If you believe you have received this message in error, contact Customer Support Services for more information.    |
|    8624    |    16    |    Yes    |    Internal Query Processor Error: The query processor could not produce a query plan. For more information, contact Customer Support Services.    |
|    8625    |    10    |    No    |    Warning: The join order has been enforced because a local join hint is used.    |
|    8628    |    17    |    Yes    |    A time out occurred while waiting to optimize the query. Rerun the query.    |
|    [8630](../../relational-databases/errors-events/mssqlserver-8630-database-engine-error.md)    |    17    |    No    |    Internal Query Processor Error: The query processor encountered an unexpected error during execution.    |
|    8631    |    17    |    No    |    Internal error: Server stack limit has been reached. Please look for potentially deep nesting in your query, and try to simplify it.    |
|    [8632](../../relational-databases/errors-events/mssqlserver-8632-database-engine-error.md)    |    17    |    No    |    Internal error: An expression services limit has been reached. Please look for potentially complex expressions in your query, and try to simplify them.    |
|    8633    |    16    |    No    |    The query processor could not produce a query plan because distributed query does not support materializing intermediate results with default in DML queries over remote sources. Try to use actual default values instead of default or split the update into multiple statements, one only containing the DEFAULT assignment, the other with the rest.    |
|    8634    |    17    |    No    |    The query processor received an error from a cluster communication layer.    |
|    8635    |    16    |    No    |    The query processor could not produce a query plan for a query with a spatial index hint. Reason: %S_MSG. Try removing the index hints or removing SET FORCEPLAN.    |
|    8636    |    16    |    No    |    The query processor could not produce a query plan because there is a subquery in the predicate of the full outer join. This is not supported for distributed queries.    |
|    8637    |    16    |    No    |    The query processor could not produce a query plan because a USE PLAN hint was used for a query that modifies data while the target table of the modification has an index that is currently being built online. Consider waiting until the online index build is done before forcing the plan, or using another way to tune the query, such as updating statistics, or using a different hint or a manual query rewrite.    |
|    [8642](../../relational-databases/errors-events/mssqlserver-8642-database-engine-error.md)    |    17    |    No    |    The query processor could not start the necessary thread resources for parallel query execution.    |
|    8644    |    16    |    No    |    Internal Query Processor Error: The plan selected for execution does not support the invoked given execution routine.    |
|    [8645](../../relational-databases/errors-events/mssqlserver-8645-database-engine-error.md)    |    17    |    Yes    |    A timeout occurred while waiting for memory resources to execute the query in resource pool '%ls' (%ld). Rerun the query.    |
|    8646    |    21    |    Yes    |    Unable to find index entry in index ID %d, of table %d, in database '%.*ls'. The indicated index is corrupt or there is a problem with the current update plan. Run DBCC CHECKDB or DBCC CHECKTABLE. If the problem persists, contact product support.    |
|    8648    |    20    |    Yes    |    Could not insert a row larger than the page size into a hash table. Resubmit the query using the ROBUST PLAN optimization hint.    |
|    [8649](../../relational-databases/errors-events/mssqlserver-8649-database-engine-error.md)    |    17    |    No    |    The query has been canceled because the estimated cost of this query (%d) exceeds the configured threshold of %d. Contact the system administrator.    |
|    [8651](../../relational-databases/errors-events/mssqlserver-8651-database-engine-error.md)    |    17    |    No    |    Could not perform the operation because the requested memory grant was not available in resource pool '%ls' (%ld). Rerun the query, reduce the query load, or check resource governor configuration setting.    |
|    8653    |    16    |    No    |    The query processor is unable to produce a plan for the table or view '%.*ls' because the table resides in a filegroup which is not online.    |
|    8655    |    16    |    No    |    The query processor is unable to produce a plan because the index '%.*ls' on table or view '%.*ls' is disabled.    |
|    8656    |    16    |    No    |    The query processor could not produce a query plan. Resubmit the query after disabling trace flag %d.    |
|    8657    |    17    |    No    |    Could not get the memory grant of %I64d KB because it exceeds the maximum configuration limit in workload group '%ls' (%ld) and resource pool '%ls' (%ld). Contact the server administrator to increase the memory usage limit.    |
|    8660    |    16    |    No    |    Cannot create the clustered index "%.*ls" on view "%.*ls" because the select list of the view definition does not include all columns in the GROUP BY clause. Consider adding these columns to the select list.    |
|    8661    |    16    |    No    |    Cannot create the clustered index "%.*ls" on view "%.*ls" because the index key includes columns that are not in the GROUP BY clause. Consider eliminating columns that are not in the GROUP BY clause from the index key.    |
|    8662    |    16    |    No    |    Cannot create the clustered index "%.*ls" on view "%.*ls" because the view references an unknown value (SUM aggregate of nullable expression). Consider referencing only non-nullable values in SUM. ISNULL() may be useful for this.    |
|    8663    |    16    |    No    |    Cannot create the clustered index "%.*ls" on view "%.*ls" because its select list does not include COUNT_BIG(*). Consider adding COUNT_BIG(*) to the select list.    |
|    8665    |    16    |    No    |    Cannot create the clustered index "%.*ls" on view "%.*ls" because no row can satisfy the view definition. Consider eliminating contradictions from the view definition.    |
|    8668    |    16    |    No    |    Cannot create the clustered index '%.*ls' on view '%.*ls' because the select list of the view contains an expression on result of aggregate function or grouping column. Consider removing expression on result of aggregate function or grouping column from select list.    |
|    8669    |    16    |    No    |    The attempt to maintain the indexed view "%.*ls" failed because it contains an expression on aggregate results, or because it contains a ranking or aggregate window function. Consider dropping the clustered index on the view, or changing the view definition.    |
|    8670    |    16    |    No    |    Query optimizer reached the internal limit of the maximum number of views that can be used during optimization.    |
|    8671    |    16    |    No    |    The attempt to maintain the indexed view "%.*ls" failed because of the ignore_dup_key option on index "%.*ls". Drop the index or re-create it without the ignore_dup_key index option.    |
|    8672    |    16    |    No    |    The MERGE statement attempted to UPDATE or DELETE the same row more than once. This happens when a target row matches more than one source row. A MERGE statement cannot UPDATE/DELETE the same row of the target table multiple times. Refine the ON clause to ensure a target row matches at most one source row, or use the GROUP BY clause to group the source rows.    |
|    8673    |    16    |    No    |    A MERGE statement is not valid if it triggers both the 'ON DELETE SET NULL' and 'ON UPDATE CASCADE' actions for a referential integrity constraint. Modify the actions performed by the MERGE statement to ensure that it does not trigger both these actions for a referential integrity constraint.    |
|    [8680](../../relational-databases/errors-events/mssqlserver-8680-database-engine-error.md)    |    17    |    No    |    Internal Query Processor Error: The query processor encountered an unexpected error during the processing of a remote query phase.    |
|    8682    |    16    |    No    |    SELECT via cursor failed because in XML plan provided to USE PLAN hint, neither Populate nor Fetch plans are provided, and at least one must be present. For best likelihood of successful plan forcing, use an XML cursor plan captured from SQL Server without modification.    |
|    8683    |    16    |    No    |    Could not force query plan because XML showplan provided in USE PLAN hint contains invalid Star Join specification. Consider specifying a USE PLAN hint that contains an unmodified XML showplan produced by SQL Server. This may allow you to force the plan.    |
|    8684    |    16    |    No    |    A query plan could not be found because optimizer exceeded number of allowed operations while searching for plan specified in USE PLAN hint. First consider removing USE PLAN hint. Then if necessary consider (1) updating statistics, (2) using other hints such as join hints, index hints, or the OPTIMIZE FOR hint, (3) rewriting query or breaking it down into two or more separate queries.    |
|    8685    |    16    |    No    |    Query cannot be compiled because \<CursorStmt\> element appears in XML plan provided to USE PLAN but USE PLAN was applied to a non-cursor statement. Consider using an XML plan obtained from SQL Server for statement without modification.    |
|    8686    |    16    |    No    |    Cursor plan forcing failed because input plan has more than one \<Operation\> node with OperationType=%ls. Consider using an XML cursor plan captured from SQL Server without modification.    |
|    8687    |    16    |    No    |    Cursor plan failed because it is not possible to force the plan for a cursor of type other than FAST_FORWARD or STATIC with a USE PLAN hint. Consider removing USE PLAN hint and updating statistics or using different hints to influence query plan choice.    |
|    8688    |    16    |    No    |    Cursor plan forcing failed because in XML plan provided to USE PLAN, required element %ls is missing under \<CursorPlan\> element. Consider using an XML cursor plan captured from SQL Server without modification.    |
|    [8689](../../relational-databases/errors-events/mssqlserver-8689-database-engine-error.md)    |    16    |    No    |    Database '%.*ls', specified in the USE PLAN hint, does not exist. Specify an existing database.    |
|    8690    |    16    |    No    |    Query cannot be compiled because USE PLAN hint conflicts with hint %ls. Consider removing hint %ls.    |
|    8691    |    16    |    No    |    Query cannot be compiled because USE PLAN hint conflicts with SET %ls ON. Consider setting %ls OFF.    |
|    8693    |    16    |    No    |    Cannot compile query because combination of LogicalOp = '%ls', PhysicalOp = '%ls', and sub_element = '%ls' under RelOp element in XML plan in USE PLAN hint is not valid. Use a recognized combination instead. Consider using an automatically generated XML plan without modification.    |
|    8694    |    16    |    No    |    Cannot execute query because USE PLAN hint conflicts with use of distributed query or full-text operations. Consider removing USE PLAN hint.    |
|    8695    |    16    |    No    |    Cannot execute query because of incorrectly formed XML plan in USE PLAN hint. Verify that XML plan is a legal plan suitable for plan forcing. See Books Online for additional details.    |
|    8696    |    16    |    No    |    Cannot run query because of improperly formed Spool element with parent RelOp with NodeId %d in XML plan in USE PLAN hint. Verify that each Spool element's parent RelOp has unique NodeId attribute, and each Spool element has either a single RelOp sub-element, or a PrimaryNodeId attribute, but not both. PrimaryNodeId of Spool must reference NodeId of an existing RelOp with a Spool sub-element. Consider using unmodified XML showplan as USE PLAN hint.    |
|    8697    |    16    |    No    |    Cannot run query because in XML plan provided to USE PLAN, element %ls must have %d %ls nodes as children, but has %d.    |
|    8698    |    16    |    No    |    Query processor could not produce query plan because USE PLAN hint contains plan that could not be verified to be legal for query. Remove or replace USE PLAN hint. For best likelihood of successful plan forcing, verify that the plan provided in the USE PLAN hint is one generated automatically by SQL Server for the same query.    |
|    8699    |    16    |    No    |    Cannot run query because it contains more than one USE PLAN hint. Use at most one USE PLAN hint.    |
|    [8710](../../relational-databases/errors-events/mssqlserver-8710-database-engine-error.md)    |    16    |    No    |    Aggregate functions that are used with CUBE, ROLLUP, or GROUPING SET queries must provide for the merging of subaggregates. To fix this problem, remove the aggregate function or write the query using UNION ALL over GROUP BY clauses.    |
|    8711    |    16    |    No    |    Multiple ordered aggregate functions in the same scope have mutually incompatible orderings.    |
|    [8712](../../relational-databases/errors-events/mssqlserver-8712-database-engine-error.md)    |    16    |    No    |    Index '%.*ls', specified in the USE PLAN hint, does not exist. Specify an existing index, or create an index with the specified name.    |
|    8720    |    15    |    No    |    Cannot execute query. There is more than one TABLE HINT clause specified for object '%.*ls'. Use at most one such TABLE HINT clause per table reference.    |
|    8721    |    15    |    No    |    Cannot execute query. TABLE HINT in the OPTION clause leads to ambiguous reference for object '%.*ls'. Consider USE PLAN query hint instead.    |
|    8722    |    15    |    No    |    Cannot execute query. Semantic affecting hint '%.*ls' appears in the '%.*ls' clause of object '%.*ls' but not in the corresponding '%.*ls' clause. Change the OPTION (TABLE HINTS...) clause so the semantic affecting hints match the WITH clause.    |
|    8723    |    15    |    No    |    Cannot execute query. Object '%.*ls' is specified in the TABLE HINT clause, but is not used in the query or does not match the alias specified in the query. Table references in the TABLE HINT clause must match the WITH clause.    |
|    8724    |    15    |    No    |    Cannot execute query. Table-valued or OPENROWSET function '%.*ls' cannot be specified in the TABLE HINT clause.    |
|    8901    |    16    |    No    |    Table error: Object ID %d has inconsistent metadata. This error cannot be repaired and prevents further processing of this object.    |
|    8902    |    17    |    No    |    Memory allocation error during DBCC processing.    |
|    8903    |    16    |    No    |    Extent %S_PGID in database ID %d is allocated in both GAM %S_PGID and SGAM %S_PGID.    |
|    8904    |    16    |    No    |    Extent %S_PGID in database ID %d is allocated by more than one allocation object.    |
|    8905    |    16    |    No    |    Extent %S_PGID in database ID %d is marked allocated in the GAM, but no SGAM or IAM has allocated it.    |
|    8906    |    16    |    No    |    Page %S_PGID in database ID %d is allocated in the SGAM %S_PGID and PFS %S_PGID, but was not allocated in any IAM. PFS flags '%hs'.    |
|    8907    |    16    |    No    |    The spatial index, XML index or indexed view '%.*ls' (object ID %d) contains rows that were not produced by the view definition. This does not necessarily represent an integrity issue with the data in this database. For more information about troubleshooting DBCC errors on indexed views, see SQL Server Books Online.    |
|    8908    |    16    |    No    |    The spatial index, XML index or indexed view '%.*ls' (object ID %d) does not contain all rows that the view definition produces. This does not necessarily represent an integrity issue with the data in this database. For more information about troubleshooting DBCC errors on spatial indexes, XML indexes, and indexed views, see SQL Server Books Online.    |
|    8909    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls), page ID %S_PGID contains an incorrect page ID in its page header. The PageId in the page header = %S_PGID.    |
|    8910    |    16    |    No    |    Page %S_PGID in database ID %d is allocated to both object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls), and object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls).    |
|    8911    |    10    |    No    |    The error has been repaired.    |
|    8912    |    10    |    No    |    %.*ls fixed %d allocation errors and %d consistency errors in database '%ls'.    |
|    8913    |    16    |    No    |    Extent %S_PGID is allocated to '%ls' and at least one other object.    |
|    8914    |    10    |    No    |    Incorrect PFS free space information for page %S_PGID in object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). Expected value %hs, actual value %hs.    |
|    8915    |    10    |    No    |    File %d (number of mixed extents = %I64d, mixed pages = %I64d).    |
|    8916    |    10    |    No    |    Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls), data extents %I64d, pages %I64d, mixed extent pages %I64d.    |
|    8917    |    10    |    No    |    Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls), index extents %I64d, pages %I64d, mixed extent pages %I64d.    |
|    8918    |    10    |    No    |    (number of mixed extents = %I64d, mixed pages = %I64d) in this database.    |
|    8919    |    16    |    No    |    Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls): The record count in the header (%d) does not match the number of records (%d) found on page %S_PGID.    |
|    8920    |    16    |    No    |    Cannot perform a %ls operation inside a user transaction. Terminate the transaction and reissue the statement.    |
|    8921    |    16    |    No    |    Check terminated. A failure was detected while collecting facts. Possibly tempdb out of space or a system table is inconsistent. Check previous errors.    |
|    8922    |    10    |    No    |    Could not repair this error.    |
|    8923    |    10    |    No    |    The repair level on the DBCC statement caused this repair to be bypassed.    |
|    8924    |    10    |    No    |    Repairing this error requires other errors to be corrected first.    |
|    8925    |    16    |    No    |    Table error: Cross object linkage: Page %S_PGID, slot %d, in object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls), refers to page %S_PGID, slot %d, in object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls).    |
|    8926    |    16    |    No    |    Table error: Cross object linkage: Parent page %S_PGID, slot %d in object %d, index %d, partition %I64d, AU %I64d (%.*ls), and page %S_PGID->next in object %d, index %d, partition %I64d, AU %I64d (%.*ls), refer to page %S_PGID but are not in the same object.    |
|    8927    |    16    |    No    |    Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls): The ghosted record count in the header (%d) does not match the number of ghosted records (%d) found on page %S_PGID.    |
|    8928    |    16    |    No    |    Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls): Page %S_PGID could not be processed. See other errors for details.    |
|    8929    |    16    |    No    |    Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls): Errors found in off-row data with ID %I64d owned by %ls record identified by %.*ls    |
|    8930    |    16    |    No    |    Database error: Database %d has inconsistent metadata. This error cannot be repaired and prevents further DBCC processing. Please restore from a backup.    |
|    8931    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls) B-tree level mismatch, page %S_PGID. Level %d does not match level %d from parent %S_PGID.    |
|    8932    |    16    |    No    |    Could not find filegroup ID %d in sys.filegroups for database '%ls'.    |
|    8933    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). The low key value on page %S_PGID (level %d) is not >= the key value in the parent %S_PGID slot %d.    |
|    8934    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). The high key value on page %S_PGID (level %d) is not less than the low key value in the parent %S_PGID, slot %d of the next page %S_PGID.    |
|    8935    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). The previous link %S_PGID on page %S_PGID does not match the previous page %S_PGID that the parent %S_PGID, slot %d expects for this page.    |
|    8936    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). B-tree chain linkage mismatch. %S_PGID->next = %S_PGID, but %S_PGID->Prev = %S_PGID.    |
|    8937    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). B-tree page %S_PGID has two parent nodes %S_PGID, slot %d and %S_PGID, slot %d.    |
|    8938    |    16    |    No    |    Table error: Page %S_PGID, Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). Unexpected page type %d.    |
|    8939    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls), page %S_PGID. Test (%hs) failed. Values are %ld and %ld.    |
|    8940    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls), page %S_PGID. Test (%hs) failed. Address 0x%x is not aligned.    |
|    8941    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls), page %S_PGID. Test (%hs) failed. Slot %d, offset 0x%x is invalid.    |
|    8942    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls), page %S_PGID. Test (%hs) failed. Slot %d, offset 0x%x overlaps with the prior row.    |
|    8943    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls), page %S_PGID. Test (%hs) failed. Slot %d, row extends into free space at 0x%x.    |
|    8944    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls), page %S_PGID, row %d. Test (%hs) failed. Values are %ld and %ld.    |
|    8945    |    16    |    No    |    Table error: Object ID %d, index ID %d will be rebuilt.    |
|    8946    |    16    |    No    |    Table error: Allocation page %S_PGID has invalid %ls page header values. Type is %d. Check type, alloc unit ID and page ID on the page.    |
|    8947    |    16    |    No    |    Table error: Multiple IAM pages for object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls) contain allocations for the same interval. IAM pages %S_PGID and %S_PGID.    |
|    8948    |    16    |    No    |    Database error: Page %S_PGID is marked with the wrong type in PFS page %S_PGID. PFS status 0x%x expected 0x%x.    |
|    8949    |    10    |    No    |    %.*ls fixed %d allocation errors and %d consistency errors in table '%ls' (object ID %d).    |
|    8950    |    16    |    No    |    %.*ls fixed %d allocation errors and %d consistency errors not associated with any single object.    |
|    8951    |    16    |    No    |    Table error: table '%ls' (ID %d). Data row does not have a matching index row in the index '%ls' (ID %d). Possible missing or invalid keys for the index row matching:    |
|    8952    |    16    |    No    |    Table error: table '%ls' (ID %d). Index row in index '%ls' (ID %d) does not match any data row. Possible extra or invalid keys for:    |
|    8953    |    10    |    No    |    Repair: Deleted off-row data column with ID %I64d, for object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls) on page %S_PGID, slot %d.    |
|    8954    |    10    |    No    |    %.*ls found %d allocation errors and %d consistency errors not associated with any single object.    |
|    8955    |    16    |    No    |    Data row (%d:%d:%d) identified by (%ls) with index values '%ls'.    |
|    8956    |    16    |    No    |    Index row (%d:%d:%d) with values (%ls) pointing to the data row identified by (%ls).    |
|    8957    |    10    |    Yes    |    %lsDBCC %ls (%ls%ls%ls)%ls executed by %ls found %d errors and repaired %d errors. Elapsed time: %d hours %d minutes %d seconds. %.*ls    |
|    8958    |    10    |    No    |    %ls is the minimum repair level for the errors found by DBCC %ls (%ls%ls%ls).    |
|    8959    |    16    |    No    |    Table error: IAM page %S_PGID for object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls) is linked in the IAM chain for object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls) by page %S_PGID.    |
|    8960    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). Page %S_PGID, slot %d, column %d is not a valid complex column.    |
|    8961    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). The off-row data node at page %S_PGID, slot %d, text ID %I64d does not match its reference from page %S_PGID, slot %d.    |
|    8962    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). The off-row data node at page %S_PGID, slot %d, text ID %I64d has incorrect node type %d.    |
|    8963    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). The off-row data node at page %S_PGID, slot %d, text ID %I64d has type %d. It cannot be placed on a page of type %d.    |
|    8964    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). The off-row data node at page %S_PGID, slot %d, text ID %I64d is not referenced.    |
|    8965    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). The off-row data node at page %S_PGID, slot %d, text ID %I64d is referenced by page %S_PGID, slot %d, but was not seen in the scan.    |
|    [8966](../../relational-databases/errors-events/mssqlserver-8966-database-engine-error.md)    |    22    |    Yes    |    Unable to read and latch page %S_PGID with latch type %ls. %ls failed.    |
|    8967    |    16    |    No    |    An internal error occurred in DBCC that prevented further processing. Contact Customer Support Services.    |
|    8968    |    16    |    No    |    Table error: %ls page %S_PGID (object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls)) is out of the range of this database.    |
|    8969    |    16    |    No    |    Table error: IAM chain linkage error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). The next page for IAM page %S_PGID is %S_PGID, but the previous link for page %S_PGID is %S_PGID.    |
|    8970    |    16    |    No    |    Row error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls), page ID %S_PGID, row ID %d. Column '%.*ls' was created NOT NULL, but is NULL in the row.    |
|    8971    |    16    |    No    |    Forwarded row mismatch: Object ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls) page %S_PGID, slot %d points to forwarded row page %S_PGID, slot %d; the forwarded row points back to page %S_PGID, slot %d    |
|    8972    |    16    |    No    |    Forwarded row referenced by more than one row. Object ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls), page %S_PGID, slot %d incorrectly points to the forwarded row page %S_PGID, slot %d, which correctly refers back to page %S_PGID, slot %d.    |
|    8973    |    16    |    No    |    CHECKTABLE object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls) processing encountered page %S_PGID, slot %d twice.    |
|    [8974](../../relational-databases/errors-events/mssqlserver-8974-database-engine-error.md)    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). The off-row data node at page %S_PGID, slot %d, text ID %I64d is pointed to by page %S_PGID, slot %d and by page %S_PGID, slot %d.    |
|    8975    |    10    |    No    |    DBCC cross-rowset check failed for object '%.*ls' (object ID %d) due to internal query error %d, severity %d, state %d. Refer to Books Online for more information on this error.    |
|    8976    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). Page %S_PGID was not seen in the scan although its parent %S_PGID and previous %S_PGID refer to it. Check any previous errors.    |
|    8977    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). Parent node for page %S_PGID was not encountered.    |
|    8978    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). Page %S_PGID is missing a reference from previous page %S_PGID. Possible chain linkage problem.    |
|    8979    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). Page %S_PGID is missing references from parent (unknown) and previous (page %S_PGID) nodes. Possible bad root entry in system catalog.    |
|    8980    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). Index node page %S_PGID, slot %d refers to child page %S_PGID and previous child %S_PGID, but they were not encountered.    |
|    8981    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). The next pointer of %S_PGID refers to page %S_PGID. Neither %S_PGID nor its parent were encountered. Possible bad chain linkage.    |
|    8982    |    16    |    No    |    Table error: Cross object linkage. Page %S_PGID->next in object ID %d, index ID %d, partition ID %I64d, AU ID %I64d (type %.*ls) refers to page %S_PGID in object ID %d, index ID %d, partition ID %I64d, AU ID %I64d (type %.*ls) but is not in the same index.    |
|    8983    |    10    |    No    |    File %d. Extents %I64d, used pages %I64d, reserved pages %I64d, mixed extents %I64d, mixed pages %I64d.    |
|    8984    |    16    |    No    |    Table error: object ID %d, index ID %d, partition ID %I64d. A row should be on partition number %d but was found in partition number %d. Possible extra or invalid keys for:    |
|    8985    |    16    |    No    |    Could not locate file '%.*ls' for database '%.*ls' in sys.database_files. The file either does not exist, or was dropped.    |
|    8986    |    16    |    No    |    Too many errors found (%d) for object ID %d. To see all error messages rerun the statement using "WITH ALL_ERRORMSGS".    |
|    8987    |    16    |    No    |    No help available for DBCC statement '%.*ls'.    |
|    8988    |    16    |    No    |    Row (%d:%d:%d) identified by (%ls).    |
|    8989    |    10    |    No    |    %.*ls found %d allocation errors and %d consistency errors in database '%ls'.    |
|    8990    |    10    |    No    |    %.*ls found %d allocation errors and %d consistency errors in table '%ls' (object ID %d).    |
|    8991    |    16    |    No    |    0x%p to 0x%p is not a valid address range.    |
|    [8992](../../relational-databases/errors-events/mssqlserver-8992-database-engine-error.md)    |    16    |    No    |    Check Catalog Msg %d, State %d: %.*ls    |
|    [8993](../../relational-databases/errors-events/mssqlserver-8993-database-engine-error.md)    |    16    |    No    |    Object ID %d, forwarding row page %S_PGID, slot %d points to page %S_PGID, slot %d. Did not encounter forwarded row. Possible allocation error.    |
|    [8994](../../relational-databases/errors-events/mssqlserver-8994-database-engine-error.md)    |    16    |    No    |    Object ID %d, forwarded row page %S_PGID, slot %d should be pointed to by forwarding row page %S_PGID, slot %d. Did not encounter forwarding row. Possible allocation error.    |
|    8995    |    16    |    No    |    System table '%.*ls' (object ID %d, index ID %d) is in filegroup %d. All system tables must be in filegroup %d.    |
|    [8996](../../relational-databases/errors-events/mssqlserver-8996-database-engine-error.md)    |    16    |    No    |    IAM page %S_PGID for object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls) controls pages in filegroup %d, that should be in filegroup %d.    |
|    8997    |    16    |    No    |    Service Broker Msg %d, State %d: %.*ls    |
|    8998    |    16    |    No    |    Page errors on the GAM, SGAM, or PFS pages prevent allocation integrity checks in database ID %d pages from %S_PGID to %S_PGID. See other errors for cause.    |
|    8999    |    10    |    No    |    Database tempdb allocation errors prevent further %ls processing.    |
