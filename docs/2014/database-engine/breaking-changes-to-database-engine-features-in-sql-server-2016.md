---
title: "Breaking Changes to Database Engine Features in SQL Server 2014 | Microsoft Docs"
ms.custom: ""
ms.date: "11/27/2018"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: release-landing
ms.topic: conceptual
helpviewer_keywords: 
  - "Database Engine [SQL Server], what's new"
  - "breaking changes [SQL Server]"
ms.assetid: 47edefbd-a09b-4087-937a-453cd5c6e061
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Breaking Changes to Database Engine Features in SQL Server 2014
  This topic describes breaking changes in the [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] [!INCLUDE[ssDE](../includes/ssde-md.md)] and earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. These changes might break applications, scripts, or functionalities that are based on earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. You might encounter these issues when you upgrade. For more information, see [Use Upgrade Advisor to Prepare for Upgrades](../sql-server/install/use-upgrade-advisor-to-prepare-for-upgrades.md).  
  
##  <a name="SQL14"></a> Breaking Changes in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)]  
 No new issues.  
  
##  <a name="Denali"></a> Breaking Changes in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]  
  
### Transact-SQL  
  
|Feature|Description|  
|-------------|-----------------|  
|Selecting from columns or tables named NEXT|Sequences use the ANSI standard NEXT VALUE FOR function. If a table or a column is named NEXT and the table or column is aliased as VALUE, and if the ANSI standard AS is omitted, the resultant statement can cause an error. To work around, include the ANSI standard AS keyword. For example, `SELECT NEXT VALUE FROM Table` should be rewritten as `SELECT NEXT AS VALUE FROM Table` and `SELECT Col1 FROM NEXT VALUE` should be rewritten as `SELECT Col1 FROM NEXT AS VALUE`.|  
|PIVOT operator|The PIVOT operator is not allowed in a recursive common table expression (CTE) query when the database compatibility level is set to 110. Rewrite the query, or change the compatibility level to 100 or lower. Using PIVOT in a recursive CTE query produces incorrect results when there is more than a single row per grouping.|  
|sp_setapprole and sp_unsetapprole|The cookie `OUTPUT` parameter for `sp_setapprole` is currently documented as `varbinary(8000)` which is the correct maximum length. However the current implementation returns `varbinary(50)`. Applications should continue to reserve `varbinary(8000)` so that the application continues to operate correctly if the cookie return size increases in a future release. For more information, see [sp_setapprole &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-setapprole-transact-sql).|  
|EXECUTE AS|The cookie OUTPUT parameter for EXECUTE AS is currently documented as `varbinary(8000)` which is the correct maximum length. However the current implementation returns `varbinary(100)`. Applications should continue to reserve `varbinary(8000)` so that the application continues to operate correctly if the cookie return size increases in a future release. For more information, see [EXECUTE AS &#40;Transact-SQL&#41;](/sql/t-sql/statements/execute-as-transact-sql).|  
|sys.fn_get_audit_file function|Two additional columns (**user_defined_event_id** and **user_defined_information**) have been added to support user-defined audit events. Applications that do not select columns by name might return more columns than expected. Either select columns by name, or adjust the application to accept these additional columns.|  
|WITHIN reserved keyword|WITHIN is now a reserved keyword. References to objects or columns named 'within' will fail. Rename the object or column name or delimit the name by using brackets or quotes.  For example, `SELECT * FROM [within]`.|  
|CAST and CONVERT operations on computed columns of type `time` or `datetime2`|In earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], the default style for CAST and CONVERT operations on `time` and `datetime2` data types is 121 except when either type is used in a computed column expression. For computed columns, the default style is 0. This behavior impacts computed columns when they are created, used in queries involving auto-parameterization, or used in constraint definitions.<br /><br /> Under compatibility level 110, the default style for CAST and CONVERT operations on `time` and `datetime2` data types is always 121. If your query relies on the old behavior, use a compatibility level less than 110, or explicitly specify the 0 style in the affected query.<br /><br /> Upgrading the database to compatibility level 110 will not change user data that has been stored to disk. You must manually correct this data as appropriate. For example, if you used SELECT INTO to create a table from a source that contained a computed column expression described above, the data (using style 0) would be stored rather than the computed column definition itself. You would need to manually update this data to match style 121.|  
|ALTER TABLE|The ALTER TABLE statement allows only two-part (schema.object) table names. Specifying a table name using the following formats now fails at compile time with error 117:<br /><br /> server.database.schema.table<br /><br /> .database.schema.table<br /><br /> ..schema.table<br /><br /> In earlier versions specifying the format server.database.schema.table returned error 4902. Specifying the format .database.schema.table or the format ..schema.table succeeded. To resolve the problem, remove the use of a 4-part prefix.|  
|Browsing metadata|Querying a view using FOR BROWSE or SET NO_BROWSETABLE ON now returns the metadata of the view, not the metadata of the underlying object. This behavior now matches other methods of browsing metadata.|  
|SOUNDEX|Under database compatibility level 110, the SOUNDEX function implements new rules that may cause the values computed by the function to be different than the values computed under earlier compatibility levels. After upgrading to compatibility level 110, you may need to rebuild the indexes, heaps, or CHECK constraints that use the SOUNDEX function. For more information, see [SOUNDEX &#40;Transact-SQL&#41;](/sql/t-sql/functions/soundex-transact-sql)
 .|  
|Row count message for failed DML statements|In [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], the [!INCLUDE[ssDE](../includes/ssde-md.md)] will consistently send the TDS DONE token with RowCount: 0 to clients when a DML statement fails. In earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], an incorrect value of -1 is sent to the client when the DML statement that fails is contained in a TRY-CATCH block and is either autoparameterized by the [!INCLUDE[ssDE](../includes/ssde-md.md)] or the TRY-CATCH block is not on the same level as the failed statement. For example, if a TRY-CATCH block calls a stored procedure and a DML statement in the procedure fails, the client will incorrectly receive a -1 value.<br /><br /> Applications that rely on this incorrect behavior will fail.|  
|SERVERPROPERTY ('Edition')|Installed product edition of the instance of [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]. Use the value of this property to determine the features and the limits, such as maximum number of CPUs that are supported by the installed product.<br /><br /> Based on the installed Enterprise edition, this can return 'Enterprise Edition' or 'Enterprise Edition: Core-based Licensing'. The Enterprise editions are differentiated based on the maximum compute capacity by a single instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For more information on Compute capacity limits in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], see [Compute Capacity Limits by Edition of SQL Server](../sql-server/compute-capacity-limits-by-edition-of-sql-server.md).|  
|CREATE LOGIN|The `CREATE LOGIN WITH PASSWORD = '`*password*`' HASHED` option cannot be used with hashes created by [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] 7 or earlier.|  
|CAST and CONVERT operations for `datetimeoffset`|The only styles that are supported when converting from date and time types to `datetimeoffset` are 0 or 1. All other conversion styles return error 9809. For example, the following code returns error 9809.<br /><br /> `SELECT CONVERT(date, CAST('7070-11-25 16:25:01.00986 -02:07' as datetimeoffset(5)), 107);`|  
  
