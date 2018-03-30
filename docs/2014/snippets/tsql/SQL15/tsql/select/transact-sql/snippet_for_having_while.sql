
--<snippet_FOR_1>
USE AdventureWorks2012;
GO
SELECT p.BusinessEntityID, FirstName, LastName, PhoneNumber AS Phone
FROM Person.Person AS p
Join Person.PersonPhone AS pph ON p.BusinessEntityID  = pph.BusinessEntityID
WHERE LastName LIKE 'G%'
ORDER BY LastName, FirstName 
FOR XML AUTO, TYPE, XMLSCHEMA, ELEMENTS XSINIL;
--</snippet_FOR_1>

--<snippet_HAVING_1>
USE AdventureWorks2012 ;
GO
SELECT SalesOrderID, SUM(LineTotal) AS SubTotal
FROM Sales.SalesOrderDetail
GROUP BY SalesOrderID
HAVING SUM(LineTotal) > 100000.00
ORDER BY SalesOrderID ;
--</snippet_HAVING_1>

--<snippet_WHILE_1>
USE AdventureWorks2012;
GO
WHILE (SELECT AVG(ListPrice) FROM Production.Product) < $300
BEGIN
   UPDATE Production.Product
      SET ListPrice = ListPrice * 2
   SELECT MAX(ListPrice) FROM Production.Product
   IF (SELECT MAX(ListPrice) FROM Production.Product) > $500
      BREAK
   ELSE
      CONTINUE
END
PRINT 'Too much for the market to bear';
--</snippet_WHILE_1>

--<snippet_WHILE_2>
USE AdventureWorks2012;
GO
DECLARE tnames_cursor CURSOR
FOR
   SELECT s.name + '.' + t.name 
   FROM sys.tables AS t
   JOIN sys.schemas AS s ON s.schema_id = t.schema_id;
OPEN tnames_cursor;
DECLARE @tablename sysname;
FETCH NEXT FROM tnames_cursor INTO @tablename;
WHILE (@@FETCH_STATUS <> -1)
BEGIN
   IF (@@FETCH_STATUS <> -2)
   BEGIN   
      SELECT @tablename = RTRIM(@tablename); 
      EXEC ('SELECT ''' + @tablename + ''' = count(*) FROM ' 
            + @tablename );
      PRINT ' ';
   END;
   FETCH NEXT FROM tnames_cursor INTO @tablename;
END;
CLOSE tnames_cursor;
DEALLOCATE tnames_cursor;
--</snippet_WHILE_2>