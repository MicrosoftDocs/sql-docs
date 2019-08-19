ALTER INDEX AK_SalesOrderHeader_SalesOrderNumber ON
    Sales.SalesOrderHeader
SET (
    STATISTICS_NORECOMPUTE = ON,
    IGNORE_DUP_KEY = ON,
    ALLOW_PAGE_LOCKS = ON
    )
;