### Dynamic Management Views  
  
|View|Description|  
|----------|-----------------|  
|sys.dm_exec_requests|The command column changes from `nvarchar(16)` to `nvarchar(32)`.|  
|sys.dm_os_memory_cache_counters|The following columns have been renamed:<br /><br /> single_pages_kb is now: <br />                          pages_kb<br /><br /> multi_pages_kb<br />                           is now: pages_in_use_kb|  
|sys.dm_os_memory_cache_entries|The column pages_allocated_count column has been renamed pages_kb.|  
|sys.dm_os_memory_clerks|The column multi_pages_kb has been removed.<br /><br /> The column single_pages_kb column has been renamed pages_kb.|  
|sys.dm_os_memory_nodes|The following columns have been renamed:<br /><br /> single_pages_kb is now: <br />                            pages_kb<br /><br /> multi_pages_kb is now: <br />                            foreign_committed_kb|  
|sys.dm_os_memory_objects|The following columns have been renamed.<br /><br /> pages_allocated_count is now:<br />                            pages_in_bytes<br /><br /> max_pages_allocated_count is now: max_pages_in_bytes|  
|sys.dm_os_sys_info|The following columns have been renamed:<br /><br /> physical_memory_in_bytes is now: <br />                            physical_memory_kb<br /><br /> bpool_commit_target is now: <br />                            committed_target_kb<br /><br /> bpool_visible is now: <br />                            visible_target_kb<br /><br /> virtual_memory_in_bytes is now: <br />                            virtual_memory_kb<br /><br /> bpool_commited is now:<br />                            committed_kb|  
|sys.dm_os_workers|The locale column has been removed.|  
  
