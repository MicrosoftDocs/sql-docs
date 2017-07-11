
-- Change the heartbeat interval at the Distributor to 5 minutes. 
USE master 
exec sp_changedistributor_property 
    @property = N'heartbeat_interval', 
    @value = 5;
GO