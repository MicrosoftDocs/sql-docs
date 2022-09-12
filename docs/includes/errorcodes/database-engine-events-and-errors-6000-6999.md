---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/19/2022
ms.topic: include
---
| Error| Severity | Event Logged | Description|
| :------ | :------| :------| :----------------------------- |
|    6001    |    10    |    No    |    SHUTDOWN is waiting for %d process(es) to complete.    |
|    6004    |    10    |    No    |    User does not have permission to perform this action.    |
|    6005    |    10    |    No    |    SHUTDOWN is in progress.    |
|    6006    |    10    |    Yes    |    Server shut down by %.*ls from login %.*ls.    |
|    6007    |    10    |    No    |    The SHUTDOWN statement cannot be executed within a transaction or by a stored procedure.    |
|    6101    |    16    |    No    |    Process ID %d is not a valid process ID. Choose a number between 1 and %d.    |
|    6102    |    14    |    No    |    User does not have permission to use the KILL statement.    |
|    6104    |    16    |    No    |    Cannot use KILL to kill your own process.    |
|    6106    |    16    |    No    |    Process ID %d is not an active process ID.    |
|    6107    |    14    |    No    |    Only user processes can be killed.    |
|    6108    |    16    |    No    |    KILL SPID WITH COMMIT/ABORT is not supported by Microsoft SQL Server. Use KILL UOW WITH COMMIT/ABORT to resolve in-doubt distributed transactions involving Microsoft Distributed Transaction Coordinator (MS DTC).    |
|    6109    |    10    |    No    |    SPID %d: transaction rollback in progress. Estimated rollback completion: %d%%. Estimated time remaining: %d seconds.    |
|    6110    |    16    |    No    |    The distributed transaction with UOW %s does not exist.    |
|    6111    |    16    |    No    |    Another user has decided a different outcome for the distributed transaction associated with UOW %s.    |
|    6112    |    16    |    No    |    Distributed transaction with UOW %s is in prepared state. Only Microsoft Distributed Transaction Coordinator can resolve this transaction. KILL command failed.    |
|    6113    |    16    |    No    |    The distributed transaction associated with UOW %s is in PREPARE state. Use KILL UOW WITH COMMIT/ABORT syntax to kill the transaction instead.    |
|    6114    |    16    |    No    |    Distributed transaction with UOW %s is being used by another user. KILL command failed.    |
|    6115    |    16    |    No    |    KILL command cannot be used inside user transactions.    |
|    6117    |    16    |    No    |    There is a connection associated with the distributed transaction with UOW %s. First, kill the connection using KILL SPID syntax.    |
|    6118    |    16    |    No    |    The distributed transaction associated with UOW %s is not in PREPARED state. Use KILL UOW to kill the transaction instead.    |
|    6119    |    10    |    No    |    Distributed transaction with UOW %s is rolling back: estimated rollback completion: %d%%, estimated time left %d seconds.    |
|    6120    |    16    |    No    |    Status report cannot be obtained. Rollback operation for Process ID %d is not in progress.    |
|    6121    |    16    |    No    |    Status report cannot be obtained. Rollback operation for UOW %s is not in progress.    |
|    6200    |    16    |    No    |    Method "%ls" of type "%ls" in assembly "%.*ls" is marked as a mutator. Mutators cannot be used in the read-only portion of the query.    |
|    6201    |    16    |    No    |    Method "%ls" of type "%ls" in assembly "%.*ls" is not marked as a mutator. Only mutators can be used to update the value of a CLR type.    |
|    6202    |    16    |    No    |    Method "%ls" of type "%ls" in assembly "%.*ls" does not return a value.    |
|    6203    |    16    |    No    |    Method '%ls' of type '%ls' in assembly '%.*ls' cannot be marked as a mutator. A mutator method must be non-static, public, and returns void type.    |
|    6204    |    16    |    No    |    Trying to send a record with %d columns(s) in a result set with %d column(s).    |
|    6205    |    16    |    No    |    %s ASSEMBLY failed because assembly '%.*ls' was compiled with /UNSAFE option, but the assembly was not registered with the required PERMISSION_SET = UNSAFE option.    |
|    6206    |    16    |    No    |    Request submitted with too many parameters. The maximum number is %ld.    |
|    6207    |    16    |    No    |    Error converting %.*ls to fixed length binary type. The result would be padded and cannot be converted back.    |
|    6208    |    16    |    No    |    %s failed because the parameter count for the FillRow method should be one more than the SQL declaration for the table valued CLR function.    |
|    6209    |    16    |    No    |    Unsound ordering on CLR type "%.*ls": returning NULL on non-NULL inputs.    |
|    6210    |    16    |    No    |    CLR type '%.*ls' is not fully comparable.    |
|    6211    |    16    |    No    |    %s ASSEMBLY failed because type '%.*ls' in %.*ls assembly '%.*ls' has a static field '%.*ls'. Attributes of static fields in %.*ls assemblies must be marked readonly in Visual C#, ReadOnly in Visual Basic, or initonly in Visual C++ and intermediate language.    |
|    6212    |    16    |    No    |    %s ASSEMBLY failed because method '%.*ls' on type '%.*ls' in %.*ls assembly '%.*ls' is storing to a static field. Storing to a static field is not allowed in %.*ls assemblies.    |
|    6213    |    16    |    No    |    %s ASSEMBLY failed because method "%.*ls" on type "%.*ls" in %.*ls assembly "%.*ls" has a synchronized attribute. Explicit synchronization is not allowed in %.*ls assemblies.    |
|    6214    |    16    |    No    |    %s ASSEMBLY failed because assembly "%.*ls" has an unmanaged entry point.    |
|    6215    |    16    |    No    |    %s ASSEMBLY failed because method '%.*ls' on type '%.*ls' in %.*ls assembly '%.*ls' has invalid attribute 0x%x.    |
|    6216    |    16    |    No    |    %s ASSEMBLY failed because type "%.*ls" in %.*ls assembly "%.*ls" has a finalizer. Finalizers are not allowed in %.*ls assemblies.    |
|    6217    |    16    |    No    |    ALTER ASSEMBLY ADD FILE failed because the file, "%.*ls", being added is empty.    |
|    6218    |    16    |    No    |    %s ASSEMBLY for assembly '%.*ls' failed because assembly '%.*ls' failed verification. Check if the referenced assemblies are up-to-date and trusted (for external_access or unsafe) to execute in the database. CLR Verifier error messages if any will follow this message%.*ls    |
|    6219    |    16    |    No    |    %s ASSEMBLY failed because assembly source parameter %d has an unmanaged entry point.    |
|    6220    |    16    |    No    |    SELECT INTO failed because CLR type "%.*ls" does not exist in the target database.    |
|    6221    |    16    |    No    |    The cursor operation generated more than one row of different column metadata.    |
|    6222    |    16    |    No    |    Type "%.*ls.%.*ls" is marked for native serialization, but field "%.*ls" of type "%.*ls.%.*ls" is not valid for native serialization.    |
|    6223    |    16    |    No    |    Type "%.*ls.%.*ls" is marked for native serialization, but field "%.*ls" of type "%.*ls.%.*ls" is of type "%.*ls.%.*ls", which is not marked with "LayoutKind.Sequential". Native serialization requires the type to be marked with "LayoutKind.Sequential".    |
|    6224    |    16    |    No    |    Type "%.*ls.%.*ls" is marked for native serialization, but field "%.*ls" of type "%.*ls.%.*ls" is marked with "System.NonSerializedAttribute". Native serialization types cannot have fields marked with "System.NonSerializedAttribute".    |
|    6225    |    16    |    No    |    Type "%.*ls.%.*ls" is marked for native serialization, but field "%.*ls" of type "%.*ls.%.*ls" is of type "%.*ls.%.*ls" which is a non-value type. Native serialization types can only have fields of blittable types. If you wish to have a field of any other type, consider using different kind of serialization format, such as User Defined Serialization.    |
|    6226    |    16    |    No    |    Type "%.*ls.%.*ls" is marked for user-defined serialization, but does not implement the "%.*ls.%.*ls" interface.    |
|    6227    |    16    |    No    |    Type "%.*ls.%.*ls" is marked for native serialization, but field "%.*ls" of type "%.*ls.%.*ls" is of type "%.*ls.%.*ls", which is not blittable, or type "%.*ls.%.*ls" has a recursive definition.    |
|    6228    |    16    |    No    |    Type "%.*ls.%.*ls" is marked for native serialization, but it contains non-blittable fields.    |
|    6229    |    16    |    No    |    Type "%.*ls.%.*ls" is marked for native serialization. It is not marked with "LayoutKind.Sequential". Native serialization requires the type to be marked with "LayoutKind.Sequential".    |
|    6230    |    16    |    No    |    Type "%.*ls.%.*ls" is marked for native serialization, but field "%.*ls" of type "%.*ls.%.*ls" has field marshallers. Native serialization types cannot have field marshallers.    |
|    6231    |    16    |    No    |    Type "%.*ls.%.*ls" is marked for native serialization, but one of its base types "%.*ls.%.*ls" is not valid for native serialization.    |
|    6232    |    16    |    No    |    Type "%.*ls.%.*ls" is marked for native serialization, but base type "%.*ls.%.*ls" is not marked with "LayoutKind.Sequential". Native serialization requires the type to be marked with "LayoutKind.Sequential".    |
|    6233    |    16    |    No    |    In proc data access is not allowed in an impersonated state.    |
|    6234    |    16    |    No    |    Data truncation error. Length (%d) exceeds maximum length (%d) for type '%.*ls'.    |
|    6235    |    16    |    No    |    Data serialization error. Length (%d) is less than fixed length (%d) for type '%.*ls'.    |
|    6236    |    16    |    No    |    %s ASSEMBLY failed because filename '%.*ls' is too long.    |
|    6237    |    16    |    No    |    %s ASSEMBLY failed because method "%.*ls" on type "%.*ls" in assembly "%.*ls" has an invalid custom attribute "%.*ls".    |
|    6238    |    16    |    No    |    %s ASSEMBLY failed because field "%.*ls" in type "%.*ls" in assembly "%.*ls" has an invalid custom attribute "%.*ls".    |
|    6239    |    16    |    No    |    %s ASSEMBLY failed because type "%.*ls" in assembly "%.*ls" has an invalid custom attribute "%.*ls".    |
|    6240    |    16    |    No    |    ALTER ASSEMBLY failed because the function '%s' of type '%s' no longer satisfies indexability requirements, and is used for the indexed view '%s'.    |
|    6241    |    16    |    No    |    Trying to send a record with type or name of column %d differing from the type or name of the corresponding column of the result set.    |
|    6242    |    16    |    No    |    CREATE ASSEMBLY failed because the user "%.*ls" specified in the authorization clause does not exist.    |
|    6243    |    16    |    No    |    '%.*ls.%.*ls' is marked for native serialization, and has the MaxByteSize property specified in the '%.*ls' attribute. Native serialization objects can not specify MaxByteSize property, it is calculated by SQL Server.    |
|    6244    |    16    |    No    |    The size (%d) for "%.*ls.%.*ls" is not in the valid range. Size must be -1 or a number between 1 and 8000.    |
|    6245    |    16    |    No    |    Invalid serialization format (%d) for type "%.*ls.%.*ls".    |
|    6246    |    16    |    No    |    Assembly "%.*ls" already exists in database "%.*ls".    |
|    6247    |    16    |    No    |    Cannot create type because '%.*ls.%.*ls' cannot have fixed length if it has MaxByteSize set to -1.    |
|    6248    |    16    |    No    |    %s failed because the type '%s' contains a method '%s' specified by SqlUserDefinedType.ValidateMethodName that does not conform to the required specification because it has an invalid signature.    |
|    6249    |    16    |    No    |    The associated file "%.*ls" already exists for assembly "%.*ls".    |
|    6250    |    11    |    No    |    Assembly "%.*ls" does not have an associated file "%.*ls".    |
|    6251    |    16    |    No    |    ALTER ASSEMBLY failed because the assembly file parameter %d is not a valid expression.    |
|    6252    |    16    |    No    |    ALTER ASSEMBLY failed because a file name was not specified for the inline assembly file parameter %d.    |
|    6253    |    10    |    Yes    |    Common language runtime (CLR) functionality initialized using CLR version %ls from %ls.    |
|    6254    |    10    |    Yes    |    Common language runtime (CLR) functionality initialized.    |
|    6255    |    16    |    No    |    %s failed because type "%s" does not conform to the %s specification: missing custom attribute "%.*ls".    |
|    6258    |    16    |    No    |    Function signature of "FillRow" method (as designated by SqlFunctionAttribute.FillRowMethodName) does not match SQL declaration for table valued CLR function'%.*ls' due to column %d.    |
|    6260    |    16    |    No    |    An error occurred while getting new row from user defined Table Valued Function : %.*ls.    |
|    6261    |    16    |    No    |    The CLR type referenced by column "%.*ls" of table variable "%.*ls" has been dropped during the execution of the batch. Run the batch again.    |
|    6262    |    16    |    No    |    Assembly was not found in current database or version did not match.    |
|    6263    |    16    |    No    |    Execution of user code in the .NET Framework is disabled. Enable "clr enabled" configuration option.    |
|    6264    |    16    |    No    |    Data access failed because the .NET Framework routine is not marked with "DataAccessKind.Read" or "SystemDataAccessKind.Read".    |
|    6265    |    16    |    No    |    %s ASSEMBLY failed because type "%.*ls" in %.*ls assembly "%.*ls" has a pinvokeimpl method. P/Invoke is not allowed in %.*ls assemblies.    |
|    6266    |    10    |    No    |    Warning: Assembly "%.*ls" was built using version %.*ls of the .NET Framework. SQL Server currently uses version %s.    |
|    6267    |    16    |    No    |    Assembly "%.*ls" does not exist, or the user does not have permission to reference it.    |
|    6269    |    16    |    No    |    ALTER ASSEMBLY failed because the user-defined aggregate "%s" does not exist or is not correctly defined in the updated assembly.    |
|    6270    |    16    |    No    |    ALTER ASSEMBLY failed because the required method "%s" in type "%s" was not found with the same signature in the updated assembly.    |
|    6271    |    16    |    No    |    ALTER ASSEMBLY failed because the required field "%s" in type "%s" was not found with the same signature in the updated assembly.    |
|    6272    |    16    |    No    |    ALTER ASSEMBLY failed because required property '%s' in type '%s' was not found with the same signature in the updated assembly.    |
|    6273    |    16    |    No    |    ALTER ASSEMBLY failed because required type '%s' does not exist or is not correctly defined in the updated assembly.    |
|    6274    |    16    |    No    |    ALTER ASSEMBLY failed because the serialization format of type '%s' would change in the updated assembly. Persisted types are not allowed to change serialization formats.    |
|    6275    |    16    |    No    |    ALTER ASSEMBLY failed because the IsByteOrdered attribute of type '%s' would change in the updated assembly.    |
|    6276    |    16    |    No    |    ALTER ASSEMBLY failed because serialization data of type '%s' would change in the updated assembly. Persisted types are not allowed to change serialization data.    |
|    6277    |    16    |    No    |    ALTER ASSEMBLY failed because the MaxLen attribute of type '%s' would change in the updated assembly. Persisted types are not allowed to change MaxLen attribute.    |
|    6278    |    16    |    No    |    ALTER ASSEMBLY failed because the IsFixedLen attribute of type '%s' would change in the updated assembly. Persisted types are not allowed to change IsFixedLen attribute.    |
|    6279    |    16    |    No    |    ALTER ASSEMBLY failed because the mutator attribute of method '%s' in type '%s' would change in the updated assembly, and the method is in use by the schema-bound function or view '%s'.    |
|    6280    |    16    |    No    |    ALTER ASSEMBLY failed because table, view or constraint '%s' depends on this assembly. Use WITH UNCHECKED DATA to skip checking for persisted data.    |
|    6281    |    16    |    No    |    ALTER ASSEMBLY failed because only members of the sysadmin role can use WITH UNCHECKED DATA.    |
|    6282    |    16    |    No    |    ALTER ASSEMBLY failed because the referenced assemblies would change. The referenced assembly list must remain the same.    |
|    6283    |    16    |    No    |    ALTER ASSEMBLY failed because only the assembly revision version number is allowed to change.    |
|    6284    |    16    |    No    |    ALTER ASSEMBLY failed because it is referenced by object '%.*ls'. Assemblies that are referenced by SQL objects cannot be made invisible.    |
|    6285    |    16    |    No    |    %s ASSEMBLY failed because the source assembly is, according to MVID, identical to an assembly that is already registered under the name "%.*ls".    |
|    6286    |    16    |    No    |    '%s' ASSEMBLY failed because a different version of assembly '%s', referenced by assembly '%s', is already in the database.    |
|    6287    |    16    |    No    |    ALTER ASSEMBLY failed because the function '%s' of type '%s' no longer satisfies indexability requirements, and is used for the persisted computed column '%s' of table '%s'.    |
|    6288    |    16    |    No    |    ALTER ASSEMBLY has marked data as unchecked in one or more objects in database "%.*ls". Refer to column "has_unchecked_assembly_data" from system views "sys.tables" and "sys.views" to locate all such objects.    |
|    6289    |    16    |    Yes    |    Failed to allocate memory for common language runtime (CLR) functionality.    |
|    6290    |    10    |    Yes    |    AppDomain %i (%.*ls) unloaded.    |
|    6291    |    16    |    Yes    |    AppDomain %i (%.*ls) failed to unload with error code 0x%x.    |
|    6292    |    16    |    No    |    The transaction that is associated with this operation has been committed or rolled back. Retry with a different transaction.    |
|    6293    |    16    |    No    |    %.*ls.%.*ls.%.*ls: SqlFacetAttribute is invalid on a non-public member.    |
|    6294    |    16    |    No    |    %.*ls.%.*ls.%.*ls: %.*ls property of SqlFacetAttribute cannot be used in this context.    |
|    6295    |    16    |    No    |    %.*ls.%.*ls.%.*ls: %.*ls property of SqlFacetAttribute has an invalid value.    |
|    6296    |    16    |    No    |    %.*ls.%.*ls.%.*ls : SqlFacetAttribute cannot be applied to a property getter or a property setter. It should be applied to the property itself.    |
|    6297    |    16    |    No    |    %.*ls.%.*ls.%.*ls: The SqlFacetAttribute property IsFixedLength cannot be set to true when MaxSize is set to -1.    |
|    6298    |    16    |    No    |    %.*ls.%.*ls.%.*ls: The SqlFacetAttribute properties Precision and Scale have to be used together.    |
|    6299    |    10    |    No    |    AppDomain %i (%.*ls) created.    |
|    6302    |    16    |    No    |    The argument of CREATE or ALTER XML SCHEMA COLLECTION statement must be a string expression.    |
|    6303    |    16    |    No    |    XML parsing: Document parsing required too much memory    |
|    6304    |    16    |    No    |    XML parsing: An unexpected error has occurred in the XML parser.    |
|    6305    |    16    |    No    |    XQuery data manipulation expression required in XML data type method.    |
|    6306    |    16    |    No    |    Invalid XQuery expression passed to XML data type method.    |
|    6307    |    16    |    No    |    XML well-formedness check: Attribute cannot appear outside of element declaration. Rewrite your XQuery so it returns well-formed XML.    |
|    6308    |    16    |    No    |    XML well-formedness check: Duplicate attribute '%.*ls'. Rewrite your XQuery so it returns well-formed XML.    |
|    6309    |    16    |    No    |    XML well-formedness check: the data for node '%.*ls' contains a character (0x%04X) which is not allowed in XML.    |
|    6310    |    16    |    No    |    Altering existing schema components is not allowed. There was an attempt to modify an existing XML Schema component, component namespace: '%.*ls' component name: '%.*ls' component kind:%.*ls    |
|    6311    |    16    |    No    |    An internal XMLDB schema processor error occurred. Contact Technical Support for assistance.    |
|    6312    |    16    |    No    |    Could not find schema components with target namespace '%.*ls' in collection '%.*ls'.    |
|    6314    |    16    |    No    |    Collection specified does not exist in metadata : '%.*ls'    |
|    6315    |    16    |    No    |    XQuery: Cannot update with value '%.*ls' as the canonical form of type '{%.*ls}%.*ls' violates the required pattern. It is recommended that you not use pattern facets on non-string types.    |
|    6316    |    16    |    No    |    Specified component '%s' can not be dropped because it is used by component:'%s'    |
|    6317    |    16    |    No    |    XQuery: Cannot update with value '%.*ls' because it failed validation against type '{%.*ls}%.*ls'    |
|    6318    |    16    |    No    |    XQuery: String conversion failed during UPDATE validation    |
|    6320    |    16    |    No    |    XQuery: Only nillable elements or text nodes can be updated with empty sequence    |
|    6321    |    16    |    No    |    xml:space attribute must have a value of 'preserve' or 'default'. '%.*ls' is not valid.    |
|    6322    |    16    |    No    |    XML Parser ran out of memory. This could be caused by too many attributes or namespace declarations.    |
|    6323    |    16    |    No    |    The xml schema collection for variable '%.*ls' has been altered while the batch was being executed. Remove all XML schema collection DDL operations it is dependent on from the batch, and re-run the batch.    |
|    6324    |    16    |    No    |    DROP XML INDEX does not support any options.    |
|    6325    |    16    |    No    |    XQuery: Replacing the value of a node with an empty sequence is allowed only if '()' is used as the new value expression. The new value expression evaluated to an empty sequence but it is not '()'.    |
|    6326    |    16    |    No    |    XML well-formedness check: XML namespace declaration cannot appear outside of element declaration. Rewrite your XQuery so it returns well-formed XML.    |
|    6327    |    16    |    No    |    The specified xml schema collection ID is not valid: %d    |
|    6328    |    16    |    No    |    Specified collection '%.*ls' cannot be dropped because it is used by %S_MSG '%ls'.    |
|    6329    |    16    |    No    |    Unsupported usage of a QName typed value in node '%.*ls'    |
|    6330    |    16    |    No    |    Column '%.*ls' on table '%.*ls' is not of type XML, which is required to create an XML index on it.    |
|    6331    |    16    |    No    |    Primary XML Index '%.*ls' already exists on column '%.*ls' on table '%.*ls', and multiple Primary XML Indexes per column are not allowed.    |
|    6332    |    16    |    No    |    Table '%.*ls' needs to have a clustered primary key with less than %d columns in it in order to create a primary XML index on it.    |
|    6333    |    16    |    No    |    Could not find%ls XML index named '%.*ls' on table '%.*ls'    |
|    6334    |    16    |    No    |    Could not create the XML or spatial index on object '%.*ls' because that object is not a table. Create the index on the base table column.    |
|    6335    |    16    |    No    |    XML datatype instance has too many levels of nested nodes. Maximum allowed depth is %d levels.    |
|    6336    |    16    |    No    |    Maximum size of primary index of table '%.*ls' is %d bytes. CREATE XML INDEX requires that such size should be limited to %d bytes    |
|    6337    |    16    |    No    |    '%.*ls' is not a valid XML Index name because it starts with '%c' character. XML Index name should not start with '#' or '\@'    |
|    6338    |    10    |    No    |    XML DTD has been stripped from one or more XML fragments. External subsets, if any, have been ignored.    |
|    6339    |    16    |    No    |    Specified collection '%.*ls' cannot be modified because it is SQL Server built-in XML Schema Collection.    |
|    6340    |    16    |    No    |    Xml schema collection '%.*ls' referenced by table variable '%.*ls' has been dropped or altered during the execution of the batch. Please re-run the batch.    |
|    6341    |    16    |    No    |    Xml schema collection referenced by column '%.*ls' of table variable '%.*ls' has been dropped or altered during the execution of the batch. Please re-run the batch.    |
|    6342    |    16    |    No    |    Cannot create primary xml or spatial index '%.*ls' on table '%.*ls', column '%.*ls', because the column is computed.    |
|    6343    |    16    |    No    |    Cannot create secondary xml index '%.*ls' without a USING XML INDEX clause.    |
|    6344    |    16    |    No    |    The primary xml index '%.*ls' does not exist on table '%.*ls' column '%.*ls'.    |
|    6345    |    16    |    No    |    The sparse column set '%.*ls' in the table '%.*ls' cannot be indexed by an XML index.    |
|    6346    |    16    |    No    |    Cannot convert a primary XML index to a secondary XML index using the DROP_EXISTING option. '%.*ls' is a primary XML index.    |
|    6347    |    16    |    No    |    Specified collection '%.*ls' cannot be altered because it does not exist or you do not have permission.    |
|    6348    |    16    |    No    |    Specified collection '%.*ls' cannot be created because it already exists or you do not have permission.    |
|    6350    |    16    |    No    |    The definition for xml schema collection '%.*ls' has changed.    |
|    6351    |    16    |    No    |    The xml schema collection for return parameter of module '%.*ls' has been altered while the batch was being executed. Please re-run the batch.    |
|    6352    |    16    |    No    |    Invalid parameter specified. XML Schema Collections can only be created from a string literal, or from a variable typed as a string or untyped XML.    |
|    6353    |    16    |    No    |    Serialization of built-in schemata is not supported.    |
|    6354    |    16    |    No    |    Target string size is too small to represent the XML instance    |
|    6355    |    16    |    No    |    Conversion of one or more characters from XML to target collation impossible    |
|    6356    |    16    |    No    |    Failed to load DLL. Make sure xmlrw.dll exists in the SQL Server installation.    |
|    6357    |    16    |    No    |    Internal error: cannot locate CreateInfoSetReaderEx in xmlrw.dll. You may have an incorrect version of xmlrw.dll.    |
|    6358    |    16    |    No    |    %d is not a valid style number when converting to XML.    |
|    6359    |    16    |    No    |    Parsing XML with internal subset DTDs not allowed. Use CONVERT with style option 2 to enable limited internal subset DTD support.    |
|    6360    |    16    |    No    |    %d is not a valid style number when converting from XML.    |
|    6361    |    16    |    No    |    Invalid null parameter specified. XML Schema Collections can only be created from a non-null value.    |
|    6362    |    16    |    No    |    Alter schema collection cannot be performed because the current schema has a lax wildcard or an element of type xs:anyType.    |
|    6363    |    16    |    No    |    ALTER SCHEMA COLLECTION failed. It cannot be performed on a schema collection that allows laxly validated content and is schema bound. Remove the schema binding before trying to alter the collection.    |
|    6364    |    16    |    No    |    ALTER SCHEMA COLLECTION failed. Revalidation of XML columns in table '%.*ls' did not succeed due to the following reason: '%.*ls'. Either the schema or the specified data should be altered so that validation does not find any mismatches.    |
|    6365    |    16    |    No    |    An XML operation resulted an XML data type exceeding 2GB in size. Operation aborted.    |
|    6401    |    16    |    No    |    Cannot roll back %.*ls. No transaction or savepoint of that name was found.    |
|    6500    |    16    |    No    |    %ls failed because method '%ls' of class '%ls' in assembly '%ls' returns %ls, but CLR Triggers must return void.    |
|    6501    |    16    |    No    |    %s ASSEMBLY failed because it could not open the physical file "%.*ls": %ls.    |
|    6502    |    16    |    No    |    %s ASSEMBLY failed because it could not read from the physical file '%.*ls': %ls.    |
|    6503    |    16    |    No    |    Assembly '%.*ls' was not found in the SQL catalog.    |
|    6504    |    16    |    No    |    The value returned from %.*ls.%.*ls is not allowed to be NULL.    |
|    6505    |    16    |    No    |    Could not find Type '%s' in assembly '%s'.    |
|    6506    |    16    |    No    |    Could not find method '%s' for type '%s' in assembly '%s'    |
|    6507    |    16    |    No    |    Failed to open malformed assembly '%ls' with HRESULT 0x%x.    |
|    6508    |    16    |    No    |    Could not find field '%s' for type '%s' in assembly '%s'.    |
|    6509    |    16    |    No    |    An error occurred while gathering metadata from assembly '%ls' with HRESULT 0x%x.    |
|    6510    |    16    |    Yes    |    Common Language Runtime (CLR) %ls not installed properly. The CLR is required to use SQL/CLR features.    |
|    6511    |    16    |    Yes    |    Failed to initialize the Common Language Runtime (CLR) %ls with HRESULT 0x%x. You may fix the problem and try again later.    |
|    6512    |    16    |    Yes    |    Failed to initialize the Common Language Runtime (CLR) %ls with HRESULT 0x%x. You need to restart SQL Server to use CLR integration features.    |
|    6513    |    16    |    Yes    |    Failed to initialize the Common Language Runtime (CLR) %ls due to memory pressure. This is probably due to memory pressure in the MemToLeave region of memory. For more information, see the CLR integration documentation in SQL Server Books Online.    |
|    6514    |    16    |    No    |    Cannot use '%s' column in the result table of a streaming user-defined function (column '%.*ls').    |
|    6515    |    16    |    No    |    Schema collection database '%.*ls' does not exist or you do not have permission.    |
|    6516    |    16    |    No    |    There is no collection '%.*ls' in metadata '%.*ls'.    |
|    6517    |    16    |    Yes    |    Failed to create AppDomain "%.*ls". %.*ls    |
|    6518    |    16    |    No    |    Could not open system assembly ''%.*ls'': %ls.    |
|    6519    |    16    |    No    |    Type '%.*ls' is not yet supported for CLR operations.    |
|    6520    |    16    |    No    |    A .NET Framework error occurred during statement execution.    |
|    6521    |    16    |    No    |    A .NET Framework error occurred during statement execution: %.*ls.    |
|    [6522](../../relational-databases/errors-events/mssqlserver-6522-database-engine-error.md)    |    16    |    No    |    A .NET Framework error occurred during execution of user-defined routine or aggregate "%.*ls": %ls.    |
|    6523    |    16    |    No    |    Method, property or field '%ls' of class '%ls' in assembly '%.*ls' is static.    |
|    6524    |    16    |    No    |    Cannot use computed column in the result table of a streaming user-defined function (column '%.*ls').    |
|    6525    |    16    |    No    |    Cannot use '%s' constraint in the result table of a streaming user-defined function.    |
|    6526    |    16    |    No    |    Cannot use '%s' constraint in the result table of a streaming user-defined function (column '%.*ls').    |
|    6527    |    10    |    Yes    |    .NET Framework runtime has been stopped.    |
|    6528    |    16    |    No    |    Assembly '%.*ls' was not found in the SQL catalog of database '%.*ls'.    |
|    6529    |    16    |    No    |    ALTER ASSEMBLY failed because the identity of referenced assembly '%.*ls' has changed. Make sure the version, name, and public key have not changed.    |
|    6530    |    16    |    No    |    Cannot perform alter on '%.*ls' because it is an incompatible object type.    |
|    6531    |    16    |    No    |    %ls failed because the function '%ls' of class '%ls' of assembly '%.*ls' takes one or more parameters but CLR Triggers do not accept parameters.    |
|    6532    |    16    |    Yes    |    .NET Framework execution was aborted by escalation policy because of out of memory. %.*ls    |
|    6533    |    16    |    Yes    |    AppDomain %.*ls was unloaded by escalation policy to ensure the consistency of your application. Out of memory happened while accessing a critical resource. %.*ls    |
|    6534    |    16    |    Yes    |    AppDomain %.*ls was unloaded by escalation policy to ensure the consistency of your application. Application failed to release a managed lock. %.*ls    |
|    6535    |    16    |    No    |    .NET Framework execution was aborted. Another query caused the AppDomain %.*ls to be unloaded. %.*ls    |
|    6536    |    16    |    Yes    |    A fatal error occurred in the .NET Framework common language runtime. SQL Server is shutting down. If the error recurs after the server is restarted, contact Customer Support Services.    |
|    6537    |    16    |    Yes    |    The .NET Framework common language runtime was shut down by user code, such as in a user-defined function or CLR type. SQL Server is shutting down. Environment.Exit should not be used to exit the process. If the intent is to return an integer to indicate failure, use a scalar function or an output parameter instead.    |
|    6538    |    16    |    Yes    |    .NET Framework execution was aborted because of stack overflow. %.*ls    |
|    6539    |    16    |    No    |    Invalid serialization format (Format.Unknown) for type '%.*ls.%.*ls'.    |
|    6540    |    16    |    No    |    The assembly name '%.*ls' being registered has an illegal name that duplicates the name of a system assembly.    |
|    6541    |    16    |    No    |    ALTER ASSEMBLY failed because assembly '%.*ls' has more than one file associated with it. Use ALTER ASSEMBLY DROP FILE to remove extra files.    |
|    6542    |    16    |    No    |    Can not create object because %ls is a generic type.    |
|    6543    |    16    |    No    |    .NET Framework execution was aborted. The UDP/UDF/CLR type did not end thread affinity.    |
|    6544    |    16    |    No    |    %s ASSEMBLY for assembly '%.*ls' failed because assembly '%.*ls' is malformed or not a pure .NET assembly. %.*ls    |
|    6545    |    16    |    No    |    Enabling of execution statistics SET options is not allowed from within CLR procedure or function.    |
|    6546    |    16    |    No    |    Could not impersonate the execution context during the execution of '%.*ls'.    |
|    6547    |    16    |    No    |    An error occurred while getting method, property or field information for "%ls" of class "%ls" in assembly "%.*ls".    |
|    6548    |    16    |    No    |    CREATE ASSEMBLY failed because the assembly references assembly '%.*ls', which is owned by another user.    |
|    6549    |    16    |    No    |    A .NET Framework error occurred during execution of user defined routine or aggregate '%.*ls': %ls. User transaction, if any, will be rolled back.    |
|    6550    |    16    |    No    |    %s failed because parameter counts do not match.    |
|    6551    |    16    |    No    |    %s for "%.*ls" failed because T-SQL and CLR types for return value do not match.    |
|    6552    |    16    |    No    |    %s for "%.*ls" failed because T-SQL and CLR types for parameter "%.*ls" do not match.    |
|    6553    |    16    |    No    |    %s failed because of an invalid .NET Framework calling convention. Use the default .NET Framework calling convention.    |
|    6554    |    16    |    No    |    SQL assembly name '%.*ls', and .NET Framework assembly name '%.*ls' do not match. Assembly names must match.    |
|    6555    |    16    |    No    |    Assembly '%.*ls' already exists for owner '%.*ls' in database '%.*ls'.    |
|    6556    |    16    |    No    |    %s failed because it could not find type '%s' in assembly '%s'.    |
|    6557    |    16    |    No    |    %s failed because type '%s' does not conform to %s specification due to field '%s'.    |
|    6558    |    16    |    No    |    %s failed because type '%s' does not conform to %s specification due to method '%s'.    |
|    6559    |    20    |    Yes    |    Could not find type ID %d in database %.*ls. This is due to a schema inconsistency.    |
|    6560    |    16    |    No    |    Assembly "%.*ls" is a system assembly. This operation is permitted only with user assemblies.    |
|    6561    |    16    |    No    |    Could not find file '%s' in directory '%s%s'.    |
|    6562    |    16    |    No    |    Version mismatch between files '%s' (%d.%d.%d) and '%s' (%d.%d.%d).    |
|    6563    |    16    |    No    |    Method, property or field '%ls' in class '%ls' in assembly '%.*ls' has invalid return type.    |
|    6564    |    16    |    No    |    The method '%ls' in class '%ls' in assembly '%.*ls' has some invalid arguments. Value of type '%ls' is not valid for argument number %d.    |
|    6565    |    16    |    No    |    %s ASSEMBLY failed because the assembly source parameter %d is not a valid assembly.    |
|    6566    |    16    |    No    |    %s ASSEMBLY failed because the assembly source parameter %d is not a valid expression.    |
|    6567    |    16    |    No    |    %s failed because a CLR Procedure may only be defined on CLR methods that return either SqlInt32, System.Int32, System.Nullable<System.Int32>, void.    |
|    6568    |    16    |    No    |    A .NET Framework error occurred while getting information from class "%.*ls" in assembly "%.*ls": %ls.    |
|    6569    |    16    |    No    |    '%.*ls' failed because parameter %d is not allowed to be null.    |
|    6570    |    16    |    No    |    Method '%ls' of class '%ls' in assembly '%.*ls' is generic. Generic methods are not supported.    |
|    6571    |    16    |    No    |    Class '%ls' in assembly '%.*ls' is generic. Generic types are not supported.    |
|    6572    |    16    |    No    |    More than one method, property or field was found with name '%ls' in class '%ls' in assembly '%.*ls'. Overloaded methods, properties or fields are not supported.    |
|    6573    |    16    |    No    |    Method, property or field '%ls' of class '%ls' in assembly '%.*ls' is not static.    |
|    6574    |    16    |    No    |    Method, property or field '%ls' of class '%ls' in assembly '%.*ls' is not public.    |
|    6575    |    16    |    No    |    Assembly names should be less than %d characters. Assembly name '%.*ls' is too long.    |
|    6576    |    16    |    No    |    Type '%ls' in assembly '%ls' is not public.    |
|    6577    |    16    |    No    |    CREATE TYPE failed because type '%s' does not conform to CLR type specification due to interface '%s'.    |
|    6578    |    16    |    No    |    Invalid attempt to continue operation after a severe error.    |
|    6579    |    16    |    No    |    Alter assembly from '%ls' to '%ls' is not a compatible upgrade.    |
|    6580    |    16    |    No    |    Declarations do not match for parameter %d. .NET Framework reference and T-SQL OUTPUT parameter declarations must match.    |
|    6581    |    16    |    No    |    Could not find assembly '%.*ls' in directory '%.*ls'.    |
|    6582    |    16    |    No    |    Assembly '%.*s' is not visible for creating SQL objects. Use ALTER ASSEMBLY to change the assembly visibility.    |
|    6583    |    16    |    No    |    Assembly '%.*s' cannot be used for creating SQL objects because it is a system assembly.    |
|    6584    |    16    |    No    |    Property or field '%ls' for type '%ls' in assembly '%ls' is not static    |
|    6585    |    16    |    No    |    Could not impersonate the client during assembly file operation.    |
|    6586    |    16    |    No    |    Assembly '%.*ls' could not be installed because existing policy would keep it from being used.    |
|    6587    |    16    |    No    |    Assembly reference '%ls' was redirected by external policy to '%ls'    |
|    6588    |    16    |    No    |    Assembly file operations are not allowed for Windows NT users activated by SETUSER.    |
|    6589    |    16    |    No    |    DROP ASSEMBLY failed because the specified assemblies are referenced by assembly '%ls'.    |
|    6590    |    16    |    No    |    DROP ASSEMBLY failed because '%ls' is referenced by object '%ls'.    |
|    6591    |    16    |    No    |    %s for "%.*ls" failed because first parameter of "%.*ls" method must be of type System.Object.    |
|    6592    |    16    |    No    |    Could not find property or field '%ls' for type '%ls' in assembly '%ls'.    |
|    6593    |    16    |    No    |    Property or field '%ls' for type '%ls' in assembly '%ls' is static.    |
|    6594    |    16    |    No    |    Could not read from property '%ls' for type '%ls' in assembly '%ls' because it does not have a get accessor.    |
|    6595    |    16    |    No    |    Could not assign to property '%ls' for type '%ls' in assembly '%ls' because it is read only.    |
|    6596    |    16    |    No    |    %s ASSEMBLY failed because assembly '%ls' is a system assembly. Consider creating a user assembly to wrap desired functionality.    |
|    6597    |    16    |    No    |    CREATE %s failed.    |
|    6598    |    16    |    No    |    DROP ASSEMBLY failed because '%ls' is referenced by CLR type '%ls'.    |
|    6599    |    16    |    No    |    Found an empty native serialization class '%.*ls'. Empty native serialization classes are not allowed.    |
|    6600    |    16    |    No    |    XML error: %.*ls    |
|    6601    |    10    |    No    |    The XML parse error 0x%x occurred on line number %d, near the XML text "%.*ls".    |
|    [6602](../../relational-databases/errors-events/mssqlserver-6602-database-engine-error.md)    |    16    |    No    |    The error description is '%.*ls'.    |
|    6603    |    16    |    No    |    XML parsing error: %.*ls    |
|    6605    |    16    |    No    |    %.*ls: Failed to obtain an IPersistStream interface on the XML text.    |
|    6607    |    16    |    No    |    %.*ls: The value supplied for parameter number %d is invalid.    |
|    6608    |    16    |    No    |    Failed to instantiate class "%ls". Verify that Msxmlsql.dll exists in the SQL Server installation.    |
|    6609    |    16    |    No    |    Invalid data type for the column "%ls". Allowed data types are CHAR/VARCHAR, NCHAR/NVARCHAR, TEXT/NTEXT, and XML.    |
|    6610    |    17    |    No    |    Failed to load Msxmlsql.dll.    |
|    6611    |    16    |    No    |    The XML data type is damaged.    |
|    6613    |    16    |    No    |    Specified value '%ls' already exists.    |
|    6621    |    16    |    No    |    XML encoding or decoding error occurred with object name '%.*ls'.    |
|    6622    |    16    |    No    |    Invalid data type for column "%ls". The data type cannot be text, ntext, image, binary, varchar(max), nvarchar(max), varbinary(max), or xml.    |
|    6623    |    16    |    No    |    Column '%ls' contains an invalid data type. Valid data types are char, varchar, nchar, and nvarchar.    |
|    6624    |    16    |    No    |    XML document could not be created because server memory is low. Use sp_xml_removedocument to release XML documents.    |
|    6625    |    16    |    No    |    Could not convert the value for OPENXML column '%ls' to sql_variant data type. The value is too long. Change the data type of this column to text, ntext or image.    |
|    6626    |    16    |    No    |    Unexpected end of data stream.    |
|    6627    |    16    |    No    |    The size of the data chunk that was requested from the stream exceeds the allowed limit.    |
|    6628    |    16    |    No    |    %.*ls can only process untyped XML. Cast the input value to XML or to a string type.    |
|    6629    |    16    |    No    |    The result of the column expression for column "%ls" is not compatible with the requested type "XML". The result must be an element, text node, comment node, processing instruction, or document node.    |
|    6630    |    16    |    No    |    Element-centric mapping must be used with OPENXML when one of the columns is of type XML.    |
|    6631    |    16    |    No    |    The requested OpenXML document is currently in use by another thread, and cannot be used.    |
|    6632    |    16    |    No    |    Invalid data type for the column "%ls". CLR types cannot be used in an OpenXML WITH clause.    |
|    6633    |    16    |    No    |    The version of MSXMLSQL.DLL that was found is older than the minimum required version. Found version "%d.%d.%d". Require version "%d.%d.%d".    |
|    6634    |    16    |    No    |    OpenXML cannot be used as the target of a DML or OUTPUT INTO operation.    |
|    6700    |    16    |    No    |    XQuery: The ' %ls' operation is not supported.    |
|    6701    |    16    |    No    |    The version of the XML index that you are trying to use is not supported anymore. Please drop and recreate the XML index.    |
|    6716    |    16    |    No    |    XML Node ID is invalid. Re-build the database if the problem persists.    |
|    6717    |    16    |    No    |    XQuery: The document tree is too deep. If the problem persists you must simplify the XML hierarchy.    |
|    6718    |    16    |    No    |    XQuery: Invalid ordpath string: "%s"    |
|    6739    |    16    |    No    |    XQuery: SQL type '%s' is not supported in XQuery.    |
|    6743    |    16    |    No    |    XQuery: The maximum allowed depth in XML instances is %d levels. One of the paths in the query tries to access nodes at a lower level.    |
|    6744    |    16    |    No    |    XQuery: One of the paths specified in the query is too deep. The maximum allowed depth is %d levels.    |
|    6745    |    16    |    No    |    XQuery: Internal compiler error.    |
|    6800    |    16    |    No    |    FOR XML AUTO requires at least one table for generating XML tags. Use FOR XML RAW or add a FROM clause with a table name.    |
|    6801    |    16    |    No    |    FOR XML EXPLICIT requires at least three columns, including the tag column, the parent column, and at least one data column.    |
|    6802    |    16    |    No    |    FOR XML EXPLICIT query contains the invalid column name '%.*ls'. Use the TAGNAME!TAGID!ATTRIBUTENAME[!..] format where TAGID is a positive integer.    |
|    6803    |    16    |    No    |    FOR XML EXPLICIT requires the first column to hold positive integers that represent XML tag IDs.    |
|    6804    |    16    |    No    |    FOR XML EXPLICIT requires the second column to hold NULL or nonnegative integers that represent XML parent tag IDs.    |
|    6805    |    16    |    No    |    FOR XML EXPLICIT stack overflow occurred. Circular parent tag relationships are not allowed.    |
|    6806    |    16    |    No    |    Undeclared tag ID %d is used in a FOR XML EXPLICIT query.    |
|    6807    |    16    |    No    |    Undeclared parent tag ID %d is used in a FOR XML EXPLICIT query.    |
|    6808    |    16    |    No    |    XML tag ID %d could not be added. The server memory resources may be low.    |
|    6809    |    16    |    No    |    Unnamed tables cannot be used as XML identifiers as well as unnamed columns cannot be used for attribute names. Name unnamed columns/tables using AS in the SELECT statement.    |
|    6810    |    16    |    No    |    Column name '%.*ls' is repeated. The same attribute cannot be generated more than once on the same XML tag.    |
|    6811    |    16    |    No    |    FOR XML is incompatible with COMPUTE expressions. Remove the COMPUTE expression.    |
|    6812    |    16    |    No    |    XML tag ID %d that was originally declared as '%.*ls' is being redeclared as '%.*ls'.    |
|    6813    |    16    |    No    |    FOR XML EXPLICIT cannot combine multiple occurrences of ID, IDREF, IDREFS, NMTOKEN, and/or NMTOKENS in column name '%.*ls'.    |
|    6814    |    16    |    No    |    In the FOR XML EXPLICIT clause, ID, IDREF, IDREFS, NMTOKEN, and NMTOKENS require attribute names in '%.*ls'.    |
|    6815    |    16    |    No    |    In the FOR XML EXPLICIT clause, ID, IDREF, IDREFS, NMTOKEN, and NMTOKENS attributes cannot be hidden in '%.*ls'.    |
|    6816    |    16    |    No    |    In the FOR XML EXPLICIT clause, ID, IDREF, IDREFS, NMTOKEN, and NMTOKENS attributes cannot be generated as CDATA, XML, or XMLTEXT in '%.*ls'.    |
|    6817    |    16    |    No    |    FOR XML EXPLICIT cannot combine multiple occurrences of ELEMENT, XML, XMLTEXT, and CDATA in column name '%.*ls'.    |
|    6819    |    16    |    No    |    The FOR XML clause is not allowed in a %ls statement.    |
|    6820    |    16    |    No    |    FOR XML EXPLICIT requires column %d to be named '%ls' instead of '%.*ls'.    |
|    6821    |    16    |    No    |    GROUP BY and aggregate functions are currently not supported with FOR XML AUTO.    |
|    6824    |    16    |    No    |    In the FOR XML EXPLICIT clause, mode '%.*ls' in a column name is invalid.    |
|    6825    |    16    |    No    |    ELEMENTS option is only allowed in RAW, AUTO, and PATH modes of FOR XML.    |
|    6826    |    16    |    No    |    Every IDREFS or NMTOKENS column in a FOR XML EXPLICIT query must appear in a separate SELECT clause, and the instances must be ordered directly after the element to which they belong.    |
|    6827    |    16    |    No    |    FOR XML EXPLICIT queries allow only one XMLTEXT column per tag. Column '%.*ls' declares another XMLTEXT column that is not permitted.    |
|    6828    |    16    |    No    |    XMLTEXT column '%.*ls' must be of a string data type or of type XML.    |
|    6829    |    16    |    No    |    FOR XML EXPLICIT and RAW modes currently do not support addressing binary data as URLs in column '%.*ls'. Remove the column, or use the BINARY BASE64 mode, or create the URL directly using the 'dbobject/TABLE[\@PK1="V1"]/\@COLUMN' syntax.    |
|    6830    |    16    |    No    |    FOR XML AUTO could not find the table owning the following column '%.*ls' to create a URL address for it. Remove the column, or use the BINARY BASE64 mode, or create the URL directly using the 'dbobject/TABLE[\@PK1="V1"]/\@COLUMN' syntax.    |
|    6831    |    16    |    No    |    FOR XML AUTO requires primary keys to create references for '%.*ls'. Select primary keys, or use BINARY BASE64 to obtain binary data in encoded form if no primary keys exist.    |
|    6832    |    16    |    No    |    FOR XML AUTO cannot generate a URL address for binary data if a primary key is also binary.    |
|    6833    |    16    |    No    |    Parent tag ID %d is not among the open tags. FOR XML EXPLICIT requires parent tags to be opened first. Check the ordering of the result set.    |
|    6834    |    16    |    No    |    XMLTEXT field '%.*ls' contains an invalid XML document. Check the root tag and its attributes.    |
|    6835    |    16    |    No    |    FOR XML EXPLICIT field '%.*ls' can specify the directive HIDE only once.    |
|    6836    |    16    |    No    |    FOR XML EXPLICIT requires attribute-centric IDREFS or NMTOKENS field '%.*ls' to precede element-centric IDREFS/NMTOKEN fields.    |
|    6838    |    16    |    No    |    Attribute-centric IDREFS or NMTOKENS field not supported on tags having element-centric field '%.*ls' of type TEXT/NTEXT or IMAGE. Either specify ELEMENT on IDREFS/NMTOKENS field or remove the ELEMENT directive.    |
|    6839    |    16    |    No    |    FOR XML EXPLICIT does not support XMLTEXT field on tag '%.*ls' that has IDREFS or NMTOKENS fields.    |
|    6840    |    16    |    No    |    Neither XMLDATA nor XMLSCHEMA supports namespace elements or attributes such as '%.*ls'. Run the SELECT FOR XML statement without it or remove the namespace prefix declaration.    |
|    6841    |    16    |    No    |    FOR XML could not serialize the data for node '%.*ls' because it contains a character (0x%04X) which is not allowed in XML. To retrieve this data using FOR XML, convert it to binary, varbinary or image data type and use the BINARY BASE64 directive.    |
|    6842    |    16    |    No    |    Could not serialize the data for node '%.*ls' because it contains a character (0x%04X) which is not allowed in XML. To retrieve this data convert it to binary, varbinary or image data type    |
|    6843    |    16    |    No    |    FOR XML EXPLICIT: XML data types and CLR types cannot be processed as CDATA in column name '%.*ls'. Consider converting XML to a string type. Consider converting CLR types to XML and then to a string type.    |
|    6844    |    16    |    No    |    Two (or more) elements named '%.*ls' are of different types and not direct siblings in the same level.    |
|    6845    |    16    |    No    |    Two (or more) elements named '%.*ls' are optional in the same level. Consider making them direct siblings or map NULL to xsi:nil    |
|    6846    |    16    |    No    |    XML name space prefix '%.*ls' declaration is missing for FOR XML %.*ls name '%.*ls'.    |
|    6847    |    16    |    No    |    The column '%.*ls' is of type sql_variant, which is not supported in attribute-centric FOR XML, with XML Schema.    |
|    6848    |    16    |    No    |    XMLDATA does not support the mapping of the type of column '%.*ls' to an XDR type. Please use XMLSCHEMA instead for AUTO and RAW mode.    |
|    6849    |    16    |    No    |    FOR XML PATH error in column '%.*ls' - '//' and leading and trailing '/' are not allowed in simple path expressions.    |
|    6850    |    16    |    No    |    %.*ls name '%.*ls' contains an invalid XML identifier as required by FOR XML; '%c'(0x%04X) is the first character at fault.    |
|    6851    |    16    |    No    |    Column '%.*ls' has invalid data type for attribute-centric XML serialization in FOR XML PATH.    |
|    6852    |    16    |    No    |    Attribute-centric column '%.*ls' must not come after a non-attribute-centric sibling in XML hierarchy in FOR XML PATH.    |
|    6853    |    16    |    No    |    Column '%.*ls': the last step in the path can't be applied to XML data type or CLR type in FOR XML PATH.    |
|    6854    |    16    |    No    |    Invalid column alias '%.*ls' for formatting column as XML processing instruction in FOR XML PATH - it must be in 'processing-instruction(target)' format.    |
|    6855    |    16    |    No    |    Inline schema is not supported with FOR XML PATH.    |
|    6856    |    16    |    No    |    FOR XML row XML tag name contains an invalid XML identifier; '%c'(0x%04X) is the first character at fault.    |
|    6857    |    16    |    No    |    FOR XML root XML tag name contains an invalid XML identifier; '%c'(0x%04X) is the first character at fault.    |
|    6858    |    16    |    No    |    XML schema URI contains character '%c'(0x%04X) which is not allowed in XML.    |
|    6859    |    16    |    No    |    Row tag name is only allowed with RAW or PATH mode of FOR XML.    |
|    6860    |    16    |    No    |    FOR XML directive XMLDATA is not allowed with ROOT directive or row tag name specified.    |
|    6861    |    16    |    No    |    Empty root tag name can't be specified with FOR XML.    |
|    6862    |    16    |    No    |    Empty FOR XML target inline schema URI is not allowed.    |
|    6863    |    16    |    No    |    Row tag omission (empty row tag name) is not compatible with XMLSCHEMA FOR XML directive.    |
|    6864    |    16    |    No    |    Row tag omission (empty row tag name) cannot be used with attribute-centric FOR XML serialization.    |
|    6865    |    16    |    No    |    FOR XML does not support CLR types - cast CLR types explicitly into one of the supported types in FOR XML queries.    |
|    6866    |    16    |    No    |    Use of a system reserved XML schema URI is not allowed.    |
|    6867    |    16    |    No    |    'xmlns' is invalid in XML tag name in FOR XML PATH, or when WITH XMLNAMESPACES is used with FOR XML.    |
|    6868    |    16    |    No    |    The following FOR XML features are not supported with WITH XMLNAMESPACES list: EXPLICIT mode, XMLSCHEMA and XMLDATA directives.    |
|    6869    |    16    |    No    |    Attempt to redefine namespace prefix '%.*ls'    |
|    6870    |    16    |    No    |    Prefix '%.*ls' used in WITH XMLNAMESPACES clause contains an invalid XML identifier. '%c'(0x%04X) is the first character at fault.    |
|    6871    |    16    |    No    |    Prefix '%.*ls' used in WITH XMLNAMESPACES is reserved and cannot be used as a user-defined prefix.    |
|    6872    |    16    |    No    |    XML namespace prefix 'xml' can only be associated with the URI http://www.w3.org/XML/1998/namespace. This URI cannot be used with other prefixes.    |
|    6873    |    16    |    No    |    Redefinition of 'xsi' XML namespace prefix is not supported with ELEMENTS XSINIL option of FOR XML.    |
|    6874    |    16    |    No    |    Empty URI is not allowed in WITH XMLNAMESPACES clause.    |
|    6875    |    16    |    No    |    URI '%.*ls' used in WITH XMLNAMESPACES is invalid. '%c'(0x%04X) is the first character at fault.    |
|    6876    |    16    |    No    |    URI used in WITH XMLNAMESPACES is too long. The maximum length is %d characters.    |
|    6877    |    16    |    No    |    Empty namespace prefix is not allowed in WITH XMLNAMESPACES clause.    |
|    6878    |    16    |    No    |    FORXML XMLSCHEMA cannot be used with a typed XML column whose schema collection is empty.    |
|    6879    |    16    |    No    |    'xml' is an invalid XML processing instruction target. Possible attempt to construct XML declaration using XML processing instruction constructor. XML declaration construction with FOR XML is not supported.    |
|    6901    |    16    |    No    |    XML Validation: XML instance must be a document.    |
|    6902    |    16    |    No    |    XML Validation: Invalid definition for type '%ls'. SQL Server does not currently support the use of the pattern or enumeration facet on lists of type QName.    |
|    6903    |    16    |    No    |    XML Validation: Invalid definition for type '%ls'. SQL Server does not currently support inclusion of ID, QName, or list of QName among the member types of a union type.    |
|    6904    |    16    |    No    |    XML Validation: Found duplicate attribute '%s'. Location: %s    |
|    6905    |    16    |    No    |    XML Validation: Attribute '%s' is not permitted in this context. Location: %s    |
|    6906    |    16    |    No    |    XML Validation: Required attribute '%s' is missing. Location: %s    |
|    6907    |    16    |    No    |    Namespace URI too long: '%.*ls'.    |
|    6908    |    10    |    No    |    XML Validation: Invalid content. Expected element(s): %s. Location: %s    |
|    6909    |    16    |    No    |    XML Validation: Text node is not allowed at this location, the type was defined with element only content or with simple content. Location: %s    |
|    6910    |    16    |    No    |    XML Validation: Invalid definition for type '%ls'. SQL Server does not currently support restriction of union types.    |
|    6911    |    16    |    No    |    XML Validation: Found duplicate element '%s' in all content model. Location: %s    |
|    6912    |    16    |    No    |    XML Validation: Element '%s' found in text only content model. Location: %s    |
|    6913    |    16    |    No    |    XML Validation: Declaration not found for element '%s'. Location: %s    |
|    6914    |    16    |    No    |    XML Validation: Type definition for type '%s' was not found, type definition is required before use in a type cast. Location: %s    |
|    6915    |    16    |    No    |    Element or attribute name too long: '%.*ls'.    |
|    6916    |    16    |    No    |    XML Validation: The content model of type or model group '%s' is ambiguous and thus violates the unique particle attribution constraint. Consult SQL Server Books Online for more information.    |
|    6917    |    16    |    No    |    XML Validation: Element '%ls' may not have xsi:nil="true" because it was not defined as nillable or because it has a fixed value constraint. Location: %ls    |
|    6918    |    16    |    No    |    XML Validation: Element '%s' must not have character or element children, because xsi:nil was set to true. Location: %s    |
|    6919    |    16    |    No    |    XML Validation: The type of element '%s' is abstract. Instantiation requires the use of xsi:type to specify a non-abstract type. Location: %s    |
|    6920    |    16    |    No    |    Invalid definition for type '%ls'. Cannot specify use="prohibited" for attribute '%ls' because there is no corresponding attribute in the base type.    |
|    6921    |    16    |    No    |    XML Validation: Element or attribute '%s' was defined as fixed, the element value has to be equal to value of 'fixed' attribute specified in definition. Location: %s    |
|    6922    |    16    |    No    |    XML Validation: Not able to resolve namespace for prefix:'%.*ls'    |
|    6923    |    16    |    No    |    XML Validation: Unexpected element(s): %s. Location: %s    |
|    6924    |    16    |    No    |    XML Validation: Text '%.*ls' found in attribute-only content model. Location: %s    |
|    6925    |    16    |    No    |    Invalid definition for element '%ls'. SQL Server does not currently permit additions to existing substitution groups via ALTER XML SCHEMA COLLECTION.    |
|    6926    |    16    |    No    |    XML Validation: Invalid simple type value: '%s'. Location: %s    |
|    6927    |    16    |    No    |    XML Validation: Invalid simple type value: '%ls'.    |
|    6928    |    16    |    No    |    XML Validation: XML instances of the content model of type or model group '%ls' can be validated in multiple ways and are not supported.    |
|    6929    |    16    |    No    |    XML Validation: Invalid QName for xsi:type attribute '%.*ls'.    |
|    6930    |    16    |    No    |    XML Validation: ID constraint check failed. Found attribute named '%.*ls' with duplicate ID value '%.*ls'. Location: %s    |
|    6931    |    16    |    No    |    XML Validation: IDREF constraint check failed. Found attribute named '%.*ls' with reference to ID value '%.*ls', which does not exist    |
|    6932    |    16    |    No    |    Invalid definition for element or attribute '%s'. Value constraints on components of type ID are not allowed.    |
|    6933    |    16    |    No    |    XML Validation: Invalid simple type operation, inserting into simple type is not permitted. Location: %s    |
|    6934    |    16    |    No    |    XML Validation: Element '%s' requires substitution, because it was defined as abstract. Location: %s    |
|    6935    |    16    |    No    |    XML Validation: ID or IDREF attribute exceeded the allowed maximum length. Location: %s    |
|    6936    |    16    |    No    |    XML Validation: Invalid cast for element '%s' from type '%s' to type '%s'. Location: %s    |
|    6937    |    16    |    No    |    XML Validation: The canonical form of the value '%ls' is not valid according to the specified type. This can result from the use of pattern facets on non-string types or range restrictions or enumerations on floating-point types. Location: %ls    |
|    6938    |    16    |    No    |    XML Validation: The canonical form of the value '%ls' is not valid according to the specified type. This can result from the use of pattern facets on non-string types or range restrictions or enumerations on floating-point types.    |
|    6939    |    16    |    No    |    XML Validation: The element '%ls' is mixed content with a fixed value and therefore not allowed to have element content. Location: %ls    |
|    6940    |    16    |    No    |    Invalid component named '%s' found in global scope. Only elements, attributes, types and groups can be defined in global context    |
|    6941    |    16    |    No    |    Invalid type definition for type '%s', types with complex content can only be derived from base types which have complex content    |
|    6942    |    16    |    No    |    Invalid type definition for type '%s', types with simple content can only be derived from base types which have simple content    |
|    6943    |    16    |    No    |    Invalid type definition for type '%s', the derivation was illegal because 'final' attribute was specified on the base type    |
|    6944    |    16    |    No    |    Invalid type definition for type '%s', '%s' facet is not restricting the value space    |
|    6945    |    16    |    No    |    Invalid facet value for facet '%s' in type definition '%s'    |
|    6946    |    16    |    No    |    Invalid type definition for type '%s', 'minLength' can not be greater than 'maxLength'    |
|    6947    |    16    |    No    |    XML Validation: Multiple ID attributes found on a single element. Location: %s    |
|    6948    |    16    |    No    |    Invalid type definition for type '%s', 'minLength' can not be greater than 'Length'    |
|    6949    |    16    |    No    |    Invalid type definition for type '%s', 'Length' can not be greater than 'maxLength'    |
|    6950    |    16    |    No    |    Invalid type definition for type '%s', 'fractionDigits' can not be greater than 'totalDigits'    |
|    6951    |    16    |    No    |    Invalid type definition for type '%s', 'minInclusive' must be less than or equal to 'maxInclusive' and less than 'maxExclusive'    |
|    6952    |    16    |    No    |    Invalid type definition for type '%s', 'minExclusive' must be less than or equal to 'maxExclusive' and less than 'maxInclusive'    |
|    6953    |    16    |    No    |    Invalid type definition for type '%s', recursive type definitions are not allowed    |
|    6954    |    16    |    No    |    Invalid group definition for group '%s', recursive group definitions are not allowed    |
|    6955    |    16    |    No    |    Invalid attribute definition for attribute '%s', attributes type has to be simple type    |
|    6956    |    16    |    No    |    Invalid type definition for type '%s', fixed facet '%s' can not be redefined to a different value.    |
|    6957    |    16    |    No    |    Invalid element definition, element '%s' is not valid derivation of element '%s'    |
|    6958    |    16    |    No    |    Invalid definition for type '%s'. An 'all' group may not appear as the child or parent of any other model group, it must have minOccurs = maxOccurs = 1, its child elements must have maxOccurs = 1    |
|    6959    |    16    |    No    |    Invalid definition, top level group definitions can not have model groups as siblings    |
|    6960    |    16    |    No    |    Component '%s' is outside of allowed range. Maximum for 'fractionDigits' is 10 and maximum number of digits for non fractional part is 28    |
|    6961    |    16    |    No    |    The system limit on the number of XML types has been reached. Redesign your database to use fewer XML types.    |
|    6962    |    16    |    No    |    'default' and 'fixed' values are not allowed on element of this type: '%s'    |
|    6963    |    16    |    No    |    'Default' or 'Fixed' value is longer than allowed, maximum length allowed is 4000 characters : '%s'    |
|    6964    |    16    |    No    |    Facet value is longer than allowed, maximum length allowed is 4000 characters : '%s'    |
|    6965    |    10    |    No    |    XML Validation: Invalid content. Expected element(s): %s. Found: element '%s' instead. Location: %s.    |
|    6966    |    10    |    No    |    Warning: Type '%s' is restricted by a facet '%s' that may impede full round-tripping of instances of this type    |
|    6967    |    16    |    No    |    Invalid type definition for type '%s'. The base and derived types must have the same value for 'mixed' unless deriving by restriction, in which case 'false' is always permitted for the derived type.    |
|    6968    |    16    |    No    |    Invalid type definition for type '%s'. Complex types cannot restrict simple types    |
|    6969    |    16    |    No    |    ID/IDREF validation consumed too much memory. Try reducing the number of ID and IDREF attributes. Rearranging the file so that elements with IDREF attributes appear after the elements which they reference may also be helpful.    |
|    6970    |    16    |    No    |    Invalid type definition for type '%s'. No type may have more than one attribute of any type derived from ID.    |
|    6971    |    16    |    No    |    Invalid type definition for type '%s'. Type contains attribute '%s' which is not allowed in base type.    |
|    6972    |    16    |    No    |    Invalid redefinition of attribute '%s' in type '%s'. Must be of a type which is a valid restriction of the corresponding attribute in the base type.    |
|    6973    |    16    |    No    |    Invalid redefinition of attribute '%s' in type '%s'. Must be required in the derived type if it is required in the base type.    |
|    6974    |    16    |    No    |    Invalid redefinition of attribute '%s' in type '%s'. Must be prohibited in the derived type if it is prohibited in the base type.    |
|    6975    |    16    |    No    |    Invalid redefinition of attribute '%s' in type '%s'. Must be fixed to the same value as in the derived type.    |
|    6976    |    16    |    No    |    Invalid redefinition of attribute '%s' in type '%s'. Derivation by extension may not redefine attributes.    |
|    6977    |    16    |    No    |    Invalid member type '%s' in union type '%s'. Unions may not have complex member types.    |
|    6978    |    16    |    No    |    Invalid item type for list type '%s'. The item type of a list may not itself be a list, and types derived from ID may not be used as item types in this release.    |
|    6979    |    16    |    No    |    Invalid restriction for type '%s'. The element in the restricted type must have the same name as and a more restrictive type than the corresponding element in the base type.    |
|    6980    |    16    |    No    |    Invalid restriction for type '%s'. The particle in the restricted type may not have an occurrence range more permissive than that of the corresponding particle in the base type.    |
|    6981    |    16    |    No    |    Invalid restriction for type '%s'. The element in the restricted type may not be nillable if the corresponding element in the base type is not.    |
|    6982    |    16    |    No    |    Invalid restriction for type '%s'. The element in the restricted type must be fixed to the same value as the corresponding element in the derived type.    |
|    6983    |    16    |    No    |    Invalid restriction for type '%s'. The element in the restricted type may not have a 'block' value more permissive than the corresponding element in the base type.    |
|    6984    |    16    |    No    |    Invalid restriction for type '%s'. The element in the restricted type must be in one of the namespaces allowed by the base type's wildcard.    |
|    6985    |    16    |    No    |    Invalid restriction for type '%s'. The Wildcard in the restricted type must be a valid subset of the corresponding wildcard in the base type, and the processContents may not be more permissive.    |
|    6986    |    16    |    No    |    Invalid restriction for type '%s'. The effective total range of the model group in the restricted type must be a valid restriction of the occurrence range of the wildcard in the base type.    |
|    6987    |    16    |    No    |    Invalid restriction for type '%s'. An 'all' particle may be restricted only by 'all', 'element', or 'sequence'.    |
|    6988    |    16    |    No    |    Invalid restriction for type '%s'. A 'choice' particle may be restricted only by 'element', 'choice', or 'sequence'.    |
|    6989    |    16    |    No    |    Invalid restriction for type '%s'. A 'sequence' particle may be restricted only by 'element' or 'sequence'.    |
|    6990    |    16    |    No    |    Invalid restriction for type '%s'. Invalid model group restriction.    |
|    6991    |    16    |    No    |    Invalid restriction for type '%s'. If the base type has empty content, then the derived type must as well, and if the derived type has empty content, then the base type must be emptiable.    |
|    6992    |    16    |    No    |    The content model of type '%s' contains two elements with the same name '%s' and different types, nullability, or value constraints.    |
|    6993    |    16    |    No    |    Value constraint on use of attribute '%s' must be consistent with value constraint on its declaration.    |
|    6994    |    16    |    No    |    Invalid restriction for type '%s'. The attribute wildcard in the restricted type must be a valid subset of the corresponding attribute wildcard in the base type, and the processContents may not be more permissive.    |
|    6995    |    16    |    No    |    Invalid definition for type or element '%s'. SQL Server does not permit the built-in XML Schema types 'ID' and 'IDREF' or types derived from them to be used as the type of an element or as the basis for derivation by extension.    |
|    6996    |    16    |    No    |    Invalid type definition for type '%s'. A type may not have both 'minInclusive' and 'minExclusive' or 'maxInclusive' and 'maxExclusive' facets.    |
|    6997    |    16    |    No    |    Invalid definition for element '%s'. An element which has a fixed value may not also be nillable.    |
|    6998    |    16    |    No    |    Invalid type definition: Type or content model '%s' is too complicated. It may be necessary to reduce the number of enumerations or the size of the content model.    |
|    6999    |    16    |    No    |    Invalid definition for element or attribute '%s'. Value constraints on components of type QName are not supported in this release.    |