### Catalog Views  
  
|View|Description|  
|----------|-----------------|  
|sys.data_spaces<br /><br /> sys.partition_schemes<br /><br /> sys.filegroups<br /><br /> sys.partition_functions|A new column, is_system, has been added to sys.data_spaces and sys.partition_functions. (sys.partition_schemes and sys.filegroups inherit the columns of sys.data_spaces.)<br /><br /> A value of 1 in this column indicates that the object is used for full-text index fragments.<br /><br /> In sys.partition_functions, sys.partition_schemes, and sys.filegroups, the new column is not the last column. Revise existing queries that rely on the order of columns returned from these catalog views.|  
  
### SQL CLR Data Types (geometry, geography, and hierarchyid)  
 The assembly **Microsoft.SqlServer.Types.dll**, which contains the spatial data types and the hierarchyid type, has been upgraded from version 10.0 to version 11.0. Custom applications that reference this assembly may fail when the following conditions are true.  
  
-   When you move a custom application from a computer on which [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)] was installed to a computer on which only [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] is installed, the application will fail because the referenced version 10.0 of the **SqlTypes** assembly is not present. You may see this error message: `"Could not load file or assembly 'Microsoft.SqlServer.Types, Version=10.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91' or one of its dependencies. The system cannot find the file specified."`  
  
-   When you reference the **SqlTypes** assembly version 11.0, and version 10.0 is also installed, you may see this error message: `"System.InvalidCastException: Unable to cast object of type 'Microsoft.SqlServer.Types.SqlGeometry' to type 'Microsoft.SqlServer.Types.SqlGeometry'."`  
  
-   When you reference the **SqlTypes** assembly version 11.0 from a custom application that targets .NET 3.5, 4, or 4.5, the application will fail because SqlClient by design loads version 10.0 of the assembly. This failure occurs when the application calls one of the following methods:  
  
    -   `GetValue` method of the `SqlDataReader` class  
  
    -   `GetValues` method of the `SqlDataReader` class  
  
    -   bracket index operator [] of the `SqlDataReader` class  
  
    -   `ExecuteScalar` method of the `SqlCommand` class  
  
 You can work around this issue by using one of the following methods:  
  
-   You can work around this issue in your code by calling the `GetSqlBytes` method, instead of the Get methods listed above, to retrieve CLR [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] system types, as shown in the following example:  
  
    ```csharp  
    string query = "SELECT [SpatialColumn] FROM [SpatialTable]";  
          using (SqlConnection conn = new SqlConnection("..."))  
          {  
                SqlCommand cmd = new SqlCommand(query, conn);  
  
                conn.Open();  
                SqlDataReader reader = cmd.ExecuteReader();  
  
                while (reader.Read())  
                {  
                      // In version 11.0 only  
                      SqlGeometry g =   
    SqlGeometry.Deserialize(reader.GetSqlBytes(0));  
  
                      // In version 10.0 or 11.0  
                      SqlGeometry g2 = new SqlGeometry();  
                      g.Read(new BinaryReader(reader.GetSqlBytes(0).Stream));  
                }  
          }  
    ```  
  
-   You can work around this issue by using assembly redirection in the application configuration file, as shown in the following example:  
  
    ```xml  
    <runtime>  
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">  
        ...  
        <dependentAssembly>  
            <assemblyIdentity name="Microsoft.SqlServer.Types" publicKeyToken="89845dcd8080cc91" culture="neutral" />  
            <bindingRedirect oldVersion="10.0.0.0" newVersion="11.0.0.0" />  
        </dependentAssembly>  
        ...  
    </assemblyBinding>  
    <runtime>  
    ```  
  
-   You can work around this issue in your connection string by specifying a value of "SQL Server 2012" for the "Type System Version" attribute to force SqlClient to load version 11.0 of the assembly. This connection string attribute is available only in .NET 4.5 and above.  
  
-   The `assemblyBinding` tag should be wrapped under the `runtime` tag.  
  
