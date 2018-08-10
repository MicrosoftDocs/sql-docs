--<snippetFilteredStats1>
USE AdventureWorks2012;
GO
SELECT OBJECT_NAME(s.object_id) AS object_name,
    COL_NAME(sc.object_id, sc.column_id) AS column_name,
    s.name AS statistics_name
FROM sys.stats AS s Join sys.stats_columns AS sc
    ON s.stats_id = sc.stats_id
WHERE s.name like '_WA%'
ORDER BY s.name;
GO
--</snippetFilteredStats1>

--<snippetFilteredStats2>
USE AdventureWorks2012;
GO
IF EXISTS ( SELECT name FROM sys.stats
    WHERE name = 'BikeWeights'
    AND object_ID = OBJECT_ID ('Production.Product'))
DROP STATISTICS Production.Product.BikeWeights;
GO
CREATE STATISTICS BikeWeights
    ON Production.Product (Weight)
WHERE ProductSubcategoryID IN (1,2,3);
GO
--</snippetFilteredStats2>