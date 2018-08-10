-- Plan Guide snippets

--<snippetPlanGuides1>
USE AdventureWorks2012;
GO
SELECT cp.plan_handle, sql_handle, st.text, objtype 
FROM sys.dm_exec_cached_plans AS cp
JOIN sys.dm_exec_query_stats AS qs ON cp.plan_handle = qs.plan_handle
CROSS APPLY sys.dm_exec_sql_text(sql_handle) AS st;
GO
--</snippetPlanGuides1>

--<snippetPlanGuides2>
USE AdventureWorks2012;
GO
SELECT WorkOrderID, p.Name, OrderQty, DueDate
FROM Production.WorkOrder AS w 
JOIN Production.Product AS p ON w.ProductID = p.ProductID
WHERE p.ProductSubcategoryID > 4
ORDER BY p.Name, DueDate;
GO
-- Inspect the query plan by using dynamic management views.
SELECT * FROM sys.dm_exec_query_stats AS qs
CROSS APPLY sys.dm_exec_sql_text(sql_handle)
CROSS APPLY sys.dm_exec_text_query_plan(qs.plan_handle, qs.statement_start_offset, qs.statement_end_offset) AS qp
WHERE text LIKE N'SELECT WorkOrderID, p.Name, OrderQty, DueDate%';
GO
-- Create a plan guide for the query by specifying the query plan in the plan cache.
DECLARE @plan_handle varbinary(64);
DECLARE @offset int;
SELECT @plan_handle = plan_handle, @offset = qs.statement_start_offset
FROM sys.dm_exec_query_stats AS qs
CROSS APPLY sys.dm_exec_sql_text(sql_handle) AS st
CROSS APPLY sys.dm_exec_text_query_plan(qs.plan_handle, qs.statement_start_offset, qs.statement_end_offset) AS qp
WHERE text LIKE N'SELECT WorkOrderID, p.Name, OrderQty, DueDate%';

EXECUTE sp_create_plan_guide_from_handle 
    @name =  N'Guide1',
    @plan_handle = @plan_handle,
    @statement_start_offset = @offset;
GO
-- Verify that the plan guide is created.
SELECT * FROM sys.plan_guides
WHERE scope_batch LIKE N'SELECT WorkOrderID, p.Name, OrderQty, DueDate%';
GO
--</snippetPlanGuides2>

--<snippetPlanGuides3>
USE AdventureWorks2012;
GO
SELECT * FROM Production.Product WHERE ProductSubcategoryID > 4;
SELECT * FROM Person.Address;
SELECT * FROM Production.Product WHERE ProductSubcategoryID > 10;
GO

-- Examine the query plans for this batch
SELECT * FROM sys.dm_exec_query_stats AS qs
CROSS APPLY sys.dm_exec_sql_text(sql_handle)
CROSS APPLY sys.dm_exec_text_query_plan(qs.plan_handle, qs.statement_start_offset, qs.statement_end_offset) AS qp
WHERE text LIKE N'SELECT * FROM Production.Product WHERE ProductSubcategoryID > 4%';
GO

-- Create plan guides for the first and third statements in the batch by specifying the statement offsets.
BEGIN TRANSACTION

DECLARE @plan_handle varbinary(64);
DECLARE @offset int;

SELECT @plan_handle = plan_handle, @offset = qs.statement_start_offset
FROM sys.dm_exec_query_stats AS qs
CROSS APPLY sys.dm_exec_sql_text(sql_handle) AS st
CROSS APPLY sys.dm_exec_text_query_plan(qs.plan_handle, qs.statement_start_offset, qs.statement_end_offset) AS qp
WHERE text LIKE N'SELECT * FROM Production.Product WHERE ProductSubcategoryID > 4%'
AND SUBSTRING(st.text, (qs.statement_start_offset/2) + 1,
                        ((CASE statement_end_offset 
                              WHEN -1 THEN DATALENGTH(st.text)
                              ELSE qs.statement_end_offset END 
                              - qs.statement_start_offset)/2) + 1)  like 'SELECT * FROM Production.Product WHERE ProductSubcategoryID > 4%'

EXECUTE sp_create_plan_guide_from_handle 
    @name =  N'Guide_Statement1_only',
    @plan_handle = @plan_handle,
    @statement_start_offset = @offset;

SELECT @plan_handle = plan_handle, @offset = qs.statement_start_offset
FROM sys.dm_exec_query_stats AS qs
CROSS APPLY sys.dm_exec_sql_text(sql_handle) AS st
CROSS APPLY sys.dm_exec_text_query_plan(qs.plan_handle, qs.statement_start_offset, qs.statement_end_offset) AS qp
WHERE text LIKE N'SELECT * FROM Production.Product WHERE ProductSubcategoryID > 4%'
AND SUBSTRING(st.text, (qs.statement_start_offset/2) + 1,
                        ((CASE statement_end_offset 
                              WHEN -1 THEN DATALENGTH(st.text)
                              ELSE qs.statement_end_offset END 
                              - qs.statement_start_offset)/2) + 1)  like 'SELECT * FROM Production.Product WHERE ProductSubcategoryID > 10%'

EXECUTE sp_create_plan_guide_from_handle 
    @name =  N'Guide_Statement3_only',
    @plan_handle = @plan_handle,
    @statement_start_offset = @offset;

COMMIT TRANSACTION
GO

-- Verify the plan guides are created.
SELECT * FROM sys.plan_guides;
GO
--</snippetPlanGuides3>