### Support for AWE  
 32-bit Address Windowing Extensions (AWE) support is discontinued. This might result in slower performance on 32-bit operating systems. For installations using large amounts of memory, migrate to a 64-bit operating system.  
  
### XQuery Functions Are Surrogate-Aware  
 The W3C recommendation for XQuery functions and operators requires them to count a surrogate pair that represents a high-range Unicode character as a single glyph in UTF-16 encoding. However, in versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] prior to [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], string functions did not recognize surrogate pairs as a single character. Some string operations - such as string length calculations and substring extractions - returned incorrect results. [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] now fully supports UTF-16 and the correct handling of surrogate pairs.  
  
 The XML data type in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] only allows well-formed surrogate pairs. However, some functions can still return undefined or unexpected results in certain circumstances, since it is possible to pass invalid or partial surrogate pairs to XQuery functions as string values. Consider the following methods for generating string values when using XQuery in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]:  
  
-   Provide a constant string value as a binary value. When using this method, it remains possible to pass invalid or partial surrogate pairs.  
  
-   Provide a constant string value by providing character entities. When using this method, it is not possible to pass invalid surrogate pairs. The XQuery functions require a single character entity for the high-level character. These functions raise an error if the character entities for the surrogate pair characters are provided.  
  
-   Import external values by using **sql:column** or **sql:variable**. When using these methods, it remains possible to introduce invalid or partial surrogate pairs.  
  
#### Affected XQuery Functions and Operators  
 The following XQuery functions and operators now handle UTF-16 surrogate pairs correctly in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]:  
  
-   **fn:string-length**. However, if an invalid or partial surrogate pair is passed as an argument, the behavior of **string-length** is undefined.  
  
-   **fn:substring**.  
  
-   **fn:contains**. However, if a partial surrogate pair is passed as a value, **contains** may return unexpected results, since it may find the partial surrogate pair contained in well-formed surrogate pair.  
  
-   **fn:concat**. However, if a partial surrogate pair is passed as a value, **concat** can produce incorrect surrogate pairs or partial surrogate pairs.  
  
-   Comparison operators and the **order by** clause. Comparison operators include +, \<, >, \<=, >=, `eq`, `lt`, `gt`, `le`, and `ge`.  
  
#### Distributed Query Calls to a System Procedure  
 Distributed query calls through `OPENQUERY` to some system procedures will fail when called from one [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] server to another. This occurs when the [!INCLUDE[ssDE](../includes/ssde-md.md)] cannot discovery metadata for a procedure. For example, `SELECT * FROM OPENQUERY(..., 'EXEC xp_loginfo')`.  
  
#### Isolation Level and sp_reset_connection  
 The isolation level for connections is handled in the following way by client drivers:  
  
-   All of the native drivers (SNAC, MDAC, ODBC) set the isolation level (based on app setting) upon sp_reset_connection.  
  
-   For ADO.NET you will essentially get a random isolation level depending on which connection you get from the pool (and if the application uses different isolation level). Since ADO.NET pool can recycle connections internally and transparently, you cannot predict what will come out of the pool.  
  
-   For the JDBC driver you get same behavior as ADO.NET  
  
     The application must always explicitly set isolation level after opening the connection to get what it wants.  
  
     The JDBC connection can be pooled, so the application may get a random isolation level and not know about it.  
  
 To preserve backward compatibility this new behavior only applies for recent clients beginning with TDS 7.4.  
  
#### Backward Compatibility  
 **New behavior depends on compatibility level**  
  
 The following functions and operators demonstrate the new behavior described above only when the compatibility level is 110 or higher:  
  
-   **fn:contains**.  
  
-   **fn:concat**.  
  
-   comparison operators and **order by** clause  
  
 **New behavior depends on default namespace URI for functions**  
  
 The following functions demonstrate the new behavior described above only when the default namespace URI corresponds to the namespace in the final recommendation, that is, [http://www.w3.org/2005/xpath-functions](http://www.w3.org/2005/xpath-functions). When the compatibility level is 110 or higher, then by default [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] binds the default function namespace to this namespace. However these functions demonstrate the new behavior when this namespace is used regardless of the compatibility level.  
  
-   **fn:string-length**  
  
-   **fn:substring**  
  
##  <a name="KJKatmai"></a> Breaking Changes in SQL Server 2008/SQL Server 2008R2  
 This section contains the breaking changes introduced in [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)]. No changes were introduced in [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)].  
  
