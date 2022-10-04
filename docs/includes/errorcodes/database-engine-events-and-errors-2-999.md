---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/19/2022
ms.topic: include
---
| Error| Severity | Event Logged | Description|
| :------ | :------| :------| :----------------------------- |
|    [-2](../../relational-databases/errors-events/mssqlserver-neg2-database-engine-error.md)    |        |        |    Timeout expired. The timeout period elapsed prior to completion of the operation or the server is not responding. (Microsoft SQL Server, Error: -2).    |
|    [-1](../../relational-databases/errors-events/mssqlserver-1-database-engine-error.md)    |        |        |    An error has occurred while establishing a connection to the server. When connecting to SQL Server 2005, this failure may be caused by the fact that under the default settings SQL Server does not allow remote connections. (provider: SQL Network Interfaces, error: 28 - Server doesn't support requested protocol) (Microsoft SQL Server, Error: -1).    |
|    [2](../../relational-databases/errors-events/mssqlserver-2-database-engine-error.md)    |        |        |    An error has occurred while establishing a connection to the server. When connecting to SQL Server, this failure may be caused by the fact that under the default settings SQL Server does not allow remote connections. (provider: Named Pipes Provider, error: 40 - Could not open a connection to SQL Server) (.Net SqlClient Data Provider)    |
|    21    |    20    |    No    |    Warning: Fatal error %d occurred at %S_DATE. Note the error and time, and contact your system administrator.    |
|    [53](../../relational-databases/errors-events/mssqlserver-53-database-engine-error.md)    |        |        |    An error has occurred while establishing a connection to the server. When connecting to SQL Server, this failure may be caused by the fact that under the default settings SQL Server does not allow remote connections. (provider: Named Pipes Provider, error: 40 - Could not open a connection to SQL Server) (.Net SqlClient Data Provider).    |
|    101    |    15    |    No    |    Query not allowed in Wait for.    |
|    [102](../../relational-databases/errors-events/mssqlserver-102-database-engine-error.md)    |    15    |    No    |    Incorrect syntax near '%.*ls'.    |
|    103    |    15    |    No    |    The %S_MSG that starts with '%.*ls' is too long. Maximum length is %d.    |
|    104    |    15    |    No    |    ORDER BY items must appear in the select list if the statement contains a UNION, INTERSECT or EXCEPT operator.    |
|    105    |    15    |    No    |    Unclosed quotation mark after the character string '%.*ls'.    |
|    106    |    16    |    No    |    Too many table names in the query. The maximum allowable is %d.    |
|    [107](../../relational-databases/errors-events/mssqlserver-107-database-engine-error.md)    |    15    |    No    |    The column prefix '%.*ls' does not match with a table name or alias name used in the query.    |
|    108    |    15    |    No    |    The ORDER BY position number %ld is out of range of the number of items in the select list.    |
|    109    |    15    |    No    |    There are more columns in the INSERT statement than values specified in the VALUES clause. The number of values in the VALUES clause must match the number of columns specified in the INSERT statement.    |
|    110    |    15    |    No    |    There are fewer columns in the INSERT statement than values specified in the VALUES clause. The number of values in the VALUES clause must match the number of columns specified in the INSERT statement.    |
|    111    |    15    |    No    |    '%ls' must be the first statement in a query batch.    |
|    112    |    15    |    No    |    Variables are not allowed in the %ls statement.    |
|    113    |    15    |    No    |    Missing end comment mark '*/'.    |
|    114    |    15    |    No    |    Browse mode is invalid for a statement that assigns values to a variable.    |
|    115    |    15    |    No    |    The FOR UPDATE clause is invalid for statements containing set operators.    |
|    116    |    15    |    No    |    Only one expression can be specified in the select list when the subquery is not introduced with EXISTS.    |
|    117    |    15    |    No    |    The %S_MSG name '%.*ls' contains more than the maximum number of prefixes. The maximum is %d.    |
|    119    |    15    |    No    |    Must pass parameter number %d and subsequent parameters as '\@name = value'. After the form '\@name = value' has been used, all subsequent parameters must be passed in the form '\@name = value'.    |
|    120    |    15    |    No    |    The select list for the INSERT statement contains fewer items than the insert list. The number of SELECT values must match the number of INSERT columns.    |
|    121    |    15    |    No    |    The select list for the INSERT statement contains more items than the insert list. The number of SELECT values must match the number of INSERT columns.    |
|    122    |    15    |    No    |    The %ls option is allowed only with %ls syntax.    |
|    123    |    15    |    No    |    Batch/procedure exceeds maximum length of %d characters.    |
|    124    |    15    |    No    |    CREATE PROCEDURE contains no statements.    |
|    125    |    15    |    No    |    Case expressions may only be nested to level %d.    |
|    126    |    15    |    No    |    Invalid pseudocolumn "%.*ls".    |
|    127    |    15    |    No    |    A TOP N value may not be negative.    |
|    128    |    15    |    No    |    The name "%.*s" is not permitted in this context. Valid expressions are constants, constant expressions, and (in some contexts) variables. Column names are not permitted.    |
|    129    |    15    |    No    |    Fillfactor %d is not a valid percentage; fillfactor must be between 1 and 100.    |
|    130    |    16    |    No    |    Cannot perform an aggregate function on an expression containing an aggregate or a subquery.    |
|    131    |    15    |    No    |    The size (%d) given to the %S_MSG '%.*ls' exceeds the maximum allowed for any data type (%d).    |
|    132    |    15    |    No    |    The label '%.*ls' has already been declared. Label names must be unique within a query batch or stored procedure.    |
|    133    |    15    |    No    |    A GOTO statement references the label '%.*ls' but the label has not been declared.    |
|    134    |    15    |    No    |    The variable name '%.*ls' has already been declared. Variable names must be unique within a query batch or stored procedure.    |
|    135    |    15    |    No    |    Cannot use a BREAK statement outside the scope of a WHILE statement.    |
|    136    |    15    |    No    |    Cannot use a CONTINUE statement outside the scope of a WHILE statement.    |
|    [137](../../relational-databases/errors-events/mssqlserver-137-database-engine-error.md)    |    15    |    No    |    Must declare the scalar variable "%.*ls".    |
|    138    |    15    |    No    |    Correlation clause in a subquery not permitted.    |
|    139    |    15    |    No    |    Cannot assign a default value to a local variable.    |
|    140    |    15    |    No    |    Can only use IF UPDATE within a CREATE TRIGGER statement.    |
|    141    |    15    |    No    |    A SELECT statement that assigns a value to a variable must not be combined with data-retrieval operations.    |
|    142    |    15    |    No    |    Incorrect syntax for definition of the '%ls' constraint.    |
|    143    |    15    |    No    |    A COMPUTE BY item was not found in the order by list. All expressions in the compute by list must also be present in the order by list.    |
|    144    |    15    |    No    |    Cannot use an aggregate or a subquery in an expression used for the group by list of a GROUP BY clause.    |
|    145    |    15    |    No    |    ORDER BY items must appear in the select list if SELECT DISTINCT is specified.    |
|    146    |    15    |    No    |    Could not allocate ancillary table for a subquery. Maximum number of tables in a query (%d) exceeded.    |
|    147    |    15    |    No    |    An aggregate may not appear in the WHERE clause unless it is in a subquery contained in a HAVING clause or a select list, and the column being aggregated is an outer reference.    |
|    148    |    15    |    No    |    Incorrect time syntax in time string '%.*ls' used with WAITFOR.    |
|    149    |    15    |    No    |    Time value '%.*ls' used with WAITFOR is not a valid value. Check date/time syntax.    |
|    150    |    15    |    No    |    Both terms of an outer join must contain columns.    |
|    151    |    15    |    No    |    '%.*ls' is an invalid money value.    |
|    152    |    15    |    No    |    The same large data placement option "%.*ls" has been specified twice.    |
|    153    |    15    |    No    |    Invalid usage of the option %.*ls in the %ls statement.    |
|    154    |    15    |    No    |    %S_MSG is not allowed in %S_MSG.    |
|    155    |    15    |    No    |    '%.*ls' is not a recognized %ls option.    |
|    156    |    15    |    No    |    Incorrect syntax near the keyword '%.*ls'.    |
|    157    |    15    |    No    |    An aggregate may not appear in the set list of an UPDATE statement.    |
|    158    |    15    |    No    |    An aggregate may not appear in the OUTPUT clause.    |
|    159    |    15    |    No    |    Must specify the table name and index name for the DROP INDEX statement.    |
|    160    |    15    |    No    |    Rule does not contain a variable.    |
|    161    |    15    |    No    |    Rule contains more than one variable.    |
|    162    |    15    |    No    |    Invalid expression in the TOP clause.    |
|    163    |    15    |    No    |    The compute by list does not match the order by list.    |
|    164    |    15    |    No    |    Each GROUP BY expression must contain at least one column that is not an outer reference.    |
|    165    |    16    |    No    |    Privilege %ls may not be granted or revoked.    |
|    166    |    15    |    No    |    '%ls' does not allow specifying the database name as a prefix to the object name.    |
|    167    |    15    |    No    |    Cannot create %S_MSG on a temporary object.    |
|    168    |    15    |    No    |    The floating point value '%.*ls' is out of the range of computer representation (%d bytes).    |
|    169    |    15    |    No    |    A column has been specified more than once in the order by list. Columns in the order by list must be unique.    |
|    171    |    15    |    No    |    Browse mode cannot be used with INSERT, SELECT INTO, or UPDATE statements.    |
|    172    |    15    |    No    |    Cannot use HOLDLOCK in browse mode.    |
|    173    |    15    |    No    |    The definition for column '%.*ls' must include a data type.    |
|    174    |    15    |    No    |    The %.*ls function requires %d argument(s).    |
|    175    |    15    |    No    |    An aggregate may not appear in a computed column expression or check constraint.    |
|    176    |    15    |    No    |    The FOR BROWSE clause is no longer supported in views. Set the database compatibility level to 80 or lower for this statement to be allowed.    |
|    177    |    15    |    No    |    The IDENTITY function can only be used when the SELECT statement has an INTO clause.    |
|    178    |    15    |    No    |    A RETURN statement with a return value cannot be used in this context.    |
|    179    |    15    |    No    |    Cannot use the OUTPUT option when passing a constant to a stored procedure.    |
|    180    |    15    |    No    |    There are too many parameters in this %ls statement. The maximum number is %d.    |
|    181    |    15    |    No    |    Cannot use the OUTPUT option in a DECLARE, CREATE AGGREGATE or CREATE FUNCTION statement.    |
|    182    |    15    |    No    |    Table and column names must be supplied for the READTEXT or WRITETEXT utility.    |
|    183    |    15    |    No    |    The scale (%d) for column '%.*ls' must be within the range %d to %d.    |
|    184    |    16    |    No    |    DEFAULT cannot be specified more than once for filegroups of the same content type.    |
|    185    |    15    |    No    |    Data stream is invalid for WRITETEXT statement in bulk form.    |
|    186    |    15    |    No    |    Data stream missing from WRITETEXT statement.    |
|    187    |    16    |    No    |    The valid range for MAX_QUEUE_READERS is 0 to 32767.    |
|    188    |    15    |    No    |    Cannot specify a log file in a CREATE DATABASE statement without also specifying at least one data file.    |
|    189    |    15    |    No    |    The %ls function requires %d to %d arguments.    |
|    190    |    15    |    No    |    An invalid date or time was specified in the statement.    |
|    191    |    15    |    No    |    Some part of your SQL statement is nested too deeply. Rewrite the query or break it up into smaller queries.    |
|    192    |    16    |    No    |    The scale must be less than or equal to the precision.    |
|    193    |    15    |    No    |    The object or column name starting with '%.*ls' is too long. The maximum length is %d characters.    |
|    194    |    15    |    No    |    A SELECT INTO statement cannot contain a SELECT statement that assigns values to a variable.    |
|    195    |    15    |    No    |    '%.*ls' is not a recognized %S_MSG.    |
|    196    |    15    |    No    |    SELECT INTO must be the first query in a statement containing a UNION, INTERSECT or EXCEPT operator.    |
|    197    |    15    |    No    |    EXECUTE cannot be used as a source when inserting into a table variable.    |
|    198    |    15    |    No    |    Browse mode is invalid for statements containing a UNION, INTERSECT or EXCEPT operator.    |
|    199    |    15    |    No    |    An INSERT statement cannot contain a SELECT statement that assigns values to a variable.    |
|    201    |    16    |    No    |    Procedure or function '%.*ls' expects parameter '%.*ls', which was not supplied.    |
|    202    |    16    |    No    |    Invalid type '%s' for WAITFOR. Supported data types are CHAR/VARCHAR, NCHAR/NVARCHAR, and DATETIME. WAITFOR DELAY supports the INT and SMALLINT data types.    |
|    203    |    16    |    No    |    The name '%.*ls' is not a valid identifier.    |
|    204    |    20    |    Yes    |    Normalization error in node %ls.    |
|    205    |    16    |    No    |    All queries combined using a UNION, INTERSECT or EXCEPT operator must have an equal number of expressions in their target lists.    |
|    206    |    16    |    No    |    Operand type clash: %ls is incompatible with %ls    |
| [207](../../relational-databases/errors-events/mssqlserver-207-database-engine-error.md)    |    16    |    No    |    Invalid column name '%.*ls'.    |
|    [208](../../relational-databases/errors-events/mssqlserver-208-database-engine-error.md)    |    16    |    No    |    Invalid object name '%.*ls'.    |
|    209    |    16    |    No    |    Ambiguous column name '%.*ls'.    |
|    210    |    16    |    No    |    Conversion failed when converting datetime from binary/varbinary string.    |
|    211    |    23    |    Yes    |    Possible schema corruption. Run DBCC CHECKCATALOG.    |
|    212    |    16    |    No    |    Expression result length exceeds the maximum. %d max, %d found.    |
|    213    |    16    |    No    |    Column name or number of supplied values does not match table definition.    |
|    214    |    16    |    No    |    Procedure expects parameter '%ls' of type '%ls'.    |
|    215    |    16    |    No    |    Parameters supplied for object '%.*ls' which is not a function. If the parameters are intended as a table hint, a WITH keyword is required.    |
|    216    |    16    |    No    |    Parameters were not supplied for the function '%.*ls'.    |
|    217    |    16    |    No    |    Maximum stored procedure, function, trigger, or view nesting level exceeded (limit %d).    |
|    218    |    16    |    No    |    Could not find the type '%.*ls'. Either it does not exist or you do not have the necessary permission.    |
|    219    |    16    |    No    |    The type '%.*ls' already exists, or you do not have permission to create it.    |
|    220    |    16    |    No    |    Arithmetic overflow error for data type %ls, value = %ld.    |
|    221    |    10    |    No    |    FIPS Warning: Implicit conversion from %ls to %ls.    |
|    222    |    16    |    No    |    The base type "%.*ls" is not a valid base type for the alias data type.    |
|    223    |    11    |    No    |    Object ID %ld specified as a default for table ID %ld, column ID %d is missing or not of type default.    |
|    224    |    11    |    No    |    Object ID %ld specified as a rule for table ID %ld, column ID %d is missing or not of type default.    |
|    225    |    16    |    No    |    The parameters supplied for the %ls "%.*ls" are not valid.    |
|    226    |    16    |    No    |    %ls statement not allowed within multi-statement transaction.    |
|    227    |    15    |    No    |    %.*ls is not a valid function, property, or field.    |
|    228    |    15    |    No    |    Method '%.*ls' of type '%.*ls' in assembly '%.*ls' does not return any value.    |
|    229    |    14    |    No    |    The %ls permission was denied on the object '%.*ls', database '%.*ls', schema '%.*ls'.    |
|    230    |    14    |    No    |    The %ls permission was denied on the column '%.*ls' of the object '%.*ls", database '%.*ls', schema '%.*ls'.    |
|    231    |    11    |    No    |    No such default. ID = %ld, database ID = %d.    |
|    232    |    16    |    No    |    Arithmetic overflow error for type %ls, value = %f.    |
|    233    |    16    |    No    |    The column '%.*ls' in table '%.*ls' cannot be null.    |
|    [233](../../relational-databases/errors-events/mssqlserver-233-database-engine-error.md)    |        |        |    A connection was successfully established with the server, but then an error occurred during the login process. (provider: Shared Memory Provider, error: 0 - No process is on the other end of the pipe.) (Microsoft SQL Server, Error: 233)    |
|    234    |    16    |    No    |    There is insufficient result space to convert a money value to %ls.    |
|    235    |    16    |    No    |    Cannot convert a char value to money. The char value has incorrect syntax.    |
|    236    |    16    |    No    |    The conversion from char data type to money resulted in a money overflow error.    |
|    237    |    16    |    No    |    There is insufficient result space to convert a money value to %ls.    |
|    239    |    16    |    No    |    Duplicate common table expression name '%.*ls' was specified.    |
|    240    |    16    |    No    |    Types don't match between the anchor and the recursive part in column "%.*ls" of recursive query "%.*ls".    |
|    241    |    16    |    No    |    Conversion failed when converting date and/or time from character string.    |
|    242    |    16    |    No    |    The conversion of a %ls data type to a %ls data type resulted in an out-of-range value.    |
|    243    |    16    |    No    |    Type %.*ls is not a defined system type.    |
|    244    |    16    |    No    |    The conversion of the %ls value '%.*ls' overflowed an %hs column. Use a larger integer column.    |
|    245    |    16    |    No    |    Conversion failed when converting the %ls value '%.*ls' to data type %ls.    |
|    246    |    16    |    No    |    No anchor member was specified for recursive query "%.*ls".    |
|    247    |    16    |    No    |    An anchor member was found in the recursive part of recursive query "%.*ls".    |
|    248    |    16    |    No    |    The conversion of the %ls value '%.*ls' overflowed an int column.    |
|    249    |    16    |    No    |    The type "%ls" is not comparable. It cannot be used in the %ls clause.    |
|    251    |    16    |    No    |    Could not allocate ancillary table for query optimization. Maximum number of tables in a query (%d) exceeded.    |
|    252    |    16    |    No    |    Recursive common table expression '%.*ls' does not contain a top-level UNION ALL operator.    |
|    253    |    16    |    No    |    Recursive member of a common table expression '%.*ls' has multiple recursive references.    |
|    254    |    16    |    No    |    Prefixed columns are not allowed in the column list of a PIVOT operator.    |
|    255    |    16    |    No    |    Pseudocolumns are not allowed in the column list of a PIVOT operator.    |
|    256    |    16    |    No    |    The data type %ls is invalid for the %ls function. Allowed types are: char/varchar, nchar/nvarchar, and binary/varbinary.    |
|    257    |    16    |    No    |    Implicit conversion from data type %ls to %ls is not allowed. Use the CONVERT function to run this query.    |
|    258    |    15    |    No    |    Cannot call methods on %ls.    |
|    259    |    16    |    No    |    Ad hoc updates to system catalogs are not allowed.    |
|    260    |    16    |    No    |    Disallowed implicit conversion from data type %ls to data type %ls, table '%.*ls', column '%.*ls'. Use the CONVERT function to run this query.    |
|    261    |    16    |    No    |    '%.*ls' is not a recognized function.    |
|    262    |    16    |    No    |    %ls permission denied in database '%.*ls'.    |
|    263    |    16    |    No    |    Must specify table to select from.    |
|    264    |    16    |    No    |    The column name '%.*ls' is specified more than once in the SET clause. A column cannot be assigned more than one value in the same SET clause. Modify the SET clause to make sure that a column is updated only once. If the SET clause updates columns of a view, then the column name '%.*ls' may appear twice in the view definition.    |
|    265    |    16    |    No    |    The column name "%.*ls" specified in the %ls operator conflicts with the existing column name in the %ls argument.    |
|    266    |    16    |    No    |    Transaction count after EXECUTE indicates a mismatching number of BEGIN and COMMIT statements. Previous count = %ld, current count = %ld.    |
|    267    |    16    |    No    |    Object '%.*ls' cannot be found.    |
|    268    |    16    |    No    |    Cannot run SELECT INTO in this database. The database owner must run sp_dboption to enable this option.    |
|    270    |    16    |    No    |    Object '%.*ls' cannot be modified.    |
|    271    |    16    |    No    |    The column "%.*ls" cannot be modified because it is either a computed column or is the result of a UNION operator.    |
|    272    |    16    |    No    |    Cannot update a timestamp column.    |
|    273    |    16    |    No    |    Cannot insert an explicit value into a timestamp column. Use INSERT with a column list to exclude the timestamp column, or insert a DEFAULT into the timestamp column.    |
|    275    |    16    |    No    |    Prefixes are not allowed in value or pivot columns of an UNPIVOT operator.    |
|    276    |    16    |    No    |    Pseudocolumns are not allowed as value or pivot columns of an UNPIVOT operator.    |
|    277    |    16    |    No    |    The column "%.*ls" is specified multiple times in the column list of the UNPIVOT operator.    |
|    278    |    16    |    No    |    The text, ntext, and image data types cannot be used in a GROUP BY clause.    |
|    279    |    16    |    No    |    The text, ntext, and image data types are invalid in this subquery or aggregate expression.    |
|    280    |    16    |    No    |    Only base table columns are allowed in the TEXTPTR function.    |
|    281    |    16    |    No    |    %d is not a valid style number when converting from %ls to a character string.    |
|    282    |    10    |    No    |    The '%.*ls' procedure attempted to return a status of NULL, which is not allowed. A status of 0 will be returned instead.    |
|    283    |    16    |    No    |    READTEXT cannot be used on inserted or deleted tables within an INSTEAD OF trigger.    |
|    284    |    16    |    No    |    Rules cannot be bound to text, ntext, or image data types.    |
|    285    |    16    |    No    |    The READTEXT, WRITETEXT, and UPDATETEXT statements cannot be used with views or functions.    |
|    286    |    16    |    No    |    The logical tables INSERTED and DELETED cannot be updated.    |
|    287    |    16    |    No    |    The %ls statement is not allowed within a trigger.    |
|    288    |    16    |    No    |    The PATINDEX function operates on char, nchar, varchar, nvarchar, text, and ntext data types only.    |
|    290    |    16    |    No    |    Invalid EXECUTE statement using object "%ls", method "%ls".    |
|    291    |    16    |    No    |    CAST or CONVERT: invalid attributes specified for type '%.*ls'    |
|    292    |    16    |    No    |    There is insufficient result space to convert a smallmoney value to %ls.    |
|    293    |    16    |    No    |    Cannot convert char value to smallmoney. The char value has incorrect syntax.    |
|    294    |    16    |    No    |    The conversion from char data type to smallmoney data type resulted in a smallmoney overflow error.    |
|    295    |    16    |    No    |    Conversion failed when converting character string to smalldatetime data type.    |
|    297    |    16    |    No    |    The user does not have permission to perform this action.    |
|    300    |    14    |    No    |    %ls permission was denied on object '%.*ls', database '%.*ls'.    |
|    301    |    16    |    No    |    Query contains an outer-join request that is not permitted.    |
|    302    |    16    |    No    |    The newsequentialid() built-in function can only be used in a DEFAULT expression for a column of type 'uniqueidentifier' in a CREATE TABLE or ALTER TABLE statement. It cannot be combined with other operators to form a complex scalar expression.    |
|    303    |    16    |    No    |    The table '%.*ls' is an inner member of an outer-join clause. This is not allowed if the table also participates in a regular join clause.    |
|    304    |    16    |    No    |    '%d' is out of range for index option '%.*ls'. See sp_configure option '%ls' for valid values.    |
|    305    |    16    |    No    |    The XML data type cannot be compared or sorted, except when using the IS NULL operator.    |
|    306    |    16    |    No    |    The text, ntext, and image data types cannot be compared or sorted, except when using IS NULL or LIKE operator.    |
|    307    |    16    |    No    |    Index ID %d on table '%.*ls' (specified in the FROM clause) does not exist.    |
|    308    |    16    |    No    |    Index '%.*ls' on table '%.*ls' (specified in the FROM clause) does not exist.    |
|    309    |    16    |    No    |    Cannot use index "%.*ls" on table "%.*ls" in a hint. XML indexes are not allowed in hints.    |
|    310    |    15    |    No    |    The value %d specified for the MAXRECURSION option exceeds the allowed maximum of %d.    |
|    311    |    16    |    No    |    Cannot use text, ntext, or image columns in the 'inserted' and 'deleted' tables.    |
|    312    |    16    |    No    |    Cannot reference text, ntext, or image columns in a filter stored procedure.    |
|    313    |    16    |    No    |    An insufficient number of arguments were supplied for the procedure or function %.*ls.    |
|    314    |    16    |    No    |    Cannot use GROUP BY ALL with the special tables INSERTED or DELETED.    |
|    315    |    16    |    No    |    Index "%.*ls" on table "%.*ls" (specified in the FROM clause) is disabled or resides in a filegroup which is not online.    |
|    316    |    16    |    No    |    The index ID %d on table "%.*ls" (specified in the FROM clause) is disabled or resides in a filegroup which is not online.    |
|    317    |    16    |    No    |    Table-valued function '%.*ls' cannot have a column alias.    |
|    318    |    16    |    No    |    The table (and its columns) returned by a table-valued method need to be aliased.    |
|    319    |    16    |    No    |    Incorrect syntax near the keyword 'with'. If this statement is a common table expression, an xmlnamespaces clause or a change tracking context clause, the previous statement must be terminated with a semicolon.    |
|    320    |    16    |    No    |    The compile-time variable value for '%.*ls' in the OPTIMIZE FOR clause must be a literal.    |
|    321    |    15    |    No    |    %.*ls is not a recognized table hints option. If it is intended as a parameter to a table-valued function or to the CHANGETABLE function, ensure that your database compatibility mode is set to 90.    |
|    322    |    15    |    No    |    The variable "%.*ls" is specified in the OPTIMIZE FOR clause, but is not used in the query.    |
|    323    |    16    |    No    |    The 'COMPUTE' clause is not allowed in a statement containing an INTERSECT or EXCEPT operator.    |
|    324    |    15    |    No    |    The 'ALL' version of the %.*ls operator is not supported.    |
|    325    |    15    |    No    |    Incorrect syntax near '%.*ls'. You may need to set the compatibility level of the current database to a higher value to enable this feature. See help for the SET COMPATIBILITY_LEVEL option of ALTER DATABASE.    |
|    326    |    16    |    No    |    Multi-part identifier '%.*ls' is ambiguous. Both columns '%.*ls' and '%.*ls' exist.    |
|    327    |    16    |    No    |    Function call '%.*ls' is ambiguous: both a user-defined function and a method call with this name exist.    |
|    328    |    16    |    No    |    A cursor plan could not be generated for the given statement because the textptr() function was used on a LOB column from one of the base tables.    |
|    329    |    16    |    No    |    Each GROUP BY expression must contain at least one column reference.    |
|    330    |    15    |    No    |    The target '%.*ls' of the OUTPUT INTO clause cannot be a view or common table expression.    |
|    331    |    15    |    No    |    The target table '%.*ls' of the OUTPUT INTO clause cannot have any enabled triggers.    |
|    332    |    15    |    No    |    The target table '%.*ls' of the OUTPUT INTO clause cannot be on either side of a (primary key, foreign key) relationship. Found reference constraint '%ls'.    |
|    333    |    15    |    No    |    The target table '%.*ls' of the OUTPUT INTO clause cannot have any enabled check constraints or any enabled rules. Found check constraint or rule '%ls'.    |
|    334    |    15    |    No    |    The target table '%.*ls' of the DML statement cannot have any enabled triggers if the statement contains an OUTPUT clause without INTO clause.    |
|    335    |    16    |    No    |    Function call cannot be used to match a target table in the FROM clause of a DELETE or UPDATE statement. Use function name '%.*ls' without parameters instead.    |
|    336    |    15    |    No    |    Incorrect syntax near '%.*ls'. If this is intended to be a common table expression, you need to explicitly terminate the previous statement with a semi-colon.    |
|    337    |    10    |    No    |    Warning: the floating point value '%.*ls' is too small. It will be interpreted as 0.    |
|    338    |    16    |    No    |    READEXT, WRITETEXT, and UPDATETEXT statements cannot be used with views, remote tables, and inserted or deleted tables inside triggers.    |
|    339    |    16    |    No    |    DEFAULT or NULL are not allowed as explicit identity values.    |
|    340    |    16    |    No    |    Cannot create the trigger "%.*ls" on view "%.*ls". AFTER triggers cannot be created on views.    |
|    341    |    16    |    No    |    Replication filter procedures may not contain columns of large object, large value, XML or CLR type.    |
|    342    |    16    |    No    |    Column "%.*ls" is not allowed in this context, and the user-defined function or aggregate "%.*ls" could not be found.    |
|    343    |    15    |    No    |    Unknown object type '%.*ls' used in a CREATE, DROP, or ALTER statement.    |
|    344    |    16    |    No    |    Remote function reference '%.*ls' is not allowed, and the column name '%.*ls' could not be found or is ambiguous.    |
|    345    |    16    |    No    |    Function '%.*ls' is not allowed in the OUTPUT clause, because it performs user or system data access, or is assumed to perform this access. A function is assumed by default to perform data access if it is not schemabound.    |
|    346    |    15    |    No    |    The parameter "%.*ls" cannot be declared READONLY since it is not a table-valued parameter.    |
|    347    |    16    |    No    |    The table-valued parameter "%.*ls" cannot be declared as an OUTPUT parameter.    |
|    348    |    16    |    No    |    The table variable "%.*ls" cannot be passed to a stored procedure with the OUTPUT option.    |
|    349    |    16    |    No    |    The procedure "%.*ls" has no parameter named "%.*ls".    |
|    350    |    16    |    No    |    The column "%.*ls" does not have a valid data type. A column cannot be of a user-defined table type.    |
|    351    |    16    |    No    |    Column, parameter, or variable %.*ls. : Cannot find data type %.*ls.    |
|    352    |    15    |    No    |    The table-valued parameter "%.*ls" must be declared with the READONLY option.    |
|    353    |    16    |    No    |    Function '%.*ls' is not allowed in the %S_MSG clause when the FROM clause contains a nested INSERT, UPDATE, DELETE, or MERGE statement. This is because the function performs user or system data access, or is assumed to perform this access. By default, a function is assumed to perform data access if it is not schema-bound.    |
|    354    |    15    |    No    |    The target '%.*ls' of the INSERT statement cannot be a view or common table expression when the FROM clause contains a nested INSERT, UPDATE, DELETE, or MERGE statement.    |
|    355    |    15    |    No    |    The target table '%.*ls' of the INSERT statement cannot have any enabled triggers when the FROM clause contains a nested INSERT, UPDATE, DELETE, or MERGE statement.    |
|    356    |    15    |    No    |    The target table '%.*ls' of the INSERT statement cannot be on either side of a (primary key, foreign key) relationship when the FROM clause contains a nested INSERT, UPDATE, DELETE, or MERGE statement. Found reference constraint '%ls'.    |
|    357    |    15    |    No    |    The target table '%.*ls' of the INSERT statement cannot have any enabled rules when the FROM clause contains a nested INSERT, UPDATE, DELETE, or MERGE statement. Found rule '%ls'.    |
|    358    |    15    |    No    |    The target table '%.*ls' of the MERGE statement cannot have any enabled rules. Found rule '%ls'.    |
|    359    |    15    |    No    |    The target '%.*ls' of an OUTPUT INTO clause has an index with the ignore_dup_key option and cannot be used when an OUTPUT clause is also used.    |
|    [360](../../relational-databases/errors-events/mssqlserver-360-database-engine-error.md)    |    15    |    No    |    The target column list of an INSERT, UPDATE, or MERGE statement cannot contain both a sparse column and the column set that contains the sparse column. Rewrite the statement to include either the sparse column or the column set, but not both.    |
|    361    |    16    |    No    |    The number of target columns that are specified in an INSERT, UPDATE, or MERGE statement exceeds the maximum of %d. This total number includes identity, timestamp, and columns that have default values. To correct this error, change the query to target a sparse column set instead of single sparse columns.    |
|    401    |    16    |    No    |    Unimplemented statement or expression %ls.    |
|    402    |    16    |    No    |    The data types %s and %s are incompatible in the %s operator.    |
|    403    |    16    |    No    |    Invalid operator for data type. Operator equals %ls, type equals %ls.    |
|    404    |    16    |    No    |    The column reference "inserted.%.*ls" is not allowed because it refers to a base table that is not being modified in this statement.    |
|    405    |    16    |    No    |    A remote table cannot be used as a DML target in a statement which includes an OUTPUT clause or a nested DML statement.    |
|    406    |    16    |    No    |    %ls cannot be used in the PIVOT operator because it is not invariant to NULLs.    |
|    407    |    16    |    No    |    Internal error. The string routine in file %hs, line %d failed with HRESULT 0x%x.    |
|    408    |    16    |    No    |    A constant expression was encountered in the ORDER BY list, position %i.    |
|    411    |    16    |    No    |    COMPUTE clause #%d, aggregate expression #%d is not in the select list.    |
|    412    |    16    |    No    |    The column "%.*ls" is not updatable because it is derived or constant.    |
|    413    |    16    |    No    |    Correlated parameters or sub-queries are not supported by the inline function "%.*ls".    |
|    414    |    16    |    No    |    UPDATE is not allowed because the statement updates view "%.*ls" which participates in a join and has an INSTEAD OF UPDATE trigger.    |
|    415    |    16    |    No    |    DELETE is not allowed because the statement updates view "%.*ls" which participates in a join and has an INSTEAD OF DELETE trigger.    |
|    416    |    16    |    No    |    The service queue "%.*ls" cannot be directly updated.    |
|    417    |    16    |    No    |    TOP is not allowed in an UPDATE or DELETE statement against a partitioned view.    |
|    418    |    16    |    No    |    Objects exposing CLR type columns are not allowed in distributed queries. Use a pass-through query to access the remote object '%.*ls'.    |
|    421    |    16    |    No    |    The %ls data type cannot be selected as DISTINCT because it is not comparable.    |
|    422    |    16    |    No    |    Common table expression defined but not used.    |
|    423    |    16    |    No    |    Xml data type methods are not supported in check constraints. Create a scalar user-defined function to wrap the method invocation. The error occurred at table "%.*ls".    |
|    424    |    16    |    No    |    Xml data type methods are not supported in computed column definitions of table variables and return tables of table-valued functions. The error occurred at column "%.*ls", table "%.*ls", in the %ls statement.    |
|    425    |    16    |    No    |    Data type %ls of receiving variable is not equal to the data type %ls of column '%.*ls'.    |
|    426    |    16    |    No    |    The length %d of the receiving variable is less than the length %d of the column '%.*ls'.    |
|    427    |    20    |    Yes    |    Could not load the definition for constraint ID %d in database ID %d. Run DBCC CHECKCATALOG to verify the integrity of the database.    |
|    428    |    16    |    No    |    Insert bulk cannot be used in a multi-statement batch.    |
|    432    |    16    |    No    |    Xml data type methods are not supported in check constraints anymore. Please drop the constraint or create a scalar user-defined function to wrap the method invocation. The error occurred at table "%.*ls".    |
|    434    |    16    |    No    |    Function '%ls' is not allowed in the OUTPUT clause.    |
|    435    |    16    |    No    |    Xml data type methods are not supported in computed column definitions. Create a scalar user-defined function to wrap the method invocation. The error occurred at column "%.*ls", table "%.*ls", in the %ls statement.    |
|    438    |    16    |    No    |    Xml data type methods are not allowed in rules. The error occurred at table "%.*ls".    |
|    440    |    16    |    No    |    Internal query compilation error. The stack overflow could not be handled.    |
|    441    |    16    |    No    |    Cannot use the '%ls' function on a remote data source.    |
|    442    |    16    |    No    |    The NEST argument must be a column reference. Expressions are not allowed.    |
|    443    |    16    |    No    |    Invalid use of a side-effecting operator '%s' within a function.    |
|    444    |    16    |    No    |    Select statements included within a function cannot return data to a client.    |
|    445    |    16    |    No    |    COLLATE clause cannot be used on expressions containing a COLLATE clause.    |
|    446    |    16    |    No    |    Cannot resolve collation conflict for %ls operation.    |
|    447    |    16    |    No    |    Expression type %ls is invalid for COLLATE clause.    |
|    448    |    16    |    No    |    Invalid collation '%.*ls'.    |
|    449    |    16    |    No    |    Collation conflict caused by collate clauses with different collation '%.*ls' and '%.*ls'.    |
|    450    |    16    |    No    |    Code page translations are not supported for the text data type. From: %d To: %d.    |
|    451    |    16    |    No    |    Cannot resolve collation conflict for column %d in %ls statement.    |
|    452    |    16    |    No    |    COLLATE clause cannot be used on user-defined data types.    |
|    453    |    16    |    No    |    Collation '%.*ls' is supported on Unicode data types only and cannot be set at the database or server level.    |
|    454    |    16    |    No    |    The UNNEST argument must be a nested table column.    |
|    455    |    16    |    No    |    The last statement included within a function must be a return statement.    |
|    456    |    16    |    No    |    Implicit conversion of %ls value to %ls cannot be performed because the resulting collation is unresolved due to collation conflict.    |
|    457    |    16    |    No    |    Implicit conversion of %ls value to %ls cannot be performed because the collation of the value is unresolved due to a collation conflict.    |
|    458    |    16    |    No    |    Cannot create the SELECT INTO target table "%.*ls" because the xml column "%.*ls" is typed with a schema collection "%.*ls" from database "%.*ls". Xml columns cannot refer to schemata across databases.    |
|    459    |    16    |    No    |    Collation '%.*ls' is supported on Unicode data types only and cannot be applied to char, varchar or text data types.    |
|    460    |    16    |    No    |    DISTINCT operator is not allowed in the recursive part of a recursive common table expression '%.*ls'.    |
|    461    |    16    |    No    |    TOP operator is not allowed in the recursive part of a recursive common table expression '%.*ls'.    |
|    462    |    16    |    No    |    Outer join is not allowed in the recursive part of a recursive common table expression '%.*ls'.    |
|    463    |    16    |    No    |    Functions with parameters are not allowed in the recursive part of a recursive common table expression '%.*ls'.    |
|    464    |    16    |    No    |    Functions with side effects are not allowed in the recursive part of a recursive common table expression '%.*ls'.    |
|    465    |    16    |    No    |    Recursive references are not allowed in subqueries.    |
|    466    |    16    |    No    |    UNION operator is not allowed in the recursive part of a recursive common table expression '%.*ls'.    |
|    467    |    16    |    No    |    GROUP BY, HAVING, or aggregate functions are not allowed in the recursive part of a recursive common table expression '%.*ls'.    |
|    468    |    16    |    No    |    Cannot resolve the collation conflict between "%.*ls" and "%.*ls" in the %ls operation.    |
|    469    |    16    |    No    |    An explicit column list must be specified for target table '%.*ls' when table hint KEEPIDENTITY is used and the table contains an identity column.    |
|    470    |    16    |    No    |    The synonym "%.*ls" referenced synonym "%.*ls". Synonym chaining is not allowed.    |
|    471    |    16    |    No    |    Only one of the three options, SINGLE_BLOB, SINGLE_CLOB or SINGLE_NCLOB, can be specified.    |
|    472    |    16    |    No    |    Either a format file or one of the three options SINGLE_BLOB, SINGLE_CLOB, or SINGLE_NCLOB must be specified.    |
|    473    |    16    |    No    |    The incorrect value "%.*ls" is supplied in the PIVOT operator.    |
|    474    |    16    |    No    |    Unable to load the computed column definitions for table "%.*ls".    |
|    475    |    16    |    No    |    Invalid SAMPLE clause. Only table names in the FROM clause of SELECT, UPDATE, and DELETE queries may be sampled.    |
|    476    |    16    |    No    |    Invalid PERCENT tablesample size "%f" for table "%.*ls". The PERCENT tablesample size must be between 0 and 100.    |
|    477    |    16    |    No    |    Invalid ROWS value or REPEATABLE seed in the TABLESAMPLE clause for table "%.*ls". The value or seed must be an integer.    |
|    478    |    16    |    No    |    The TABLESAMPLE clause cannot be used in a view definition or inline table function definition.    |
|    479    |    16    |    No    |    Invalid ROWS value or REPEATABLE seed "%I64d" in the TABLESAMPLE clause for table "%.*ls". The value or seed must be greater than 0.    |
|    480    |    16    |    No    |    The TABLESAMPLE clause cannot be used with the table function "%.*ls".    |
|    481    |    16    |    No    |    The TABLESAMPLE clause cannot be used with the linked server table "%.*ls".    |
|    482    |    16    |    No    |    Non-constant or invalid expression is in the TABLESAMPLE or the REPEATABLE clause.    |
|    483    |    16    |    No    |    The OUTPUT clause cannot be used in an INSERT...EXEC statement.    |
|    484    |    16    |    No    |    Cannot declare more than %d local variables.    |
|    485    |    16    |    No    |    Views and inline functions cannot return xml columns that are typed with a schema collection registered in a database other than current. Column "%.*ls" is typed with the schema collection "%.*ls", which is registered in database "%.*ls".    |
|    486    |    16    |    No    |    %.*ls does not allow specifying a schema name as a prefix to the assembly name.    |
|    487    |    16    |    No    |    An invalid option was specified for the statement "%.*ls".    |
|    488    |    16    |    No    |    %s columns must be comparable. The type of column "%.*ls" is "%s", which is not comparable.    |
|    489    |    16    |    No    |    The OUTPUT clause cannot be specified because the target view "%.*ls" is a partitioned view.    |
|    490    |    16    |    No    |    The resync functionality is temporarily disabled.    |
|    491    |    16    |    No    |    A correlation name must be specified for the bulk rowset in the from clause.    |
|    492    |    16    |    No    |    Duplicate column names are not allowed in result sets obtained through OPENQUERY and OPENROWSET. The column name "%.*ls" is a duplicate.    |
|    493    |    16    |    No    |    The column '%.*ls' that was returned from the nodes() method cannot be used directly. It can only be used with one of the four XML data type methods, exist(), nodes(), query(), and value(), or in IS NULL and IS NOT NULL checks.    |
|    494    |    16    |    No    |    The TABLESAMPLE clause can only be used with local tables.    |
|    495    |    16    |    No    |    The return table column "%.*ls" is not the same type as the type it was created with. Drop and recreate the module using a two-part name for the type, or use sp_refreshsqlmodule to refresh its parameters metadata.    |
|    496    |    16    |    No    |    The parameter "%.*ls" is not the same type as the type it was created with. Drop and recreate the module using a two-part name for the type, or use sp_refreshsqlmodule to refresh its parameters metadata.    |
|    497    |    16    |    No    |    Variables are not allowed in the TABLESAMPLE or REPEATABLE clauses.    |
|    498    |    16    |    No    |    Invalid value in the TABLESAMPLE or the REPEATABLE clause.    |
|    499    |    16    |    No    |    Invalid parameter for the getchecksum function.    |
|    500    |    16    |    No    |    Trying to pass a table-valued parameter with %d column(s) where the corresponding user-defined table type requires %d column(s).    |
|    505    |    16    |    No    |    The current user account was invoked with SETUSER or SP_SETAPPROLE. Changing databases is not allowed.    |
|    506    |    16    |    No    |    The invalid escape character "%.*ls" was specified in a %ls predicate.    |
|    507    |    16    |    No    |    Invalid argument for SET ROWCOUNT. Must be a non-null non-negative integer.    |
|    509    |    11    |    No    |    User name '%.*ls' not found.    |
|    510    |    16    |    No    |    Cannot create a worktable row larger than allowable maximum. Resubmit your query with the ROBUST PLAN hint.    |
|    [511](../../relational-databases/errors-events/mssqlserver-511-database-engine-error.md)    |    16    |    No    |    Cannot create a row of size %d which is greater than the allowable maximum row size of %d.    |
|    512    |    16    |    No    |    Subquery returned more than 1 value. This is not permitted when the subquery follows =, !=, <, <= , >, >= or when the subquery is used as an expression.    |
|    513    |    16    |    No    |    A column insert or update conflicts with a rule imposed by a previous CREATE RULE statement. The statement was terminated. The conflict occurred in database '%.*ls', table '%.*ls', column '%.*ls'.    |
|    515    |    16    |    No    |    Cannot insert the value NULL into column '%.*ls', table '%.*ls'; column does not allow nulls. %ls fails.    |
|    517    |    16    |    No    |    Adding a value to a '%ls' column caused an overflow.    |
|    518    |    16    |    No    |    Cannot convert data type %ls to %ls.    |
|    522    |    16    |    No    |    The WAITFOR thread was evicted.    |
|    523    |    16    |    No    |    A trigger returned a resultset and/or was running with SET NOCOUNT OFF while another outstanding result set was active.    |
|    524    |    16    |    No    |    A trigger returned a resultset and the server option 'disallow results from triggers' is true.    |
|    525    |    16    |    No    |    The column that was returned from the nodes() method cannot be converted to the data type %ls. It can only be used with one of the four XML data type methods, exist(), nodes(), query(), and value(), or in IS NULL and IS NOT NULL checks.    |
|    526    |    16    |    No    |    %ls of XML types constrained by different XML schema collections and/or DOCUMENT/CONTENT option is not allowed. Use the CONVERT function to run this query.    |
|    527    |    16    |    No    |    Implicit conversion between XML types constrained by different XML schema collections is not allowed. Use the CONVERT function to run this query.    |
|    529    |    16    |    No    |    Explicit conversion from data type %ls to %ls is not allowed.    |
|    530    |    16    |    No    |    The statement terminated. The maximum recursion %d has been exhausted before statement completion.    |
|    531    |    10    |    No    |    Cannot set NOCOUNT to OFF inside the trigger execution because the server option "disallow_results_from_triggers" is true or we are inside LOGON trigger execution.    |
|    532    |    16    |    No    |    The timestamp (changed to %S_TS) shows that the row has been updated by another user.    |
|    533    |    10    |    No    |    Cannot set XACT ABORT to OFF inside the trigger execution unless the database compatibility is 90.    |
|    534    |    16    |    No    |    '%.*ls' failed because it is not supported in the edition of this SQL Server instance '%.*ls'. See books online for more details on feature support in different SQL Server editions.    |
|    535    |    16    |    No    |    The datediff function resulted in an overflow. The number of dateparts separating two date/time instances is too large. Try to use datediff with a less precise datepart.    |
|    536    |    16    |    No    |    Invalid length parameter passed to the %ls function.    |
|    537    |    16    |    No    |    Invalid length parameter passed to the LEFT or SUBSTRING function.    |
|    539    |    16    |    No    |    Schema changed after the target table was created. Rerun the Select Into query.    |
|    540    |    16    |    Yes    |    There is insufficient system memory to run RAISERROR.    |
|    541    |    16    |    No    |    There is not enough stack to execute the statement    |
|    542    |    16    |    No    |    An invalid datetime value was encountered. Value exceeds the year 9999.    |
|    543    |    16    |    No    |    Creation of a return table failed for the table valued function '%.*ls'.    |
|    544    |    16    |    No    |    Cannot insert explicit value for identity column in table '%.*ls' when IDENTITY_INSERT is set to OFF.    |
|    545    |    16    |    No    |    Explicit value must be specified for identity column in table '%.*ls' either when IDENTITY_INSERT is set to ON or when a replication user is inserting into a NOT FOR REPLICATION identity column.    |
|    547    |    16    |    No    |    The %ls statement conflicted with the %ls constraint "%.*ls". The conflict occurred in database "%.*ls", table "%.*ls"%ls%.*ls%ls.    |
|    548    |    16    |    No    |    The insert failed. It conflicted with an identity range check constraint in database '%.*ls', replicated table '%.*ls'%ls%.*ls%ls. If the identity column is automatically managed by replication, update the range as follows: for the Publisher, execute sp_adjustpublisheridentityrange; for the Subscriber, run the Distribution Agent or the Merge Agent.    |
|    549    |    16    |    No    |    The collation '%.*ls' of receiving variable is not equal to the collation '%.*ls' of column '%.*ls'.    |
|    550    |    16    |    No    |    The attempted insert or update failed because the target view either specifies WITH CHECK OPTION or spans a view that specifies WITH CHECK OPTION and one or more rows resulting from the operation did not qualify under the CHECK OPTION constraint.    |
|    552    |    16    |    No    |    CryptoAPI function '%ls' failed. Error 0x%x: %ls    |
|    555    |    16    |    No    |    User-defined functions are not yet enabled.    |
|    556    |    16    |    No    |    INSERT EXEC failed because the stored procedure altered the schema of the target table.    |
|    557    |    16    |    No    |    Only functions and some extended stored procedures can be executed from within a function.    |
|    558    |    16    |    No    |    Remote function calls are not allowed within a function.    |
|    561    |    16    |    No    |    Failed to access file '%.*ls'    |
|    562    |    16    |    No    |    Failed to access file '%.*ls'. Files can be accessed only through shares    |
|    563    |    14    |    No    |    The transaction for the INSERT EXEC statement has been rolled back. The INSERT EXEC operation will be terminated.    |
|    564    |    16    |    No    |    Attempted to create a record with a fixed length of '%d'. Maximum allowable fixed length is '%d'.    |
|    565    |    18    |    No    |    A stack overflow occurred in the server while compiling the query. Please simplify the query.    |
|    566    |    21    |    Yes    |    An error occurred while writing an audit trace. SQL Server is shutting down. Check and correct error conditions such as insufficient disk space, and then restart SQL Server. If the problem persists, disable auditing by starting the server at the command prompt with the "-f" switch, and using SP_CONFIGURE.    |
|    567    |    16    |    No    |    File '%.*ls' either does not exist or is not a recognizable trace file. Or there was an error opening the file.    |
|    568    |    16    |    No    |    Encountered an error or an unexpected end of trace file '%.*ls'.    |
|    569    |    16    |    No    |    The handle that was passed to %ls was invalid.    |
|    570    |    15    |    No    |    INSTEAD OF triggers do not support direct recursion. The trigger execution failed.    |
|    571    |    16    |    No    |    The specified attribute value for %ls is invalid.    |
|    572    |    16    |    No    |    Invalid regular expression "%.*ls" near the offset %d.    |
|    573    |    16    |    No    |    Evaluation of the regular expression is too complex: '%.*ls'.    |
|    574    |    16    |    No    |    %ls statement cannot be used inside a user transaction.    |
|    575    |    16    |    No    |    A LOGON trigger returned a resultset. Modify the LOGON trigger to not return resultsets.    |
|    576    |    16    |    No    |    Cannot create a row that has sparse data of size %d which is greater than the allowable maximum sparse data size of %d.    |
|    577    |    16    |    No    |    The value provided for the timeout is not valid. Timeout must be a valid integer between 0 and 2147483647.    |
|    578    |    16    |    No    |    Insert Exec not allowed in WAITFOR queries.    |
|    579    |    16    |    No    |    Cannot execute WAITFOR query with snapshot isolation level.    |
|    582    |    16    |    No    |    Offset is greater than the length of the column to be updated in write.    |
|    583    |    16    |    No    |    Negative offset or length in write.    |
|    584    |    16    |    No    |    Select Into not allowed in WAITFOR queries.    |
|    585    |    16    |    No    |    Changing database context is not allowed when populating the resource database.    |
|    587    |    16    |    No    |    An invalid delayed CLR type fetch token is provided.    |
|    588    |    16    |    No    |    Multiple tasks within the session are using the same delayed CLR type fetch token at the same time.    |
|    589    |    16    |    No    |    This statement has attempted to access data whose access is restricted by the assembly.    |
|    590    |    16    |    No    |    RPC was aborted without execution.    |
|    591    |    16    |    No    |    %ls: The formal parameter "%ls" was defined as OUTPUT, but the actual parameter was not declared as OUTPUT.    |
|    592    |    16    |    No    |    Cannot find %S_MSG ID %d in database ID %d.    |
|    593    |    10    |    No    |    fn_trace_gettable: XML conversion of trace data for event 165 failed.    |
|    594    |    10    |    No    |    fn_trace_gettable: XML conversion of trace data is not supported in fiber mode.    |
|    595    |    16    |    No    |    Bulk Insert with another outstanding result set should be run with XACT_ABORT on.    |
|    596    |    16    |    No    |    Cannot continue the execution because the session is in the kill state.    |
|    597    |    16    |    No    |    The execution of in-proc data access is being terminated due to errors in the User Datagram Protocol (UDP).    |
|    598    |    16    |    No    |    An error occurred while executing CREATE/ALTER DB. Please look at the previous error for more information.    |
|    599    |    16    |    No    |    %.*ls: The length of the result exceeds the length limit (2GB) of the target large type.    |
|    [601](../../relational-databases/errors-events/mssqlserver-601-database-engine-error.md)    |    12    |    No    |    Could not continue scan with NOLOCK due to data movement.    |
|    602    |    21    |    Yes    |    Could not find an entry for table or index with partition ID %I64d in database %d. This error can occur if a stored procedure references a dropped table, or metadata is corrupted. Drop and re-create the stored procedure, or execute DBCC CHECKDB.    |
|    603    |    21    |    Yes    |    Could not find an entry for table or index with object ID %d (partition ID %I64d) in database %d. This error can occur if a stored procedure references a dropped table, or metadata is corrupted. Drop and re-create the stored procedure, or execute DBCC CHECKDB.    |
|    [605](../../relational-databases/errors-events/mssqlserver-605-database-engine-error.md)    |    21    |    Yes    |    Attempt to fetch logical page %S_PGID in database %d failed. It belongs to allocation unit %I64d not to %I64d.    |
|    606    |    21    |    Yes    |    Metadata inconsistency. Filegroup id %ld specified for table '%.*ls' does not exist. Run DBCC CHECKDB or CHECKCATALOG.    |
|    608    |    16    |    Yes    |    No catalog entry found for partition ID %I64d in database %d. The metadata is inconsistent. Run DBCC CHECKDB to check for a metadata corruption.    |
|    609    |    16    |    No    |    B+ tree is not empty when waking up on RowsetBulk.    |
|    610    |    16    |    Yes    |    Invalid header value from a page. Run DBCC CHECKDB to check for a data corruption.    |
|    [611](../../relational-databases/errors-events/mssqlserver-611-database-engine-error.md)    |    16    |    No    |    Cannot insert or update a row because total variable column size, including overhead, is %d bytes more than the limit.    |
|    613    |    21    |    No    |    Could not find an entry for worktable rowset with partition ID %I64d in database %d.    |
|    615    |    21    |    Yes    |    Could not find database ID %d, name '%.*ls'. The database may be offline. Wait a few minutes and try again.    |
|    [617](../../relational-databases/errors-events/mssqlserver-617-database-engine-error.md)    |    20    |    Yes    |    Descriptor for object ID %ld in database ID %d not found in the hash table during attempt to unhash it. A work table is missing an entry. Rerun the query. If a cursor is involved, close and reopen the cursor.    |
|    622    |    16    |    No    |    The filegroup "%.*ls" has no files assigned to it. Tables, indexes, text columns, ntext columns, and image columns cannot be populated on this filegroup until a file is added.    |
|    627    |    16    |    No    |    Cannot use SAVE TRANSACTION within a distributed transaction.    |
|    628    |    16    |    No    |    Cannot issue SAVE TRANSACTION when there is no active transaction.    |
|    650    |    16    |    No    |    You can only specify the READPAST lock in the READ COMMITTED or REPEATABLE READ isolation levels.    |
|    651    |    16    |    No    |    Cannot use the %ls granularity hint on the table "%.*ls" because locking at the specified granularity is inhibited.    |
|    652    |    16    |    No    |    The index "%.*ls" for table "%.*ls" (RowsetId %I64d) resides on a read-only filegroup ("%.*ls"), which cannot be modified.    |
|    666    |    16    |    No    |    The maximum system-generated unique value for a duplicate group was exceeded for index with partition ID %I64d. Dropping and re-creating the index may resolve this; otherwise, use another clustering key.    |
|    667    |    16    |    No    |    The index "%.*ls" for table "%.*ls" (RowsetId %I64d) resides on a filegroup ("%.*ls") that cannot be accessed because it is offline, is being restored, or is defunct.    |
|    669    |    22    |    No    |    The row object is inconsistent. Please rerun the query.    |
|    670    |    16    |    No    |    Large object (LOB) data for table "%.*ls" resides on an offline filegroup ("%.*ls") that cannot be accessed.    |
|    671    |    16    |    No    |    Large object (LOB) data for table "%.*ls" resides on a read-only filegroup ("%.*ls"), which cannot be modified.    |
|    672    |    10    |    No    |    Failed to queue cleanup packets for orphaned rowsets in database "%.*ls". Some disk space may be wasted. Cleanup will be attempted again on database restart.    |
|    674    |    10    |    Yes    |    Exception occurred in destructor of RowsetNewSS 0x%p. This error may indicate a problem related to releasing pre-allocated disk blocks used during bulk-insert operations. Restart the server to resolve this problem.    |
|    675    |    10    |    Yes    |    Worktable with partition ID %I64d was dropped successfully after repeated attempts.    |
|    676    |    10    |    Yes    |    Error occurred while attempting to drop worktable with partition ID %I64d.    |
|    677    |    10    |    Yes    |    Unable to drop worktable with partition ID %I64d after repeated attempts. Worktable is marked for deferred drop. This is an informational message only. No user action is required.    |
|    678    |    10    |    Yes    |    Active rowset for partition ID %I64d found at the end of the batch. This error may indicate incorrect exception handling. Use the current activity window in SQL Server Management Studio or the Transact-SQL KILL statement to terminate the server process identifier (SPID) responsible for generating the error.    |
|    679    |    16    |    No    |    One of the partitions of index '%.*ls' for table '%.*ls'(partition ID %I64d) resides on a filegroup ("%.*ls") that cannot be accessed because it is offline, restoring, or defunct. This may limit the query result.    |
|    680    |    10    |    Yes    |    Error [%d, %d, %d] occurred while attempting to drop allocation unit ID %I64d belonging to worktable with partition ID %I64d.    |
|    681    |    16    |    No    |    Attempting to set a non-NULL-able column's value to NULL.    |
|    682    |    16    |    No    |    Internal error. Buffer provided to read column value is too small. Run DBCC CHECKDB to check for any corruption.    |
|    683    |    22    |    No    |    An internal error occurred while trying to convert between variable-length and fixed-length decimal formats. Run DBCC CHECKDB to check for any database corruption.    |
|    684    |    22    |    No    |    An internal error occurred while attempting to convert between compressed and uncompressed storage formats. Run DBCC CHECKDB to check for any corruption.    |
|    685    |    22    |    No    |    An internal error occurred while attempting to retrieve a backpointer for a heap forwarded record.    |
|    [701](../../relational-databases/errors-events/mssqlserver-701-database-engine-error.md)    |    19    |    Yes    |    There is insufficient system memory in resource pool '%ls' to run this query.    |
|    708    |    10    |    Yes    |    Server is running low on virtual address space or machine is running low on virtual memory. Reserved memory used %d times since startup. Cancel query and re-run, decrease server load, or cancel other applications.    |
|    801    |    20    |    Yes    |    A buffer was encountered with an unexpected status of 0x%x.    |
|    [802](../../relational-databases/errors-events/mssqlserver-802-database-engine-error.md)    |    17    |    No    |    There is insufficient memory available in the buffer pool.    |
|    803    |    10    |    Yes    |    simulated failure (DEBUG only)    |
|    805    |    10    |    Yes    |    restore pending    |
|    806    |    10    |    Yes    |    audit failure (a page read from disk failed to pass basic integrity checks)    |
|    807    |    10    |    Yes    |    (no disk or the wrong disk is in the drive)    |
|    808    |    10    |    Yes    |    insufficient bytes transferred    |
|    821    |    20    |    Yes    |    Could not unhash buffer at 0x%p with a buffer page number of %S_PGID and database ID %d with HASHED status set. The buffer was not found. %S_PAGE. Contact Technical Support.    |
|    822    |    21    |    Yes    |    Could not start I/O operation for request %S_BLKIOPTR. Contact Technical Support.    |
|    [823](../../relational-databases/errors-events/mssqlserver-823-database-engine-error.md)    |    24    |    Yes    |    The operating system returned error %ls to SQL Server during a %S_MSG at offset %#016I64x in file '%ls'. Additional messages in the SQL Server error log and system event log may provide more detail. This is a severe system-level error condition that threatens database integrity and must be corrected immediately. Complete a full database consistency check (DBCC CHECKDB). This error can be caused by many factors; for more information, see SQL Server Books Online.    |
|    [824](../../relational-databases/errors-events/mssqlserver-824-database-engine-error.md)    |    24    |    Yes    |    SQL Server detected a logical consistency-based I/O error: %ls. It occurred during a %S_MSG of page %S_PGID in database ID %d at offset %#016I64x in file '%ls'. Additional messages in the SQL Server error log or system event log may provide more detail. This is a severe error condition that threatens database integrity and must be corrected immediately. Complete a full database consistency check (DBCC CHECKDB). This error can be caused by many factors; for more information, see SQL Server Books Online.    |
|    [825](../../relational-databases/errors-events/mssqlserver-825-database-engine-error.md)    |    10    |    Yes    |    A read of the file '%ls' at offset %#016I64x succeeded after failing %d time(s) with error: %ls. Additional messages in the SQL Server error log and system event log may provide more detail. This error condition threatens database integrity and must be corrected. Complete a full database consistency check (DBCC CHECKDB). This error can be caused by many factors; for more information, see SQL Server Books Online.    |
|    826    |    10    |    Yes    |    incorrect pageid (expected %d:%d; actual %d:%d)    |
|    829    |    21    |    Yes    |    Database ID %d, Page %S_PGID is marked RestorePending, which may indicate disk corruption. To recover from this state, perform a restore.    |
|    830    |    10    |    No    |    stale page (a page read returned a log sequence number (LSN) (%u:%u:%u) that is older than the last one that was written (%u:%u:%u))    |
|    831    |    20    |    No    |    Unable to deallocate a kept page.    |
|    [832](../../relational-databases/errors-events/mssqlserver-832-database-engine-error.md)    |    24    |    Yes    |    A page that should have been constant has changed (expected checksum: %08x, actual checksum: %08x, database %d, file '%ls', page %S_PGID). This usually indicates a memory failure or other hardware or OS corruption.    |
|    [833](../../relational-databases/errors-events/mssqlserver-833-database-engine-error.md)    |    10    |    No    |    SQL Server has encountered %d occurrence(s) of I/O requests taking longer than %d seconds to complete on file '%ls' in database '%ls' (%d). The OS file handle is 0x%p. The offset of the latest long I/O is: %#016I64x    |
|    [844](../../relational-databases/errors-events/mssqlserver-844-database-engine-error.md)    |    10    |    No    |    Time out occurred while waiting for buffer latch -- type %d, bp %p, page %d:%d, stat %#x, database id: %d, allocation unit id: %I64d%ls, task 0x%p : %d, waittime %d, flags 0x%I64x, owning task 0x%p. Continuing to wait.    |
|    [845](../../relational-databases/errors-events/mssqlserver-845-database-engine-error.md)    |    17    |    No    |    Time-out occurred while waiting for buffer latch type %d for page %S_PGID, database ID %d.    |
|    [846](../../relational-databases/errors-events/mssqlserver-846-database-engine-error.md)    |    10    |    No    |    A time-out occurred while waiting for buffer latch -- type %d, bp %p, page %d:%d, stat %#x, database id: %d, allocation unit Id: %I64d%ls, task 0x%p : %d, waittime %d, flags 0x%I64x, owning task 0x%p. Not continuing to wait.    |
|    [847](../../relational-databases/errors-events/mssqlserver-847-database-engine-error.md)    |    10    |    Yes    |    Timeout occurred while waiting for latch: class '%ls', id %p, type %d, Task 0x%p : %d, waittime %d, flags 0x%I64x, owning task 0x%p. Continuing to wait.    |
|    848    |    10    |    Yes    |    Using large pages for buffer pool.    |
|    849    |    10    |    Yes    |    Using locked pages for buffer pool.    |
|    850    |    10    |    Yes    |    %I64u MB of large page memory allocated.    |
|    851    |    10    |    No    |    the page is in an OFFLINE file which cannot be read    |
|    [854](../../relational-databases/errors-events/mssqlserver-854-database-engine-error.md)    |    10    |    Yes    |    Machine supports memory error recovery. SQL memory protection is enabled to recover from memory corruption.    |
|    [855](../../relational-databases/errors-events/mssqlserver-855-database-engine-error.md)    |    10    |    Yes    |    Uncorrectable hardware memory corruption detected. Your system may become unstable. Check the Windows event log for more details.    |
|    [856](../../relational-databases/errors-events/mssqlserver-856-database-engine-error.md)    |    10    |    Yes    |    SQL Server has detected hardware memory corruption in database '%ls', file ID: %u, page ID; %u, memory address: 0x%I64x and has successfully recovered the page.    |
|    902    |    16    |    No    |    To change the %ls, the database must be in state in which a checkpoint can be executed.    |
|    904    |    16    |    No    |    Database %ld cannot be autostarted during server shutdown or startup.    |
|    [905](../../relational-databases/errors-events/mssqlserver-905-database-engine-error.md)    |    21    |    Yes    |    Database '%.*ls' cannot be started in this edition of SQL Server because it contains a partition function '%.*ls'. Only Enterprise edition of SQL Server supports partitioning.    |
|    907    |    16    |    No    |    The database "%ls" has inconsistent database or file metadata.    |
|    908    |    10    |    Yes    |    Filegroup %ls in database %ls is unavailable because it is %ls. Restore or alter the filegroup to be available.    |
|    909    |    21    |    Yes    |    Database '%.*ls' cannot be started in this edition of SQL Server because part or all of object '%.*ls' is enabled with data compression or vardecimal storage format. Data compression and vardecimal storage format are only supported on SQL Server Enterprise Edition.    |
|    910    |    10    |    No    |    Database '%.*ls' is upgrading script '%.*ls' from level %d to level %d.    |
|    911    |    16    |    No    |    Database '%.*ls' does not exist. Make sure that the name is entered correctly.    |
|    912    |    16    |    No    |    Script level upgrade for database '%.*ls' failed because upgrade step '%.*ls' encountered error %d, state %d, severity %d. This is a serious error condition which might interfere with regular operation and the database will be taken offline. If the error happened during upgrade of the 'master' database, it will prevent the entire SQL Server instance from starting. Examine the previous errorlog entries for errors, take the appropriate corrective actions and re-start the database so that the script upgrade steps run to completion.    |
|    913    |    22    |    Yes    |    Could not find database ID %d. Database may not be activated yet or may be in transition. Reissue the query once the database is available. If you do not think this error is due to a database that is transitioning its state and this error continues to occur, contact your primary support provider. Please have available for review the Microsoft SQL Server error log and any additional information relevant to the circumstances when the error occurred.    |
|    914    |    21    |    No    |    Script level upgrade for database '%.*ls' failed because upgrade step '%.*ls' was aborted before completion. If the abort happened during upgrade of the 'master' database, it will prevent the entire SQL Server instance from starting. Examine the previous errorlog entries for errors, take the appropriate corrective actions and re-start the database so that the script upgrade steps run to completion.    |
|    915    |    21    |    No    |    Unable to obtain the current script level for database '%.*ls'. If the error happened during startup of the 'master' database, it will prevent the entire SQL Server instance from starting. Examine the previous errorlog entries for errors, take the appropriate corrective actions and re-start the database so that script upgrade may run to completion.    |
|    [916](../../relational-databases/errors-events/mssqlserver-916-database-engine-error.md)    |    14    |    No    |    The server principal "%.*ls" is not able to access the database "%.*ls" under the current security context.    |
|    917    |    21    |    No    |    An upgrade script batch failed to execute for database '%.*ls' due to compilation error. Check the previous error message for the line which caused compilation to fail.    |
|    918    |    21    |    No    |    Failed to load the engine script metadata from script DLL '%.*ls'. The error code reported by Windows was %d. This is a serious error condition, which usually indicates a corrupt or incomplete installation. Repairing the SQL Server instance may help resolve this error.    |
|    919    |    10    |    No    |    User '%.*ls' is changing database script level entry %d to a value of %d.    |
|    920    |    20    |    No    |    Only members of the sysadmin role can modify the database script level.    |
|    921    |    14    |    No    |    Database '%.*ls' has not been recovered yet. Wait and try again.    |
|    922    |    14    |    No    |    Database '%.*ls' is being recovered. Waiting until recovery is finished.    |
|    923    |    14    |    No    |    Database '%.*ls' is in restricted mode. Only the database owner and members of the dbcreator and sysadmin roles can access it.    |
|    924    |    14    |    No    |    Database '%.*ls' is already open and can only have one user at a time.    |
|    925    |    19    |    Yes    |    Maximum number of databases used for each query has been exceeded. The maximum allowed is %d.    |
|    [926](../../relational-databases/errors-events/mssqlserver-926-database-engine-error.md)    |    14    |    No    |    Database '%.*ls' cannot be opened. It has been marked SUSPECT by recovery. See the SQL Server errorlog for more information.    |
|    927    |    14    |    No    |    Database '%.*ls' cannot be opened. It is in the middle of a restore.    |
|    928    |    20    |    Yes    |    During upgrade, database raised exception %d, severity %d, state %d, address %p. Use the exception number to determine the cause.    |
|    929    |    20    |    Yes    |    Unable to close a database that is not currently open. The application should reconnect and try again. If this action does not correct the problem, contact your primary support provider.    |
|    930    |    21    |    Yes    |    Attempting to reference recovery unit %d in database '%ls' which does not exist. Contact Technical Support.    |
|    931    |    21    |    Yes    |    Attempting to reference database fragment %d in database '%ls' which does not exist. Contact Technical Support.    |
|    932    |    21    |    Yes    |    SQL Server cannot load database '%.*ls' because change tracking is enabled. The currently installed edition of SQL Server does not support change tracking. Either disable change tracking in the database by using a supported edition of SQL Server, or upgrade the instance to one that supports change tracking.    |
|    933    |    21    |    Yes    |    Database '%.*ls' cannot be started because some of the database functionality is not available in the current edition of SQL Server.    |
|    934    |    21    |    Yes    |    SQL Server cannot load database '%.*ls' because Change Data Capture is enabled. The currently installed edition of SQL Server does not support Change Data Capture. Either disable Change Data Capture in the database by using a supported edition of SQL Server, or upgrade the instance to one that supports Change Data Capture.    |
|    935    |    21    |    Yes    |    The script level for '%.*ls' in database '%.*ls' cannot be downgraded from %d to %d, which is supported by this server. This usually implies that a future database was attached and the downgrade path is not supported by the current installation. Install a newer version of SQL Server and re-try opening the database.    |
|    942    |    14    |    No    |    Database '%.*ls' cannot be opened because it is offline.    |
|    943    |    14    |    No    |    Database '%.*ls' cannot be opened because its version (%d) is later than the current server version (%d).    |
|    944    |    10    |    No    |    Converting database '%.*ls' from version %d to the current version %d.    |
|    [945](../../relational-databases/errors-events/mssqlserver-945-database-engine-error.md)    |    16    |    No    |    Database '%.*ls' cannot be opened due to inaccessible files or insufficient memory or disk space. See the SQL Server errorlog for details.    |
|    946    |    14    |    No    |    Cannot open database '%.*ls' version %d. Upgrade the database to the latest version.    |
|    947    |    16    |    Yes    |    Error while closing database '%.*ls'. Check for previous additional errors and retry the operation.    |
|    [948](../../relational-databases/errors-events/mssqlserver-948-database-engine-error.md)    |    20    |    Yes    |    The database '%.*ls' cannot be opened because it is version %d. This server supports version %d and earlier. A downgrade path is not supported.    |
|    949    |    16    |    No    |    tempdb is skipped. You cannot run a query that requires tempdb    |
|    950    |    20    |    Yes    |    Database '%.*ls' cannot be upgraded because its non-release version (%d) is not supported by this version of SQL Server. You cannot open a database that is incompatible with this version of sqlservr.exe. You must re-create the database.    |
|    951    |    10    |    No    |    Database '%.*ls' running the upgrade step from version %d to version %d.    |
|    952    |    16    |    No    |    Database '%.*ls' is in transition. Try the statement later.    |
|    954    |    14    |    No    |    The database "%.*ls" cannot be opened. It is acting as a mirror database.    |
|    955    |    14    |    No    |    Database %.*ls is enabled for Database Mirroring, but the database lacks quorum: the database cannot be opened. Check the partner and witness connections if configured.    |
|    956    |    14    |    No    |    Database %.*ls is enabled for Database Mirroring, but has not yet synchronized with its partner. Try the operation again later.    |
|    957    |    17    |    No    |    Database '%.*ls' is enabled for Database Mirroring, The name of the database may not be changed.    |
|    958    |    10    |    Yes    |    The resource database build version is %.*ls. This is an informational message only. No user action is required.    |
|    959    |    20    |    Yes    |    The resource database version is %d and this server supports version %d. Restore the correct version or reinstall SQL Server.    |
|    960    |    10    |    No    |    Warning: User "sys" (principal_id = %d) in database "%.*ls" has been renamed to "%.*ls". "sys" is a reserved user or schema name in this version of SQL Server.    |
|    961    |    10    |    No    |    Warning: Index "%.*ls" (index_id = %d) on object ID %d in database "%.*ls" was renamed to "%.*ls" because its name is a duplicate of another index on the same object.    |
|    962    |    10    |    No    |    Warning: Primary key or Unique constraint "%.*ls" (object_id = %d) in database "%.*ls" was renamed to "%.*ls" because its index was renamed.    |
|    963    |    10    |    No    |    Warning: Database "%.*ls" was marked suspect because of actions taken during upgrade. See errorlog or eventlog for more information. Use ALTER DATABASE to bring the database online. The database will come online in restricted_user state.    |
|    964    |    10    |    No    |    Warning: System user '%.*ls' was found missing from database '%.*ls' and has been restored. This user is required for SQL Server operation.    |
|    965    |    10    |    No    |    Warning: A column nullability inconsistency was detected in the metadata of index "%.*ls" (index_id = %d) on object ID %d in database "%.*ls". The index may be corrupt. Run DBCC CHECKTABLE to verify consistency.    |
|    966    |    10    |    No    |    Warning: Assembly "%.*ls" in database "%.*ls" has been renamed to "%.*ls" because the name of the assembly conflicts with a system assembly in this version of SQL Server.    |
|    967    |    10    |    No    |    Warning: The index "%.*ls" on "%.*ls"."%.*ls" is disabled because the XML data bound to it may contain negative values for xs:date and xs:dateTime which are not longer supported.    |
|    968    |    10    |    No    |    Warning: The XML facet on type "%.*ls" in schema collection "%.*ls" is updated from "%.*ls" to "%.*ls" because Sql Server does not support negative years inside values of type xs:date or xs:dateTime.    |
|    969    |    10    |    No    |    Warning: The default or fixed value on XML element or attribute "%.*ls" in schema collection "%.*ls" is updated from "%.*ls" to "%.*ls" because Sql Server does not support negative years inside values of type xs:date or xs:dateTime.    |
|    970    |    10    |    No    |    Warning: The XML instances in the XML column "%.*ls.%.*ls.%.*ls" may contain negative simple type values of type xs:date or xs:dateTime. It will be impossible to run XQuery or build a primary XML index on these XML instances.    |
|    971    |    10    |    No    |    The resource database has been detected in two different locations. Attaching the resource database in the same directory as sqlservr.exe at '%.*ls' instead of the currently attached resource database at '%.*ls'.    |
|    972    |    17    |    No    |    Could not use database '%d' during procedure execution.    |
|    973    |    10    |    Yes    |    Database %ls was started . However, FILESTREAM is not compatible with the READ_COMMITTED_SNAPSHOT and ALLOW_SNAPSHOT_ISOLATION options. Either remove the FILESTREAM files and the FILESTREAM filegroups, or set READ_COMMITTED_SNAPSHOT and ALLOW_SNAPSHOT_ISOLATION to OFF.    |
|974 | 10  | No  |  Attaching the resource database in the same directory as sqlservr.exe at '%.*ls' failed as the database files do not exist.|
|975 | 10  | Yes |  System objects could not be updated in database '%.*ls' because it is read-only. |
|976 | 14  | No  |  The target database, '%.*ls', is participating in an availability group and is currently not accessible for queries. Either data movement is suspended or the availability replica is not enabled for read access. To allow read-only access to this and other d |
|977 | 10 |  No  |  Warning: Could not find associated index for the constraint '%.*ls' on object_id '%d' in database '%.*ls'.|
|978 | 14 |  No  |  The target database ('%.*ls') is in an availability group and is currently accessible for connections when the application intent is set to read only. For more information about application intent, see SQL Server Books Online. |
|979  | 14 | No  |  The target database ('%.*ls') is in an availability group and currently does not allow read only connections. For more information about application intent, see SQL Server Books Online.|
|980 |  21 |  Yes |  SQL Server cannot load database '%.*ls' because it contains a columnstore index. The currently installed edition of SQL Server does not support columnstore indexes. Either disable the columnstore index in the database by using a supported edition of SQL Se|
|981  |  10 | No | Database manager will be using %d target database version. |
|982  |  14 | No | Unable to access the '%.*ls' database because no online secondary replicas are enabled for read-only access. Check the availability group configuration to verify that at least one secondary replica is configured for read-only access. Wait for an enabled re|
|983 |  14  | No | Unable to access availability database '%.*ls' because the database replica is not in the PRIMARY or SECONDARY role. Connections to an availability database is permitted only when the database replica is in the PRIMARY or SECONDARY role. Try the operation |
|984 | 21  | Yes | Failed to perform a versioned copy of sqlscriptdowngrade.dll from Binn to Binn\Cache folder. VerInstallFile API failed with error code %d.|
|985 |  10 | Yes  |      Successfully installed the file '%ls' into folder '%ls'. |
|986 |  10 | No   |     Couldn't get a clean bootpage for database  '%.*ls' after %d tries. This is an informational message only. No user action is required. |
|987 |  23  |    Yes | A duplicate key insert was hit when updating system objects in database '%.*ls'.|
|988 |  14  |    No  | Unable to access database '%.*ls' because it lacks a quorum of nodes for high availability. Try the operation again later.|
|989 |  16  |    No  | Failed to take the host database with ID %d offline when one or more of its partition databases is marked as suspect.|
|990 |  16  |    No  | Taking the host database with ID %d offline because one or more of its partition databases is marked as suspect.|
|991 |  16  |    No  | Failed to take the host database '%.*ls' offline when one or more of its partition databases is marked as suspect.|
|992 |  16  |    No  | Failed to get the shared lock on database '%.*ls'.|
|993 |  10  |    No  | Redo for database '%.*ls' applied version upgrade step from %d to %d.|
|994 |  10  |    No  | Warning: Index "%.*ls" on "%.*ls"."%.*ls" is disabled because it contains a computed column.|
|995 |  10  |    No  | Warning: Index "%.*ls" on "%.*ls"."%.*ls" is disabled. It cannot be upgraded as it resides on a read-only filegroup.|
|996 |  10  |    No  | Warning: Index "%.*ls" on "%.*ls"."%.*ls" is disabled. This columnstore index cannot be upgraded, likely because it exceeds the row size limit of '%d' bytes.|
