---
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/19/2022
ms.topic: include
---
| Error| Severity | Event Logged | Description|
| :------ | :------| :------| :----------------------------- |
|    2002    |    16    |    No    |    Cannot create a procedure for replication with group number greater than one.    |
|    2003    |    16    |    No    |    Procedures with a group number cannot have parameters of XML or CLR types. Parameter '%.*ls' of procedure '%.*ls' has type '%ls'.    |
|    2004    |    16    |    No    |    Procedure '%.*ls' has already been created with group number %d. Create procedure with an unused group number.    |
|    2007    |    10    |    No    |    The module '%.*ls' depends on the missing object '%.*ls'. The module will still be created; however, it cannot run successfully until the object exists.    |
|    2008    |    16    |    No    |    The object '%.*ls' is not a procedure so you cannot create another procedure under that group name.    |
|    2010    |    16    |    No    |    Cannot perform alter on '%.*ls' because it is an incompatible object type.    |
|    2011    |    16    |    No    |    Index hints cannot be specified within a schema-bound object.    |
|    2013    |    10    |    No    |    Warning: 'is_ms_shipped' property is turned off for %S_MSG '%.*ls' because you do not have permission to create or alter an object with this property.    |
|    2014    |    16    |    No    |    Remote access is not allowed from within a schema-bound object.    |
|    [2020](../../relational-databases/errors-events/mssqlserver-2020-database-engine-error.md)    |    16    |    No    |    The dependencies reported for entity "%.*ls" do not include references to columns. This is either because the entity references an object that does not exist or because of an error in one or more statements in the entity. Before rerunning the query, ensure that there are no errors in the entity and that all objects referenced by the entity exist.    |
|    2101    |    14    |    No    |    Cannot %S_MSG a server level %S_MSG for user '%.*ls' since there is no login corresponding to the user.    |
|    2102    |    16    |    No    |    Cannot %S_MSG %S_MSG '%.*ls' since there is no user for login '%.*ls' in database '%.*ls'.    |
|    2103    |    15    |    No    |    Cannot %S_MSG trigger '%.*s' because its schema is different from the schema of the target table or view.    |
|    2104    |    14    |    No    |    Cannot %S_MSG the %S_MSG '%.*ls', because you do not have permission.    |
|    2108    |    15    |    No    |    Cannot %S_MSG %S_MSG on '%.*ls' as the target is not in the current database.    |
|    2110    |    15    |    No    |    Cannot alter trigger '%.*ls' on '%.*ls' because this trigger does not belong to this object. Specify the correct trigger name or the correct target object name.    |
|    2111    |    16    |    No    |    Cannot %S_MSG trigger '%.*ls' on %S_MSG '%.*ls' because an INSTEAD OF %s trigger already exists on this object.    |
|    2112    |    16    |    No    |    Cannot create trigger '%.*ls' on view '%.*ls' because the view is defined with CHECK OPTION.    |
|    2113    |    16    |    No    |    Cannot %S_MSG INSTEAD OF DELETE or INSTEAD OF UPDATE TRIGGER '%.*ls' on table '%.*ls'. This is because the table has a FOREIGN KEY with cascading DELETE or UPDATE.    |
|    2114    |    16    |    No    |    Column '%.*ls' cannot be used in an IF UPDATE clause because it is a computed column.    |
|    2115    |    16    |    No    |    Server level event notifications are disabled as the database msdb does not exist.    |
|    2116    |    16    |    No    |    Cannot CREATE EVENT NOTIFICATION to database '%.*ls' because it is not a valid broker database.    |
|    2117    |    16    |    No    |    Cannot %S_MSG INSTEAD OF trigger '%.*ls' on %S_MSG '%.*ls' because the %S_MSG has a FILESTREAM column.    |
|    2201    |    16    |    No    |    %sDerivation from "anySimpleType" by restriction is not permitted, and derivation by restriction from a type derived from "anySimpleType" by extension is allowed only if no constraining facets are specified.    |
|    2202    |    16    |    No    |    %sAn error has occurred while compiling the query. To obtain more detailed information about the error, the query must be run by a user with EXECUTE permissions on the xml schema collection used in the query.    |
|    2203    |    16    |    No    |    %sOnly 'http://www.w3.org/2001/XMLSchema#decimal?', 'http://www.w3.org/2001/XMLSchema#boolean?' or 'node()*' expressions allowed as predicates, found '%ls'    |
|    2204    |    16    |    No    |    %sOnly 'http://www.w3.org/2001/XMLSchema#boolean?' or 'node()*' expressions allowed in conditions and with logical operators, found '%ls'    |
|    2205    |    16    |    No    |    %s"%ls" was expected.    |
|    2206    |    16    |    No    |    %sNamespace prefix 'xml' can only be associated with the URI 'http://www.w3.org/XML/1998/namespace' and this URI cannot be used with other prefixes.    |
|    2207    |    16    |    No    |    %sOnly non-document nodes can be inserted. Found "%ls".    |
|    2208    |    16    |    No    |    %sThe URI that starts with '%ls' is too long. Maximum allowed length is %d characters.    |
|    2209    |    16    |    No    |    %sSyntax error near '%ls'    |
|    2210    |    16    |    No    |    %sHeterogeneous sequences are not allowed: found '%ls' and '%ls'    |
|    2211    |    16    |    No    |    %sSingleton (or empty sequence) required, found operand of type '%ls'    |
|    2212    |    16    |    No    |    %sInvalid source character '%c' (0x%02x) found in an identifier near '%ls'.    |
|    2213    |    16    |    No    |    %sCannot atomize/apply data() on expression that contains type '%ls' within inferred type '%ls'    |
|    2214    |    16    |    No    |    %sThe type '%ls' is not an atomic type    |
|    2215    |    16    |    No    |    %sThe value of attribute '%ls' exceeds 4000 characters, the maximum permitted in XML schema documents    |
|    2216    |    16    |    No    |    %sInvalid XPath value in "%ls".    |
|    2217    |    16    |    No    |    %s'%ls' or '%ls' expected    |
|    2218    |    16    |    No    |    %sThere is no attribute named '\@%ls'    |
|    2219    |    16    |    No    |    %sThere is no attribute named '\@%ls' in the type '%ls'.    |
|    2220    |    16    |    No    |    %sThere is no attribute named '\@%ls:%ls'    |
|    2221    |    16    |    No    |    %sThere is no attribute named '\@%ls:%ls' in the type '%ls'.    |
|    2222    |    16    |    No    |    %sInvalid source character 0x%02x found in an identifier near '%ls'.    |
|    2223    |    16    |    No    |    %sSyntax error near '%ls', expected an identifier.    |
|    2225    |    16    |    No    |    %sA string literal was expected    |
|    2226    |    16    |    No    |    %sThe target of 'insert' must be a single node, found '%ls'    |
|    2227    |    16    |    No    |    %sThe variable '%ls' was not found in the scope in which it was referenced.    |
|    2228    |    16    |    No    |    %sThe variable '%ls:%ls' was not found in the scope in which it was referenced.    |
|    2229    |    16    |    No    |    %sThe name "%ls" does not denote a namespace.    |
|    2230    |    16    |    No    |    %sThe name "%ls" has already been defined.    |
|    2231    |    16    |    No    |    %sThe name "%ls" does not denote a defined type.    |
|    2232    |    16    |    No    |    %sThe name "%ls:%ls" does not denote a defined type.    |
|    2233    |    16    |    No    |    %sThe operand of "%ls" has an invalid type.    |
|    2234    |    16    |    No    |    %sThe operator "%ls" cannot be applied to "%ls" and "%ls" operands.    |
|    2235    |    16    |    No    |    %sAn argument list was applied to a non-function term.    |
|    2236    |    16    |    No    |    %sThere are not enough actual arguments in the call to function "%ls".    |
|    2237    |    16    |    No    |    %sDerivation from anyType by extension is not supported in this release.    |
|    2238    |    16    |    No    |    %sToo many arguments in call to function '%ls'    |
|    2240    |    16    |    No    |    %sThe target of 'insert into' must be an element/document node, found '%ls'    |
|    2241    |    16    |    No    |    %sVariable expected: '$name'    |
|    2242    |    16    |    No    |    %sType specification expected.    |
|    2243    |    16    |    No    |    %sRelative path expression used without any context    |
|    2247    |    16    |    No    |    %sThe value is of type "%ls", which is not a subtype of the expected type "%ls".    |
|    2248    |    16    |    No    |    %sSyntax error near '%ls', expected 'as', 'into', 'before' or 'after'.    |
|    2249    |    16    |    No    |    %sThe target of 'insert before/after' must be an element/PI/comment/text node, found '%ls'    |
|    2256    |    16    |    No    |    %sSyntax error near '%ls', expected a "node test".    |
|    2258    |    16    |    No    |    %sThe position may not be specified when inserting an attribute node, found '%ls'    |
|    2260    |    16    |    No    |    %sThere is no element named '%ls'    |
|    2261    |    16    |    No    |    %sThere is no element named '%ls' in the type '%ls'.    |
|    2262    |    16    |    No    |    %sThere is no element named '%ls:%ls'    |
|    2263    |    16    |    No    |    %sThere is no element named "%ls:%ls" in the type "%ls".    |
|    2264    |    16    |    No    |    %sOnly non-document nodes may be deleted, found '%ls'    |
|    2266    |    16    |    No    |    %sExpected end tag '%ls:%ls'    |
|    2267    |    16    |    No    |    %sExpected end tag '%ls'    |
|    2268    |    16    |    No    |    %sEnd tag '/%ls:%ls' has no matching begin tag    |
|    2269    |    16    |    No    |    %sEnd tag '/%ls' has no matching begin tag    |
|    2270    |    16    |    No    |    %sDuplicate attribute '%ls:%ls'    |
|    2271    |    16    |    No    |    %sDuplicate attribute '%ls'    |
|    2272    |    16    |    No    |    %s'?>' expected    |
|    2273    |    16    |    No    |    %sUnterminated CDATA section    |
|    2274    |    16    |    No    |    %sUnterminated string constant (started on line %u)    |
|    2275    |    16    |    No    |    %sUnterminated XML declaration    |
|    2276    |    16    |    No    |    %sDerivation from 'QName' by restriction is not supported in this release    |
|    2277    |    16    |    No    |    %sA tag name may not contain the character '%c'    |
|    2278    |    16    |    No    |    %sA tag name may not start with the character '%c'    |
|    2279    |    16    |    No    |    %sA name/token may not start with the character '%c'    |
|    2280    |    16    |    No    |    %s<! was not followed by a valid construct    |
|    2281    |    16    |    No    |    %sCannot construct DTDs in XQuery    |
|    2282    |    16    |    No    |    %sInvalid entity reference    |
|    2283    |    16    |    No    |    %sThe character '%c' may not be part of an entity reference    |
|    2284    |    16    |    No    |    %sThe namespace prefix '%ls' has not been defined    |
|    2285    |    16    |    No    |    %sInvalid numeric entity reference    |
|    2291    |    16    |    No    |    %sNo root element was found.    |
|    2292    |    16    |    No    |    %sWhen a type with simple content restricts a type with mixed content, it must have an embedded simple type definition. Location: '%ls'.    |
|    2293    |    16    |    No    |    %sChoice cannot be empty unless minOccurs is 0. Location: '%ls'.    |
|    2294    |    16    |    No    |    %s'xml' is not allowed as a processing instruction target.    |
|    2297    |    16    |    No    |    %sElement <%ls> is not valid at location '%ls'.    |
|    2298    |    16    |    No    |    %sAttribute '%ls' is not valid at location '%ls'.    |
|    2299    |    16    |    No    |    %sRequired attribute "%ls" of XSD element "%ls" is missing.    |
|    2300    |    16    |    No    |    %sRequired sub-element "%ls" of XSD element "%ls" is missing.    |
|    2301    |    16    |    No    |    %sThe element "%ls" has already been defined.    |
|    2302    |    16    |    No    |    %sThe name "%ls" has already been defined in this scope.    |
|    2305    |    16    |    No    |    %sElement or attribute type specified more than once. Location: '%ls'.    |
|    2306    |    16    |    No    |    %sThe qualified name "%ls" was found in a context where only NCName is allowed.    |
|    2307    |    16    |    No    |    %sReference to an undefined name '%ls'    |
|    2308    |    16    |    No    |    %sReference to an undefined name '%ls' within namespace '%ls'    |
|    2309    |    16    |    No    |    %sThe value of "%ls" is not a valid number.    |
|    2310    |    16    |    No    |    %sThe attribute "%ls" is declared more than once.    |
|    2311    |    16    |    No    |    %sThe attribute "%ls" is declared more than once within "%ls".    |
|    2312    |    16    |    No    |    %sThe value of attribute '%ls' does not conform to the type definition 'http://www.w3.org/2001/XMLSchema#%ls': '%ls'.    |
|    2313    |    16    |    No    |    %sThe attribute "%ls" cannot have a value of "%ls".    |
|    2314    |    16    |    No    |    %sThe attribute "%ls" cannot have a negative value.    |
|    2315    |    16    |    No    |    %sThe attribute "%ls" should have a string value.    |
|    2316    |    16    |    No    |    %sThe required 'base' attribute is missing. Location: '%ls'.    |
|    2317    |    16    |    No    |    %sThe base type "%ls" defined on XSD element "%ls" is not a simple type.    |
|    2319    |    16    |    No    |    %sThis type may not have a '%ls' facet. Location: '%ls'.    |
|    2320    |    16    |    No    |    %sDuplicate facet '%ls' found at location '%ls'.    |
|    2321    |    16    |    No    |    %sFacets cannot follow attribute declarations. Found facet '%ls' at location '%ls'.    |
|    2322    |    16    |    No    |    %sThe element type is not a subclass of the substitution group's head    |
|    2323    |    16    |    No    |    %sThe end tag '%ls' doesn't match opening tag '%ls' from line %u    |
|    2324    |    16    |    No    |    %sThe end tag '%ls:%ls' doesn't match opening tag '%ls' from line %u    |
|    2325    |    16    |    No    |    %sThe end tag '%ls' doesn't match opening tag '%ls:%ls' from line %u    |
|    2326    |    16    |    No    |    %sThe end tag '%ls:%ls' doesn't match opening tag '%ls:%ls' from line %u    |
|    2327    |    16    |    No    |    %sThe content or definition of <%ls> is missing.    |
|    2328    |    16    |    No    |    %sSchema namespace '%ls' doesn't match \<include\> directive's '%ls'    |
|    2329    |    16    |    No    |    %sThe string "%ls" is not a valid time duration value.    |
|    2331    |    16    |    No    |    %sRedefinition has to have itself as base type. Location: '%ls'.    |
|    2332    |    16    |    No    |    %s'%ls' may not be used with an 'empty' operand    |
|    2333    |    16    |    No    |    %sInvalid source character 0x%02x    |
|    2334    |    16    |    No    |    %sInvalid source character '%c' (0x%02x)    |
|    2335    |    16    |    No    |    %sNewline in character/string constant    |
|    2336    |    16    |    No    |    %s'%c' is not a valid octal digit (numbers starting with '0' are implicitly octal)    |
|    2337    |    16    |    No    |    %sThe target of 'replace' must be at most one node, found '%ls'    |
|    2338    |    16    |    No    |    %sThe second 'replace' operand must contain only nodes, found '%ls'    |
|    2339    |    16    |    No    |    %sEither a memberType attribute or a simpleType child must be present. Location: '%ls'.    |
|    2340    |    16    |    No    |    %sComment started on line %u has no end    |
|    2341    |    16    |    No    |    %sExpected hex character code following '\\x'    |
|    2342    |    16    |    No    |    %sInvalid numeric constant.    |
|    2343    |    16    |    No    |    %sUnterminated text section - missing `    |
|    2348    |    16    |    No    |    %sA namespace URI should contain at least one non-whitespace character.    |
|    2349    |    16    |    No    |    %sAttempt to redefine namespace prefix '%ls'    |
|    2350    |    16    |    No    |    %sInvalid XML element content    |
|    2351    |    16    |    No    |    %sExpected 'first' or 'last'    |
|    2353    |    16    |    No    |    %s'to' or 'insert' or 'delete' expected    |
|    2354    |    16    |    No    |    %sInvalid source character encoding    |
|    2355    |    16    |    No    |    %s'else' expected    |
|    2356    |    16    |    No    |    %sThe target of 'replace value of' must be a non-metadata attribute or an element with simple typed content, found '%ls'    |
|    2357    |    16    |    No    |    %sA document node may only be replaced with another document node, found '%ls'    |
|    2358    |    16    |    No    |    %sDerivation with both a 'base' attribute and an embedded type definition is not supported in this release. Location: '%ls'.    |
|    2359    |    16    |    No    |    %sThe target of '%ls' may not be a constructed node    |
|    2360    |    16    |    No    |    %sCannot have both a 'name' and 'ref' attribute. Location: '%ls'.    |
|    2361    |    16    |    No    |    %sThe base type of an XSD extension or restriction type must be a simple type.    |
|    2362    |    16    |    No    |    %sXSD schema too complex    |
|    2363    |    16    |    No    |    %sXQuery too complex    |
|    2364    |    16    |    No    |    %sCannot implicitly convert from '%ls' to '%ls'    |
|    2365    |    16    |    No    |    %sCannot explicitly convert from '%ls' to '%ls'    |
|    2366    |    16    |    No    |    %s"%ls" has a circular definition.    |
|    2367    |    16    |    No    |    %sThe item type of an XSD list type must be a simple type. Location: '%ls'.    |
|    2368    |    16    |    No    |    %sCannot have element content in a complex type with simple content. Location: '%ls'.    |
|    2369    |    16    |    No    |    %sCannot have more than one group/sequence/choice/all within a restriction or extension. Location: '%ls'.    |
|    2370    |    16    |    No    |    %sNo more tokens expected at the end of the XQuery expression. Found '%ls'.    |
|    2371    |    16    |    No    |    %s'%ls' can only be used within a predicate or XPath selector    |
|    2372    |    16    |    No    |    %sMetadata attribute '\@%ls:%ls' may not be used with '%ls'    |
|    2373    |    16    |    No    |    %s%ls is not supported with constructed XML    |
|    2374    |    16    |    No    |    %sA node or set of nodes is required for %ls    |
|    2375    |    16    |    No    |    %sAggregate function '%ls' expects a sequence argument    |
|    2376    |    16    |    No    |    %sOperand of a single numeric type expected    |
|    2377    |    16    |    No    |    %sResult of '%ls' expression is statically 'empty'    |
|    2378    |    16    |    No    |    %sExpected XML schema document    |
|    2379    |    16    |    No    |    %sThe name specified is not a valid XML name :'%ls'    |
|    2380    |    16    |    No    |    %sMixed content is not allowed at location '%ls'.    |
|    2382    |    16    |    No    |    %sInvalid combination of minOccurs and maxOccurs values, minOccurs has to be less than or equal to maxOccurs. Location: '%ls'.    |
|    2383    |    16    |    No    |    %sInvalid value '%ls' for the %ls attribute. The value has to be between 0 and %ld.    |
|    2384    |    16    |    No    |    %sInvalid element occurrence, element '%ls' was found multiple times in the context of element '%ls'    |
|    2385    |    16    |    No    |    Invalid target namespace specified    |
|    2386    |    16    |    No    |    %sThe value of '%ls' facet is outside of the allowed range    |
|    2387    |    16    |    No    |    %sCannot have both 'type' and 'ref' attributes. Location: '%ls'.    |
|    2388    |    16    |    No    |    %sInvalid element occurrence, element '%ls' has to appear first in the context of '%ls'    |
|    2389    |    16    |    No    |    %s'%ls' requires a singleton (or empty sequence), found operand of type '%ls'    |
|    2390    |    16    |    No    |    %sTop-level %s nodes are not supported    |
|    2391    |    16    |    No    |    %sRedefining XSD schemas is not supported    |
|    2392    |    16    |    No    |    %s'%ls::' is not a valid axis    |
|    2393    |    16    |    No    |    %sEither an itemType attribute or a simpleType child must be present. Location: '%ls'.    |
|    2394    |    16    |    No    |    %sThere is no function '%ls()'    |
|    2395    |    16    |    No    |    %sThere is no function '%ls:%ls()'    |
|    2396    |    16    |    No    |    %sAttribute may not appear outside of an element    |
|    2397    |    16    |    No    |    %sIdentifiers may not contain more than %u characters    |
|    2398    |    16    |    No    |    %sDuplicate id value found: '%ls'    |
|    2399    |    16    |    No    |    %sAn attribute cannot have a value of type '%ls', a simple type was expected    |
|    [2501](../../relational-databases/errors-events/mssqlserver-2501-database-engine-error.md)    |    16    |    No    |    Cannot find a table or object with the name "%.*ls". Check the system catalog.    |
|    2502    |    16    |    No    |    Memory object list dump failed due to temporary inconsistency in the memory object structure. Please try again.    |
|    2503    |    10    |    No    |    Successfully deleted the physical file '%ls'.    |
|    2504    |    16    |    No    |    Could not delete the physical file '%ls'. The DeleteFile system function returned error %ls.    |
|    2505    |    16    |    No    |    The device '%.*ls' does not exist. Use sys.backup_devices to show available devices.    |
|    2506    |    16    |    No    |    Could not find a table or object name '%.*ls' in database '%.*ls'.    |
|    2507    |    16    |    No    |    The CONCAT_NULL_YIELDS_NULL option must be set to ON to run DBCC CHECKCONSTRAINTS.    |
|    [2508](../../relational-databases/errors-events/mssqlserver-2508-database-engine-error.md)    |    16    |    No    |    The %.*ls count for object "%.*ls", index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls) is incorrect. Run DBCC UPDATEUSAGE.    |
|    2509    |    16    |    No    |    DBCC CHECKCONSTRAINTS failed due to an internal query error.    |
|    2510    |    16    |    No    |    DBCC %ls error: %ls.    |
|    [2511](../../relational-databases/errors-events/mssqlserver-2511-database-engine-error.md)    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). Keys out of order on page %S_PGID, slots %d and %d.    |
|    [2512](../../relational-databases/errors-events/mssqlserver-2512-database-engine-error.md)    |    16    |    No    |    Table error: Object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). Duplicate keys on page %S_PGID slot %d and page %S_PGID slot %d.    |
|    2514    |    16    |    No    |    A DBCC PAGE error has occurred: %ls.    |
|    [2515](../../relational-databases/errors-events/mssqlserver-2515-database-engine-error.md)    |    16    |    No    |    The page %S_PGID, object ID %d, index ID %d, partition ID %I64d, allocation unit ID %I64d (type %.*ls) has been modified, but is not marked as modified in the differential backup bitmap.    |
|    [2516](../../relational-databases/errors-events/mssqlserver-2516-database-engine-error.md)    |    16    |    Yes    |    Repair has invalidated the differential bitmap for database %.*ls. The differential backup chain is broken. You must perform a full database backup before you can perform a differential backup.    |
|    2517    |    16    |    Yes    |    Bulk-logging has been turned on for database %.*ls. To ensure that all data has been secured, run backup log operations again.    |
|    [2518](../../relational-databases/errors-events/mssqlserver-2518-database-engine-error.md)    |    10    |    No    |    Object ID %ld (object "%.*ls"): Computed columns and CLR types cannot be checked for this object because the common language runtime (CLR) is disabled.    |
|    [2519](../../relational-databases/errors-events/mssqlserver-2519-database-engine-error.md)    |    10    |    No    |    Computed columns and CLR types cannot be checked for object ID %ld (object "%.*ls") because the internal expression evaluator could not be initialized.    |
|    2520    |    16    |    No    |    Could not find database '%.*ls'. The database either does not exist, or was dropped before a statement tried to use it. Verify if the database exists by querying the sys.databases catalog view.    |
|    2521    |    16    |    No    |    Could not find database ID %d. The database ID either does not exist, or the database was dropped before a statement tried to use it. Verify if the database ID exists by querying the sys.databases catalog view.    |
|    [2522](../../relational-databases/errors-events/mssqlserver-2522-database-engine-error.md)    |    16    |    No    |    Unable to process index %.*ls of table %.*ls because filegroup %.*ls is invalid.    |
|    2523    |    16    |    No    |    Filegroup %.*ls is invalid.    |
|    2524    |    16    |    No    |    Cannot process object ID %ld (object "%.*ls") because it is a Service Broker queue. Try the operation again with the object ID of the corresponding internal table for the queue, found in sys.internal_tables.    |
|    2525    |    16    |    No    |    Database file %.*ls is offline.    |
|    2526    |    16    |    No    |    Incorrect DBCC statement. Check the documentation for the correct DBCC syntax and options.    |
|    [2527](../../relational-databases/errors-events/mssqlserver-2527-database-engine-error.md)    |    16    |    No    |    Unable to process index %.*ls of table %.*ls because filegroup %.*ls is offline.    |
|    2528    |    10    |    No    |    DBCC execution completed. If DBCC printed error messages, contact your system administrator.    |
|    2529    |    16    |    No    |    Filegroup %.*ls is offline.    |
|    [2530](../../relational-databases/errors-events/mssqlserver-2530-database-engine-error.md)    |    16    |    No    |    The index "%.*ls" on table "%.*ls" is disabled.    |
|    2531    |    16    |    No    |    Table error: object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls) B-tree level mismatch, page %S_PGID. Level %d does not match level %d from the previous %S_PGID.    |
|    2532    |    16    |    No    |    One or more WITH options specified are not valid for this command.    |
|    [2533](../../relational-databases/errors-events/mssqlserver-2533-database-engine-error.md)    |    16    |    No    |    Table error: page %S_PGID allocated to object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls) was not seen. The page may be invalid or may have an incorrect alloc unit ID in its header.    |
|    [2534](../../relational-databases/errors-events/mssqlserver-2534-database-engine-error.md)    |    16    |    No    |    Table error: page %S_PGID, whose header indicates that it is allocated to object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls), is allocated by another object.    |
|    [2536](../../relational-databases/errors-events/mssqlserver-2536-database-engine-error.md)    |    10    |    No    |    DBCC results for '%.*ls'.    |
|    [2537](../../relational-databases/errors-events/mssqlserver-2537-database-engine-error.md)    |    16    |    No    |    Table error: object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls), page %S_PGID, row %d. The record check (%hs) failed. The values are %I64d and %I64d.    |
|    [2538](../../relational-databases/errors-events/mssqlserver-2538-database-engine-error.md)    |    10    |    No    |    File %d. The number of extents = %I64d, used pages = %I64d, and reserved pages = %I64d.    |
|    [2539](../../relational-databases/errors-events/mssqlserver-2539-database-engine-error.md)    |    10    |    No    |    The total number of extents = %I64d, used pages = %I64d, and reserved pages = %I64d in this database.    |
|    [2540](../../relational-databases/errors-events/mssqlserver-2540-database-engine-error.md)    |    10    |    No    |    The system cannot self repair this error.    |
|    2541    |    10    |    No    |    DBCC UPDATEUSAGE: Usage counts updated for table '%.*ls' (index '%.*ls', partition %ld):    |
|    2542    |    10    |    No    |    DATA pages %.*ls: changed from (%I64d) to (%I64d) pages.    |
|    2543    |    10    |    No    |    USED pages %.*ls: changed from (%I64d) to (%I64d) pages.    |
|    2544    |    10    |    No    |    RSVD pages %.*ls: changed from (%I64d) to (%I64d) pages.    |
|    2545    |    10    |    No    |    ROWS count: changed from (%I64d) to (%I64d) rows.    |
|    [2546](../../relational-databases/errors-events/mssqlserver-2546-database-engine-error.md)    |    10    |    No    |    Index '%.*ls' on table '%.*ls' is marked as disabled. Rebuild the index to bring it online.    |
|    2547    |    16    |    No    |    Unable to process object ID %ld (object "%.*ls") because it is a synonym. If the object referenced by the synonym is a table or view, retry the operation using the base object that the synonym references.    |
|    2548    |    10    |    No    |    DBCC: Compaction phase of index '%.*ls' is %d%% complete.    |
|    2549    |    10    |    No    |    DBCC: Defrag phase of index '%.*ls' is %d%% complete.    |
|    2550    |    16    |    No    |    The index "%.*ls" (partition %ld) on table "%.*ls" cannot be reorganized because it is being reorganized by another process.    |
|    2551    |    16    |    No    |    The indexes on table "%.*ls" cannot be reorganized because there is already an online index build or rebuild in progress on the table.    |
|    2552    |    16    |    No    |    The index "%.*ls" (partition %ld) on table "%.*ls" cannot be reorganized because page level locking is disabled.    |
|    2553    |    10    |    Yes    |    Table '%.*ls' will not be available during reorganizing index '%.*ls'. This is because the index reorganization operation performs inside a user transaction and the entire table is exclusively locked.    |
|    2554    |    16    |    No    |    The index "%.*ls" (partition %ld) on table "%.*ls" cannot be reorganized because the filegroup is read-only.    |
|    2555    |    16    |    No    |    Cannot move all contents of file "%.*ls" to other places to complete the emptyfile operation.    |
|    2556    |    16    |    No    |    There is insufficient space in the filegroup to complete the emptyfile operation.    |
|    2557    |    14    |    No    |    User '%.*ls' does not have permission to run DBCC %ls for object '%.*ls'.    |
|    2558    |    16    |    No    |    %I64d incorrect counts were detected in database '%.*ls'.    |
|    2559    |    16    |    No    |    The '%ls' and '%ls' options are not allowed on the same statement.    |
|    2560    |    16    |    No    |    Parameter %d is incorrect for this DBCC statement.    |
|    2561    |    16    |    No    |    Parameter %d is incorrect for this statement.    |
|    2562    |    16    |    No    |    Checking FILESTREAM filegroup "%.*ls" (ID %d) is not supported in DBCC CHECKFILEGROUP. Specify a filegroup containing user objects with FILESTREAM data instead.    |
|    2566    |    14    |    No    |    DBCC DBREINDEX cannot be used on system tables.    |
|    2567    |    14    |    No    |    DBCC INDEXDEFRAG cannot be used on system table indexes    |
|    2568    |    16    |    No    |    Page %S_PGID is out of range for this database or is in a log file.    |
|    [2570](../../relational-databases/errors-events/mssqlserver-2570-database-engine-error.md)    |    16    |    No    |    Page %S_PGID, slot %d in object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type "%.*ls"). Column "%.*ls" value is out of range for data type "%.*ls". Update column to a legal value.    |
|    2571    |    14    |    No    |    User '%.*ls' does not have permission to run DBCC %.*ls.    |
|    2572    |    16    |    No    |    DBCC cannot free DLL '%.*ls'. The DLL is in use.    |
|    2573    |    16    |    No    |    Could not find table or object ID %.*ls. Check system catalog.    |
|    [2574](../../relational-databases/errors-events/mssqlserver-2574-database-engine-error.md)    |    16    |    No    |    Table error: Page %S_PGID is empty in object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). This is not permitted at level %d of the B-tree.    |
|    [2575](../../relational-databases/errors-events/mssqlserver-2575-database-engine-error.md)    |    16    |    No    |    The Index Allocation Map (IAM) page %S_PGID is pointed to by the next pointer of IAM page %S_PGID in object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls), but it was not detected in the scan.    |
|    [2576](../../relational-databases/errors-events/mssqlserver-2576-database-engine-error.md)    |    16    |    No    |    The Index Allocation Map (IAM) page %S_PGID is pointed to by the previous pointer of IAM page %S_PGID in object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls), but it was not detected in the scan.    |
|    [2577](../../relational-databases/errors-events/mssqlserver-2577-database-engine-error.md)    |    16    |    No    |    Chain sequence numbers are out of order in the Index Allocation Map (IAM) chain for object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls). Page %S_PGID with sequence number %d points to page %S_PGID with sequence number %d.    |
|    [2579](../../relational-databases/errors-events/mssqlserver-2579-database-engine-error.md)    |    16    |    No    |    Table error: Extent %S_PGID in object ID %d, index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.*ls) is beyond the range of this database.    |
|    2580    |    16    |    No    |    Table '%.*ls' is either a system or temporary table. DBCC CLEANTABLE cannot be applied to a system or temporary table.    |
|    2581    |    10    |    No    |    DBCC cannot free the DLL "%.*ls". The DLL is not loaded.    |
|    2583    |    16    |    No    |    An incorrect number of parameters was given to the DBCC statement.    |
|    2585    |    16    |    No    |    Cannot find partition number %ld for table "%.*ls".    |
|    2586    |    16    |    No    |    Cannot find partition number %ld for index "%.*ls", table "%.*ls".    |
|    2587    |    16    |    No    |    The invalid partition number %ld was specified.    |
|    2588    |    16    |    No    |    Cannot find partition number %ld for index ID %d, object ID %d.    |
|    2589    |    16    |    No    |    Repair could not fix all errors on the first attempt.    |
|    2590    |    10    |    Yes    |    User "%.*ls" is modifying bytes %d to %d of page %S_PGID in database "%.*ls".    |
|    2591    |    16    |    No    |    Cannot find a row in the system catalog with the index ID %d for table "%.*ls".    |
|    [2592](../../relational-databases/errors-events/mssqlserver-2592-database-engine-error.md)    |    10    |    No    |    Repair: The %ls index successfully rebuilt for the object "%.*ls" in database "%.*ls".    |
|    [2593](../../relational-databases/errors-events/mssqlserver-2593-database-engine-error.md)    |    10    |    No    |    There are %I64d rows in %I64d pages for object "%.*ls".    |
|    2594    |    10    |    No    |    Cannot process rowset ID %I64d of object "%.*ls" (ID %d), index "%.*ls" (ID %d), because it resides on filegroup "%.*ls" (ID %d), which was not checked.    |
|    [2596](../../relational-databases/errors-events/mssqlserver-2596-database-engine-error.md)    |    16    |    No    |    The repair statement was not processed. The database cannot be in read-only mode.    |
|    2597    |    10    |    No    |    Ignoring trace flag %d. It is either an invalid trace flag or a trace flag that can only be specified during server startup.    |
|    2599    |    16    |    No    |    Cannot switch to in row text in table "%.*ls".    |
|    2601    |    14    |    No    |    Cannot insert duplicate key row in object '%.*ls' with unique index '%.*ls'.    |
|    2628    |    16    |    No    |    String or binary data would be truncated in table '%.*ls', column '%.*ls'. Truncated value: '%.*ls'.|
|    2627    |    14    |    No    |    Violation of %ls constraint '%.*ls'. Cannot insert duplicate key in object '%.*ls'.    |
|    2701    |    10    |    No    |    Database name '%.*ls' ignored, referencing object in tempdb.    |
|    2702    |    16    |    No    |    Database '%.*ls' does not exist.    |
|    2703    |    16    |    No    |    Cannot use duplicate column names in the partition columns list. Column name '%.*ls' appears more than once.    |
|    2704    |    16    |    No    |    Invalid partition scheme '%.*ls' specified.    |
|    2705    |    16    |    No    |    Column names in each table must be unique. Column name '%.*ls' in table '%.*ls' is specified more than once.    |
|    2706    |    11    |    No    |    Table '%.*ls' does not exist.    |
|    2707    |    16    |    No    |    Column '%.*ls' in %S_MSG '%.*ls' cannot be used in an index or statistics or as a partition key because it depends on a non-schemabound object.    |
|    2709    |    16    |    No    |    Column '%.*ls' in %S_MSG '%.*ls' cannot be used in an index or statistics or as a partition key because it does user or system data access.    |
|    2710    |    16    |    No    |    You are not the owner specified for the object '%.*ls' in this statement (CREATE, ALTER, TRUNCATE, UPDATE STATISTICS or BULK INSERT).    |
|    2711    |    16    |    No    |    The definition of object "%.*ls" in the resource database contains the non-ASCII character "%.*ls".    |
|    2712    |    16    |    No    |    Database '%.*ls' can not be configured as a distribution database because it has change tracking enabled.    |
|    2714    |    16    |    No    |    There is already an object named '%.*ls' in the database.    |
|    2715    |    16    |    No    |    Column, parameter, or variable #%d: Cannot find data type %.*ls.    |
|    2716    |    16    |    No    |    Column, parameter, or variable #%d: Cannot specify a column width on data type %.*ls.    |
|    2717    |    15    |    No    |    The size (%d) given to the %S_MSG '%.*ls' exceeds the maximum allowed (%d).    |
|    2719    |    16    |    No    |    Upgrade of database "%.*ls" failed because it contains a user named "sys" which is a reserved user or schema name in this version of SQL Server.    |
|    2720    |    16    |    No    |    Cannot schema bind %S_MSG '%.*ls' because it references system object '%.*ls'.    |
|    2722    |    16    |    No    |    Xml data type methods are not allowed in expressions in this context.    |
|    2724    |    10    |    No    |    Parameter or variable '%.*ls' has an invalid data type.    |
|    2725    |    16    |    No    |    An online operation cannot be performed for %S_MSG '%.*ls' because the index contains column '%.*ls' of data type text, ntext, image, varchar(max), nvarchar(max), varbinary(max), xml, or large CLR type. For a nonclustered index, the column could be an include column of the index. For a clustered index, the column could be any column of the table. If DROP_EXISTING is used, the column could be part of a new or old index. The operation must be performed offline.    |
|    2726    |    16    |    No    |    Partition function '%.*ls' uses %d columns which does not match with the number of partition columns used to partition the table or index.    |
|    2727    |    11    |    No    |    Cannot find index '%.*ls'.    |
|    2728    |    16    |    No    |    Cannot partition on more than %d columns.    |
|    2729    |    16    |    No    |    Column '%.*ls' in %S_MSG '%.*ls' cannot be used in an index or statistics or as a partition key because it is non-deterministic.    |
|    2730    |    11    |    No    |    Cannot create procedure '%.*ls' with a group number of %d because a procedure with the same name and a group number of 1 does not currently exist in the database. Must execute CREATE PROCEDURE '%.*ls';1 first.    |
|    2731    |    16    |    No    |    Column '%.*ls' has invalid width: %d.    |
|    2732    |    16    |    No    |    Error number %ld is invalid. The number must be from %ld through %ld and it cannot be 50000.    |
|    2733    |    16    |    No    |    The %ls data type is invalid for return values.    |
|    2735    |    16    |    No    |    Cannot create primary xml or spatial index '%.*ls' on '%.*ls' because PRIMARY KEY constraint contains column(s) of type timestamp.    |
|    2738    |    16    |    No    |    A table can only have one timestamp column. Because table '%.*ls' already has one, the column '%.*ls' cannot be added.    |
|    2739    |    16    |    No    |    The text, ntext, and image data types are invalid for local variables.    |
|    2740    |    16    |    No    |    SET LANGUAGE failed because '%.*ls' is not an official language name or a language alias on this SQL Server.    |
|    2741    |    16    |    No    |    SET DATEFORMAT date order '%.*ls' is invalid.    |
|    2742    |    16    |    No    |    SET DATEFIRST %d is out of range.    |
|    2743    |    16    |    No    |    %ls statement requires %S_MSG parameter.    |
|    2744    |    16    |    No    |    Multiple identity columns specified for table '%.*ls'. Only one identity column per table is allowed.    |
|    2745    |    10    |    No    |    Process ID %d has raised user error %d, severity %d. SQL Server is terminating this process.    |
|    2747    |    16    |    No    |    Too many substitution parameters for RAISERROR. Cannot exceed %d substitution parameters.    |
|    2748    |    16    |    No    |    Cannot specify %ls data type (parameter %d) as a substitution parameter.    |
|    2749    |    16    |    No    |    Identity column '%.*ls' must be of data type int, bigint, smallint, tinyint, or decimal or numeric with a scale of 0, and constrained to be nonnullable.    |
|    2750    |    16    |    No    |    Column or parameter #%d: Specified column precision %d is greater than the maximum precision of %d.    |
|    2751    |    16    |    No    |    Column or parameter #%d: Specified column scale %d is greater than the specified precision of %d.    |
|    2752    |    16    |    No    |    Identity column '%.*ls' contains invalid SEED.    |
|    2753    |    16    |    No    |    Identity column '%.*ls' contains invalid INCREMENT.    |
|    2754    |    16    |    No    |    Error severity levels greater than %d can only be specified by members of the sysadmin role, using the WITH LOG option.    |
|    2755    |    16    |    No    |    SET DEADLOCK_PRIORITY option is invalid. Valid options are {HIGH \| NORMAL \| LOW \| [%d ... %d] of type integer}.    |
|    2756    |    16    |    No    |    Invalid value %d for state. Valid range is from %d to %d.    |
|    2759    |    16    |    No    |    CREATE SCHEMA failed due to previous errors.    |
|    2760    |    16    |    No    |    The specified schema name "%.*ls" either does not exist or you do not have permission to use it.    |
|    2761    |    16    |    No    |    The ROWGUIDCOL property can only be specified on the uniqueidentifier data type.    |
|    2762    |    16    |    No    |    sp_setapprole was not invoked correctly. Refer to the documentation for more information.    |
|    2766    |    16    |    No    |    The definition for user-defined data type '%.*ls' has changed.    |
|    2767    |    15    |    No    |    Could not locate statistics '%.*ls' in the system catalogs.    |
|    2770    |    16    |    No    |    The SELECT INTO statement cannot have same source and destination tables.    |
|    2772    |    16    |    No    |    Cannot access temporary tables from within a function.    |
|    2773    |    16    |    No    |    The collation ID is corrupted because the sort order ID %d is not valid.    |
|    2774    |    16    |    No    |    Collation ID %d is invalid.    |
|    2775    |    16    |    No    |    The code page %d is not supported by the server.    |
|    2778    |    16    |    No    |    Only System Administrator can specify %s option for %s command.    |
|    2779    |    16    |    No    |    The %S_MSG '%.*ls' is an auto-drop system object. It cannot be used in queries or DDL.    |
|    2780    |    16    |    No    |    View '%.*ls' is not schemabound.    |
|    2782    |    16    |    No    |    Cannot create table "%.*ls": A table must have a clustered primary key in order to have XML data type columns.    |
|    2785    |    16    |    No    |    User-defined functions, user-defined aggregates, CLR types, and methods on CLR types are not allowed in expressions in this context.    |
|    2786    |    16    |    No    |    The data type of substitution parameter %d does not match the expected type of the format specification.    |
|    2787    |    16    |    No    |    Invalid format specification: '%.*ls'.    |
|    2788    |    16    |    No    |    Synonyms are invalid in a schemabound object or a constraint expression.    |
|    2789    |    16    |    No    |    Must specify a two-part name for %S_MSG '%.*ls' in a schemabound object or a constraint expression.    |
|    2790    |    16    |    No    |    Cannot use a column of type TEXT, NTEXT, or IMAGE in a constraint expression.    |
|    2791    |    16    |    No    |    Could not resolve expression for Schema-bound object or constraint.    |
|    2792    |    16    |    No    |    Cannot specify a sql CLR type in a Schema-bound object or a constraint expression.    |
|    2793    |    16    |    No    |    Specified owner name '%.*ls' either does not exist or you do not have permission to act on its behalf.    |
|    2794    |    16    |    No    |    Message text expects more than the maximum number of arguments (%d).    |
|    2795    |    16    |    No    |    Could not %S_MSG %S_MSG because the new %S_MSG '%.*ls' does not match the FILESTREAM %S_MSG '%.*ls' of the table.    |
|    2796    |    16    |    No    |    Cannot specify database name with $partition in a Schema-bound object, computed column or constraint expression.    |
|    2797    |    16    |    No    |    The default schema does not exist.    |
|    2798    |    16    |    No    |    Cannot create index or statistics '%.*ls' on table '%.*ls' because SQL Server cannot verify that key column '%.*ls' is precise and deterministic. Consider removing column from index or statistics key, marking computed column persisted, or using non-CLR-derived column in key.    |
|    2799    |    16    |    No    |    Cannot create index or statistics '%.*ls' on table '%.*ls' because the computed column '%.*ls' is imprecise and not persisted. Consider removing column from index or statistics key or marking computed column persisted.    |
|    2801    |    16    |    No    |    The definition of object '%.*ls' has changed since it was compiled.    |
|    2802    |    10    |    No    |    SQL Server has encountered %d occurrence(s) of cachestore flush for the '%s' cachestore (part of plan cache) due to 'DBCC FREEPROCCACHE' or 'DBCC FREESYSTEMCACHE' operations.    |
|    2803    |    10    |    No    |    SQL Server has encountered %d occurrence(s) of cachestore flush for the '%s' cachestore (part of plan cache) due to some database maintenance or reconfigure operations.    |
|    2809    |    18    |    No    |    The request for %S_MSG '%.*ls' failed because '%.*ls' is a %S_MSG object.    |
|    2812    |    16    |    No    |    Could not find stored procedure '%.*ls'.    |
|    2813    |    16    |    No    |    %.*ls is not supported on this edition of SQL Server.    |
|    [2814](../../relational-databases/errors-events/mssqlserver-2814-database-engine-error.md)    |    10    |    No    |    A possible infinite recompile was detected for SQLHANDLE %hs, PlanHandle %hs, starting offset %d, ending offset %d. The last recompile reason was %d.    |
|    2628    |    16    |    No    |    String or binary data would be truncated in table '%.*ls', column '%.*ls'. Truncated value: '%.*ls'.    |