### Collations  
  
|Feature|Description|  
|-------------|-----------------|  
|New collations|[!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] introduces new collations that are in full alignment with the collations provided by Windows Server 2008. These 80 new collations have improved linguistic accuracy and are denoted by *_100 version references. If you choose a new collation for your server or database, be aware that the collation may not be recognized by clients with older client drivers. Unrecognized collations can cause the application to return errors and fail. Consider the following solutions:<br /><br /> Upgrade the client operating system so that the underlying system collations are updated.<br /><br /> If your client has database client software installed, consider applying a service update to the database client software.<br /><br /> Choose an existing collation that maps to a code page on the client.|  
  
### Common Language Runtime (CLR)  
  
|Feature|Description|  
|-------------|-----------------|  
|CLR Assemblies|When a database is upgraded to [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)], the `Microsoft.SqlServer.Types` assembly to support new data types is automatically installed. Upgrade Advisor rules detect any user type or assemblies with conflicting names. The Upgrade Advisor will advise renaming of any conflicting assembly, and either renaming any conflicting type, or using two-part names in the code to refer to that preexisting user type.<br /><br /> If a database upgrade detects a user assembly with a conflicting name, it will automatically rename that assembly and put the database into suspect mode.<br /><br /> If a user type with conflicting name exists during the upgrade, no special steps are taken. After the upgrade, both the old user type, and the new system type, will exist. The user type will be available only through two-part names.|  
|CLR Assemblies|[!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] installs .NET Framework 3.5 SP1, which updates libraries in the Global Assembly Cache (GAC). If you have unsupported libraries registered in a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database, your [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] application may stop working after upgrading to [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)]. This is because servicing or upgrading libraries in the GAC does not update assemblies inside [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. If an assembly exists both in a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database and in the GAC, the two copies of the assembly must exactly match. If they do not match, an error will occur when the assembly is used by [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] CLR integration. For more information, see [Supported .NET Framework Libraries](../relational-databases/clr-integration/database-objects/supported-net-framework-libraries.md).<br /><br /> After upgrading your database, service or upgrade the copy of the assembly inside your [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] databases with the ALTER ASSEMBLY statement. For more information, see [Knowledge Base article 949080](https://go.microsoft.com/fwlink/?LinkId=154563).<br /><br /> To detect whether you are using any unsupported .NET framework library in your application, run the following query in your database.<br /><br /> `SELECT name FROM sys.assemblies WHERE clr_name LIKE '%publickeytoken=b03f5f7f11d50a3a,%';`|  
|CLR Routines|Using impersonation inside CLR user-defined functions, user-defined aggregates, or user-defined types (UDTs) may cause your application to fail with error 6522 after upgrading to [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)]. The following scenarios succeed in [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)] but fail in [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)]. Resolutions are provided for each scenario.<br /><br /> A CLR user-defined function, user-defined aggregate, or UDT method that uses impersonation has a parameter of type `nvarchar(max)`, `varchar(max)`, `varbinary(max)`, `ntext`, `text`, `image`, or a large UDT, and does not have the **DataAccessKind.Read** attribute on the method. To resolve this issue, add the **DataAccessKind.Read** attribute on the method, recompile the assembly, and re-deploy the routine and the assembly.<br /><br /> A CLR table-valued function that has an **Init** method that performs impersonation. To resolve this issue add the **DataAccessKind.Read** attribute on the method, recompile the assembly, and re-deploy the routine and the assembly.<br /><br /> A CLR table-valued function that has a **FillRow** method that performs impersonation. To resolve this issue, remove impersonation from the **FillRow** method. Do not access external resources by using the **FillRow** method. Instead, access external resources from the **Init** method.|  
  
### Dynamic Management Views  
  
|View|Description|  
|----------|-----------------|  
|sys.dm_os_sys_info|Removed the cpu_ticks_in_ms and sqlserver_start_time_cpu_ticks columns.|  
|sys.dm_exec_query_resource_semaphoressys.dm_exec_query_memory_grants|The resource_semaphore_id column is not a unique ID in [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)]. This change can affect troubleshooting query execution. For more information, see [sys.dm_exec_query_resource_semaphores &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-query-resource-semaphores-transact-sql).|  
  
### Errors and Events  
  
