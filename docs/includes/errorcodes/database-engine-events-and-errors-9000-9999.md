---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/19/2022
ms.topic: include
---
| Error| Severity | Event Logged | Description|
| :------ | :------| :------| :----------------------------- |
|    [9001](../../relational-databases/errors-events/mssqlserver-9001-database-engine-error.md)    |    10    |    Yes    |    The log for database '%.*ls' is not available. Check the event log for related error messages. Resolve any errors and restart the database.    |
|    [9002](../../relational-databases/errors-events/mssqlserver-9002-database-engine-error.md)    |    17    |    Yes    |    The transaction log for database '%.*ls' is full. To find out why space in the log cannot be reused, see the log_reuse_wait_desc column in sys.databases    |
|    [9003](../../relational-databases/errors-events/mssqlserver-9003-database-engine-error.md)    |    20    |    Yes    |    The log scan number %S_LSN passed to log scan in database '%.*ls' is not valid. This error may indicate data corruption or that the log file (.ldf) does not match the data file (.mdf). If this error occurred during replication, re-create the publication. Otherwise, restore from backup if the problem results in a failure during startup.    |
|    [9004](../../relational-databases/errors-events/mssqlserver-9004-database-engine-error.md)    |    21    |    Yes    |    An error occurred while processing the log for database '%.*ls'. If possible, restore from backup. If a backup is not available, it might be necessary to rebuild the log.    |
|    9005    |    16    |    No    |    Either start LSN or end LSN specified in OpenRowset(DBLog, ...) is invalid.    |
|    9006    |    10    |    No    |    Cannot shrink log file %d (%s) because total number of logical log files cannot be fewer than %d.    |
|    9007    |    10    |    No    |    Cannot shrink log file %d (%s) because requested size (%dKB) is larger than the start of the last logical log file.    |
|    9008    |    10    |    No    |    Cannot shrink log file %d (%s) because the logical log file located at the end of the file is in use.    |
|    9009    |    10    |    No    |    Cannot shrink log file %d (%s) because of minimum log space required.    |
|    9010    |    14    |    No    |    User does not have permission to query the virtual table, DBLog. Only members of the sysadmin fixed server role and the db_owner fixed database role have this permission    |
|    9011    |    14    |    No    |    User does not have permission to query backup files with the virtual table DBLog. Only members of the sysadmin fixed server role has this permission    |
|    9012    |    10    |    No    |    There have been %d misaligned log IOs which required falling back to synchronous IO. The current IO is on file %ls.    |
|    9013    |    10    |    No    |    The tail of the log for database %ls is being rewritten to match the new sector size of %d bytes. %d bytes at offset %I64d in file %ls will be written.    |
|    9014    |    21    |    Yes    |    An error occurred while processing the log for database '%.*ls'. THe log block version is higher than this server allows.    |
|    9015    |    24    |    Yes    |    The log record at LSN %S_LSN is corrupted.    |
|    9016    |    21    |    Yes    |    An error occurred while processing the log for database '%.*ls'. The log block could not be decrypted.    |
|    [9017](../../relational-databases/errors-events/mssqlserver-9017-database-engine-error.md)    |    10    |    No    |    Database %ls has more than %d virtual log files which is excessive. Too many virtual log files can cause long startup and backup times. Consider shrinking the log and using a different growth increment to reduce the number of virtual log files.  |
|    9100    |    23    |    Yes    |    Possible index corruption detected. Run DBCC CHECKDB.    |
|    9101    |    16    |    No    |    auto statistics internal    |
|    9104    |    16    |    No    |    auto statistics internal    |
|    9105    |    16    |    No    |    The provided statistics stream is corrupt.    |
|    9106    |    16    |    No    |    Histogram support not allowed for input data type 0x%08x.    |
|    9201    |    10    |    Yes    |    %d active query notification subscription(s) in database '%.*ls' owned by security identification number '%.*ls' were dropped.    |
|    9202    |    16    |    No    |    The query notification subscription message is invalid.    |
|    9204    |    16    |    No    |    The query notification subscription timeout is invalid. The allowed range is 1 through 2147483647.    |
|    9205    |    16    |    No    |    User "%.*ls" does not have permission to request query notification subscriptions on database "%.*ls".    |
|    9206    |    16    |    No    |    The query notification subscription "%ld" cannot be deleted because it does not exist or it has already been fired.    |
|    9207    |    10    |    No    |    The query notification dialog on conversation handle '%.*ls' closed due to the following error: '%.*ls'.    |
|    9208    |    16    |    No    |    Query notification subscription could not get dialog endpoint. Could not open service broker dialog for service name '%.*ls' of broker instance '%.*ls'.    |
|    9209    |    16    |    No    |    Query notification subscription could not begin dialog with service name '%.*ls' of broker instance '%.*ls'.    |
|    9210    |    10    |    No    |    Query notification delivery could not send message on dialog '%.*ls'. Delivery failed for notification '%.*ls' because of the following error in service broker: '%.*ls'.    |
|    9211    |    10    |    No    |    Failed to check for pending query notifications in database "%d" because of the following error when opening the database: '%.*ls'.    |
|    9213    |    10    |    No    |    Query notification subscription could not access database with id %d. Could not open broker dialog for service name '%.*ls' of broker instance '%.*ls'.    |
|    9214    |    16    |    No    |    The query notification delivery failed to decode the error message from the Service Broker.    |
|    9215    |    16    |    No    |    Query notification delivery failed to encode message. Delivery failed for notification '%.*ls'.    |
|    9216    |    10    |    No    |    Failed to delete the expired query notification subscription "%d".    |
|    9217    |    10    |    No    |    Failed to drop the unused internal query notification table "%d" in database "%d".    |
|    9218    |    16    |    No    |    Query notifications reached the internal limit of the maximum number of objects.    |
|    9219    |    16    |    No    |    The query notification subscriptions cleanup operation failed. See previous errors for details.    |
|    9220    |    10    |    No    |    Query notification dialog on conversation handle '%.*ls' closed due to an unknown service broker error.    |
|    9221    |    10    |    No    |    Query notification delivery could not get dialog endpoint for dialog '%.*ls'. Delivery failed for notification '%.*ls' because of the following error in service broker '%.*ls'.    |
|    9222    |    16    |    No    |    Internal query notification table has an outdated schema and the table has been dropped. Query notification cleanup has not been performed for this table.    |
|    9223    |    10    |    Yes    |    %d active query notification subscription(s) owned by security identification number '%.*ls' were dropped.    |
|    9224    |    10    |    No    |    Query notification delivery could not access database with id %d. Delivery failed for notification '%.*ls'.    |
|    9225    |    16    |    No    |    The notification options identifier string has %d characters. The maximum allowed length is %d characters.    |
|    9226    |    16    |    No    |    A string value within the notification options identifier is too long. String with prefix '%.*ls' must be %d characters or less.    |
|    9227    |    16    |    No    |    Unmatched quote in notification options identifier string.    |
|    9228    |    16    |    No    |    Name expected in notification options identifier option.    |
|    9229    |    16    |    No    |    Unknown option name '%.*ls' present in notification options identifier. The following are valid option names: 'Service', 'Broker Instance', 'Local Database'. Option names cannot be quoted.    |
|    9230    |    16    |    No    |    Option '%ls' was specified multiple times in the notification options identifier.    |
|    9231    |    16    |    No    |    An equal (=) character was expected after the option name. Found '%.*ls' instead.    |
|    9232    |    16    |    No    |    A semicolon (;) must be use to separate options in a notification options identifier. String '%.*ls' was found following an option.    |
|    9233    |    16    |    No    |    The option 'Service' must be specified in the notification options identifier.    |
|    9234    |    16    |    No    |    The options 'Broker Instance' and 'Local Database' were both specified in the notification options identifier.    |
|    9235    |    16    |    No    |    Value missing for option '%ls' in notification options identifier.    |
|    9236    |    16    |    No    |    Database %.*ls is not a valid local database.    |
|    9237    |    16    |    No    |    Database %.*ls is not a valid broker database.    |
|    9238    |    16    |    No    |    Query notification subscriptions are not allowed under an active application role context. Consider re-issuing the request without activating the application role.    |
|    9239    |    16    |    No    |    Internal query notifications error: The garbage collector corrected an inconsistency.    |
|    9240    |    10    |    No    |    Service broker dialog '%.*ls' could not be closed due to a broker error in database with id '%d' because of the following error in service broker: '%.*ls'.    |
|    9241    |    16    |    No    |    Service broker dialog '%.*ls' could not be closed because the database with id '%d' is not available. Consider closing the dialogs manually once the database is available again.    |
|    9242    |    10    |    No    |    Query notification delivery could not get dialog endpoint for dialog '%.*ls'. Query notification delivery failed because of the following error in service broker: '%.*ls'. See the error log for additional information.    |
|    9243    |    10    |    No    |    Query notification delivery could not send message on dialog '%.*ls'. Query notification delivery failed because of the following error in service broker: '%.*ls'. See the error log for additional information.    |
|    9244    |    16    |    No    |    Query notification cleanup could not access metadata for database "%d". Check whether the database is successfully restored and online.    |
|    9300    |    16    |    No    |    %sIn this version of the server, the 'fn:id()' function only accepts an argument of type 'IDREF *'.    |
|    9301    |    16    |    No    |    %sIn this version of the server, 'cast as \<type\>%s' is not available. Please use the 'cast as \<type\> ?' syntax.    |
|    9302    |    16    |    No    |    %sThe context item in which the 'fn:id()' function is used must be a node.    |
|    9303    |    16    |    No    |    %sSyntax error near '%ls', expected '%ls'.    |
|    9304    |    16    |    No    |    %sThis version of the server only supports XQuery version '1.0'.    |
|    9305    |    16    |    No    |    %sOnly type names followed by '?' are supported in the target of 'instance of'.    |
|    9306    |    16    |    No    |    %sThe target of 'replace value of' cannot be a union type, found '%ls'.    |
|    9308    |    16    |    No    |    %sThe argument of '%ls' must be of a single numeric primitive type or 'http://www.w3.org/2004/07/xpath-datatypes#untypedAtomic'. Found argument of type '%ls'.    |
|    9309    |    16    |    No    |    %sThe target of 'replace value of' cannot be 'http://www.w3.org/2001/XMLSchema#anySimpleType', found '%ls'.    |
|    9310    |    16    |    No    |    %sThe 'with' clause of 'replace value of' cannot contain constructed XML.    |
|    9311    |    16    |    No    |    %sHeterogeneous sequences are not allowed in '%ls', found '%ls' and '%ls'.    |
|    9312    |    16    |    No    |    %s'%ls' is not supported on simple typed or 'http://www.w3.org/2001/XMLSchema#anyType' elements, found '%ls'.    |
|    9313    |    16    |    No    |    %sThis version of the server does not support multiple expressions or expressions mixed with strings in an attribute constructor.    |
|    9314    |    16    |    No    |    %sCannot implicitly atomize or apply 'fn:data()' to complex content elements, found type '%ls' within inferred type '%ls'.    |
|    9315    |    16    |    No    |    %sOnly constant expressions are supported for the name expression of computed element and attribute constructors.    |
|    9316    |    16    |    No    |    %sCannot use 'xmlns' in the name expression of computed attribute constructor.    |
|    9317    |    16    |    No    |    %sSyntax error near '%ls', expected string literal.    |
|    9318    |    16    |    No    |    %sSyntax error at source character '0x%02x' near '%ls', expected string literal.    |
|    9319    |    16    |    No    |    %sStatic simple type validation: Invalid simple type value '%ls'.    |
|    9320    |    16    |    No    |    %sThe result of applying the 'parent' axis on the document node is statically 'empty'.    |
|    9321    |    16    |    No    |    %sThe result of applying 'parent::%ls' is statically 'empty'.    |
|    9322    |    16    |    No    |    %sTwo consecutive '-' can only appear in a comment constructor if they are used to close the comment ('-->').    |
|    9323    |    16    |    No    |    %sUsing ':' in variable names is not supported in this version of the server.    |
|    9324    |    16    |    No    |    %sFound '}' without matching '{'. If you want to use the characters '{' or '}', you need to escape them as '{{' or '}}' respectively.    |
|    9325    |    16    |    No    |    %sComputed processing instruction constructors are not supported.    |
|    9326    |    16    |    No    |    %sComputed comment constructors are not supported.    |
|    9327    |    16    |    No    |    %sAll prolog entries need to end with ';', found '%ls'.    |
|    9328    |    16    |    No    |    %sType specification expected, found '%ls'.    |
|    9330    |    16    |    No    |    %sOnly comparable types are allowed in '%ls', found '%ls'.    |
|    9331    |    16    |    No    |    %sSyntax error near '%ls', expected '%ls' or '%ls'.    |
|    9332    |    16    |    No    |    %sSyntax error near '%ls', expected 'where', '(stable) order by' or 'return'.    |
|    9333    |    16    |    No    |    %s'//' followed by 'self', 'parent' or 'descendant-or-self' axes is not supported when it encounters simple typed or 'http://www.w3.org/2001/XMLSchema#anyType' elements, found '%ls'.    |
|    9334    |    16    |    No    |    %sThe 'form' attribute cannot be specified on a local attribute or element definition that has the 'ref' attribute. Location: '%ls'.    |
|    9335    |    16    |    No    |    %sThe XQuery syntax '%ls' is not supported.    |
|    9336    |    16    |    No    |    %sThe XML Schema syntax '%ls' is not supported.    |
|    9337    |    16    |    No    |    %sThe XML Schema type 'NOTATION' is not supported.    |
|    9338    |    16    |    No    |    %sThe value of a namespace declaration attribute must be a string literal. It cannot contain expressions.    |
|    9339    |    16    |    No    |    %sThe 'form' attribute cannot be specified on a global attribute or element definition. Location: '%ls'.    |
|    9340    |    16    |    No    |    %sExplicit import of the current target namespace is invalid. References to items in the current target namespace that have already been loaded in the schema collection will be resolved implicitly.    |
|    9341    |    16    |    No    |    %sSyntax error near '%ls', expected a step expression.    |
|    9342    |    16    |    No    |    %sAn XML instance is only supported as the direct source of an insert using sql:column/sql:variable.    |
|    9343    |    16    |    No    |    %sThe XML instance referred to by sql:column() and sql:variable() must be either untyped XML or must be typed with the same XML schema collection as the context XML instance on which the XML method is being applied to.    |
|    9344    |    16    |    No    |    %sThe SQL type '%s' is not supported with sql:column() and sql:variable().    |
|    9400    |    16    |    No    |    XML parsing: line %d, character %d, unexpected end of input    |
|    9401    |    16    |    No    |    XML parsing: line %d, character %d, unrecognized encoding    |
|    9402    |    16    |    No    |    XML parsing: line %d, character %d, unable to switch the encoding    |
|    9403    |    16    |    No    |    XML parsing: line %d, character %d, unrecognized input signature    |
|    9410    |    16    |    No    |    XML parsing: line %d, character %d, whitespace expected    |
|    9411    |    16    |    No    |    XML parsing: line %d, character %d, semicolon expected    |
|    9412    |    16    |    No    |    XML parsing: line %d, character %d, '>' expected    |
|    9413    |    16    |    No    |    XML parsing: line %d, character %d, A string literal was expected    |
|    9414    |    16    |    No    |    XML parsing: line %d, character %d, equal expected    |
|    9415    |    16    |    No    |    XML parsing: line %d, character %d, well formed check: no '<' in attribute value    |
|    9416    |    16    |    No    |    XML parsing: line %d, character %d, hexadecimal digit expected    |
|    9417    |    16    |    No    |    XML parsing: line %d, character %d, decimal digit expected    |
|    9418    |    16    |    No    |    XML parsing: line %d, character %d, '[' expected    |
|    9419    |    16    |    No    |    XML parsing: line %d, character %d, '(' expected    |
|    9420    |    16    |    No    |    XML parsing: line %d, character %d, illegal xml character    |
|    9421    |    16    |    No    |    XML parsing: line %d, character %d, illegal name character    |
|    9422    |    16    |    No    |    XML parsing: line %d, character %d, incorrect document syntax    |
|    9423    |    16    |    No    |    XML parsing: line %d, character %d, incorrect CDATA section syntax    |
|    9424    |    16    |    No    |    XML parsing: line %d, character %d, incorrect comment syntax    |
|    9425    |    16    |    No    |    XML parsing: line %d, character %d, incorrect conditional section syntax    |
|    9426    |    16    |    No    |    XML parsing: line %d, character %d, incorrect ATTLIST declaration syntax    |
|    9427    |    16    |    No    |    XML parsing: line %d, character %d, incorrect DOCTYPE declaration syntax    |
|    9428    |    16    |    No    |    XML parsing: line %d, character %d, incorrect ELEMENT declaration syntax    |
|    9429    |    16    |    No    |    XML parsing: line %d, character %d, incorrect ENTITY declaration syntax    |
|    9430    |    16    |    No    |    XML parsing: line %d, character %d, incorrect NOTATION declaration syntax    |
|    9431    |    16    |    No    |    XML parsing: line %d, character %d, NDATA expected    |
|    9432    |    16    |    No    |    XML parsing: line %d, character %d, PUBLIC expected    |
|    9433    |    16    |    No    |    XML parsing: line %d, character %d, SYSTEM expected    |
|    9434    |    16    |    No    |    XML parsing: line %d, character %d, name expected    |
|    9435    |    16    |    No    |    XML parsing: line %d, character %d, one root element    |
|    9436    |    16    |    No    |    XML parsing: line %d, character %d, end tag does not match start tag    |
|    9437    |    16    |    No    |    XML parsing: line %d, character %d, duplicate attribute    |
|    9438    |    16    |    No    |    XML parsing: line %d, character %d, text/xmldecl not at the beginning of input    |
|    9439    |    16    |    No    |    XML parsing: line %d, character %d, namespaces beginning with "xml" are reserved    |
|    9440    |    16    |    No    |    XML parsing: line %d, character %d, incorrect text declaration syntax    |
|    9441    |    16    |    No    |    XML parsing: line %d, character %d, incorrect xml declaration syntax    |
|    9442    |    16    |    No    |    XML parsing: line %d, character %d, incorrect encoding name syntax    |
|    9443    |    16    |    No    |    XML parsing: line %d, character %d, incorrect public identifier syntax    |
|    9444    |    16    |    No    |    XML parsing: line %d, character %d, well formed check: pes in internal subset    |
|    9445    |    16    |    No    |    XML parsing: line %d, character %d, well formed check: pes between declarations    |
|    9446    |    16    |    No    |    XML parsing: line %d, character %d, well formed check: no recursion    |
|    9447    |    16    |    No    |    XML parsing: line %d, character %d, entity content not well formed    |
|    9448    |    16    |    No    |    XML parsing: line %d, character %d, well formed check: undeclared entity    |
|    9449    |    16    |    No    |    XML parsing: line %d, character %d, well formed check: parsed entity    |
|    9450    |    16    |    No    |    XML parsing: line %d, character %d, well formed check: no external entity references    |
|    9451    |    16    |    No    |    XML parsing: line %d, character %d, incorrect processing instruction syntax    |
|    9452    |    16    |    No    |    XML parsing: line %d, character %d, incorrect system identifier syntax    |
|    9453    |    16    |    No    |    XML parsing: line %d, character %d, '?' expected    |
|    9454    |    16    |    No    |    XML parsing: line %d, character %d, no ']]>' in element content    |
|    9455    |    16    |    No    |    XML parsing: line %d, character %d, illegal qualified name character    |
|    9456    |    16    |    No    |    XML parsing: line %d, character %d, multiple colons in qualified name    |
|    9457    |    16    |    No    |    XML parsing: line %d, character %d, colon in name    |
|    9458    |    16    |    No    |    XML parsing: line %d, character %d, redeclared prefix    |
|    9459    |    16    |    No    |    XML parsing: line %d, character %d, undeclared prefix    |
|    9460    |    16    |    No    |    XML parsing: line %d, character %d, non default namespace with empty uri    |
|    9461    |    16    |    No    |    XML %ls starting with '%.*ls' is %d characters long, which exceeds the limit. Maximum allowed length is %d characters.    |
|    9462    |    16    |    No    |    XML parsing: line %d, character %d, not all chunks of value have been read    |
|    9463    |    16    |    No    |    XML parsing: line %d, character %d, xml:space has a non-legal value    |
|    9464    |    16    |    No    |    XML parsing: line %d, character %d, XML namespace prefix 'xml' can only be associated with the URI http://www.w3.org/XML/1998/namespace. This URI cannot be used with other prefixes.    |
|    9465    |    16    |    No    |    XML parsing: line %d, character %d, XML namespace prefix 'xmlns' is reserved for use by XML.    |
|    9466    |    16    |    No    |    XML parsing: line %d, character %d, XML namespace xml namespace URI (https://www.w3.org/XML/1998/namespace) must be assigned only to prefix 'xml'.    |
|    9467    |    16    |    No    |    XML parsing: line %d, character %d, xmlns namespace URI (https://www.w3.org/2000/xmlns/) is reserved and must not be used.    |
|    9480    |    16    |    No    |    XML parsing: line %d, character %d, unsupported xml    |
|    9500    |    16    |    No    |    The data type '%.*ls' used in the VALUE method is invalid.    |
|    9501    |    16    |    No    |    XQuery: Unable to resolve sql:variable('%.*ls'). The variable must be declared as a scalar TSQL variable.    |
|    9502    |    16    |    No    |    The string literal provided for the argument %d of the '%.*ls' method must not exceed %d bytes.    |
|    9503    |    16    |    No    |    Errors and/or warnings occurred when processing the XQuery statement for XML data type method '%.*ls'. See previous error messages for more details.    |
|    9504    |    16    |    No    |    Errors and/or warnings occurred when processing the XQuery statement for XML data type method '%.*ls', invoked on column '%.*ls', table '%.*ls'. See previous error messages for more details.    |
|    9506    |    16    |    No    |    The XMLDT method '%.*ls' can only be invoked on columns of type xml.    |
|    9507    |    16    |    No    |    The XML data type method on a remote column used in this query can not be executed either locally or remotely. Please rewrite your query.    |
|    9508    |    16    |    No    |    The reference parameter given to XMLDT method '%.*ls' was generated from a different XML instance than the one it is being applied to.    |
|    9509    |    16    |    No    |    XMLUNNEST method requires typed xml column with single global element    |
|    9510    |    16    |    No    |    Functionality not yet implemented: XMLNODEREFS cannot use references exposed by views.    |
|    9512    |    16    |    No    |    Xml data type is not supported as a parameter to remote calls.    |
|    9513    |    16    |    No    |    Error processing XML data type method '%.*ls'. The following SET options required by XML data type methods are not set: '%.*ls'.    |
|    9514    |    16    |    No    |    Xml data type is not supported in distributed queries. Remote object '%.*ls' has xml column(s).    |
|    9515    |    16    |    No    |    An XML schema has been altered or dropped, and the query plan is no longer valid. Please rerun the query batch.    |
|    9516    |    16    |    No    |    XQuery: The name or one of the parts of a multi-part name supplied to %S_MSG('%.*ls') is empty. Empty names cannot be used to identify objects, columns or variables in SQL.    |
|    9517    |    16    |    No    |    XQuery: The name or one of the parts of a multi-part name that starts with '%.*ls' supplied to %S_MSG() is not a valid SQL identifier - it is too long. Maximum length is %d, actual length is %d.    |
|    9518    |    16    |    No    |    XQuery: The name or one of the parts of a multi-part name that starts with '%.*ls' supplied to %S_MSG() is not a valid SQL identifier - it contains invalid characters.    |
|    9519    |    16    |    No    |    XQuery: The name supplied to sql:variable('%.*ls') is not a valid SQL variable name. Variable names must start with the '\@' symbol followed by at least one character.    |
|    9520    |    16    |    No    |    XQuery: '%.*ls' referenced by sql:variable() is not a valid system function name.    |
|    9521    |    16    |    No    |    Error processing XML data type. The XML data type instance contains a negative xs:date or xs:dateTime value.    |
|    9522    |    16    |    No    |    The XQuery modify method is not allowed on sparse column sets.    |
|    9523    |    16    |    No    |    Cannot update the sparse column set '%.*ls' because the XML content supplied references the non-sparse column '%.*ls' which does not belong to this column set. The XML data used to update a sparse column set cannot reference columns that don't belong to the column set.    |
|    [9524](../../relational-databases/errors-events/mssqlserver-9524-database-engine-error.md)    |    16    |    No    |    The XML content provided does not conform to the required XML format for sparse column sets.    |
|    9525    |    16    |    No    |    The XML content that is supplied for the sparse column set '%.*ls' contains duplicate references to the column '%.*ls'. A column can only be referenced once in XML content supplied to a sparse column set.    |
|    9526    |    16    |    No    |    In the XML content that is supplied for the sparse column set '%.*ls', the '%.*ls' attribute value on the element '%.*ls' is out of range. The valid range is from 1 to %d.    |
|    9527    |    16    |    No    |    In the XML content that is supplied for the column set '%.*ls', the sqltypes:scale attribute value on the element '%.*ls' is out of range. The valid range for the scale is from 0 to the specified precision.    |
|    9528    |    16    |    No    |    In the XML content that is supplied for the column set '%.*ls', the '%.*ls' attribute on the element '%.*ls' is not valid. The attribute is valid only for sparse columns of data type sql_variant.    |
|    9529    |    16    |    No    |    In the XML content that is supplied for the column set column '%.*ls', the sqlDBType:base64Encoded attribute on the element '%.*ls' is not valid. The base64Encoded attribute can only be used when the corresponding sparse column is of character data type (char, varchar, nchar, nvarchar), or if the sparse column is of data type sql_variant and the value of the xsi:type attribute is "Char", "VarChar", "NChar", or "NVarChar".    |
|    9530    |    16    |    No    |    In the XML content that is supplied for the column set column '%.*ls, the '%.*ls' attribute on the element '%.*ls' is not valid. Remove the attribute.    |
|    9531    |    16    |    No    |    In the XML content that is supplied for the column set column '%.*ls', the '%.*ls' attribute value on the element '%.*ls' is not valid.    |
|    [9532](../../relational-databases/errors-events/mssqlserver-9532-database-engine-error.md)    |    16    |    No    |    In the query/DML operation involving column set '%.*ls', conversion failed when converting from the data type '%ls' to the data type '%ls' for the column '%.*ls'.    |
|    9533    |    16    |    No    |    In the XML that is supplied for the column set '%.*ls', the element '%.*ls' should reside in the global namespace. Remove the default namespace declaration or the prefix on the element.    |
|    9534    |    16    |    No    |    In the query/DML operation involving column set '%.*ls', conversion failed when converting from the data type '%ls' to the data type '%ls' for the column '%.*ls'. Please refer to the Books-on-line for more details on providing XML conversion methods for CLR types.    |
|    9601    |    16    |    No    |    Cannot relate to %S_MSG %.*ls because it is %S_MSG.    |
|    9605    |    10    |    No    |    Conversation Priorities analyzed: %d.    |
|    9606    |    16    |    No    |    The conversation priority with ID %d has been dropped.    |
|    9607    |    16    |    No    |    The conversation priority with ID %d is referencing the missing service with ID %d.    |
|    9608    |    16    |    No    |    The conversation priority with ID %d is referencing the missing service contract with ID %d.    |
|    9609    |    16    |    No    |    The %S_MSG name '%.*ls' contains more than the maximum number of prefixes. The maximum is %d.    |
|    9610    |    16    |    No    |    The service '%.*ls' in the FROM SERVICE clause must match the service '%.*ls' referenced by %s = '%.*ls'.    |
|    9611    |    16    |    No    |    Cannot find the specified user '%.*ls'.    |
|    9613    |    16    |    No    |    The queue '%.*ls' cannot be activated because the activation user is not specified.    |
|    9614    |    16    |    No    |    The queue '%.*ls' cannot be activated because the activation stored procedure is either not specified or is invalid.    |
|    9615    |    16    |    No    |    A message of type '%.*ls' failed XML validation on the target service. %.*ls This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    9616    |    16    |    No    |    A message of type '%.*ls' was received and failed XML validation. %.*ls This occurred in the message with Conversation ID '%.*ls', Initiator: %d, and Message sequence number: %I64d.    |
|    9617    |    16    |    No    |    The service queue "%.*ls" is currently disabled.    |
|    9618    |    16    |    No    |    The message cannot be sent because the service queue '%.*ls' associated with the dialog is currently disabled and retention is on.    |
|    9619    |    16    |    No    |    Failed to create remote service binding '%.*ls'. A remote service binding for service '%.*ls' already exists.    |
|    9620    |    16    |    No    |    The activation stored procedure '%.*ls' is invalid. Functions are not allowed.    |
|    9621    |    16    |    No    |    An error occurred while processing a message in the Service Broker and Database Mirroring transport: error %i, state %i.    |
|    9622    |    16    |    No    |    The crypto provider context is not initialized.    |
|    9623    |    16    |    No    |    The key passed in for this operation is in the wrong state.    |
|    9624    |    16    |    No    |    The key size is unacceptable for this key object.    |
|    9625    |    16    |    No    |    The key buffer size is inconsistent with the key modulus size.    |
|    9626    |    16    |    No    |    An internal Service Broker error occurred: an object is in the wrong state for this operation. This error indicates a serious problem with SQL Server. Check the SQL Server error log and the Windows event logs for information pointing to possible hardware problems.    |
|    9627    |    16    |    No    |    The hash buffer size is not correct for initializing the hash object.    |
|    9628    |    16    |    No    |    The encryption/decryption data buffer size is not 8 byte aligned.    |
|    9629    |    16    |    No    |    The decrypted signature size is wrong.    |
|    9630    |    16    |    No    |    The signature did not verify the internal hash.    |
|    9631    |    16    |    No    |    The salt size is unacceptable for this key object.    |
|    9632    |    16    |    No    |    The salt buffer size is too small.    |
|    9633    |    16    |    No    |    The passed in name is too long.    |
|    9634    |    16    |    No    |    Service Broker was unable to allocate memory for cryptographic operations. This message is a symptom of another problem. Check the SQL Server error log for additional messages, and address the underlying problem.    |
|    9635    |    16    |    No    |    The certificate is not valid at this point in time.    |
|    9636    |    16    |    No    |    The requested object was not found.    |
|    9637    |    16    |    No    |    The passed in serialized object is incorrectly encoded.    |
|    9638    |    16    |    No    |    The cer or pvk file size is too large.    |
|    9639    |    16    |    No    |    A password was supplied and the pvk file is not encrypted.    |
|    9640    |    16    |    No    |    The operation encountered an OS error.    |
|    9641    |    16    |    No    |    A cryptographic operation failed. This error indicates a serious problem with SQL Server. Check the SQL Server error log and the Windows event logs for further information.    |
|    9642    |    16    |    No    |    An error occurred in a Service Broker/Database Mirroring transport connection endpoint, Error: %i, State: %i. (Near endpoint role: %S_MSG, far endpoint address: '%.*hs')    |
|    9643    |    16    |    No    |    An error occurred in the Service Broker/Database Mirroring transport manager: Error: %i, State: %i.    |
|    9644    |    16    |    No    |    An error occurred in the service broker message dispatcher. Error: %i, State: %i.    |
|    9645    |    16    |    No    |    An error occurred in the service broker manager, Error: %i, State: %i.    |
|    9646    |    16    |    No    |    An error occurred in the timer event cache. Error %i, state %i.    |
|    9647    |    16    |    Yes    |    Received a malformed message from the network. Unable to retrieve a broker message attribute from a message destined for database ID %d. This may indicate a network problem or that another application connected to the Service Broker endpoint.    |
|    9648    |    20    |    No    |    The queue '%.*ls' has been enabled for activation, but the MAX_QUEUE_READERS is zero. No procedures will be activated. Consider increasing the number of MAX_QUEUE_READERS.    |
|    9649    |    16    |    Yes    |    A security (SSPI) error occurred when connecting to another service broker: '%.*ls'. Check the Windows Event Log for more information.    |
|    9650    |    16    |    No    |    A system cryptographic call failed during a Service Broker or Database Mirroring operation: system error '%ls'.    |
|    9651    |    16    |    No    |    The system call failed during a Service Broker or Database Mirroring operation. System error: '%ls'.    |
|    9652    |    16    |    No    |    Service Broker failed to retrieve the session key for encrypting a message.    |
|    9653    |    16    |    No    |    The signature of activation stored procedure '%.*ls' is invalid. Parameters are not allowed.    |
|    9654    |    16    |    No    |    Attempting to use database and it doesn't exist.    |
|    9655    |    16    |    Yes    |    The transmission queue table structure in database is inconsistent. Possible database corruption.    |
|    9657    |    23    |    Yes    |    The structure of the Service Broker transmission work-table in tempdb is incorrect or corrupt. This indicates possible database corruption or hardware problems. Check the SQL Server error log and the Windows event logs for information on possible hardware problems. Restart SQL Server to rebuild tempdb.    |
|    9658    |    16    |    No    |    Cannot access the transmission queue table in database.    |
|    9659    |    16    |    No    |    The %s of route '%.*ls' cannot be empty.    |
|    9660    |    16    |    No    |    The %s of route '%.*ls' must be less than %d characters long.    |
|    9661    |    16    |    No    |    The SERVICE_NAME and BROKER_INSTANCE of route "%.*ls" must be specified when using mirroring.    |
|    9662    |    16    |    No    |    Cannot specify BROKER_INSTANCE without SERVICE_NAME in route "%.*ls".    |
|    9663    |    16    |    No    |    The system object cannot be modified.    |
|    9666    |    10    |    No    |    The %S_MSG protocol transport is disabled or not configured.    |
|    9667    |    10    |    No    |    Services analyzed: %d.    |
|    9668    |    10    |    No    |    Service Queues analyzed: %d.    |
|    9669    |    10    |    No    |    Conversation Endpoints analyzed: %d.    |
|    9670    |    10    |    No    |    Remote Service Bindings analyzed: %d.    |
|    9671    |    16    |    No    |    Messages with conversation ID '%ls' have been removed from the transmission queue.    |
|    9672    |    16    |    No    |    Messages with conversation handle '%ls' and conversation group '%ls' have been removed from the queue with ID %d.    |
|    9673    |    16    |    No    |    Activation has been disabled on the queue with ID %d.    |
|    9674    |    10    |    No    |    Conversation Groups analyzed: %d.    |
|    9675    |    10    |    No    |    Message Types analyzed: %d.    |
|    9676    |    10    |    No    |    Service Contracts analyzed: %d.    |
|    9677    |    16    |    No    |    The service contract with ID %d is referencing the missing message type with ID %d.    |
|    9678    |    16    |    No    |    The service with ID %d is referencing the missing service contract with ID %d.    |
|    9679    |    16    |    No    |    The service with ID %d is referencing the missing service queue with ID %d.    |
|    9680    |    16    |    No    |    The conversation endpoint '%ls' is referencing the missing conversation group '%ls'.    |
|    9681    |    16    |    No    |    The conversation endpoint with ID '%ls' and is_initiator: %d is referencing the missing service contract with ID %d.    |
|    9682    |    16    |    No    |    The conversation endpoint with ID '%ls' and is_initiator: %d is referencing the missing service with ID %d.    |
|    9683    |    16    |    No    |    The conversation group '%ls' is referencing the missing service with ID %d.    |
|    9684    |    16    |    No    |    The service with ID %d has been dropped.    |
|    9685    |    16    |    No    |    The service contract with ID %d has been dropped.    |
|    9686    |    16    |    No    |    The conversation endpoint with handle '%ls' has been dropped.    |
|    9687    |    16    |    No    |    The conversation group '%ls' has been dropped.    |
|    9688    |    10    |    No    |    Service Broker manager has started.    |
|    9689    |    10    |    No    |    Service Broker manager has shut down.    |
|    9690    |    10    |    Yes    |    The %S_MSG protocol transport is now listening for connections.    |
|    9691    |    10    |    No    |    The %S_MSG protocol transport has stopped listening for connections.    |
|    [9692](../../relational-databases/errors-events/mssqlserver-9692-database-engine-error.md)    |    16    |    No    |    The %S_MSG protocol transport cannot listen on port %d because it is in use by another process.    |
|    9693    |    16    |    No    |    The %S_MSG protocol transport could not listen for connections due to the following error: '%.*ls'.    |
|    9694    |    16    |    No    |    Could not start Service Broker manager. Check the SQL Server error log and the Windows error log for additional error messages.    |
|    9695    |    16    |    No    |    Could not allocate enough memory to start the Service Broker task manager. This message is a symptom of another problem. Check the SQL Server error log for additional messages, and address the underlying problem.    |
|    9696    |    16    |    No    |    Cannot start the Service Broker primary event handler. This error is a symptom of another problem. Check the SQL Server error log for additional messages, and address this underlying problem.    |
|    9697    |    10    |    No    |    Could not start Service Broker for database id: %d. A problem is preventing SQL Server from starting Service Broker. Check the SQL Server error log for additional messages.    |
|    9698    |    16    |    No    |    Cannot start Service Broker security manager. This message is a symptom of another problem. Check the SQL Server error log and the Windows event log for additional messages, and address the underlying problem.    |
|    9699    |    16    |    No    |    Could not allocate memory for extra Service Broker tasks while adding CPUs.    |
|    9701    |    16    |    No    |    Cannot start Service Broker activation manager. This message is a symptom of another problem. Check the SQL Server error log and the Windows event log for additional messages and address the underlying problem.    |
|    9704    |    16    |    No    |    This message could not be delivered because it failed XML validation. This failure occurred while the message was being delivered to the target service.    |
|    9705    |    16    |    No    |    The messages in the queue with ID %d are referencing the invalid conversation handle '%ls'.    |
|    9706    |    16    |    No    |    The stored procedure with ID %d is invalid but is referenced by the queue with ID %d.    |
|    9707    |    16    |    No    |    The activation user with ID %d is invalid but is referenced by queue with ID %d.    |
|    9708    |    16    |    No    |    The messages in the queue with ID %d are referencing the invalid conversation group '%ls'.    |
|    9709    |    16    |    No    |    The messages in the queue with ID %d are referencing the invalid message type with ID %d.    |
|    9710    |    16    |    No    |    The conversation endpoint with ID '%ls' and is_initiator: %d is referencing the invalid conversation group '%ls'.    |
|    9711    |    16    |    No    |    The transmission queue is referencing the invalid conversation ID '%ls'.    |
|    9712    |    16    |    No    |    The remote service binding with ID %d is referencing the invalid service contract with ID %d.    |
|    9713    |    16    |    No    |    The message type with ID %d is referencing the invalid XML schema collection ID %d.    |
|    9715    |    16    |    No    |    The conversation endpoint with conversation handle '%ls' is in an inconsistent state. Check the SQL Server error logs and the Windows event logs for information on possible hardware problems. To recover the database, restore the database from a clean backup. If no clean backup is available, consider running DBCC CHECKDB. Note that DBCC CHECKDB may remove data.    |
|    9716    |    16    |    No    |    The conversation group '%ls' reports references to %d conversation handle(s), but actually references %d.    |
|    9717    |    16    |    No    |    Cannot enable stored procedure activation on queue '%.*ls'. Event notification for queue_activation is already configured on this queue.    |
|    9718    |    16    |    No    |    Cannot create event notification for queue_activation on queue "%.*ls". Stored procedure activation is already configured on this queue.    |
|    9719    |    16    |    No    |    The database for this conversation endpoint is attached or restored.    |
|    9720    |    16    |    No    |    The database for the remote conversation endpoint is attached or restored.    |
|    9721    |    10    |    No    |    Service broker failed to clean up conversation endpoints on database '%.*ls'. Another problem is preventing SQL Server from completing this operation. Check the SQL Server error log for additional messages.    |
|    9723    |    10    |    No    |    The database "%i" will not be started as the broker due to duplication in the broker instance ID.    |
|    9724    |    10    |    No    |    The activated proc '%ls' running on queue '%ls' output the following: '%.*ls'    |
|    9725    |    16    |    No    |    The invalid schema has been dropped from the message type with ID %d.    |
|    9726    |    16    |    No    |    The remote service binding with ID %d has been dropped.    |
|    9727    |    16    |    Yes    |    Dialog security is not available for this conversation because there is no remote service binding for the target service. Create a remote service binding, or specify ENCRYPTION = OFF in the BEGIN DIALOG statement.    |
|    9728    |    16    |    Yes    |    Cannot find the security certificate because the lookup database principal ID (%i) is not valid. The security principal may have been dropped after the conversation was created.    |
|    9730    |    16    |    Yes    |    Cannot find the security certificate because the lookup database principal (Id: %i) does not correspond to a server principal. The security principal may have been dropped after the conversation was created.    |
|    9731    |    16    |    Yes    |    Dialog security is unavailable for this conversation because there is no security certificate bound to the database principal (Id: %i). Either create a certificate for the principal, or specify ENCRYPTION = OFF when beginning the conversation.    |
|    9733    |    16    |    Yes    |    There is no private key for the security certificate bound to database principal (Id: %i). The certificate may have been created or installed incorrectly. Reinstall the certificate, or create a new certificate.    |
|    9734    |    16    |    Yes    |    The length of the private key for the security certificate bound to database principal (Id: %i) is incompatible with the Windows cryptographic service provider. The key length must be a multiple of 64 bytes.    |
|    9735    |    16    |    Yes    |    The length of the public key for the security certificate bound to database principal (Id: %i) is incompatible with the Windows cryptographic service provider. The key length must be a multiple of 64 bytes.    |
|    9736    |    16    |    No    |    An error occurred in dialog transmission: Error: %i, State: %i. %.*ls    |
|    9737    |    16    |    Yes    |    The private key for the security certificate bound to the database principal (ID %i) is password protected. Password protected private keys are not supported for use with secure dialogs.    |
|    9738    |    16    |    No    |    Cannot create task for Service Broker message dispatcher. This message is a symptom of another problem that is preventing SQL Server from creating tasks. Please check the SQL Server error log and the Windows event log for additional messages.    |
|    9739    |    16    |    No    |    Message transmitter in service broker message dispatcher failed %d times    |
|    9740    |    16    |    No    |    Cannot start the Service Broker message dispatcher. This error is a symptom of another problem. Check the SQL Server error log and the Windows event log for additional messages, and address this underlying problem.    |
|    9741    |    10    |    No    |    The %S_MSG '%.*ls' was dropped on upgrade because it referenced a system contract that was dropped.    |
|    9742    |    16    |    No    |    The activation stored procedure '%.*ls' is invalid. Temporary procedures may not be configured for activation.    |
|    9743    |    16    |    No    |    The %s of route "%.*ls" must be an address when using mirroring.    |
|    9744    |    16    |    No    |    The %s of route "%.*ls" is not a valid address.    |
|    9745    |    16    |    No    |    The ADDRESS of route '%.*ls' cannot be 'TRANSPORT' when SERVICE_NAME is specified.    |
|    9746    |    16    |    No    |    The LIFETIME of route '%.*ls' must be in the range %d to %d.    |
|    9747    |    16    |    No    |    The ADDRESS and MIRROR_ADDRESS of route '%.*ls' cannot be the same.    |
|    9748    |    10    |    No    |    The %S_MSG protocol transport is not available.    |
|    9749    |    10    |    No    |    Target queue is busy; message(s) queued for delivery.    |
|    9750    |    10    |    No    |    No route matches the destination service name for this conversation. Create a route to the destination service name for messages in this conversation to be delivered.    |
|    9751    |    10    |    No    |    Authentication failed with error: '%.*ls'.    |
|    9752    |    10    |    No    |    %S_MSG connection was refused. The user account of the remote server is not permitted to log in to this SQL Server: User account: '%.*ls', IP address: '%.*hs'.    |
|    9753    |    10    |    No    |    The target service broker is unreachable.    |
|    9754    |    10    |    No    |    Connection attempt failed with error: '%.*ls'.    |
|    9755    |    10    |    No    |    An error occurred while receiving data: '%.*ls'.    |
|    9756    |    10    |    No    |    An internal exception occurred while connecting to an adjacent broker: Error: %i, State: %i. %.*ls    |
|    9757    |    10    |    No    |    Service Broker/Database Mirroring network protocol error occurred.    |
|    9758    |    10    |    No    |    Login protocol negotiation error occurred.    |
|    9759    |    10    |    No    |    An error occurred while sending data: '%.*ls'.    |
|    9761    |    16    |    Yes    |    The Broker Configuration conversation on dialog handle '%s' closed due to an error. To troubleshoot this problem, investigate the error: '%.*ls'.    |
|    9762    |    10    |    No    |    An error occurred while looking up the public key certificate associated with this SQL Server instance: No certificate was found.    |
|    9763    |    10    |    No    |    An error occurred while looking up the public key certificate associated with this SQL Server instance: The certificate found is not valid at the current time.    |
|    9764    |    10    |    No    |    An error occurred while looking up the public key certificate associated with this SQL Server instance: The certificate found is too large.    |
|    9765    |    10    |    No    |    An error occurred while looking up the public key certificate associated with this SQL Server instance: The certificate found has no associated private key.    |
|    9766    |    10    |    No    |    An unknown internal error (%d) occurred while looking up the public key certificate associated with this SQL Server instance.    |
|    9767    |    16    |    Yes    |    The security certificate bound to database principal (Id: %i) has been disabled for use with BEGIN DIALOG. See the Books Online topics "Certificates and Service Broker" for an overview and "ALTER CERTIFICATE (Transact-SQL)" for syntax to make a certificate ACTIVE FOR BEGIN_DIALOG.    |
|    9768    |    16    |    Yes    |    A database user associated with the secure conversation was dropped before credentials had been exchanged with the far endpoint. Avoid using DROP USER while conversations are being created.    |
|    9769    |    10    |    No    |    Insufficient memory prevented the Service Broker/Database Mirroring Transport Manager from starting.    |
|    9770    |    10    |    No    |    Locating routes and security information via the Broker Configuration Service.    |
|    9771    |    10    |    No    |    The service broker manager is disabled in single-user mode.    |
|    9772    |    16    |    No    |    The Service Broker in database "%.*ls" cannot be enabled because there is already an enabled Service Broker with the same ID.    |
|    9773    |    10    |    No    |    The Service Broker in database "%d" is disabled because there is already an enabled Service Broker with the same ID.    |
|    9774    |    10    |    No    |    Cannot create a new Service Broker in the attached read-only database "%.*ls". The Service Broker will be disabled.    |
|    9775    |    16    |    No    |    Cannot create a new Service Broker in read-only database "%.*ls".    |
|    9776    |    16    |    No    |    Cannot enable the Service Broker in database "%.*ls" because the Service Broker GUID in the database (%s) does not match the one in sys.databases (%s).    |
|    9777    |    10    |    No    |    The Service Broker in database "%.*ls" will be disabled because the Service Broker GUID in the database (%s) does not match the one in sys.databases (%s).    |
|    9778    |    16    |    No    |    Cannot create a new Service Broker in a mirrored database "%.*ls".    |
|    9779    |    10    |    No    |    Service Broker received an END CONVERSATION message on this conversation. Service Broker will not transmit the message; it will be held until the application ends the conversation.    |
|    9780    |    10    |    No    |    The service broker manager is initializing.    |
|    9781    |    10    |    No    |    The service broker manager is shutting down.    |
|    9782    |    10    |    No    |    An internal exception occurred while dispatching a message: Error: %i, State: %i. %.*ls    |
|    9783    |    10    |    No    |    DNS lookup failed with error: '%.*ls'.    |
|    9784    |    10    |    No    |    Service Broker received an error message on this conversation. Service Broker will not transmit the message; it will be held until the application ends the conversation.    |
|    9785    |    10    |    No    |    Invalid address specified: '%.*ls'.    |
|    9786    |    10    |    No    |    Cannot retrieve user name from security context. Error: '%.*ls'. State: %hu.    |
|    9787    |    10    |    No    |    An error occurred while processing broker mirroring routes. Error: %i. State: %i.    |
|    9788    |    10    |    No    |    Unable to route the incoming message. The system database msdb containing routing information is not available.    |
|    9789    |    10    |    No    |    Unable to route the incoming message. The system database msdb containing routing information is not available. The broker is disabled in msdb.    |
|    [9790](../../relational-databases/errors-events/mssqlserver-9790-database-engine-error.md)    |    10    |    No    |    Unable to route the incoming message. The system database msdb containing routing information is in SINGLE USER mode.    |
|    9791    |    10    |    No    |    The broker is disabled in the sender's database.    |
|    9792    |    10    |    No    |    Could not forward the message because forwarding is disabled in this SQL Server instance.    |
|    9793    |    10    |    No    |    The target service name could not be found. Ensure that the service name is specified correctly and/or the routing information has been supplied.    |
|    9794    |    10    |    No    |    The broker mirroring manager has not fully initialized.    |
|    9795    |    10    |    No    |    Could not find the target broker in the local SQL Server instance.    |
|    9796    |    10    |    No    |    The target service name matched a LOCAL route, but there is no service by that name in the local SQL Server instance.    |
|    9797    |    10    |    No    |    Classification has been delayed because the routing information is currently being updated.    |
|    9798    |    16    |    No    |    The message could not be delivered because it could not be classified. Enable broker message classification trace to see the reason for the failure.    |
|    9801    |    16    |    No    |    Error converting %.*ls to %ls. The result would be truncated.    |
|    9802    |    16    |    No    |    The locale identifier (LCID) %d is not supported by SQL Server.    |
|    9803    |    16    |    No    |    Invalid data for type "%ls".    |
|    9804    |    16    |    No    |    Column or parameter #%d: Invalid fractional second precision %d specified for %ls data type. The maximum fractional second precision is %d.    |
|    9805    |    10    |    No    |    Warning: converting %ls to %ls caused a loss of information.    |
|    9806    |    16    |    No    |    The datepart %.*ls is not supported by date function %.*ls.    |
|    9807    |    16    |    No    |    The input character string does not follow style %d, either change the input character string or use a different style.    |
|    9808    |    16    |    No    |    This session's YDM date format is not supported when converting from this character string format to date, time, datetime2 or datetimeoffset. Change the session's date format or provide a style to the explicit conversion.    |
|    9809    |    16    |    No    |    The style %d is not supported for conversions from %s to %s.    |
|    9810    |    16    |    No    |    The datepart %.*ls is not supported by date function %.*ls for data type %s.    |
|    9811    |    16    |    No    |    The system timezone information could not be retrieved.    |
|    9812    |    16    |    No    |    The timezone provided to builtin function %.*ls is invalid.    |
|    9813    |    16    |    No    |    The timezone provided to builtin function %.*ls would cause the datetimeoffset to overflow the range of valid date range in either UTC or local time.    |
|    9814    |    16    |    No    |    The date provided is before the start of the Hijri calendar which in Microsoft's 'Kuwaiti Algorithm' is July 15th, 622 C.E. (Julian calendar) or July 18th, 622 C.E (proleptic Gregorian calendar).    |
|    9815    |    16    |    No    |    Waitfor delay and waitfor time cannot be of type %s.    |
|    9816    |    16    |    No    |    The number of columns in the column set exceeds 2048. Reduce the number of columns that are referenced in the column set.    |
|    9817    |    16    |    No    |    The specified column set value causes the estimated row size to be at least %d bytes. This exceeds the maximum allowed row size of %d bytes. To reduce the row size, reduce the number of columns specified in the column set.    |
|    9901    |    16    |    Yes    |    Full-text catalog '%ls' ('%d') in database '%ls' ('%d') is low on disk space. Pausing all populations in progress until more space becomes available. Reason code: %d. Error: %ls. To resume populations, free up disk space.    |
|    9902    |    10    |    No    |    Full-text catalog '%ls' ('%d') in database '%ls' ('%d') is low on system resources. Any population in progress will be paused until more resources become available. Reason code: %d. Error: %ls. If this message occurs frequently, try to serialize full-text indexing for multiple catalogs.    |
|    9903    |    10    |    No    |    The full-text catalog health monitor reported a failure for full-text catalog '%ls' (%d) in database '%ls' (%d). Reason code: %d. Error: %ls. The system will restart any in-progress population from the previous checkpoint. If this message occurs frequently, consult SQL Server Books Online for troubleshooting assistance. This is an informational message only. No user action is required.    |
|    9904    |    10    |    No    |    The full-text catalog '%ls' (%d) in database '%ls' (%d) will be remounted to recover from a failure. Reason code: %d. Error: %ls. If this message occurs frequently, consult SQL Server Books Online for troubleshooting assistance. This is an informational message only. No user action is required.    |
|    9905    |    10    |    No    |    Informational: Full-text indexer requested status change for catalog '%ls' ('%d') in database '%ls' ('%d'). New Status: %ls, Reason: %ls (%ls).    |
|    9906    |    10    |    No    |    The full-text catalog monitor reported catalog '%ls' (%d) in database '%ls' (%d) in %ls state. This is an informational message only. No user action is required.    |
|    9907    |    10    |    No    |    Error: Total number of items in full-text catalog ID '%d' in database ID '%d' exceeds the supported limit. See Books Online for troubleshooting assistance.    |
|    9908    |    10    |    No    |    Changing the status to %ls for full-text catalog '%ls' (%d) in database '%ls' (%d). This is an informational message only. No user action is required.    |
|    9909    |    10    |    No    |    Warning: Failed to change the status to %ls for full-text catalog '%ls' (%d) in database '%ls' (%d). Error: %ls.    |
|    9910    |    10    |    No    |    Warning: Error occurred during full-text %ls population for table or indexed view '%ls', database '%ls' (table or indexed view ID '%d', database ID '%d'). Error: %ls.    |
|    9911    |    10    |    No    |    Informational: Full-text %ls population initialized for table or indexed view '%ls' (table or indexed view ID '%d', database ID '%d'). Population sub-tasks: %d.    |
|    9912    |    10    |    No    |    Error: Failed to initialize full-text %ls population for table or indexed view '%ls', database '%ls' (table or indexed view ID '%d', database ID '%d'). Error: %d.    |
|    9913    |    10    |    No    |    Informational: Resuming full-text population for table or indexed view '%ls' in database '%ls' (table or indexed view ID '%d', database ID '%d'). Prior number of documents processed: %d, error encountered: %d.    |
|    9914    |    16    |    No    |    Error: Failed to resume full-text %ls population for table or indexed view '%ls' in database '%ls' (table or indexed view ID '%d', database ID '%d'). Error: 0x%x. Repeat the operation that triggered the resume, or drop and re-create the index.    |
|    9915    |    10    |    No    |    Reinitialized full-text %ls population for table '%ls' (table ID '%d', database ID '%d') after a temporary failure. Number of documents processed prior to failure: %d, errors encountered: %d. This is an informational message only. No user action is required.    |
|    9916    |    10    |    No    |    Error: Failed to reinitialize full-text %ls population after a temporary failure for table or indexed view '%ls', database '%ls' (table or indexed view ID '%d', database ID '%d'). Error: %d.    |
|    9917    |    17    |    No    |    An internal error occurred in full-text docid mapper.    |
|    9918    |    10    |    No    |    Warning: Full-text catalog '%ls' uses FAT volume. Security and differential backup are not supported for the catalog.    |
|    9919    |    16    |    No    |    Fulltext DDL command failed because SQL Server was started in single user mode.    |
|    9920    |    10    |    No    |    Warning: Failed to get MSFTESQL indexer interface for full-text catalog '%ls' ('%d') in database '%ls' ('%d'). Error: %ls.    |
|    9921    |    16    |    No    |    During upgrade fatal error 0x%x encountered in CoCreateGuid. Failed to resolve full-text catalog file name for '%ls'.    |
|    9922    |    10    |    No    |    Warning: Full-text population for table or indexed view '%ls' failed to send batch of data to MSFTESQL service (table or indexed view ID '%d', catalog ID '%d', database ID '%d'). Error: %ls.    |
|    9923    |    10    |    No    |    Warning: Full-text population for table or indexed view '%ls' reported low resources while sending a batch of data to MSFTESQL service (table or indexed view ID '%d', catalog ID '%d', database ID '%d'). Error: %ls.    |
|    9924    |    16    |    No    |    Rebuild full-text catalog '%ls' failed: Catalog header file is read-only.    |
|    9925    |    16    |    No    |    Rebuild full-text catalog '%ls' failed: Full-text catalog is read-only.    |
|    9926    |    10    |    No    |    Informational: MS Search stop limit reached. The full-text query may have returned fewer rows than it should.    |
|    9927    |    10    |    No    |    Informational: The full-text search condition contained noise word(s).    |
|    9928    |    16    |    No    |    Computed column '%.*ls' cannot be used for full-text search because it is nondeterministic or imprecise nonpersisted computed column.    |
|    9929    |    16    |    No    |    Computed column '%.*ls' cannot be used as full-text type column for image or varbinary(MAX) column. This computed column must be deterministic, precise or persisted, with a size less or equal than %d characters.    |
|    9930    |    10    |    No    |    Null document type provided. Row will not be full-text indexed.    |
|    9931    |    10    |    No    |    Document type exceeds the maximum permitted length. Row will not be full-text indexed.    |
|    9932    |    10    |    No    |    Document type value is malformed. Row will not be full-text indexed.    |
|    9933    |    10    |    No    |    Internal error: The row cannot be full-text indexed. The protocol handler was invoked out of sequence. This is an informational message only. No user action is required.    |
|    9934    |    10    |    No    |    Row was not found. It was deleted or updated while indexing was in progress.    |
|    9935    |    10    |    No    |    Warning: Wordbreaker, filter, or protocol handler used by catalog '%ls' does not exist on this instance. Use sp_help_fulltext_catalog_components and sp_help_fulltext_system_components check for mismatching components. Rebuild catalog is recommended.    |
|    9936    |    10    |    No    |    Informational: No full-text supported languages found.    |
|    9937    |    16    |    No    |    Too many full-text columns or the full-text query is too complex to be executed.    |
|    9938    |    16    |    No    |    Cannot find the specified user or role '%.*ls'.    |
|    9939    |    16    |    No    |    Current user or role '%.*ls' does not have the required permission to set the owner.    |
|    9940    |    10    |    No    |    Error: Full-text %ls population for table or indexed view '%ls' (table or indexed view ID '%d', database ID '%d') is terminated due to the preceding error.    |
|    9941    |    10    |    No    |    Informational: Full-text %ls population for table or indexed view '%ls' (table or indexed view ID '%d', database ID '%d') is being suspended by the system as the database is unavailable. System will resume the population whenever the database is available    |
|    9942    |    10    |    No    |    Informational: Full-text %ls population for table or indexed view '%ls' (table or indexed view ID '%d', database ID '%d') was cancelled by user.    |
|    9943    |    10    |    No    |    Informational: Full-text %ls population completed for table or indexed view '%ls' (table or indexed view ID '%d', database ID '%d'). Number of documents processed: %d. Number of documents failed: %d. Number of documents that will be retried: %d.    |
|    9944    |    10    |    No    |    Informational: Full-text retry pass of %ls population completed for table or indexed view '%ls' (table or indexed view ID '%d', database ID '%d'). Number of retry documents processed: %d. Number of documents failed: %d.    |
|    9945    |    10    |    No    |    Error: All Full-text populations in progress, for catalog '%ls' ('%d') in database '%ls' ('%d') were terminated due to error. Error: 0x%x.    |
|    9947    |    10    |    No    |    Warning: Identity of full-text catalog in directory '%ls' does not match database '%.*ls'. The full-text catalog cannot be attached.    |
|    9948    |    10    |    No    |    Warning: Full-text catalog path '%ls' is invalid. It exceeds the length limit, or it is a relative path, or it is a hidden directory. The full-text catalog cannot be attached.    |
|    9949    |    10    |    No    |    Warning: All Full-text populations in progress for full-text catalog '%ls' ('%d') in database '%ls' ('%d') are paused. Reason code: %d. Error: %ls. If this message occurs frequently, consult Books Online for indexing performance tuning assistance.    |
|    9950    |    10    |    No    |    Informational: Full-text catalog health monitor reported a failure for catalog '%ls' ('%d') in database '%ls' ('%d'). Reason code: %d. Error: %ls. The catalog is corrupted and all in-progress populations will be stopped. Use rebuild catalog to recover the failure and start population from scratch.    |
|    9951    |    10    |    No    |    Warning: Database %.*ls cannot be modified during detach because database is in read-only, standby, or shutdown state. Full-text catalog is not dropped, and '\@keepfulltextindexfile = false' is ignored.    |
|    9952    |    10    |    No    |    Informational: Full-text auto change tracking is turned off for table or indexed view '%ls' (table or indexed view ID '%d', database ID '%d') due to fatal crawl error.    |
|    9953    |    16    |    No    |    The path '%.*ls' has invalid attributes. It needs to be a directory. It must not be hidden, read-only, or on a removable drive.    |
|    9954    |    16    |    No    |    SQL Server failed to communicate with filter daemon launch service (Windows error: %ls). Full-Text filter daemon process failed to start. Full-text search functionality will not be available.    |
|    [9955](../../relational-databases/errors-events/mssqlserver-9955-database-engine-error.md)    |    16    |    No    |    SQL Server failed to create named pipe '%ls' to communicate with the full-text filter daemon (Windows error: %d). Either a named pipe already exists for a filter daemon host process, the system is low on resources, or the security identification number (SID) lookup for the filter daemon account group failed. To resolve this error, terminate any running full-text filter daemon processes, and if necessary reconfigure the full-text daemon launcher service account.    |
|    9959    |    16    |    No    |    Cannot perform requested task because full-text memory manager is not initialized.    |
|    9960    |    16    |    No    |    View '%.*ls' is not an indexed view. Full-text index is not allowed to be created on it.    |
|    9961    |    16    |    No    |    Logical name, size, maxsize, filegrowth, and offline properties of full-text catalog cannot be modified.    |
|    9962    |    16    |    No    |    Failed to move full-text catalog from '%ls' to '%ls'. OS error '%ls'.    |
|    9963    |    10    |    No    |    Inconsistent accent sensitivity of full-text catalog is detected. Full-text catalog for catalog ID '%d', database ID '%d' is reset.    |
|    9964    |    16    |    No    |    Failed to finish full-text operation. Filegroup '%.*ls' is empty, read-only, or not online.    |
|    9965    |    16    |    No    |    NULL or Invalid type of value specified for '%ls' parameter.    |
|    9966    |    16    |    No    |    Cannot use full-text search in master, tempdb, or model database.    |
|    9967    |    10    |    No    |    A default full-text catalog does not exist in database '%.*ls' or user does not have permission to perform this action.    |
|    9968    |    10    |    No    |    Warning: No appropriate filter was found during full-text index population for table or indexed view '%ls' (table or indexed view ID '%d', database ID '%d'), full-text key value '%ls'. Some columns of the row were not indexed.    |
|    9969    |    10    |    No    |    Warning: No appropriate wordbreaker was found during full-text index population for table or indexed view '%ls' (table or indexed view ID '%d', database ID '%d'), full-text key value '%ls'. Neutral wordbreaker was used for some columns of the row.    |
|    9970    |    16    |    No    |    Couldn't complete full-text operation because full-text key for table or indexed view '%.*ls' is offline.    |
|    9971    |    10    |    No    |    Warning: No appropriate filter for embedded object was found during full-text index population for table or indexed view '%ls' (table or indexed view ID '%d', database ID '%d'), full-text key value '%ls'. Some embedded objects in the row could not be indexed.    |
|    9972    |    16    |    No    |    Database is not fully started up or it is not in an ONLINE state. Try the full-text DDL command again after database is started up and becomes ONLINE.    |
|    9973    |    10    |    No    |    Informational: Full-text %ls population paused for table or indexed view '%ls' (table or indexed view ID '%d', database ID '%d'). Number of documents processed: %d. Number of documents failed: %d.    |
|    9974    |    10    |    No    |    Warning: Only running full population can be paused. The command is ignored. Other type of population can just be stopped and it will continue when your start the same type of crawl again.    |
|    9975    |    10    |    No    |    Warning: Only paused full population can be resumed. The command is ignored.    |
|    9977    |    10    |    No    |    Warning: Last population complete time of full-text catalog in directory '%ls' does not match database '%.*ls'. The full-text catalog is attached and it may need to be repopulated.    |
|    9978    |    10    |    No    |    Warning: During upgrade full-text index on table '%ls' is disabled because at least one of full-text key column, full-text columns, or type columns is a non-deterministic or imprecise nonpersisted computed column.    |
|    9979    |    10    |    No    |    Warning: During upgrade full-text catalog '%ls' in database '%ls' is set as offline because it failed to be created at path '%ls'. Please fix the full-text catalog path and rebuild the full-text catalog after upgrade.    |
|    9980    |    16    |    No    |    Variable parameters can not be passed to fulltext predicates: contains, freetext and functions: containstable, freetexttable applied to remote table.    |
|    9982    |    16    |    No    |    Cannot use full-text search in user instance.    |
|    9983    |    16    |    No    |    The value '%ls' for the full-text component '%ls' is longer than the maximum permitted (%d characters). Please reduce the length of the value.    |
|    9984    |    10    |    No    |    Informational: Full-text %ls population paused for table or indexed view '%ls' (table or indexed view ID '%d', database ID '%d').    |
|    9998    |    16    |    No    |    The column '%.*ls' cannot be added to a full-text index. Full-text indexes are limited to 1024 columns. When you create a full-text index, add fewer columns.    |
|    9999    |    16    |    No    |    The column '%.*ls' in the table '%.*ls' cannot be used for full-text search because it is a sparse column set.    |
