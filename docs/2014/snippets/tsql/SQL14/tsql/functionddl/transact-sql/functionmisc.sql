--System Function examples

--<snippetFunctionMisc1>
SELECT *
FROM sys.fn_helpcollations();
GO
--</snippetFunctionMisc1>
--<snippetFunctionMisc2>
-- Display metadata about the user-defined functions in AdventureWorks2012.
USE AdventureWorks2012;
GO
SELECT *
FROM sys.objects
WHERE type IN ('IF','TF','FN','FS','FT');
GO
-- Return parameters associated with the functions
SELECT o.name AS FunctionName, p.*
FROM sys.objects AS o
JOIN sys.parameters AS p ON o.object_id = p.object_ID
WHERE type IN ('IF','TF','FN','FS','FT');
GO
--</snippetFunctionMisc2>
--<snippetFunctionMisc3>
USE AdventureWorks2012;
GO
SELECT OBJECT_DEFINITION(OBJECT_ID('dbo.ufnGetContactInformation'));
GO
--</snippetFunctionMisc3>
--<snippetFunctionMisc4>
USE AdventureWorks2012;
GO
SELECT d.class, OBJECT_NAME(d.object_id) AS ObjectName, 
    OBJECT_NAME(referenced_major_id) AS ReferencedObjectName, 
    referenced_minor_id AS ReferencedColumnID,
    c.name AS ReferencedColumnName,
    is_selected, is_updated, is_select_all 
FROM sys.sql_dependencies AS d
JOIN sys.columns AS c ON c.object_id = d.referenced_major_id
    AND c.column_id = d.referenced_minor_id
WHERE d.object_id = OBJECT_ID(N'AdventureWorks2012.dbo.ufnGetContactInformation');
GO
--</snippetFunctionMisc4>
--<snippetFunctionMisc4_ktm>
USE AdventureWorks2012;
GO
SELECT OBJECT_NAME(d.referencing_id) AS referencing_entity, 
    OBJECT_NAME(referenced_id) AS referenced_entity, 
    referenced_minor_id AS referenced_column_id,
    c.name AS referenced_column 
FROM sys.sql_expression_dependencies AS d
JOIN sys.columns AS c ON c.object_id = d.referenced_id
    AND c.column_id = d.referenced_minor_id
WHERE d.referencing_id = OBJECT_ID(N'AdventureWorks2012.dbo.ufnGetContactInformation');
--</snippetFunctionMisc4_ktm>

--<snippetOBJECT_SCHEMA_NAME1>
USE AdventureWorks2012;
GO
SELECT DISTINCT OBJECT_SCHEMA_NAME(object_id)
FROM master.sys.objects;
GO
--</snippetOBJECT_SCHEMA_NAME1>

--<snippetOBJECT_SCHEMA_NAME2>
USE AdventureWorks2012;
GO
SELECT DISTINCT OBJECT_SCHEMA_NAME(object_id, 1) AS schema_name
FROM master.sys.objects;
GO
--</snippetOBJECT_SCHEMA_NAME2>


--<snippetOBJECT_SCHEMA_NAME3>
SELECT DB_NAME(st.dbid) AS database_name, 
    OBJECT_SCHEMA_NAME(st.objectid, st.dbid) AS schema_name,
    OBJECT_NAME(st.objectid, st.dbid) AS object_name, 
    st.text AS query_statement
FROM sys.dm_exec_query_stats AS qs
CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) AS st
WHERE st.objectid IS NOT NULL;
GO

--</snippetOBJECT_SCHEMA_NAME3>
--<snippetOBJECT_SCHEMA_NAME4>
SELECT QUOTENAME(DB_NAME(database_id)) 
    + N'.' 
    + QUOTENAME(OBJECT_SCHEMA_NAME(object_id, database_id)) 
    + N'.' 
    + QUOTENAME(OBJECT_NAME(object_id, database_id))
    , * 
FROM sys.dm_db_index_operational_stats(null, null, null, null);
GO
--</snippetOBJECT_SCHEMA_NAME4>