-- DROP FUNCTION DDL examples

--<snippetDropFunction1>
USE AdventureWorks2012;
GO
IF OBJECT_ID (N'Sales.fn_SalesByStore', N'IF') IS NOT NULL
    DROP FUNCTION Sales.fn_SalesByStore;
GO
--</snippetDropFunction1>