|Feature|Description|  
|-------------|-----------------|  
|Login errors|In [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)], error 18452 is returned when a SQL login is used to connect to a server configured to use only Windows Authentication. In [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)], error 18456 is returned instead.|  
  
### Showplan  
  
|Feature|Description|  
|-------------|-----------------|  
|Showplan XML schema|A new **SeekPredicateNew** element is added to the Showplan XML schema, and the enclosing xsd sequence (**SqlPredicatesType**) is converted into an **\<xsd:choice>** item. Instead of one or more **SeekPredicate** elements, one or more **SeekPredicateNew** elements may now appear in the Showplan XML. The two elements are mutually exclusive. **SeekPredicate** is maintained in the Showplan XML schema for backwards compatibility; however, query plans created in [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] may contain the **SeekPredicateNew** element. Applications that expect to retrieve only the **SeekPredicate** child from the node ShowPlanXML/BatchSequence/Batch/Statements/StmtSimple/QueryPlan/RelOp/IndexScan/SeekPredicates may fail if the **SeekPredicate** element does not exist. Rewrite the application to expect either the **SeekPredicate** or **SeekPredicateNew** element in this node. For more information, see .|  
|Showplan XML schema|A new **IndexKind** attribute is added to the **ObjectType** complex type in the Showplan XML schema. Applications that strictly validate [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] plans against the [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)] schema will fail.|  
  
### Transact-SQL  
  
|Feature|Description|  
|-------------|-----------------|  
|ALTER_AUTHORIZATION_DATABASE DDL event|In [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)], when the DDL event ALTER_AUTHORIZATION_DATABASE fires, the value 'object' is returned in the **ObjectType** element of the EVENTDATA xml for this event when the entity type of the securable in the data definition language (DDL) operation is an object. In [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)], the actual type (for example, 'table', or 'function') is returned.|  
|CONVERT|If an invalid style is passed to the CONVERT function, an error is returned when the type of conversion is binary to character or character to binary. In earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], the invalid style is set to the default style for binary-to-character and character-to-binary conversions.|  
|GRANT/DENY/REVOKE EXECUTE on assemblies|EXECUTE permission cannot be granted, denied, or revoked to assemblies. This permission has no affect and now causes an error. Grant, deny, or revoke EXECUTE permission on the stored procedures or functions that reference the assembly method instead.|  
|GRANT/DENY/REVOKE permissions on system types|Permissions cannot be granted, denied, or revoked to system types. In earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], these statements succeed but have no effect. In [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)], an error is returned.|  
|GROUP BY|The GROUP BY clause cannot contain a subquery in an expression that is used for the group by list. In earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], this was allowed. In [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)], error 144 is returned.<br /><br /> For example, the following code will succeed in SQL Server 2005 and fail in SQL Server 2008.<br /><br /> `DECLARE @Test TABLE(a int NOT NULL);` <br /> `INSERT INTO @Test SELECT 1 union ALL SELECT 2;` <br /> `SELECT COUNT(*)` <br /> `FROM @Test` <br /> `GROUP BY CASE WHEN a IN (SELECT t.a FROM @Test AS t)` <br /> `THEN 1 ELSE 0` <br /> `END;`|  
|OUTPUT clause|To prevent nondeterministic behavior, the OUTPUT clause cannot reference a column from a view or inline table-valued function when that column is defined by one of the following methods:<br /><br /> A subquery.<br /><br /> A user-defined function that performs user- or system-data access, or is assumed to perform such access.<br /><br /> A computed column that contains in its definition a user-defined function that performs user- or system-data access.<br /><br /> <br /><br /> When [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] detects such a column in the OUTPUT clause, error 4186 is raised. For more information, see [MSSQLSERVER_4186](../relational-databases/errors-events/mssqlserver-4186-database-engine-error.md).|  
|OUTPUT INTO clause|The target table of the OUTPUT INTO clause cannot have any enabled triggers.|  
|precompute rank server-level option|This option is not supported in [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)]. Modify applications that currently use this feature as soon as possible.|  
|READPAST table hint|You cannot specify the READPAST hint under Snapshot Isolation.<br /><br /> The READPAST hint is ignored when either the READ_COMMITED_SNAPSHOT or ALLOW_SNAPSHOT_ISOLATION database option is set to ON. However, if you combine the READPAST hint with READCOMMITTEDLOCK, the READPAST behavior is same as with the blocking READCOMMITTED hint.|  
|sp_helpuser|The following column names that are returned in the result set of the sp_helpuser stored procedure have changed:<br /><br /> GroupName is now:<br />                            RoleName<br /><br /> Group_name is now:<br />                            Role_name<br /><br /> Group_id is now:<br />                            Role_id<br /><br /> Users_in_group is now:<br />                            Users_in_role|  
|Transparent Data Encryption|Transparent data encryption (TDE) is performed at the I/O level: the page structure is unencrypted in memory and is encrypted only when the page is written to disk. Both the database files and log files are encrypted. Third-party applications that bypass the regular [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] mechanism for accessing pages (for example, by scanning the data or log files directly), will fail when a database uses TDE because the data is encrypted in the files. Such applications can leverage the Window Cryptographic API to develop a solution for decrypting the data outside of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].|  
  
