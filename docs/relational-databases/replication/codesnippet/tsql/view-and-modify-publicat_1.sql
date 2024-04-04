DECLARE @myTranPub AS sysname
SET @myTranPub = N'AdvWorksProductTran' 

USE [AdventureWorks2022]
EXEC sp_helppublication @publication = @myTranPub
GO