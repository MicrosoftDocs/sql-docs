-- Change the password on the Distributor. 
-- To avoid storing the password in the script file, the value is passed 
-- into SQLCMD as a scripting variable. For information about how to use 
-- scripting variables on the command line and in SQL Server Management
-- Studio, see the "Executing Replication Scripts" section in the topic
-- "Programming Replication Using System Stored Procedures".
USE master
DECLARE @password nvarchar(50) = "YourLongP@$$w0rdHere"
EXEC sp_changedistributor_password @password
GO