### XQuery  
  
|Feature|Description|  
|-------------|-----------------|  
|Datetime support|In [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)], the data types `xs:time`, `xs:date`, and `xs:dateTime` do not have timezone support. Timezone data is mapped to the UTC timezone. [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)], provides standard conformant behavior, resulting in the following changes:<br /><br /> Values without timezone are validated.<br /><br /> The provided timezone or the absence of a timezone is preserved.<br /><br /> The internal storage representation is modified.<br /><br /> Resolution of stored values is increased.<br /><br /> Negative years are disallowed.<br /><br /> <br /><br /> Note: Modify applications and XQuery expressions to account for the new type values.|  
|XQuery and Xpath expressions|In [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)], steps in an XQuery or XPath expression that begin with a colon (':') are allowed. For example, the following statement contains a name test (`CTR02)` within the path expression that begins with a colon.<br /><br /> `SELECT FileContext.query('for n$ in //CTR return <C>{data )(n$/:CTR02)} </C>) AS Files FROM dbo.MyTable;`<br /><br /> In [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)], this usage is disallowed because it does not conform to XML standards. Error 9341 is returned. Remove the leading colon or specify a prefix for the name test--for example, (n$/CTR02) or (n$/p1:CTR02).|  
  
### Connecting  
  
|Feature|Description|  
|-------------|-----------------|  
|Connecting from [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Native Client using SSL|When connecting with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Native Client, applications that use "SERVER=shortname; FORCE ENCRYPTION=true" with certificate whose Subjects specify Fully Qualified Domain Names (FQDN's) have connected in the past due to relaxed validation. SQL Server 2008 R2 enhances security by enforcing FQDN subjects for certificates. Applications that rely upon relaxed validation must take one of the following actions:<br /><br /> Use the FQDN in the connection string.<br /><br /> -This option does not require recompiling the application if the SERVER keyword of the connection string is configured outside the application.<br /><br /> -This option does not work for applications that have their connection strings hardcoded.<br /><br /> -This option does not work for applications that use Database Mirroring since the mirrored server replies with a simple name.|  
||Add an alias for the shortname to map to the FQDN.<br /><br /> -This option works even for applications that have their connection strings hardcoded.<br /><br /> -This option does not work for applications that use Database Mirroring since the providers don't look up aliases for received failover partner names.|  
||Have a certificate issued for shortname.<br /><br /> -This option works for all applications.|  

## <a name="Yukon"></a> Breaking Changes in SQL Server 2005  

[!INCLUDE[Archived documentation for very old versions of SQL Server](../includes/paragraph-content/previous-versions-archive-documentation-sql-server.md)]

## See Also  
 [Deprecated Database Engine Features in SQL Server 2014](deprecated-database-engine-features-in-sql-server-2016.md)   
 [Behavior Changes to Database Engine Features in SQL Server 2014](../../2014/database-engine/behavior-changes-to-database-engine-features-in-sql-server-2014.md)   
 [Discontinued Database Engine Functionality in SQL Server 2014](discontinued-database-engine-functionality-in-sql-server-2016.md)   
 [SQL Server Database Engine Backward Compatibility](sql-server-database-engine-backward-compatibility.md)   
 [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-compatibility-level)  
 [Breaking Changes to Management Tools Features in SQL Server 2014](breaking-changes-to-management-tools-features-in-sql-server-2014.md?view=sql-server-2014)  
  
