--<snippetsp_dropsalesorderssub>
-- This batch is executed at the Subscriber to remove 
-- the Sales Orders sample subscription.
DECLARE @publisher AS sysname;

-- change this value to the name of the Publisher server.
SET @publisher = N'PUBSERVER'; 

USE [AdventureWorks2012Local]
EXEC sp_dropmergepullsubscription 
  @publisher = @publisher, 
  @publisher_db = N'AdventureWorks2012',
  @publication = N'AdvWorksSalesOrders';
GO
--</snippetsp_dropsalesorderssub>

--<snippetsp_dropsalesorderssubatpub>
-- This batch is executed at the Publisher to remove 
-- the Sales Orders sample subscription.
DECLARE @subscriber AS sysname;

-- change this value to the name of the Subscriber server.
SET @subscriber = N'SUBSERVER'; 

USE [AdventureWorks2012]
EXEC sp_dropmergesubscription 
  @publication = N'AdvWorksSalesOrders', 
  @subscriber = @subscriber, 
  @subscriber_db = N'AdventureWorks2012Local';
GO
--</snippetsp_dropsalesorderssubatpub>